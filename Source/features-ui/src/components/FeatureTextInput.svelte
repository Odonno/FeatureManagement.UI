<script lang="ts">
	import { updateFeatureValue } from '$functions/api';
	import type { Feature } from '$models';
	import { dashboardStore } from '$stores/dashboard';
	import { Button, TextBlock, TextBox } from 'fluent-svelte';

	const { selectedAuthScheme } = dashboardStore;

	export let feature: Feature;
	export let value: string;

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

<div>
	<p style="display: flex; flex-direction: column;">
		<TextBlock variant="subtitle">
			{feature.name}
		</TextBlock>
		{#if feature.description}
			<TextBlock variant="caption">
				{feature.description}
			</TextBlock>
		{/if}
	</p>

	<TextBox
		bind:value={newValue}
		disabled={feature.readonly}
		prefix={feature.uiPrefix}
		suffix={feature.uiSuffix}
	/>

	<p>
		{#if canSave}
			<Button variant="accent" on:click={onValidateButtonClicked}>Save</Button>
		{/if}
		{#if canCancel}
			<Button on:click={onCancelButtonClicked}>Cancel</Button>
		{/if}
	</p>
</div>