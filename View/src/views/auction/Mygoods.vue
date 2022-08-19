<template>
	<div>
		<HeadBar :title="title" toback="User" :leftarrow="false"></HeadBar>
		<van-tabs title-active-color="red" :border="false" v-model="active" @click="onClick">
			<!-- <van-tab title="我的商品" name="0"></van-tab> -->
			<van-tab title="全部" name="99"></van-tab>
			<van-tab title="待付款" name="1"></van-tab>
			<van-tab title="待收款" name="2"></van-tab>
			<van-tab title="待上架" name="3"></van-tab>
			<van-tab title="已出售" name="4"></van-tab>

			<!-- <van-tab title="我的卖单(字画)" name="1"></van-tab> -->
		</van-tabs>

		<div class="cardBox">
			<div style="margin-bottom:10px" class="cardContent" v-for="(item,index) in data" :key="index">
				<van-row style="padding: 5px;font-size: 13px;">
					<van-col span="18">订单号:{{item.holdno}}</van-col>
					<van-col span="6" style="text-align: right;color: #FC4702;"></van-col>
				</van-row>
				<van-row>
					<van-col span="8">
						<img :src="getimg(item.jimg)" style="width:100%" />
					</van-col>
					<van-col span="16">
						<div class="content">
							<div class="content-name">{{ item.jname }}</div>
							<div class="maijia-label"><span>买家</span>{{item.busername}}({{item.busertel}})</div>
							<div class="maijia-label2"><span>卖家</span>{{item.username}}({{item.usertel}})</div>
						</div>
					</van-col>
				</van-row>
				<div class="jine-wrap-row2">
					<div v-if="item.state > 0">抢购：{{item.qgdate}}</div>
					<div>确认：{{ item.skdate }}</div>
				</div>
				<div class="jine-wrap-row">
					<div>金额：{{ item.jprice }}</div>
					<div>上架费：{{ item.sjjine }}</div>
				</div>
				<div class="jine-wrap-row" v-if="uid == item.buid && active == 3">
					<div>付款方式</div>
					<div>
						<van-radio-group v-model="lx">
							<van-radio :name="1">画贝支付</van-radio>
							<van-radio :name="2" style="padding-top: 10px;">线下支付</van-radio>
						</van-radio-group>
					</div>
				</div>
				<!-- <div class="upload" v-if="uid != item.buid">
					<div>支付凭证</div>
					<
				</div> -->
				<div class="upload" v-if="item.state != 3 && lx == 2">
					<div>上传支付凭证</div>
					
						<UploadPictures
							:haveImgList="item.zhimg?[{url: getimg(item.zhimg)}]: haveImgList[index] ? [{url: getimg(haveImgList[index])}] : []"
							@getUploadPictures="getUploadPictures" @click.native="uploadClick(index)" title="" />
					
					<div v-if="item.zfimg != null && item.uid == uid" style="text-align: center;padding: 20px 0px;color: #969799;">
						买家未上传凭证
					</div>
					
				</div>
				<div class="upload" v-if="item.state == 3 && item.uid != uid && lx == 2">
					<div>上架支付凭证</div>
					<UploadPictures
						:haveImgList="item.sjimg?[{url: getimg(item.sjimg)}]: haveImgList[index] ? [{url: getimg(haveImgList[index])}] : []"
						@getUploadPictures="getUploadPictures" @click.native="uploadClick(index)" title="" />
				</div>
				<div style="padding: 4px;;" class="bnt-wrap">
					<!-- <div v-if="item.state == 3" style="font-size: 12px;padding: 5px 0px;color: red;">转售截止时间:{{item.edate}}</div> -->
					<van-button size="small" v-if="item.state == 1 && item.buid ==uid "
						style="background-color: #FC4702; color: #fff" @click="onShowBank(item,1)">立即付款</van-button>
						
					<van-col span="24" v-if="item.state==2  && item.uid ==uid" style="padding: 5px;text-align: right;">
						<van-button size="small" style="background-color: #FC4702; color: #fff"
							@click="shoukuanSubmit(item.id)">确认收款</van-button>
					</van-col>
					
					<van-button size="small" v-if="lx == 1 &&item.state == 3 && item.buid ==uid && item.issj == 0"
						style=" background-color: #FC4702; color: #fff" @click="shangjia(item.id)">上架</van-button>
						
					<van-button size="small" v-if="lx == 2 && item.state == 3 && item.buid ==uid && item.issj == 0"
						style="background-color: #FC4702; color: #fff" @click="onShowBank(item,2)">上架申请</van-button>
					<van-button size="small" v-if="item.state == 3 && item.buid ==uid && item.issj == 1"
						style="background-color: #969799; color: #fff">上架审核中</van-button>
					<van-button size="small" v-if="item.state == 4" style="background-color: #FC4702; color: #fff"
						@click="tolink('OrderDetails?id=' + item.id)">查看详情</van-button>
				</div>
			</div>
		</div>
		<van-empty description="暂无订单" v-if="data.length == 0" />


		<van-dialog v-model="showBank" show-cancel-button :confirmButtonText="showBankBtnLx == 1 ? '确认付款' : '确认申请'"
			@confirm="confirmZhongzhuan">
			<div class="dialog-bank">
				<div style="padding:20px 10px" v-if="orderActiveObj.state != 3">
					<div class="bank-title">卖家账户</div>
					<div style="border-bottom: 1px solid #E6A23C;margin-bottom:10px"
						v-for="(bank,bankindex) in orderActiveObj.banklist" :key="bankindex">
						<van-cell center title="账户名称" :value="bank.bankname" />
						<van-cell center title="账号" :value="bank.bankcard">
							<template #right-icon>
								<img style="padding-left: 10px;" src="../../assets/img/fz.png"
									@click="CopyLink(bank.bankcard)" />
							</template>
						</van-cell>
						<van-cell center title="姓名" :value="bank.username">
							<template #right-icon>
								<img style="padding-left: 10px;" src="../../assets/img/fz.png"
									@click="CopyLink(bank.username)" />
							</template>
						</van-cell>
						<van-cell center title="电话" :value="bank.usertel">
							<template #right-icon>
								<img style="padding-left: 10px;" src="../../assets/img/fz.png"
									@click="CopyLink(bank.usertel)" />
							</template>
						</van-cell>
						<div style="text-align:center">
							<img style="width: 60%;" :src="getimg(bank.bankimg)" />
						</div>
					</div>
				</div>
				<div style="padding:20px 10px" v-if="orderActiveObj.state == 3">
					<div class="bank-title">画室长账户</div>
					<div style="border-bottom: 1px solid #E6A23C;margin-bottom:10px"
						v-for="(bank2,bank2indx) in orderActiveObj.hsbanklist" :key="bank2indx">
						<van-cell center title="账户名称" :value="bank2.bankname" />
						<van-cell center title="账号" :value="bank2.bankcard">
							<template #right-icon>
								<img style="padding-left: 10px;" src="../../assets/img/fz.png"
									@click="CopyLink(bank2.bankcard)" />
							</template>
						</van-cell>
						<van-cell center title="姓名" :value="bank2.username">
							<template #right-icon>
								<img style="padding-left: 10px;" src="../../assets/img/fz.png"
									@click="CopyLink(bank2.username)" />
							</template>
						</van-cell>
						<van-cell center title="电话" :value="bank2.usertel">
							<template #right-icon>
								<img style="padding-left: 10px;" src="../../assets/img/fz.png"
									@click="CopyLink(bank2.usertel)" />
							</template>
						</van-cell>
						<div style="text-align:center">
							<img style="width: 60%;" :src="getimg(bank2.bankimg)" />
						</div>
					</div>
				</div>
			</div>
		</van-dialog>
		<BottomBar />
	</div>
