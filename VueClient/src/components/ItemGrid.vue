<template>
  <v-data-table
    :headers="headers"
    :items="items"
    class="elevation-1"
    :loading="isLoading"
    loading-text="Loading... Please wait"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>Current existing items</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" persistent max-width="700px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
              <v-icon right dark class="mr-2"> mdi-briefcase-plus </v-icon>
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
                    <v-col cols="12" sm="10" md="6">
                      <v-text-field
                        :items="items"
                        v-model="editedItem.name"
                        label="Name"
                        :rules="[required]"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="10" md="6">
                      <v-select
                        :items="goodTypes"
                        v-model="editedItem.type"
                        item-value="type"
                        item-text="name"
                        label="Type"
                        :rules="[requiredSelect]"
                      ></v-select>
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
    <template v-slot:item.type="{ item }">
      <v-chip text-color="white" :color="getColor(item.type)">
        {{ item.goodTypeString }}
      </v-chip>
    </template>
  </v-data-table>
</template>
<script lang="ts">
import ApiService from "@/services/api-service";
import ItemDto from "@/models/item-dto";
import { getColor } from "@/helpers/common";
import { GoodType } from "@/models/good-type";
import validators from "@/helpers/validationHelper";

export default {
  name: "ItemGrid",
  data: () => ({
    dialog: false,
    dialogDelete: false,
    isLoading: false,
    headers: [
      { text: "Name", align: "start", value: "name" },
      { text: "Good type", value: "type" },
      { text: "Description", value: "description", sortable: false },
      { text: "Actions", value: "actions", sortable: false },
    ],
    items: [] as ItemDto[],
    editedIndex: -1,
    editedItem: new ItemDto(),
    defaultItem: new ItemDto(),
  }),
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "New Item" : "Edit Item";
    },
    goodTypes(): any[] {
      const types = [];
      Object.keys(GoodType).forEach((key) => {
        if (!Number.isNaN(Number(key))) {
          types.push({
            type: Number(key),
            name: GoodType[key],
          });
        }
      });
      return types;
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
      this.items = (await ApiService.getItems()) || [];
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
    async editItem(item: ItemDto) {
      this.editedIndex = this.items.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item: ItemDto) {
      this.editedIndex = this.items.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    async deleteItemConfirm() {
      try {
        this.isLoading = true;
        await ApiService.deleteItem(this.editedItem.id);
        this.items.splice(this.editedIndex, 1);
        this.closeDelete();
        this.$toast.success("Item deleted successfully");
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

      if (this.editedIndex > -1) {
        await this.saveItem();
      } else {
        await this.createItem();
      }
      this.close();
    },
    async saveItem() {
      try {
        this.isLoading = true;
        const item = await ApiService.saveItem(this.editedItem);
        this.items.splice(this.editedIndex, 1, item);
        this.$toast.success("Item saved successfully");
      } catch (error) {
        this.$toast.error(error.message);
      } finally {
        this.isLoading = false;
      }
    },
    async createItem() {
      try {
        this.isLoading = true;
        const item = await ApiService.createItem(this.editedItem);
        this.items.push(item);
        this.$toast.success("Item created successfully");
      } catch (error) {
        this.$toast.error(error.message);
      } finally {
        this.isLoading = false;
      }
    },
  },
};
</script>