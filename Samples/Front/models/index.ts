export type Featuretype = boolean | number | string;

export type Feature = {
    name: string;
    description: string;
    value: Featuretype;
};