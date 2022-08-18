<template>
	<div class="zengjianselect">
		<PageTitle title="货币增减"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-row>
					<el-col :span="12" :xs="24" :sm="24" :md="12">
						<el-form ref="form" label-width="80px">
							<el-form-item label="会员编号" style="width: 292px;">
								<el-input v-model="userid" placeholder="会员编号"></el-input>
							</el-form-item>
							<el-form-item label="货币类型" style="width: 295px;">
								<el-select v-model="mrhbname" placeholder="请选择" @change="hblxChange">
									<el-option :value="item.id" :label="item.coinname" v-for="(item,index) of selectlx"
										:key="index"></el-option>
								</el-select>
							</el-form-item>
							<el-form-item label="金额" style="width: 292px;">
								<el-input type="number" v-model="jine" placeholder="金额"></el-input>
							</el-form-item>
							<el-form-item label="备注" style="width: 292px;">
								<el-input v-model="beizhu" placeholder="可不填"></el-input>
							</el-form-item>
							<el-form-item>
								<el-button icon="el-icon-check" type="success" size="mini" @click="zengjian">提交
								</el-button>
								<el-button icon="el-icon-search" type="primary" size="mini" @click="cx">查询</el-button>
							</el-form-item>
						</el-form>
					</el-col>
					<el-col :span="12" :xs="24" :sm="24" :md="12" v-if="cxshow">
						<el-row>
							<el-col :span="8" class="wauseridtext">会员编号</el-col>
							<el-col :span="16" class="wauserid">{{suserid}}</el-col>
						</el-row>
						<el-row v-for="(item,index) of wallet" :key="index" class="walletbox">
							<el-col :span="8" class="waacnamezh">{{item.cnameZh}}</el-col>
							<el-col :span="16">{{item.jine}}</el-col>
						</el-row>
					</el-col>
				</el-row>

				<TablePadding title="货币增减" :list="list" :key="refresh" :ischeckbox="true" :isexcel="true" :summary="true"
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt" >
						<el-button  icon="el-icon-delete" type="danger" size="mini" @click="zjdelete" v-if="useridadmin == 'admin'">删除</el-button>
					</div>
					<el-table-column prop="zdate" label-class-name="zdate" label="日期"></el-table-column>
					<el-table-column prop="userid" label-class-name="userid" label="会员编号"></el-table-column>
					<el-table-column prop="coinname" label-class-name="coinname" label="类型"></el-table-column>
					<el-table-column prop="jine" label="金额"></el-table-column>
					<el-table-column prop="bz" label="备注"></el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import PageTitle from "../../components/PageTitle";
	import TablePadding from '../../components/TablePadding';
	import {
		Notification,
		MessageBox,
		InputNumber,
		Select,
		Input
	} from "element-ui";
	Vue.use(InputNumber);
	Vue.use(Select);
	Vue.use(Input);
	Vue.prototype.$prompt = MessageBox.prompt;
	Vue.prototype.$notify = Notification;
	export default {
		name: "zengjianselect",
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				index: 1,
				list: [],
				pagelist: [],
				refresh: false,
				zjid: 0,
				selectlx: [],
				mrhbname: '',
				userid: '',
				suserid: '',
				jine: '',
				beizhu: '',
				wallet: [],
				cxshow: false,
				useridadmin:''
			};
		},
		created() {
			this.useridadmin = this.$utils.getloc("userid_admin");
			//console.log(this.useridadmin);
			this.getselectlx(); // 查询所有货币
			this.getdata(); // 查询增减记录
		},
		methods: {
			hblxChange: function(e) {
				this.zjid = e;
			},
			getselectlx: function() { // 查询所有货币
				var _this = this;
				this.$request.post(
					"Api/WalletsCoin_Admin/List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin")
					},
					(res) => {
						var list = res.data.data;
						//console.log(l);
						if (list.length <= 0) {
							_this.mrhbname = "请先设置可增减的货币";
						} else {
							_this.selectlx = list;
							_this.mrhbname = list[0].coinname;
							_this.zjid = list[0].id;
						}

					}
				);
			},
			getdata: function() { // 查询增减记录
				var _this = this;
				_this.$request.post("Api/WalletsZengjian_Admin/List", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				});
			},
			zjdelete: function() {
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
							"Api/WalletsZengjian_Admin/Delete", {
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
							}
						);
					})
					.catch(() => {});
			},

			zengjian: function() { // 货币增减
				var _this = this;
				if (_this.userid == "") {
					Notification({
						message: '请输入用户名',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				if (_this.jine == "" || _this.jine == 0) {
					Notification({
						message: '请输入正确金额',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				_this.$confirm('确定要提交吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					var _this = this;
					_this.$request.post("Api/WalletsZengjian_Admin/Zengjian", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						userid: _this.userid,
						zid: _this.zjid,
						jine: _this.jine,
						beizhu: _this.beizhu
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.getdata();
						_this.cx();


						_this.jine = "";
						_this.beizhu = "";
					});
				}).catch(() => {

				});
			},
			cx: function() { // 查询单个用户数据
				var _this = this;
				_this.wallet =[];
				if (_this.userid == "") {
					Notification({
						message: '请输入用户名',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				this.$request.post(
					"Api/Wallets_Admin/GetWallet", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						userid: _this.userid
					},
					(res) => {
						var data = res.data.data;
						//console.log(data);
						data.forEach(item=>{
							if(item.cid == 1 || item.cid == 2){
								_this.wallet.push(item);
							}
						})
						
						_this.suserid = _this.userid;
						_this.cxshow = true;
					}
				);
			},
		},
	};
</script>

<style scoped="scoped">
	.wauserid {
		font-size: 14px;
	}

	.wauseridtext,
	.waacnamezh {
		text-align: right;
		padding-right: 30px;
		font-size: 14px;
	}

	.walletbox {
		padding-top: 15px;
		font-size: 14px;
	}
</style>
