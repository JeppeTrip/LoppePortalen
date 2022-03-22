//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

export class ClientBase {
    baseApiUrl : string = "https://localhost:5001";

    protected async transformOptions(options: RequestInit): Promise<RequestInit>{
        const token = "";
        //TODO: Add authorization
        options.mode = 'cors';
        options.headers = {
            ...options.headers, /*, authorization: `Bearer ${token}` */};
        return Promise.resolve(options);
    }

    protected transformResult(url: string, response: Response, processor: (response: Response) => Promise<any>){
        return processor(response);
    }

    protected getBaseUrl(defaultUrl: string, baseUrl?: string){
        return this.baseApiUrl;
    }
}

export interface IMarketClient {

    createMarket(dto: CreateMarketRequest): Promise<CreateMarketResponse>;

    getMarketInstance(id: string | null): Promise<GetMarketInstanceQueryResponse>;

    getAllMarketInstances(): Promise<GetAllMarketInstancesQueryResponse[]>;

    editMarketInstance(dto: EditMarketInstanceRequest): Promise<EditMarketInstanceResponse>;
}

export class MarketClient extends ClientBase implements IMarketClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : window as any;
        this.baseUrl = this.getBaseUrl("", baseUrl);
    }

    createMarket(dto: CreateMarketRequest): Promise<CreateMarketResponse> {
        let url_ = this.baseUrl + "/api/Market/new";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(dto);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processCreateMarket(_response));
        });
    }

    protected processCreateMarket(response: Response): Promise<CreateMarketResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as CreateMarketResponse;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<CreateMarketResponse>(null as any);
    }

    getMarketInstance(id: string | null): Promise<GetMarketInstanceQueryResponse> {
        let url_ = this.baseUrl + "/api/Market/instance/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetMarketInstance(_response));
        });
    }

    protected processGetMarketInstance(response: Response): Promise<GetMarketInstanceQueryResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as GetMarketInstanceQueryResponse;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<GetMarketInstanceQueryResponse>(null as any);
    }

    getAllMarketInstances(): Promise<GetAllMarketInstancesQueryResponse[]> {
        let url_ = this.baseUrl + "/api/Market/instance/all";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetAllMarketInstances(_response));
        });
    }

    protected processGetAllMarketInstances(response: Response): Promise<GetAllMarketInstancesQueryResponse[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as GetAllMarketInstancesQueryResponse[];
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<GetAllMarketInstancesQueryResponse[]>(null as any);
    }

    editMarketInstance(dto: EditMarketInstanceRequest): Promise<EditMarketInstanceResponse> {
        let url_ = this.baseUrl + "/api/Market/instance/edit";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(dto);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processEditMarketInstance(_response));
        });
    }

    protected processEditMarketInstance(response: Response): Promise<EditMarketInstanceResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as EditMarketInstanceResponse;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<EditMarketInstanceResponse>(null as any);
    }
}

export interface IOrganiserClient {

    createOrganiser(dto: CreateOrganiserRequest): Promise<CreateOrganiserResponse>;

    addContactInformation(dto: AddContactsToOrganiserRequest): Promise<AddContactsToOrganiserResponse>;

    getAllOrganisers(): Promise<GetAllOrganisersResponse[]>;

    getOrganisers(pageNumber: number, pageSize: number): Promise<GetOrganisersWithPaginationResponse>;
}

