<script lang="ts">
	import 'fluent-svelte/theme.css';
	import NavBar from '$components/NavBar.svelte';
</script>

<NavBar />

<main>
	<slot />
</main>

<style>
	:global(body) {
		background-color: var(--fds-solid-background-base);
		color: var(--fds-text-primary);
		margin: 0;
	}

	:global(html, body) {
		height: 100%;
	}

	:global(#svelte) {
		height: 100%;
		display: flex;
		flex-direction: column;
	}

	main {
		margin-top: 70px;
		padding: 0 16px;
		height: 100%;
	}
</style>
