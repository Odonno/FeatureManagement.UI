<script lang="ts">
	import LockIcon from '@fluentui/svg-icons/icons/lock_closed_24_regular.svg?raw';
	import { Button, TextBlock } from 'fluent-svelte';
	import AuthDialogContent from './AuthDialogContent.svelte';

	let isDialogOpened = false;

	const handleAuthClicked = () => {
		isDialogOpened = true;
	};
</script>

<div
	style="height: 100%; display: flex; flex-direction: column; justify-content: center; align-items: center;"
>
	<div style="margin-bottom: 12px;">
		<TextBlock variant="body">Please select an authentication scheme...</TextBlock>
	</div>

	<Button type="button" variant="accent" on:click={handleAuthClicked}>
		<div class="auth-button">
			{@html LockIcon}
			<TextBlock variant="bodyStrong">Auth</TextBlock>
		</div>
	</Button>
</div>

<AuthDialogContent bind:open={isDialogOpened} />

<style lang="scss">
	.auth-button {
		display: flex;
		align-items: center;
		justify-content: center;

		:global(svg) {
			margin-right: 8px;
		}
	}
</style>
