<template>
	<div>
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false" 
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
						<!-- <el-button type="success" size="mini" icon="el-icon-circle-plus-outline"
							@click="dialogFormVisible2=true">撤销买家</el-button> -->
					</div>
					<el-table-column prop="hdate" label-class-name="hdate" label="时间"  />
					<el-table-column prop="holdno" label-class-name="holdno" label="编号"  />
					<el-table-column prop="userid" label-class-name="userid" label="卖家用户名"  />
					<el-table-column prop="username" label-class-name="username" label="卖家姓名"  />
					<el-table-column prop="usertel" label-class-name="usertel" label="卖家电话"  />
					<el-table-column prop="jname" label-class-name="jname" label="产品名称"  />
					<el-table-column prop="jprice"  label="交易金额"  />
					<!-- <el-table-column prop="zhouqi" label="周期"  /> -->
					<el-table-column prop="buserid" label-class-name="buserid" label="买家用户名"  />
					<el-table-column prop="busername" label-class-name="busername" label="买家姓名"  />
					<el-table-column prop="busertel" label-class-name="busertel" label="买家电话"  />
					
					<!-- <el-table-column prop="sitename" label-class-name="sitename" label="场次">
						<template slot-scope="scope">
						<el-select v-model="scope.row.sid" placeholder="请选择"  @change="sitechange(scope.row.sid,scope.row.id)">
							<el-option  v-for="item in sitelist" :key="item.id" :label="item.name" :value="item.id" ></el-option>
						</el-select>
						</template>
					</el-table-column> -->
					<el-table-column label="支付凭证" width="100">
						<template slot-scope="scope">
							<a :href="getimg(scope.row.zhimg)">
								<img :src="getimg(scope.row.zhimg)" width="50px" height="50px" />
							</a>
						</template>
					</el-table-column>
					<el-table-column label="上架凭证" width="100">
						<template slot-scope="scope">
							<a :href="getimg(scope.row.sjimg)">
								<img :src="getimg(scope.row.sjimg)" width="50px" height="50px" />
							</a>
						</template>
					</el-table-column>
					<el-table-column  prop="state" label="状态"  width="100">
						<template slot-scope="scope">
							<span v-if="scope.row.state ==1" style="color: green;">等待付款</span>
							<span v-if="scope.row.state == 2" style="color: red;">等待收款</span>
							<span v-if="scope.row.state == 3" style="color: red;">待上架</span>
						</template>
					</el-table-column>
					
				</TablePadding>
				<el-dialog title="撤销买家" :visible.sync="dialogFormVisible2" width="400px">
					<el-form label-width="100px">
						<el-form-item label="指定编号">
							<el-input v-model="zuserid" style="width: 200px;" placeholder="如不指定抢单人,请留空"></el-input>
						</el-form-item>
					</el-form>
					<div slot="footer" class="dialog-footer">
						<el-button @click="dialogFormVisible2 = false">取 消</el-button>
						<el-button type="primary" @click="chexiao()">确 定</el-button>
					</div>
				</el-dialog>
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
		TimeSelect,
		Dialog
	} from "element-ui";
	Vue.use(Input);
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Switch);
	Vue.use(InputNumber);
	Vue.use(TimeSelect );
	Vue.use(Dialog);
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				title:"交易中列表",
				refresh: false,
				list: [],
				dialogFormVisible2:false,
				zuserid:'',
				sid:"",
				sitelist:[],
			};
		},
		created() {
			
			this.getdata();
		},
		methods: {
			sitechange(sid,hid){
				this.$request.post(
					"Api/UsersHold_Admin/UpdateSite",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						sid:sid,
						hid:hid
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
			},
			getsitedata: function () {
				this.$request.post(
					"Api/Site_Admin/List",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						var date = res.data.data;
						this.sitelist = date;
					}
				);
			},
			getdata: function () {
				this.$request.post(
					"Api/UsersHold_Admin/SellList",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						lx:1
					},
					(res) => {
						//console.log(res.data);
						this.list = res.data.data;
						//console.log(this.list);
			
						this.refresh = !this.refresh;
					}
				);
			},
			chexiao: function () {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: "提示",
						message: "请选择要撤销的数据",
						offset: 100,
						type: "warning",
					});
				} else {
					this.$confirm("确定要撤销吗?", "提示", {
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
								"Api/UsersHold_Admin/Revoke",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									ids: "," + ids.toString() + ",",
									zuserid: this.zuserid
								},
								(res) => {
									this.$notify({
										title: "提示",
										message: res.data.msg,
										offset: 100,
										type: "success",
									});
									this.dialogFormVisible2= false;
									this.getdata();
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
									this.getdata();
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