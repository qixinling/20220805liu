<template>
	<div class="tree">
		<PageTitle title="推荐图谱" :excel="false"></PageTitle>
		<div class="cardbox">
			<el-row>
				<el-col :span="4" style="padding: 10px 0 10px 10px;">
					<el-input style="float: left;" v-model="filterText" placeholder="请输入要查询的用户"></el-input>
				</el-col>
				<el-col :span="20"></el-col>
			</el-row>
			<el-row>
				<el-col :span="24" style="padding: 0 0 20px 10px">
					<el-switch v-model="value" active-color="#409EFF" inactive-color="#909399" inactive-text="展开全部"
						@change="expandChange">
					</el-switch>
				</el-col>
			</el-row>
			<el-tree class="filter-tree" :data="data" :props="defaultProps" node-key="Id"
				:filter-node-method="filterNode" ref="tree">
				<span slot-scope="{ node, data }">
					<i :class="data.Icon"></i>
					<span class="trrr_span">{{ node.label }}</span>
				</span>
			</el-tree>
		</div>
	</div>
</template>

<script>
	import Vue from 'vue';
	import PageTitle from '../../components/PageTitle';
	import Tree from "element-ui/lib/tree";
	import "element-ui/lib/theme-chalk/icon.css";
	import "element-ui/lib/theme-chalk/tree.css";

	import {
		Switch,
		Input
	} from 'element-ui';

	Vue.use(Tree);
	Vue.use(Switch);
	Vue.use(Input);
	export default {
		name: 'tree',
		components: {
			PageTitle
		},
		data() {
			return {
				value: false,
				filterText: "",
				data: [],
				expandAll: false,
				defaultProps: {
					children: "Children",
					label: "Label"
				}
			}
		},
		created() {
			this.getdata();
		},
		methods: {
			getdata: function() { // 查询用户
				this.$request.post("Api/Users_Admin/Tree", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.data = JSON.parse(res.data.data.tree);
				});

			},
			filterNode(value, data) {
				if (!value) return true;
				return data.Label.indexOf(value) !== -1;
			},
			expandChange() {
				for (var i = 0; i < this.$refs.tree.store._getAllNodes().length; i++) {
					this.$refs.tree.store._getAllNodes()[i].expanded = this.value;
				}

			},
		},
		watch: {
			filterText(val) {
				this.$refs.tree.filter(val);
			},
		}
	}
</script>

<style scoped="scoped">

	.filter-tree {
		width: 100%;
	}

	/deep/.ispay0 {
		color: #909399;
	}

	/deep/.ispay1 {
		color: #67c23a;
	}

	/deep/.ispay2 {
		color: #e6a23c;
	}

	.trrr_span {
		padding-left: 4px;
		font-size: 14px;
	}
</style>
