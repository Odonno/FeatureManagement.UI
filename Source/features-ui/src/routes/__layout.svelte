<script lang="ts">
	import 'fluent-svelte/theme.css';
</script>

<main>
	<slot />
</main>

<style>
	:global(body) {
		background-color: var(--fds-solid-background-base);
		color: var(--fds-text-primary);
	}
</style>
