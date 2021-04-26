<style lang="scss">
$scheme-invert: hsl(0, 0%, 4%);
.button.is-floating {
  position: fixed !important;
  width: 60px;
  height: 60px !important;
  bottom: 40px;
  right: 40px;
  border-radius: 100px !important;
  text-align: center !important;
  font-size: 1.6rem !important;
  box-shadow: 0 0.0625em 0.125em rgba(10, 10, 10, 0.05) !important;
  z-index: 3;
  .is-large {
    width: 90px;
    height: 90px;
    font-size: 2.6rem;
  }

  .is-medium {
    width: 75px;
    height: 75px;
    font-size: 2.2rem;
  }

  .is-small {
    width: 45px;
    height: 45px;
    font-size: 1.2rem;
    border-radius: 50px;
  }
}
</style>
<template>
  <div>
    <top-nav></top-nav>
    <router-view
      @showLoading="showLoading"
      @hideLoading="hideLoading"
    ></router-view>
    <b-loading v-model="isLoading" :can-cancel="false"></b-loading>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import TopNav from "@/views/TopNav.vue";
import store from "./store";
export default Vue.extend({
  name: "App",
  components: {
    TopNav
  },
  data() {
    return {
      isLoading: false
    };
  },
  methods: {
    showLoading() {
      this.isLoading = true;
    },
    hideLoading() {
      this.isLoading = false;
    }
  },
  store: store
});
</script>
