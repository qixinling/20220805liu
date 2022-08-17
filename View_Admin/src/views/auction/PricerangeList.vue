<template>
	<div>
		<PageTitle :title="title"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false" 
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt">
						<!-- <el-button type="success" size="mini" icon="el-icon-circle-plus-outline"
							@click="tolink('PricerangeAdd')">添加</el-button> -->
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</div>
					
					<el-table-column prop="name" label-class-name="name" label="名称"  />
					<el-table-column label="价格范围" >
						<template slot-scope="scope">
							<el-input-number size="mini" v-model="scope.row.minprice" :min="0"  :precision="2"
								@change="Update(scope.row)" :controls="false"></el-input-number> -
							<el-input-number size="mini" v-model="scope.row.maxprice" :min="0"  :precision="2"
								@change="Update(scope.row)" :controls="false"></el-input-number>
							
						</template>
					</el-table-column>
					<!-- <el-table-column label="进场信用值" >
						<template slot-scope="scope">
							<el-input-number size="mini" v-model="scope.row.minnum" :min="0"  :precision="2"
								@change="Update(scope.row)" :controls="false"></el-input-number>
						</template>
					</el-table-column> -->
					
					
				</TablePadding>
			</div>
		</div>
	</div>
</template>
<script>
	import Vue from "vue";
	import PageTitle from "../../components/PageTitle";
	import TablePadding from '../../components/TablePadding';
	import {
		Input,
		Select,
		Option,
		Notification,
		Switch,
		InputNumber,
		TimePicker
	} from "element-ui";
	Vue.use(Input);
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Switch);
	Vue.use(InputNumber);
	Vue.use(TimePicker );
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
			TablePadding
		},
		data() {
			return {
				title:"区间列表",
				refresh: false,
				list: [],
			};
		},
		created() {
			this.getdata();
		},
		methods: {
			time:function(row){
				//console.log(row);
				 //row.qdate = row.qdate.format("hh:mm:ss");
				  //console.log(row);
				  this.Update(row);
			},
			Update(row) {
				
				//console.log(JSON.stringify(row));
				let _this = this;
				_this.$request.post(
					"Api/Pricerange_Admin/Update",
					{
						token_admin: _this.$utils.getloc("token_admin"),
						userid_admin: _this.$utils.getloc("userid_admin"),
						price: JSON.stringify(row),
					},
					(res) => {
						_this.$notify({
							title: "提示",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
					}, false, false
				);
			},
			//获取数据
			getdata: function () {
				this.$request.post(
					"Api/Pricerange_Admin/List",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						this.list = res.data.data;
						this.refresh = !this.refresh;
					}
				);
			},
			Delete: function () {
				if (this.$refs.tablepadding.select.length == 0) {
					this.$notify({
						title: "提示",
						message: "请选择删除的数据",
						offset: 100,
						type: "warning",
					});
				} else {
					this.$confirm("确定要删除吗?", "提示", {
						confirmButtonText: "确定",
						cancelButtonText: "取消",
						type: "warning",
					})
						.then(() => {
							var ids =[];
							this.$refs.tablepadding.select.forEach((item) => {
								ids.push(item.id);
							});
							this.$request.post(
								"Api/Pricerange_Admin/Delete",
								{
									token_admin: this.$utils.getloc("token_admin"),
									userid_admin: this.$utils.getloc("userid_admin"),
									ids: ","+ids.toString()+",",
								},
								(res) => {
									this.$notify({
										title: "提示",
										message: res.data.msg,
										offset: 100,
										type: "success",
									});
									this.getdata();
								}
							);
						})
						.catch(() => { });
				}
			},
			getimg: function (img) {
				return this.$config.img_url + img;
			},
			tolink: function (_path) {
				this.$router.push({
					path: _path,
				});
			},
		},
	};
</script>

<style scoped="scoped">
	.routerlink {
		color: #409eff;
		text-decoration: none;
	}

	/deep/ .cell {
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
	}
</style>