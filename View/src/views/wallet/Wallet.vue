<template>
    <div class="wallet">
        <!-- <van-nav-bar title="我的钱包"  :border="false" style="background-image: linear-gradient(to right, #52BDFF 0%, #0084FF 100%);">
		</van-nav-bar> -->
        <div class="wallet-box textcolor2" style="padding: 15px;">
            <van-row>
                <van-col span="18">
                    <span style="font-size: 25px; color: #641602; font-weight: 600">我的钱包</span>
                </van-col>
                <van-col span="3" style="text-align: right">
                    <router-link to="/Bouns">
                        <van-icon size="25" color="#641602" name="balance-list-o" />
                        <div style="font-size: 10px; color: #641602">收益</div>
                    </router-link>
                </van-col>
                <van-col span="3" style="text-align: right">
                    <router-link to="/Bill">
                        <van-icon size="25" color="#641602" name="todo-list-o" />
                        <div style="font-size: 10px; color: #641602">账单</div>
                    </router-link>
                </van-col>
            </van-row>
            <van-row>
                <van-col span="24" class="top">
                    <van-cell style="background: none; color: #641602" :border="false" title="昨日收入" :value="zuori" />
                    <van-cell style="background: none; color: #641602" :border="false" title="本日收入" :value="jinri" />
					<van-cell style="background: none; color: #641602" :border="false" title="冻结信用值" :value="djxyz" />
                </van-col>
            </van-row>

           <!-- <van-grid :border="false" :column-num="4">
                <van-grid-item icon="refund-o" to="Chongzhi" text="充值" />
                <van-grid-item icon="edit" to="Tixian" text="提现" />
                <van-grid-item icon="friends-o" to="Zhuanzhang" text="转账" />
                <van-grid-item icon="after-sale" to="Zhuanhuan" text="转换" />
            </van-grid> -->

        </div>
		<!-- <van-tabs v-model="active" @click="tabclick()">
		  <van-tab  v-for="(item, index) in walletlist" :key="index" :title="item.cnamezh" ></van-tab>
		</van-tabs> -->
        <div style="text-align: center;padding-top: 80px;">
			<van-row>
				<van-col span="2"  style="display: flex;align-items: center;height: 150px;padding-left: 10px;">
					<van-icon name="arrow-left" size="20" @click="leftclick()"/>
				</van-col>
				<van-col span="20">
					<div><van-icon name="balance-o" size="50" color="#f7b226"/></div>
					<div style="font-weight: bold;font-size: 24px;padding-top: 10px;">{{wallet.jine}}</div>
					<div style="font-size: 14px;padding-top: 15px;">{{wallet.cnameZh}}</div>
					
					<div style="padding-top: 120px;">
						<div>
							<van-button v-for="cz in czselectlist" :key="cz.id" v-show="cz.bid == wallet.cid " type="primary" class="btn" @click="tolink('Chongzhi',wallet.cid)">充值</van-button>
						</div>
						<div>
							<van-button v-for="tx in txselectlist" :key="tx.id" v-show="tx.bid == wallet.cid " type="primary" class="btn" @click="tolink('Tixian',wallet.cid)">提现</van-button>
						</div>
						<div>
							<van-button v-for="zz in zzselectlist" :key="zz.id" v-show="zz.bid == wallet.cid " type="primary" class="btn" @click="tolink('Zhuanzhang',wallet.cid)">转账</van-button>
						</div>
						<div>
							<van-button v-for="zh in zhselectlist" :key="zh.id" v-show="zh.bid1 == wallet.cid" type="primary" class="btn" @click="tolink('Zhuanhuan',zh.id)">转换</van-button>
						</div>
						<!-- <div>
							<van-button type="primary" v-if="wallet.cid == 1" class="btn" @click="tolink('Buy')">购买星座</van-button>
						</div> -->
						
					</div>
				</van-col>
				<van-col span="2"  style="display: flex;align-items: center;height: 150px;">
					<van-icon name="arrow" size="20" @click="rightclick()"/>
				</van-col>
			</van-row>
			
		</div>
        
        <Login></Login>
        <BottomBar></BottomBar>
    </div>
</template>

<script>
import Login from "@/components/Login";
import BottomBar from "@/components/BottomBar";
import Vue from "vue";
import { Cell, CellGroup } from "vant";
import { NavBar } from "vant";
import { Grid, GridItem } from "vant";
import { Tab, Tabs } from 'vant';

Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Grid);
Vue.use(GridItem);
Vue.use(NavBar);
Vue.use(Cell);
Vue.use(CellGroup);

