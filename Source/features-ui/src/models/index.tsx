export type FeatureValueType = boolean | number | string;
export type FeatureChoicesType = number | string;

type FeatureBase<T> = {
    name: string;
    description: string;
    value: T;
    choices?: T[];
    readonly: boolean;
    uiPrefix?: string;
    uiSuffix?: string;
};

export type Feature =
    | FeatureBase<boolean>
    | FeatureBase<number>
    | FeatureBase<string>;

export type AuthenticationScheme =
    | { type: 'None' }
    | { type: 'Query', key: string }
    | { type: 'Header', key: string };