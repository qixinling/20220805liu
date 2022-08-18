<template>
	<div>
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				
				<el-row >
					
					<el-col :span="12" style="padding-right: 10px;">
						<TablePadding :title="title" :list="buylist" :key="refresh" :ischeckbox="true" :isexcel="false"
							ref="tablepadding">
							<!--操作按钮插槽-->
							<div slot="btn_solt">
								<el-button type="success" size="mini" icon="el-icon-check" @click="AutoMatch()">自动匹配</el-button>
								<el-button icon="el-icon-check" type="danger" size="mini" @click="ManualMatch()">手动匹配</el-button>
								<div style="padding-top: 10px;">
									<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">买方删除</el-button>
								</div>
							</div>
							
							<el-table-column prop="userid" label-class-name="userid" label="买方用户名"  />
							<el-table-column prop="username" label-class-name="username" label="姓名"  />
							<el-table-column prop="usertel" label-class-name="usertel" label="电话"  />
							<el-table-column prop="cname" label-class-name="cname" label="宠物名"  />
							<el-table-column prop="cjindan" label-class-name="cjindan" label="金丹"  />
							

						</TablePadding>
					</el-col>
					<el-col :span="12" style="padding-left: 10px;">
						<TablePadding :title="title" :list="selllist" :key="refresh2" :ischeckbox="true" :isexcel="false"
							ref="tablepadding2">
							<!--操作按钮插槽-->
							<div slot="btn_solt" style="padding-top: 37px;">
								<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete2">卖方删除</el-button>
							</div>
							<el-table-column prop="userid" label-class-name="userid" label="卖方用户名"  />
							<el-table-column prop="username" label-class-name="username" label="姓名"  />
							<el-table-column prop="usertel" label-class-name="usertel" label="电话"  />
							<el-table-column prop="cname" label-class-name="cname" label="宠物名"  />
							<el-table-column prop="cprice" label-class-name="cprice" label="当前价值"  />
						
						</TablePadding>
					</el-col>
				</el-row>
				
			</div>
		</div>
	</div>
</template>
<script>
	import Vue from "vue";
	import PageTitle from "../../components/PageTitle";
	import TablePadding from '../../components/TablePadding';
	import {
		Input,
		Select,
		Option,
		Notification,
		Switch,
		InputNumber,
		TimeSelect
	} from "element-ui";
	Vue.use(Input);
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Switch);
	Vue.use(InputNumber);
	Vue.use(TimeSelect );
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				title:"买卖列表",
				refresh: false,
				refresh2: false,
				buylist: [],
				selllist: [],
			};
		},
		created() {
			this.getdata();
			this.getdata2();
		},
		methods: {
			//获取数据
			getdata: function () {
				this.$request.post(
					"Api/UsersYuyue_Admin/BuyList",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						lx:1
					},
					(res) => {
						this.buylist = res.data.data;
						//console.log(this.buylist);
						this.refresh = !this.refresh;
					}
				);
			},
			//获取数据
			getdata2: function () {
				this.$request.post(
					"Api/UsersHold_Admin/SellList",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						lx:1
					},
					(res) => {
						//console.log(res.data);
						this.selllist = res.data.data;
						//console.log(this.selllist);
						this.refresh2 = !this.refresh2;
					}
				);
			},
			AutoMatch: function () {
				//console.log(this.$refs.tablepadding.select);
				//console.log(this.$refs.tablepadding2.select);
				
					this.$confirm("确定要自动匹配吗?", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
						.then(() => {
							this.$request.post(
								"Api/UsersHold_Admin/AutoMatch",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
								},
								(res) => {
									this.$notify({
										title: "提示",
										message: res.data.msg,
										offset: 100,
										type: "success",
									});
									this.getdata();
									this.getdata2();
								}
							);
						})
						.catch(() => { });
			},
			ManualMatch: function () {
				//console.log(this.$refs.tablepadding.select);
				//console.log(this.$refs.tablepadding2.select);
				if (this.$refs.tablepadding.select.length == 0 || this.$refs.tablepadding2.select.length == 0) {
					this.$notify({
						title: "提示",
						message: "请选择匹配的数据",
						offset: 100,
						type: "warning",
					});
				} else {
					this.$confirm("确定要匹配吗?", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
						.then(() => {
							var buyids =[];
							this.$refs.tablepadding.select.forEach((item) => {
								buyids.push(item.id);
							});
							var sellids =[];
							this.$refs.tablepadding2.select.forEach((item) => {
								sellids.push(item.id);
							});
							//console.log("," +sellids.toString());
							this.$request.post(
								"Api/UsersHold_Admin/ManualMatch",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									buyids: "," +buyids.toString() +",",
									sellids: "," +sellids.toString()+",",
								},
								(res) => {
									this.$notify({
										title: "提示",
										message: res.data.msg,
										offset: 100,
										type: "success",
									});
									this.getdata();
									this.getdata2();
								}
							);
						})
						.catch(() => { });
				}
			},
			Delete: function () {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: "提示",
						message: "请选择删除的数据",
						offset: 100,
						type: "warning",
					});
				} else {
					this.$confirm("确定要删除吗?", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
						.then(() => {
							var ids =[];
							this.$refs.tablepadding.select.forEach((item) => {
								ids.push(item.id);
							});
							this.$request.post(
								"Api/UsersYuyue_Admin/Delete",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									ids: "," +ids.toString() + ",",
								},
								(res) => {
									this.$notify({
										title: "提示",
										message: res.data.msg,
										offset: 100,
										type: "success",
									});
									this.getdata();
								}
							);
						})
						.catch(() => { });
				}
			},
			Delete2: function () {
				if (this.$refs.tablepadding2.select.length == 0) {
					this.$notify({
						title: "提示",
						message: "请选择删除的数据",
						offset: 100,
						type: "warning",
					});
				} else {
					this.$confirm("确定要删除吗?", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
						.then(() => {
							var ids =[];
							this.$refs.tablepadding2.select.forEach((item) => {
								ids.push(item.id);
							});
							this.$request.post(
								"Api/UsersHold_Admin/Delete",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									ids: "," +ids.toString() + ",",
								},
								(res) => {
									this.$notify({
										title: "提示",
										message: res.data.msg,
										offset: 100,
										type: "success",
									});
									this.getdata2();
								}
							);
						})
						.catch(() => { });
				}
			},
			getimg: function (img) {
				return this.$config.img_url + img;
			},
			tolink: function (_path) {
				this.$router.push({
					path: _path,
				});
			},
		},
	};
</script>

<style scoped="scoped">
	.routerlink {
		color: #409eff;
		text-decoration: none;
	}

	/deep/ .cell {
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
	}
</style>