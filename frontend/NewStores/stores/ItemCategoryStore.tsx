import { action, flow, makeAutoObservable, observable } from "mobx";
import { Booth } from "../@DomainObjects/Booth";
import { RootStore } from "../RootStore";
import { BoothBaseVM as Dto, BoothClient, ItemCategoryClient} from '../../services/clients';

export class ItemCategoryStore {
    rootStore: RootStore
    transportLayer : ItemCategoryClient
    @observable categories: string[]

    constructor(rootStore: RootStore, transportLayer : ItemCategoryClient) {
        makeAutoObservable(this)
        this.rootStore = rootStore
        this.transportLayer = transportLayer
    }

    @flow 
    *fetchCategories()
    {
        const res = yield this.transportLayer.getAllItemCategories();
        this.categories = res
    }
}