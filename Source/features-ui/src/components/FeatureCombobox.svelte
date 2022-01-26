<script lang="ts">
	import { updateFeatureValue } from '$functions/api';
	import type { Feature, FeatureChoicesType } from '$models';
	import { dashboardStore } from '$stores/dashboard';
	import { Button, TextBlock, ComboBox, Expander } from 'fluent-svelte';
	import FeaturePrefix from './FeaturePrefix.svelte';
	import FeatureSuffix from './FeatureSuffix.svelte';
	import NumberRowIcon from '@fluentui/svg-icons/icons/number_row_24_regular.svg?raw';
	import TextChangeCaseIcon from '@fluentui/svg-icons/icons/text_change_case_24_regular.svg?raw';

	const { selectedAuthScheme } = dashboardStore;

	export let feature: Feature;
	export let value: FeatureChoicesType;

	let expanded = true;

	const items = feature.choices.map((choice) => ({ name: choice }));

	let newValue = value;

	$: hasChanged = value !== newValue;

	$: canSave = hasChanged;
	$: canCancel = hasChanged;

	const featureType = typeof value;

	const onValidateButtonClicked = () => {
		updateFeatureValue($selectedAuthScheme, feature.name, newValue);
	};

	const onCancelButtonClicked = () => {
		newValue = value;
	};
</script>

<Expander {expanded}>
	<div style="display: flex; align-items: center;">
		<div style="margin-right: 24px;">
			{#if featureType === 'number'}
				{@html NumberRowIcon}
			{:else if featureType === 'string'}
				{@html TextChangeCaseIcon}
			{/if}
		</div>

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
			<ComboBox
				bind:value={newValue}
				{items}
				placeholder="Select an option"
				disabled={feature.readonly}
			/>
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
