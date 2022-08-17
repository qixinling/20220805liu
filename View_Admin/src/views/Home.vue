<template>
	<div>
		<el-row>
			<el-col :sm="24" :md="16" style="margin-bottom: 10px;">
				<el-row :gutter="20">
					<el-col :sm="24" :md="8">
						<div class="cardbox nopadding">
							<div class="title bg1">
								<div class="bg-overlay"></div>
								<div class="titletext">
									用户
								</div>
							</div>
							<div class="cardcontent">
								<el-row style="line-height: 30px;border-bottom: 1px #eeeeee solid;margin-bottom: 5px;">
									<el-col :span="12"><span style="font-size: 12px;">总计</span></el-col>
									<el-col :span="12" style="text-align: right;">
										<span class="zongji">{{data.userscount}}</span>
									</el-col>
								</el-row>
								<el-row>
									<el-col :span="12"><span style="font-size: 12px;">+ {{data.zuouserscount}}</span></el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">+ {{data.jinusercount}}</span></el-col>
								</el-row>
								<el-row>
									<el-col :span="12"><span style="font-size: 12px;">昨日</span></el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">本日</span></el-col>
								</el-row>
							</div>
						</div>
					</el-col>

					<el-col :sm="24" :md="8">
						<div class="cardbox nopadding">
							<div class="title bg2">
								<div class="bg-overlay"></div>
								<div class="titletext">
									业绩
								</div>
							</div>
							<div class="cardcontent">
								<el-row style="line-height: 30px;border-bottom: 1px #eeeeee solid;margin-bottom: 5px;">
									<el-col :span="12"><span style="font-size: 12px;">总计</span></el-col>
									<el-col :span="12" style="text-align: right;"><span class="zongji">{{data.usersjine}}</span></el-col>
								</el-row>
								<el-row>
									<el-col :span="12"><span style="font-size: 12px;">+ {{data.zuousersjine}}</span></el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">+ {{data.jinuserjine}}</span></el-col>
								</el-row>
								<el-row>
									<el-col :span="12"><span style="font-size: 12px;">昨日</span></el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">本日</span></el-col>
								</el-row>
							</div>
						</div>
					</el-col>

					<el-col :sm="24" :md="8">
						<div class="cardbox nopadding">
							<div class="title bg3">
								<div class="bg-overlay"></div>
								<div class="titletext">
									奖金
								</div>
							</div>
							<div class="cardcontent">
								<el-row style="line-height: 30px;border-bottom: 1px #eeeeee solid;margin-bottom: 5px;">
									<el-col :span="12"><span style="font-size: 12px;">总计</span></el-col>
									<el-col :span="12" style="text-align: right;"><span class="zongji">{{data.bonus}}</span></el-col>
								</el-row>
								<el-row>
									<el-col :span="12"><span style="font-size: 12px;">+ {{data.zuobonus}}</span></el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">+ {{data.jinbonus}}</span></el-col>
								</el-row>
								<el-row>
									<el-col :span="12"><span style="font-size: 12px;">昨日</span></el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">本日</span></el-col>
								</el-row>
							</div>
						</div>
					</el-col>



					<el-col :sm="24" :md="8">
						<div class="cardbox nopadding">
							<div class="title bg4">
								<div class="bg-overlay"></div>
								<div class="titletext">
									充值
								</div>
							</div>
							<div class="cardcontent">
								<el-row style="line-height: 30px;border-bottom: 1px #eeeeee solid;margin-bottom: 5px;">
									<el-col :span="12"><span style="font-size: 12px;">总计</span></el-col>
									<el-col :span="12" style="text-align: right;">
										<span class="zongji">{{data.chongzhijine}}</span>
									</el-col>
								</el-row>
								<el-row>
									<el-col :span="12"><span style="font-size: 12px;color: red;">{{data.dsczjine}}</span></el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">{{data.ysczjine}}</span></el-col>
								</el-row>
								<el-row>
									<el-col :span="12">
										<router-link to="Chongzhi" style="font-size: 12px;color: red;">待审核</router-link>
									</el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">已审核</span></el-col>
								</el-row>
							</div>
						</div>
					</el-col>

					<el-col :sm="24" :md="8">
						<div class="cardbox nopadding">
							<div class="title  bg5">
								<div class="bg-overlay"></div>
								<div class="titletext">
									提现
								</div>
							</div>
							<div class="cardcontent">
								<el-row style="line-height: 30px;border-bottom: 1px #eeeeee solid;margin-bottom: 5px;">
									<el-col :span="12"><span style="font-size: 12px;">总计</span></el-col>
									<el-col :span="12" style="text-align: right;"><span class="zongji">{{data.tlistjine}}</span></el-col>
								</el-row>
								<el-row>
									<el-col :span="12"><span style="font-size: 12px;color: red;">{{data.dtxzjine}}</span></el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">{{data.ytxzjine}}</span></el-col>
								</el-row>
								<el-row>
									<el-col :span="12">
										<router-link to="Tixian" style="font-size: 12px;color: red;">待发放</router-link>
									</el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">已发放</span></el-col>
								</el-row>
							</div>
						</div>
					</el-col>

					<el-col :sm="24" :md="8">
						<div class="cardbox nopadding">
							<div class="title bg6">
								<div class="bg-overlay"></div>
								<div class="titletext">
									订单
								</div>
							</div>
							<div class="cardcontent">
								<el-row style="line-height: 30px;border-bottom: 1px #eeeeee solid;margin-bottom: 5px;">
									<el-col :span="12"><span style="font-size: 12px;">总计</span></el-col>
									<el-col :span="12" style="text-align: right;"><span class="zongji">{{data.ordercount}}</span></el-col>
								</el-row>
								<el-row>
									<el-col :span="12"><span style="font-size: 12px;color: red;">{{data.dfordercount}}</span></el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">{{data.yfordercount}}</span></el-col>
									<el-col :span="12">
										<router-link to="OrderList" style="font-size: 12px;color: red;">待发货</router-link>
									</el-col>
									<el-col :span="12" style="text-align: right;"><span style="font-size: 12px;">已发货</span></el-col>
								</el-row>
							</div>
						</div>
					</el-col>
				</el-row>

				<el-row :gutter="20">
					<el-col :sx="24" :md="16">
						<div class="cardbox" style="margin-top:10px;">
							<div class="cardcontent">
								<el-tabs v-model="activeName" @tab-click="handleClick">
									<el-tab-pane label="用户" name="1"></el-tab-pane>
									<el-tab-pane label="业绩" name="2"></el-tab-pane>
									<el-tab-pane label="奖金" name="3"></el-tab-pane>
									<el-tab-pane label="充值" name="4"></el-tab-pane>
									<el-tab-pane label="提现" name="5"></el-tab-pane>
									<el-tab-pane label="订单" name="6"></el-tab-pane>
								</el-tabs>
								<div id="main" style="width: 100%;height:300px; margin-top: 20px;"></div>
							</div>
						</div>
					</el-col>
					<el-col :sx="24" :md="8" style="padding: 10px;">
						<el-card class="box-card">
							<div slot="header" class="clearfix">
								<span>登录日志</span>
								<el-button style="float: right; padding: 3px 0" type="text" @click="tolog">查看更多</el-button>
							</div>
							<div style="font-size: 14px;color: #333;" class="text item">
								<el-row v-for="(item,index) in data.loglist" :key="index" style="line-height: 37px;">
									<el-col :span="6">{{item.Userid}}</el-col>
									<el-col :span="5" style="text-align: center;font-size: 12px;">登录于</el-col>
									<el-col :span="13" style="text-align: right;font-size: 12px;color: #909399;">{{$utils.timestampFormat(item.Ldate)}}</el-col>
								</el-row>
							</div>
						</el-card>

					</el-col>
				</el-row>

			</el-col>
			<el-col :sm="24" :md="8" style="margin-bottom: 10px;">
				<div class="block">
					<el-timeline>
						<div v-for="(item,index) in data.logplist" :key="index">
							<el-timeline-item :timestamp="item.lxname + ' 操作'" placement="top">
								<el-card>
									<h4>{{item.bz}}</h4>
									<p style="font-style:oblique;">{{item.userid}} 提交于 <span style="font-size: 12px;color: #909399;">{{$utils.timestampFormat(item.zdate)}} </span></p>
								</el-card>
							</el-timeline-item>
						</div>
					</el-timeline>
				</div>
			</el-col>
		</el-row>
	</div>
