<template>
	<div>
		<PageTitle title="异常日志"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="异常日志" :list="list" :key="refresh" :ischeckbox="false" :isexcel="false" ref="tablepadding">
					<el-table-column prop="date" label="日期" width="180"></el-table-column>
					<el-table-column prop="name" label="日志" width="500"></el-table-column>
					<el-table-column label="操作">
						<el-button type="text" size="mini">详情</el-button>
					</el-table-column>
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import TablePadding from '../../components/TablePadding';
	import Vue from "vue";
	import PageTitle from '../../components/PageTitle';

	export default {
		name: "LogAbnormity",
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				list: [],
				refresh: false,
			};
		},
		created() {
			this.getdata()
		},
		methods: {
			getdata: function() {
				this.$request.post("Api/SystemLogError_Admin/Error_List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				}, true);
			},

		}
	};
</script>

<style scoped>
</style>
