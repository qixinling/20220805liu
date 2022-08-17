<template>
	<div class="bonussource">
		<PageTitle title="奖金来源"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="奖金来源" :list="list" :key="refresh" :ischeckbox="false" :isexcel="true" :summary="true" ref="tablepadding">
					<el-table-column label="日期" label-class-name="sdate">
						<template slot-scope="scope">{{ scope.row.sdate }}</template>
					</el-table-column>
					<el-table-column prop="userid" label="会员编号" label-class-name="userid"></el-table-column>
					<el-table-column prop="bonusName" label="奖项" ></el-table-column>
					<el-table-column prop="jine" label="金额" ></el-table-column>
					<el-table-column prop="yuserid" label="来源"></el-table-column>
					<el-table-column prop="bz" label="备注"></el-table-column>
				</TablePadding>
			</div>
		</div>
		
	</div>
</template>

<script>
	import Vue from 'vue';
	import PageTitle from '../../components/PageTitle';
	import TablePadding from '../../components/TablePadding';

	export default {
		name: 'bonussource',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				multipleSelection: [],
				searchlist: [],
				refresh: false,
				list: []
			}
		},
		created() {
			this.getdata();
		},
		methods: {
			getdata: function() {
				this.$request.post("Api/Bonus_Admin/Source_List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					console.log(this.list)
					this.searchlist = this.list;
					this.refresh = !this.refresh;
				});
			},
		}
	}
</script>

<style scoped="scoped">
	.bonussource{
		
	}

</style>
