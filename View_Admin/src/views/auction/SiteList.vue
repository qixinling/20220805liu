<template>
  <div>
    <PageTitle title="时间列表"></PageTitle>

    <div class="cardbox">
			<div class="cardcontent">
        <TablePadding :title="title" :list="list" :key="refresh" :ischeckbox="true" :isexcel="false" 
					ref="tablepadding">
					<!--操作按钮插槽-->
					<!-- <div slot="btn_solt">
						<el-button type="success" size="mini" icon="el-icon-circle-plus-outline"
							@click="tolink('SiteAdd')">添加场次</el-button>
						
					</div> -->
					<el-table-column label="名称" prop="name"></el-table-column>
					<!-- <el-table-column label="简介" >
						<template slot-scope="scope">
							<el-input type="text" v-model="scope.row.prompt" @change="Update(scope.row)"></el-input>
						</template>
					</el-table-column> -->
					<el-table-column label="开始时间" >
						<template slot-scope="scope">
							<el-time-picker v-model="scope.row.sdate" @change="Update(scope.row)" value-format="HH:mm:ss"></el-time-picker>
						</template>
					</el-table-column>
					<el-table-column label="结束时间">
						<template slot-scope="scope">
							<el-time-picker v-model="scope.row.edate" @change="Update(scope.row)" value-format="HH:mm:ss"></el-time-picker>
						</template>
					</el-table-column>
					<!-- <el-table-column label="预约">
						<template slot-scope="scope">
							<el-switch v-model="scope.row.isyy" :inactive-value="0" :active-value="1" @change="Update(scope.row)"></el-switch>
						</template>
					</el-table-column> -->
					<!-- <el-table-column label="最大抢购数量" >
						<template slot-scope="scope">
							<el-input type="text" v-model="scope.row.maxnum" @change="Update(scope.row)"></el-input>
						</template>
					</el-table-column> -->
					<!-- <el-table-column label="开放">
						<template slot-scope="scope">
							<el-switch v-model="scope.row.iskf" :inactive-value="0" :active-value="1" @change="Update(scope.row)"></el-switch>
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
  name: '',
  components: {
    PageTitle,
		TablePadding
  },
  data () {
    return {
      title: "场次列表",
      refresh: false,
	  list: [],
    }
  },
  created() {
		this.getdata();
	},
  methods: {
    //获取数据
		getdata: function () {
			this.$request.post(
				"Api/Site_Admin/List",
				{
					token_admin: this.$utils.getloc("token_admin"),
					userid_admin: this.$utils.getloc("userid_admin"),
				},
				(res) => {
					this.list = res.data.data;
					//console.log(res.data.data);
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
							"Api/Site_Admin/Delete",
							{
								token_admin: this.$utils.getloc("token_admin"),
								userid_admin: this.$utils.getloc("userid_admin"),
								ids:","+ ids.toString()+",",
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
    Update(row) {
			let _this = this;
			_this.$request.post(
				"Api/Site_Admin/Update",
				{
					token_admin: _this.$utils.getloc("token_admin"),
					userid_admin: _this.$utils.getloc("userid_admin"),
					site: JSON.stringify(row),
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
		tolink: function (_path) {
			this.$router.push({
				path: _path,
			});
		},
  }
}
</script>

<style scoped>
</style>