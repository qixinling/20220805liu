<template>
	<div>
		<PageTitle title="数据管理" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-input v-model="backup_name" placeholder="手动备份名称,可不填写" style="width: 300px;padding-right: 20px;"></el-input>
				<el-row>
					<el-col :md="24" :sm="24" :span="24" style="margin-top: 10px;">
						<el-button icon="el-icon-document-copy" type="primary" @click="backup" size="mini">手动备份</el-button>
						<el-button icon="el-icon-folder-opened" type="danger" @click="delAll" size="mini">清空所有数据</el-button>
					</el-col>
					<el-col :md="24" :sm="24" :span="24" style="margin-top: 10px;font-size: 12px;color: #777;">
						已开启自动备份,凌晨1点至2点会进行备份,备份文件保留7天
					</el-col>
				</el-row>
				<TablePadding :list="list" :key="refresh" :ischeckbox="false" ref="tablepadding">
					<el-table-column prop="name" label="名称"></el-table-column>
					<el-table-column prop="date" label="时间"></el-table-column>
					<el-table-column prop="length" label="大小"></el-table-column>
					<el-table-column label="操作">
						<template slot-scope="scope">
							<el-button icon="el-icon-delete" type="danger" @click="del(scope.row.name)" size="mini">删除</el-button>
							<el-button icon="el-icon-document-checked" type="warning" @click="restore(scope.row.name)" size="mini">还原</el-button>
							<el-button icon="el-icon-download" type="success" @click="download(scope.row.name)" size="mini">下载</el-button>
						</template>
					</el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import TablePadding from "../components/TablePadding";
	import Vue from "vue";
	import PageTitle from "../components/PageTitle";
	import {
		Table,
		TableColumn,
		Input,
		MessageBox,
		Notification,
	} from "element-ui";
	Vue.use(Input);
	Vue.use(Table);
	Vue.use(TableColumn);
	Vue.prototype.$notify = Notification;
	Vue.prototype.$confirm = MessageBox.confirm;
	export default {
		name: "Database",
		components: {
			PageTitle,
			TablePadding,
		},
		data() {
			return {
				backup_name: "",
				list: [],
				searchlist: [],
				refresh: false,
			};
		},
		created() {
			this.getdata();
		},
		methods: {
			getdata: function() {
				this.$request.post(
					"Api/DataBase_Admin/List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						//console.log(res.data);
						this.list = res.data.data;
						this.searchlist = this.list;
						this.refresh = !this.refresh;
					},
					true
				);
			},

			backup: function() {
				//备份
				this.$confirm('确定要备份吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
				var _this = this;
				var backupname = _this.backup_name;
				if (backupname == null || backupname.trim() == "") {
					var date = new Date();
					var month =
						date.getMonth() + 1 < 10 ?
						"0" + (date.getMonth() + 1) :
						date.getMonth() + 1;
					var strDate =
						date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
					var currentdate =
						date.getFullYear().toString() +
						month.toString() +
						strDate.toString() +
						date.getHours().toString() +
						date.getMinutes().toString() +
						date.getSeconds().toString();
					backupname = currentdate;
				}

				this.$request.post(
					"Api/DataBase_Admin/Backup", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						backup_name: backupname.toString(),
					},
					(res) => {
						this.$notify({
							title: "提示",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
						this.getdata();
					}
				);
				}).catch(() => {
				
				});
			},
			delAll: function() {
				//清空所有数据
				var _this = this;
				this.$confirm("确定要清空所有数据吗?清空前请做好数据备份", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
					.then(() => {
						this.$request.post(
							"Api/DataBase_Admin/Reset", {
								token_admin: this.$utils.getloc("token_admin"),
								userid_admin: this.$utils.getloc("userid_admin"),
							},
							(res) => {
								this.$notify({
									title: "提示",
									message: res.data.msg,
									offset: 100,
									type: "success",
								});
								this.getdata();
							}
						);
					})
					.catch(() => {});
			},
			restore: function(restorename) {
				//还原
				this.$confirm("确定要还原数据吗？请注意数据备份", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
					.then(() => {
						this.$request.post(
							"Api/DataBase_Admin/Restore", {
								token_admin: this.$utils.getloc("token_admin"),
								userid_admin: this.$utils.getloc("userid_admin"),
								restore_name: restorename.toString(),
							},
							(res) => {
								this.$notify({
									title: "提示",
									message: res.data.msg,
									offset: 100,
									type: "success",
								});
								this.getdata();
							}
						);
					})
					.catch(() => {});
			},
			del: function(backupname) {
				//删除
				this.$confirm("确定要删除备份吗？", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
					.then(() => {
						this.$request.post(
							"Api/DataBase_Admin/Delete", {
								token_admin: this.$utils.getloc("token_admin"),
								userid_admin: this.$utils.getloc("userid_admin"),
								del_name: backupname.toString(),
							},
							(res) => {
								this.$notify({
									title: "提示",
									message: res.data.msg,
									offset: 100,
									type: "success",
								});
								this.getdata();
							}
						);
					})
					.catch(() => {});
			},
			download: function(backupname) {
				//下载
				var uri = this.$config.send_url + "Api/DataBase_Admin/Download?";
				var token_admin = encodeURIComponent(this.$utils.getloc("token_admin"));
				uri += "token_admin=" + token_admin;
				uri += "&userid_admin=" + this.$utils.getloc("userid_admin");
				uri += "&download_name=" + backupname;
				//console.log(uri);
				window.open(uri, "_blank");
			},
		},
	};
</script>

<style scoped>
</style>
