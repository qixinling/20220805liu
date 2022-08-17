<template>
	<div class="bill">
		<van-sticky>
			<van-nav-bar :title="title" left-arrow @click-left="onClickLeft"
				style="background: transparent; color: #641602" :border="false" :fixed="true" />
			<div class="top">
				<van-row>
					<van-col span="12">
						<div class="jine">{{ srjine }}</div>
						<div class="text">收入</div>
					</van-col>
					<van-col span="12">
						<div class="jine">{{ zcjine }}</div>
						<div class="text">支出</div>
					</van-col>
				</van-row>
				<van-dropdown-menu>
					<van-dropdown-item :title="year + '年 ' + month + '月'" ref="dateitem">
						<van-picker show-toolbar :columns="columns" @confirm="onConfirm" @cancel="onCancel" />
					</van-dropdown-item>
					<van-dropdown-item title="筛选" ref="item">
						<van-switch-cell v-model="all" title="全部" />
						<van-switch-cell v-model="sw.sr" title="收入" />
						<van-switch-cell v-model="sw.zc" title="支出" />
						<van-switch-cell v-model="sw.s0" title="收益" />
						<van-switch-cell v-model="sw.s1" title="充值" />
						<van-switch-cell v-model="sw.s2" title="提现" />
						<van-switch-cell v-model="sw.s3" title="转账" />
						<van-switch-cell v-model="sw.s4" title="转换" />
						<van-switch-cell v-model="sw.s5" title="增减" />
						<van-switch-cell v-model="sw.s6" title="消费" />
						
					</van-dropdown-item>
				</van-dropdown-menu>
			</div>
		</van-sticky>
		<div class="cardBox">
			<div class="cardContent">
				<FlowLoad :datalist="list" :key="flowload">
					<template v-slot:default="slotProps">
						<div class="van-hairline--bottom" style="padding: 10px">
							<van-row  style="display: flex;align-items: center;">
								<van-col span="24">
									<van-row>
										<van-col span="12" style=" font-size: 16px;font-weight: 600;color: #555;">
											{{ slotProps.item.bname }}
										</van-col>
										<van-col span="12" style="text-align: right;color: #777;font-size: 12px;">
										{{$utils.timestampFormat(slotProps.item.bdate)}}</van-col>
									</van-row>
									<!-- <van-col span="24" style="font-size: 12px;padding-top: 5px;color: #888;"></van-col> -->
								</van-col>
							</van-row>
							
								<van-row style="padding-top: 5px;" >
									<van-col span="12" style="font-size: 12px;padding-top: 5px;color: #888;">{{ slotProps.item.bz }}</van-col>
									<van-col span="12" >
									 <van-row>
										 <van-col span="24" v-for="item2 in slotProps.item.dbBillAmount" :key="item2.id" style="text-align: right;font-size: 10px;color: #888;line-height: 22px;">
											 {{item2.cname}}
											  <span style="color: red;padding-left: 5px;"><span v-if="item2.amount>0">+</span>{{item2.amount}}</span>
										 </van-col>
									 </van-row>
									
									</van-col>
									<!-- <van-col span="3" style="text-align: right;color: red;font-size: 10px;">{{item2.amount}}</van-col> -->
								</van-row>
							
							
						</div>
					</template>
				</FlowLoad>
			</div>
		</div>
		<Login></Login>
	</div>
</template>

