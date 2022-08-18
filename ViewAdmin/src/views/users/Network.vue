<template>
	<div class="Network">
		<PageTitle title="网络图谱" :excel="false"></PageTitle>
		<div class="cardbox">
			<el-row>
				<el-col :span="4" style="padding: 10px 0 10px 10px;">
					<el-input style="float: left;" v-model="userid" placeholder="请输入要查询的用户"></el-input>
				</el-col>
				<el-col :span="20"></el-col>
			</el-row>

			<el-row class="text-center">
				<el-col :span="6" class="text left_text">
					团队人数：
					<span class="number">{{ Teamcount }}</span>
				</el-col>
				<el-col :span="18" class="text">
					A区：
					<span class="number">{{ Area1 }}</span>
				</el-col>
			</el-row>
			<el-row class="text-center">
				<el-col :span="6" class="text left_text">
					团队业绩：
					<span class="number">{{ Teamyeji }}</span>
				</el-col>
				<el-col :span="18" class="text">
					<div v-if="xian >= 2">
						B区：
						<span class="number">{{ Area2 }}</span>
					</div>
				</el-col>
			</el-row>
			<el-row class="text-center">
				<el-col :span="6" style="padding: 5px;"></el-col>
				<el-col :span="18" class="text">
					<div v-if="xian >= 3">
						C区：
						<span class="number">{{ Area3 }}</span>
					</div>
				</el-col>
			</el-row>
			<el-row class="text-center">
				<el-col :span="6" style="padding: 5px;"></el-col>
				<el-col :span="18" class="text">
					<div v-if="xian >= 4">
						D区：
						<span class="number">{{ Area4 }}</span>
					</div>
				</el-col>
			</el-row>
			<el-row class="text-center">
				<el-col :span="6" style="padding: 5px;"></el-col>
				<el-col :span="18" class="text">
					<div v-if="xian >= 5">
						E区：
						<span class="number">{{ Area5 }}</span>
					</div>
				</el-col>
			</el-row>
			<el-row>
				<el-col :span="24" style="padding: 10px 0 20px 10px">
					<el-switch v-model="value" active-color="#409EFF" inactive-color="#909399" inactive-text="展开全部"
						@change="expandChange">
					</el-switch>
				</el-col>
			</el-row>
			<div class="scrollBox text-center">
				<vue2-org-tree name="test" :data="data" :horizontal="horizontal" :collapsable="collapsable"
					:label-class-name="labelClassName" :render-content="renderContent" @on-expand="onExpand" />
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from 'vue';
	import PageTitle from '../../components/PageTitle';
	import Vue2OrgTree from "vue2-org-tree/dist/index.js";
	import "vue2-org-tree/dist/style.css";
	import {
		Switch,
		Input
	} from 'element-ui';

	Vue.use(Vue2OrgTree);
	Vue.use(Switch);
	Vue.use(Input);
	export default {
		name: 'Network',
		components: {
			PageTitle
		},
		data() {
			return {
				value: false,
				userid: "",
				data: {},
				rawdata: {},
				Teamcount: 0,
				Teamyeji: 0,
				Area1: 0,
				Area2: 0,
				Area3: 0,
				Area4: 0,
				Area5: 0,
				xian: 5, //控制有几条线
				colspan: 8,
				horizontal: false,
				collapsable: true,
				labelClassName: "bg-white", //bg-white,bg-orange,bg-gold,bg-gray,bg-lightpink,bg-chocolate,bg-tomato
			}
		},
		created() {
			this.getdata();
			if (this.xian == 1) {
				this.colspan = 24;
			} else if (this.xian == 2) {
				this.colspan = 12;
			} else if (this.xian == 3) {
				this.colspan = 8;
			} else if (this.xian == 4) {
				this.colspan = 6;
			} else if (this.xian == 3) {
				this.colspan = 8;
			}
		},
		methods: {
			getdata: function() { // 查询用户
				var _this = this;
				this.$request.post("api/Users_Admin/Network", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin'),
					areacount: _this.xian
				}, (res) => {
					_this.rawdata = JSON.parse(res.data.data.network);
					_this.Teamcount = _this.rawdata[0].Teamcount;
					_this.Teamyeji = _this.rawdata[0].Teamyeji;
					_this.Area1 = _this.rawdata[0].Area1;
					_this.Area2 = _this.rawdata[0].Area2;
					_this.Area3 = _this.rawdata[0].Area3;
					//递归点位下方添加注册

					var pushregister = function(pdata) {
						if (pdata.Id > 0) {
							for (var i = 0; i < _this.xian; i++) {
								if (pdata.children[i]) {
									pushregister(pdata.children[i]);
								}
							}
						}
					};
					pushregister(_this.rawdata[0]);
					_this.data = _this.rawdata[0];
				});

			},
			renderContent(h, data) {
				return data.Label;
			},
			onExpand(e, data) {
				if ("expand" in data) {
					data.expand = !data.expand;
					if (!data.expand && data.children) {
						this.collapse(data.children);
					}
				} else {
					this.$set(data, "expand", true);
				}
			},
			collapse(list) {
				var _this = this;
				list.forEach(function(child) {
					if (child.expand) {
						child.expand = false;
					}
					child.children && _this.collapse(child.children);
				});
			},
			expandChange() { // 展开全部
				this.toggleExpand(this.data, this.value);
			},
			toggleExpand(data, val) { // 展开全部
				var _this = this;
				if (Array.isArray(data)) {
					data.forEach(function(item) {
						_this.$set(item, "expand", val);
						if (item.children) {
							_this.toggleExpand(item.children, val);
						}
					});
				} else {
					this.$set(data, "expand", val);
					if (data.children) {
						_this.toggleExpand(data.children, val);
					}
				}
			}
		},
		watch: {
			userid: function(val) { // 搜索框发生变换
				var _this = this;
				var queryuserid = function(qdata) {
					if (qdata.Label == val) {
						_this.data = qdata;
						_this.Teamcount = qdata.Teamcount;
						_this.Teamyeji = qdata.Teamyeji;
						_this.Area1 = qdata.Area1;
						_this.Area2 = qdata.Area2;
						_this.Area3 = qdata.Area3;
						_this.Area4 = qdata.Area4;
						_this.Area5 = qdata.Area5;
					} else {
						qdata.children.forEach(item => {
							if (item.Label == val) {
								_this.data = item;
								_this.Teamcount = item.Teamcount;
								_this.Teamyeji = item.Teamyeji;
								_this.Area1 = item.Area1;
								_this.Area2 = item.Area2;
								_this.Area3 = item.Area3;
								_this.Area4 = item.Area4;
								_this.Area5 = item.Area5;
							} else {
								if (item.Id != 0) {
									queryuserid(item);
								}
							}
						});
					}
				};
				queryuserid(_this.rawdata[0]);
			}
		}
	}
</script>

<style scoped="scoped">

	.filter-Network {
		width: 100%;
	}

	.scrollBox {
		width: 100%;
		height: 100%;
	}

	/* .text-center {
		text-align: center;
	} */

	.number {
		font-weight: 600;
		color: orangered;
		padding-top: 10px;
	}

	.text {
		padding-top: 10px;
		padding-bottom: 10px;
		color: #888;
		font-size: 14px;
	}

	.org-tree-container {
		background: none;
	}

	/deep/ .org-tree-node-label {
		background: #fff;
	}

	.org-tree-node-label {
		white-space: nowrap;
	}

	/*颜色*/
	.bg-white {
		background-color: white;
	}

	.bg-orange {
		background-color: orange;
	}

	.bg-gold {
		background-color: gold;
	}

	.bg-gray {
		background-color: gray;
	}

	.bg-lightpink {
		background-color: lightpink;
	}

	.bg-chocolate {
		background-color: chocolate;
	}

	.bg-tomato {
		background-color: tomato;
	}

	.left_text {
		padding-left: 10px;
	}
</style>
