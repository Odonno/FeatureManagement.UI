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
	}

	main {
		margin-top: 70px;
		padding: 0 16px;
	}
</style>
