<template>
  <div v-if="twitchConfig" class="section">
    <div class="container">
      <div class="columns is-centered">
        <div class="column is-half">
          <div class="box">
            <p class="title is-3 has-text-centered">
              In order to access this application you must allow it to access
              twitch!
            </p>
            <a :href="this.url" class="is-fullwidth button is-primary">
              Allow
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { mapState } from "vuex";
import Vue from "vue";
import Component from "vue-class-component";
import { TwitchConfig } from "../models/twitch-config.model";
import { Watch } from "vue-property-decorator";
import store from "@/store";
@Component({
  store: store,
  computed: {
    twitchConfig: mapState({
      get: "twitchConfig",
      set: "twitchConfig"
    })
  }
})
export default class Login extends Vue {
  url = "";
  twitchConfig!: TwitchConfig;
  @Watch("twitchConfig")
  onTwitchConfigChanged(newValue: TwitchConfig) {
    this.twitchConfig = newValue;
    this.url = `https://id.twitch.tv/oauth2/authorize?&response_type=code&client_id=${this.twitchConfig.clientId}&redirect_uri=${this.twitchConfig.redirectUrl}&scope=user_read clips:edit channel:manage:videos`;
  }
}
</script>
