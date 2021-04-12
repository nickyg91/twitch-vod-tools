import { TwitchVod } from "../models/twitch-vod.model";
import { Pagination } from "./twitch-pagination.model";

export class TwitchVodContainer {
  vods: TwitchVod[] = [];
  pagination?: Pagination = undefined;
}
