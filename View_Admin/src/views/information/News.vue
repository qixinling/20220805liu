<template>
	<div class="news">
		<PageTitle title="新闻" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="操作日志" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false" ref="tablepadding">
					<div slot="btn_solt">
						<el-button icon="el-icon-circle-plus-outline" type="success" size="mini" @click="tolink('NewsAdd')">发布新闻</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>
					<el-table-column prop="news_title" label="标题" label-class-name="news_title"></el-table-column>
					<el-table-column prop="news_operator" label="发布人"></el-table-column>
					<el-table-column label="发布时间" label-class-name="news_time">
						<template slot-scope="scope">{{ scope.row.news_time }}</template>
					</el-table-column>
					<el-table-column prop="zt" label="状态">
						<template slot-scope="scope">
							<div v-if="scope.row.display==0" style="color: red;">未发布</div>
							<div v-if="scope.row.display==1">已发布</div>
						</template>
					</el-table-column>
					<el-table-column label="操作">
						<template slot-scope="scope">
							<router-link :to="'NewsUpdate?id=' + scope.row.id" class="routerlink">查看详情</router-link>
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
		Notification
	} from 'element-ui';

	Vue.prototype.$notify = Notification;
	export default {
		name: 'news',
		components: {
			PageTitle,
			TablePadding,
		},
		data() {
			return {
				refresh: false,
				list: []
			}
		},
		created() {
			this.getdata();
		},
		methods: {
			tolink: function (_path) {
				this.$router.push({
					path: _path
				})
			},
			getdata: function() { // 查询所有新闻
				this.$request.post("Api/News_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				},true);

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
				var idslist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idslist.push(item.id);
				})

				_this.$confirm('确定要删除吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					this.$request.post("Api/News_Admin/Delete", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						delete_id: idslist.toString()
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
	.news {
		
	}

	.routerlink {
		color: #409EFF;
		text-decoration: none;
	}
</style>
