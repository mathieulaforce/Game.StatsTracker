export const capitalize = (value: string) => {
  if (!value || !value.length) {
    return value;
  }
  return `${value[0].toUpperCase()}${value.slice(1)}`;
};
