import { AxiosInstance } from "axios";

export class VodService {
    private readonly httpClient: AxiosInstance;
    constructor(httpInstance: AxiosInstance) {
        this.httpClient = httpInstance;
    }
}
