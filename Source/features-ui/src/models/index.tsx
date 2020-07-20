export type FeatureType = boolean | number | string;
export type FeatureChoicesType = number | string;

type FeatureBase<T> = {
    name: string;
    description: string;
    value: T;
    choices?: T[];
};

export type Feature = 
    | FeatureBase<boolean>
    | FeatureBase<number>
    | FeatureBase<string>;