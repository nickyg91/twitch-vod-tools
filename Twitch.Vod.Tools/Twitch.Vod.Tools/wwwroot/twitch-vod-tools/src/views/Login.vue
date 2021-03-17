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
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
@Component
export default class Login extends Vue {
  onAuthorizeClick = () => {
    const twitchConfig = this.$store.getters.getTwitchSettings;
    console.log(this.$store.state.getTwitchSettings);
    window.location.href = `https://id.twitch.tv/oauth2/authorize?client_id=${twitchConfig.clientId}&redirect_uri=${twitchConfig.redirectUrl}&response_type=token&scope=clips:edit channel:manage:videos`;
  };
}
</script>
