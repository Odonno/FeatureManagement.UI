<script lang="ts">
	import { TextBlock } from 'fluent-svelte';

	export let value: string;
</script>

<div class="container">
	<TextBlock>{value}</TextBlock>
</div>

<style lang="scss">
	.container {
		margin-left: 12px;
		display: flex;
		justify-content: center;
		align-items: center;
		padding: 6px 12px;
		background: var(--fds-control-fill-default);
		color: var(--fds-text-primary);
		border-radius: var(--fds-control-corner-radius);
	}
</style>
