import { GoodType } from "./good-type";

export default class ItemDto {
  public id: number;
  public name: string;
  public description?: string;
  public type: GoodType;
  public goodTypeString: string;

  constructor(
    id?: number,
    name?: string,
    description?: string,
    type?: GoodType
  ) {
    this.id = id || 0;
    this.name = name || "";
    this.description = description || "";
    this.type = type || -1;
  }

  deserialize(item: ItemDto): ItemDto {
    if (item) {
      Object.assign(this, item);
      this.goodTypeString = GoodType[this.type];
    }
    return this;
  }
}
