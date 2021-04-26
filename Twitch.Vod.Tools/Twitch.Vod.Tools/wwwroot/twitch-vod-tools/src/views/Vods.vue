<style lang="scss">
@import "~bulma/sass/utilities/derived-variables.sass";
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
  border-color: $danger;
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
          <div class="buttons">
            <button
              @click="onReloadClicked"
              class="is-pulled-left button is-twitch is-outlined mr-1"
            >
              <span class="icon">
                <i class="fas fa-redo"></i>
              </span>
              <span class="icon-text">Reload</span>
            </button>
            <button
              @click="onGetAllClicked"
              class="is-info button mr-1 is-outlined"
            >
              <span class="icon">
                <i class="fas fa-globe"></i>
              </span>
              <span class="icon-text">Get All Vods</span>
            </button>
            <button
              :disabled="isDeleteDisabled"
              @click="selectAllLoadedForDeletion"
              class="is-danger button mr-1 is-outlined"
            >
              <span class="icon">
                <i class="fas fa-trash"></i>
              </span>
              <span class="icon-text">Delete Selected Vods</span>
            </button>
            <button class="button is-success is-outlined">
              <span class="icon">
                <i class="fas fa-check-double"></i>
              </span>
              <span class="icon-text">Select Current Vods</span>
            </button>
          </div>
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
      <button
        v-if="scrollPosition > 100"
        @click="scrollToTop"
        class="is-floating button is-medium is-twitch"
      >
        <span class="icon">
          <i class="fas fa-arrow-up"></i>
        </span>
      </button>
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
  scrollPosition = 0;

  get isDeleteDisabled(): boolean {
    return this.vodsForDeletion.length < 1;
  }

  private getVods() {
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
          console.error(error);
        }
      );
  }

  mounted() {
    this.$emit("showLoading");
    this.getVods();
  }

  deleteVods() {
    this.$buefy.dialog.confirm({
      message:
        "Are you sure you want to delete these vods? This action is permanent.",
      title: "Delete?",
      onConfirm: () => {
        this.$emit("showLoading");
        this.$vodService
          .deleteVods(this.vodsForDeletion)
          .then(
            (result) => {
              return result.data;
            },
            (error) => {
              this.$buefy.toast.open({
                message:
                  "An error occurred while deleting Vods. Please try again.",
                type: "is-danger"
              });
              this.$emit("hideLoading");
              console.error(error);
            }
          )
          .then((result) => this.getVods());
      }
    });
  }

  selectAllLoadedForDeletion() {
    this.vodsForDeletion = this.vods.map((x) => x.id);
  }

  scrollToTop() {
    document
      .getElementsByClassName("vods-container")[0]
      .scrollTo({ top: 0, behavior: "smooth" });
  }

  onGetAllClicked() {
    this.$buefy.dialog.confirm({
      message:
        "Are you sure you want to get all your vods? This make take a hot minute if you have a lot.",
      title: "Are you sure you want to get 'em all?",
      onConfirm: () => {
        this.$emit("showLoading");
        this.vodCount = 0;
        this.$vodService
          .getVods(
            (this.$store.getters.getTwitchUser as TwitchUser).id,
            undefined,
            true
          )
          .then(
            (result) => {
              this.$emit("hideLoading");
              this.cursor = "";
              this.vods = result.data.vods;
              this.vodCount += this.vods.length;
            },
            (error) => {
              this.$buefy.toast.open({
                message:
                  "An error occurred while getting Vods. Please try again.",
                type: "is-danger"
              });
              this.$emit("hideLoading");
              console.error(error);
            }
          );
      }
    });
  }

  onReloadClicked() {
    this.vodCount = 0;
    document
      .getElementsByClassName("vods-container")[0]
      .scrollTo({ top: -100, behavior: "smooth" });
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
          console.error(error);
        }
      );
  }

  addForDeletion(id?: number) {
    if (id) {
      const isContained = this.vodsForDeletion?.find((x) => x === id);
      if (isContained) {
        const idx = this.vodsForDeletion?.indexOf(id);
        this.vodsForDeletion?.splice(idx, 1);
      } else {
        this.vodsForDeletion?.push(id);
      }
    }
  }

  @Debounce(250)
  onScroll(event: Event) {
    const element = event.target as HTMLElement;
    this.scrollPosition = element.scrollTop;
    if (this.cursor === "") {
      return;
    }
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
            this.vods.push(...result.data.vods);
            this.vodCount += result.data.vods.length;
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
            console.error(error);
          }
        );
    }
  }

  isVodMarkedForDeletion(id?: number): boolean {
    if (id) {
      return this.vodsForDeletion?.indexOf(id) > -1;
    }
    return false;
  }
}
</script>
