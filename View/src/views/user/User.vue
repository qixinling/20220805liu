<template>
    <div>
        <div class="hader-nav">个人中心</div>
        <div class="user-block">
            <div class="wallet">
                <div>余额</div>
                <div style="color:#ff4500">{{walletJine}}</div>
                <div class="wallet-tixian-btn" @click="tolink('tixian')">提现</div>
            </div>
            <div class="user-info">
                <img :src="usinfo.tx" width="70" height="70" style="border-radius:8px" />
                <div style="margin-top:4px">{{ usinfo.userid }}</div>
            </div>
        </div>
        <div class="order-block">
            <div class="order-header">
                <div class="order-header-left">
                    <img src="@/assets/img/user-order.svg" width="25" height="25" style="margin-right:8px" />
                    我的订单
                </div>
                <div class="order-header-right" @click="tolink('Mygoods?state=99')">
                    查看全部
                    <van-icon name="arrow" size="12" />
                </div>
            </div>
            <div class="order-row">
                <div class="order-col" @click="tolink('Mygoods?state=1')">
                    <van-icon name="paid" size="30" />
                    <div>等待付款</div>
                </div>
                <div class="order-col" @click="tolink('Mygoods?state=2')">
                    <van-icon name="clock-o" size="30" />
                    <div>收款确认</div>
                </div>
                <div class="order-col" @click="tolink('Mygoods?state=3')">
                    <van-icon name="logistics" size="30" />
                    <div>委托代卖</div>
                </div>
            </div>
        </div>

        <div class="tool-box">
            <div class="cardContent">
                <van-cell title="我的字画" icon="send-gift-o" is-link to="Mygoods2" value="" />
                <van-cell title="预约记录" icon="orders-o" is-link to="YuyueRecord?lx=1" value="" />
                <!-- <van-cell title="提货订单" icon="gift-o" is-link to="Tihuo" value="" />
                <van-cell title="商城订单" icon="shop-o" is-link to="Order" value="" /> -->
                <van-cell title="安全中心" icon="bulb-o" is-link to="Safe" value="" />
                <!-- <van-cell title="新闻公告" icon="newspaper-o" is-link to="News" value="" /> -->
                <!-- <van-cell title="我的团队" icon="friends-o " value="" is-link to="./Tree" /> -->
                <van-cell title="邀请好友" icon="qr" is-link :to="'./Qrcode?recode=' + usinfo.recode +'&usertel=' + usinfo.usertel +'&tx=' +usinfo.tx" />
                <van-cell title="绑定信息" icon="setting-o" is-link to="UserInfo" value="" />
               <van-cell title="消息" icon="chat-o" is-link to="./Msg">
               	<div v-if="msgCount > 0">{{ msgCount }} 未读</div>
               </van-cell>
			   <!-- <van-cell title="常见问题" icon="gift-o" is-link to="Question" value="" /> -->
			   <van-cell title="收货地址" icon="location-o" is-link to="Address" value="" />
            </div>
        </div>

        <div class="btn-box">
            <van-button style="background-color: #ff4500; color: #fff; border-radius:5px" block @click="logout">切换账号</van-button>
        </div>

        <Login></Login>
        <BottomBar></BottomBar>
    </div>
</template>

<script>
import Login from "@/components/Login";
import BottomBar from "@/components/BottomBar";

import Vue from "vue";
import { Grid, GridItem } from "vant";
import { Cell, CellGroup, Tag, NoticeBar } from "vant";

Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Grid);
Vue.use(GridItem);
Vue.use(Tag);
Vue.use(NoticeBar);

