<template>
  <div class="home">
    <div class="header-nav">
      {{headerDate}}
    </div>
	  <div class="bjdiv">
    

	<!-- <van-notice-bar left-icon="volume-o" :text="marqueemsg" /> -->

  <div class="order-block">
    <div class="block-title">
      <van-icon name="bars" size="16" color="gray" style="margin-right:3px" />
      订单列表
    </div>
    <div class="block-row">
      <div @click="tolink('Mygoods?state=1')">
        <img src="@/assets/img/order1.png" width="70" height="70" />
        <div>等待付款</div>
      </div>
      <div @click="tolink('Mygoods?state=2')">
        <img src="@/assets/img/order2.png" width="70" height="70" />
        <div>收款确认</div>
      </div>
      <div @click="tolink('Mygoods?state=3')">
        <img src="@/assets/img/order3.png" width="70" height="70" />
        <div>委托代卖</div>
      </div>
    </div>
  </div>

  <!-- <div class="imgbox">
    <img src="@/assets/img/banner2.png" style="width:100%;height:120px" />
  </div> -->
  <div style="padding: 15px 0px;">
	  <van-swipe class="my-swipe" :autoplay="3000" indicator-color="white">
	    <van-swipe-item v-for="(item, index) in slidelist" :key="index">
	      <div>
	        <img :src="getimg(item.img)" />
	      </div>
	    </van-swipe-item>
	  </van-swipe>
  </div>

  <div class="shop-block">
    <div class="block-title">
      <van-icon name="bars" size="16" color="gray" style="margin-right:3px" />
      画宛商城
    </div>
    <div class="block-row">
      <div @click="tolink('QiangPaiQu')">
        <img src="@/assets/img/nav1.png" width="70" height="70" />
        <div style="font-size: 16px;">{{usinfo.mystudioname}}</div>
      </div>
      <div @click="KfClick()">
        <img src="@/assets/img/nav2.png" width="70" height="70" />
        <div>体验区</div>
      </div>
      <div  @click="tolink('yuyue')">
        <img src="@/assets/img/nav3.png" width="70" height="70" />
        <div>预约区</div>
      </div>
    </div>
  </div>

  <div class="huajia-block">
    <div class="block-title">
      <van-icon name="bars" size="16" color="gray" style="margin-right:3px" />
      画家资料
    </div>
    <div class="huajia-list">
      <div class="huajia-item" v-for="item in tlist" :key="item.id" @click="tolink('TeachersDetails?tid=' + item.id)">
        <img :src="getimg(item.tximg)" width="80" height="80"/>
        <div>{{item.name}}</div>
      </div>
    </div>
  </div>

  <div class="imgbox">
    <img src="@/assets/img/banner3.jpg" style="width:100%;height:120px" />
  </div>
	
	<!-- <div style="margin-top: 20px; text-align: center;padding: 10px;">
		<div style="padding: 20px 10px;background-color: #fffbe8;border-radius: 10px;">
			<div style="font-weight: 600;display: flex;align-items: center;justify-content: center;">
				<van-icon name="bars" size="20" />
				画宛商城
			</div>
			<van-row style="padding: 10px;padding-top: 30px;">
				<van-col span="8">
					<van-icon name="gem-o" size="45" @click="tolink('QiangPaiQu')" />
					<div>新起点</div>
				</van-col>
				<van-col span="8">
					<van-icon name="gem-o" size="45" @click="KfClick()"/>
					<div>体验区</div>
				</van-col>
				<van-col span="8">
					<van-icon name="gem-o" size="45" @click="KfClick()"/>
					<div>待开放</div>
				</van-col>
				
			</van-row>
		</div>
	</div> -->
</div>

    <Login></Login>
    <BottomBar />
  </div>
</template>

<script>
import Login from "@/components/Login";
import BottomBar from "@/components/BottomBar";

import Vue from "vue";
import { Swipe, SwipeItem, Col, Row, Button,NoticeBar, CountDown } from "vant";
import { Popup,Toast,Dialog } from 'vant';

Vue.use(Popup);
Vue.use(Swipe);
Vue.use(SwipeItem);
Vue.use(Col);
Vue.use(Row);
Vue.use(Button);
Vue.use(NoticeBar);
Vue.use(CountDown);
Vue.use(Toast);
Vue.use(Dialog);

