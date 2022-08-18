<template>
	<div class="zhuanzhang">
		<PageTitle title="转账申请"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="转账申请" :list="list" :key="refresh" :ischeckbox="true" :isexcel="true"  :summary="true"
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>
					<el-table-column prop="zdate" label-class-name="zdate" label="日期" width="200"></el-table-column>
					<el-table-column prop="userid" label-class-name="userid" label="转款人"></el-table-column>
					<el-table-column prop="username" label-class-name="username" label="姓名"></el-table-column>
					<el-table-column prop="zqjine" label="转账前金额" width="100"></el-table-column>
					<el-table-column prop="zhjine" label="转账后金额" width="100"></el-table-column>
					<el-table-column prop="jine" label="金额"></el-table-column>
					<el-table-column prop="coinname" label-class-name="coinname" label="转账类型"></el-table-column>
					<el-table-column prop="suserid" label-class-name="suserid" label="收款人"></el-table-column>
					<el-table-column prop="susername" label-class-name="susername" label="姓名"></el-table-column>
					<el-table-column prop="szqjine" label="转账前金额" width="100"></el-table-column>
					<el-table-column prop="szhjine" label="转账后金额" width="100"></el-table-column>
					<el-table-column prop="bz" label="备注" width="200"></el-table-column>
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
		name: 'zhuanzhang',
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
			getdata: function() { // 查询转账记录
				this.$request.post("Api/WalletsZhuanzhang_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				}, true);
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
					_this.$request.post("Api/WalletsZhuanzhang_Admin/Delete", {
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
	.zhuanzhang {
		
	}
</style>
