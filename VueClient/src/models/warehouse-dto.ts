export default class WarehouseDto {
  public id: number;
  public name: string;
  public location: string;

  public get nameAndLocation(): string {
    return `${this.name}-(${this.location})`;
  }

  constructor(id?: number, name?: string, location?: string) {
    this.id = id || 0;
    this.name = name || "";
    this.location = location || "";
  }

  deserialize(warehouse: WarehouseDto): WarehouseDto {
    if (warehouse) {
      Object.assign(this, warehouse);
    }

    return this;
  }
}
