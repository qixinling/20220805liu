<template>
	<div>
		<PageTitle title="正式会员" ref="pageTitle"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="正式会员" :list="list" :key="refresh" :ischeckbox="true" :isexcel="true" :summary="false" ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button icon="el-icon-unlock" type="success" size="mini" @click="Jiesuo">解锁</el-button>
						<el-button icon="el-icon-lock" type="warning" size="mini" @click="Suoding">锁定</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>
					
					<el-table-column label-class-name="userid" label="会员编号" width="150" fixed>
						<template slot-scope="scope">
							<div @click="login(scope.row.userid)" style="color:#409EFF;">
								{{scope.row.userid}}
							</div>
						</template>
					</el-table-column>
					<!-- <el-table-column prop="username" label-class-name="username" label="姓名" width="100">
					</el-table-column> -->
					<el-table-column prop="usertel" label-class-name="usertel" label="手机号" width="200"></el-table-column>
					<el-table-column prop="rename" label-class-name="rename" label="推荐人" width="100"></el-table-column>
					<el-table-column label-class-name="pdt" label="注册时间" width="200">
						<template slot-scope="scope">{{ scope.row.rdt }}</template>
					</el-table-column>
					<el-table-column prop="ulevelname" label-class-name="ulevelname" label="级别" width="100"></el-table-column>
					<el-table-column prop="mystudioname" label-class-name="mystudioname" label="所属画室长" width="100"></el-table-column>
					
					<el-table-column prop="recount" label-class-name="recount" label="推荐人数" width="100"></el-table-column>
					<el-table-column prop="teamcount" label-class-name="teamcount" label="团队人数" width="100"></el-table-column>
					<el-table-column prop="teamyeji" label-class-name="teamyeji" label="团队业绩" width="100"></el-table-column>
					
					<el-table-column prop="islockname" label-class-name="islockname" label="状态" width="100"></el-table-column>
					
					<el-table-column label="资料">
						<template slot-scope="scope">
							<router-link :to="'UserEdit?id=' + scope.row.id" class="routerlink">查看</router-link>
						</template>
					</el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from 'vue';
	import config from '../../assets/js/config.js';
	import PageTitle from '../../components/PageTitle';
	import TablePadding from '../../components/TablePadding';
	import {
		Notification,
		Backtop
	} from 'element-ui';
	Vue.use(Backtop);
	Vue.prototype.$notify = Notification;
	export default {
		name: 'user',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				refresh:false,
				list: []
			}
		},
		created() {
			this.getdata();
		},
		methods: {
			login: function(userid) {
				var _this = this;
				_this.$request.post("Api/Users_Admin/Login", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin'),
					userid: userid
				}, (res) => {

					var token = res.data.data.token;
					var id = res.data.data.id;
					var recode = res.data.data.recode;
					var userid = res.data.data.userid;
					var username = res.data.data.username;
					var tx = res.data.data.tx;

					let _url = config.send_url + '?';
					_url += 'action=login';
					_url += '&t=' + token;
					_url += '&id=' + id;
					_url += '&recode=' + recode;
					_url += '&userid=' + userid;
					_url += '&username=' + username;
					_url += '&tx=' + tx;

					window.open(_url);
				});
			},
			Suoding: function() { // 锁定会员
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要操作的数据',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				var idlist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idlist.push(item.id);
				})

				_this.$confirm('确定要锁定吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Users_Admin/Suoding", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						s_id: idlist.toString()
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.getdata();
					});
				}).catch(() => {

				});
			},
			Jiesuo: function() { // 解锁会员
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要操作的数据',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				var idlist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idlist.push(item.id);
				})

				_this.$confirm('确定要解锁吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Users_Admin/Jiesuo", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						j_id: idlist.toString()
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.getdata();
					});
				}).catch(() => {

				});
			},
			Kongdan: function() { // 空单会员
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要操作的数据',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				var idlist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idlist.push(item.id);
				})

				_this.$confirm('确定要操作吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Users_Admin/Kongdan", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						k_id: idlist.toString()
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.getdata();
					});
				}).catch(() => {

				});
			},
			Delete: function() {
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要删除的数据',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				var idlist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idlist.push(item.id);
				})

				_this.$confirm('确定要删除吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Users_Admin/Delete", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						delete_id: idlist.toString()
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.getdata();
					});
				}).catch(() => {

				});
			},
			getdata: function() { // 查询用户
				var _this = this;
				_this.$request.post("Api/Users_Admin/List", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin'),
					ispay: 1
				}, (res) => {
					this.list = res.data.data;
					//console.log(res.data.data);
					this.refresh = !this.refresh;
				});
			}
		}
	}
</script>

<style scoped="scoped">
	.routerlink {
		color: #409EFF;
		text-decoration: none;
	}
</style>
