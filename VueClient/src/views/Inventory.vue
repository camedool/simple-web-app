<template>
  <div>
    <div class="container mt-5">
      <div class="row">
        <div class="col-md-12">
          <v-card :loading="isLoading" min-height="500px">
            <v-card-title class="secondary px-0 pb-0 pt-2 mb-5" flat>
              <v-tabs
                v-model="tab"
                centered
                icons-and-text
                grow
                dark
                background-color="secondary"
              >
                <v-tab key="inventoriesGrid">
                  <v-layout class="text-centered column" justify-end>
                    <v-icon x-large color="primary">mdi-stack-exchange</v-icon>
                    <span>Inventories</span>
                  </v-layout>
                </v-tab>
                <v-tab key="itemsGrid">
                  <v-layout class="text-centered column" justify-end>
                    <v-icon x-large color="primary">mdi-palette-swatch</v-icon>
                    <span>Items</span>
                  </v-layout>
                </v-tab>
              </v-tabs>
            </v-card-title>

            <v-card-text>
              <v-tabs-items v-model="tab" class="pa-5">
                <v-tab-item>
                  <inventory-grid />
                </v-tab-item>

                <v-tab-item>
                  <item-grid />
                </v-tab-item>
              </v-tabs-items>
            </v-card-text>
            <v-overlay
              v-if="isLoading"
              absolute
              color="white"
              opacity="0.5"
            ></v-overlay>
          </v-card>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import InventoryGrid from "@/components/InventoryGrid.vue";
import ItemGrid from "@/components/ItemGrid.vue";

export default Vue.extend({
  data() {
    return {
      tab: 0,
      isLoading: false,
    };
  },
  components: { InventoryGrid, ItemGrid },
});
</script>
