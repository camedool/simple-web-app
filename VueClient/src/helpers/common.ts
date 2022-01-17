import { GoodType } from "@/models/good-type";

export function getColor(type: GoodType): string {
  switch (type) {
    case GoodType.HealthCare:
      return "red darken-4";

    case GoodType.Food:
      return "light-green darken-2";

    case GoodType.Digital:
      return "blue-grey darken-1";

    case GoodType.Clothes:
      return "orange darken-2";

    case GoodType.General:
    default:
      return "brown darken-1";
  }
}
