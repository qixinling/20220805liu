<template>
	<div>
		<PageTitle title="大厅出售列表"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
						<TablePadding :title="title" :list="selllist" :key="refresh" :ischeckbox="true" :isexcel="false"
							ref="tablepadding">
							<!--操作按钮插槽-->
							<div slot="btn_solt" style="padding-top: 37px;">
								<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete2">删除</el-button>
								<!-- <el-button type="success" size="mini" @click="dialogFormVisible=true">批量修改场次</el-button> -->
							</div>
							<el-table-column prop="userid" label-class-name="userid" label="卖方"  />
							<!-- <el-table-column prop="zuserid" label-class-name="zuserid" label="指定买方"  /> -->
							<!-- <el-table-column prop="username" label-class-name="username" label="姓名"  /> -->
							<el-table-column prop="usertel" label-class-name="usertel" label="电话"  />
							<el-table-column prop="jname" label-class-name="jname" label="产品名"  />
							<el-table-column prop="jprice" label-class-name="jprice" label="竞拍价格"  />
							<el-table-column prop="sitename" label-class-name="sitename" label="画室长"  />
						</TablePadding>	
						<el-dialog title="修改场次" :visible.sync="dialogFormVisible" width="400px">
							<el-form label-width="100px">
								<el-form-item label="场次">
									<el-select v-model="sid" placeholder="请选择">
										<el-option  v-for="item in sitelist" :key="item.id" :label="item.name" :value="item.id" ></el-option>
									</el-select>
								</el-form-item>
							</el-form>
							<div slot="footer" class="dialog-footer">
								<el-button @click="dialogFormVisible2 = false">取 消</el-button>
								<el-button type="primary" @click="updatesite()">确 定</el-button>
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
				title:"买卖列表",
				refresh: false,
				selllist: [],
				dialogFormVisible:false,
				sitelist:[],
				sid:''
			};
		},
		created() {
			//this.getdata();
			this.getdata2();
			this.getsitedata();
		},
		methods: {
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
						this.sid = this.sitelist[0].id;
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
						lx:0
					},
					(res) => {
						//console.log(res.data);
						this.selllist = res.data.data;
						//console.log(this.selllist);
						this.refresh = !this.refresh;
					}
				);
			},
			updatesite: function () {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: "提示",
						message: "请选择修改的数据",
						offset: 100,
						type: "warning",
					});
				} else {
					this.$confirm("确定要修改吗?", "提示", {
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
								"Api/UsersHold_Admin/UpdateSite",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									ids: "," +ids.toString() + ",",
									sid:this.sid
								},
								(res) => {
									this.$notify({
										title: "提示",
										message: res.data.msg,
										offset: 100,
										type: "success",
									});
									this.dialogFormVisible=false;
									this.getdata2();
								}
							);
						})
						.catch(() => { });
				}
			},
			Delete2: function () {
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