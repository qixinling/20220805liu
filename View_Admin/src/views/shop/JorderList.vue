<template>
	<div>
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="true" :isexcel="true" :summary="false"
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button icon="el-icon-check" type="primary" size="mini" @click="Fahuo">发货</el-button>
						<el-button icon="el-icon-bank-card" type="warning" size="mini" @click="Tuikuan">退款</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>

					<el-table-column prop="odate" label-class-name="odate" label="时间" width="200"></el-table-column>
					<el-table-column prop="orderNo" label-class-name="orderNo" label="订单编号" width="300" fixed></el-table-column>
					<el-table-column prop="userid" label-class-name="userid" label="会员编号" width="200" fixed></el-table-column>
					<el-table-column prop="username" label-class-name="username" label="姓名" width="100"></el-table-column>
					<el-table-column label="商品列表" width="400">
						<template slot-scope="scope">
							<div v-for="(item, index) in scope.row.goodslist" :key="index">
								{{item.Goodsname}} × {{item.Num}} {{huanhang}}
							</div>
						</template>
					</el-table-column>
					<el-table-column prop="goodsnum" label="数量" show-overflow-tooltip></el-table-column>
					<el-table-column prop="sjine" label="金额" show-overflow-tooltip></el-table-column>
					<!-- <el-table-column label="金额">
						<template slot-scope="scope">
							<div v-for="(item, index) in scope.row.bill.dbBillAmount" :key="index">
								<div>{{item.amount}}</div>
							</div>
						</template>
					</el-table-column> -->
					
					<el-table-column prop="usertel" label-class-name="usertel" label="手机号" width="200"></el-table-column>
					<el-table-column prop="sheng" label-class-name="sheng" label="地址" width="300"></el-table-column>
					<el-table-column prop="ordername" label-class-name="ordername" label="状态" show-overflow-tooltip>
						<template slot-scope="scope">
							<div v-if="scope.row.orderstate==1" style="color: red;">待发货</div>
							<div v-else-if="scope.row.orderstate==2" style="color: green;">待收货</div>
							<div v-else-if="scope.row.orderstate==3" style="color: #E6A23C;">已收货</div>
							<div v-else-if="scope.row.orderstate==4" style="color: blue;">已退款</div>
						</template>
					</el-table-column>
					<el-table-column label="快递信息" width="250">
						<template slot-scope="scope">
							<div>名称: {{scope.row.kuaidiname}} </div>
							<div>单号: {{scope.row.kuaidiNo}} </div>
							<el-button type="buttom" size="mini" icon="el-icon-edit"
								@click="kdupdate(scope.row.id,scope.row.kuaidiname,scope.row.kuaidiNo)">修改
							</el-button>
						</template>
					</el-table-column>
					
					<el-table-column label="操作" width="100">
						<template slot-scope="scope">
							<router-link :to="'OrderChild?id=' + scope.row.id" class="routerlink">详情</router-link>
						</template>
					</el-table-column>
				</TablePadding>
				<template>
					<el-dialog title="快递信息" :visible.sync="dialogFormVisible">
						<el-form>
							<el-form-item label="快递名" label-width="70px">
								<el-input v-model="kuaidiname" autocomplete="off"></el-input>
							</el-form-item>
							<el-form-item label="快递编号" label-width="70px">
								<el-input v-model="kuaidiNo" autocomplete="off"></el-input>
							</el-form-item>
						</el-form>
						<div slot="footer" class="dialog-footer">
							<el-button @click="dialogFormVisible = false">取 消</el-button>
							<el-button type="primary" @click="update(kuaidiname,kuaidiNo)">确 定</el-button>
						</div>
					</el-dialog>

				</template>
			</div>
		</div>

	</div>

</template>