</template>

<script>
	var echarts = require("echarts");
	import Vue from 'vue';
	import {
		Table,
		TableColumn,
		Divider,
		Timeline,
		TimelineItem,
		Card,
		Tabs,
		TabPane
	} from 'element-ui';

	Vue.use(Tabs);
	Vue.use(TabPane);
	Vue.use(Card);
	Vue.use(Timeline);
	Vue.use(TimelineItem);
	Vue.use(Divider);
	Vue.use(Table);
	Vue.use(TableColumn);
	export default {
		data() {
			return {
				activeName:'1',
				data: []
			}
		},
		mounted() {
			this.getdata();
		},
		methods: {
			tolog:function(){
				this.$router.push('/LogLogin')
			},
			getdata: function() {
				this.$request.post("Api/Home_Admin/home_admin", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					id:this.$utils.getloc('id_admin')
				}, (res) => {
					this.data = res.data.data;
					this.data.loglist = JSON.parse(this.data.loglist);
					this.data.logplist = JSON.parse(this.data.logplist);
					this.getchart('1');
				});
			},
			getchart: function(lx) {
				let _title = "";
				let _data = new Array();

				switch (lx) {
					case '1':
						_title = "用户数量";
						_data = JSON.parse(this.data.usercountlist);
						break;
					case '2':
						_title = "用户业绩";
						_data = JSON.parse(this.data.userlsklist);
						break;
					case '3':
						_title = "奖金";
						_data = JSON.parse(this.data.bonuslist);
						break;
					case '4':
						_title = "充值";
						_data = JSON.parse(this.data.chongzhilist);
						break;
					case '5':
						_title = "提现";
						_data = JSON.parse(this.data.tixianlist);
						break;
					case '6':
						_title = "订单";
						_data = JSON.parse(this.data.orderlist);
						break;
				}

				var now = new Date(); //当前日期
				var nowMonth = now.getMonth() + 1; //当前月

				let _monthArr = new Array();
				for (var i = 0; i <= 11; i++) {
					var m = nowMonth - i;
					if (m <= 0) {
						m += 12;
					}
					_monthArr.push(m + '月');
				}
				_monthArr.reverse();
				_data.reverse();

				var myChart = echarts.init(document.getElementById("main"));
				// 绘制图表
				myChart.setOption({
					title: {
						text: _title,
						left: "center"
					},
					tooltip: {
						trigger: "item",
						formatter: "{b} : {c}"
					},
					xAxis: {
						type: 'category',
						data: _monthArr
					},
					yAxis: {
						type: 'value'
					},
					series: [{
						data: _data,
						type: 'bar',
						color: '#409EFF',
						showBackground: true,
						backgroundStyle: {
							color: 'rgba(220, 220, 220, 0.8)'
						}
					}]
				});
			},
			edit: function() {
				this.$router.push({
					name: 'AdministratorList'
				})
			},
			handleClick(tab, event) {
				this.getchart(tab.name);
			}
		}
	}
