<script lang="ts">
	import LockIcon from '@fluentui/svg-icons/icons/lock_closed_24_regular.svg?raw';
	import { Button, TextBlock } from 'fluent-svelte';
	import AuthDialogContent from './AuthDialogContent.svelte';
	import { base } from '$app/paths';

	let isDialogOpened = false;

	const handleAuthClicked = () => {
		isDialogOpened = true;
	};
</script>

<header class="navbar">
	<div class="navbar-inner">
		<a class="logo" href={base + '/'}>
			<!-- TODO : svg logo -->
			<TextBlock variant="title">Features Management</TextBlock>
			<TextBlock variant="caption">
				<span class="preview">PREVIEW</span>
			</TextBlock>
		</a>
	
		<Button type="button" variant="accent" on:click={handleAuthClicked}>
			<div class="auth-button">
				{@html LockIcon}
				<TextBlock variant="bodyStrong">Auth</TextBlock>
			</div>
		</Button>
	</div>
</header>

<AuthDialogContent bind:open={isDialogOpened} />

<style lang="scss">
	.navbar {
		backdrop-filter: blur(60px);
		background-clip: padding-box;
		background-color: var(--fds-layer-background-default);
		border-bottom: 1px solid var(--fds-surface-stroke-flyout);
		box-sizing: border-box;
		inset-block-start: 0;
		inset-inline-start: 0;
		justify-content: center;
		min-block-size: 56px;
		z-index: 1000;
		position: fixed;
		left: 0;
		right: 0;
		display: flex;

		&-inner {
			display: flex;
			align-items: center;
			align-items: center;
			justify-content: space-between;
			inline-size: 100%;
			max-inline-size: 1440px;
			padding-inline: 18px 12px;
			margin: 0 auto;
		}
	}

	.logo {
		display: flex;
		align-items: center;
		justify-content: center;
		border-radius: var(--control-corner-radius);
		position: relative;
		text-decoration: none;
		color: var(--text-primary);
		transition: var(--control-fast-duration) var(--control-fast-out-slow-in-easing);

		font: {
			family: var(--fds-font-family-display);
			size: 20px;
			weight: 600;
		}

		.preview {
			margin-left: 12px;
		}
	}

	.auth-button {
		display: flex;
		align-items: center;
		justify-content: center;

		:global(svg) {
			margin-right: 8px;
		}
	}
</style>
