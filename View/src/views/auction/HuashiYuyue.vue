<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>
    <van-tabs v-model="active" @change="change">
      <van-tab title="全部"></van-tab>
      <van-tab title="已预约"></van-tab>
      <van-tab title="配单成功"></van-tab>
      <van-tab title="配单失败"></van-tab>
    </van-tabs>

    <div>
      <div class="item-block" v-for="(item,index) in 2" :key="index">
        <div class="item-row">
          <div style="font-weight:600">新起点</div>
          <div style="font-size:12px;color:#777">配对成功</div>
        </div>
        <div style="font-size:12px;color:#777">预约人：admin(1415635984)</div>
        <div style="font-size:12px;color:#777">预约时间：2022-07-13 14:32:00</div>
        <div style="font-size:12px;color:#777">预约范围：5000-8000</div>
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
      active: "1"
    }
  },
  methods: {
    change(val) {
      console.log(val)
    }
  }
}
</script>

<style scoped>
.item-block {
  padding: 10px;
  background-color: #fff;
  border-bottom: 1px solid #ccc;
  font-size: 14px;
}

.item-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 8px;
}
</style>