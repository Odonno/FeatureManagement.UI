<script lang="ts">
	import type { AuthenticationScheme } from '$models';
	import { dashboardStore } from '$stores/dashboard';
	import { Button, Expander, TextBlock, TextBox } from 'fluent-svelte';

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

	let expanded = false;

	const handleSelectClicked = () => {
		expanded = false;

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

<Expander bind:expanded class={`auth-expander ${selected ? 'selected' : ''}`}>
	{#if authScheme.type === 'None'}
		<div>No authentication</div>
	{:else}
		<div>
			<TextBlock variant="bodyStrong">Type:</TextBlock>
			<TextBlock>{authScheme.type}</TextBlock>
			<TextBlock>|</TextBlock>
			<TextBlock variant="bodyStrong">Key:</TextBlock>
			<TextBlock>{authScheme.key}</TextBlock>
		</div>
	{/if}

	<div slot="content">
		{#if authScheme.type === 'None'}
			{#if selected === false}
				<Button variant="accent" on:click={handleSelectClicked}>Select this mode</Button>
			{/if}
		{:else}
			<TextBlock variant="caption">Value</TextBlock>
			{#if selected}
				<TextBox value="******" readonly={true} />
			{:else}
				<TextBox placeholder="Value" bind:value />
			{/if}

			{#if selected === false}
				<div style="margin-top: 12px;">
					<Button variant="accent" on:click={handleSelectClicked}>Select this mode</Button>
				</div>
			{/if}
		{/if}
	</div>
</Expander>

<style lang="scss">
	:global {
		.auth-expander {
			margin-bottom: 8px;

			&.selected {
				border: 2px solid var(--fds-accent-default);
			}
		}
	}
</style>
