<script lang="ts">
	import type { Feature } from '$models';
	import FeatureToggle from '$components/FeatureToggle.svelte';
	import FeatureTextInput from '$components/FeatureTextInput.svelte';
	import FeatureCombobox from '$components/FeatureCombobox.svelte';
	import FeatureNumberInput from '$components/FeatureNumberInput.svelte';

	export let feature: Feature;
</script>

{#if typeof feature.value === 'boolean'}
	<FeatureToggle {feature} checked={feature.value} />
{:else if feature.choices}
	<FeatureCombobox {feature} value={feature.value} />
{:else if typeof feature.value === 'string'}
	<FeatureTextInput {feature} value={feature.value} />
{:else if typeof feature.value === 'number'}
	<FeatureNumberInput {feature} value={feature.value} />
{/if}
