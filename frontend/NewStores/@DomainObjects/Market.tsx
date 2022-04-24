import { action, computed, makeAutoObservable, observable } from "mobx";
import { ModelState } from "../../@types/ModelState";
import { BookStallsRequest, CreateMarketRequest, EditMarketRequest, GetAllMarketsVM, GetMarketInstanceVM, MarketBaseVM as Dto, StallBooking, UsersMarketsVM } from "../../services/clients";
import { MarketStore } from "../stores/MarketStore";
import { Booth } from "./Booth";
import { Organiser } from "./Organiser";
import { Stall } from "./Stall";
import { StallType } from "./StallType";

export class Market {
    store: MarketStore
    @observable state: symbol
    @observable oldState: Market
    @observable id: number
    @observable organiser: Organiser
    @observable name: string
    @observable description: string
    @observable startDate: Date
    @observable endDate: Date
    @observable isCancelled: boolean
    @observable stallTypes: StallType[]
    @observable stalls: Stall[]
    @observable booths: Booth[]

    @action
    set setId(id: number) {
        this.id = id;
    }

    @action
    set setOrganiserId(id: number) {
        this.id = id;
    }

    @action
    set setName(name: string) {
        this.name = name
    }

    @action
    set setDescription(description: string) {
        this.description = description
    }

    @action
    set setStartDate(date: Date) {
        this.startDate = date
    }

    @action
    set setEndDate(date: Date) {
        this.startDate = date
    }

    @action
    set setIsCancelled(isCancelled: boolean) {
        this.isCancelled = isCancelled
    }

    constructor(store: MarketStore) {
        makeAutoObservable(this);
        this.state = ModelState.NEW
        this.store = store
        this.stallTypes = [] as StallType[]
        this.startDate = null
        this.endDate = null
        this.oldState = null
        this.organiser = null
        this.stalls = [] as Stall[]
        this.booths = [] as Booth[]
    }

    @action
    updateFromServer(dto: Dto) {
        if (this.state != ModelState.UPDATING) {
            this.state = ModelState.UPDATING
            this.id = dto.marketId
            this.name = dto.marketName
            this.description = dto.description
            this.startDate = new Date(dto.startDate)
            this.endDate = new Date(dto.endDate)
            this.isCancelled = dto.isCancelled
            switch (dto.constructor.name) {
                case "GetAllMarketsVM":
                    this.updateFromServerGetAllMarketsVM(dto)
                    break;
                case "GetMarketInstanceVM":
                    this.updateFromServerGetMarketInstanceVM(dto)
                    break;
                case "UsersMarketsVM":
                    this.updateFromServerGetUsersMarketsVM(dto)
                    break;
            }
            this.state = ModelState.IDLE
        }
        return this;
    }

    @action
    private updateFromServerGetAllMarketsVM(dto: GetAllMarketsVM) {
        const organiser = this.store.rootStore.organiserStore.updateOrganiserFromServer(dto.organiser)
        if (this.organiser == null || this.organiser.id != organiser.id)
            this.organiser = organiser
        this.organiser.addMarket(this)
    }

    @action
    private updateFromServerGetMarketInstanceVM(dto: GetMarketInstanceVM) {
        const organiser = this.store.rootStore.organiserStore.updateOrganiserFromServer(dto.organiser)
        if (this.organiser == null || this.organiser.id != organiser.id)
            this.organiser = organiser
        this.organiser.addMarket(this)

        //Update stalltypes based on the releated VM
        dto.stallTypes.forEach(x => {
            const stallType = this.store.rootStore.stallTypeStore.updateStallTypeFromServer(x)
            stallType.market = this
            const instance = this.stallTypes.find(x => x.id === stallType.id)
            if(!instance)
                this.stallTypes.push(stallType)
        });

        //Update the stalls based on the related VM.
        dto.stalls.forEach(x => {
            const stall = this.store.rootStore.stallStore.updateStallFromServer(x)
            stall.market = this
            const instance = this.stalls.find(x => x.id === stall.id)
            if(!instance)
                this.stalls.push(stall)
        })

        //Update  the booths based on the related VM.
        dto.booths.forEach(x => {
            const booth = this.store.rootStore.boothStore.updateBoothFromServer(x)
            if(!booth.stall.market || booth.stall.market == null)
                booth.stall.market = this
            const instance = this.booths.find(x => x.id === booth.id)
            if(!instance)
                this.booths.push(booth)
        });
    }

