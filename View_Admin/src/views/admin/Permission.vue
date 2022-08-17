<template>
	<div>
		<PageTitle title="权限管理" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-select v-model="gid" @change="selectChanged" placeholder="请选择职位" style="margin-right: 10px; ">
					<el-option v-for="(item,index) in grouplist" :key="index" :label="item.groupname" :value="item.id"></el-option>
				</el-select>
				<el-button type="primary" size="mini" style="margin-right: 10px; margin-top: 10px;" icon="el-icon-edit" @click="update">修改</el-button>
				<el-button size="mini" type="success" style="margin-right: 10px; margin-top: 10px;" icon="el-icon-circle-plus-outline"
				 @click="addname">添加</el-button>
				<el-button type="warning" size="mini" style="margin-right: 10px; margin-top: 10px;" icon="el-icon-refresh" @click="getDefaultPermission">重置</el-button>
				<el-button size="mini" type="danger" style="margin-right: 10px; margin-top: 10px;" icon="el-icon-delete" @click="del">删除</el-button>
			</div>
		</div>
		<div class="cardbox" v-if="permission">
			<div class="cardcontent">
				<el-tabs tab-position="left">
					<el-tab-pane :label="item.Name" v-for="(item,index) in permission" v-bind:key="index">
						<el-row>
							<el-col :span="24">
								<el-switch v-model="item.Display" :active-text="item.Name" @change="changeSort(item.Name)"></el-switch>
							</el-col>
						</el-row>
						<el-row v-for="(item2,index2) in item.Pclist" v-bind:key="index2">
							<el-col :span="24">
								<el-switch v-model="item3.Display" :active-text="item3.Name" v-for="(item3,index3) in item2" v-bind:key="index3"></el-switch>
							</el-col>
						</el-row>
					</el-tab-pane>
				</el-tabs>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import PageTitle from "../../components/PageTitle";
	import {
		Tabs,
		TabPane,
		Switch,
		Select,
		Option,
		Loading,
		Notification,
		MessageBox
	} from "element-ui";
	Vue.use(Tabs);
	Vue.use(TabPane);
	Vue.use(Switch);
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Loading);
	Vue.prototype.$prompt = MessageBox.prompt;
	Vue.prototype.$notify = Notification;
	export default {
		name: "Permission",
		components: {
			PageTitle
		},
		data() {
			return {
				grouplist: [],
				gid: '',
				permission: '',
				name: ''
			};
		},
		created() {
			this.getGroupList();
			this.getDefaultPermission();
		},
		methods: {
			getGroupList: function() {          // 查询所有职位
				this.$request.post("Api/SystemAdminGroup_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.grouplist = res.data.data;
				});
			},
			addname: function() {               // 添加职位
				this.$prompt('请输入职位名称', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消'
				}).then(({
					value
				}) => {
					if (value == "" || value == null) {
						this.$notify({
							title: '提示',
							message: "职位名称不能为空",
							offset: 100,
							type: 'warning'
						});
					} else {
						this.$request.post("Api/SystemAdminGroup_Admin/Add", {
							token_admin: this.$utils.getloc('token_admin'),
							userid_admin: this.$utils.getloc('userid_admin'),
							add_groupname: value
						}, (res) => {
							this.$notify({
								title: '提示',
								message: res.data.msg,
								offset: 100,
								type: 'success'
							});

							this.getGroupList();
							this.getDefaultPermission();
						});
					}
				});
			},
			del: function() {                   // 删除职位
				if (this.gid == '') {
					this.$notify({
						title: '提示',
						message: "请选择要删除的职位",
						offset: 100,
						type: 'warning'
					});
				} else {
					this.$confirm('确定要删除吗?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						type: 'warning'
					}).then(() => {
						this.$request.post("Api/SystemAdminGroup_Admin/Delete", {
							token_admin: this.$utils.getloc('token_admin'),
							userid_admin: this.$utils.getloc('userid_admin'),
							delete_id: this.gid
						}, (res) => {
							this.$notify({
								title: '提示',
								message: res.data.msg,
								offset: 100,
								type: 'success'
							});
							this.gid = '';
							this.getGroupList();
							this.getDefaultPermission();
						});
					});
				}
			},
			getDefaultPermission: function() {  // 查询所有权限
				this.$request.post("Api/SystemAdminGroup_Admin/GetPermissionMod", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.permission = JSON.parse(res.data.data.pmlist);
				});
			},
			update: function() {                // 修改职位
				if (this.gid != "") {
					this.$prompt('确定要修改权限吗?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						inputPlaceholder: '如需修改职位名称,请输入新的名称,不修改则不填'
					}).then(({
						value
					}) => {
						if (value == "" || value == null) {
							value = " ";
						}
						this.$request.post("Api/SystemAdminGroup_Admin/Update", {
							token_admin: this.$utils.getloc('token_admin'),
							userid_admin: this.$utils.getloc('userid_admin'),
							id: this.gid,
							update_groupname: value,
							permission: JSON.stringify(this.permission)
						}, (res) => {
							Notification({
								title: '操作成功',
								message: res.data.msg,
								offset: 100,
								type: 'success'
							});
							this.getGroupList();
						});
					});
				} else {
					Notification({
						message: "请选择要修改的职位",
						offset: 100,
						type: 'warning'
					});
				}
			},
			selectChanged: function() {  // 选择职位
				this.grouplist.forEach((item, index) => {
					if (this.gid == item.id) {
						if (item.permission) {
							this.permission = JSON.parse(item.permission);
						}
					}
				});
			},
			changeSort: function(sort) {
				this.permission.forEach((item, index) => {
					if (item.Name == sort) {
						item.Pclist.forEach((item2, index2) => {
							item2.forEach((item3, index3) => {
								item3.Display = item.Display;
							})
						})
					}
				});
			}
		}
	};
</script>

<style scoped>
	.el-switch {
		margin: 10px;
	}
</style>
