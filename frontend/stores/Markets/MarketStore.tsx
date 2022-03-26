import { action, makeAutoObservable } from 'mobx';
import * as React from 'react';
import { MarketContextType, IMarket } from '../../@types/Market';
import { MarketClient } from '../models';
import { RootStore } from '../RootStore';

class MarketStore {
    rootStore: RootStore;
    markets: IMarket[] = [];
    selectedMarket: IMarket = null;
    isLoading = true;

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        this.rootStore = rootStore;
        //Don't actually think this is a good idea. Since all data will be loaded at once.
        this.loadMarkets();
    }

    loadMarkets() {
        this.isLoading = true;
        //TODO: Replace with a fetch, just have placeholder data for now.
        this.markets = [
            {
                id: 1,
                organiserId: 1,
                name: "Amazing Market",
                startDate: new Date("2022/01/01"),
                endDate: new Date("2022/01/10"),
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris at erat orci. Praesent sed viverra dui. Donec ullamcorper lacus vel ornare semper. Suspendisse viverra, leo in iaculis luctus, est sapien iaculis odio, eu ultricies sem ligula nec leo. Quisque posuere orci eget gravida auctor. Proin odio leo, congue quis dui et, imperdiet pellentesque erat. Sed euismod eget magna ut lobortis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nulla maximus auctor turpis sit amet bibendum. Mauris gravida, magna ullamcorper ultrices ultrices, mi ligula mattis odio, sed sodales ligula sem at tellus. Cras lobortis tellus eget dictum hendrerit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris congue rutrum iaculis. Cras sed interdum diam, sed ornare turpis. Etiam finibus odio in viverra sagittis. Nulla tincidunt pharetra malesuada. Ut tellus urna, condimentum in sapien sit amet, molestie tempus arcu. Nulla ut gravida augue, at condimentum diam. Nullam egestas aliquet dolor. Sed justo ante, interdum non ullamcorper eget, luctus quis sapien. Integer id pellentesque mauris, in varius metus. Donec eros ante, mollis at cursus aliquet, volutpat vitae nisi. Etiam ullamcorper eros vel pulvinar pellentesque. Morbi eros turpis, varius vitae accumsan ut, fermentum ac leo. Proin dictum malesuada metus, nec egestas orci lacinia eu. Suspendisse ut nibh at eros imperdiet pellentesque et ut velit. Nulla eleifend eget enim a volutpat. Sed sodales pharetra felis, vitae pulvinar sem mattis quis. Morbi eget quam ante. Mauris quis purus mauris. Praesent commodo sapien vel lorem sodales vehicula. Donec non orci nec sapien imperdiet accumsan sed quis lectus. Donec quis metus ligula. Suspendisse interdum varius ultrices. Vivamus leo eros, ullamcorper eu nunc eget, laoreet euismod ipsum. Nunc vehicula vel urna id accumsan. In tincidunt aliquet mauris. Sed a ex vitae est pulvinar molestie et et enim. Aliquam in sem id nisl vestibulum sollicitudin nec venenatis tellus. Duis urna risus, elementum id urna id, laoreet cursus turpis. Suspendisse rhoncus magna at mauris ultricies, at sagittis lorem hendrerit. Pellentesque nec nisl id tortor fringilla sollicitudin sit amet vel orci. Proin pharetra finibus ligula, bibendum ornare eros feugiat ac. Donec eget condimentum sem. Praesent neque lectus, sagittis ut egestas et, tempus eget ante. In in mauris eu dolor tristique sodales et sit amet turpis. Nam sit amet neque vitae est sodales viverra. Nulla facilisi. Curabitur imperdiet lacinia facilisis. Aliquam malesuada nec eros vel lobortis. Donec nec dolor felis. Praesent mollis metus mi, id vulputate nulla elementum eget. Integer consequat, arcu non facilisis eleifend, risus enim vestibulum est, id pretium sapien augue eu enim. "
            },
            {
                id: 2,
                organiserId: 1,
                name: "Brunhildas Knitting Extravaganza",
                startDate: new Date("2022/01/01"),
                endDate: new Date("2022/01/10"),
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris at erat orci. Praesent sed viverra dui. Donec ullamcorper lacus vel ornare semper. Suspendisse viverra, leo in iaculis luctus, est sapien iaculis odio, eu ultricies sem ligula nec leo. Quisque posuere orci eget gravida auctor. Proin odio leo, congue quis dui et, imperdiet pellentesque erat. Sed euismod eget magna ut lobortis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nulla maximus auctor turpis sit amet bibendum. Mauris gravida, magna ullamcorper ultrices ultrices, mi ligula mattis odio, sed sodales ligula sem at tellus. Cras lobortis tellus eget dictum hendrerit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris congue rutrum iaculis. Cras sed interdum diam, sed ornare turpis. Etiam finibus odio in viverra sagittis. Nulla tincidunt pharetra malesuada. Ut tellus urna, condimentum in sapien sit amet, molestie tempus arcu. Nulla ut gravida augue, at condimentum diam. Nullam egestas aliquet dolor. Sed justo ante, interdum non ullamcorper eget, luctus quis sapien. Integer id pellentesque mauris, in varius metus. Donec eros ante, mollis at cursus aliquet, volutpat vitae nisi. Etiam ullamcorper eros vel pulvinar pellentesque. Morbi eros turpis, varius vitae accumsan ut, fermentum ac leo. Proin dictum malesuada metus, nec egestas orci lacinia eu. Suspendisse ut nibh at eros imperdiet pellentesque et ut velit. Nulla eleifend eget enim a volutpat. Sed sodales pharetra felis, vitae pulvinar sem mattis quis. Morbi eget quam ante. Mauris quis purus mauris. Praesent commodo sapien vel lorem sodales vehicula. Donec non orci nec sapien imperdiet accumsan sed quis lectus. Donec quis metus ligula. Suspendisse interdum varius ultrices. Vivamus leo eros, ullamcorper eu nunc eget, laoreet euismod ipsum. Nunc vehicula vel urna id accumsan. In tincidunt aliquet mauris. Sed a ex vitae est pulvinar molestie et et enim. Aliquam in sem id nisl vestibulum sollicitudin nec venenatis tellus. Duis urna risus, elementum id urna id, laoreet cursus turpis. Suspendisse rhoncus magna at mauris ultricies, at sagittis lorem hendrerit. Pellentesque nec nisl id tortor fringilla sollicitudin sit amet vel orci. Proin pharetra finibus ligula, bibendum ornare eros feugiat ac. Donec eget condimentum sem. Praesent neque lectus, sagittis ut egestas et, tempus eget ante. In in mauris eu dolor tristique sodales et sit amet turpis. Nam sit amet neque vitae est sodales viverra. Nulla facilisi. Curabitur imperdiet lacinia facilisis. Aliquam malesuada nec eros vel lobortis. Donec nec dolor felis. Praesent mollis metus mi, id vulputate nulla elementum eget. Integer consequat, arcu non facilisis eleifend, risus enim vestibulum est, id pretium sapien augue eu enim. "
            },
            {
                id: 3,
                organiserId: 1,
                name: "Thorbjørns Smithing Supplies",
                startDate: new Date("2022/01/01"),
                endDate: new Date("2022/01/10"),
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris at erat orci. Praesent sed viverra dui. Donec ullamcorper lacus vel ornare semper. Suspendisse viverra, leo in iaculis luctus, est sapien iaculis odio, eu ultricies sem ligula nec leo. Quisque posuere orci eget gravida auctor. Proin odio leo, congue quis dui et, imperdiet pellentesque erat. Sed euismod eget magna ut lobortis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nulla maximus auctor turpis sit amet bibendum. Mauris gravida, magna ullamcorper ultrices ultrices, mi ligula mattis odio, sed sodales ligula sem at tellus. Cras lobortis tellus eget dictum hendrerit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris congue rutrum iaculis. Cras sed interdum diam, sed ornare turpis. Etiam finibus odio in viverra sagittis. Nulla tincidunt pharetra malesuada. Ut tellus urna, condimentum in sapien sit amet, molestie tempus arcu. Nulla ut gravida augue, at condimentum diam. Nullam egestas aliquet dolor. Sed justo ante, interdum non ullamcorper eget, luctus quis sapien. Integer id pellentesque mauris, in varius metus. Donec eros ante, mollis at cursus aliquet, volutpat vitae nisi. Etiam ullamcorper eros vel pulvinar pellentesque. Morbi eros turpis, varius vitae accumsan ut, fermentum ac leo. Proin dictum malesuada metus, nec egestas orci lacinia eu. Suspendisse ut nibh at eros imperdiet pellentesque et ut velit. Nulla eleifend eget enim a volutpat. Sed sodales pharetra felis, vitae pulvinar sem mattis quis. Morbi eget quam ante. Mauris quis purus mauris. Praesent commodo sapien vel lorem sodales vehicula. Donec non orci nec sapien imperdiet accumsan sed quis lectus. Donec quis metus ligula. Suspendisse interdum varius ultrices. Vivamus leo eros, ullamcorper eu nunc eget, laoreet euismod ipsum. Nunc vehicula vel urna id accumsan. In tincidunt aliquet mauris. Sed a ex vitae est pulvinar molestie et et enim. Aliquam in sem id nisl vestibulum sollicitudin nec venenatis tellus. Duis urna risus, elementum id urna id, laoreet cursus turpis. Suspendisse rhoncus magna at mauris ultricies, at sagittis lorem hendrerit. Pellentesque nec nisl id tortor fringilla sollicitudin sit amet vel orci. Proin pharetra finibus ligula, bibendum ornare eros feugiat ac. Donec eget condimentum sem. Praesent neque lectus, sagittis ut egestas et, tempus eget ante. In in mauris eu dolor tristique sodales et sit amet turpis. Nam sit amet neque vitae est sodales viverra. Nulla facilisi. Curabitur imperdiet lacinia facilisis. Aliquam malesuada nec eros vel lobortis. Donec nec dolor felis. Praesent mollis metus mi, id vulputate nulla elementum eget. Integer consequat, arcu non facilisis eleifend, risus enim vestibulum est, id pretium sapien augue eu enim. "
            },
            {
                id: 4,
                organiserId: 1,
                name: "Knudsker IF Loppemarked",
                startDate: new Date("2022/01/01"),
                endDate: new Date("2022/01/10"),
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris at erat orci. Praesent sed viverra dui. Donec ullamcorper lacus vel ornare semper. Suspendisse viverra, leo in iaculis luctus, est sapien iaculis odio, eu ultricies sem ligula nec leo. Quisque posuere orci eget gravida auctor. Proin odio leo, congue quis dui et, imperdiet pellentesque erat. Sed euismod eget magna ut lobortis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nulla maximus auctor turpis sit amet bibendum. Mauris gravida, magna ullamcorper ultrices ultrices, mi ligula mattis odio, sed sodales ligula sem at tellus. Cras lobortis tellus eget dictum hendrerit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris congue rutrum iaculis. Cras sed interdum diam, sed ornare turpis. Etiam finibus odio in viverra sagittis. Nulla tincidunt pharetra malesuada. Ut tellus urna, condimentum in sapien sit amet, molestie tempus arcu. Nulla ut gravida augue, at condimentum diam. Nullam egestas aliquet dolor. Sed justo ante, interdum non ullamcorper eget, luctus quis sapien. Integer id pellentesque mauris, in varius metus. Donec eros ante, mollis at cursus aliquet, volutpat vitae nisi. Etiam ullamcorper eros vel pulvinar pellentesque. Morbi eros turpis, varius vitae accumsan ut, fermentum ac leo. Proin dictum malesuada metus, nec egestas orci lacinia eu. Suspendisse ut nibh at eros imperdiet pellentesque et ut velit. Nulla eleifend eget enim a volutpat. Sed sodales pharetra felis, vitae pulvinar sem mattis quis. Morbi eget quam ante. Mauris quis purus mauris. Praesent commodo sapien vel lorem sodales vehicula. Donec non orci nec sapien imperdiet accumsan sed quis lectus. Donec quis metus ligula. Suspendisse interdum varius ultrices. Vivamus leo eros, ullamcorper eu nunc eget, laoreet euismod ipsum. Nunc vehicula vel urna id accumsan. In tincidunt aliquet mauris. Sed a ex vitae est pulvinar molestie et et enim. Aliquam in sem id nisl vestibulum sollicitudin nec venenatis tellus. Duis urna risus, elementum id urna id, laoreet cursus turpis. Suspendisse rhoncus magna at mauris ultricies, at sagittis lorem hendrerit. Pellentesque nec nisl id tortor fringilla sollicitudin sit amet vel orci. Proin pharetra finibus ligula, bibendum ornare eros feugiat ac. Donec eget condimentum sem. Praesent neque lectus, sagittis ut egestas et, tempus eget ante. In in mauris eu dolor tristique sodales et sit amet turpis. Nam sit amet neque vitae est sodales viverra. Nulla facilisi. Curabitur imperdiet lacinia facilisis. Aliquam malesuada nec eros vel lobortis. Donec nec dolor felis. Praesent mollis metus mi, id vulputate nulla elementum eget. Integer consequat, arcu non facilisis eleifend, risus enim vestibulum est, id pretium sapien augue eu enim. "
            },
            {
                id: 5,
                organiserId: 1,
                name: "Odins Exclusive Eye Auction",
                startDate: new Date("2022/01/01"),
                endDate: new Date("2022/01/10"),
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris at erat orci. Praesent sed viverra dui. Donec ullamcorper lacus vel ornare semper. Suspendisse viverra, leo in iaculis luctus, est sapien iaculis odio, eu ultricies sem ligula nec leo. Quisque posuere orci eget gravida auctor. Proin odio leo, congue quis dui et, imperdiet pellentesque erat. Sed euismod eget magna ut lobortis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nulla maximus auctor turpis sit amet bibendum. Mauris gravida, magna ullamcorper ultrices ultrices, mi ligula mattis odio, sed sodales ligula sem at tellus. Cras lobortis tellus eget dictum hendrerit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris congue rutrum iaculis. Cras sed interdum diam, sed ornare turpis. Etiam finibus odio in viverra sagittis. Nulla tincidunt pharetra malesuada. Ut tellus urna, condimentum in sapien sit amet, molestie tempus arcu. Nulla ut gravida augue, at condimentum diam. Nullam egestas aliquet dolor. Sed justo ante, interdum non ullamcorper eget, luctus quis sapien. Integer id pellentesque mauris, in varius metus. Donec eros ante, mollis at cursus aliquet, volutpat vitae nisi. Etiam ullamcorper eros vel pulvinar pellentesque. Morbi eros turpis, varius vitae accumsan ut, fermentum ac leo. Proin dictum malesuada metus, nec egestas orci lacinia eu. Suspendisse ut nibh at eros imperdiet pellentesque et ut velit. Nulla eleifend eget enim a volutpat. Sed sodales pharetra felis, vitae pulvinar sem mattis quis. Morbi eget quam ante. Mauris quis purus mauris. Praesent commodo sapien vel lorem sodales vehicula. Donec non orci nec sapien imperdiet accumsan sed quis lectus. Donec quis metus ligula. Suspendisse interdum varius ultrices. Vivamus leo eros, ullamcorper eu nunc eget, laoreet euismod ipsum. Nunc vehicula vel urna id accumsan. In tincidunt aliquet mauris. Sed a ex vitae est pulvinar molestie et et enim. Aliquam in sem id nisl vestibulum sollicitudin nec venenatis tellus. Duis urna risus, elementum id urna id, laoreet cursus turpis. Suspendisse rhoncus magna at mauris ultricies, at sagittis lorem hendrerit. Pellentesque nec nisl id tortor fringilla sollicitudin sit amet vel orci. Proin pharetra finibus ligula, bibendum ornare eros feugiat ac. Donec eget condimentum sem. Praesent neque lectus, sagittis ut egestas et, tempus eget ante. In in mauris eu dolor tristique sodales et sit amet turpis. Nam sit amet neque vitae est sodales viverra. Nulla facilisi. Curabitur imperdiet lacinia facilisis. Aliquam malesuada nec eros vel lobortis. Donec nec dolor felis. Praesent mollis metus mi, id vulputate nulla elementum eget. Integer consequat, arcu non facilisis eleifend, risus enim vestibulum est, id pretium sapien augue eu enim. "
            }
        ]
        this.isLoading = false;

    }

    @action
    setSelectedMarket(market: IMarket) {
        this.selectedMarket = market;
    }

    getMarket(id : number) : IMarket
    {
        return this.markets.find(market => market.id === id);
    }

    @action
    addNewMarket(market : IMarket){
        market.id = Math.floor(Math.random()*1000)
        this.markets.push(market);
        return market.id;
    }
}

export { MarketStore }