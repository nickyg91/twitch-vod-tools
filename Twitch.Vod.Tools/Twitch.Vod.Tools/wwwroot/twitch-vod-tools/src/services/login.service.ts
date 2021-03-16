import { AxiosInstance } from "axios";

export class LoginService {
  private readonly httpClient: AxiosInstance;
  constructor(httpInstance: AxiosInstance) {
    this.httpClient = httpInstance;
  }

  login() {
    return this.httpClient.get("/api/login");
  }
}
