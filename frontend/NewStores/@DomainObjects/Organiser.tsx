import { action, makeAutoObservable, observable } from "mobx";
import { Location } from "../../@types/Location";
import { ModelState } from "../../@types/ModelState";
import { AddOrganiserContactInformationRequest, CreateOrganiserRequest, EditOrganiserRequest, FileParameter, GetOrganiserVM, OrganiserBaseVM as Dto, RemoveContactInformationRequest } from "../../services/clients";
import { OrganiserStore } from "../stores/OrganiserStore";
import { ContactInfo } from "./ContactInfo";
import { Market } from "./Market";

export class Organiser {
    store: OrganiserStore
    @observable state: symbol
    @observable id: number = null
    @observable userId: string = null
    @observable name: string = ""
    @observable description: string = ""
    @observable markets: Market[]
    @observable contactInfo: ContactInfo[]
    @observable imageData : string //base64 string representation of image data.
    @observable street: string = ""
    @observable streetNumber: string = ""
    @observable appartment: string = ""
    @observable postalCode: string = ""
    @observable city: string = ""

    set Name(name: string) { this.name = name }
    set Description(description: string) { this.description = description }
    set Street(street: string) { this.street = street }
    set StreetNumber(streetNumber: string) { this.streetNumber = streetNumber }
    set Appartment(appartment: string) { this.appartment = appartment }
    set PostalCode(postalCode: string) { this.postalCode = postalCode }
    set City(city: string) { this.city = city }

    constructor(store: OrganiserStore, id?: number) {
        makeAutoObservable(this)
        this.store = store
        this.id = id
        this.userId = ""
        this.markets = [] as Market[]
        this.state = ModelState.NEW
        this.contactInfo = [] as ContactInfo[]
    }

    /**
     * Push market instance into the markets list if it doesn't exist there already.
     */
    @action
    addMarket(market: Market) {
        const res = this.markets.find(x => x.id === market.id)
        if (!res)
            this.markets.push(market)
    }

    /**
     * Updates the entity itself based on the generated model in the backend.
     */
    @action
    updateFromServer(dto: Dto) {
        if (this.state != ModelState.UPDATING) {
            this.state = ModelState.UPDATING
            this.id = dto.id
            this.userId = dto.userId
            this.name = dto.name
            this.description = dto.description
            this.street = dto.street
            this.streetNumber = dto.streetNumber
            this.appartment = dto.appartment
            this.postalCode = dto.postalCode
            this.city = dto.city

            if (dto instanceof GetOrganiserVM)
                this.updateFromServerGetOrganiserVm(dto);

            this.state = ModelState.IDLE
        }
        return this;
    }

    @action
    private updateFromServerGetOrganiserVm(dto: GetOrganiserVM) {
        let currentMarket: Market;
        dto.markets.forEach(x => {
            currentMarket = this.store.rootStore.marketStore.updateMarketFromServer(x)
            currentMarket.organiser = this
            this.markets.push(currentMarket)
        })

        let currentContact: ContactInfo
        dto.contacts.forEach(contactDto => {
            currentContact = this.contactInfo.find(x => contactDto.value === x.value)

            if (!currentContact) {
                currentContact = new ContactInfo()
                this.contactInfo.push(currentContact)
            }

            currentContact.type = contactDto.type
            currentContact.value = contactDto.value
            currentContact.state = ModelState.IDLE
        })

        this.imageData = dto.imageData
    }

    @action
    select() {
        this.store.selectedOrganiser = this;
    }

    /**
     * submit things to the server.
     */
    @action
    save() {
        this.state = ModelState.SAVING
        if (!this.id) {
            this.store.transportLayer.createOrganiser(new CreateOrganiserRequest({
                name: this.name,
                description: this.description,
                street: this.street,
                number: this.streetNumber,
                appartment: this.appartment,
                city: this.city,
                postalCode: this.postalCode
            })).then(
                action("submitSuccess", res => {
                    this.id = res.id,
                        this.store.organisers.push(this);
                    this.state = ModelState.IDLE
                }),
                action("submitError", error => {
                    this.state = ModelState.ERROR
                })
            )
        }
        else {
            this.store.transportLayer.editOrganiser(new EditOrganiserRequest({
                organiserId: this.id,
                name: this.name,
                description: this.description,
                street: this.street,
                number: this.streetNumber,
                appartment: this.appartment,
                city: this.city,
                postalCode: this.postalCode
            }))
                .then(
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
    addContactInfo(contactInfo: ContactInfo) {
        this.store.transportLayer.addContactInformation(new AddOrganiserContactInformationRequest({
            organiserId: this.id,
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
        this.store.transportLayer.removeContactInformation(new RemoveContactInformationRequest({
            organiserId: this.id,
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

    @action
    uploadBanner(file : File)
    {
        let fileParameter: FileParameter = { data: file, fileName: file.name };
        this.store.transportLayer.uploadOrganiserBanner(this.id, fileParameter)
        .then(
            action("submitSuccess", res => {
                console.log("do nothing I guess?")
            }),
            action("submitError", error => {
                this.state = ModelState.ERROR
            })
        )
    }
}