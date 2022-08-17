<template>
	<div>
		<PageTitle title="奖金参数" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="50px" size="mini">
					<el-form-item>
						<el-row>
							<el-col :span="24">
								<el-input type="text" style="width: 200px;margin-right: 10px;"
									v-model="systemSettingBonus.name" placeholder="名称"></el-input>
								<el-input style="width: 100px;margin-right: 10px;" v-model="systemSettingBonus.value"
									placeholder="参数"></el-input>
								<el-input style="width: 300px;margin-right: 10px;" v-model="systemSettingBonus.bz"
									placeholder="备注"></el-input>
								<el-input style="width: 100px;margin-right: 10px;" v-model="systemSettingBonus.code"
									placeholder="代号"></el-input>
								<el-button type="success" @click="add" icon="el-icon-plus"></el-button>
							</el-col>
						</el-row>
					</el-form-item>
					<el-form-item v-for="(item,index) in systemSettingBonusList" :key="index">
						<el-row>
							<el-col :span="14">
								<el-input style="width: 200px;margin-right: 10px;" v-model="item.name"></el-input>
								<el-input style="width: 100px;margin-right: 10px;" v-model="item.value"></el-input>
								<span style="font-size: 12px;color: #99A9BF;"> {{item.bz}}</span>
							</el-col>
							<el-col :span="4" style="text-align: right;">
								<el-input style="width: 100px;margin-right: 10px;" v-model="item.code"></el-input>
							</el-col>
							<el-col :span="6" style="text-align: right;">
								<el-button type="primary" v-if="item.index>1" @click="update(item,item.index-1)"
									icon="el-icon-caret-top"></el-button>
								<el-button type="primary" v-if="item.index<systemSettingBonusList.length"
									@click="update(item,item.index+1)" icon="el-icon-caret-bottom"></el-button>
								<el-button type="danger" @click="del(item)" icon="el-icon-delete"></el-button>
							</el-col>
						</el-row>
					</el-form-item>
				</el-form>
				<div style="text-align: center;margin-top: 20px;">
					<el-button type="primary" size="mini" @click="updateAll">修改</el-button>
					<el-button type="danger" size="mini" @click="delAll">清空</el-button>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import PageTitle from "../../components/PageTitle";
	import {
		Form,
		FormItem,
		Input,
		InputNumber,
		Notification,
		Button,
		Tabs,
		TabPane,
	} from "element-ui";
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Button);
	Vue.use(Input);
	Vue.use(InputNumber);
	Vue.use(Tabs);
	Vue.use(TabPane);
	Vue.prototype.$notify = Notification;
	export default {
		name: "BonusParameter",
		components: {
			PageTitle,
		},
		data() {
			return {
				tabPosition: "left",
				systemSettingBonus: {
					code: '',
					name: '',
					value: '',
					bz: ''
				},
				systemSettingBonusList: []
			};
		},
		created() {
			this.getdata();
			//this.getulevel();
		},
		methods: {
			add: function() {
				this.$request.post(
					"Api/SystemSettingBonus_Admin/Add", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						systemSettingBonus: JSON.stringify(this.systemSettingBonus)
					},
					(res) => {
						this.systemSettingBonusList = res.data.data;
					}
				);
			},
			update: function(systemSettingBonus, index) {
				this.$request.post(
					"Api/SystemSettingBonus_Admin/Update", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						systemSettingBonus: JSON.stringify(systemSettingBonus),
						index: index
					},
					(res) => {
						this.systemSettingBonusList = res.data.data;
					}
				);
			},
			del: function(systemSettingBonus) {
				this.$confirm('确定要删除' + systemSettingBonus.name + '吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					this.$request.post(
						"Api/SystemSettingBonus_Admin/Delete", {
							token_admin: this.$utils.getloc("token_admin"),
							userid_admin: this.$utils.getloc("userid_admin"),
							systemSettingBonus: JSON.stringify(systemSettingBonus)
						},
						(res) => {
							this.systemSettingBonusList = res.data.data;
						}
					);
				}).catch(() => {
			
				});
			},
			delAll: function() {
				this.$confirm('确定要清空吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					this.$request.post(
						"Api/SystemSettingBonus_Admin/DeleteAll", {
							token_admin: this.$utils.getloc("token_admin"),
							userid_admin: this.$utils.getloc("userid_admin")
						},
						(res) => {
							this.systemSettingBonusList = res.data.data;
						}
					);
				}).catch(() => {
			
				});
			},
			getdata: function() {
				this.$request.post(
					"Api/SystemSettingBonus_Admin/List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						this.systemSettingBonusList = res.data.data;
					}
				);
			},
			updateAll: function() {
				this.$confirm('确定要修改吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					this.$request.post(
						"Api/SystemSettingBonus_Admin/UpdataAll", {
							token_admin: this.$utils.getloc("token_admin"),
							userid_admin: this.$utils.getloc("userid_admin"),
							systemSettingBonusList: JSON.stringify(this.systemSettingBonusList),
						},
						(res) => {
							this.$notify({
								title: "提示",
								message: res.data.msg,
								offset: 100,
								type: "success",
							});
						}
					);
				}).catch(() => {

				});
			},

		},
	};
</script>

<style scoped>
	.cardcontent .title {
		font-size: 14px;
		color: #333;
		margin-bottom: 10px;
	}

	.margin-b {
		margin-bottom: 10px;
		margin-right: 5px;
		width: 200px;
	}

	@media screen and (max-width: 900px) {
		.margin-b {
			width: 80%;
		}
	}
</style>
