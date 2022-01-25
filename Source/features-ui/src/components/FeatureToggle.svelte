<script lang="ts">
	import { updateFeatureValue } from '$functions/api';
	import type { Feature } from '$models';
	import { dashboardStore } from '$stores/dashboard';
	import { Button, TextBlock, ToggleSwitch } from 'fluent-svelte';

	const { selectedAuthScheme } = dashboardStore;

	export let feature: Feature;
	export let checked: boolean;

	let newValue = checked;

	$: hasChanged = checked !== newValue;

	$: canSave = hasChanged;
	$: canCancel = hasChanged;

	const onValidateButtonClicked = () => {
		updateFeatureValue($selectedAuthScheme, feature.name, newValue);
	};

	const onCancelButtonClicked = () => {
		newValue = checked;
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

	<ToggleSwitch
		bind:checked={newValue}
		disabled={feature.readonly}
		prefix={feature.uiPrefix}
		suffix={feature.uiSuffix}
	/>

	<p>
		{#if canSave}
			<Button variant="accent" on:click={onValidateButtonClicked}>Save changes</Button>
		{/if}
		{#if canCancel}
			<Button on:click={onCancelButtonClicked}>Cancel</Button>
		{/if}
	</p>
</div>
