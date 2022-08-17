<template>
	<div class="achievement">
		<PageTitle title="业绩统计"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="业绩统计" :list="list" :key="refresh" :ischeckbox="false" :isexcel="false" :summary="true" ref="tablepadding">
					<el-table-column label="日期" width="200" label-class-name="Adate">
						<template slot-scope="scope">{{ scope.row.Adate }}</template>
					</el-table-column>
					<el-table-column prop="Userscount" label="新增用户"></el-table-column>
					<!-- <el-table-column prop="Usersjine" label="新增业绩"></el-table-column> -->
					<el-table-column prop="Bonus" label="奖金发放"></el-table-column>
					<!-- <el-table-column prop="Gouwucount" label="产品销量"></el-table-column>
					<el-table-column prop="Gouwujine" label="销售金额"></el-table-column> -->
					<el-table-column prop="Chongzhijine" label="充值金额"></el-table-column>
					<el-table-column prop="Tixianjine" label="提现金额"></el-table-column>
					<el-table-column prop="Shangjiajine" label="上架费金额"></el-table-column>
					<el-table-column prop="Xyzjine" label="信用值余额"></el-table-column>
					<el-table-column prop="Zhuanjine" label="消费券转换为信用值总额"></el-table-column>
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
		name: 'achievement',
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
			this.getdata();
		},
		methods: {
			getdata: function() {  // 查询所有业绩
				this.$request.post("Api/SystemAchievement_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = JSON.parse(res.data.data[0].alist);
					console.log(this.list);
					this.refresh = !this.refresh;
				}, true);
			}
		}
	}
</script>

<style scoped="scoped">
	.achievement {
		
	}
</style>
