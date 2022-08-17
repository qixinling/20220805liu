<template>
	<div class="qiangpaiqu">
		<van-row style="background-color: #fff;padding: 12px 8px;" class="navbar">
			<van-col span="8">
				<van-icon name="arrow-left" size="medium" @click="tolink" />
			</van-col>

			<van-col span="8" style="text-align: center;">画室大厅</van-col>

			<van-col span="8" style="text-align: right;">

				<!-- <van-count-down v-if="site.ispay == 0" :time="site.downDate" @finish="getsite">
					<template #default="timeData">
						<span class="block">{{ timeData.hours }}</span>
						<span class="colon">:</span>
						<span class="block">{{ timeData.minutes }}</span>
						<span class="colon">:</span>
						<span class="block">{{ timeData.seconds }}</span>
					</template>
				</van-count-down> -->
			</van-col>
		</van-row>
		<div style="background-color: #dbdbdb;padding-top: 50px;text-align: center;" >
			<div style="padding: 10px;font-size: 14px;" v-if="site.length > 0">
				<span style="padding-right:10px">营业中 营业时间：{{site[1].sdate}} - {{site[1].edate}}</span>
				
				<van-button v-if="site[0].ispay == 1 && hsisyy == 1 && week >= 3 && week <= 7" size="mini"  color="#EC5624" @click="yuyue">
					预约
				</van-button>
			</div>
		</div>
		<!-- <div class="fenqu-wrap" v-if="site.id == 1">
			<div class="box">
				<div class="label">请选择你想要预约的分区</div>
				<div class="list">
					<div class="item" @click="itemClick(index)" v-for="(item,index) in fenquList" :key="index">
						<div>{{ item.name }} {{ item.minprice }}-{{ item.maxprice }}</div>
						<van-icon name="success" size="21px" color="#444" v-show="fenquIndex == index" />
					</div>
				</div>
			</div>

			<van-button block round color="linear-gradient(to right, #ff9700, #ed1c24)" @click="yuyue">
				预约
			</van-button>
		</div> -->

		<!-- <HeadBar :title="title" :bg="'transparent'"></HeadBar> -->
		<!--<van-list v-model="loading" :finished="finished" finished-text="没有更多了" :offset="0" :immediate-check="false" @load="onLoad"> -->
		<div>
			<div style="padding: 5px 5px 60px" class="goods-content">
				<van-row>
					<van-col style="padding: 5px" span="8" v-for="(item, index) in data" :key="index">
						<div class="goods-card">
							<div class="goodsimg">
								<img :src="getimg(item.jimg)" style="width: 100%" />
							</div>
							<div class="goods-content">
								<div class="goodsname">{{ item.jname }}</div>
								<div class="goodsprice">
									<span>￥{{ item.jprice }}</span>
								</div>
								<div class="goodsother">
									<van-button block size="small" style="background-color:#ff4500;color:#fff" @click="qiangpai(item.id)">
										抢购
									</van-button>
								</div>
							</div>
						</div>
					</van-col>
				</van-row>

				<van-empty description="暂无抢购" v-if="data.length == 0" />
			</div>
			<div class="footer">
				<van-pagination v-model="pageIndex" :page-count="pagecount" mode="simple" @change="pagechange" />
			</div>
		</div>


		<!-- </van-list> -->
	</div>
</template>

