export class ClientBase {
    baseApiUrl : string = "https://localhost:5001";

    protected async transformOptions(options: RequestInit): Promise<RequestInit>{
        const token = JSON.parse(localStorage.getItem("user"));
        console.log(`Client Base Token ${token}`);
        
        options.mode = 'cors';
        options.headers = {
            ...options.headers, 
            authorization: `Bearer ${token}`};
        return Promise.resolve(options);
    }

    protected transformResult(url: string, response: Response, processor: (response: Response) => Promise<any>){
        return processor(response);
    }

    protected getBaseUrl(defaultUrl: string, baseUrl?: string){
        return this.baseApiUrl;
    }
}