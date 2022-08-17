<template>
	<div class="loglogin">
		<PageTitle title="登录日志"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="登录日志" :list="list" :key="refresh" :ischeckbox="false" :isexcel="false" :summary="false" ref="tablepadding">
					<el-table-column prop="userid" label="管理员账号" label-class-name="userid"></el-table-column>
					<el-table-column prop="username" label="管理员姓名" label-class-name="username"></el-table-column>
					<el-table-column prop="ip" label="IP地址" label-class-name="ip"></el-table-column>
					<el-table-column label="登录时间" label-class-name="ldate">
						<template slot-scope="scope">{{ scope.row.ldate }}</template>
					</el-table-column>
				</TablePadding>
			</div>
		</div>

	</div>
</template>

<script>
	import TablePadding from '../../components/TablePadding';
	import Vue from 'vue';
	import PageTitle from '../../components/PageTitle';
	export default {
		name: 'loglogin',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				list: [],
				refresh: false,
			}
		},
		created() {
			this.getdata()
		},
		methods: {
			getdata: function() {
				this.$request.post("Api/SystemLog_Admin/Login_List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				},true);
			},
		}
	}
</script>

<style scoped="scoped">
</style>
