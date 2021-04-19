import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import axios from "axios";
import { LoginService } from "./services/login.service";
import Buefy from "buefy";
import "buefy/dist/buefy.css";
import "bulma/css/bulma.css";
import { VodService } from "./services/vod.service";

Vue.config.productionTip = false;
const httpClient = axios.create({});
httpClient.defaults.headers.common["Access-Control-Allow-Origin"] = "*";
Vue.prototype.$http = httpClient;
Vue.prototype.$loginService = new LoginService(Vue.prototype.$http);
Vue.prototype.$vodService = new VodService(Vue.prototype.$http);
Vue.use(Buefy);

new Vue({
  router,
  store: store,
  render: (h) => h(App)
}).$mount("#app");
