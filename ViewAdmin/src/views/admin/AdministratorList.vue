<template>
	<div>
		<PageTitle title="管理员列表"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="管理员列表" :list="list" :key="refresh" :ischeckbox="true" ref="tablepadding">
						<div slot="btn_solt">
							<el-button size="mini" type="success" icon="el-icon-circle-plus-outline" @click="tolink('AdministratorAdd')">添加</el-button>
							<el-button @click="del" size="mini" type="danger" icon="el-icon-delete">删除</el-button>
						</div>
						<el-table-column prop="userid" label="账号" width="120"></el-table-column>
						<el-table-column prop="username" label="姓名" width="120"></el-table-column>
						<el-table-column prop="permissionname" label="职位" width="120"></el-table-column>
						<el-table-column label="操作">
							<template slot-scope="scope">
								<el-button type="text" size="mini" @click="update(scope.row.id)">编辑</el-button>
							</template>
						</el-table-column>	
				</TablePadding>	
			</div>
		</div>
	</div>
</template>

<script>
	import TablePadding from '../../components/TablePadding';
	import Vue from "vue";
	import PageTitle from "../../components/PageTitle";
	import {
		Table,
		TableColumn,
		Loading,
		Notification
	} from "element-ui";
	Vue.use(Table);
	Vue.use(TableColumn);
	Vue.use(Loading);
	Vue.prototype.$notify = Notification;
	export default {
		name: "AdministratorList",
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				data: [],
				list: [],
				refresh: false
			};
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
			update(id) {
				this.$router.push({
					name: "AdministratorUpdate",
					query: {
						id: id
					}
				})
			},
			handleSelectionChange(val) {
				this.selectlist = [];
				val.forEach(item => {
					this.selectlist.push(item.id);
				});
			},
			getdata: function() {
				this.$request.post("Api/SystemAdmin_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				});
			},

			del: function() {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: '提示',
						message: '请选择删除的数据',
						offset: 100,
						type: 'warning'
					});
				} else {
					this.$confirm('确定要删除吗?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						type: 'warning'
					}).then(() => {
						var ids =[];
					this.$refs.tablepadding.select.forEach(item => {
						ids.push(item.id);
					})
					this.$request.post("Api/SystemAdmin_Admin/Delete", {
						token_admin: this.$utils.getloc('token_admin'),
						userid_admin: this.$utils.getloc('userid_admin'),
						delete_id: ids.toString()
					}, (res) => {
						this.$notify({
							title: '提示',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						this.getdata();
					});
					}).catch(() => {
					
					});
				}
			}
		}
	}
</script>

<style scoped>
	.btn {
		color: white;
		text-decoration: none;
	}
</style>
