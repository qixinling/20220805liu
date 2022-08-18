<template>
	<div class="coin">
		<PageTitle title="货币管理"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<div class="addHb">
					<el-dialog title="提示" :visible.sync="dialogFormVisible" width="30%" :modal-append-to-body="false">
						<el-input v-model="codename" placeholder="代号"></el-input>
						<el-input v-model="coinname" placeholder="名称" class="coinname"></el-input>
						<span slot="footer" class="dialog-footer">
							<el-button @click="dialogFormVisible = false">取 消</el-button>
							<el-button type="primary" @click="Add">确 定</el-button>
						</span>
					</el-dialog>
				</div>

				<TablePadding title="货币管理" :list="list" :key="refresh" :ischeckbox="true"  
					ref="tablepadding">
					<div slot="btn_solt">
						<el-button icon="el-icon-circle-plus" type="success" size="mini"
							@click="dialogFormVisible = true">添加货币</el-button>
						<!-- <el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button> -->
					</div>
					<el-table-column label="代号">
						<template slot-scope="scope">
							<el-input size="mini" v-model="scope.row.codename" :min="0"
								@change="ChangeCoinName(scope.row)" :controls="false"></el-input>
						</template>
					</el-table-column>
					<el-table-column label="名称">
						<template slot-scope="scope">
							<el-input size="mini" v-model="scope.row.coinname" :min="0"
								@change="ChangeCoinNameZh(scope.row)" :controls="false"></el-input>
						</template>
					</el-table-column>
					<el-table-column label="启用" prop="state">
						<template slot-scope="scope">
							<el-switch v-model="scope.row.state" @change="ChangeState(scope.row)" :inactive-value="0"
								:active-value="1"></el-switch>
						</template>
					</el-table-column>
				</TablePadding>
				<div style="padding-top: 15px;">
					<span style="font-size: 12px;color: red;">说明:该页面的数据请勿随便操作，如需操作可咨询技术人员</span>
				</div>

			</div>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import PageTitle from "../../components/PageTitle";
	import TablePadding from "../../components/TablePadding";
	import {
		Notification,
		MessageBox,
		Switch,
		Dialog
	} from "element-ui";

	Vue.use(Switch);
	Vue.use(Dialog);
	Vue.prototype.$prompt = MessageBox.prompt;
	Vue.prototype.$notify = Notification;
	export default {
		name: "coin",
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				refresh: false,
				list: [],
				dialogFormVisible: false,
				codename: '',
				coinname: ''
			};
		},
		created() {
			this.getdata();
		},
		methods: {
			getdata: function() { // 查询所有货币
				this.$request.post(
					"Api/WalletsCoin_Admin/List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin")
					},
					(res) => {
						this.list = res.data.data;
						this.refresh = !this.refresh;
					}
				);
			},
			ChangeState: function(row) { // 是否启用
				this.$request.post(
					"Api/WalletsCoin_Admin/ChangeState", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id: row.id
					},
					(res) => {
						this.$notify({
							title: "操作成功",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
						if (row.state == 0) {
							this.getdata();
						}
					},
					false,
					false
				);
			},
			ChangeCoinName: function(row) { // 修改英文名称
				if (row.codename == "") {
					this.$notify({
						title: "提示",
						message: "不能为空",
						offset: 100,
						type: "warning",
					});
					return;
				}
				this.$request.post(
					"Api/WalletsCoin_Admin/ChangeCoinName", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id: row.id,
						codename: row.codename
					},
					(res) => {
						this.$notify({
							title: "操作成功",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
					},
					false,
					false
				);
			},
			ChangeCoinNameZh: function(row) { // 修改中文名称
				if (row.coinname == "") {
					this.$notify({
						title: "提示",
						message: "不能为空",
						offset: 100,
						type: "warning",
					});
					return;
				}
				this.$request.post(
					"Api/WalletsCoin_Admin/ChangeCoinNameZh", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id: row.id,
						coinname: row.coinname
					},
					(res) => {
						this.$notify({
							title: "操作成功",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
						this.getdata();
					},
					false,
					false
				);
			},
			Delete: function() { // 删除货币
				// 删除
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
							"Api/WalletsCoin_Admin/Delete", {
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
								_this.getdata();
							},
							false,
							false
						);
					})
					.catch(() => {});
			},
			Add: function() { // 添加货币
				if (this.codename == "") {
					this.$notify({
						title: '提示',
						message: "请输入英文名称",
						offset: 100,
						type: 'warning'
					});
					return;
				}
				if (this.coinname == "") {
					this.$notify({
						title: '提示',
						message: "请输入中文名称",
						offset: 100,
						type: 'warning'
					});
					return;
				}

				this.$request.post("Api/WalletsCoin_Admin/Add", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					codename: this.codename,
					coinname: this.coinname
				}, (res) => {
					this.codename = '';
					this.coinname = '';
					this.dialogFormVisible = false;
					this.$notify({
						title: '操作成功',
						message: res.data.msg,
						offset: 100,
						type: 'success'
					});
					this.getdata();
				});
			},

		},
	};
</script>

<style scoped="scoped">
	.coinname {
		padding-top: 20px;
	}

	.delete_btn {
		margin-left: 20px;
	}

	.zhbox {
		float: left;
		width: 120px;
		padding-left: 10px;
	}

	.zhbox span {
		padding-left: 5px;
	}
</style>
