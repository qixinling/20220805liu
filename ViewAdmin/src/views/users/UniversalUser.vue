<template>
	<div>
		<PageTitle title="画室长" ref="pageTitle"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="画室长" :list="list" :key="refresh" :ischeckbox="true" :isexcel="true" :summary="false" ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						
					</div>
					
					<el-table-column prop="userid" label-class-name="userid" label="会员编号"  fixed>
						<!-- <template slot-scope="scope">
							<div @click="login(scope.row.userid)" style="color:#409EFF;">
								{{scope.row.userid}}
							</div>
						</template> -->
					</el-table-column>
					<el-table-column prop="username" label-class-name="username" label="姓名" >
					</el-table-column>
					<el-table-column prop="usertel" label-class-name="usertel" label="手机号" ></el-table-column>

					<el-table-column prop="ulevelname" label-class-name="ulevelname" label="级别" ></el-table-column>
					<el-table-column prop="studioName" label-class-name="studioName" label="画室名称" ></el-table-column>
					<el-table-column prop="studioCard" label-class-name="studioCard" label="画室号" ></el-table-column>
					<!-- <el-table-column label="资料">
						<template slot-scope="scope">
							<router-link :to="'UserEdit?id=' + scope.row.id" class="routerlink">查看</router-link>
						</template>
					</el-table-column> -->
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
			
			getdata: function() { // 查询用户
				var _this = this;
				_this.$request.post("Api/Users_Admin/HsList", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin')
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
