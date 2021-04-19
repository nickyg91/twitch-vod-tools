import { AxiosInstance, AxiosResponse } from "axios";
import { TwitchVodContainer } from "./twitch-vod-container.model";

export class VodService {
  private readonly httpClient: AxiosInstance;
  constructor(httpInstance: AxiosInstance) {
    this.httpClient = httpInstance;
  }

  getVods(
    userId: string,
    cursor?: string
  ): Promise<AxiosResponse<TwitchVodContainer>> {
    if (cursor !== undefined) {
      return this.httpClient.get(`/api/vod/${userId}?pagination=${cursor}`);
    }
    return this.httpClient.get(`/api/vod/${userId}`);
  }
}