</script>

<style scoped>
	.nopadding {
		padding: 0;
	}

	/deep/.el-timeline-item__node {
		background-color: #ccc;
	}

	/deep/.el-timeline-item__tail {
		border-left: 2px solid #ccc;
	}

	/deep/.el-timeline .el-timeline-item:last-child .el-timeline-item__tail{
		display: inline;
	}

	/deep/.el-card__body {
		padding: 10px;
	}

	.bg1 {
		background-image: url(../assets/img/home-t-1.jpg);
	}

	.bg2 {
		background-image: url(../assets/img/home-t-2.jpg);
	}

	.bg3 {
		background-image: url(../assets/img/home-t-3.jpg);
	}

	.bg4 {
		background-image: url(../assets/img/bg-title-01.jpg);
	}

	.bg5 {
		background-image: url(../assets/img/bg-title-02.jpg);
	}

	.bg6 {
		background-image: url(../assets/img/home-t-6.jpg);
	}

	.title {
		position: relative;
		padding: 10px;
		height: 50px;
		-webkit-background-size: cover;
		-moz-background-size: cover;
		-o-background-size: cover;
		background-size: cover;
		background-position: center center;
		background-repeat: no-repeat;
		-webkit-border-top-left-radius: 5px;
		-moz-border-radius-topleft: 5px;
		border-top-left-radius: 5px;
		-webkit-border-top-right-radius: 5px;
		-moz-border-radius-topright: 5px;
		border-top-right-radius: 5px;
		-webkit-box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
		-moz-box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
		box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
	}

	.titletext {
		line-height: 50px;
		font-size: 20px;
		position: relative;
		z-index: 2;
		color: #fff;
		text-align: right;
	}

	.bg-overlay {
		/* background: rgba(49, 89, 253, 0.7); */
		background: rgba(0, 0, 0, 0.5);
		position: absolute;
		width: 100%;
		height: 100%;
		top: 0;
		left: 0;
		z-index: 0;
		-webkit-border-top-left-radius: 5px;
		-moz-border-radius-topleft: 5px;
		border-top-left-radius: 5px;
		-webkit-border-top-right-radius: 5px;
		-moz-border-radius-topright: 5px;
		border-top-right-radius: 5px;
	}
	.cardcontent .zongji{
		font-size: 18px;
		font-weight: 600;
	}
</style>
