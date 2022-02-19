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

export type Feature = FeatureBase<boolean> | FeatureBase<number> | FeatureBase<string>;

type NoneAuthenticationSchemeType = 'None';
type QueryAuthenticationSchemeType = 'Query';
type HeaderAuthenticationSchemeType = 'Header';

export type AuthenticationSchemeType =
	| NoneAuthenticationSchemeType
	| QueryAuthenticationSchemeType
	| HeaderAuthenticationSchemeType;

export type AuthenticationScheme =
	| { type: NoneAuthenticationSchemeType }
	| { type: QueryAuthenticationSchemeType; key: string; value: string }
	| { type: HeaderAuthenticationSchemeType; key: string; value: string };