<script>
	import HeadBar from "@/components/HeadBar";
	import Vue from 'vue';
	import {
		Button,
		Dialog,
		Empty,
		CountDown,
		Toast,
		Pagination
	} from 'vant';
	Vue.use(Button);
	Vue.use(Dialog);
	Vue.use(Empty);
	Vue.use(CountDown);
	Vue.use(Toast);
	Vue.use(Pagination);
	export default {
		name: "",
		components: {
			HeadBar,
		},
		data() {
			return {
				title: "抢购专场",
				data: [],
				minPrice: "",
				maxPrice: "",
				sid: "",
				fqid: '',
				site: [],
				show: false,
				pageIndex: 1,
				pageSize: 50,
				pagecount: 0,
				loadingicon: require("@/assets/img/loading.gif"),
				getTime: null,
				fenquList: [],
				fenquIndex: 0,
				hsisyy:0,
				week:''
			};
		},
		created() {
			this.load();
		},
		methods: {
			load: function () {
			    //登录组件登录成功后会触发该函数,刷新父窗体数据
			    if (this.$store.state.LoginState) {
			        //子组件调用加载
			      this.getsite();
			      this.getdata(this.pageIndex);
			    }
			},
			tolink() {
				this.$router.back(-1);
			},
			getimg: function(img) {
				if (img != "") {
					return this.$config.send_url + "Upload/" + img;
				}
			},
			pagechange(e) {
				this.getdata(e);
				window.scrollTo(0, 0);
			},
			itemClick(index) {
				this.fenquIndex = index
			},
			yuyue: function() {
				this.$router.push({
				    name: "Yuyue",
				});
				// //let fqid = this.fenquList[this.fenquIndex].id;
				// let that = this
				// that.$request.post(
				// 	"api/UsersHold/yuyue", {
				// 		token: that.$utils.getloc("token"),
				// 		userid: that.$utils.getloc("userid"),
				// 		uid: that.$utils.getloc("id"),
				// 		//pid: fqid
				// 	},
				// 	(res) => {
				// 		that.$dialog.alert({
				// 			title: "提示",
				// 			message: res.data.msg,
				// 		});
				// 	}
				// )
			},
			getdata(pageindex) {
				let that = this
				that.$request.post(
					"api/UsersHold/List", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						uid: that.$utils.getloc("id"),
						pageIndex: pageindex,
						pageSize: that.pageSize,
					},
					(res) => {
						if (res.data.code == 0) {
							that.$dialog.alert({
								title: "提示",
								message: res.data.msg,
							})
							return
						}
						that.week = res.data.data.week;
						that.data = res.data.data.hlist;
						that.pagecount = res.data.data.pagecount;
						// if(list.length > 0){
						// 	 that.finished = false
						// }else{
						// 	 that.finished = true;
						// }
						// that.data.push(...list);
						//console.log(that.data);
					}
				)
			},
			onLoad() {
				this.loading = true
				this.pageIndex++
				this.getdata();
			},
			getsite() {
				let that = this
				that.$request.post(
					"api/Site/Get", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						uid: that.$utils.getloc("id"),
					},
					(res) => {
						if (res.data.code == 0) {
							that.$dialog.alert({
								title: "提示",
								message: res.data.msg,
							})
							return
						}
						that.site = res.data.data.slist;
						//console.log(res.data.data);
						that.hsisyy = res.data.data.hsus.iswn;
						//console.log(that.site);
						// if (item) {

						// 	let sdateArr = item.sdate.split(":")
						// 	// 场次开始时间的毫秒数
						// 	let sdateMiner = that.toMiner(sdateArr[0], sdateArr[1], sdateArr[2])
						// 	// 获取当前时间 (24小时制)
						// 	let current = new Date().toLocaleTimeString('chinese', {
						// 		hour12: false
						// 	})
						// 	let currentArr = current.split(":")
						// 	// 获取当前时间毫秒数
						// 	let currentMiner = that.toMiner(currentArr[0], currentArr[1], currentArr[2])
						// 	// 获取抢拍结束时间毫秒数
						// 	let edateArr = item.edate.split(":")
						// 	let edateMiner = that.toMiner(edateArr[0], edateArr[1], edateArr[2])
						// 	item.downDate = sdateMiner - currentMiner
						// 	// 当倒计时结束并且当前时间已超过抢拍时间时重新计算距离下一次抢拍时间
						// 	if (item.downDate <= 0 && currentMiner >= edateMiner) {
						// 		item.downDate = 86400000 - currentMiner + sdateMiner
						// 	}

						// 	that.site = item;
						// }
					});
			},
			// 获取毫秒数
			toMiner(hour, min, seconds) {
				var miner;
				miner = hour * (60 * 60 * 1000) + min * (60 * 1000) + seconds * 1000;
				return miner;
			},
			qiangpai(id) {
				let that = this
				let _toast = that.$toast.loading({
					message: "抢购中...",
					icon: this.loadingicon,
					duration: 0, // 持续展示 toast
				});
				that.$request.post(
					"api/UsersHold/Buy", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						uid: that.$utils.getloc("id"),
						hid: id,
						sid: that.sid
					},
					(res) => {
						_toast.clear();
						if (res.data.code == 0) {
							that.getdata(that.pageIndex);
							Toast.fail(res.data.msg);
							return
						}
						Dialog.alert({
							title: "提示",
							message: res.data.msg
						}).then(() => {
							that.getdata(that.pageIndex);
						})
					},
					false
				)
			},
		},
	};
