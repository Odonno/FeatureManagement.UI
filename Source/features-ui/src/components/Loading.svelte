<script lang="ts">
	import { ProgressRing, TextBlock } from 'fluent-svelte';
</script>

<div
	style="height: 100%; display: flex; flex-direction: column; justify-content: center; align-items: center;"
>
	<ProgressRing />
	<div style="margin-top: 12px;">
		<TextBlock>Loading...</TextBlock>
	</div>
</div>
