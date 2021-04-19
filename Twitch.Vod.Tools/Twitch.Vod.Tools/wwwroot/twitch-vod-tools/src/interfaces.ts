import { TwitchConfig } from "./models/twitch-config.model";
import { TwitchOAuthToken } from "./models/twitch-oauth-token.model";
import { TwitchUser } from "./models/twitch-user.model";

export interface RootState {
  accessToken: string | null;
  twitchConfig: TwitchConfig | null;
  twitchUser: TwitchUser | null;
}
