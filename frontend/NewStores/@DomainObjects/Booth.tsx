import { action, makeAutoObservable, observable } from "mobx"
import { BoothStore } from "../stores/BoothStore"
import { BoothBaseVM as Dto, GetFilteredBoothsVM, UpdateBoothRequest } from "../../services/clients"
import { Stall } from "./Stall"
import { ModelState } from "../../@types/ModelState"
import { Merchant } from "./Merchant"
import be from "date-fns/esm/locale/be/index.js"
import { BreakfastDining } from "@mui/icons-material"

export class Booth {
    store: BoothStore
    @observable state: symbol
    @observable id: string
    @observable name: string
    @observable description: string
    @observable stall: Stall
    @observable merchant: Merchant
    @observable itemCategories: string[]

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
            switch (dto.constructor.name) {
                case "GetFilteredBoothsVM":
                    this.updateFromServerGetFilteredBoothsVM(dto)
                    break;
                default:
                    this.stall = this.store.rootStore.stallStore.updateStallFromServer(dto.stall)
                    this.stall.booth = this
                    break;
            }

            this.state = ModelState.IDLE
        }
        return this;
    }

    @action
    private updateFromServerGetFilteredBoothsVM(dto : GetFilteredBoothsVM)
    {
        this.stall = this.store.rootStore.stallStore.updateStallFromServer(dto.stall)
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
}