import adapter from '@sveltejs/adapter-static';
import preprocess from 'svelte-preprocess';
import path from 'path';

const buildOutputPath = '../AspNetCore.FeatureManagement.UI/AspNetCore.FeatureManagement.UI/ui';
const dev = process.env.NODE_ENV !== 'production';

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

		paths: {
			base: dev ? '' : '/features-ui'
		},

		adapter: adapter({
			pages: buildOutputPath,
			assets: buildOutputPath
		}),

		// hydrate the <div id="svelte"> element in src/app.html
		target: '#svelte'
	}
};

export default config;
