<template>
	<div class="jihuo_record">
		<PageTitle title="激活记录"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="激活记录" :list="list" :key="refresh" :ischeckbox="true" :isexcel="true"
					ref="tablepadding">
					<div slot="btn_solt">
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>
					<el-table-column prop="jdate" label-class-name="jdate" label="激活时间" width="180"></el-table-column>
					<el-table-column prop="userid" label-class-name="userid" label="操作人用户名"></el-table-column>
					<el-table-column prop="username" label-class-name="username" label="操作人姓名"></el-table-column>
					<el-table-column prop="juserid" label-class-name="juserid" label="激活用户名"></el-table-column>
					<el-table-column prop="jusername" label-class-name="jusername" label="激活用户姓名"></el-table-column>
					<el-table-column prop="jine" label="激活金额"></el-table-column>
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
		name: 'jihuo_record',
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
			getdata: function() { // 查询用户
				this.$request.post("Api/UsersJihuoRecord_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					ispay: 0
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
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
					_this.$request.post("Api/UsersJihuoRecord_Admin/Delete", {
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
		}
	}
</script>

<style scoped="scoped">
	.jihuo_record {
		
	}

	.routerlink {
		color: #409EFF;
		text-decoration: none;
	}
</style>
