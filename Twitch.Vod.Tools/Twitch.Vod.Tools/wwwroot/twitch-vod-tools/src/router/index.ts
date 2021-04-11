import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Login from "@/views/Login.vue";
import Vods from "@/views/Vods.vue";
import store from "@/store/index";

Vue.use(VueRouter);

const router = new VueRouter({});
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
  },
  {
    path: "/authenticated",
    name: "Authenticated",
    beforeEnter: (to, from, next) => {
      console.log("in before enter");
      const code = to.params["code"];
      store.dispatch("setAccessToken", code);
      router.app.$http.defaults.headers = {
        Authorization: `Bearer ${code}`
      };
      next({ name: "Vods" });
    }
  }
];

router.addRoutes(routes);

router.beforeEach((to, from, next) => {
  console.log(from);
  if (to.name === "Authenticated" && to.fullPath.indexOf("code") > -1) {
    console.log("have the code");
    next();
  }
  if (to.name !== "Login" && !store.state.accessToken) {
    next({ name: "Login" });
  } else {
    next();
  }
});

export default router;
