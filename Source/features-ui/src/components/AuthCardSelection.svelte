<script lang="ts">
	import type { AuthenticationScheme } from '$models';
	import { dashboardStore } from '$stores/dashboard';
	import { Button, TextBox } from 'fluent-svelte';

	const { selectedAuthScheme } = dashboardStore;

	export let authScheme: AuthenticationScheme;

	let value = '';

	const isSelected = (s1: AuthenticationScheme, s2: AuthenticationScheme) => {
		if (s1.type === 'None') {
			return s2.type === 'None';
		}

		return s1.type === s2.type && s1.key === s2.key;
	};

	$: selected = isSelected(authScheme, $selectedAuthScheme);

	const handleSelectClicked = () => {
		if (authScheme.type === 'None') {
			selectedAuthScheme.set({ type: 'None' });
			return;
		}

		selectedAuthScheme.set({
			...authScheme,
			value
		});

		value = '';
	};
</script>

{#if authScheme.type === 'None'}
	<div {selected}>
		<div>No authentication</div>

		{#if selected === false}
			<Button variant="accent" on:click={handleSelectClicked}>Select</Button>
		{/if}
	</div>
{:else}
	<div {selected}>
		<div>
			Type: {authScheme.type}
		</div>
		<div>
			Key: {authScheme.key}
		</div>

		{#if selected}
			<TextBox value="******" readonly={true} />
		{:else}
			<TextBox placeholder="Value" bind:value />
		{/if}

		{#if selected === false}
			<Button variant="accent" on:click={handleSelectClicked}>Select</Button>
		{/if}
	</div>
{/if}