</template>

<script>
	import HeadBar from "@/components/HeadBar";
	import UploadPictures from "@/components/UploadPictures";
	import BottomBar from "@/components/BottomBar";
	import Vue from "vue";
	import {
		Tab,
		Tabs,
		Dialog,
		Col,
		Row,
		Cell,
		Collapse,
		CollapseItem,
		Button,
		ActionSheet,
		Form,
		Empty,
		Field,
		CellGroup,
		CountDown,
		Radio,
		RadioGroup
	} from "vant";
	Vue.use(Dialog);
	Vue.use(Tab);
	Vue.use(Tabs);
	Vue.use(Col);
	Vue.use(Row);
	Vue.use(Cell);
	Vue.use(Collapse);
	Vue.use(CollapseItem);
	Vue.use(Button);
	Vue.use(ActionSheet);
	Vue.use(Form);
	Vue.use(Empty);
	Vue.use(Field);
	Vue.use(CellGroup);
	Vue.use(CountDown);
	Vue.use(Radio);
	Vue.use(RadioGroup);
	export default {
		name: '',
		components: {
			HeadBar,
			UploadPictures,
			BottomBar
		},
		data() {
			return {
				title: "全部订单",
				data: [],
				active: 99,
				activeNames: [],
				haveImgList: [],
				zfimg: "",
				sjimg: "",
				showsk: false,
				showfk: false,
				sushow: false,
				password2: "",
				hid: "",
				bbz: '',
				time: 30 * 60 * 60 * 1000,
				showBank: false,
				orderActiveObj: {},
				site: {},
				showBankBtnLx: "",
				uid: '',
				lx: 2
			}
		},
		created() {
			if (this.$route.query.state != null) {
				this.active = this.$route.query.state;
			}
			this.uid = this.$utils.getloc("id")

			this.getdata(this.active)
		},
		methods: {
			getimg: function(img) {
				if (img != "") {
					return this.$config.send_url + "Upload/" + img;
				}
			},
			tolink(path) {
				this.$router.push(path)
			},
			Sellshensu(id) {
				this.hid = id;
				this.sushow = true;
			},
			suconfirm() {
				let that = this
				that.$request.post(
					"api/UsersHold/SellShenshu", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						uid: that.$utils.getloc("id"),
						hid: that.hid,
						bbz: that.bbz
					},
					(res) => {
						that.$dialog.alert({
							title: "提示",
							message: res.data.msg,
						})
						return
						that.bbz = "";
						that.hid = "";
						that.sushow = false;
						//console.log(that.data)
					}
				)
			},
			onClick() {
				this.getdata(this.active)
			},
			getdata(lx) {
				let that = this
				that.data = [];
				that.$request.post(
					"api/UsersHold/MyOrderList", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						state: lx
					},
					(res) => {
						if (res.data.code == 0) {
							that.$dialog.alert({
								title: "提示",
								message: res.data.msg,
							})
							return
						}
						that.data = res.data.data.hlist;
						that.site = res.data.data.site
						console.log(that.data);
						console.log(that.site);
					}
				)
			},
			getUploadPictures(img) {
				this.haveImgList[this.uploadIndex] = img
			},
			fkClick(id) {
				this.showfk = true
				this.hid = id
			},
			skClick(id) {
				this.showsk = true
				this.hid = id
			},
			onShowBank(item, lx) {
				if (this.site.ispay == 1) {
					this.hid = item.id
					this.showBankBtnLx = lx
					this.orderActiveObj = item
					this.showBank = true
				} else {
					this.$dialog.alert({
						title: "提示",
						message: "未到交易时间",
					});
				}
			},
			shangjia(hid) {
				if (this.site.ispay == 1) {
					let that = this;
					that.hid = hid;
					that.$dialog.confirm({
						title: "提示",
						message: "你确认上架吗？"
					}).then(() => {
						that.$request.post(
							"api/UsersHold/Fukuan2", {
								token: that.$utils.getloc("token"),
								userid: that.$utils.getloc("userid"),
								uid: that.$utils.getloc("id"),
								hid: hid,
								lx: 1,
							},
							(res) => {
								if (res.data.code == 0) {
									that.$dialog.alert({
										title: "提示",
										message: res.data.msg,
									});
									return;
								}
								that.$dialog.alert({
									title: "提示",
									message: res.data.msg,
								});

								that.getdata(that.active)
							}
						)
					})
				} else {
					this.$dialog.alert({
						title: "提示",
						message: "未到交易时间",
					});
				}
			},
			// 弹窗确认按钮 中转
			confirmZhongzhuan() {
				if (this.showBankBtnLx == 1) {
					this.fukuanSubmit()
				} else if (this.showBankBtnLx == 2) {
					this.zhuanshou()
				}
			},
			// 收款
			shoukuanSubmit(id) {
				let that = this
				that.hid = id
				that.$dialog.confirm({
					title: "提示",
					message: "你确认收款吗？"
				}).then(() => {
					that.$request.post(
						"api/UsersHold/Shoukuan", {
							token: that.$utils.getloc("token"),
							userid: that.$utils.getloc("userid"),
							uid: that.$utils.getloc("id"),
							hid: that.hid,
							password2: that.password2
						},
						(res) => {
							that.$dialog.alert({
								title: "提示",
								message: res.data.msg,
							});
							that.showsk = false
							that.password2 = ""
							that.getdata(that.active)
						}
					)
				})
			},

			// jiaogeClick(hid){
			// 	let that = this
			//    Dialog.confirm({
			//      title: '提示',
			//      message: '确定要交割吗？',
			//    })
			//      .then(() => {
			//        that.$request.post(
			//          "api/UsersHold/Jiaoge",
			//          {
			//            token: that.$utils.getloc("token"),
			//            userid: that.$utils.getloc("userid"),
			//            uid: that.$utils.getloc("id"),
			//            hid: hid
			//          },
			//          (res) => {
			//            that.$dialog.alert({
			//              title: "提示",
			//              message: res.data.msg,
			//            });
			//            that.getdata(that.active)
			//          }
			//        )
			//      })
			//      .catch(() => {
			//        // on cancel
			//      });
			// },
			//上架审核
			zhuanshou() {
				let that = this
				let sjimg = this.haveImgList[that.uploadIndex] ? this.haveImgList[that.uploadIndex] : "";
				if (sjimg == '' || sjimg == null) {
					that.$dialog.alert({
						title: "提示",
						message: "请先上传凭证",
					});
					return;
				}
				// console.log(sjimg);
				that.$request.post(
					"api/UsersHold/Fukuan2", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						uid: that.$utils.getloc("id"),
						hid: that.hid,
						lx: 2,
						sjimg
					},
					(res) => {
						that.$dialog.alert({
							title: "提示",
							message: res.data.msg,
						});
						that.getdata(that.active)
					}
				)
			},
			// 付款
			fukuanSubmit() {
				let that = this
				let zfimg = this.haveImgList[that.uploadIndex] ? this.haveImgList[that.uploadIndex] : ""
				this.$request.post(
					"api/UsersHold/Fukuan", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						uid: that.$utils.getloc("id"),
						// password2: that.password2,
						hid: that.hid,
						zfimg
					},
					(res) => {
						that.$dialog.alert({
							title: "提示",
							message: res.data.msg,
						});
						that.showfk = false
						that.password2 = ""
						that.getdata(that.active)
					}
				)
			},
			uploadClick(index) {
				this.uploadIndex = index
			},
			CopyLink(val) {
				if (val != null && val != "") {
					var domUrl = document.createElement("input");
					domUrl.value = val;
					domUrl.id = "creatDom";
					document.body.appendChild(domUrl);
					domUrl.select(); // 选择对象
					document.execCommand("Copy"); // 执行浏览器复制命令
					let creatDom = document.getElementById("creatDom");
					creatDom.parentNode.removeChild(creatDom);
					this.$toast.success("复制成功");
				}
			},
		}
	}
