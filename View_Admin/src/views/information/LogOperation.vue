<template>
	<div>
		<PageTitle title="操作日志"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="操作日志" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false" :summary="false" ref="tablepadding">
					<div slot="btn_solt">
						
						<el-button v-if="id == 1" size="mini" type="danger" icon="el-icon-delete" @click="Delete">删除</el-button>
					</div>
					<el-table-column prop="ldate" label="日期" width="200" label-class-name="zdate"></el-table-column>
					<el-table-column prop="userid" label="会员编号" width="120" label-class-name="userid"></el-table-column>
					<el-table-column prop="ip" label="IP" width="120" label-class-name="ip"></el-table-column>
					<el-table-column prop="lxName" label="操作类型" width="120" label-class-name="lxname"></el-table-column>
					<el-table-column prop="bz" label="备注"></el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import TablePadding from '../../components/TablePadding';
	import Vue from "vue";
	import PageTitle from '../../components/PageTitle';
	import {
		Notification
	} from "element-ui";
Vue.prototype.$notify = Notification;
	export default {
		name: "LogOperation",
		components: {
			PageTitle,
			TablePadding,
		},
		data() {
			return {
				list: [],
				refresh: false,
				id:0
			};
		},
		created() {
			this.id = this.$utils.getloc('id_admin');
			console.log(this.$utils.getloc('id_admin'));
			this.getdata();
		},
		methods: {
			getdata: function() {  // 查询所有操作日志
				this.$request.post("Api/SystemLog_Admin/Operation_List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					id:this.$utils.getloc('id_admin')
				}, (res) => {
					this.list = res.data.data;

					this.refresh = !this.refresh;
				},true);
			},
			Delete: function() {
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
							"Api/SystemLog_Admin/Operation_Delete", {
								token_admin: _this.$utils.getloc("token_admin"),
								userid_admin: _this.$utils.getloc("userid_admin"),
								delete_id: ","+idlist.toString()+","
							},
							(res) => {
								_this.$notify({
									title: "提示",
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
		
		}
	};
</script>

<style scoped>
</style>
