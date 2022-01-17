import InventoryDto from "@/models/inventory-dto";
import ItemDto from "@/models/item-dto";
import WarehouseDto from "@/models/warehouse-dto";
import axios from "axios";

const headers = {
  "Content-Type": "application/json",
};

const API_USERS = "https://localhost:7221/api/";

// TODO: split this service into individual services per controller
export default class ApiService {
  private static _instance = axios.create({
    baseURL: API_USERS,
    headers,
  });

  public static async getInventories(): Promise<
    Array<InventoryDto> | undefined
  > {
    try {
      const response = await this._instance.get("/inventories");
      return response.data.map((x) => new InventoryDto().deserialize(x));
    } catch (error: any) {
      if (error?.response) {
        throw new Error(error?.response?.data);
      } else {
        throw new Error(error);
      }
    }
  }

  public static async getItems(): Promise<Array<ItemDto> | undefined> {
    try {
      const response = await this._instance.get("/items");
      return response.data.map((x) => new ItemDto().deserialize(x));
    } catch (error: any) {
      if (error?.response) {
        throw new Error(error?.response?.data);
      } else {
        throw new Error(error);
      }
    }
  }

  public static async getWarehouses(): Promise<
    Array<WarehouseDto> | undefined
  > {
    try {
      const response = await this._instance.get("/warehouses");
      return response.data;
    } catch (error: any) {
      if (error?.response) {
        throw new Error(error?.response?.data);
      } else {
        throw new Error(error);
      }
    }
  }

  public static async saveInventory(
    inventory: InventoryDto
  ): Promise<InventoryDto | undefined> {
    try {
      const response = await this._instance.put(
        `/inventories/${inventory.id}`,
        inventory
      );
      return new InventoryDto().deserialize(response.data);
    } catch (error: any) {
      if (error?.response) {
        throw new Error(error?.response?.data);
      } else {
        throw new Error(error);
      }
    }
  }

  public static async createInventory(
    inventory: InventoryDto
  ): Promise<InventoryDto | undefined> {
    try {
      const response = await this._instance.post("/inventories", inventory);
      return new InventoryDto().deserialize(response.data);
    } catch (error: any) {
      if (error?.response) {
        throw new Error(error?.response?.data);
      } else {
        throw new Error(error);
      }
    }
  }

  public static async deleteInventory(inventoryId: number): Promise<void> {
    try {
      await this._instance.delete(`/inventories/${inventoryId}`);
    } catch (error: any) {
      if (error?.response) {
        throw new Error(error?.response?.data);
      } else {
        throw new Error(error);
      }
    }
  }
}