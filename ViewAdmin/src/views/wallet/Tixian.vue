<template>
	<div class="tixian">
		<PageTitle title="提现申请"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="提现申请" :list="list" :key="refresh" :ischeckbox="true" :isexcel="true" :summary="true"
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button icon="el-icon-check" type="success" size="mini" @click="shenhe">审核通过</el-button>
						<el-button icon="el-icon-warning-outline" type="warning" size="mini" @click="chexiao">撤销
						</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>
					<el-table-column prop="tdate" label-class-name="tdate" label="日期" width="200"></el-table-column>
					<el-table-column prop="userid" label-class-name="userid" label="会员编号"></el-table-column>
					<el-table-column prop="username" label-class-name="username" label="姓名"></el-table-column>
					<el-table-column prop="usertel" label-class-name="usertel" label="手机号" width="160">
					</el-table-column>
					<el-table-column prop="coinname" label-class-name="coinname" label="提现类型"></el-table-column>
					<el-table-column prop="jine" label="金额"></el-table-column>
					<el-table-column prop="shouxu" label="手续费"></el-table-column>
					<el-table-column prop="bankname" label-class-name="bankname" label="账户"></el-table-column>
					<el-table-column prop="bankcard" label="账号" width="200"></el-table-column>
					<el-table-column label="收款码" width="120">
						<template slot-scope="scope">
							<el-popover placement="top" trigger="focus">
								<img :src="getimg(scope.row.bankimg)">
								<el-button slot="reference" size="mini">长按显示</el-button>
							</el-popover>
						</template>
					</el-table-column>
					<el-table-column prop="ispay" label="状态" label-class-name="ispay">
						<template slot-scope="scope">
							<el-button v-if="scope.row.ispay==0" type="text" size="small" style="color: red;">待审核
							</el-button>
							<el-button v-else-if="scope.row.ispay==1" type="text" size="small" style="color: green;">已审核
							</el-button>
							<el-button v-else-if="scope.row.ispay==2" type="text" size="small" style="color: #E6A23C;">
								已撤销</el-button>
						</template>
					</el-table-column>
					<el-table-column prop="bz" label="备注" width="200"></el-table-column>
					<el-table-column prop="chexiaoyuanyin" label="撤销原因" width="100"></el-table-column>
					<el-table-column></el-table-column>
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
		Notification,
		Popover
	} from 'element-ui';
	Vue.use(Popover);
	Vue.prototype.$notify = Notification;
	export default {
		name: 'tixian',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				lx: 99,
				list: [],
				refresh: false
			}
		},
		created() {
			this.getdata();
		},
		methods: {
			getdata: function() { // 查询提现记录
				this.$request.post("Api/WalletsTixian_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.searchlist = this.list;
					this.refresh = !this.refresh;
				}, true);
			},
			shenhe: function() { // 审核
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

				_this.$confirm('确定要审核吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {

					_this.$request.post("Api/WalletsTixian_Admin/Pass", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						txpass_id: idlist.toString()
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						})
						_this.getdata();
					});
				}).catch(() => {

				});
			},
			chexiao: function() { // 撤销
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

				_this.$prompt('确定要撤销吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning',
					inputPlaceholder: "可选输入撤销原因"
				}).then(({
					value
				}) => {
					let chexiaoyuanyin = value == null ? "-" : value;
					_this.$request.post("Api/WalletsTixian_Admin/Revoke", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						chexiao_id: idlist.toString(),
						chexiaoyuanyin: chexiaoyuanyin
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
					_this.$request.post("Api/WalletsTixian_Admin/Delete", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						delete_id: idlist.toString()
					}, (res) => {
						_this.getdata();
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
					});
				}).catch(() => {

				});
			},
			getimg: function(img) {
				return this.$config.img_url + img;
			},
		}
	}
</script>

<style scoped="scoped">
	.tixian {
		
	}
</style>
