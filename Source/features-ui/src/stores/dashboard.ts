import type { AuthenticationScheme, Feature } from '$models';
import { writable, derived } from 'svelte/store';

const loading = writable(true);

const authSchemes = writable<AuthenticationScheme[] | undefined>(undefined);
const selectedAuthScheme = writable<AuthenticationScheme | undefined>(undefined);

const features = writable<Feature[] | undefined>(undefined);

const authSelectionEnabled = derived(
	authSchemes,
	($authSchemes) =>
		$authSchemes && $authSchemes.length > 0 && $authSchemes.some((as) => as.type !== 'None')
);

export const dashboardStore = {
	loading,
	authSchemes,
	selectedAuthScheme,
	features,
	authSelectionEnabled
};
