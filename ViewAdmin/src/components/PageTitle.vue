<template>
	<div class="title">
		{{title}}
		<el-button v-if="btn" size="small" type="success" icon="el-icon-refresh" ref="refresh" @click="refresh" circle></el-button>
	</div>
</template>

<script>
	import TablePadding from './TablePadding';
	export default {
		name: 'pagetitle',
		props: {
			title: {
				type: String,
				default: ''
			},
			btn: {
				type: Boolean,
				default: true
			}
		},
		data(){
			return{
			}
		},
		methods: {
			refresh: function() {
				location.reload();
			}
		}
	}
</script>

<style scoped="scoped">
	.title {
		text-align: left;
		font-weight: 400;
		font-size: 28px;
		padding-bottom: 20px;
	}

	.title button {
		float: right;
		margin-left: 5px;
	}
</style>
