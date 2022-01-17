import { GoodType } from "./good-type";

export default class InventoryDto {
  public id: number;
  public itemName: string;
  public description: string;
  public goodType: GoodType;
  public goodTypeString?: string;
  public itemId: number;
  public warehouseId: number;
  public warehouseLocation: string;
  public quantity: number;

  constructor(
    id?: number,
    itemName?: string,
    description?: string,
    goodType?: GoodType,
    itemId?: number,
    warehouseId?: number,
    warehouseLocation?: string,
    quantity?: number
  ) {
    this.id = id || 0;
    this.itemName = itemName || "";
    this.description = description || "";
    this.goodType = goodType || GoodType.General;
    this.itemId = itemId || -1;
    this.warehouseId = warehouseId || -1;
    this.warehouseLocation = warehouseLocation || "";
    this.quantity = quantity || 0;
  }

  deserialize(inventory: InventoryDto): InventoryDto {
    if (inventory) {
      Object.assign(this, inventory);
      this.goodTypeString = GoodType[this.goodType];
    }
    return this;
  }
}
