<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <div class="cardBox">
      <div class="cardContent" v-for="(item,index) in data" :key="index">
        <van-row>
          <van-col span="6">
            <img :src="getimg(item.cimg)"/>
          </van-col>
          <van-col span="18">
            <div class="textcolor" style="font-size: 15px;">
			{{item.cname}}
			<span style="float: right;font-size: 12px;color:#E6A23C;" v-if="item.state == 0">预约中</span>
			<span style="float: right;font-size: 12px;color:#E6A23C;" v-if="item.state == 1">预约成功</span>
			</div>
            <div>价值：{{item.cminprice}}-{{item.cmaxprice}}</div>
            <div>日收益：{{item.crishouri}}%</div>
            <div>周期：{{item.czhouqi}}</div>
            <div v-if="item.state == 0">开抢时间</div>
			<div v-if="item.state == 0">{{item.cqdate}}-{{item.cedate}}</div>
          </van-col>
        </van-row>
      </div>
    </div>

    <van-empty description="暂无记录" v-if="data.length == 0"/>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";

import Vue from 'vue';
import { Col, Row, Empty } from 'vant';

Vue.use(Col);
Vue.use(Row);
Vue.use(Empty);

export default {
  name: '',
  components: {
    HeadBar
  },
  data () {
    return {
      title: document.title,
      data: []
    }
  },
  created() {
    this.getdata()
  },
  methods: {
    getimg: function (img) {
      if (img != "") {
				console.log(img)
        return this.$config.send_url + "Upload/" + img;
      }
    },
    getdata() {
      let _this = this
      this.$request.post("Api/UsersYuyue/List", 
        {
          lx: 0,
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
          _this.data = res.data.data[0].data
        }
      )
    }
  }
}
</script>

<style scoped>
.cardContent {
  margin-bottom: 10px;
}

.cardContent img {
  width: 100%;
  height: 100px;
}

.cardContent .van-row {
  font-size: 13px;
}

.van-row .van-col:last-child {
  padding-left: 10px;
}
.van-row .van-col:last-child div {
  padding: 1px 0;
}
</style>