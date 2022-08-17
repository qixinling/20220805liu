<template>
	<div class="fwzxjilu">
		<PageTitle title="服务中心列表"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="服务中心列表" :list="list" :key="refresh" :ischeckbox="true" :isexcel="true"
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button icon="el-icon-circle-close" type="danger" size="mini" @click="Revoke">撤销资格
						</el-button>
					</div>
					<el-table-column prop="bddate" label-class-name="bddate" label="日期" width="180"></el-table-column>
					<el-table-column prop="userid" label-class-name="userid" label="会员编号"></el-table-column>
					<el-table-column prop="username" label-class-name="bdlevelname" label="姓名"></el-table-column>
					<el-table-column prop="usertel" label-class-name="bdsheng" label="手机号"></el-table-column>
					<el-table-column label="地址">
						<template
							slot-scope="scope">{{ scope.row.bdsheng }}{{ scope.row.bdshi }}{{ scope.row.bdxian }}</template>
					</el-table-column>
					<el-table-column prop="bdaddress" label-class-name="bdshi" label="详细地址"></el-table-column>
					<el-table-column prop="bdlevelname" label-class-name="bdxian" label="级别"></el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from 'vue';
	import PageTitle from '../../components/PageTitle';
	import TablePadding from '../../components/TablePadding';
	import {
		Notification
	} from 'element-ui';
	Vue.prototype.$notify = Notification;
	export default {
		name: 'fwzxjilu',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				list: [],
				refresh: false
			}
		},
		created() {
			this.getdata();
		},
		methods: {
			Revoke: function() { // 撤销资格
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要撤销资格的会员',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				var idlist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idlist.push(item.id);
				})

				_this.$confirm('确定要撤销吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Users_Admin/RevokeFwzx", {
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
			getdata: function() { // 查询服务中心列表
				this.$request.post("Api/Users_Admin/ListFwzx", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				});
			},
		}
	}
</script>

<style scoped="scoped">
	.fwzxjilu {
		
	}
</style>
