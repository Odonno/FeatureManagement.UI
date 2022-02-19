import { dev } from '$app/env';

const baseApiUrl = dev ? 'https://localhost:5001' : window.location.origin;
const apiEndpoint = dev ? '/features' : '/#apiPath#';

const isProduction = !dev;

export const env = {
	isProduction,
	apiEndpoint: `${baseApiUrl}${apiEndpoint}`
};
