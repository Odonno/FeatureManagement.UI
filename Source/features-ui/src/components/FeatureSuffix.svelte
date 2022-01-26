<script lang="ts">
	import { TextBlock } from 'fluent-svelte';

	export let value: string;
</script>

<div
	style="display: flex; justify-content: center; align-items: center; padding: 6px 12px; background: #161514; color: #edebe9;"
>
	<TextBlock>{value}</TextBlock>
</div>
