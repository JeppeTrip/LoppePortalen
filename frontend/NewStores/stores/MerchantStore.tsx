import { action, flow, makeAutoObservable, observable } from "mobx"
import { ModelState } from "../../@types/ModelState"
import { MerchantClient } from "../../services/clients"
import { Merchant } from "../@DomainObjects/Merchant"
import { RootStore } from "../RootStore"
import { Merchant as Dto } from "../../services/clients"

export class MerchantStore {
    rootStore: RootStore
    transportLayer: MerchantClient
    @observable merchants: Merchant[]

    constructor(rootStore: RootStore, transportLayer: MerchantClient) {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
        this.merchants = [] as Merchant[]
    }

    /**
     * Create client side merchant instance. Doesn't update db.
     * @returns merchant instance.
     */
    createMerchant() {
        const merchant = new Merchant(this)
        this.merchants.push(merchant)
        return merchant
    }

    /**
     * Tries to submit merchant to the database.
     */
    @action
    saveMerchant(merchant: Merchant) {
        if (!merchant.id) {
            merchant.state = ModelState.SAVING
            this.transportLayer.createMerchant({
                name: merchant.name,
                description: merchant.description
            }).then(
                action("submitSuccess", res => {
                    if (res.succeeded) {
                        this.updateMerchantFromServer({
                            id: res.merchant.id,
                            name: res.merchant.name,
                            description: res.merchant.description,
                            userId: res.merchant.userId
                        })
                    }
                    else {
                        merchant.state = ModelState.ERROR
                    }

                }),
                action("submitError", error => {
                    merchant.state = ModelState.ERROR
                })
            )
        }
    }


    /**
     * Call when updates are arriving from the server.
     * Check if merchant is already in store if not then add it.
     * Then call updateFromServer on the merchant instance to update its data.
     */
    @action
    updateMerchantFromServer(dto: Dto) {
        let merchant = this.merchants.find(x => x.id === dto.id);
        if (!merchant) {
            merchant = this.createMerchant()
        }
        merchant.updateFromServer(dto);
        return merchant
    }

    /**
     * Force fetch all merchants from the server.
     */
    @action
    getAllMerchants() {
        this.transportLayer.getAllMerchants()
            .then(
                action("fetchSuccess", res => {
                    if (res.succeeded) {
                        res.merchantList.forEach(
                            x => this.updateMerchantFromServer({
                                id: x.id,
                                name: x.name,
                                description: x.description,
                                userId: x.userId
                            }))
                    }
                    else {
                        //do something with the error?
                    }

                }),
                action("fetchError", error => {
                    //do something with this
                })
            )
    }

    /**
     * Used to find a specific merchant.
     * Searches through the list of merchants currently held in the store
     * if found uses this value. 
     * If it isn't here a fetch is made.
     */
    @flow
    *resolveMerchant(id : number)
    {
        let merchant = this.merchants.find(x => x.id === id)
        if(!merchant)
        {
            var res = yield this.transportLayer.getMerchant(id)
            if(res.succeeded)
            {
                merchant = this.updateMerchantFromServer(res.merchant)
            }
        }
        return merchant
    }
}