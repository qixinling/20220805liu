<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>
    <!-- <div class="header-cell">
      <div class="header-left">画室</div>
      <div class="header-cell-center" @click="showCalendar = true">
        <div>新起点</div>
        <van-icon name="arrow" color="gray" />
      </div>
      <div class="header-cell-btn1">统计</div>
    </div> -->
    <div class="header-cell">
      <div class="header-left">选择日期</div>
      <div class="header-cell-center" @click="showCalendar = true">
        <div>{{showDateLabel}}</div>
        <van-icon name="arrow" color="gray" />
      </div>
      <div class="header-cell-btn2" @click="exportXls">下载报表</div>
    </div>
    <van-calendar color="#1989fa" :min-date="minDate" :max-date="maxDate" v-model="showCalendar" @confirm="onConfirm" />

    <div class="jine-block">
      <van-row>
        <van-col span="12">订单数：{{ data.count }}</van-col>
        <van-col span="12">金额：{{ data.zjine }}</van-col>
        <van-col span="12">上架费：{{ data.sjjine }}</van-col>
      </van-row>
    </div>

    <div>
      <div class="order-item" v-for="(item,index) in data.hlist" :key="index">
        <div class="order-item-header">
          <div>订单号：{{ item.holdno }}</div>
          <div style="color:#FC4702">{{ item.statename }}</div>
        </div>
        <div class="order-goods">
          <div class="goods-name">{{ item.jname }}</div>
          <div class="maijia-label"><span>买家</span>{{ item.busername }}({{item.busertel}})</div>
          <div class="maijia-label2"><span>卖家</span>{{item.username}}({{item.usertel}})</div>
        </div>
        <div class="mairu-date">
          <div>交易时间：{{ item.dkdate }}</div>
          <span>价值 ￥{{ item.jprice }}</span>
        </div>
        <div class="queren-date">
          <div>确认时间：{{ item.skdate }}</div>
          <span>上架费 ￥{{ item.sjjine }}</span>
        </div>
      </div>
      <van-empty description="暂无数据" v-if="data.hlist && data.hlist.length == 0" />
    </div>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";

import Vue from 'vue';
import { Col, Row, Calendar, Empty } from 'vant';

import * as XLSX from 'xlsx';

Vue.use(Col);
Vue.use(Row);
Vue.use(Calendar);
Vue.use(Empty);

export default {
  name: '',
  components: {
    HeadBar
  },
  data() {
    return {
      title: document.title,
      showCalendar: false,
      showDateLabel: "",
      minDate: new Date(2022, 0, 1),
      maxDate: new Date(),
      data: {},
      totalJine: 0
    }
  },
  created() {
    let date = new Date()
    this.showDateLabel = this.formatDate(date)
    this.getData(this.showDateLabel)
  },
  methods: {
    formatDate(date) {
      return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
    },
    onConfirm(date) {
      this.showDateLabel = this.formatDate(date)
      this.showCalendar = false
      this.getData(this.showDateLabel)
    },
    getData(time) {
      let that = this
      that.$request.post(
        "api/UsersHold/TjList",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
          uid: that.$utils.getloc("id"),
          time: time
        },
        (res) => {
          if (res.data.code == 0) {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
            return;
          }
          let data = res.data.data
          that.data = data
          console.log(that.data)
        }
      )
    },
    // 下载报表
    exportXls() {
      // 模拟数据
      let list = this.data.hlist
      console.log(list)
      let data = []
      data[0] = ["订单号", "字画", "买方", "卖方", "金额", "交易时间", "收款时间", "状态"],
      list.forEach(item => {
        data.push([
          item.holdno,
          item.jname,
          item.busername,
          item.username,
          item.jprice,
          item.dkdate,
          item.skdate,
          item.statename
        ])
      })
      //1. 新建一个工作簿
      let workbook = XLSX.utils.book_new();
      //2. 生成一个工作表，
      let sheet = XLSX.utils.aoa_to_sheet(data);
      // 文件名
      let date = new Date()
      let xlsDate = `${date.getFullYear()}${date.getMonth() + 1}${date.getDate()}${date.getHours()}${date.getMinutes()}${date.getSeconds()}`
      let xlsName = `画室统计${xlsDate}.xlsx`
      XLSX.utils.book_append_sheet(workbook, sheet, 'sheetName1'); //工作簿名称
      XLSX.writeFile(workbook, xlsName); // 保存的文件名
    }
  }
}
</script>

<style scoped>
.header-cell {
  display: flex;
  align-items: center;
  padding: 10px;
  background-color: #fff;
  font-size: 15px;
}


.header-cell:nth-child(2) {
  border-bottom: 1px solid #ccc;
}

.header-cell-center {
  padding: 0 20px;
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.header-left {
  width: 70px;
}

.header-cell-btn1 {
  padding: 5px 0;
  width: 70px;
  text-align: center;
  color: #fff;
  border-radius: 6px;
  background-color: #4269E2;
}

.header-cell-btn2 {
   padding: 5px 0;
  width: 70px;
  text-align: center;
  color: #fff;
  border-radius: 6px;
  background-color: #018102;
}

.jine-block {
  padding: 10px;
  background-color: #FE8D27;
  font-size: 15px;
  font-weight: 600;
}

.goods-name {
  font-weight: 700;
  margin-bottom: 4px;
  font-size: 15px;
}

.maijia-label {
  margin-bottom: 4px;
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

.order-item {
  padding: 10px;
  background-color: #fff;
  font-size: 14px;
  /* font-weight: 600; */
  border-bottom: 4px solid #ccc;
}

.order-item-header {
  margin-bottom: 10px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.order-goods {
  padding: 10px;
  border-bottom: 1px solid #ccc;
  border-top: 1px solid #ccc;
}

.mairu-date {
  padding: 6px 0;
  display: flex;
  align-items: center;
  justify-content: space-between;
  border-bottom: 1px solid #ccc;
}

.mairu-date span {
  color: #FC4702;
}

.queren-date {
  padding: 6px 0;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.queren-date span {
  color: #027D04;
}
</style>