    @action
    private updateFromServerGetUsersMarketsVM(dto: UsersMarketsVM) {
        const organiser = this.store.rootStore.organiserStore.updateOrganiserFromServer(dto.organiser)
        if (this.organiser == null || this.organiser.id != organiser.id)
            this.organiser = organiser
        this.organiser.addMarket(this)
    }

    @action
    select() {
        this.store.selectedMarket = this;
    }

    @action
    deselect() {
        if (this.store.selectedMarket?.id === this.id) {
            this.store.selectedMarket = null;
        }
    }

    @action
    save() {
        this.state = ModelState.SAVING
        if (!this.id) {
            this.store.transportLayer.createMarket(new CreateMarketRequest({
                organiserId: this.organiser?.id,
                marketName: this.name,
                description: this.description,
                startDate: this.startDate,
                endDate: this.endDate
            })).then(
                action("submitSuccess", res => {
                    this.id = res.market.marketId,
                        this.setOldState();
                    this.state = ModelState.IDLE
                }),
                action("submitError", error => {
                    this.state = ModelState.ERROR
                })
            )
        }
        else {
            this.store.transportLayer.updateMarket(new EditMarketRequest({
                marketId: this.id,
                organiserId: this.organiser?.id,
                marketName: this.name,
                description: this.description,
                startDate: this.startDate,
                endDate: this.endDate,
            })).then(
                action("submitSuccess", res => {
                    if (res.succeeded) {
                        this.setOldState();
                        this.state = ModelState.IDLE
                    }
                    else {
                        this.state = ModelState.ERROR
                    }
                }),
                action("submitError", error => {
                    this.state = ModelState.ERROR
                })
            )
        }
    }

    @action
    addNewStallType() {
        const type = this.store.rootStore.stallTypeStore.createStallType()
        type.market = this
        this.stallTypes.push(type);
        return type;
    }

    /**
     * Used internally to store the state of the component.
     * This is used for editing if you want to cancel your changes.
     */
    private setOldState() {
        const state = new Market(null);
        state.name = this.name;
        state.description = this.description;
        state.startDate = new Date(this.startDate);
        state.endDate = new Date(this.endDate);
        state.isCancelled = this.isCancelled;
        this.oldState = state;
    }

    @action
    resetState() {
        if (this.oldState && this.oldState != null) {
            this.name = this.oldState.name;
            this.description = this.oldState.description;
            this.startDate = new Date(this.oldState.startDate);
            this.endDate = new Date(this.oldState.endDate);
            this.isCancelled = this.oldState.isCancelled;
        }
    }

    @computed
    get savedStallTypes() {
        return this.stallTypes.filter(x => x.id > 0)
    }

    /**
     * Remove stall with the given id from the stall list of this market.
     * Internal change only. Nothing comitted to the database.
     */
    @action
    removeStall(id: number) {
        this.stalls = this.stalls.filter(x => x.id != id)
    }

    @action
    bookStalls(merchantId: number) {
        this.state = ModelState.UPDATING
        this.store.transportLayer.bookStalls(new BookStallsRequest({
            marketId: this.id,
            merchantId: merchantId,
            stalls: this.stallTypes.filter(x => x.bookingCount > 0).map(x => {
                return new StallBooking({ stallTypeId: x.id, bookingAmount: x.bookingCount })
            })
        })).then(
            action("bookingSuccess", res => {
                if (res.succeeded) {
                    this.state = ModelState.IDLE
                }
                else {
                    this.state = ModelState.ERROR
                }
            }),
            action("bookingError", error => {
                this.state = ModelState.ERROR
            })
        )
    }
}