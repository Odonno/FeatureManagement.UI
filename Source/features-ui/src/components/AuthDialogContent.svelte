<script lang="ts">
	import { dashboardStore } from '$stores/dashboard';
	import { Button, ContentDialog } from 'fluent-svelte';
	import AuthCardSelection from './AuthCardSelection.svelte';

	const { authSchemes } = dashboardStore;

	export let open: boolean;
</script>

<ContentDialog
	bind:open
	title="Authentication"
	on:backdropclick={() => (open = false)}
	on:backdropmousedown={() => (open = false)}
>
	<div>Select the desired authentication scheme to use to obtain additional rights.</div>

	<p>
		{#each $authSchemes as authScheme}
			<AuthCardSelection {authScheme} />
		{/each}
	</p>

	<Button slot="footer" on:click={() => (open = false)}>Close</Button>
</ContentDialog>
