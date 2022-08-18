<template>
	<div class="shopimglist">
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false" :summary="false"  ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button type="success" size="mini" icon="el-icon-circle-plus-outline" @click="tolink('ShopimgAdd')">添加图片</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>

					<el-table-column label="图片" :width="300">
						<template slot-scope="scope">
							<div v-if="scope.row.lx==1">
								<img :src="getimguri(scope.row.img[0])" style="width: 150px;border-radius: 10px;" />
							</div>
							<div v-if="scope.row.lx==2">
								<img v-for="(item, index) of scope.row.img" :key="index" :src="getimguri(item)" style="width: 80px;margin-left: 5px;border-radius: 10px;" />
							</div>
						</template>
					</el-table-column>
					<el-table-column label="类型">
						<template slot-scope="scope">
							<div v-if="scope.row.lx==1">单张</div>
							<div v-if="scope.row.lx==2">三张</div>
						</template>
					</el-table-column>
					<!-- <el-table-column label="绑定">
						<template slot-scope="scope">
							<div v-html="scope.row.gid" class="imgbdname"></div>
						</template>
					</el-table-column> -->
					
					<el-table-column label="绑定">
						<template slot-scope="scope">
							<div v-for="(item, index) of scope.row.gid" :key="index" class="imgbdname">{{item}}</div>
						</template>
					</el-table-column>

					<el-table-column label="操作">
						<template slot-scope="scope">
							<router-link :to="'ShopimgUpdate?id=' + scope.row.id" class="routerlink">查看详情</router-link>
						</template>
					</el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from 'vue';
	import PageTitle from '../../components/PageTitle';
	import TablePadding from '../../components/TablePadding';
	import {
		Message,
		Table,
		TableColumn,
		Notification
	} from 'element-ui';
	Vue.use(Table);
	Vue.use(TableColumn);
	Vue.prototype.$notify = Notification;
	export default {
		name: 'shopimglist',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				title:"商城图片",
				searchlist: [],
				refresh: false,
				list: [],
				goodslist: [],
				newslist: []
			}
		},
		created() {
			this.getgoods();
		},
		methods: {
			getgoods: function() {  // 查询所有商品
				this.$request.post("Api/ShopGoods_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.goodslist = res.data.data;
					this.getnews();
				});
			},
			getnews: function() {  // 查询所有新闻
				this.$request.post("Api/News_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.newslist = res.data.data;
					this.getdata();
				});
			},
			tolink: function(_path) {
				this.$router.push({
					path: _path
				})
			},
			getimguri: function(img) {
				if (img != null) {
					return this.$config.img_url + img;
				} else {
					return this.$config.img_url + 'null.jpg';
				}
			},
			getdata: function() {  // 查询所有商城图片
				this.$request.post("Api/Shopimg_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					var list = res.data.data;
					
					var newlist = [];
					list.forEach((item, index) => {
						var imglist = item.img.split(",");
						item.img = imglist;

						item.gid = item.gid.split(",");
						item.bdlx = item.bdlx.split(",");
						var gidlist = "";
						
						for (var i = 0; i < item.bdlx.length; i++) {
							if (item.bdlx[i] == 1) { // 商品
								for (var j = 0; j < this.goodslist.length; j++) {
									if (item.gid[i] == this.goodslist[j].id) {
										if (gidlist != "") {
											gidlist += ",";
										}
										gidlist += "[商品]" + this.goodslist[j].goodsname;
									}
								}
							} else { // 新闻
								for (var k = 0; k < this.newslist.length; k++) {
									if (item.gid[i] == this.newslist[k].id) {
										if (gidlist != "") {
											gidlist += ",";
										}
										gidlist += "[新闻]" + this.newslist[k].news_title;
									}
								}
							}
						}
						
						item.gid = gidlist.split(",");
						newlist.push(item);
					})

					this.list = newlist;
					this.searchlist = this.list;
					this.refresh = !this.refresh;
				}, false);
			},
			Delete: function() {
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要删除的数据',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				_this.$confirm('确定要删除吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					var idlist = [];
					_this.$refs.tablepadding.select.forEach(item => {
						idlist.push(item.id);
					})

					_this.$request.post("Api/Shopimg_Admin/Delete", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						delete_id: idlist.toString()
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.getdata();
					});
				}).catch(() => {

				});
			}
		}
	}
</script>

<style scoped="scoped">
	.shopimglist {
		
	}

	.routerlink {
		color: #409EFF;
		text-decoration: none;
	}

	.imgbdname {
		display: -webkit-box;
		overflow: hidden;
		-webkit-box-orient: vertical;
		-webkit-line-clamp: 3;
	}
</style>
