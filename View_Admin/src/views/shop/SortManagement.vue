<template>
	<div>
		<PageTitle title="分类管理" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-row style="margin-top: 10px;margin-bottom: 10px;">
					<el-col :md="24" :sm="24">
						<el-button type="success" size="mini" icon="el-icon-circle-plus-outline" @click="ToLink('DaleiAdd')">添加大类</el-button>
						<el-button type="success" size="mini" icon="el-icon-circle-plus-outline" @click="ToLink('XiaoleiAdd')">添加小类</el-button>
					</el-col>
				</el-row>
				<el-table :data="data" style="width: 100%;margin-bottom: 20px;" row-key="key" border lazy :load="load" :tree-props="{hasChildren: 'child'}"
				 :row-class-name="tableRowClassName">
					<el-table-column prop="name" label="名称" width="180"></el-table-column>
					<el-table-column prop="img" label="图片" width="100">
						<template slot-scope="scope">
							<img :src="getimg(scope.row.img)" width="50px" height="50px" />
						</template>
					</el-table-column>
					<el-table-column prop="order" label="排序" width="180"></el-table-column>
					<el-table-column prop="putaway" label="是否上架" width="180">
						<template slot-scope="scope">
							<el-switch v-model="scope.row.putaway" @change="ChangePutaway(scope.row)"></el-switch>
						</template>
					</el-table-column>
					<el-table-column prop="num" label="商品数量" width="180"></el-table-column>
					<el-table-column label="操作">
						<template slot-scope="scope">
							<el-button class="jiange" @click="ToLink(scope.row.ischild==1?'XiaoleiUpdate?id='+scope.row.id:'DaleiUpdate?id='+scope.row.id)"
							 type="text" size="small">编辑</el-button>
							<el-button class="jiange" @click="Del(scope.row,scope.$index)" type="text" size="small" style="color:#f56c6c;">删除</el-button>
						</template>
					</el-table-column>
				</el-table>
			</div>
		</div>
	</div>
</template>

<script>
	import PageTitle from "../../components/PageTitle";
	import Vue from "vue";
	import {
		Table,
		TableColumn,
		Notification,
		Switch
	} from "element-ui";
	Vue.use(Table);
	Vue.use(TableColumn);
	Vue.use(Switch);
	Vue.prototype.$notify = Notification;
	export default {
		name: "SortManagement",
		components: {
			PageTitle,
		},
		data() {
			return {
				data: [],
			};
		},
		created() {
			this.GetData();
		},
		methods: {
			//获取大/小类api
			GetApi(ischild) {
				var api = "Api/ShopGoodsSort_Admin";
				if (ischild == 1) {
					api = "Api/ShopGoodsSortChild_Admin";
				}
				return api;
			},
			//是否展示修改
			ChangePagemark(row) {
				this.$request.post(
					this.GetApi(row.ischild) + "/ChangePagemark", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id: row.id,
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
			},
			//上下架修改
			ChangePutaway(row) {
				this.$request.post(
					this.GetApi(row.ischild) + "/ChangePutaway", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id: row.id,
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
			},
			//删除
			Del(row, index) {
				var msg = "确定要删除吗?";
				if (row.num > 0) {
					msg = msg + "此类下存在的" + row.num + "个商品将会移至未分类";
				}
				this.$confirm(msg, {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
					.then(() => {
						this.$request.post(
							this.GetApi(row.ischild) + "/Delete", {
								token_admin: this.$utils.getloc("token_admin"),
								userid_admin: this.$utils.getloc("userid_admin"),
								ids: row.id,
							},
							(res) => {
								this.GetData();
								this.$notify({
									title: "提示",
									message: res.data.msg,
									offset: 100,
									type: "success",
								});
							}
						);
					})
					.catch(() => {});
			},
			//获取数据
			GetData() {
				this.data = [];
				this.$request.post(
					"Api/ShopGoodsSort_Admin/List", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						res.data.data.forEach((item) => {
							item.pagemark = item.pagemark == 1 ? true : false;
							item.putaway = item.putaway == 1 ? true : false;
							if (item.haveChild == 1) {
								item.child = JSON.parse(item.child);
							}
						});
						this.data = res.data.data;

					}
				);
			},
			//懒加载小类
			load(tree, treeNode, resolve) {
				this.$request.post(
					"Api/ShopGoodsSort_Admin/ChildList", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id: tree.id,
					},
					(res) => {
						res.data.data.forEach((item) => {
							item.pagemark = item.pagemark == 1 ? true : false;
							item.putaway = item.putaway == 1 ? true : false;
						});
						resolve(res.data.data);
					},
					false,
					false
				);
			},
			//颜色区分大小类
			tableRowClassName({
				row,
				rowIndex
			}) {
				if (row.ischild == 1) {
					return "child-class";
				}
				return "";
			},
			//链接
			ToLink: function(_path) {
				this.$router.push({
					path: _path,
				});
			},
			//获取图片
			getimg(img) {
				return this.$config.img_url + img;
			},
		},
	};
</script>

<style scoped>
	/deep/ .el-table .child-class {
		background: #f0f9eb;
	}

	.jiange {
		margin: 0 0.2em;
	}
</style>
