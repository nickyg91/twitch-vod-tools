import { RootState } from "@/interfaces";
import { TwitchConfig } from "@/models/twitch-config.model";
import { TwitchUser } from "@/models/twitch-user.model";
import Vue from "vue";
import Vuex, {
  ActionTree,
  GetterTree,
  MutationTree,
  Store,
  StoreOptions
} from "vuex";

// import { Action, Module, Mutation, VuexModule } from "vuex-module-decorators";
// @Module({ namespaced: false })
// class TwitchStore extends VuexModule {
//   accessToken = "";
//   twitchConfig: TwitchConfig | null = null;
//   twitchUser: TwitchUser | null = null;
//   twitchOAuthToken: TwitchOAuthToken | null = null;

//   @Mutation
//   setAccessToken(payload: string) {
//     this.accessToken = payload;
//   }

//   @Mutation
//   setTwitchSettings(payload: TwitchConfig) {
//     this.twitchConfig = payload;
//   }

//   @Mutation
//   setTwitchUser(payload: TwitchUser) {
//     this.twitchUser = payload;
//   }

//   @Mutation
//   setOauthToken(payload: TwitchOAuthToken) {
//     this.twitchOAuthToken = payload;
//   }

//   @Action
//   updateAccessToken(payload: string) {
//     this.context.commit("setAccessToken", payload);
//   }

//   @Action
//   updateTwitchSettings(payload: TwitchConfig) {
//     this.context.commit("setTwitchSettings", payload);
//   }

//   @Action
//   uptdateTwitchUser(payload: TwitchUser) {
//     this.context.commit("setTwitchUser", payload);
//   }

//   @Action
//   updateOauthToken(payload: TwitchOAuthToken) {
//     this.context.commit("setOauthToken", payload);
//   }
// }

// const store = new Vuex.Store({
//   state: TwitchStore
// });
// export default store;
Vue.use(Vuex);

const getters = {
  getAccessToken: (state) => {
    return state.accessToken;
  },
  getTwitchSettings: (state): TwitchConfig | null => {
    return state.twitchConfig;
  },
  getTwitchUser: (state): TwitchUser | null => {
    return state.twitchUser;
  }
} as GetterTree<RootState, any>;

const actions = {
  setAccessToken: (context, payload) => {
    return context.commit("setAccessToken", payload);
  },
  setTwitchSettings: (context, payload) => {
    return context.commit("setTwitchSettings", payload);
  },
  setTwitchUser: (context, payload) => {
    return context.commit("setTwitchUser", payload);
  }
} as ActionTree<RootState, any>;

const mutations = {
  setAccessToken: (state, token: string) => {
    state.accessToken = token;
  },
  setTwitchSettings: (state, config: TwitchConfig) => {
    state.twitchConfig = config;
  },
  setTwitchUser: (state, user: TwitchUser) => {
    state.twitchUser = user;
  }
} as MutationTree<RootState>;

const store: StoreOptions<RootState> = {
  state: {
    accessToken: "",
    twitchConfig: new TwitchConfig(),
    twitchUser: new TwitchUser()
  },
  mutations: mutations,
  getters: getters,
  actions: actions,
  modules: {}
};
export default new Vuex.Store<RootState>(store) as Store<RootState>;
