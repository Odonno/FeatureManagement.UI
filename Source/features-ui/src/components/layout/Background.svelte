<script lang="ts">
	import 'fluent-svelte/theme.css';
	import { onMount } from 'svelte';
	import bgLight from '$assets/bloom-mica-light.png';
	import bgDark from '$assets/bloom-mica-dark.png';

	const applyBackground = (colorScheme: 'dark' | 'light') => {
		const backgroundElement = document.getElementsByClassName('background')[0] as HTMLDivElement;

		if (colorScheme === 'dark') {
			backgroundElement.style.backgroundImage = `url(${bgDark})`;
		} else {
			backgroundElement.style.background = `url(${bgLight}) center/170% no-repeat fixed`;
		}
	};

	onMount(() => {
		window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
			const colorScheme = e.matches ? 'dark' : 'light';
			applyBackground(colorScheme);
		});

		const colorScheme = window.matchMedia('(prefers-color-scheme: dark)').matches
			? 'dark'
			: 'light';
		applyBackground(colorScheme);
	});
</script>

<div class="background" />

<style lang="scss">
	.background {
		position: fixed;
		z-index: -1;
		width: 100vw;
		height: 100vh;

		box-shadow: inset 0 0 0 100vmax var(--fds-card-background-secondary);
	}
</style>
