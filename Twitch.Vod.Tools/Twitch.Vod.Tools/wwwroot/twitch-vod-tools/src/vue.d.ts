import { AxiosInstance } from "axios";
import { LoginService } from "./services/login.service";
import Vue from "vue";
import { VodService } from "./services/vod.service";
import { Store } from "vuex";
import { RootState } from "./interfaces";

declare module "vue/types/vue" {
  interface Vue {
    $http: AxiosInstance;
    $loginService: LoginService;
    $vodService: VodService;
    $store: Store<RootState | any>;
  }
}