export class OrganiserClient extends ClientBase implements IOrganiserClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : window as any;
        this.baseUrl = this.getBaseUrl("", baseUrl);
    }

    createOrganiser(dto: CreateOrganiserRequest): Promise<CreateOrganiserResponse> {
        let url_ = this.baseUrl + "/api/Organiser/new";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(dto);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processCreateOrganiser(_response));
        });
    }

    protected processCreateOrganiser(response: Response): Promise<CreateOrganiserResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as CreateOrganiserResponse;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<CreateOrganiserResponse>(null as any);
    }

    addContactInformation(dto: AddContactsToOrganiserRequest): Promise<AddContactsToOrganiserResponse> {
        let url_ = this.baseUrl + "/api/Organiser/add/contactInformation";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(dto);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processAddContactInformation(_response));
        });
    }

    protected processAddContactInformation(response: Response): Promise<AddContactsToOrganiserResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as AddContactsToOrganiserResponse;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<AddContactsToOrganiserResponse>(null as any);
    }

    getAllOrganisers(): Promise<GetAllOrganisersResponse[]> {
        let url_ = this.baseUrl + "/api/Organiser/all";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetAllOrganisers(_response));
        });
    }

    protected processGetAllOrganisers(response: Response): Promise<GetAllOrganisersResponse[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as GetAllOrganisersResponse[];
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<GetAllOrganisersResponse[]>(null as any);
    }

    getOrganisers(pageNumber: number, pageSize: number): Promise<GetOrganisersWithPaginationResponse> {
        let url_ = this.baseUrl + "/api/Organiser/{pageNumber}/{pageSize}";
        if (pageNumber === undefined || pageNumber === null)
            throw new Error("The parameter 'pageNumber' must be defined.");
        url_ = url_.replace("{pageNumber}", encodeURIComponent("" + pageNumber));
        if (pageSize === undefined || pageSize === null)
            throw new Error("The parameter 'pageSize' must be defined.");
        url_ = url_.replace("{pageSize}", encodeURIComponent("" + pageSize));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetOrganisers(_response));
        });
    }

    protected processGetOrganisers(response: Response): Promise<GetOrganisersWithPaginationResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as GetOrganisersWithPaginationResponse;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<GetOrganisersWithPaginationResponse>(null as any);
    }
}

export interface ITestClient {

    getTest(): Promise<TestCommandResponse>;
}

export class TestClient extends ClientBase implements ITestClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : window as any;
        this.baseUrl = this.getBaseUrl("", baseUrl);
    }

    getTest(): Promise<TestCommandResponse> {
        let url_ = this.baseUrl + "/api/Test";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.transformResult(url_, _response, (_response: Response) => this.processGetTest(_response));
        });
    }

    protected processGetTest(response: Response): Promise<TestCommandResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as TestCommandResponse;
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<TestCommandResponse>(null as any);
    }
}

export interface CreateMarketResponse {
    marketId: number;
}

export interface CreateMarketRequest {
    organiserId: number;
    marketName?: string | null;
    description?: string | null;
    startDate: Date;
    endDate: Date;
}

export interface GetMarketInstanceQueryResponse {
    marketId: number;
    organiserId: number;
    marketName?: string | null;
    description?: string | null;
    startDate: Date;
    endDate: Date;
}

export interface GetAllMarketInstancesQueryResponse {
    marketId: number;
    organiserId: number;
    marketName?: string | null;
    description?: string | null;
    startDate: Date;
    endDate: Date;
}

export interface EditMarketInstanceResponse {
    organiserId: number;
    marketInstanceId: number;
    marketName?: string | null;
    description?: string | null;
    startDate: Date;
    endDate: Date;
}

export interface EditMarketInstanceRequest {
    organiserId: number;
    marketInstanceId: number;
    marketName?: string | null;
    description?: string | null;
    startDate: Date;
    endDate: Date;
}

export interface CreateOrganiserResponse {
    id: number;
    name?: string | null;
    description?: string | null;
    street?: string | null;
    number?: string | null;
    appartment?: string | null;
    postalCode?: string | null;
    city?: string | null;
}

export interface CreateOrganiserRequest {
    name?: string | null;
    description?: string | null;
    street?: string | null;
    number?: string | null;
    appartment?: string | null;
    postalCode?: string | null;
    city?: string | null;
}

export interface AddContactsToOrganiserResponse {
    organiserId: number;
    contactInformation?: { [key: string]: KeyValuePairOfStringAndContactInfoType; } | null;
}

export interface KeyValuePairOfStringAndContactInfoType {
    key: string;
    value: ContactInfoType;
}

export enum ContactInfoType {
    PHONE_NUMER = 0,
    FACEBOOK = 1,
    TWITTER = 2,
    EMAIL = 3,
    INSTAGRAM = 4,
    TIKTOK = 5,
}

export interface AddContactsToOrganiserRequest {
    organiserId: number;
    contactInformation?: { [key: string]: ContactInfoType; } | null;
}

export interface GetAllOrganisersResponse {
    id: number;
    name?: string | null;
}

export interface GetOrganisersWithPaginationResponse {
    organisers?: PaginatedListOfOrganiser | null;
}

export interface PaginatedListOfOrganiser {
    items?: Organiser[] | null;
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}

export interface Organiser {
    id: number;
    name?: string | null;
}

export interface TestCommandResponse {
    testResult?: string | null;
}

export class SwaggerException extends Error {
    override message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new SwaggerException(message, status, response, headers, null);
}