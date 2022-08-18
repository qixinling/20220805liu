<template>
	<div>
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false" 
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button type="success" size="mini" icon="el-icon-circle-plus-outline"
							@click="tolink('TeachersAdd')">添加</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>
					<el-table-column label="图片" width="100">
						<template slot-scope="scope">
							<img :src="getimg(scope.row.tximg)" width="50px" height="50px" />
						</template>
					</el-table-column>
					<el-table-column prop="name" label-class-name="name" label="姓名"  />
					<el-table-column prop="introduce" label-class-name="introduce" label="简介"  />
					
					<!-- <el-table-column label="上架" width="80">
						<template slot-scope="scope">
							<el-switch v-model="scope.row.ispay" :inactive-value="0" :active-value="1"
								@change="Update(scope.row)"></el-switch>
						</template>
					</el-table-column> -->
					
					<el-table-column label="操作">
						<template slot-scope="scope">
							<router-link :to="'TeachersUpdate?id=' + scope.row.id" class="routerlink">编辑</router-link>
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
		TimePicker
	} from "element-ui";
	Vue.use(Input);
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Switch);
	Vue.use(InputNumber);
	Vue.use(TimePicker );
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				title:"画家列表",
				refresh: false,
				list: [],
			};
		},
		created() {
			this.getdata();
		},
		methods: {
			time:function(row){
				console.log(row);
				 //row.qdate = row.qdate.format("hh:mm:ss");
				  //console.log(row);
				  this.Update(row);
			},
			//获取数据
			getdata: function () {
				this.$request.post(
					"Api/Teachers_Admin/List",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						this.list = res.data.data;
						//console.log(res.data.data);
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
								"Api/Teachers_Admin/Delete",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									ids: ids.toString(),
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