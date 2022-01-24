<script lang="ts">
	import { dashboardStore } from '$stores/dashboard';
	import { onMount } from 'svelte';
	import FeatureToggle from '$components/FeatureToggle.svelte';
	import FeatureTextInput from '$components/FeatureTextInput.svelte';
	import FeatureCombobox from '$components/FeatureCombobox.svelte';
	import FeatureNumberInput from '$components/FeatureNumberInput.svelte';
	import { loadAuthSchemes, loadFeatures } from '$functions/api';
	import Loading from '$components/Loading.svelte';
	import AuthSchemeRequired from '$components/AuthSchemeRequired.svelte';

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

{#if $loading}
	<Loading />
{:else if $selectedAuthScheme === undefined}
	<AuthSchemeRequired />
{:else}
	<div>
		{#each $features as feature (feature.name)}
			{#if typeof feature.value === 'boolean'}
				<FeatureToggle {feature} checked={feature.value} />
			{:else if feature.choices}
				<FeatureCombobox {feature} value={feature.value} />
			{:else if typeof feature.value === 'string'}
				<FeatureTextInput {feature} value={feature.value} />
			{:else if typeof feature.value === 'number'}
				<FeatureNumberInput {feature} value={feature.value} />
			{/if}
		{/each}
	</div>
{/if}
