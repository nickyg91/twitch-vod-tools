<style lang="scss" scoped>
@import "~bulma";
.vods-container {
  max-height: 1000px;
  overflow: scroll;
  cursor: pointer;
}

.grow {
  transition: all 0.2s ease-in-out;
}
.grow:hover {
  transform: scale(1.055);
}

.marked-for-delete {
  border-color: $primary;
  border-style: solid;
  border-radius: 0.25rem;
  border-width: 3px;
}
</style>
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
            @click="onReloadClicked"
            class="is-pulled-left button is-large"
          >
            Reload
          </button>
        </div>
      </div>
      <hr />
      <div @scroll="onScroll" class="vods-container columns is-multiline">
        <div
          @click="addForDeletion(vod.id)"
          v-bind:key="vod.id"
          v-for="vod in vods"
          class="column is-3"
        >
          <vod
            class="grow"
            v-bind:class="{
              'marked-for-delete': isVodMarkedForDeletion(vod.id)
            }"
            v-bind:vod="vod"
          ></vod>
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
import { Debounce } from "vue-debounce-decorator";
@Component({
  components: {
    Vod
  }
})
export default class Vods extends Vue {
  vods: TwitchVod[] = [];
  isLoading = false;
  vodCount = 0;
  cursor = "";
  vodsForDeletion: number[] = [];
  mounted() {
    this.$emit("showLoading");
    this.$vodService
      .getVods((this.$store.getters.getTwitchUser as TwitchUser).id)
      .then(
        (result) => {
          this.$emit("hideLoading");
          this.vods = result.data.vods;
          this.vodCount += this.vods.length;
          if (result.data.pagination !== undefined) {
            this.cursor = result.data.pagination.cursor ?? "";
          }
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

  onReloadClicked() {
    this.vodCount = 0;
    this.$vodService
      .getVods((this.$store.getters.getTwitchUser as TwitchUser).id)
      .then(
        (result) => {
          this.$emit("hideLoading");
          this.vods = result.data.vods;
          this.vodCount = this.vods.length;
          if (result.data.pagination !== undefined) {
            this.cursor = result.data.pagination.cursor ?? "";
          }
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

  addForDeletion(id?: number) {
    if (id) {
      const isContained = this.vodsForDeletion.find((x) => x === id);
      if (isContained) {
        const idx = this.vodsForDeletion.indexOf(id);
        this.vodsForDeletion.splice(idx, 1);
      } else {
        this.vodsForDeletion.push(id);
      }
    }
  }

  @Debounce(250)
  onScroll(event: Event) {
    const element = event.target as HTMLElement;
    if (
      element.offsetHeight + element.scrollTop >= element.scrollHeight &&
      this.cursor.length > 0
    ) {
      this.$emit("showLoading");
      this.$vodService
        .getVods(
          (this.$store.getters.getTwitchUser as TwitchUser).id,
          this.cursor
        )
        .then(
          (result) => {
            this.vods.push(
              ...result.data.vods.slice(1, result.data.vods.length)
            );
            this.vodCount += result.data.vods.length - 1;
            this.cursor = result.data.pagination?.cursor ?? "";
            this.$emit("hideLoading");
          },
          (error) => {
            this.$buefy.toast.open({
              message:
                "An error occurred while getting Vods. Please try again.",
              type: "is-danger"
            });
            this.$emit("hideLoading");
          }
        );
    }
  }

  isVodMarkedForDeletion(id?: number): boolean {
    if (id) {
      return this.vodsForDeletion.indexOf(id) > -1;
    }
    return false;
  }
}
</script>
