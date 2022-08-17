<template>
	<div>
		<PageTitle title="系统参数" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form label-width="80px">
					<el-tabs :tab-position="'left'">
						<el-tab-pane label="系统设置">
							<el-form-item label="系统开关">
								<el-switch :active-value="1" :inactive-value="0" v-model="ss.switchsystem"></el-switch>
							</el-form-item>
							<el-form-item label="关闭通知">
								<el-input type="textarea" :rows="2" placeholder="请输入系统关闭通知" v-model="ss.systemclosemsg">
								</el-input>
							</el-form-item>
							<el-form-item label="开放时段">
								<el-input style="width: 100px;" v-model="ss.timestart"></el-input>
								--
								<el-input style="width: 100px;" v-model="ss.timeend"></el-input>
							</el-form-item>
							<el-form-item label="关闭通知">
								<el-input type="textarea" :rows="2" placeholder="请输入关闭时段通知" v-model="ss.timeclosemsg">
								</el-input>
							</el-form-item>
							<el-form-item label="滚动消息">
								<el-input type="textarea" :rows="2" placeholder="请输入滚动消息" v-model="ss.marqueemsg">
								</el-input>
							</el-form-item>
							<!-- <el-form-item label="验证码">
								<el-switch :active-value="1" :inactive-value="0" v-model="ss.czbs"></el-switch>
							</el-form-item> -->
						</el-tab-pane>
						<el-tab-pane label="可选账户">
							<el-form-item label="会员可选">
								<el-input type="textarea" :rows="2" placeholder="请输入会员可选账户" v-model="ss.bank">
								</el-input>
							</el-form-item>
						</el-tab-pane>
						<el-tab-pane label="团队参数">
							<el-form-item label="推荐图">
								<el-switch :active-value="1" :inactive-value="0" v-model="ss.switchtjt"></el-switch>
							</el-form-item>
							<el-form-item label="网络图">
								<el-switch :active-value="1" :inactive-value="0" v-model="ss.switchwlt"></el-switch>
							</el-form-item>
						</el-tab-pane>
						<!-- <el-tab-pane label="充值参数">
							<el-form-item label="充值开关">
								<el-switch :active-value="1" :inactive-value="0" v-model="ss.Switchchongzhi"></el-switch>
							</el-form-item>
							<el-form-item label="最小金额">
								<el-input v-model="ss.Czmin" placeholder="请输入充值最小金额"></el-input>
							</el-form-item>
							<el-form-item label="最大金额">
								<el-input v-model="ss.Czmax" placeholder="请输入充值最大金额"></el-input>
							</el-form-item>
							<el-form-item label="倍数">
								<el-input v-model="ss.Czbs" placeholder="请输入充值倍数"></el-input>
							</el-form-item>
						</el-tab-pane>
						<el-tab-pane label="提现参数">
							<el-form-item label="提现开关">
								<el-switch :active-value="1" :inactive-value="0" v-model="ss.Switchtixian"></el-switch>
							</el-form-item>
							<el-form-item label="最小金额">
								<el-input v-model="ss.Txmin" placeholder="请输入提现最小金额"></el-input>
							</el-form-item>
							<el-form-item label="最大金额">
								<el-input v-model="ss.Txmax" placeholder="请输入提现最大金额"></el-input>
							</el-form-item>
							<el-form-item label="倍数">
								<el-input v-model="ss.Txbs" placeholder="请输入提现倍数"></el-input>
							</el-form-item>
							<el-form-item label="手续费 %">
								<el-input v-model="ss.Txsl" placeholder="请输入提现手续费"></el-input>
							</el-form-item>
						</el-tab-pane>
						<el-tab-pane label="转账参数">
							<el-form-item label="转账开关">
								<el-switch :active-value="1" :inactive-value="0" v-model="ss.Switchzhuanzhang"></el-switch>
							</el-form-item>
							<el-form-item label="最小金额">
								<el-input v-model="ss.Zzmin" placeholder="请输入转账最小金额"></el-input>
							</el-form-item>
							<el-form-item label="最大金额">
								<el-input v-model="ss.Zzmax" placeholder="请输入转账最大金额"></el-input>
							</el-form-item>
							<el-form-item label="倍数">
								<el-input v-model="ss.Zzbs" placeholder="请输入转账倍数"></el-input>
							</el-form-item>
						</el-tab-pane>
						<el-tab-pane label="转换参数">
							<el-form-item label="转换开关">
								<el-switch :active-value="1" :inactive-value="0" v-model="ss.Switchzhuanhuan"></el-switch>
							</el-form-item>
							<el-form-item label="倍数">
								<el-input v-model="ss.Zhbs" placeholder="请输入转换倍数"></el-input>
							</el-form-item>
						</el-tab-pane> -->
					</el-tabs>
					<el-form-item>
						<el-button icon="el-icon-edit" type="primary" size="mini" @click="update">修改</el-button>

					</el-form-item>
				</el-form>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import PageTitle from '../../components/PageTitle';
	import {
		Switch,
		Tabs,
		Form,
		FormItem,
		Input,
		TimePicker,
		Button,
		Notification,
		TabPane
	} from "element-ui";
	Vue.use(Switch);
	Vue.use(Button)
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Input);
	Vue.use(Tabs);
	Vue.use(TimePicker);
	Vue.use(TabPane);
	Vue.prototype.$notify = Notification;
	export default {
		name: "SystemParameter",
		components: {
			PageTitle
		},
		data() {
			return {
				ss: []
			};
		},
		created() {
			this.getdata();
		},
		methods: {
			getdata: function () {
				let _this = this;
				_this.$request.post("Api/SystemSetting_Admin/Get", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin')
				}, (res) => {
					_this.ss =res.data.data;
					
				});
			},
			update: function () {
				var _this = this;
				_this.$confirm('确定要修改吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/SystemSetting_Admin/Updata", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						ss: JSON.stringify(_this.ss)
					}, (res) => {
						_this.$notify({
							title: '提示',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
					});
				}).catch(() => {

				});
			},
		}
	};
</script>

<style scoped>
	.el-row {
		margin-bottom: 10px;
	}

	.el-switch {
		margin: 10px;
	}
</style>