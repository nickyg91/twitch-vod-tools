import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import axios, { AxiosInstance } from "axios";
import { LoginService } from "./services/login.service";

Vue.config.productionTip = false;

const httpClient = axios.create({
  headers: {
    Authorization: `OAuth ${store.state.accessToken}`
  }
});

Vue.prototype.loginService = new LoginService(httpClient);
new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App)
}).$mount("#app");
