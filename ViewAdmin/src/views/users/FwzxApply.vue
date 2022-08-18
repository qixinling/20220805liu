<template>
	<div class="fwzxapply">
		<PageTitle title="服务中心申请"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="服务中心申请" :list="list" :key="refresh" :ischeckbox="true" :isexcel="true"
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button icon="el-icon-check" type="success" size="mini" @click="Pass">审核</el-button>
						<el-button icon="el-icon-warning-outline" type="warning" size="mini" @click="Revoke">撤销
						</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>
					<el-table-column prop="fdate" label-class-name="fdate" label="日期" width="180"></el-table-column>
					<el-table-column prop="userid" label-class-name="userid" label="会员编号"></el-table-column>
					<el-table-column prop="bdlevelname" label-class-name="bdlevelname" label="级别"></el-table-column>
					<el-table-column prop="bdsheng" label-class-name="bdsheng" label="省"></el-table-column>
					<el-table-column prop="bdshi" label-class-name="bdshi" label="市"></el-table-column>
					<el-table-column prop="bdxian" label-class-name="bdxian" label="县"></el-table-column>
					<el-table-column prop="bdaddress" label="地址"></el-table-column>
					<el-table-column prop="bz" label="备注"></el-table-column>
					<el-table-column prop="ispay" label="状态">
						<template slot-scope="scope">
							<el-button type="text" size="small" :class="'zt'+scope.row.ispay">{{scope.row.ispayname}}
							</el-button>
						</template>
					</el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from 'vue';
	import PageTitle from '../../components/PageTitle';
	import TablePadding from '../../components/TablePadding';
	import {Notification} from 'element-ui';
	Vue.prototype.$notify = Notification;
	export default {
		name: 'fwzxapply',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				input: "",
				mrlx: "全部",
				list: [],
				refresh: false
			}
		},
		created() {
			this.getdata();
		},
		methods: {
			Pass: function() { // 审核申请
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要审核的数据',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				var idlist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idlist.push(item.id);
				})

				_this.$confirm('确定要通过审核吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/UsersFwzxApply_Admin/Pass", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						fapass_id: idlist.toString()
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
			Revoke: function() { // 撤销申请
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要撤销的数据',
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
					_this.$request.post("Api/UsersFwzxApply_Admin/Revoke", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						fapass_id: idlist.toString()
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
			Delete: function() { // 删除
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
					_this.$request.post("Api/UsersFwzxApply_Admin/Delete", {
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
			getdata: function() { // 查询服务中心申请记录
				this.$request.post("Api/UsersFwzxApply_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					lx: 0
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				});
			},
		}
	}
</script>

<style scoped="scoped">
	.fwzxapply {
		
	}

	.zt0 {
		color: red;
	}

	.zt1 {
		color: #409eff;
	}

	.zt2 {
		color: #E6A23C;
	}
</style>
