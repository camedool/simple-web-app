<template>
  <v-app>
    <v-app-bar app color="secondary" dark>
      <div>
        <img src="./assets/logo-main_ententee.svg" alt="" />
      </div>
      <v-spacer></v-spacer>
      <div>
        <span class="text-h4 ml-3">INVENTORIES</span>
      </div>
      <v-spacer></v-spacer>
    </v-app-bar>

    <v-main>
      <v-fade-transition mode="out-in">
        <Inventory></Inventory>
      </v-fade-transition>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import Inventory from "./views/Inventory.vue";

export default Vue.extend({
  name: "App",

  components: {
    Inventory,
  },
});
</script>
<style>
.v-application,
.v-application .text-h5 {
  font-family: "Maven Pro", sans-serif !important;
}
</style>
