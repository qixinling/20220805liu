<template>
	<div class="zhuanhuanselect">
		<PageTitle title="转换管理"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<div class="addHb">
					<el-dialog title="提示" :visible.sync="addVisible" width="30%" :modal-append-to-body="false">
						<el-row>
							<el-col :span="24" class="el_option">
								<el-select v-model="coinname1" placeholder="请选择货币" @change="ChangeSelecthb1">
									<el-option v-for="(item,index) in options" :key="index" :label="item.coinname"
										:value="item.cid"></el-option>
								</el-select>
							</el-col>
						</el-row>
						<el-row>
							<el-col :span="24" class="el_option">
								<el-select v-model="coinname2" placeholder="请选择货币" @change="ChangeSelecthb2">
									<el-option v-for="(item,index) in options" :key="index" :label="item.coinname"
										:value="item.cid"></el-option>
								</el-select>
							</el-col>
						</el-row>
						<el-row>
							<el-col :span="7">
								<span>最小转换金额</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinemin" @change="handleChangeJinemin"
									:min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>最大转换金额</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinemax" @change="handleChangeJinemax"
									:min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>转换倍数</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinebs" @change="handleChangeJinebs"
									:min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>转换比例%</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="bili" @change="handleChangeBili" :min="0"
									:max="100"></el-input-number>
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
								<span>最小转换金额</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinemin" @change="handleChangeJinemin"
									:min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>最大转换金额</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinemax" @change="handleChangeJinemax"
									:min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>转换倍数</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="jinebs" @change="handleChangeJinebs"
									:min="1" :max="99999"></el-input-number>
							</el-col>
						</el-row>
						<el-row class="el_row">
							<el-col :span="7">
								<span>转换比例%</span>
							</el-col>
							<el-col :span="17">
								<el-input-number class="input_number" v-model="bili" @change="handleChangeBili" :min="0"
									:max="100"></el-input-number>
							</el-col>
						</el-row>
						<span slot="footer" class="dialog-footer">
							<el-button @click="dialogFormVisible = false">取 消</el-button>
							<el-button type="primary" @click="Update">确 定</el-button>
						</span>
					</el-dialog>
				</div>

				<TablePadding title="转换管理" :list="list" :key="refresh" :ischeckbox="true" :summary="false"
					 ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button icon="el-icon-circle-plus" type="success" size="mini" @click="addVisible = true">
							添加转换货币</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete" class="delete_btn">删除
						</el-button>
					</div>
					<el-table-column label="名称" width="160">
						<template slot-scope="scope">
							<div>{{scope.row.coinname1}} 转 {{scope.row.coinname2}}</div>
						</template>
					</el-table-column>
					<el-table-column prop="jinemin" label="最小转换金额"></el-table-column>
					<el-table-column prop="jinemax" label="最大转换金额"></el-table-column>
					<el-table-column prop="jinebs" label="转换倍数"></el-table-column>
					<el-table-column prop="bili" label="转换比例%"></el-table-column>
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
		Select
	} from "element-ui";

	Vue.use(Dialog);
	Vue.use(InputNumber);
	Vue.use(Select);
	Vue.prototype.$prompt = MessageBox.prompt;
	Vue.prototype.$notify = Notification;
	export default {
		name: "zhuanhuanselect",
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
				cid1: 0,
				cid2: 0,
				jinemax: 1,
				jinemin: 1,
				jinebs: 1,
				bili: 0,
				coinname1: '',
				coinname2: '',
				options: [],
			};
		},
		created() {
			this.getcoin();
			this.getdata();
		},
		methods: {
			ChangeSelecthb1: function(value) { // 选择货币
				this.cid1 = value;
			},
			ChangeSelecthb2: function(value) { // 选择货币
				this.cid2 = value;
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
			Add: function() { // 添加
				if (this.cid1 <= 0 || this.cid2 <= 0) {
					this.$notify({
						title: '提示',
						message: '请选择货币',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				if (this.cid1 == this.cid2) {
					this.$notify({
						title: '提示',
						message: '请选择两种不一样的货币',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				this.$request.post(
					"Api/WalletsZhuanhuan_Select_Admin/Add", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						cid1: this.cid1,
						cid2: this.cid2,
						jinemax: this.jinemax,
						jinemin: this.jinemin,
						jinebs: this.jinebs,
						bili: this.bili
					},
					(res) => {
						this.$notify({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						this.coinname2 = '';
						this.cid2 = 0;
						this.coinname1 = '';
						this.cid1 = 0;
						this.jinemax = 1;
						this.jinemin = 1;
						this.jinebs = 1;
						this.bili = 0;
						this.addVisible = false;
						this.getdata();
					}
				);
			},
			Delete: function() { // 删除货币
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
							"Api/WalletsZhuanhuan_Select_Admin/Delete", {
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
			handleChangeJinemin: function(value) { // 最小
				this.jinemin = value;
			},
			handleChangeJinemax: function(value) { // 最大
				this.jinemax = value;
			},
			handleChangeJinebs: function(value) { // 倍数
				this.jinebs = value;
			},
			handleChangeBili: function(value) { // 比例
				this.bili = value;
			},
			dialogFormVisibleShow: function(id) { // 弹出弹框
				this.list.forEach(item => {
					if (item.id == id) {
						this.cid = id;
						this.jinemax = item.jinemax;
						this.jinemin = item.jinemin;
						this.jinebs = item.jinebs;
						this.bili = item.bili;
					}
				})
				this.dialogFormVisible = true;
			},
			Update: function() { // 修改
				this.$request.post(
					"Api/WalletsZhuanhuan_Select_Admin/Update", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						cid: this.cid,
						jinemax: this.jinemax,
						jinemin: this.jinemin,
						jinebs: this.jinebs,
						bili: this.bili
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
						this.bili = 0;
						this.dialogFormVisible = false;
						this.getdata();
					}
				);
			},
			getdata: function() { // 查询所有数据
				this.$request.post(
					"Api/WalletsZhuanhuan_Select_Admin/List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin")
					},
					(res) => {
						this.list = res.data.data;
						this.refresh = !this.refresh;

					}
				);
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
