<template>
  <v-data-table
    :headers="headers"
    :items="inventories"
    class="elevation-1"
    :loading="isLoading"
    loading-text="Loading... Please wait"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>Current inventories in all warehouses</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" persistent max-width="700px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              color="primary"
              dark
              class="mb-2"
              v-bind="attrs"
              v-on="on"
              @click="onNew"
            >
              <v-icon right dark class="mr-2"> mdi-cart-plus </v-icon>
              New
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ formTitle }}</span>
            </v-card-title>

            <v-form ref="editedItem">
              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col cols="12" sm="8" md="5">
                      <v-select
                        :items="items"
                        v-model="editedItem.itemId"
                        item-value="id"
                        item-text="name"
                        label="Item"
                        :rules="[requiredSelect]"
                      ></v-select>
                    </v-col>
                    <v-col cols="8" sm="7" md="5">
                      <v-select
                        :items="warehouses"
                        v-model="editedItem.warehouseId"
                        item-value="id"
                        item-text="location"
                        label="Warehouse"
                        :rules="[requiredSelect]"
                      ></v-select>
                    </v-col>
                    <v-col cols="6" sm="2" md="2">
                      <v-text-field
                        v-model="editedItem.quantity"
                        :rules="[positiveInteger]"
                        label="Quantity"
                        type="number"
                        :min="1"
                      ></v-text-field>
                    </v-col>
                    <v-col class="mt-1" cols="12" sm="12" md="12">
                      <v-text-field
                        v-model="editedItem.description"
                        label="Description"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>
            </v-form>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="close"> Cancel </v-btn>
              <v-btn
                :disabled="isLoading"
                :loading="isLoading"
                color="blue darken-1"
                text
                @click="save"
              >
                Save
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>

        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5"
              >Are you sure you want to delete this item?</v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeDelete"
                >Cancel</v-btn
              >
              <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                >OK</v-btn
              >
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon small class="mr-2" @click="editItem(item)"> mdi-pencil </v-icon>
      <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
    </template>
    <template v-slot:item.goodType="{ item }">
      <v-chip text-color="white" :color="getColor(item.goodType)">
        {{ item.goodTypeString }}
      </v-chip>
    </template>
  </v-data-table>
</template>
<script lang="ts">
import ApiService from "@/services/api-service";
import InventoryDto from "@/models/inventory-dto";
import ItemDto from "@/models/item-dto";
import WarehouseDto from "@/models/warehouse-dto";
import { getColor } from "@/helpers/common";
import { GoodType } from "@/models/good-type";
import validators from "@/helpers/validationHelper";

export default {
  name: "InventoryGrid",
  data: () => ({
    dialog: false,
    dialogDelete: false,
    isLoading: false,
    headers: [
      { text: "Name", align: "start", value: "itemName" },
      { text: "Good type", value: "goodType" },
      { text: "Warehouse", value: "warehouseLocation" },
      { text: "Quantity", value: "quantity" },
      { text: "Description", value: "description", sortable: false },
      { text: "Actions", value: "actions", sortable: false },
    ],
    inventories: [] as InventoryDto[],
    items: [] as ItemDto[],
    warehouses: [] as WarehouseDto[],
    editedIndex: -1,
    editedItem: new InventoryDto(),
    defaultItem: new InventoryDto(),
  }),
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "New Inventory" : "Edit Inventory";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },

  async created() {
    try {
      this.isLoading = true;
      this.inventories = (await ApiService.getInventories()) || [];
      const warehouses = (await ApiService.getWarehouses()) || [];
      this.warehouses = warehouses.sort((x, y) =>
        x.location.localeCompare(y.location)
      );
    } catch (error) {
      this.$toast.error(error.message);
    } finally {
      this.isLoading = false;
    }
  },

  methods: {
    ...validators,
    getColor(goodType: GoodType): string {
      return getColor(goodType);
    },
    async editItem(item: InventoryDto) {
      await this.loadItems();
      this.editedIndex = this.inventories.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },
    async loadItems() {
      try {
        const items = (await ApiService.getItems()) || [];
        this.items = items.sort((x, y) => x.name.localeCompare(y.name));
      } catch (error) {
        this.$toast.error(error.message);
      }
    },
    async onNew() {
      await this.loadItems();
      this.editedIndex = -1;
      this.editedItem = Object.assign({}, this.defaultItem);
      this.dialog = true;
    },

    deleteItem(item: InventoryDto) {
      this.editedIndex = this.inventories.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    async deleteItemConfirm() {
      try {
        this.isLoading = true;
        await ApiService.deleteInventory(this.editedItem.id);
        this.inventories.splice(this.editedIndex, 1);
        this.closeDelete();
        this.$toast.success("Inventory deleted successfully");
      } catch (error) {
        this.$toast.error(error.message);
      } finally {
        this.isLoading = false;
      }
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    async save() {
      if (!this.$refs.editedItem.validate()) {
        return;
      }

      this.updateEditedItem();
      if (this.editedIndex > -1) {
        await this.saveInventory();
      } else {
        await this.createInventory();
      }
      this.close();
    },
    async saveInventory() {
      try {
        this.isLoading = true;
        const inventory = await ApiService.saveInventory(this.editedItem);
        this.inventories.splice(this.editedIndex, 1, inventory);
        this.$toast.success("Inventory saved successfully");
      } catch (error) {
        this.$toast.error(error.message);
      } finally {
        this.isLoading = false;
      }
    },
    async createInventory() {
      try {
        this.isLoading = true;
        const inventory = await ApiService.createInventory(this.editedItem);
        this.inventories.push(inventory);
        this.$toast.success("Inventory created successfully");
      } catch (error) {
        this.$toast.error(error.message);
      } finally {
        this.isLoading = false;
      }
    },
    updateEditedItem() {
      const item = this.items.find((x) => x.id === this.editedItem.itemId);
      const warehouse = this.warehouses.find(
        (x) => x.id === this.editedItem.warehouseId
      );

      this.editedItem.itemName = item.name;
      this.editedItem.goodType = item.type;
      this.editedItem.goodTypeString = item.goodTypeString;
      this.editedItem.warehouseLocation = warehouse.location;
    },
  },
};
</script>