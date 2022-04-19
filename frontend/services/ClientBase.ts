export class ClientBase {
    baseApiUrl : string = process.env.NEXT_PUBLIC_BACKEND_URL;

    protected async transformOptions(options: RequestInit): Promise<RequestInit>{
        const token = localStorage.getItem("loppeportalen_jwt");

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