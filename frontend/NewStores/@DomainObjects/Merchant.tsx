import { action, makeAutoObservable, observable } from "mobx"
import { ModelState } from "../../@types/ModelState"
import { MerchantStore } from "../stores/MerchantStore"
import { AddMerchantContactInformationRequest, CreateMerchantRequest, EditMerchantRequest, GetMerchantVM, MerchantBaseVM as Dto, RemoveMerchantContactRequest } from "../../services/clients"
import { Booth } from "./Booth"
import { ContactInfo } from "./ContactInfo"


export class Merchant {
    store: MerchantStore
    @observable _id: number
    @observable _state: ModelState
    @observable _name: string
    @observable _description: string
    @observable _userId: string
    @observable _oldState: Merchant
    @observable booths: Booth[]
    @observable contactInfo: ContactInfo[]

    constructor(store: MerchantStore) {
        makeAutoObservable(this)
        this.store = store
        this.state = ModelState.NEW
        this.name = ""
        this.description = ""
        this.booths = [] as Booth[]
        this.contactInfo = [] as ContactInfo[]
    }

    /**
     * Try and push this merchant to the server.
     * Updates state to saving.
     * If successful update state to idle. 
     * If error is encountered update state to error.
     */
    @action
    save() {
        if (!this.id) {
            this.state = ModelState.SAVING
            this.store.transportLayer.createMerchant(new CreateMerchantRequest({
                name: this.name,
                description: this.description
            })).then(
                action("submitSuccess", res => {
                    this.updateFromServer(res)
                }),
                action("submitError", error => {
                    this.state = ModelState.ERROR
                })
            )
        } else {
            this.state = ModelState.SAVING
            this.store.transportLayer.updateMerchant(new EditMerchantRequest({
                id: this.id,
                name: this.name,
                description: this.description
            })).then(
                action("submitSuccess", res => {
                    this.state = ModelState.IDLE
                }),
                action("submitError", error => {
                    this.state = ModelState.ERROR
                })
            )
        }
    }

    @action
    updateFromServer(dto: Dto) {
        if (this.state != ModelState.UPDATING) {
            this.state = ModelState.UPDATING
            this._id = dto.id
            this._userId = dto.userId
            this.name = dto.name
            this.description = dto.description
            if (dto instanceof GetMerchantVM)
                this.updateFromServerGetMerchantVM(dto);

            this._oldState = new Merchant(undefined)
            this.updateOldState()
            this.state = ModelState.IDLE
        }
        return this
    }

    private updateFromServerGetMerchantVM(dto: GetMerchantVM) {
        this.booths = []
        dto.booths.forEach(boothDto => {
            const booth = this.store.rootStore.boothStore.updateBoothFromServer(boothDto)
            booth.merchant = this
            this.booths.push(booth)
        });

        let currentContact: ContactInfo
        dto.contactInfo.forEach(contactDto => {
            currentContact = this.contactInfo.find(x => contactDto.value === x.value)

            if (!currentContact) {
                currentContact = new ContactInfo()
                this.contactInfo.push(currentContact)
            }

            currentContact.type = contactDto.type
            currentContact.value = contactDto.value
            currentContact.state = ModelState.IDLE
        })
    }

    /**
     * Reset the fields in this class to what is stored in the oldstate.
     */
    @action
    resetFields() {
        this._name = this.oldState._name
        this._description = this.oldState._description
    }

    /**
     * Update the oldstate field such that it contains the most recent fields.
     */
    @action
    updateOldState() {
        this.oldState._name = this.name
        this.oldState._description = this.description
    }

    @action
    addContactInfo(contactInfo: ContactInfo) {
        this.store.transportLayer.addContactInformation(new AddMerchantContactInformationRequest({
            merchantId: this.id,
            type: contactInfo.type,
            value: contactInfo.value
        }))
            .then(
                action("submitSuccess", res => {
                    this.contactInfo.push(contactInfo)
                    contactInfo.state = ModelState.IDLE
                }),
                action("submitError", error => {
                    contactInfo.state = ModelState.ERROR
                })
            )
    }

    @action
    deleteContactInfo(contactInfo: ContactInfo) {
        this.store.transportLayer.removeContactInformation(new RemoveMerchantContactRequest({
            merchantId: this.id,
            value: contactInfo.value
        }))
            .then(
                action("submitSuccess", res => {
                    this.contactInfo = this.contactInfo.filter(x => x.value != contactInfo.value)
                }),
                action("submitError", error => {
                    contactInfo.state = ModelState.ERROR
                })
            )
    }


    get state() {
        return this._state
    }

    set state(state: ModelState) {
        this._state = state
    }

    get name() {
        return this._name
    }

    set name(name: string) {
        this._name = name
    }

    get description() {
        return this._description
    }

    set description(description: string) {
        this._description = description
    }

    get id() {
        return this._id
    }

    get userId() {
        return this._userId
    }

    get oldState() {
        return this._oldState
    }


}