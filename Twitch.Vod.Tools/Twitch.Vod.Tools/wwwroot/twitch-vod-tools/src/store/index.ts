import { TwitchConfig } from "@/models/twitch-config.model";
import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    accessToken: "",
    twitchConfig: new TwitchConfig()
  },
  mutations: {
    setAccessToken: (state, token: string) => {
      state.accessToken = token;
    },
    setTwitchSettings: (state, config: TwitchConfig) => {
      state.twitchConfig = config;
    }
  },
  getters: {
    getAccessToken: (state) => {
      return state.accessToken;
    },
    getTwitchSettings: (state): TwitchConfig => {
      return state.twitchConfig;
    }
  },
  actions: {
    setAccessToken: (context, payload) => {
      return context.commit("setAccessToken", payload);
    },
    setTwitchSettings: (context, payload) => {
      return context.commit("setTwitchSettings", payload);
    }
  },
  modules: {}
});
