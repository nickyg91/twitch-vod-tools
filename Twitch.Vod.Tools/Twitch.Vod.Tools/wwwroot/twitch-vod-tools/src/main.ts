import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import axios from "axios";
import { LoginService } from "./services/login.service";
import { TwitchConfig } from "./models/twitch-config.model";

Vue.config.productionTip = false;
const httpClient = axios.create({});

Vue.prototype.$http = httpClient;
Vue.prototype.loginService = new LoginService(Vue.prototype.$http);
new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App),
  beforeCreate: () => {
    axios.get<TwitchConfig>("/api/authentication/config").then((result) => {
      store.dispatch("setTwitchSettings", result.data);
    });
  }
}).$mount("#app");
