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
							@click="tolink('GoodsAdd')">添加商品</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>

					<el-table-column label="图片" width="100">
						<template slot-scope="scope">
							<img :src="getimg(scope.row.goodsimg)" width="50px" height="50px" />
						</template>
					</el-table-column>
					<el-table-column prop="goodsNo" label-class-name="goodsNo" label="编号" width="150" />
					<el-table-column prop="goodsname" label-class-name="goodsname" label="名称" width="300" />
					<el-table-column label="上架" width="80">
						<template slot-scope="scope">
							<el-switch v-model="scope.row.ispay" :inactive-value="0" :active-value="1"
								@change="Update(scope.row)"></el-switch>
						</template>
					</el-table-column>
					<el-table-column label="首页展示" width="80">
						<template slot-scope="scope">
							<el-switch v-model="scope.row.ishome" :inactive-value="0" :active-value="1"
								@change="Update(scope.row)"></el-switch>
						</template>
					</el-table-column>
					<el-table-column label="排序" width="160">
						<template slot-scope="scope">
							<el-input-number size="mini" v-model="scope.row.sort" :min="0" @change="Update(scope.row)"
								:controls="false"></el-input-number>
						</template>
					</el-table-column>
					<el-table-column label="类型" width="140">
						<template slot-scope="scope">
							<el-select v-model="scope.row.goodstype" placeholder="请选择商品类型" @change="Update(scope.row)">
								<el-option v-for="(item, index) in options" :key="index" :label="item.label"
									:value="item.value"></el-option>
							</el-select>
						</template>
					</el-table-column>
					<el-table-column label="分类" width="140">
						<template slot-scope="scope">
							<el-select v-model="scope.row.xlid" placeholder="请选择小类" @change="Update(scope.row)">
								<el-option v-for="(item, index) in xllist" :key="index" :label="item.xiaoleiname"
									:value="item.id"></el-option>
							</el-select>
						</template>
					</el-table-column>
					<el-table-column label="零售价" width="160">
						<template slot-scope="scope">
							<el-input-number size="mini" v-model="scope.row.goodsprice" :min="0"
								@change="Update(scope.row)" :controls="false"></el-input-number>
						</template>
					</el-table-column>
					<el-table-column label="信用值" width="160">
						<template slot-scope="scope">
							<el-input-number size="mini" v-model="scope.row.xyzjine" :min="0"
								@change="Update(scope.row)" :controls="false"></el-input-number>
						</template>
					</el-table-column>
					<el-table-column prop="stock" label="库存" width="160">
						<template slot-scope="scope">
							<el-input-number size="mini" v-model="scope.row.stock" :min="0" @change="Update(scope.row)"
								:controls="false"></el-input-number>
						</template>
					</el-table-column>
					<el-table-column prop="sales" label="销量" width="100"></el-table-column>
					<el-table-column label="操作">
						<template slot-scope="scope">
							<router-link :to="'GoodsUpdate?Id=' + scope.row.id" class="routerlink">编辑</router-link>
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
	} from "element-ui";
	Vue.use(Input);
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Switch);
	Vue.use(InputNumber);
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				title:"商品列表",
				refresh: false,
				list: [],
				delids: [],
				options: [
					{
						value: 1,
						label: "商城产品",
					},
					{
						value: 0,
						label: "激活商品",
					},
					
				],
				xllist: [],
			};
		},
		created() {
			this.getdata();
			this.GetXiaolei();
		},
		methods: {
			Update(row) {
				//console.log(JSON.stringify(row));
				let _this = this;
				_this.$request.post(
					"Api/ShopGoods_Admin/Update",
					{
						token_admin: _this.$utils.getloc("token_admin"),
						userid_admin: _this.$utils.getloc("userid_admin"),
						goods: JSON.stringify(row),
					},
					(res) => {
						_this.$notify({
							title: "提示",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
					}, false, false
				);
			},
			//获取小类
			GetXiaolei() {
				let _this = this;
				_this.$request.post(
					"Api/ShopGoodsSortChild_Admin/List_Name",
					{
						token_admin: _this.$utils.getloc("token_admin"),
						userid_admin: _this.$utils.getloc("userid_admin"),
					},
					(res) => {
						_this.xllist = res.data.data;
					},
					false,
					false
				);
			},
			//获取数据
			getdata: function () {
				this.$request.post(
					"Api/ShopGoods_Admin/List",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						var list = res.data.data;
						list.forEach((item) => {
							item.goodsimg = item.goodsimg.split(",")[0];
						});
						this.list = list;
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
							this.$refs.tablepadding.select.forEach((item) => {
								this.delids.push(item.id);
							});
							this.$request.post(
								"Api/ShopGoods_Admin/Delete",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									delete_id: this.delids.toString(),
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