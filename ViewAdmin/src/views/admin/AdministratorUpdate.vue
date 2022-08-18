<template>
	<div>
		<PageTitle title="修改" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="150px">
					<el-form-item label="账号">
						<el-input type="text" v-model="userid" disabled style="width: 220px;"></el-input>
					</el-form-item>
					<el-form-item label="密码">
						<el-input v-model="password" placeholder="不改请留空" style="width: 220px;"></el-input>
					</el-form-item>
					<el-form-item label="姓名">
						<el-input v-model="username" placeholder="请输入姓名" style="width: 220px;"></el-input>
					</el-form-item>
					<el-form-item label="职位" v-if="id != 1">
						<el-select v-model="permission">
							<el-option v-for="(item,index) in options" :key="index" :label="item.groupname"
								:value="item.id"></el-option>
						</el-select>
					</el-form-item>
					<el-form-item>
						<el-button icon="el-icon-edit" type="success" @click="update" size="mini">修改</el-button>
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
		Input,
		Notification
	} from "element-ui";
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Select);
	Vue.use(Input);
	Vue.prototype.$notify = Notification;
	export default {
		name: "AdministratorUpdate",
		components: {
			PageTitle
		},
		data() {
			return {
				id: "",
				userid: "",
				password: "",
				username: "",
				permission: "",
				options: []
			};
		},
		created() {
			this.id = this.$route.query.id;
			
			this.getoptions();
		},
		methods: {
			getdata: function() {
				this.$request.post("Api/SystemAdmin_Admin/Get", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					select_id: this.id
				}, (res) => {
					this.userid = res.data.data.userid;
					this.username = res.data.data.username;
					this.permission =JSON.parse( res.data.data.permission);
					console.log(res.data.data);
					
				});
			},
			getoptions: function() {
				this.$request.post("Api/SystemAdminGroup_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					select_id: this.id
				}, (res) => {
					this.options = res.data.data;
					console.log(this.options);
					this.getdata();
				});
			},
			update: function() {
				console.log(this.permission);
				if (this.username == '') {
					this.$notify({
						title: '提示',
						message: '请输入姓名',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				if (this.userid != 'admin' && this.permission == '') {
					this.$notify({
						title: '提示',
						message: '请选择职位',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				this.$confirm('确定要修改吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					this.$request.post("Api/SystemAdmin_Admin/Update", {
						token_admin: this.$utils.getloc('token_admin'),
						userid_admin: this.$utils.getloc('userid_admin'),
						up_id: this.id,
						username: this.username,
						password: this.password,
						permission: this.permission
					}, (res) => {
						this.$notify({
							title: '提示',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
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
