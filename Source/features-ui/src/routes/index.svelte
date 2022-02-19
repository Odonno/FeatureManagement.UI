<script context="module" lang="ts">
	export const prerender = true;
</script>

<script lang="ts">
	import { dashboardStore } from '$stores/dashboard';
	import { onMount } from 'svelte';
	import { loadAuthSchemes, loadFeatures } from '$functions/api';
	import Loading from '$components/layout/Loading.svelte';
	import AuthSchemeRequired from '$components/AuthSchemeRequired.svelte';
	import FeatureCard from '$components/FeatureCard.svelte';

	const { loading, features, authSchemes, selectedAuthScheme } = dashboardStore;

	onMount(async () => {
		loading.set(true);

		await loadAuthSchemes($selectedAuthScheme);

		// selected default auth scheme if none is selected
		if ($authSchemes !== undefined && $selectedAuthScheme === undefined) {
			const hasAnonymousAuth =
				$authSchemes.length <= 0 || $authSchemes.some((as) => as.type === 'None');

			if (hasAnonymousAuth) {
				selectedAuthScheme.set({ type: 'None' });
			}
		}

		// reload features when selected auth scheme changes
		selectedAuthScheme.subscribe(async (authScheme) => {
			loading.set(true);

			await loadFeatures(authScheme);

			loading.set(false);
		});
	});
</script>

<div class="container">
	{#if $loading}
		<Loading />
	{:else if $selectedAuthScheme === undefined}
		<AuthSchemeRequired />
	{:else}
		<div style="width: 100%; align-self: flex-start;">
			{#each $features as feature (feature.name)}
				<div style="margin-bottom: 4px;">
					<FeatureCard {feature} />
				</div>
			{/each}
		</div>
	{/if}
</div>

<style lang="scss">
	.container {
		height: 100%;
		display: flex;
		align-items: center;
		align-items: center;
		justify-content: space-between;
		inline-size: 100%;
		max-inline-size: 1440px;
		margin: 0 auto;
	}
</style>
