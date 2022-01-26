<script lang="ts">
	import { updateFeatureValue } from '$functions/api';
	import type { Feature } from '$models';
	import { dashboardStore } from '$stores/dashboard';
	import { Button, TextBlock, NumberBox, Expander } from 'fluent-svelte';
	import FeaturePrefix from './FeaturePrefix.svelte';
	import FeatureSuffix from './FeatureSuffix.svelte';
	import NumberRowIcon from '@fluentui/svg-icons/icons/number_row_24_regular.svg?raw';

	const { selectedAuthScheme } = dashboardStore;

	export let feature: Feature;
	export let value: number;

	let expanded = true;

	let newValue = value;

	$: hasChanged = value !== newValue;

	$: canSave = hasChanged;
	$: canCancel = hasChanged;

	const onValidateButtonClicked = () => {
		updateFeatureValue($selectedAuthScheme, feature.name, newValue);
	};

	const onCancelButtonClicked = () => {
		newValue = value;
	};
</script>

<Expander {expanded}>
	<div style="display: flex; align-items: center;">
		<div style="margin-right: 24px;">{@html NumberRowIcon}</div>

		<div style="display: flex; flex-direction: column;">
			<TextBlock variant="bodyStrong">
				{feature.name}
			</TextBlock>
			<TextBlock variant="caption">
				{feature.description ?? 'No description'}
			</TextBlock>
		</div>
	</div>

	<div style="padding: 0 42px;" slot="content">
		<div style="display: flex;">
			{#if feature.uiPrefix}
				<FeaturePrefix value={feature.uiPrefix} />
			{/if}
			<NumberBox bind:value={newValue} disabled={feature.readonly} />
			{#if feature.uiSuffix}
				<FeatureSuffix value={feature.uiSuffix} />
			{/if}
		</div>

		<p>
			{#if canSave}
				<Button variant="accent" on:click={onValidateButtonClicked}>Save changes</Button>
			{/if}
			{#if canCancel}
				<Button on:click={onCancelButtonClicked}>Cancel</Button>
			{/if}
		</p>
	</div>
</Expander>
