<template>
	<div>
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false" 
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						
					</div>
					<el-table-column prop="hdate" label-class-name="hdate" label="时间"  />
					<el-table-column prop="userid" label-class-name="userid" label="会员编号"  />
					<el-table-column prop="username" label-class-name="username" label="姓名"  />
					<el-table-column prop="usertel" label-class-name="usertel" label="电话"  />
					<el-table-column prop="cname" label-class-name="cname" label="宠物名"  />
					<el-table-column prop="cprice"  label="当前价值"  />
					<el-table-column prop="rishouyi" label="日收益(%)"  />
					<el-table-column prop="zshouyi" label="总收益"  />
					<el-table-column prop="zhouqi" label="周期"  />
					
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
				title:"正在分红列表",
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
					"Api/UsersHold_Admin/SellList",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						lx:0
					},
					(res) => {
						//console.log(res.data);
						this.list = res.data.data;
						console.log(this.list);
			
						this.refresh = !this.refresh;
					}
				);
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