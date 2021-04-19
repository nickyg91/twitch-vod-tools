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