</script>

<style scoped>
	/deep/.van-tabs__line {
		background-color: #e57516;
	}

	/deep/.van-tab--active {
		font-weight: 600;
		color: #e57516 !important;
	}

	/deep/.van-tab {
		color: #3b3e47;
		font-weight: 600;
	}

	.content {
		font-size: 14px;
		padding-left: 10px;
		padding-top: 10px;
	}

	.content div {
		margin-bottom: 10px;
	}

	.content-name {
		font-weight: 600;
	}

	.content-jine span {
		color: red;
		font-size: 15px;
		font-weight: 600;
	}

	/deep/ .van-cell {
		padding: 5px;
	}

	.upload {
		font-size: 13px;
		padding: 5px;
	}

	/deep/ .van-field__control {
		display: flex;
		justify-content: center;
		align-items: center;
		padding: 10px 0;
	}

	.bnt-wrap {
		display: flex;
		justify-content: flex-end;
	}

	.bnt-wrap .van-button {
		/* margin: 0 10px; */
		font-size: 12px;
		border-radius: 6px;
	}

	.maijia-label span {
		margin-right: 8px;
		background-color: #FC4702;
		color: #fff;
	}

	.maijia-label2 span {
		margin-right: 8px;
		background-color: #027D04;
		color: #fff;
	}

	.jine-wrap-row {
		padding: 4px 10px;
		font-weight: 600;
		border-top: 1px solid #ccc;
		border-bottom: 1px solid #ccc;
		display: flex;
		align-items: center;
		justify-content: space-between;
		font-size: 14px;
		color: #FC4702;
	}

	.jine-wrap-row2 {
		font-size: 12px;
		padding: 4px 10px;
		border-top: 1px solid #ccc;
	}

	.bank-title {
		padding: 0 4px 10px;
		font-size: 14px;
		font-weight: 600;
		color: #FC4702;
	}

	.dialog-bank {
		max-height: 320px;
		overflow-y: auto;
	}
</style>
