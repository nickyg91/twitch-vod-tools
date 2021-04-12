<template>
  <div>
    <top-nav></top-nav>
    <router-view
      @showLoading="showLoading"
      @hideLoading="hideLoading"
    ></router-view>
    <b-loading v-model="isLoading" :can-cancel="false"></b-loading>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import TopNav from "@/views/TopNav.vue";
import axios from "axios";
import { TwitchConfig } from "./models/twitch-config.model";
import store from "./store";
export default Vue.extend({
  name: "App",
  components: {
    TopNav
  },
  data() {
    return {
      isLoading: false
    };
  },
  methods: {
    showLoading() {
      this.isLoading = true;
    },
    hideLoading() {
      this.isLoading = false;
    }
  },
  store: store,
  beforeCreate: () => {
    axios.get<TwitchConfig>("/api/authentication/config").then((result) => {
      store.dispatch("setTwitchSettings", result.data);
    });
  }
});
</script>
