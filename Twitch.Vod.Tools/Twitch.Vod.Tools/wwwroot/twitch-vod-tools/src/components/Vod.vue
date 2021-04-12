<template>
  <div class="card">
    <div class="card-image">
      <figure class="is-4by3 image">
        <img :src="vod.thumbnailUrl" />
      </figure>
    </div>
    <div class="card-content">
      <div class="media">
        <div class="media-content">
          <div>
            <p>Title: {{ vod.title }}</p>
          </div>
          <div>
            <p>Duration: {{ vod.duration }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { TwitchVod } from "@/models/twitch-vod.model";
import { Component, Prop, Vue } from "vue-property-decorator";
@Component
export default class Vod extends Vue {
  @Prop({
    default: () => new TwitchVod()
  })
  vod!: TwitchVod;
}
</script>
