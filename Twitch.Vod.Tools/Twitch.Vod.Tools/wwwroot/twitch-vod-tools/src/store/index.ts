import { TwitchConfig } from "@/models/twitch-config.model";
import Vue from "vue";
import Vuex from "vuex";
import { TwitchUser } from "../services/twitch-user.model";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    accessToken: "",
    twitchConfig: new TwitchConfig(),
    twitchUser: new TwitchUser()
  },
  mutations: {
    setAccessToken: (state, token: string) => {
      state.accessToken = token;
    },
    setTwitchSettings: (state, config: TwitchConfig) => {
      state.twitchConfig = config;
    },
    setTwitchUser: (state, user: TwitchUser) => {
      state.twitchUser = user;
    }
  },
  getters: {
    getAccessToken: state => {
      return state.accessToken;
    },
    getTwitchSettings: (state): TwitchConfig => {
      return state.twitchConfig;
    },
    getTwitchUser: (state): TwitchUser => {
      return state.twitchUser;
    }
  },
  actions: {
    setAccessToken: (context, payload) => {
      return context.commit("setAccessToken", payload);
    },
    setTwitchSettings: (context, payload) => {
      return context.commit("setTwitchSettings", payload);
    },
    setTwitchUser: (context, payload) => {
      return context.commit("setTwitchUser", payload);
    }
  },
  modules: {}
});