// 引入 ECharts 主模块
var echarts = require("echarts");
export default {
    name: "wallet",
    components: {
        Login,
        BottomBar,
    },
    data() {
        return {
			wallet:[],
            walletlist: [],
            jinri: "",
            zuori: "",
            jjye: 0,
			active:0,
			czselectlist:[],
			txselectlist:[],
			zzselectlist:[],
			zhselectlist:[],
			djxyz:0
        };
    },
    created() {
        this.load();
		this.Wallet_ChongzhiSelect();
		this.Wallet_TixianSelect();
		this.Wallet_ZhuanzhangSelect();
		this.Wallet_ZhuanhuanSelect();
    },
    mounted() {},
    methods: {
        load: function () {
            //登录组件登录成功后会触发该函数,刷新父窗体数据
            if (this.$store.state.LoginState) {
                //子组件调用加载
                this.Wallet_GetWallet();
                this.Bill_Shouru();
            }
        },
        tolink: function (path,id) {
            this.$router.push({
                name: path,
				query:{
					cid:id
				}
            });
        },
		rightclick(){
			this.active += 1;
			if(this.active >= this.walletlist.length){
				this.active = this.active - this.walletlist.length;
			}	
			this.wallet = this.walletlist[this.active];
			//console.log(this.active);
		},
		leftclick(){
			this.active -= 1;
			if(this.active < 0){
				this.active = this.active + this.walletlist.length;		
			}
			this.wallet = this.walletlist[this.active];
			//console.log(this.active);
		},
		tabclick(){
			//console.log(this.active);
			this.wallet = this.walletlist[this.active];
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
                    _this.walletlist = res.data.data.ulist;
					_this.wallet = _this.walletlist[1];
					_this.djxyz = res.data.data.djxyz
					//console.log(_this.wallet);
                }
            );
        },
        Bill_Shouru: function () {
            var _this = this;
            _this.$request.post(
                "api/Bill/Shouru",
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
					//console.log(res);
                    _this.jinri = res.data.data[0].jinri;
                    _this.zuori = res.data.data[0].zuori;
                }
            );
        },
		Wallet_ChongzhiSelect: function () {
		  let _this = this;
		  let _toast = _this.$toast.loading({
		    message: "正在加载...",
		    duration: 0, // 持续展示 toast
		  });
		  _this.$request.post(
		    "api/WalletsChongzhi/WalletsChongzhiSelect",
		    {
		      token: _this.$utils.getloc("token"),
		      userid: _this.$utils.getloc("userid"),
		    },
		    (res) => {
		      let data = res.data.data;
		      _this.czselectlist = data;
			 // console.log(res);
		    }
		  );
		},
		Wallet_TixianSelect: function () {
		  let _this = this;
		  _this.$request.post(
		    "api/WalletsTixian/TixianSelect",
		    {
		      token: _this.$utils.getloc("token"),
		      userid: _this.$utils.getloc("userid"),
		    },
		    (res) => {
		      let data = res.data.data;
		      _this.txselectlist = data;
		    }
		  );
		},
		Wallet_ZhuanzhangSelect: function () {
		  let _this = this;
		  _this.$request.post(
		    "api/WalletsZhuanzhang/ZhuanzhangSelect",
		    {
		      token: _this.$utils.getloc("token"),
		      userid: _this.$utils.getloc("userid"),
		    },
		    (res) => {
		      let data = res.data.data;
		      _this.zzselectlist = data;
		    }
		  );
		},
		Wallet_ZhuanhuanSelect: function() {
			let _this = this;
			_this.$request.post(
				"api/WalletsZhuanhuan/ZhuanhuanSelect", {
					token: _this.$utils.getloc("token"),
					userid: _this.$utils.getloc("userid"),
				},
				(res) => {
					let data = res.data.data;
					_this.zhselectlist = data;
				}
			);
		},
        onClickLeft: function () {
            this.$router.back(-1);
        },

    },
   
};
</script>

<style scoped>
	.wallet-box {
		background: url("../../assets/img/qianbao.png");
		background-size: 100% 100%;
	}
	.btn{
		height: 30px;
		width: 200px;
		margin-top: 15px;
	}
.top .van-cell__value {
    color: #641602;
}
/deep/.van-tabs__line{
	background-color: #49b685;
}
/deep/.van-tab--active {
	color:#49b685 ;
}
/deep/.van-nav-bar .van-icon {
    color: #fff;
}

/deep/.van-nav-bar__title {
    color: #fff;
}

/deep/.van-nav-bar__right {
    color: #fff;
}

/deep/.van-button--primary{
	  background-color: #F7B226;
		border: 1px solid #F7B226;
}

/deep/.van-grid-item__content {
    background-color: transparent;
}

/deep/.van-grid-item__icon {
    color: #fff;
}

/deep/.van-icon-underway::before,
/deep/.van-icon-send-gift::before,
/deep/.van-icon-invition::before {
    color: #fff;
}

/deep/.van-grid-item__text {
    color: #fff;
}
</style>
