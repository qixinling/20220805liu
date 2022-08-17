<template>
	<div>
		<PageTitle title="添加" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="150px">
					<el-form-item label="账号">
						<el-input type="text" placeholder="请输入账号" v-model="userid" style="width: 220px;"></el-input>
					</el-form-item>
					<el-form-item label="密码">
						<el-input v-model="password" placeholder="请输入密码" type="password" show-password style="width: 220px;"></el-input>
					</el-form-item>
					<el-form-item label="姓名">
						<el-input type="text" placeholder="请输入姓名" v-model="username" style="width: 220px;"></el-input>
					</el-form-item>
					<el-form-item label="职位">
						<el-select v-model="permission">
							<el-option v-for="(item,index) in options" :key="index" :label="item.groupname" :value="item.id"></el-option>
						</el-select>
					</el-form-item>
					<el-form-item>
						<el-button icon="el-icon-circle-plus-outline" type="success" @click="add" size="mini">添加</el-button>
						<el-button icon="el-icon-back" @click="back" size="mini">返回</el-button>
					</el-form-item>
				</el-form>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import PageTitle from "../../components/PageTitle";
	import {
		Form,
		FormItem,
		Select,
		Notification,
		Input
	} from "element-ui";
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Select);
	Vue.use(Input);
	Vue.prototype.$notify = Notification;
	export default {
		name: "AdministratorAdd",
		components: {
			PageTitle
		},
		data() {
			return {
				userid: "",
				password: "",
				username: "",
				permission: "",
				options: []
			};
		},
		created() {
			this.getoptions()
		},
		methods: {
			getoptions: function() {
				this.$request.post("Api/SystemAdminGroup_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.options = res.data.data;
					if (this.options.length > 0) {
						this.permission = this.options[0].id;
					}
				});
			},
			add: function() {
				if (this.userid == '') {
					this.$notify({
						title: '提示',
						message: '请输入账号',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				if (this.password == '') {
					this.$notify({
						title: '提示',
						message: '请输入密码',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				if (this.username == '') {
					this.$notify({
						title: '提示',
						message: '请输入姓名',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				if (this.permission == '') {
					this.$notify({
						title: '提示',
						message: '请选择职位',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				
				this.$confirm('确定要添加吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
				this.$request.post("Api/SystemAdmin_Admin/Add", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					add_userid: this.userid,
					add_username: this.username,
					add_password: this.password,
					permission: this.permission
				}, (res) => {
					this.$notify({
						title: '提示',
						message: res.data.msg,
						offset: 100,
						type: 'success'
					});
					
					this.userid = "";
					this.username = "";
					this.password = "";
					this.permission = this.options[0].groupname;
				});
				}).catch(() => {
				
				});
			},
			back: function() {
				this.$router.push({
					name: "AdministratorList"
				});
			}
		}
	};
</script>

<style scoped>
</style>
