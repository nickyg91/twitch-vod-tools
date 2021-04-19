import { TwitchOAuthToken } from "./twitch-oauth-token.model";

export class TwitchUser {
  displayName = "";
  id = "";
  profileImageUrl = "";
  viewCount = 0;
  oauthToken?: TwitchOAuthToken = undefined;
}
