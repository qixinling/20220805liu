<template>
	<div>
		<PageTitle title="实名认证"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<TableSearch>
					<el-row class="search-row">
						<el-col class="search-col" :sm="24" :xs="24" :md="6">
							<el-date-picker v-model="date1" type="date" placeholder="开始日期">
							</el-date-picker>
						</el-col>
						<el-col class="search-col" :sm="24" :xs="24" :md="6">
							<el-date-picker v-model="date2" type="date" placeholder="结束日期">
							</el-date-picker>
						</el-col>
					</el-row>
					<el-row class="search-row">
						<el-col class="search-col" :sm="24" :xs="24" :md="6">
							<el-select v-model="mrlx" placeholder="请选择" @change="lxChange">
								<el-option value="1" label="会员编号"></el-option>
								<el-option value="2" label="姓名"></el-option>
							</el-select>
						</el-col>
						<el-col class="search-col" :sm="24" :xs="24" :md="6">
							<el-input v-model="input" placeholder="请输入查询内容"></el-input>
						</el-col>
					</el-row>
					<el-row class="search-row">
						<el-col class="search-col" :sm="24" :xs="24" :md="24">
							<el-button icon="el-icon-search" type="primary" size="mini" @click="search(1)">查询</el-button>
							<el-button icon="el-icon-refresh" type="primary" size="mini" @click="search(2)">重置</el-button>
						</el-col>
					</el-row>
				</TableSearch>

				<el-row style="margin-top: 10px;margin-bottom: 10px;">
					<el-col :md="24" :sm="24" :span="24">
						<el-button icon="el-icon-unlock" type="success" size="mini" @click="Shenhe">通过认证</el-button>
						<el-button icon="el-icon-lock" type="warning" size="mini" @click="Nopass">不通过</el-button>
						<el-button icon="el-icon-delete" type="danger" size="mini" @click="Delete">删除</el-button>
					</el-col>
				</el-row>
				<TablePadding :list="searchlist" :key="issearch" :ischeckbox="true" ref="tablepadding">
					<el-table-column prop="userid" label="会员编号"></el-table-column>
					<el-table-column prop="username" label="姓名"></el-table-column>
					<el-table-column prop="usercode" label="身份证号"></el-table-column>
					<el-table-column label="申请时间">
						<template slot-scope="scope">{{ scope.row.sdate }}</template>
					</el-table-column>
					<!-- <el-table-column label="正面照">
					  <template slot-scope="scope">
						  <a v-if="scope.row.zimg" :href="getimg(scope.row.zimg)" target="_blank">
						    <el-button type="text" size="small">查看</el-button>
						  </a>
					  </template>
					</el-table-column>
					<el-table-column label="反面照">
					  <template slot-scope="scope">
						  <a v-if="scope.row.fimg" :href="getimg(scope.row.fimg)" target="_blank">
						    <el-button type="text" size="small">查看</el-button>
						  </a>
					  </template>
					</el-table-column> -->
					<el-table-column label="状态">
						<template slot-scope="scope">
							<div v-if="scope.row.state==0" style="color:red;">未审核</div>
							<div v-else-if="scope.row.state==1" style="color:#67C23A;">已审核</div>
							<div v-else-if="scope.row.state==2" style="color:#E6A23C;">已撤销</div>
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
	import TableSearch from '../../components/TableSearch';
	import {
		Table,
		TableColumn,
		Checkbox,
		CheckboxButton,
		CheckboxGroup,
		DatePicker,
		Select,
		Input,
		Notification
	} from 'element-ui';
	Vue.use(Table);
	Vue.use(TableColumn);
	Vue.use(Checkbox);
	Vue.use(CheckboxButton);
	Vue.use(CheckboxGroup);
	Vue.use(DatePicker);
	Vue.use(Select);
	Vue.use(Input);

	Vue.prototype.$notify = Notification;
	export default {
		name: 'user',
		components: {
			PageTitle,
			TablePadding,
			TableSearch
		},
		data() {
			return {
				input: "",
				lx: 1,
				mrlx: '会员编号',
				date1: '',
				date2: '',
				list: [],
				searchlist: [],
				issearch: false
			}
		},
		created() {
			this.getdata();
		},
		methods: {
			getimg: function(img) {
				if (img != "" && img != null) {
					return this.$config.img_url + img;
				}
			},
			Shenhe: function() { // 审核会员
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要操作的数据',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				var idlist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idlist.push(item.id);
				})

				_this.$confirm('确定要通过认证吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Renzheng_Admin/Shenhe", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						ids:","+ idlist.toString()+","
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.getdata();
					});
				}).catch(() => {

				});
			},
			Nopass: function() { // 审核不通过
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要操作的数据',
						offset: 100,
						type: 'warning'
					});
					return;
				}
			
				var idlist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idlist.push(item.id);
				})
			
				_this.$confirm('确定要执行此操作吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Renzheng_Admin/Nopass", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						ids: ","+idlist.toString()+","
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.getdata();
					});
				}).catch(() => {
			
				});
			},
			
			Delete: function() {
				var _this = this;
				if (_this.$refs.tablepadding.select.length == 0) {
					Notification({
						message: '请选择要删除的数据',
						offset: 100,
						type: 'warning'
					});
					return;
				}

				var idlist = [];
				_this.$refs.tablepadding.select.forEach(item => {
					idlist.push(item.id);
				})

				_this.$confirm('确定要删除吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Renzheng_Admin/Delete", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						ids: ","+idlist.toString()+","
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.getdata();
					});
				}).catch(() => {

				});
			},
			
			getdata: function() { // 查询用户
				var _this = this;
				_this.$request.post("Api/Renzheng_Admin/List", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin')
				}, (res) => {
					this.list = res.data.data;
					//console.log(res.data);
					this.searchlist = this.list;
					this.issearch = !this.issearch;
				});
			},
			lxChange: function(e) {
				this.lx = e;
			},
			search: function(lx) { // 查询
				let _list = this.list;
				if (lx == 1) { //筛选
					//日期筛选
					if (this.date1 && this.date2) {
						let start = this.date1.format('yyyy-MM-dd');
						let end = this.date2.format('yyyy-MM-dd') + ' 23:59:59';
						_list = this.list.filter(item => item.sdate >= start && item.sdate <= end);
					}

					// //类型筛选
					if (this.ulname != '全部') {
						_list = this.list;
					}

					if (this.input) {
						if (this.lx == 1) { // 会员编号
							_list = this.list.filter(item => item.userid == this.input);
						} else if (this.lx == 2) { // 姓名
							_list = this.list.filter(item => item.username == this.input);
						}
					}
				}else if(lx == 2){
					this.date1 = "";
					this.date2 = "";
					this.input = "";
				}
				this.searchlist = _list;
				this.issearch = !this.issearch;
			},
		}
	}
</script>

<style scoped="scoped">
	.routerlink {
		color: #409EFF;
		text-decoration: none;
	}
</style>
