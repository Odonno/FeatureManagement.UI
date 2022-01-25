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
	<Expander {expanded} class="auth-expander">
		<div>No authentication</div>

		<div slot="content">
			{#if selected === false}
				<Button variant="accent" on:click={handleSelectClicked}>
					Select this mode
				</Button>
			{/if}
		</div>
	</Expander>
{:else}
	<Expander {expanded} class="auth-expander">
		<div>
			Type: {authScheme.type} / Key: {authScheme.key}
		</div>

		<div slot="content">
			<TextBlock variant="caption">
				Value
			</TextBlock>
			{#if expanded}
				<TextBox value="******" readonly={true} />
			{:else}
				<TextBox placeholder="Value" bind:value />
			{/if}

			{#if selected === false}
				<div style="margin-top: 12px;">
					<Button variant="accent" on:click={handleSelectClicked}>
						Select this mode
					</Button>
				</div>
			{/if}
		</div>
	</Expander>
{/if}

<style lang="scss">
	:global(.auth-expander) {
		margin-bottom: 1rem;
	}
</style>