import { AxiosInstance } from "axios";
import { LoginService } from "./services/login.service";
import Vue from "vue";
import { VodService } from "./services/vod.service";

declare module "vue/types/vue" {
  interface Vue {
    $http: AxiosInstance;
    $loginService: LoginService;
    $vodService: VodService;
  }
}
