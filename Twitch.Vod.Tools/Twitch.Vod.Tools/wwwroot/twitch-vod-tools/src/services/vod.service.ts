import { AxiosInstance, AxiosResponse } from "axios";
import { TwitchVodContainer } from "./twitch-vod-container.model";

export class VodService {
  private readonly httpClient: AxiosInstance;
  constructor(httpInstance: AxiosInstance) {
    this.httpClient = httpInstance;
  }

  getVods(userId: string): Promise<AxiosResponse<TwitchVodContainer>> {
    return this.httpClient.get(`/api/vod/${userId}`);
  }
}
