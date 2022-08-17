<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <div class="cardBox">
      <div class="jiangjin-box">
        <div class="label">
          <van-icon name="balance-o" size="20" color="#f7b226"/>
          <span>奖金余额</span>
        </div>
        <div class="balance">
        {{mey.toFixed(2)}}
        </div>
      </div>
    </div>

    <div style="padding: 10px">
      <van-row class="card" gutter="10">
        <van-col span="12" v-for="(item,index) in data" :key="index">
          <div class="card-item">
            <img :src="getimg(item.img)" />
            <div class="card-name">{{item.name}}</div>
            <div class="card-info">
              <div>价值：{{item.minprice}}-{{item.maxprice}}</div>
             <!-- <div>开抢时间：{{item.qdate}}</div> -->
              <div>周期：{{item.zhouqi}}</div>
              <div>日收益：{{item.rishouyi}}%</div>
              <!-- <div>押金：{{item.jindan}}</div> -->
            </div>
            <van-button color="#F4C607" size="small" block @click="onYuyue(item.id)">购买</van-button>
          </div>
        </van-col>
      </van-row>
    </div>

    <Login></Login>
    <!-- <BottomBar /> -->
  </div>
</template>

<script>
import Login from "@/components/Login";
import BottomBar from "@/components/BottomBar";
import HeadBar from "@/components/HeadBar";

import Vue from "vue";
import { Swipe, SwipeItem, Col, Row, Button,Dialog } from "vant";

Vue.use(Swipe);
Vue.use(SwipeItem);
Vue.use(Col);
Vue.use(Row);
Vue.use(Button);
Vue.use(Dialog);

export default {
  name: "",
  components: {
    BottomBar,
    Login,
    HeadBar
  },
  data() {
    return {
      title: document.title,
      slidelist: [],
      data: [],
	  mey:0
    };
  },
  created() {
    this.load()
  },
  methods: {
    load: function () {
      if (this.$store.state.LoginState) {
      
        this.getdata();
		this.Wallet_GetWallet();
      }
    },
    getimg: function (img) {
      if (img != "") {
        return this.$config.send_url + "Upload/" + img;
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
              var data = res.data.data;
			  data.forEach(item=>{
				  if(item.cid == 1){
					  _this.mey = item.jine;
				  }
			  })
           }
       );
   },
    getdata() {
      let _this = this
      this.$request.post("Api/Constellation/List",
        {
          token: _this.$utils.getloc("token"),
          userid: _this.$utils.getloc("userid"),
        },
        (res) => {
          if(res.data.code == 0) {
            _this.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            })
            return 
          }
          _this.data = res.data.data
        }
      )
    },
    // 购买
    onYuyue(id) {
		Dialog.confirm({
		    title: "购买",
		    message: "确定要购买吗？",
		}).then(() => {
			let _this = this;
			this.$request.post("Api/UsersHold/Buy",
			  {
			    token: _this.$utils.getloc("token"),
			    userid: _this.$utils.getloc("userid"),
			    uid: _this.$utils.getloc("id"),
			    cid: id
			  },
			  (res) => {
			    _this.$dialog.alert({
			      title: "提示",
			      message: res.data.msg,
			    })
			  }
			)
		})

    }
  },
};
</script>

<style scoped>
.van-swipe img {
  width: 100%;
}

/* .box .van-col {
  padding: 8px;
} */

.card-item {
  padding: 10px;
  font-size: 12px;
  border-radius: 6px;
  color: #ccc;
  background-color: #3b3e47;
  margin-bottom: 10px;
}

.card-name {
  color: #3fceb3;
  font-size: 16px;
  text-align: center;
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

.jiangjin-box {
  display: flex;
  flex-direction: column;
  padding: 30px 20px;
  /* align-items: center; */
  border-radius: 10px;
  background-image: linear-gradient(to right top, #3b3e47, #41434d, #474854, #4d4e5a, #535361);
}

.balance {
  display: flex;
  justify-content: flex-end;
  align-items: center;
  font-size: 24px;
  font-weight: 600;
  color: #F7B226;
}

.balance span {
  margin-left: 4px;
}

.label {
  font-size: 16px;
  display: flex;
  align-items: center;
}

.label span {
  margin-left: 2px;
}
</style>