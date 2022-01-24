import adapter from '@sveltejs/adapter-auto';
import preprocess from 'svelte-preprocess';
import path from 'path';

/** @type {import('@sveltejs/kit').Config} */
const config = {
	// Consult https://github.com/sveltejs/svelte-preprocess
	// for more information about preprocessors
	preprocess: preprocess(),

	kit: {
		vite: {
			resolve: {
				alias: {
					$components: path.resolve('./src/components'),
					$config: path.resolve('./src/config'),
					$functions: path.resolve('./src/functions'),
					$models: path.resolve('./src/models'),
					$stores: path.resolve('./src/stores')
				}
			}
		},

		adapter: adapter(),

		// hydrate the <div id="svelte"> element in src/app.html
		target: '#svelte'
	}
};

export default config;