<script>
	import TablePadding from '../../components/TablePadding';
	import PageTitle from '../../components/PageTitle';
	import Vue from 'vue';
	import {
		Input,
		Notification,
		DatePicker,
		Select,
		Form,
		FormItem,
		Dialog
	} from 'element-ui';
	Vue.use(Dialog);
	Vue.use(Input);
	Vue.use(DatePicker);
	Vue.use(Select);
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				title: "激活订单",
				huanhang: '\n',
				id: '',
				kuaidiname: '',
				kuaidiNo: '',
				emdata: "",
				emd: false,
				dialogFormVisible: false,
				refresh: false,
				list: [],
				options: [{
					value: '99',
					label: '全部订单'
				}, {
					value: '1',
					label: '待发货'
				}, {
					value: '2',
					label: '待收货'
				},
				{
					value: '3',
					label: '已收货'
				}, {
					value: '4',
					label: '已退款'
				}
				],
				delids: []
			}
		},
		created() {
			this.getdata();
		},
		methods: {
			kdupdate: function (id, kuaidiname, kuaidiNo) {
				this.id = id;
				this.kuaidiNo = kuaidiNo;
				this.kuaidiname = kuaidiname;
				this.dialogFormVisible = true;
			},
			zhankai: function () {
				if (this.emd == false) {
					this.emd = true;
				} else {
					this.emd = false;
				}
				this.refresh = !this.refresh;
			},
			getdata: function () {
				this.$request.post("Api/ShopOrder_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					lx:3
				}, (res) => {
					this.list = res.data.data;
					this.list.forEach(item => {
						item.sheng = item.shi + item.xian + item.useraddress;
						item.goodslist = JSON.parse(item.goodslist);
					})
					this.refresh = !this.refresh;
				});
			},
			update: function (kuaidiname, kuaidiNo) {
				this.$request.post("Api/ShopOrder_Admin/Update", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					kuaidiNo: kuaidiNo,
					kuaidiname: kuaidiname,
					beizhu: '-',
					up_id: this.id
				}, (res) => {
					this.$notify({
						title: '提示',
						message: res.data.msg,
						offset: 100,
						type: 'success'
					});
					this.dialogFormVisible=false;
					this.getdata();
				});
			},

			Delete: function () {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: '提示',
						message: '请选择删除的数据',
						offset: 100,
						type: 'warning'
					});
				} else {
					this.$confirm('确定要删除吗?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						type: 'warning'
					}).then(() => {
						this.$refs.tablepadding.select.forEach(item => {
							this.delids.push(item.id);
						})
						this.$request.post("Api/ShopOrder_Admin/Delete", {
							token_admin: this.$utils.getloc('token_admin'),
							userid_admin: this.$utils.getloc('userid_admin'),
							delete_id: this.delids.toString()
						}, (res) => {
							this.$notify({
								title: '提示',
								message: res.data.msg,
								offset: 100,
								type: 'success'
							});
							this.getdata();
						});
					}).catch(() => {

					});
				}

			},
			Fahuo: function () {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: '提示',
						message: '请选择要发货的数据',
						offset: 100,
						type: 'warning'
					});
				} else {
					this.$confirm('确定要发货吗?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						type: 'warning'
					}).then(() => {
						this.$refs.tablepadding.select.forEach(item => {
							this.delids.push(item.id);
						})
						this.$request.post("Api/ShopOrder_Admin/Pass", {
							token_admin: this.$utils.getloc('token_admin'),
							userid_admin: this.$utils.getloc('userid_admin'),
							ids: this.delids.toString()
						}, (res) => {
							this.$notify({
								title: '提示',
								message: res.data.msg,
								offset: 100,
								type: 'success'
							});
							this.getdata();
						});
					}).catch(() => {

					});
				}
			},
			Tuikuan: function () {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: '提示',
						message: '请选择要退款的数据',
						offset: 100,
						type: 'warning'
					});
				} else {
					this.$confirm('确定要退款吗?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						type: 'warning'
					}).then(() => {
						this.$refs.tablepadding.select.forEach(item => {
							this.delids.push(item.id);
						})
						this.$request.post("Api/ShopOrder_Admin/Revoke", {
							token_admin: this.$utils.getloc('token_admin'),
							userid_admin: this.$utils.getloc('userid_admin'),
							ids: this.delids.toString()
						}, (res) => {
							this.$notify({
								title: '提示',
								message: res.data.msg,
								offset: 100,
								type: 'success'
							});
							this.getdata();
						});
					}).catch(() => {

					});
				}
			},
		}
	}
</script>

<style scoped="scoped">
	.btn {
		color: white;
		text-decoration: none;
	}

	.routerlink {
		color: #409EFF;
		text-decoration: none;
	}

	.demo-table-expand {
		font-size: 10px;
	}

	.demo-table-expand label {
		width: 90px;
		color: #99a9bf;
	}

	.demo-table-expand .el-form-item {
		margin-right: 0;
		margin-bottom: 0;
		width: 100%;
	}
</style>