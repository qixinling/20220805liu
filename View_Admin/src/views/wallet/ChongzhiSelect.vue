<template>
	<div class="chongzhiselect">
		<PageTitle title="充值管理"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<div class="addHb">
					<el-dialog title="提示" :visible.sync="addVisible" width="30%" :modal-append-to-body="false">
						<el-row>
							<el-col :span="24" class="el_option">
								<el-select v-model="coinname" placeholder="请选择货币" @change="ChangeSelecthb">
									<el-option v-for="(item,index) in options" :key="index" :label="item.coinname" :value="item.cid"></el-option>
								</el-select>
							</el-col>
						</el-row>
								
						<el-row>
							<el-col :span="7">
								<span>最小充值金额</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinemin" @change="handleChangeJinemin" :min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>最大充值金额</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinemax" @change="handleChangeJinemax" :min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>充值倍数</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinebs" @change="handleChangeJinebs" :min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<span slot="footer" class="dialog-footer">
							<el-button @click="addVisible = false">取 消</el-button>
							<el-button type="primary" @click="Add">确 定</el-button>
						</span>
					</el-dialog>
					
				</div>
				<div class="updateHb">
					<el-dialog title="提示" :visible.sync="dialogFormVisible" width="30%" :modal-append-to-body="false">
						<el-row>
							<el-col :span="7">
								<span>最小充值金额</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinemin" @change="handleChangeJinemin" :min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>最大充值金额</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinemax" @change="handleChangeJinemax" :min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>充值倍数</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinebs" @change="handleChangeJinebs" :min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<span slot="footer" class="dialog-footer">
							<el-button @click="dialogFormVisible = false">取 消</el-button>
							<el-button type="primary" @click="Update">确 定</el-button>
						</span>
					</el-dialog>
				</div>
				
				<TablePadding title="充值管理" :list="list" :key="refresh" :ischeckbox="true" :summary="false"
					ref="tablepadding">
					<div slot="btn_solt">
						<el-button icon="el-icon-circle-plus" type="success" size="mini" @click="addVisible = true">添加充值货币</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete" class="delete_btn">删除</el-button>
					</div>
					<el-table-column prop="coinname" label="名称" width="130"></el-table-column>
					<el-table-column prop="jinemin" label="最小充值金额"></el-table-column>
					<el-table-column prop="jinemax" label="最大充值金额"></el-table-column>
					<el-table-column prop="jinebs" label="充值倍数"></el-table-column>

					<el-table-column label="操作">
						<template slot-scope="scope">
							<div class="updatetext" @click="dialogFormVisibleShow(scope.row.id)">修改</div>
						</template>
					</el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import PageTitle from "../../components/PageTitle";
	import TablePadding from "../../components/TablePadding";
	import {
		Notification,
		MessageBox,
		Dialog,
		InputNumber,
		Select,
		Option
	} from "element-ui";

	Vue.use(Dialog);
	Vue.use(InputNumber);
	Vue.use(Select);
	Vue.use(Option);
	Vue.prototype.$prompt = MessageBox.prompt;
	Vue.prototype.$notify = Notification;
	export default {
		name: "chongzhiselect",
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				index: 1,
				refresh: false,
				list: [],
				dialogFormVisible: false,
				addVisible: false,
				cid: 0,
				jinemax: 1,
				jinemin: 1,
				jinebs: 1,
				coinname: '',
				options: [],
			};
		},
		created() {
			this.getcoin();
			this.getdata();
		},
		methods: {
			ChangeSelecthb: function (value) {  // 选择货币
				this.cid = value;
			},
			handleChangeJinemin: function(value) { // 最小充值金额
				this.jinemin = value;
			},
			handleChangeJinemax: function(value) { // 最大充值金额
				this.jinemax = value;
			},
			handleChangeJinebs: function(value) { // 充值倍数
				this.jinebs = value;
			},
			dialogFormVisibleShow: function(id) { // 弹出弹框
				this.list.forEach(item => {
					if (item.id == id) {
						this.cid = id;
						this.jinemax = item.jinemax;
						this.jinemin = item.jinemin;
						this.jinebs = item.jinebs;
					}
				})
				this.dialogFormVisible = true;
			},
			Add: function() { // 添加
				if (this.cid <= 0) {
					this.$notify({
						title: '提示',
						message: '请选择货币',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				this.$request.post(
					"Api/WalletsChongzhi_Select_Admin/Add", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						cid: this.cid,
						jinemax: this.jinemax,
						jinemin: this.jinemin,
						jinebs: this.jinebs
					},
					(res) => {
						this.$notify({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						this.coinname = '';
						this.cid = 0;
						this.jinemax = 1;
						this.jinemin = 1;
						this.jinebs = 1;
						this.addVisible = false;
						this.getdata();
					}
				);
			},
			Update: function() { // 修改
				this.$request.post(
					"Api/WalletsChongzhi_Select_Admin/Update", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						cid: this.cid,
						jinemax: this.jinemax,
						jinemin: this.jinemin,
						jinebs: this.jinebs
					},
					(res) => {
						this.$notify({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						this.cid = 0;
						this.jinemax = 1;
						this.jinemin = 1;
						this.jinebs = 1;
						this.dialogFormVisible = false;
						this.getdata();
					}
				);
			},
			getcoin: function() { // 查询所有货币
				this.$request.post(
					"Api/WalletsCoin_Admin/List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin")
					},
					(res) => {
						var list = res.data.data;
						if (list.length > 0) {
							var olist = [];
							list.forEach(item => {
								var option = {
									cid: item.id,
									coinname: item.coinname
								}
								olist.push(option);
							})
							this.options = olist;
						}
					}
				);
			},
			getdata: function() { // 查询所有可充值货币
				this.$request.post(
					"Api/WalletsChongzhi_Select_Admin/List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin")
					},
					(res) => {
						this.list = res.data.data;
						this.refresh = !this.refresh;
					}
				);
			},
			Delete: function() {  // 删除货币
				// 删除
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: "请选择要删除的数据",
						offset: 100,
						type: "warning",
					});
					return;
				}
				var idlist = [];
				_this.$refs.tablepadding.select.forEach((item) => {
					idlist.push(item.id);
				});
			
				_this
					.$confirm("确定要删除吗?", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
					.then(() => {
						_this.$request.post(
							"Api/WalletsChongzhi_Select_Admin/Delete", {
								token_admin: _this.$utils.getloc("token_admin"),
								userid_admin: _this.$utils.getloc("userid_admin"),
								delete_id: idlist.toString(),
							},
							(res) => {
								Notification({
									title: "操作成功",
									message: res.data.msg,
									offset: 100,
									type: "success",
								});
								_this.getdata();
							},
							false,
							false
						);
					})
					.catch(() => {});
			},
			
			
			
		},
	};
</script>

<style scoped="scoped">
	/deep/ .input_number {
		line-height: 25px;
		width: 160px;
	}

	/deep/ .input_number input {
		height: 27px;
	}

	.el_row {
		padding-top: 20px;
	}

	.updatetext {
		color: #409EFF;
	}
	
	.el_option {
		padding-bottom: 20px;
	}
	
	.delete_btn {
		margin-left: 20px;
	}
</style>
