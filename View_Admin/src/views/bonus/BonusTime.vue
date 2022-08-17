<template>
	<div class="bonustime">
		<PageTitle title="奖金总表"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding v-if="bonus.length" title="奖金总表" :list="list" :key="refresh" :ischeckbox="false" :isexcel="false" :summary="true" ref="tablepadding">
					<el-table-column label="日期" label-class-name="btdate">
						<template slot-scope="scope">{{ scope.row.btdate }}</template>
					</el-table-column>
					<el-table-column v-if="bonus[1].isDisplay" prop="b1" :label="bonus[1].bonusName"></el-table-column>
					<el-table-column v-if="bonus[2].isDisplay" prop="b2" :label="bonus[2].bonusName"></el-table-column>
					<el-table-column v-if="bonus[3].isDisplay" prop="b3" :label="bonus[3].bonusName"></el-table-column>
					<el-table-column v-if="bonus[4].isDisplay" prop="b4" :label="bonus[4].bonusName"></el-table-column>
					<el-table-column v-if="bonus[5].isDisplay" prop="b5" :label="bonus[5].bonusName"></el-table-column>
					<el-table-column v-if="bonus[6].isDisplay" prop="b6" :label="bonus[6].bonusName"></el-table-column>
					<el-table-column v-if="bonus[7].isDisplay" prop="b7" :label="bonus[7].bonusName"></el-table-column>
					<el-table-column v-if="bonus[8].isDisplay" prop="b8" :label="bonus[8].bonusName"></el-table-column>
					<el-table-column v-if="bonus[9].isDisplay" prop="b9" :label="bonus[9].bonusName"></el-table-column>
					<el-table-column v-if="bonus[10].isDisplay" prop="b10" :label="bonus[10].bonusName"></el-table-column>
					<el-table-column v-if="bonus[11].isDisplay" prop="b11" :label="bonus[11].bonusName"></el-table-column>
					<el-table-column v-if="bonus[12].isDisplay" prop="b12" :label="bonus[12].bonusName"></el-table-column>
					<el-table-column v-if="bonus[13].isDisplay" prop="b13" :label="bonus[13].bonusName"></el-table-column>
					<el-table-column v-if="bonus[14].isDisplay" prop="b14" :label="bonus[14].bonusName"></el-table-column>
					<el-table-column v-if="bonus[15].isDisplay" prop="b15" :label="bonus[15].bonusName"></el-table-column>
					<el-table-column v-if="bonus[0].isDisplay" prop="b0" :label="bonus[0].bonusName"></el-table-column>
					<el-table-column label="操作" width="100">
						<template slot-scope="scope">
							<el-button @click="details(scope.row.id,scope.row.btdate)" type="text" size="small">查看详情</el-button>
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
	export default {
		name: 'bonustime',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				bonus: [],
				list: [],
				refresh: false,
			}
		},
		created() {
			this.getname();
			this.getdata()
		},
		methods: {
			details: function(id,date) {
				this.$router.push({
					name: 'Bonus',
					query: {
						id: id,
						date:date
					}
				})
			},
			getname: function() {
				this.$request.post("Api/Bonus_Admin/Name", {},
					(res) => {
						this.bonus = res.data.data;
					}, true);
			},
			getdata: function() {
				this.$request.post("Api/Bonus_Admin/Time_List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				});
			},
		}
	}
</script>

<style scoped="scoped">
	.bonustime {
		
	}
</style>
