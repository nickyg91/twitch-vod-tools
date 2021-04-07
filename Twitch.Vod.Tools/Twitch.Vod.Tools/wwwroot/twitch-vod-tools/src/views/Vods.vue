<template>
  <v-container fluid>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md4>
        Vods here
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script lang="ts">
import { Component } from "vue-property-decorator";
import Vue from "vue";
@Component
export default class Vods extends Vue {
  mounted() {
    console.log(this.$http);
    this.$loginService.getUser().then(
      (x) => {
        this.$store.dispatch("setTwitchUser", x.data);
      },
      (err) => {
        console.error(err);
      }
    );
  }
}
</script>
