import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Login from "@/views/Login.vue";
import Vods from "@/views/Vods.vue";
import store from "@/store/index";

Vue.use(VueRouter);

const router = new VueRouter({
  mode: "history"
});
const routes: Array<RouteConfig> = [
  {
    path: "/login",
    name: "Login",
    component: Login
  },
  {
    path: "/vods",
    name: "Vods",
    component: Vods
  },
  {
    path: "/authenticated",
    name: "Authenticated",
    beforeEnter: (to, from, next) => {
      const existingToken = store.getters.getAccessToken;
      if (!existingToken) {
        const parsedAccessToken = to.query.code;
        router.app.$store.dispatch("setAccessToken", parsedAccessToken);
        router.app.$http.defaults.headers.common[
          "x-user-access-token"
        ] = parsedAccessToken;
        next({ name: "Vods" });
      } else {
        next({ name: "Vods" });
      }
    }
  }
];

routes.forEach((x) => {
  router.addRoute(x);
});

router.beforeEach((to, from, next) => {
  const existingToken = store.getters.getAccessToken;
  if (!existingToken && to.name !== "Login" && to.name !== "Authenticated") {
    next({ name: "Login" });
  } else {
    next();
  }
});

export default router;