<script>
	import FlowLoad from "@/components/FlowLoad";
	import Login from "@/components/Login";
	import Vue from "vue";
	import {
		Picker
	} from "vant";
	import {
		Popup
	} from "vant";
	import {
		Sticky
	} from "vant";
	import {
		NavBar
	} from "vant";
	import {
		DropdownMenu,
		DropdownItem
	} from "vant";
	import {
		SwitchCell
	} from "vant";

	Vue.use(SwitchCell);
	Vue.use(DropdownMenu);
	Vue.use(DropdownItem);
	Vue.use(NavBar);
	Vue.use(Sticky);
	Vue.use(Popup);
	Vue.use(Picker);
	export default {
		name: "bill",
		components: {
			FlowLoad,
			Login,
		},
		data() {
			return {
				title: document.title,
				flowload: true,
				srjine: 0,
				zcjine: 0,
				year: 2020,
				month: 8,
				all: true,
				sw: {
					sr: true,
					zc: true,
					s0: true,
					s1: true,
					s2: true,
					s3: true,
					s4: true,
					s5: true,
					s6: true,
					s7: true,
					s8: true,
					s9: true,
					s10: true,
				},
				columns: [
					// 第一列
					{
						values: [],
						defaultIndex: 0,
					},
					// 第二列
					{
						values: [
							"1",
							"2",
							"3",
							"4",
							"5",
							"6",
							"7",
							"8",
							"9",
							"10",
							"11",
							"12",
						],
						defaultIndex: 1,
					},
				],
				baselist: [],
				list: [],
			};
		},
		created() {
			var myDate = new Date();
			for (var i = 2018; i <= myDate.getFullYear(); i++) {
				this.columns[0].values.push(i);
			}
			this.columns[0].defaultIndex = this.columns[0].values.length - 1;
			this.columns[1].defaultIndex = myDate.getMonth();

			this.year = this.columns[0].values[this.columns[0].defaultIndex];
			this.month = this.columns[1].values[this.columns[1].defaultIndex];

			this.load();
		},
		methods: {
			load: function() {
				if (this.$store.state.LoginState) {
					//子组件调用加载
					 this.getdata();
				}
			},
			xiangqing: function(id) {
				this.$router.push({
					name: "BillContent",
					query: {
						id: id,
					},
				});
			},
			getdata: function() {
				let _this = this;
				_this.$request.post(
					"api/Bill/List", {
						token: _this.$utils.getloc("token"),
						userid: _this.$utils.getloc("userid"),
						id: _this.$utils.getloc("id"),
						year: _this.year,
						month: _this.month,
					},
					(res) => {
						if (res.data.code == 0) {
							_this.$dialog.alert({
								title: "提示",
								message: res.data.msg,
							});
							return;
						}
						console.log(res.data.data);
						_this.baselist = res.data.data;
						_this.filter();
					}
				);
			},
			filter: function() {
				let _list = [];
				if (this.sw.s0) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 0)
					);
				}
				if (this.sw.s1) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 1)
					);
				}
				if (this.sw.s2) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 2)
					);
				}
				if (this.sw.s3) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 3)
					);
				}
				if (this.sw.s4) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 4)
					);
				}
				if (this.sw.s5) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 5)
					);
				}
				if (this.sw.s6) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 6)
					);
				}
				if (this.sw.s7) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 7)
					);
				}
				if (this.sw.s8) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 8)
					);
				}
				if (this.sw.s9) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 9)
					);
				}
				if (this.sw.s10) {
					_list = _list.concat(
						this.baselist.filter((item) => item.blx == 10)
					);
				}

				if (this.sw.sr && !this.sw.zc) {
					_list = this.baselist.filter((item) => item.dbBillAmount.filter((item2) =>item2.amount > 0) );
				} else if (!this.sw.sr && this.sw.zc) {
					_list = this.baselist.filter((item) => item.dbBillAmount.filter((item2) =>item2.amount < 0) );

				}
				_list.sort(function(a, b) {
					return b.id - a.id;
				});

				let _srjine = 0;
				let _zcjine = 0;

				_list.forEach((item) => {
					item.dbBillAmount.forEach((item2)=>{
						if (item2.amount > 0) {
							_srjine += parseFloat(item2.amount);
						} else if (item2.amount < 1) {
							_zcjine += parseFloat(item2.amount);
						}
					})
					
				});

				this.srjine = _srjine.toFixed(2);
				this.zcjine = _zcjine.toFixed(2);

				this.list = _list;
				this.flowload = !this.flowload;
			},
			onClickLeft() {
				this.$router.back(-1);
			},
			onConfirm(value, index) {
				this.year = value[0];
				this.month = value[1];
				this.columns[0].defaultIndex = index[0];
				this.columns[1].defaultIndex = index[1];

				this.getdata();

				this.$refs.dateitem.toggle(); //关闭下拉菜单
			},
			onCancel() {
				this.$refs.dateitem.toggle(); //关闭下拉菜单
			},
		},
		watch: {
			all: {
				handler: function() {
					let _sw = this.sw;
					_sw.sr = this.all;
					_sw.zc = this.all;
					_sw.s0 = this.all;
					_sw.s1 = this.all;
					_sw.s2 = this.all;
					_sw.s3 = this.all;
					_sw.s4 = this.all;
					_sw.s5 = this.all;
					_sw.s6 = this.all;
					_sw.s7 = this.all;
					_sw.s8 = this.all;
					_sw.s9 = this.all;
					_sw.s10 = this.all;
					this.sw = _sw;
				},
			},
			sw: {
				handler: function() {
					this.filter();
				},
				deep: true,
			},
		},
	};
</script>

<style scoped>
	/deep/.van-switch--on {
		background-color: #F7B226;
	}

	.operationtype {
		width: 40px;
		height: 40px;
		border-radius: 40px;
		text-align: center;
		display: table-cell;
		vertical-align: middle;
		font-weight: 500;
	}

	.ot0 {
		background: palevioletred;
		color: #ffffff;
	}

	.ot1 {
		background: #37a2da;
		color: #ffffff;
	}

	.ot2 {
		background: #ff9f7f;
		color: #ffffff;
	}

	.ot3 {
		background: #67e0e3;
		color: #ffffff;
	}

	.ot4 {
		background: #ffdb5c;
		color: #ffffff;
	}

	.ot5 {
		background: #32c5e9;
		color: #ffffff;
	}

	.ot6 {
		background: #37a2da;
		color: #ffffff;
	}

	.ot7 {
		background: #a7a2da;
		color: #ffffff;
	}

	.ot8 {
		background: #ff9f7f;
		color: #ffffff;
	}

	/deep/.van-nav-bar .van-icon {
		color: #641602;
	}

	/deep/.van-nav-bar__title {
		color: #641602;
	}

	.top {
		padding-top: 50px;
		background: url("../../assets/img/qianbao.png");
		background-size: 100% 100%;
		color: rgb(100, 22, 2);
	}

	.jine {
		font-size: 18px;
		color: #641602;
		text-align: center;
		font-weight: 600;
	}

	.text {
		font-size: 12px;
		color: #641602;
		text-align: center;
		margin-top: 5px;
	}

	/deep/.van-dropdown-menu__bar {
		background: transparent;
	}

	/deep/.van-dropdown-menu__title {
		color: #641602;
	}

	/deep/ .cardContent [class*=van-hairline]::after {
		border-bottom: 1px solid rgba(122, 122, 122, 0.6);
	}

	/deep/ .van-dropdown-menu__title::after {
		border-color: transparent transparent #641602 #641602
	}
</style>
