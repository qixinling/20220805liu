<template>
	<div>
		<PageTitle title="常见问题"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="常见问题" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false"  ref="tablepadding">
					<div slot="btn_solt">
						<el-button size="mini" type="success" icon="el-icon-circle-plus-outline" @click="Add">添加
						</el-button>
						<el-button size="mini" type="danger" icon="el-icon-delete" @click="Delete">删除</el-button>
					</div>
					
					<el-table-column prop="id" label="ID" width="80"></el-table-column>
					<el-table-column prop="question" label="问题" width="400"></el-table-column>
					<el-table-column prop="answer" label="答案" width="400"></el-table-column>
					<el-table-column label="排序" width="200">
						<template slot-scope="scope">
							<el-input-number v-model="scope.row.rank" size="mini" :min="0"
								@change="ChangeRank(scope.row)"></el-input-number>
						</template>
					</el-table-column>
					<el-table-column label="是否显示">
						<template slot-scope="scope">
							<el-switch v-model="scope.row.show" @change="ChangeShow(scope.row)"></el-switch>
						</template>
					</el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import PageTitle from "../../components/PageTitle";
	import TablePadding from "../../components/TablePadding";
	import Vue from "vue";
	import {
		Switch,
		InputNumber,
		Notification
	} from "element-ui";
	Vue.use(Switch);
	Vue.use(InputNumber);
	Vue.prototype.$notify = Notification;
	export default {
		name: "AutoReturnList",
		components: {
			PageTitle,
			TablePadding,
		},
		data() {
			return {
				loading: false,
				list: [],
				searchlist: [],
				refresh: false,
			};
		},
		created() {
			this.GetData();
		},
		methods: {
			ChangeRank: function(row) {  // 排序
				this.$request.post(
					"Api/Help_Admin/ChangeRank", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						qid: row.id,
						rank: row.rank
					},
					(res) => {
						Notification({
							title: "操作成功",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
					}
				);
			},
			ChangeShow: function(row) {  // 是否显示
				this.$request.post(
					"Api/Help_Admin/ChangeShow", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						qid: row.id,
					},
					(res) => {
						Notification({
							title: "操作成功",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
					}
				);
			},
			ChangeTop: function(row) {
				this.$request.post(
					"Api/Help_Admin/ChangeTop", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						qid: row.id,
					},
					(res) => {
						Notification({
							title: "操作成功",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
					}
				);
			},
			GetData: function() {
				this.$request.post(
					"Api/Help_Admin/Question_List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						res.data.data.forEach((item) => {
							if (item.show == 0) {
								item.show = false;
							} else if (item.show == 1) {
								item.show = true;
							}
							if (item.hlevel == 0) {
								item.hlevel = false;
							} else if (item.hlevel == 1) {
								item.hlevel = true;
							}
						});
						this.list = res.data.data;
						this.searchlist = this.list;
						this.refresh = !this.refresh;
					}
				);
			},
			Add: function() {
				this.$router.push({
					name: "AutoReturnAdd"
				});
			},
			Delete: function() {
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: "请选择要删除的数据",
						offset: 100,
						type: "warning",
					});
					return;
				}

				var idlist = [];
				_this.$refs.tablepadding.select.forEach((item) => {
					idlist.push(item.id);
				});

				_this
					.$confirm("确定要删除吗?", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
					.then(() => {
						_this.$request.post(
							"Api/Help_Admin/Delete", {
								token_admin: _this.$utils.getloc("token_admin"),
								userid_admin: _this.$utils.getloc("userid_admin"),
								delete_id: idlist.toString(),
							},
							(res) => {
								Notification({
									title: "操作成功",
									message: res.data.msg,
									offset: 100,
									type: "success",
								});
								_this.GetData();
							}
						);
					})
					.catch(() => {});
			},
			Update: function(id) {
				this.$router.push({
					name: "AutoReturnUpDate",
					query: {
						id: id
					}
				});
			},
		},
	};
</script>

<style scoped>
	.routerlink {
		color: #409eff;
		text-decoration: none;
	}
</style>
