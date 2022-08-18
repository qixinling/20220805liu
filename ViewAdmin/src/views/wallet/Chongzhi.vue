<template>
	<div class="chongzhi">
		<PageTitle title="充值申请"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="充值申请" :list="list" :key="refresh" :ischeckbox="true" ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button icon="el-icon-check" type="success" size="mini" @click="shenhe">审核通过</el-button>
						<el-button icon="el-icon-warning-outline" type="warning" size="mini" @click="chexiao">撤销
						</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="czdelete">删除</el-button>
					</div>
					<el-table-column prop="cdate" label-class-name="cdate" label="日期" width="180"></el-table-column>
					<el-table-column prop="userid" label-class-name="userid" label="会员编号"></el-table-column>
					<el-table-column prop="username" label-class-name="username" label="姓名"></el-table-column>
					<el-table-column prop="coinname" label-class-name="coinname" label="充值类型"></el-table-column>
					<el-table-column prop="jine" label="金额"></el-table-column>
					<el-table-column prop="bz" label="备注"></el-table-column>
				<el-table-column label="支付凭证" width="100">
					<template slot-scope="scope">
						<a :href="getimg(scope.row.czimg)">
							<img :src="getimg(scope.row.czimg)" width="50px" height="50px" />
						</a>
					</template>
				</el-table-column>
					<el-table-column prop="ispay" label-class-name="ispay" label="状态">
						<template slot-scope="scope">
							<div v-if="scope.row.ispay==0" style="color:red;">未审核</div>
							<div v-else-if="scope.row.ispay==1" style="color:#67C23A;">已审核</div>
							<div v-else-if="scope.row.ispay==2" style="color:#E6A23C;">已撤销</div>
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
	} from "element-ui";
	Vue.prototype.$notify = Notification;
	export default {
		name: "chongzhi",
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				refresh: false,
				list: []
			};
		},
		created() {
			this.getdata();
		},
		methods: {
			getdata: function() {
				this.$request.post(
					"Api/WalletsChongzhi_Admin/List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						this.list = res.data.data;
						this.refresh = !this.refresh;
					}
				);
			},
			shenhe: function() {
				console.log("1")
				// 审核
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: "请选择要操作的数据",
						offset: 100,
						type: "warning",
					});
					return;
				}
				var ids = [];
				_this.$refs.tablepadding.select.forEach((item) => {
					ids.push(item.id);
				});
				console.log("审核");
				_this
					.$confirm("确定要通过审核吗?", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
					.then(() => {
						this.$request.post(
							"Api/WalletsChongzhi_Admin/Pass", {
								token_admin: this.$utils.getloc("token_admin"),
								userid_admin: this.$utils.getloc("userid_admin"),
								ids: ids.toString()
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
			chexiao: function() {
				// 撤销
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: "请选择要操作的数据",
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
					.$confirm("确定要撤销吗?", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
					.then(() => {
						this.$request.post(
							"Api/WalletsChongzhi_Admin/Revoke", {
								token_admin: this.$utils.getloc("token_admin"),
								userid_admin: this.$utils.getloc("userid_admin"),
								cid: idlist.toString(),
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
			czdelete: function() {
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
							"Api/WalletsChongzhi_Admin/Delete", {
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
			getimg: function (img) {
				return this.$config.img_url + img;
			},
		}
	};
</script>

<style scoped="scoped">
</style>