export default {
  name: "",
  components: {
    BottomBar,
    Login
  },
  data() {
    return {
      slidelist: [],
      tlist: [],
	    marqueemsg: "",
	    cshow:false,
	    sshow:false,
      time: 30 * 60 * 60 * 1000,
      headerDate: "",
	  usinfo:{}
    };
  },
  created() {
    this.load();
    this.headerDate = this.getHeaderDate()
  },
  methods: {
	  load:function(){
		  this.getslide();
		 this.getteachers();
		  this.getgg();
		  if (this.$store.state.LoginState) {
			  this.getuser();
		  }
	  },
    tolink(path) {
      this.$router.push(path)
    },
	KfClick:function(){
		Toast('暂未开放');
	},
	getuser: function () {
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
	        }
	    );
	},
	getteachers: function () {
	    var _this = this;
	    _this.$request.post(
	        "api/Teachers/List",
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
	            _this.tlist = res.data.data;
				console.log( _this.tlist);
	          
	        }
	    );
	},
    getimg: function (img) {
      if (img != "") {
        return this.$config.send_url + "Upload/" + img;
      }
    },
    slideTo: function (id, lx, url) {
        if (lx == 2) {
            this.$router.push({
                name: "Goods",
                query: {
                    id: id,
                },
            });
        } else if (lx == 3) {
            this.$router.push({
                name: "NewsContent",
                query: {
                    id: id,
                },
            });
        } else if (lx == 4) {
            window.open(url);
        } else if (lx == 5) {
            this.$router.push({
                name: "GoodsSearch",
                query: {
                    id: id,
                },
            });
        }
    },
    getgg: function () {
        var _this = this;
        _this.$request.post("Api/News/Gonggao", {}, (res) => {
            if (res.data.code == 0) {
                _this.$dialog.alert({
                    title: "提示",
                    message: res.data.msg,
                });
                return;
            }
            _this.marqueemsg = res.data.data;
        });
    },
    getslide() {
      let _this = this
      this.$request.post("Api/Slide/List",
        {
          lx: 1
        },
        (res) => {
          if(res.data.code == 0) {
            _this.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            })
            return 
          }
          _this.slidelist = res.data.data
        }
      )
    },
    // 获取场次
    getdata() {
      let that = this
      that.$request.post("api/Site/List",
      {},
      (res) => {
        if(res.data.code == 0) {
          that.$dialog.alert({
            title: "提示",
            message: res.data.msg,
          })
          return 
        }
        let data = res.data.data
		//console.log(data);
        data.forEach(item => {
          let sdateArr = item.sdate.split(":")
          // 场次开始时间的毫秒数
          let sdateMiner = this.toMiner(sdateArr[0], sdateArr[1], sdateArr[2])
          // 获取当前时间 (24小时制)
          let current = new Date().toLocaleTimeString('chinese', { hour12: false })
          let currentArr = current.split(":")
          // 获取当前时间毫秒数
          let currentMiner = this.toMiner(currentArr[0], currentArr[1], currentArr[2])
          // 获取抢拍结束时间毫秒数
          let edateArr = item.edate.split(":")
          let edateMiner = this.toMiner(edateArr[0], edateArr[1], edateArr[2])
          item.downDate =  sdateMiner - currentMiner
          // 当倒计时结束并且当前时间已超过抢拍时间时重新计算距离下一次抢拍时间
          if(item.downDate <= 0 && currentMiner >= edateMiner) {
            item.downDate = 86400000 - currentMiner + sdateMiner
          }
        })
        that.data = data
      })
    },
    jiaoge() {
    	var that = this;
    	Dialog.confirm({
    			title: '提示',
    			message: '请先交割一单在进行抢购',
    		})
    		.then(() => {
    			that.$request.post(
    				"api/UsersHold/QxJiaoge", {
    					token: that.$utils.getloc("token"),
    					userid: that.$utils.getloc("userid"),
    					uid: that.$utils.getloc("id"),
    				},
    				(res) => {
    					if (res.data.code == 0) {
    						Toast.fail(res.data.msg);
    						return
    					}
    					Toast.fail(res.data.msg);
    					that.$router.push("/Mygoods")
    				}
    			)
    		})
    		.catch(() => {
    			
    		});
    		
    },
 //    tolink(name) {
	// 		this.$router.push({
	// 		  path: name,
			 
	// 		})
	
 //    },
    // 获取毫秒数
    toMiner(hour, min, seconds) {
      var miner;
      miner = hour * (60 * 60 * 1000) + min * (60 * 1000) + seconds * 1000;
      return miner;
    },
    getHeaderDate() {
      let now = new  Date();
      let y = now.getFullYear();
      let m = now.getMonth() + 1 ;
      let d = now.getDate();
      m = m < 10 ? "0" + m:m;
      d = d < 10 ? "0" + d:d;
      return  y + "-" + m + "-" + d;
    }
  }
};
</script>

