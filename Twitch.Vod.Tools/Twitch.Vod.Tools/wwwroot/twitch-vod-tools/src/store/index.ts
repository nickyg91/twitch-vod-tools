import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    accessToken: ""
  },
  mutations: {
    setAccessToken: (state, token: string) => {
      state.accessToken = token;
    }
  },
  getters: {
    getAccessToken: (state) => {
      return state.accessToken;
    }
  },
  actions: {
    getAccessToken: (context) => {
      return context.dispatch("getAccessToken");
    }
  },
  modules: {}
});
