<template>
  <div class="section">
    <div class="container">
      <div class="columns">
        <div class="column is-half">
          <p class="mt-3 is-pulled-right subtitle is-4">
            Total Vods Loaded: {{ this.vodCount }}
          </p>
        </div>
        <div class="column is-half">
          <button
            @click="onrReloadClicked"
            class="is-pulled-left button is-large"
          >
            Reload
          </button>
        </div>
      </div>
      <hr />
      <div class="columns is-multiline">
        <div v-bind:key="vod.id" v-for="vod in vods" class="column is-3">
          <vod v-bind:vod="vod"></vod>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component } from "vue-property-decorator";
import Vue from "vue";
import { TwitchVod } from "@/models/twitch-vod.model";
import { TwitchUser } from "@/models/twitch-user.model";
import Vod from "@/components/Vod.vue";
@Component({
  components: {
    Vod
  }
})
export default class Vods extends Vue {
  vods: TwitchVod[] = [];
  isLoading = false;
  vodCount = 0;
  mounted() {
    this.$loginService
      .getUser()
      .then((x) => {
        this.$store.dispatch("setTwitchUser", x.data);
        this.$emit("showLoading");
        return this.$vodService.getVods(x.data.id);
      })
      .then(
        (result) => {
          this.$emit("hideLoading");
          this.vods = result.data.vods;
          this.vodCount += this.vods.length;
        },
        (error) => {
          this.$buefy.toast.open({
            message: "An error occurred while getting Vods. Please try again.",
            type: "is-danger"
          });
          this.$emit("hideLoading");
        }
      );
  }
  onrReloadClicked() {
    this.vodCount = 0;
    this.$vodService
      .getVods((this.$store.getters.getTwitchUser as TwitchUser).id)
      .then(
        (result) => {
          this.$emit("hideLoading");
          this.vods = result.data.vods;
          this.vodCount += this.vods.length;
        },
        (error) => {
          this.$buefy.toast.open({
            message: "An error occurred while getting Vods. Please try again.",
            type: "is-danger"
          });
          this.$emit("hideLoading");
        }
      );
  }
}
</script>
