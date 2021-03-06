import { action, flow, makeAutoObservable, observable } from "mobx"
import { GetMerchantQueryResponse, MerchantBaseVM as Dto, MerchantClient } from "../../services/clients"
import { Merchant } from "../@DomainObjects/Merchant"
import { RootStore } from "../RootStore"

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
    @action
    createMerchant() {
        const merchant = new Merchant(this)
        this.merchants.push(merchant)
        return merchant
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
                    res.merchantList.forEach(
                        x => this.updateMerchantFromServer(x))
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
    *resolveMerchant(id: number) {
        let merchant = this.merchants.find(x => x.id === id)
        if (!merchant) {
            var res : GetMerchantQueryResponse = yield this.transportLayer.getMerchant(id)
            merchant = this.updateMerchantFromServer(res.merchant)
        }
        return merchant
    }

    @flow
    *fetchMerchant(id: number) {
            var res : GetMerchantQueryResponse = yield this.transportLayer.getMerchant(id)
            const merchant = this.updateMerchantFromServer(res.merchant)
            return merchant
    }
}