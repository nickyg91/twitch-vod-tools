import { AxiosInstance, AxiosResponse } from "axios";
import { TwitchUser } from "./twitch-user.model";

export class LoginService {
  private readonly httpClient: AxiosInstance;
  constructor(httpInstance: AxiosInstance) {
    this.httpClient = httpInstance;
  }

  login() {
    return this.httpClient.get("/api/authentication");
  }

  getUser(): Promise<AxiosResponse<TwitchUser>> {
    return this.httpClient.get("/api/authentication/user");
  }
}
