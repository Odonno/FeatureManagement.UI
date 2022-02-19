<script lang="ts">
	import 'fluent-svelte/theme.css';
	import NavBar from '$components/layout/NavBar.svelte';
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
		window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', function (e) {
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

<NavBar />

<main>
	<slot />
</main>

<style lang="scss">
	:global {
		*,
		*::before,
		*::after {
			box-sizing: border-box;
			-webkit-user-drag: none;
		}

		html,
		body {
			height: 100%;
		}

		body {
			background-color: var(--fds-solid-background-base);
			color: var(--fds-text-primary);
			margin: 0;
		}

		#svelte {
			height: 100%;
			display: flex;
			flex-direction: column;
		}

		.scroller {
			--mica-tint: 0, 0%, 13%;
			--mica-tint-opacity: 0.8;
			overflow: auto;
			@supports (overflow: overlay) {
				overflow: overlay;
			}

			// An attempt at recreating WinUI 2.6 scrollbars
			// (mixed results)
			&::-webkit-scrollbar {
				display: block;
				width: 14px;
				border-radius: 14px;

				// Why does webkit have to be such a pain to work with sometimes?
				// At least it's not firefox.
				&:vertical {
					@media (prefers-color-scheme: light) {
						--scrollbar-caret-top: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'%3E%3Cpath d='M6.10204 16.9814C5.0281 16.9814 4.45412 15.7165 5.16132 14.9083L10.6831 8.59765C11.3804 7.80083 12.6199 7.80083 13.3172 8.59765L18.839 14.9083C19.5462 15.7165 18.9722 16.9814 17.8983 16.9814H6.10204Z' fill='hsl(0, 0%, 0%, 0.447)'/%3E%3C/svg%3E");
						--scrollbar-caret-bottom: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'%3E%3Cpath d='M6.10204 8C5.0281 8 4.45412 9.2649 5.16132 10.0731L10.6831 16.3838C11.3804 17.1806 12.6199 17.1806 13.3172 16.3838L18.839 10.0731C19.5462 9.2649 18.9722 8 17.8983 8H6.10204Z' fill='hsl(0, 0%, 0%, 0.447)'/%3E%3C/svg%3E");
					}
					@media (prefers-color-scheme: dark) {
						--scrollbar-caret-top: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'%3E%3Cpath d='M6.10204 16.9814C5.0281 16.9814 4.45412 15.7165 5.16132 14.9083L10.6831 8.59765C11.3804 7.80083 12.6199 7.80083 13.3172 8.59765L18.839 14.9083C19.5462 15.7165 18.9722 16.9814 17.8983 16.9814H6.10204Z' fill='hsl(0, 0%, 100%, 0.545)'/%3E%3C/svg%3E");
						--scrollbar-caret-bottom: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'%3E%3Cpath d='M6.10204 8C5.0281 8 4.45412 9.2649 5.16132 10.0731L10.6831 16.3838C11.3804 17.1806 12.6199 17.1806 13.3172 16.3838L18.839 10.0731C19.5462 9.2649 18.9722 8 17.8983 8H6.10204Z' fill='hsl(0, 0%, 100%, 0.545)'/%3E%3C/svg%3E");
					}
				}

				&:horizontal {
					@media (prefers-color-scheme: light) {
						--scrollbar-caret-top: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'%3E%3Cpath d='M9 17.8983C9 18.9722 10.2649 19.5462 11.0731 18.839L17.3838 13.3172C18.1806 12.6199 18.1806 11.3804 17.3838 10.6831L11.0731 5.16132C10.2649 4.45412 9 5.02809 9 6.10204V17.8983Z' fill='hsl(0, 0%, 0%, 0.447)'/%3E%3C/svg%3E");
						--scrollbar-caret-bottom: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'%3E%3Cpath d='M15 17.8983C15 18.9722 13.7351 19.5462 12.9268 18.839L6.61617 13.3172C5.81935 12.6199 5.81935 11.3804 6.61617 10.6831L12.9268 5.16132C13.7351 4.45412 15 5.02809 15 6.10204L15 17.8983Z' fill='hsl(0, 0%, 100%, 0.545)'/%3E%3C/svg%3E");
					}
					@media (prefers-color-scheme: dark) {
						--scrollbar-caret-top: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'%3E%3Cpath d='M9 17.8983C9 18.9722 10.2649 19.5462 11.0731 18.839L17.3838 13.3172C18.1806 12.6199 18.1806 11.3804 17.3838 10.6831L11.0731 5.16132C10.2649 4.45412 9 5.02809 9 6.10204V17.8983Z' fill='hsl(0, 0%, 0%, 0.447)'/%3E%3C/svg%3E");
						--scrollbar-caret-bottom: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'%3E%3Cpath d='M15 17.8983C15 18.9722 13.7351 19.5462 12.9268 18.839L6.61617 13.3172C5.81935 12.6199 5.81935 11.3804 6.61617 10.6831L12.9268 5.16132C13.7351 4.45412 15 5.02809 15 6.10204L15 17.8983Z' fill='hsl(0, 0%, 100%, 0.545)'/%3E%3C/svg%3E");
					}
				}

				&:hover {
					background: var(--scrollbar-caret-bottom) bottom center/contain no-repeat,
						var(--scrollbar-caret-top) top center/contain no-repeat,
						hsl(var(--mica-tint), var(--mica-tint-opacity), 100%);
				}

				&-thumb {
					border: 6px solid transparent;
					border-block: none;
					border-radius: 7px;
					background: var(--fds-control-strong-fill-default);
					background-clip: padding-box;

					&:hover {
						border: 4px solid transparent;
					}
				}

				&-button:single-button {
					display: block;
					height: 14px;
				}
			}
		}

		:global(svg) {
			fill: var(--fds-text-primary);
		}
	}

	.background {
		position: fixed;
		z-index: -1;
		width: 100vw;
		height: 100vh;

		box-shadow: inset 0 0 0 100vmax var(--fds-card-background-secondary);
	}

	main {
		margin-top: 70px;
		padding: 0 16px;
		height: 100%;
	}
</style>
