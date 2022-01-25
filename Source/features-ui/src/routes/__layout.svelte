<script lang="ts">
	import 'fluent-svelte/theme.css';
	import NavBar from '$components/NavBar.svelte';
</script>

<div class="background" />

<NavBar />

<main>
	<slot />
</main>

<style lang="scss">
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

	.background {
		position: fixed;
    	z-index: -1;
		width: 100vw;
		height: 100vh;

		box-shadow: inset 0 0 0 100vmax var(--fds-card-background-secondary);
		background: url("/bloom-mica-light.png") center/170% no-repeat fixed;
		@media (prefers-color-scheme: dark) {
			background-image: url("/bloom-mica-dark.png");
		}
	}

	main {
		margin-top: 70px;
		padding: 0 16px;
		height: 100%;
	}
</style>
