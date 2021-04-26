import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Login from "@/views/Login.vue";
import Vods from "@/views/Vods.vue";
import About from "@/views/About.vue";
import store from "@/store/index";
import { TwitchUser } from "@/models/twitch-user.model";

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
    path: "/about",
    name: "About",
    component: About
  },
  {
    path: "/authenticated",
    name: "Authenticated",
    beforeEnter: (to, from, next) => {
      router.app.$loginService.getUser().then((result) => {
        router.app.$store.dispatch("setTwitchUser", result.data);
        next({ name: "Vods" });
      });
    }
  }
];

routes.forEach((x) => {
  router.addRoute(x);
});

router.beforeEach((to, from, next) => {
  const user = store.getters.getTwitchUser as TwitchUser;
  if (!user.id && to.name !== "Login" && to.name !== "Authenticated") {
    next({ name: "Login" });
  } else {
    next();
  }
});

export default router;
