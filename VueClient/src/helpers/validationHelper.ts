export function positiveInteger(value: string): boolean | string {
  const num = Number(value);
  return (
    (Number.isInteger(num) && num > 0) || "Should be integer greater than 0"
  );
}

export function requiredSelect(value: any): boolean | string {
  return Number(value) > -1 || "Required";
}

export function required(value: any): boolean | string {
  return (value !== undefined && value !== null && value !== "") || "Required";
}

const validationRules = {
  positiveInteger,
  requiredSelect,
  required,
};

export default validationRules;
