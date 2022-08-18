<template>
	<div class="jiesuan">
		<PageTitle title="分红结算"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding title="分红结算" :list="list" :key="refresh" :ischeckbox="false" :isexcel="false" ref="tablepadding">
					<div slot="btn_solt">
						<el-button icon="el-icon-data-line" type="danger" size="mini" @click="jiesuan(1)">奖金结算</el-button>
					</div>
					
					<el-table-column label="结算时间" label-class-name="jdate">
						<template slot-scope="scope">{{ scope.row.jdate }}</template>
					</el-table-column>
					<el-table-column label="完成时间" label-class-name="wdate">
						<template slot-scope="scope">{{ scope.row.wdate }}</template>
					</el-table-column>
					<el-table-column label="状态">
						<template slot-scope="scope">{{ scope.row.statename }}</template>
					</el-table-column>
					<el-table-column prop="lxname" label="结算类型" label-class-name="lxname"></el-table-column>
					<el-table-column prop="userid" label="操作人" label-class-name="userid"></el-table-column>
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
		MessageBox,
		Notification
	} from 'element-ui';

	Vue.prototype.$notify = Notification;
	Vue.prototype.$confirm = MessageBox.confirm;
	export default {
		name: 'chongzhi',
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				list: [],
				multipleSelection: [],
				refresh: false
			}
		},
		created() {
			this.getdata()
		},
		methods: {
			getdata: function() {
				this.$request.post("Api/BonusJiesuan_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					this.refresh = !this.refresh;
				});
			},
			jiesuan: function(lx) {
				this.$confirm('确定要结算吗', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					this.$request.post("Api/BonusJiesuan_Admin/Jiesuan", {
						token_admin: this.$utils.getloc('token_admin'),
						userid_admin: this.$utils.getloc('userid_admin'),
						lx: lx
					}, (res) => {
						this.$notify({
							title: '提示',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						this.getdata()
					});
				})
			},
		}
	}
</script>

<style scoped="scoped">
</style>
