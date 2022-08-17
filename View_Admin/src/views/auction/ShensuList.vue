<template>
	<div>
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false" 
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<!-- <el-button type="success" size="mini" icon="el-icon-circle-plus-outline"
							@click="comshenshu">申诉确认</el-button> -->
					</div>
					<el-table-column prop="hdate" label-class-name="hdate" label="创建时间"  />
					<el-table-column prop="ssdate" label-class-name="ssdate" label="申诉时间"  />
					<el-table-column prop="holdno" label-class-name="holdno" label="编号"  />
					<el-table-column prop="userid" label-class-name="userid" label="卖家用户名"  />
					<el-table-column prop="username" label-class-name="username" label="卖家姓名"  />
					<el-table-column prop="usertel" label-class-name="usertel" label="卖家电话"  />
					<el-table-column prop="jname" label-class-name="jname" label="产品名称"  />
					<el-table-column prop="jprice"  label="抢单金额"  />
					<!-- <el-table-column prop="zhouqi" label="周期"  /> -->
					<el-table-column prop="buserid" label-class-name="buserid" label="买家用户名"  />
					<el-table-column prop="busername" label-class-name="busername" label="买家姓名"  />
					<el-table-column prop="busertel" label-class-name="busertel" label="买家电话"  />
					<el-table-column label="支付凭证" width="100">
						<template slot-scope="scope">
							<a :href="getimg(scope.row.zhimg)">
								<img :src="getimg(scope.row.zhimg)" width="50px" height="50px" />
							</a>
						</template>
					</el-table-column>
					<el-table-column   label="申诉"  width="100">
						<template slot-scope="scope">
							<div v-if="scope.row.sellissu == 1" style="color: green;">卖家申诉</div>
							<div v-if="scope.row.buyissu == 1" style="color: red;">买家申诉</div>
						</template>
					</el-table-column>
					<el-table-column prop="sbz" label="卖家申诉原因"  />
					<el-table-column prop="bbz"  label="买家申诉原因"  />
					<el-table-column   label="交易状态"  width="100">
						<template slot-scope="scope">
							<div v-if="scope.row.state <= 3" style="color: green;">交易中</div>
							<div v-if="scope.row.state == 4" style="color: red;">交易结束</div>
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
				title:"申诉列表",
				refresh: false,
				list: [],
			};
		},
		created() {
			this.getdata();
		},
		methods: {
			getdata: function () {
				this.$request.post(
					"Api/UsersHold_Admin/ShensuList",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin")
					},
					(res) => {
						//console.log(res.data);
						this.list = res.data.data;
						console.log(this.list);
			
						this.refresh = !this.refresh;
					}
				);
			},
			comshenshu: function () {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: "提示",
						message: "请选择要通过申诉的数据",
						offset: 100,
						type: "warning",
					});
				} else {
					this.$confirm("确定要通过申诉吗?", "提示", {
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
								"Api/UsersHold_Admin/PassShensu",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									ids: "," + ids.toString() + ","
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
								"Api/Constellation_Admin/Delete",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									delete_id: ids.toString(),
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