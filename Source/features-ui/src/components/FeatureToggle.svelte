<script lang="ts">
	import { updateFeatureValue } from '$functions/api';
	import type { Feature } from '$models';
	import { dashboardStore } from '$stores/dashboard';
	import { Button, Expander, TextBlock, ToggleSwitch } from 'fluent-svelte';
	import ToggleLeftIcon from '@fluentui/svg-icons/icons/toggle_left_28_regular.svg?raw';

	const { selectedAuthScheme } = dashboardStore;

	export let feature: Feature;
	export let checked: boolean;

	let expanded = true;

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

<Expander {expanded}>
	<div style="display: flex; align-items: center;">
		<div style="margin-right: 24px;">{@html ToggleLeftIcon}</div>

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
</Expander>