<style scoped>
.home {
  min-height: calc(100vh - 50px);
  background-color: #fff;
}

.van-swipe img {
  width: 100%;
}
.bjdiv{
  padding-top: 40px;
	width: 100%;
	color: #fff;
	color: #000;
	
}
.card-item {
  padding: 10px;
  font-size: 12px;
  border-radius: 6px;
  /* color: #ccc; */
  background-color: #fff;
  margin-bottom: 10px;
}

.card-name {
  color: #000;
  font-size: 13px;
  /* text-align: center; */
  padding: 6px 0;
}

.card-item img {
  width: 100%;
  height: 150px;
}

.card-info div {
  padding: 1px 0;
}

.van-button {
  margin-top: 10px;
}

.card-jine span {
  color: red;
  font-size: 14px;
  font-weight: 600;
}

.list {
  padding: 10px;
}

.item {
  position: relative;
  width: 100%;
  margin-bottom: 10px;
  color: #fff;
  background: url("../assets/img/cardbg.jpg");
  background-size: 100% 100%;
  border-radius: 10px;
  overflow: hidden;
}

.item:nth-child(1) {
  background: url("../assets/img/card1.png");
  background-size: 100% 100%;
}

.item:nth-child(2) {
  background: url("../assets/img/card2.png");
  background-size: 100% 100%;
}

.item:nth-child(3) {
  background: url("../assets/img/card3.png");
  background-size: 100% 100%;
}

.item:nth-child(4) {
  background: url("../assets/img/card4.png");
  background-size: 100% 100%;
}

.item-box {
  padding-top: 24px;
  height: 120px;
}

.overlay {
  background-color: rgba(0,0,0,.6);
}

.item-content {
  padding: 16px 0;
  font-size: 13px;
  text-align: center;
}

.item-shang {
  position: absolute;
  font-size: 13px;
  left: 0;
  top: 0;
  background-color: rgb(73, 106, 148);
  padding: 6px 10px;
  border-bottom-right-radius: 10px;
}

.item-bnt {
  padding: 15px 0;
}

.item-bnt div {
  display: inline-block;
  padding: 4px 20px;
  background-color: #ee0a24;
  border-radius: 6px;
}

.count {
  display: flex;
  justify-content: center;
  align-items: center;
}

.colon {
  display: inline-block;
  margin: 0 4px;
  color: #fff;
}

.block {
  display: inline-block;
  width: 22px;
  color: #000;
  font-size: 12px;
  text-align: center;
  background-color: #fff;
  border-radius: 4px;
}

.content-msg {
  font-size: 18px;
padding-bottom: 12px;
color: red;
font-weight: bold;
}

.header-nav {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  height: 40px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 16px;
  z-index: 98;
  background-color: #fff;
}

.block-title {
  padding: 10px;
  margin-bottom: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.block-row {
  display: flex;
  align-items: center;
  justify-content: space-around;
  font-size: 14px;
  text-align: center;
}

.block-row > div {
  margin-bottom: 5px;
}

.imgbox {
  margin-top: 15px;
}

.shop-block {
  margin: 10px;
}

.huajia-block {
  margin: 30px 10px 10px;
}

.huajia-list {
  white-space: nowrap;
  overflow-y: auto;
  text-align: center;
}

.huajia-item {
  display: inline-block;
  width: 30%;
  text-align: center;
  font-size: 14px;
}

.huajia-item img {
  border-radius: 8px;
}
</style>