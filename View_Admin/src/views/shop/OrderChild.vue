<template>
	
		
	<div>
		
		<PageTitle title="订单列表" :btn="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-row>
					<el-col :md="6" :sm="24" style="margin-bottom: 10px;" >
						<el-button icon="el-icon-back" size="mini" @click="tolink('OrderList')">返回</el-button>
					</el-col>
					
				</el-row>
				<template>
					<TablePadding :list="list" :key="refresh" :ischeckbox="false" ref="tablepadding" >
						
						<el-table-column prop="userid" label="会员编号" width="120">
						</el-table-column>
						<el-table-column prop="goodsimg" label="图片" show-overflow-tooltip>
							<template slot-scope="scope">
								<img :src="scope.row.goodsimg" min-width="70" height="70" />
							</template>
						</el-table-column>
						<el-table-column prop="goodsname" label="商品名称" show-overflow-tooltip>
						</el-table-column>
						<el-table-column prop="num" label="数量" show-overflow-tooltip>
						</el-table-column>
						<el-table-column prop="sjine" label="金额" show-overflow-tooltip></el-table-column>	
					</TablePadding>	
				</template>
			</div>
		</div>
		
	</div>
</template>

<script>
	import TablePadding from '../../components/TablePadding';
	import PageTitle from '../../components/PageTitle';
	import Vue from 'vue';
	import {
		Table,
		TableColumn,
		Checkbox,
		CheckboxButton,
		CheckboxGroup,
		DatePicker,
		Select,
		Input
	} from 'element-ui';
	Vue.use(Table);
	Vue.use(TableColumn);
	Vue.use(Checkbox);
	Vue.use(CheckboxButton);
	Vue.use(CheckboxGroup);
	Vue.use(DatePicker);
	Vue.use(Select);
	Vue.use(Input);
	export default {
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				oid: '',
				searchlist: [],
				refresh: false,
				list: [],
				value1: '',
			}
		},
		created() {
			this.oid = this.$route.query.id;
			this.getdata();
		},
		methods: {
			tolink: function (_path) {
				this.$router.push({
					path: _path
				})
			},
			getdata: function() {
				this.$request.post("Api/ShopOrder_Admin/Child_List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					oc_id: this.oid
				}, (res) => {
					var list = res.data.data;
					list.forEach(item => {
						item.goodsimg = item.goodsimg.split(",")[0];
					})
					this.list = list;
					this.searchlist = this.list;
					this.refresh = !this.refresh;
				});
			},
			getimg: function(img) {
				return this.$config.img_url + img;
			},
			
		}
	}
</script>

<style scoped="scoped">
	.jihuo{
		
	}
</style>
