<template>
	<div class="bonus">
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="false" :isexcel="true"
					:summary="true" ref="tablepadding" v-if="bonus.length">
					<div slot="btn_solt">
						<el-button icon="el-icon-back" type="danger" size="mini" @click="tolink('BonusTime')">返回
						</el-button>
					</div>
					<el-table-column prop="userid" label="会员编号" label-class-name="userid"></el-table-column>
					<el-table-column v-if="bonus[1].isDisplay" prop="jine[1]" :label="bonus[1].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[2].isDisplay" prop="jine[2]" :label="bonus[2].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[3].isDisplay" prop="jine[3]" :label="bonus[3].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[4].isDisplay" prop="jine[4]" :label="bonus[4].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[5].isDisplay" prop="jine[5]" :label="bonus[5].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[6].isDisplay" prop="jine[6]" :label="bonus[6].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[7].isDisplay" prop="jine[7]" :label="bonus[7].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[8].isDisplay" prop="jine[8]" :label="bonus[8].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[9].isDisplay" prop="jine[9]" :label="bonus[9].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[10].isDisplay" prop="jine[10]" :label="bonus[10].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[11].isDisplay" prop="jine[11]" :label="bonus[11].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[12].isDisplay" prop="jine[12]" :label="bonus[12].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[13].isDisplay" prop="jine[13]" :label="bonus[13].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[14].isDisplay" prop="jine[14]" :label="bonus[14].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[15].isDisplay" prop="jine[15]" :label="bonus[15].bonusName">
					</el-table-column>
					<el-table-column v-if="bonus[0].isDisplay" prop="jine[0]" :label="bonus[0].bonusName">
					</el-table-column>
				</TablePadding>
			</div>
		</div>

	</div>
</template>

<script>
	import TablePadding from '../../components/TablePadding';
	import PageTitle from '../../components/PageTitle';
	import Vue from 'vue';
	export default {
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				title: '',
				bid: '',
				bonus: [],
				refresh: false,
				list: []
			}
		},
		created() {
			this.bid = this.$route.query.id;
			this.title = this.$route.query.date + ' 奖金记录';
			this.getdata();
			this.getname()
		},
		methods: {
			tolink: function(_path) {
				this.$router.push({
					path: _path
				})
			},
			getname: function() {
				this.$request.post("Api/Bonus_Admin/Name", {},
					(res) => {
						this.bonus = res.data.data;
					}, true);
			},
			getdata: function() {
				this.$request.post("Api/Bonus_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					id: this.bid
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				});
			},
		}
	}
</script>

<style scoped="scoped">
	.bonus {}

	.fhbtn {
		color: white;
		text-decoration: none;
	}
</style>
