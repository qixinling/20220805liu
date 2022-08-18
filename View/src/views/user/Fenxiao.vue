<template>
    <div class="fenxiao">
        <div class="hader-nav">分销中心</div>
        <div style="background-color:#f7f7f7;padding-bottom:10px">
            <div class="userinfo">
                <img :src="usinfo.tx" width="70" height="70" style="border-radius:8px" />
                <div style="margin-left:20px">
                    <div style="font-size:16px;margin-bottom:5px">{{usinfo.userid}}</div>
                    <div>推荐人：{{usinfo.rename}}</div>
                    <div v-if="usinfo.ulevel == 0">会员【{{usinfo.ulevelname}}】</div>
                    <div v-if="usinfo.ulevel == 1">{{usinfo.studioname }}【{{usinfo.ulevelname}}画室长】</div>
                    <!-- <div v-if="usinfo.mystudiocard">我的画室长：{{usinfo.mystudiocard}}</div> -->
                </div>
            </div>
            <div class="qrcode">
                我的邀请码：{{usinfo.recode}}
                <span @click="CopyLink(usinfo.recode)">复制</span>
            </div>

            <div class="wallet-block">
                <div class="wallet-header">
                    <div class="wallet-header-row">
                        <img src="@/assets/img/fenxiao-wallet.svg" width="25" height="25" style="margin-right:8px" />
                        我的佣金
                    </div>
                    <div class="wallet-header-row" style="font-size:12px;color:gray" @click="tolink('tixian')">
                        佣金提现
                        <van-icon name="arrow" size="12" />
                    </div>
                </div>
                <div class="wallet-content">
                    <div>
                        <div class="wallet-jine">{{wallet.jine}}</div>
                        <div style="font-size:14px">可提现佣金(元)</div>
                    </div>
                </div>
            </div>

            <div class="nav-block">
                <div class="nav-item" @click="tolink('Bouns')">
                    <van-icon name="point-gift-o" size="30" color="#ff035b" />
                    <div class="nav-item-label">推广佣金</div>
                </div>
                <div class="nav-item" @click="tolink('TgOrder')">
                    <van-icon name="shop-o" size="30" color="#55aaff" />
                    <div class="nav-item-label">推广订单</div>
                </div>
                <div class="nav-item" @click="tolink('Tixianrecord')">
                    <van-icon name="shop-collect-o" size="30" color="#ffd53a" />
                    <div class="nav-item-label">提现明细</div>
                </div>
                <div class="nav-item" @click="tolink('Tree')">
                    <van-icon name="friends-o" size="30" color="#ff5500" />
                    <div class="nav-item-label">我的粉丝</div>
                </div>
            </div>
        </div>
        <!-- <div class="user-header" style="">
            <van-row style="padding: 25px 15px 20px 15px;display: flex;justify-content: center;align-items: center;">
                <van-col span="6" style="text-align: center">
                    <img style="width: 60px;height: 60px;border-radius: 60px;border: 2px snow solid;box-shadow: 0 1px 5px rgba(0, 0, 0, 0.5);" :src="getimg(usinfo.tx)" />
                </van-col>
                <van-col span="18" style="color: #641602;line-height: 25px;">
                    <van-col span="24" style="font-size: 16px;display: flex;align-items: center;font-weight: 600;">{{ usinfo.userid }}</van-col>
					<van-col span="24" style="font-size: 14px;display: flex;align-items: center;">推荐人：{{ usinfo.rename }}</van-col>
                    <van-col span="24" v-if="usinfo.ulevel == 1" style="font-size: 12px;">
                        {{usinfo.studioname }}【画室长】
                    </van-col>
                </van-col>
            </van-row>
        </div>
		<div style="background-color: #fff;" v-if="usinfo.mystudiocard">
			<div style="padding: 10px;font-size: 14px;">我的画室长：{{usinfo.mystudiocard}}</div>
		</div> -->
	    <div class="cell-list">
	    	<van-cell title="业绩统计" icon="manager" value="" is-link to="./TgOrder" />
	    	<van-cell v-if="usinfo.ulevel == 1" title="画室控制台"  icon="friends" value="" is-link to="./Kongzhi" />
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
           wallet:[],
            msgCount: 0,
            marqueemsg: "",
            defaulttx: require("../../assets/img/tx.png"),
			zjine:0
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
				this.Wallet_GetWallet();
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
		            _this.wallet = res.data.data.ulist[0];
					
		        }
		    );
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
                    // console.log(_this.usinfo)
                   // _this.zjine = Number(_this.usinfo.mey) + Number(_this.usinfo.jindou);
                    _this.userid = _this.usinfo.userid;
                    _this.msgCount = _this.usinfo.msgcount;
                }
            );
        },
 
        tolink(path) {
            this.$router.push(path)
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
    },
};
</script>

<style scoped>
.fenxiao {
    padding-top: 44px;
    min-height: calc(100vh - 94px);
    background-color: #fff;
}

.van-cell__value {
    font-size: 12px;
}

/deep/.van-cell__left-icon {
    font-size: 24px;
    padding-right: 4px;
}


.cell-list /deep/.van-icon-manager::before{
	color: rgb(108, 0, 108);
}

.cell-list /deep/.van-icon-friends::before{
	color: rgb(108, 0, 108);
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
    margin: 0 10px 10px 10px;
    border-radius: 10PX;
    overflow: hidden;
}

.user-header {
    padding-bottom: 10px;
	background-color: #f5cf9c;
    /* color: rgb(100, 22, 2);
    background: url("../../assets/img/user.png");
    background-size: 100% 100%; */
	
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

.userinfo {
    padding: 10px;
    display: flex;
    align-items: center;
    background-color: #ff7606;
}

.userinfo {
    color: #fff;
    font-size: 14px;
}

.qrcode {
    padding: 5px 10px;
    margin-bottom: 10px;
    font-size: 14px;
    background-color: #fff;
}

.qrcode span {
    color: #ff7606;
    margin-left: 4px;
}

.wallet-block {
    background-color: #fff;
    margin-bottom: 10px;
}

.wallet-header {
    padding: 10px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 16px;
    border-bottom: 1px solid #ccc;
}

.wallet-header-row {
    display: flex;
    align-items: center;
}

.wallet-content {
    padding: 10px 0;
    display: flex;
    align-items: center;
    justify-content: center;
    text-align: center;
}

.wallet-jine {
    margin-bottom: 6px;
    font-weight: 600;
    color: #ff7606;
}

.nav-block {
    display: flex;
    flex-wrap: wrap;
    background-color: #fff;
}

.nav-item {
    box-sizing: border-box;
    padding-left: 15px;
    height: 66px;
    width: 50%;
    display: flex;
    align-items: center;
    font-size: 14px;
}

.nav-item:nth-child(1) {
    border-right: 1px solid #ccc;
    border-bottom: 1px solid #ccc;
}

.nav-item:nth-child(2) {
    border-bottom: 1px solid #ccc;
}

.nav-item:nth-child(3) {
    border-right: 1px solid #ccc;
}

.nav-item-label {
    padding-left: 10px;
}
</style>
