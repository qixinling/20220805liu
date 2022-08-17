<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>
    <van-tabs v-model="active" @change="change">
      <van-tab name="99" title="全部"></van-tab>
      <van-tab name="1" title="已预约"></van-tab>
      <van-tab name="2" title="配对成功"></van-tab>
      <van-tab name="3" title="配单失败"></van-tab>
    </van-tabs>

    <div>
      <div class="item-block" v-for="(item,index) in list" :key="index">
        <div class="item-block-row">
          <div style="font-weight:600">{{item.hsname}}</div>
          <div style="font-size:12px;color:#777">配对成功</div>
        </div>
		<div style="font-size:12px;color:#777">预约人：{{item.busername}}</div>
        <div style="font-size:12px;color:#777">预约时间：{{item.qgdate}}</div>
        <div style="font-size:12px;color:#777" v-if="item.minprice != item.maxprice">预约范围：{{item.minprice}}-{{item.maxprice}}</div>
		<div style="font-size:12px;color:#777" v-if="item.minprice == item.maxprice">预约范围：{{item.maxprice}}以上</div>
      </div>
    </div>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";

import Vue from 'vue';
import { Tab, Tabs } from 'vant';

Vue.use(Tab);
Vue.use(Tabs);

export default {
  name: '',
  components: {
    HeadBar
  },
  data() {
    return {
      title: document.title,
      active: "99",
	  lx:'',
	  list:[]
    }
  },
  created(){
	  this.lx = this.$route.query.lx
	  console.log(this.lx);
	  this.getdata();
  },
  methods: {
    change(val) {
		if(val == 99 || val == 2){
			this.getdata();
		}else{
			this.list=[];
		}
      //console.log(val)
    },
	getdata: function() {
		let _this = this;
		this.$request.post(
			"Api/UsersHold/YList", {
				token: _this.$utils.getloc("token"),
				userid: _this.$utils.getloc("userid"),
				uid:_this.$utils.getloc("id"),
				lx:_this.lx
			},
			(res) => {
				if (res.data.code == 0) {
				    _this.$dialog.alert({
				        title: "提示",
				        message: res.data.msg,
				    });
				    return;
				}
				_this.list = res.data.data;
				console.log(_this.list);
			}
		);
	},
  }
}
</script>

<style scoped>
.item-block {
  padding: 10px;
  border-bottom: 1px solid #ccc;
  background-color: #fff;
  font-size: 14px;
}

.item-block-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 6px;
}
</style>