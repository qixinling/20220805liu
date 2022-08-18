<template>
	<div class="slide">
		<PageTitle title="幻灯片"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :list="list" :key="refresh" :ischeckbox="true"  ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<el-button type="success" size="mini" icon="el-icon-circle-plus-outline" @click="tolink('SlideAdd')">添加幻灯片</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>
					
					<el-table-column label="图片" width="200">
						<template slot-scope="scope">
							<img :src="getimguri(scope.row.img)" style="width: 180px;" />
						</template>
					</el-table-column>
					<el-table-column label="展示页面" prop="pagelxname"></el-table-column>
					<el-table-column prop="lxname" label="关联类型"></el-table-column>
					<el-table-column label="关联名称">
						<template slot-scope="scope">
							<div class="glname">{{scope.row.gid}}</div>
						</template>
					</el-table-column>
					<el-table-column label="操作">
						<template slot-scope="scope">
							<router-link :to="'SlideUpdate?id=' + scope.row.id" class="routerlink">查看详情</router-link>
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
		name: 'slide',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				input: '',
				searchlist: [],
				refresh: false,
				list: []
			}
		},
		created() {
			this.getdata();
		},
		methods: {
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
			getdata: function() {
				var _this = this;
				this.$request.post("Api/Slide_Admin/List", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin')
				}, (res) => {
					var list = res.data.data;
					list.forEach(item => {
						if (item.lx == 2) {
							_this.$request.post("Api/ShopGoods_Admin/Get", {
								token_admin: _this.$utils.getloc('token_admin'),
								userid_admin: _this.$utils.getloc('userid_admin'),
								gid: item.gid
							}, (res) => {
								var goods = JSON.parse(res.data.data.data);
								item.gid = goods.Goodsname;
							});
						} else if (item.lx == 3) {
							_this.$request.post("Api/News_Admin/Get", {
								token_admin: _this.$utils.getloc('token_admin'),
								userid_admin: _this.$utils.getloc('userid_admin'),
								id: item.gid
							}, (res) => {
								item.gid = res.data.data.news_title;
							});
						} else if (item.lx == 4) {
							item.gid = item.url;
						} else if (item.lx == 5) {
							_this.$request.post("Api/ShopGoodsSortChild_Admin/Get", {
								token_admin: _this.$utils.getloc('token_admin'),
								userid_admin: _this.$utils.getloc('userid_admin'),
								id: item.gid
							}, (res) => {
								item.gid = res.data.data.xiaoleiname;
							});
						}
					})
					this.list = list;
					this.searchlist = this.list;
					this.refresh = !this.refresh;
				});
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

					_this.$request.post("Api/Slide_Admin/Delete", {
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
	.slide {
		
	}

	.routerlink {
		color: #409EFF;
		text-decoration: none;
	}

	.glname {
		display: -webkit-box;
		overflow: hidden;
		-webkit-box-orient: vertical;
		-webkit-line-clamp: 3;
	}
</style>
