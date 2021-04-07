import { TwitchVod } from "../models/twitch-vod.model";
import { Pagination } from "./twitch-pagination.model";

export class TwitchVodContainer {
  data: TwitchVod[] = [];
  pagination?: Pagination = undefined;
}
