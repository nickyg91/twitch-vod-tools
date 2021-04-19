<template>
  <div class="section">
    <div class="container">
      <div class="columns is-centered">
        <div class="column is-half">
          <div class="box">
            <p class="title is-3 has-text-centered">
              In order to access this application you must allow it to access
              twitch!
            </p>
            <a
              href="/api/authentication/login"
              class="is-fullwidth button is-primary"
            >
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
  login() {
    this.$loginService.login();
  }
}
</script>
