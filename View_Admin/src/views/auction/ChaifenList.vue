<template>
	<div>
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false"
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button type="success" size="mini" icon="el-icon-circle-plus-outline" @click="btnClick(1)">拆单
						</el-button>
						<el-button type="danger" size="mini" @click="btnClick(2)">退回大厅
						</el-button>
					</div>
					<el-table-column prop="hdate" label-class-name="hdate" label="时间" />
					<el-table-column prop="holdno" label-class-name="holdno" label="编号" />
					<el-table-column prop="userid" label-class-name="userid" label="卖家用户名" />
					<el-table-column prop="usertel" label-class-name="usertel" label="卖家电话" />
					<el-table-column prop="jname" label-class-name="jname" label="产品名称" />
					<el-table-column prop="jprice" label="抢单金额" />
					<!-- <el-table-column prop="zhouqi" label="周期"  /> -->
					<el-table-column prop="buserid" label-class-name="buserid" label="买家用户名" />
					<el-table-column prop="busertel" label-class-name="busertel" label="买家电话" />
					<el-table-column prop="sitename" label-class-name="sitename" label="参与场次" />

					<!-- <el-table-column  prop="isbss" label="状态" label-class-name="isbss" width="100">
						<template slot-scope="scope">
							<span v-if="scope.row.isbss == 0" style="color: green;">正常</span>
							<span v-if="scope.row.isbss == 1" style="color: red;">已申诉</span>
						</template>
					</el-table-column> -->

				</TablePadding>

				<el-dialog title="拆单" :visible.sync="dialogFormVisible" width="400px">
					<el-form label-width="80px">
						<el-form-item label="拆单金额">
							<el-input v-model="price" style="width: 200px;" placeholder="直接填写拆单后的金额"></el-input>
						</el-form-item>
						<el-form-item label="打款金额">
							<el-input v-model="zshouyi" style="width: 200px;" placeholder="打款金额"></el-input>
						</el-form-item>
						<el-form-item label="数量">
							<el-input v-model="count" style="width: 200px;" placeholder="拆分数量"></el-input>
						</el-form-item>
					</el-form>
					<div slot="footer" class="dialog-footer">
						<el-button @click="dialogFormVisible = false">取 消</el-button>
						<el-button type="primary" @click="chaidan">确 定</el-button>
					</div>
				</el-dialog>
				
				<el-dialog title="退回大厅" :visible.sync="dialogFormVisible2" width="400px">
					<el-form label-width="100px">
						<el-form-item label="指定编号">
							<el-input v-model="zuserid" style="width: 200px;" placeholder="如不指定抢单人,请留空"></el-input>
						</el-form-item>
					</el-form>
					<div slot="footer" class="dialog-footer">
						<el-button @click="dialogFormVisible2 = false">取 消</el-button>
						<el-button type="primary" @click="dating()">确 定</el-button>
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
		Dialog,
		Form,
		FormItem
	} from "element-ui";
	Vue.use(Input);
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Switch);
	Vue.use(InputNumber);
	Vue.use(TimeSelect);
	Vue.use(Dialog);
	Vue.use(FormItem);
	Vue.use(Form);
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				title: "拆单列表",
				refresh: false,
				list: [],
				dialogFormVisible: false,
				dialogFormVisible2: false,
				count: '',
				price: '',
				zshouyi: '',
				zuserid: ''
			};
		},
		created() {
			this.getdata();
		},
		methods: {
			getdata: function() {
				this.$request.post(
					"Api/UsersHold_Admin/CaidanList", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						this.list = res.data.data;
						this.refresh = !this.refresh;
					}
				);
			},
			dating: function() {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: "提示",
						message: "请选择要操作的数据",
						offset: 100,
						type: "warning",
					});
					return;
				}
				var ids = [];
				this.$refs.tablepadding.select.forEach((item) => {
					ids.push(item.id);
				});
				this.$request.post(
					"Api/UsersHold_Admin/Dating", {
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
						if(res.data.code == 100){
							this.dialogFormVisible2 = false;
						}
						this.getdata();
					}
				);
			},
			btnClick: function(lx) {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: "提示",
						message: "请选择要拆单数据",
						offset: 100,
						type: "warning",
					});
				} else {
					if (lx == 1){
						this.dialogFormVisible = true;
					}else if(lx==2){
						this.dialogFormVisible2 = true;
					}
				}
			},
			chaidan: function() {
				if (this.price == '') {
					this.$notify({
						title: "提示",
						message: "请填写拆单金额",
						offset: 100,
						type: "warning",
					});
					return;
				}
				if (this.count == '') {
					this.$notify({
						title: "提示",
						message: "请填写拆单数量",
						offset: 100,
						type: "warning",
					});
					return;
				}
				var ids = [];
				this.$refs.tablepadding.select.forEach((item) => {
					ids.push(item.id);
				});
				this.$request.post("Api/UsersHold_Admin/Caidan", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					ids: "," + ids.toString() + ",",
					count: this.count,
					price: this.price,
					zshouyi: this.zshouyi
				}, (res) => {
					this.$notify({
						title: "提示",
						message: res.data.msg,
						offset: 100,
						type: "success",
					});
					this.dialogFormVisible = false;
					this.getdata();

				})
			},

			getimg: function(img) {
				return this.$config.img_url + img;
			},
			tolink: function(_path) {
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
