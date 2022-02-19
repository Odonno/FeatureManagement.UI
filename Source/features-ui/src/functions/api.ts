import { env } from '$config';
import type { AuthenticationScheme, Feature, FeatureValueType } from '$models';
import { dashboardStore } from '$stores/dashboard';
import { createRequest } from './http';

export const loadFeatures = async (selectedAuthScheme: AuthenticationScheme): Promise<void> => {
	const response = await createRequest(env.apiEndpoint, selectedAuthScheme);
	if (response.ok) {
		dashboardStore.features.set(await response.json());
	}
};

export const loadAuthSchemes = async (selectedAuthScheme: AuthenticationScheme): Promise<void> => {
	const response = await createRequest(`${env.apiEndpoint}/auth/schemes`, selectedAuthScheme);
	if (response.ok) {
		dashboardStore.authSchemes.set(await response.json());
	}
};

export const updateFeatureValue = async (
	selectedAuthScheme: AuthenticationScheme,
	featureName: string,
	value: FeatureValueType
): Promise<void> => {
	const payload = {
		value
	};

	const request = {
		method: 'POST',
		body: JSON.stringify(payload)
	};

	const response = await createRequest(
		`${env.apiEndpoint}/${featureName}/set`,
		selectedAuthScheme,
		request
	);
	if (response.ok) {
		const updatedFeature = (await response.json()) as Feature;

		dashboardStore.features.update((features) => {
			return features.map((f) => {
				if (f.name === featureName) {
					return updatedFeature;
				}
				return f;
			});
		});
	}
};
