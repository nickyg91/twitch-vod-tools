<template>
  <b-navbar v-if="twitchUser.id != ''" class="has-background-twitch">
    <template #brand> </template>
    <template #start> </template>
    <template #end>
      <b-navbar-item tag="div">
        <span class="mr-3">
          <figure class="image is-24x24">
            <img :src="twitchUser.profileImageUrl" class="is-rounded" />
          </figure>
        </span>
        <span>
          <a
            class="button is-inverted is-twitch"
            target="_blank"
            :href="'https://twitch.tv/' + twitchUser.displayName"
          >
            <span class="icon">
              <i class="fas fa-external-link-alt"></i>
            </span>
            <span class="icon-text">
              {{ twitchUser.displayName }}
            </span>
          </a>
        </span>
      </b-navbar-item>
    </template>
  </b-navbar>
</template>
<script lang="ts">
import { TwitchUser } from "@/models/twitch-user.model";
import { Component, Vue, Watch } from "vue-property-decorator";
import { mapState } from "vuex";
@Component({
  computed: {
    twitchUser: mapState({
      get: "twitchUser",
      set: "twitchUser"
    })
  }
})
export default class TopNav extends Vue {
  twitchUser!: TwitchUser;
  @Watch("twitchUser")
  onTwitchUserChanged(newValue: TwitchUser) {
    this.twitchUser = newValue;
  }
}
</script>
