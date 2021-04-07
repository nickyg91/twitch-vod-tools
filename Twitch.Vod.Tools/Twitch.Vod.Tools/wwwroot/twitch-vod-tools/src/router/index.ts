import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Login from "@/views/Login.vue";
import Vods from "@/views/Vods.vue";
import store from "@/store/index";

Vue.use(VueRouter);

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
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue")
  }
];

const router = new VueRouter({
  routes
});

router.beforeEach((to, from, next) => {
  if (to.fullPath.indexOf("access_token") > -1) {
    const splitHash = to.fullPath.split("&");
    splitHash.map(part => part.replace(/#\//, ""));
    splitHash.forEach(result => {
      const parts = result.split("=");
      console.log(parts);
      if (parts[0].indexOf("access_token") > -1) {
        store.dispatch("setAccessToken", parts[1]);
        router.app.$http.defaults.headers = {
          Authorization: `Bearer ${parts[1]}`
        };
        next({ name: "Vods" });
      }
    });
  }
  if (to.name !== "Login" && !store.state.accessToken) {
    next({ name: "Login" });
  } else if (to.name === "Login" && store.state.accessToken.length > 0) {
    next({ name: "Vods" });
  } else {
    next();
  }
});

export default router;