</script>

<style scoped>
	.qiangpaiqu {
		min-height: 100vh;
		background-color: #fff;
	}

	.fenqu-wrap {
		min-height: calc(100vh - 90px);
		display: flex;
		flex-direction: column;
		justify-content: space-around;
		padding: 20px;
		font-size: 15px;
	}

	.box {
		padding: 20px 10px;
		color: #fff;
		background: url("../../assets/img/fenqu.png");
		background-size: 100% 100%;
		border-radius: 10px;
		margin-bottom: 16px;
	}

	.label {
		padding: 10px 0;
	}

	.list .item {
		display: flex;
		justify-content: space-between;
		align-items: center;
		padding: 10px 20px;
		background-color: #fff;
		color: #000;
		margin-bottom: 16px;
		border-radius: 99rem;
	}


	/deep/.van-pagination {

		padding: 5px 5px;
	}

	/deep/.van-pagination__item {
		font-weight: 600;
	}

	.footer {
		border-top: 1px solid rgba(226, 224, 224, .4);
		height: 50px;
		width: 100%;
		background-color: #fff;
		position: fixed;
		bottom: 0;
	}

	.navbar {
		position: fixed;
		top: 0;
		left: 0;
		right: 0;
		background-color: #fff;
		z-index: 9;
		border-bottom: 1px solid rgba(209, 208, 208, 0.3);
	}

	.goods-card {
		overflow: hidden;
		background-color: #fff;
		/* box-shadow: rgba(0, 0, 0, 0.04) 0px 3px 5px; */
	}

	.goods-card:last-child {
		border-bottom: 0;
	}

	.goods-wrap {
		display: flex;
		padding: 12px 0;
	}

	.goodsimg {
		text-align: center;
	}

	.goods-content {
		flex: 5;
		display: flex;
		flex-direction: column;
		justify-content: space-between;
		padding: 8px;
	}

	.goodsname {
		/* height: 38px; */
		margin-bottom: 4px;
		font-size: 13px;
		font-weight: 600;
		display: -webkit-box;
		-webkit-box-orient: vertical;
		-webkit-line-clamp: 1;
		overflow: hidden;
		color: #333;
	}

	.jianjie {
		height: 16px;
		font-size: 12px;
		color: #888;
		margin: 5px 0;
		display: -webkit-box;
		-webkit-box-orient: vertical;
		-webkit-line-clamp: 1;
		overflow: hidden;
	}

	.goodsprice {
		margin-bottom: 4px;
		font-size: 13px;
	}

	.goodsprice span {
		color: red;
		/* font-size: 14px; */
		font-weight: 600;
	}

	/* .goodsother {
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 13px;
    color: #f0ad4e;
} */

	.goodsother {
		padding: 4px 0;
	}

	.goodsprice2 {
		color: #1b8bc5;
		font-size: 16px;
		font-weight: 500;
	}

	.goodsprice2 span {
		font-size: 12px;
		margin-left: 1px;
	}

	.kucun {
		/* padding-top: 12px; */
		font-size: 12px;
		color: #888;
	}

	.colon {
		display: inline-block;
		margin: 0 4px;
		color: #555;
	}

	.block {
		display: inline-block;
		width: 22px;
		color: red;
		font-size: 12px;
		font-weight: bold;
		text-align: center;
		background-color: #ccc;
		border-radius: 4px;
	}
</style>
