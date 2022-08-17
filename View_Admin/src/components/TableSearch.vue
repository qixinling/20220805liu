<template>
	<div>
		<el-collapse>
			<el-collapse-item name="search">
				<template slot="title">
					<i class="el-icon-search" style="font-size: 14px; margin-right: 5px;"></i> <span
						style="font-size: 14px;font-weight: 300;">搜索</span>
				</template>
				<slot class="search_slot"></slot>
			</el-collapse-item>
		</el-collapse>
	</div>
</template>

<script>
	import Vue from 'vue';
	import {
		Collapse,
		CollapseItem
	} from 'element-ui'
	Vue.use(Collapse);
	Vue.use(CollapseItem);
	export default {
		name: 'tablesearch',
		data() {
			return {
			}
		},
		created() {
			//绑定回车事件
			window.addEventListener("keydown", this.keyDown);
		},
		methods: {
			Search(arr,fnc){
				let _newArr = arr;
				fnc(_newArr);
			},
			keyDown() {
				let _key = window.event.keyCode;
				//!this.clickState是防止用户重复点击回车
				if (_key === 13 && !this.clickState) {
					this.$parent.search(1);
				}
			}
		}
	}
</script>

<style scoped>
	.el-collapse {
		border: 0;
	}

	/deep/.el-collapse-item__header {
		border: 0;
	}

	/deep/.el-collapse-item__content {
		padding-bottom: 0;
	}
</style>
