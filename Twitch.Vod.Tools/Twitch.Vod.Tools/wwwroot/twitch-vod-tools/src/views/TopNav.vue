<template>
  <b-navbar>
    <template #brand></template>
    <template #end>
      <b-navbar-item tag="div">
        <span class="mr-3">
          <figure v-if="twitchUser != null" class="image">
            <img :src="twitchUser.profileImageUrl" class="is-rounded" />
          </figure>
        </span>
        <span class="is-5">{{ twitchUser.displayName }}</span>
      </b-navbar-item>
    </template>
  </b-navbar>
</template>
<script lang="ts">
import { TwitchUser } from "@/models/twitch-user.model";
import store from "@/store";
import { Component, Vue, Watch } from "vue-property-decorator";
//import Vue from "vue";
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
// export default Vue.extend({
//   store: store,
//   computed: {
//     twitchUser: mapState({
//       get: "twitchUser",
//       set: "twitchUser"
//     })
//   },
//   watch: {
//     twitchUser(newValue: TwitchUser) {
//       this.twitchUser = newValue;
//     }
//   }
// });
</script>
