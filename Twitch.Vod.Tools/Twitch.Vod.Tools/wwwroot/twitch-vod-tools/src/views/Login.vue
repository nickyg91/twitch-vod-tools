<template>
  <v-container fluid>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md4>
        <v-card class="elevation-12">
          <v-card-text centered>
            <div class="centered text-h5">
              In order to use this app please allow it to access Twitch!
            </div>
          </v-card-text>
          <v-card-actions class="justify-center">
            <v-btn
              @click="onAuthorizeClick"
              x-large
              large
              color="purple darken-4"
              elevation="2"
              class="white--text"
            >
              <v-icon left color="white">mdi-account</v-icon>
              Authorize
            </v-btn>
            {{ url }}
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import axios from "axios";
import { TwitchConfig } from "../models/twitch-config.model";
@Component
export default class Login extends Vue {
  url = "";
  mounted() {
    axios.get<TwitchConfig>("/api/authentication/config").then((result) => {
      this.$store.dispatch("setTwitchSettings", result.data);
      this.url = `https://id.twitch.tv/oauth2/authorize?&response_type=code&client_id=${result.data.clientId}&redirect_uri=${result.data.redirectUrl}&scope=user_read clips:edit channel:manage:videos`;
    });
  }
  onAuthorizeClick = () => {
    window.location.href = this.url;
  };
}
</script>
