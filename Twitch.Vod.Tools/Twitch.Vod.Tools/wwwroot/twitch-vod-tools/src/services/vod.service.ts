import { AxiosInstance, AxiosResponse } from "axios";
import { TwitchVodContainer } from "./twitch-vod-container.model";

export class VodService {
  private readonly httpClient: AxiosInstance;
  constructor(httpInstance: AxiosInstance) {
    this.httpClient = httpInstance;
  }

  getVods(
    userId: string,
    cursor?: string,
    getAll = false
  ): Promise<AxiosResponse<TwitchVodContainer>> {
    if (getAll) {
      return this.httpClient.get<TwitchVodContainer>(
        `/api/vod/${userId}?getAll=true`
      );
    }
    if (cursor !== undefined) {
      return this.httpClient.get<TwitchVodContainer>(
        `/api/vod/${userId}?pagination=${cursor}`
      );
    }
    return this.httpClient.get<TwitchVodContainer>(`/api/vod/${userId}`);
  }

  deleteVods(ids: number[]) {
    return this.httpClient.post<number[]>("/api/vod/delete", ids);
  }
}
