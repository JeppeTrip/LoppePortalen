import { action, makeAutoObservable, observable } from "mobx"
import { ModelState } from "../../@types/ModelState"
import { BoothBaseVM as Dto, FileParameter, UpdateBoothRequest, GetBoothVM } from "../../services/clients"
import { BoothStore } from "../stores/BoothStore"
import { Merchant } from "./Merchant"
import { Stall } from "./Stall"


export class Booth {
    store: BoothStore
    @observable state: symbol
    @observable id: string
    @observable name: string
    @observable description: string
    @observable stall: Stall
    @observable merchant: Merchant
    @observable itemCategories: string[]
    @observable imageData : string //base64 string representation of image data.

    set State(state : symbol) { this.state = state }
    set Id(id : string) {this.id = id}
    set Name(name : string) {this.name = name}
    set Description(description : string) {this.description = description }

    constructor(store: BoothStore) {
        makeAutoObservable(this)
        this.store = store
        this.stall = null
        this.merchant = null
        this.state = ModelState.NEW
        this.itemCategories = [] as string[]
    }

    @action
    updateFromServer(dto: Dto) {
        if (this.state != ModelState.UPDATING) {
            this.state = ModelState.UPDATING;
            this.id = dto.id
            this.name = dto.name
            this.description = dto.description
            this.itemCategories = dto.categories
            
            this.stall = this.store.rootStore.stallStore.updateStallFromServer(dto.stall)
            this.stall.booth = this

            if(dto instanceof GetBoothVM)
                this.updateFromServerGetBoothVM(dto)

            this.state = ModelState.IDLE
        }
        return this;
    }

    @action
    private updateFromServerGetBoothVM(dto : GetBoothVM)
    {
        this.imageData = dto.imageData
    }

    @action
    save() {
        this.state = ModelState.SAVING
        this.store.transportLayer.updateBooth(new UpdateBoothRequest(
            {
                boothDescription: this.description,
                boothName: this.name,
                id: this.id,
                itemCategories: this.itemCategories
            })
        ).then(
            action("updateSuccess", res => {
                this.state = ModelState.IDLE
            }),
            action("updateError", error => {
                this.state = ModelState.ERROR
            })
        )
    }

    @action
    uploadBanner(file : File)
    {
        let fileParameter: FileParameter = { data: file, fileName: file.name };
        this.store.transportLayer.uploadBoothBanner(this.id, fileParameter)
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