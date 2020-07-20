export type FeatureType = boolean | number | string;

export type Feature = {
    name: string;
    description: string;
    value: FeatureType;
};