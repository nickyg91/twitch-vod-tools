import { AxiosInstance } from "axios";
import { LoginService } from "./services/login.service";
import Vue from "vue";

declare module "vue/types/vue" {
  interface Vue {
    $http: AxiosInstance;
    $loginService: LoginService;
  }
}
// declare module "vue/types/options" {
//   interface ComponentOptions<V extends Vue> {
//     $http: AxiosInstance;
//     $loginService: LoginService;
//   }
// }
