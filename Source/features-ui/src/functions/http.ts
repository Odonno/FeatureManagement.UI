import type { AuthenticationScheme } from '$models';

export const createRequest = (
	url: string,
	authScheme?: AuthenticationScheme,
	request?: RequestInit
): Promise<Response> => {
	if (authScheme && authScheme.type === 'Header') {
		request = {
			...(request || {}),
			headers: {
				...(request.headers || {}),
				[authScheme.key]: authScheme.value
			}
		};
	}
	if (authScheme && authScheme.type === 'Query') {
		url += `?${authScheme.key}=${authScheme.value}`;
	}

	return fetch(url, request);
};