export default {
    name: "user",
    components: {
        Login,
        BottomBar,
    },
    data() {
        return {
            active: "",
            userid: "您还没有登录,请先登录.",
            usinfo: {},
            num: {
                yynum: 0,
                lynum: 0,
                zrnum: 0
            },
            msgCount: 0,
            marqueemsg: "",
            defaulttx: require("../../assets/img/tx.png"),
			zjine:0,
            walletJine: 0
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
                this.getdata();
                this.Wallet_GetWallet()
                // this.getordernum();
            }
        },
        getimg: function (imgname) {
            let path = this.$config.send_url + "Upload/" + imgname;
            if (typeof imgname == "undefined") {
                path = this.defaulttx;
            }
            return path;
        },
        getdata: function () {
            var _this = this;
            _this.$request.post(
                "api/Users/Get",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.usinfo = res.data.data;
                    if(_this.usinfo.tx == "" || _this.usinfo.tx == "tx.png") {
                        _this.usinfo.tx = require('@/assets/img/tx.png')
                    } else {
                        _this.usinfo.tx = _this.getimg(_this.usinfo.tx)
                    }
                   // _this.zjine = Number(_this.usinfo.mey) + Number(_this.usinfo.jindou);
                    _this.userid = _this.usinfo.userid;
                    _this.msgCount = _this.usinfo.msgcount;
                }
            );
        },
        getordernum: function () {
            var _this = this;
            _this.$request.post(
                "api/UsersHold/AllNum",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    //console.log(res.data);
                    _this.num = res.data.data;
					//console.log(_this.ordernum);
                }
            );
        },
        logout: function () {
            this.$utils.setloc("token", "");
            this.$store.commit("setLoginState", false);
            //this.$router.go(0);
            location.reload();
        },
        tolink(path) {
            this.$router.push(path)
        },
		CopyLink() {
			if (this.usinfo.usertel != null && this.usinfo.usertel != "") {
				var domUrl = document.createElement("input");
				domUrl.value = this.usinfo.usertel;
				domUrl.id = "creatDom";
				document.body.appendChild(domUrl);
				domUrl.select(); // 选择对象
				document.execCommand("Copy"); // 执行浏览器复制命令
				let creatDom = document.getElementById("creatDom");
				creatDom.parentNode.removeChild(creatDom);
				this.$toast.success("复制成功");
			}
		},
        Wallet_GetWallet: function () {
		    let _this = this;
		    let _toast = _this.$toast.loading({
		        message: "正在加载...",
		        duration: 0, // 持续展示 toast
		    });
		    _this.$request.post(
		        "api/Wallets/GetWallet",
		        {
		            token: _this.$utils.getloc("token"),
		            userid: _this.$utils.getloc("userid"),
		            id: _this.$utils.getloc("id"),
		        },
		        (res) => {
		            if (res.data.code == 0) {
		                _this.$dialog.alert({
		                    title: "提示",
		                    message: res.data.msg,
		                });
		                return;
		            }
		            _toast.clear();
		            _this.walletJine = res.data.data.ulist[0].jine;
					
		        }
		    );
		},
    },
};
</script>

<style scoped>
.van-cell__value {
    font-size: 12px;
}

/deep/.van-cell__left-icon {
    font-size: 24px;
    padding-right: 4px;
}

/deep/.van-icon-underway::before,
/deep/.van-icon-send-gift::before,
/deep/.van-icon-gift-card::before,
/deep/.van-icon-warn-o::before
{
    color: #e9b69b;
}

.a /deep/.van-cell {
    padding: 5px 16px;
}

/deep/.topbtn .van-grid-item__content {
    background-color: transparent;
}

/deep/.topbtn .van-grid-item__icon {
    color: #fff;
}

/deep/.topbtn .van-icon-underway::before,
/deep/.topbtn .van-icon-send-gift::before,
/deep/.topbtn .van-icon-invition::before {
    color: #fff;
}

/deep/.topbtn .van-grid-item__text {
    color: #fff;
}

/* .van-cell {
    background-color: #3B3E47;
    color: #fff;
} */

/* /deep/ .van-grid-item__content {
    background-color: #3B3E47;
}

/deep/ .van-grid-item__text {
    color: #fff;
} */

.van-button--default {
    border: 0;
}

/* .van-cell::after {
    border: 1px solid rgba(88, 88, 88, .6);
} */

/deep/ .van-cell__value {
    color: #B5B6B8 !important;
}

.card-info {
    background-color: #3B3E47;
    margin: 10px 10px 0 10px;
    border-radius: 10px;
}

.card-info .van-col {
  padding: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.card-info .value {
  color: #f4c40b;
  font-size: 16px;
  font-weight: 600;
}

.card-info .key {
  font-size: 12px;
  margin-top: 4px;
}

.tool-box {
    margin: 15px 0 0;
}

.user-header {
    padding-bottom: 10px;
    color: rgb(100, 22, 2);
    background: url("../../assets/img/user.png");
    background-size: 100% 100%;
}

.hader-nav {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    height: 44px;
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 99;
    font-size: 16px;
    font-weight: 600;
    background-color: #fff;
}

.user-block {
    margin-top: 44px;
    padding: 20px 0;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #fff;
    background-color: #012d17;
}

.wallet {
    position: absolute;
    left: 10%;
    font-size: 14px;
    font-weight: 500;
}

.wallet > div {
    margin-top: 8px;
    text-align: center;
}

.wallet-tixian-btn {
    padding: 3px 8px;
    border: 1px solid #fff;
    border-radius: 8px;
}

.user-info {
    font-size: 14px;
    text-align: center;
}

.order-block {
    background-color: #fff;
}

.order-header {
    padding: 10px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    border-bottom: 1px solid #ccc;
}

.order-header-left, .order-header-right {
    display: flex;
    align-items: center;
}

.order-header-right {
    font-size: 12px;
}

.order-row {
    margin-top: 15px;
    padding-bottom: 8px;
    display: flex;
    align-items: center;
    justify-content: space-around;
    font-size: 14px;
    text-align: center;
}

.btn-box {
    margin: 30px 20px;
}
</style>
