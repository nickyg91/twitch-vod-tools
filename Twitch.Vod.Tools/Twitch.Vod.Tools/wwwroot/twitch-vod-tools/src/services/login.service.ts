import { TwitchOAuthToken } from "@/models/twitch-oauth-token.model";
import { TwitchUser } from "@/models/twitch-user.model";
import { AxiosInstance, AxiosResponse } from "axios";

export class LoginService {
  private readonly httpClient: AxiosInstance;
  constructor(httpInstance: AxiosInstance) {
    this.httpClient = httpInstance;
  }

  login(): Promise<AxiosResponse<any>> {
    return this.httpClient.get("/api/authentication/token");
  }

  refresh(refreshToken: string): Promise<AxiosResponse<TwitchOAuthToken>> {
    return this.httpClient.get(`/api/authentication/token/${refreshToken}`);
  }

  getUser(): Promise<AxiosResponse<TwitchUser>> {
    return this.httpClient.get("/api/authentication/user");
  }
}
