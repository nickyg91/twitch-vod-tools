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
    <div v-if="twitchUser.id != ''" class="section">
      <div class="container">
        <div class="tabs is-centered is-large mt-5">
          <ul>
            <router-link
              :class="{ 'is-active': getCurrentRoute === 'Vods' }"
              custom
              :to="{ name: 'Vods' }"
              exact
              v-slot="{ navigate }"
            >
              <li>
                <a @click="navigate">Vods</a>
              </li>
            </router-link>
            <router-link
              :class="{ 'is-active': getCurrentRoute === 'About' }"
              v-slot="{ navigate }"
              custom
              :to="{ name: 'About' }"
              exact
            >
              <li @click="navigate"><a>About</a></li>
            </router-link>
          </ul>
        </div>
      </div>
    </div>
    <transition
      enter-active-class="animate__animated animate__bounceInLeft"
      leave-active-class="animate__animated animate__bounceOutRight"
      mode="out-in"
    >
      <router-view
        @showLoading="showLoading"
        @hideLoading="hideLoading"
      ></router-view>
    </transition>
    <b-loading v-model="isLoading" :can-cancel="false"></b-loading>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import TopNav from "@/views/TopNav.vue";
import store from "./store";
import { mapState } from "vuex";
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
  computed: {
    getCurrentRoute() {
      return this.$route.name;
    },
    ...mapState(["twitchUser"])
  },
  methods: {
    showLoading() {
      this.$data.isLoading = true;
    },
    hideLoading() {
      this.$data.isLoading = false;
    }
  },
  store: store
});
</script>
