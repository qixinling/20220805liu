<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <div v-if="data">
      <div class="cell-title">基本信息</div>
      <van-cell title="订单状态" :value="data.statename" />
      <van-cell title="订单编号" :value="data.holdno" />
      <van-cell title="字画" :value="data.jname" />
      <van-cell title="交易总额" :value="'￥' + data.jprice" />
      <van-cell title="抢购时间" :value="data.qgdate" />
      
      <div class="cell-title">卖家收款方式</div>
      <van-cell title="卖家信息" :value="data.username + '(' + data.usertel + ')'" />
      <van-collapse v-model="activeNames" accordion>
        <van-collapse-item title="卖家账户" name="1" v-for="item in data.banklist" :key="item.id">
          <van-cell center title="账户名称" :value="item.bankname" />
          <van-cell center title="账号" :value="item.bankcard">
            <template #right-icon>
              <img style="padding-left: 10px;" src="../../assets/img/fz.png" @click="CopyLink(item.bankcard)"/>
            </template>
          </van-cell>
          <van-cell center title="姓名" :value="item.username">
            <template #right-icon>
              <img style="padding-left: 10px;" src="../../assets/img/fz.png" @click="CopyLink(item.username)"/>
            </template>
          </van-cell>
          <van-cell center title="电话" :value="item.usertel">
            <template #right-icon>
              <img style="padding-left: 10px;" src="../../assets/img/fz.png" @click="CopyLink(item.usertel)"/>
            </template>
          </van-cell>
          <div style="text-align:center;margin-top:10px">
            <img style="width: 60%;" :src="getimg(item.bankimg)" />
          </div>
        </van-collapse-item>
      </van-collapse>

      <div class="cell-title">付款信息</div>
      <van-cell title="买家信息" :value="data.busername + '(' + data.busertel + ')'" />
      <van-cell title="付款金额" :value="'￥' + data.jprice" />

      <div style="height:10px"></div>
    </div>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";

import Vue from 'vue';
import { Cell, Collapse, CollapseItem } from 'vant';

Vue.use(Cell)
Vue.use(Collapse)
Vue.use(CollapseItem)

export default {
  name: '',
  components: {
    HeadBar,
  },
  data() {
    return {
      title: "订单详情",
      activeNames: "",
      hid: "",
      data: null
    }
  },
  created() {
    if(this.$route.query.id) {
      this.hid = this.$route.query.id
    }
    this.getdata()
  },
  methods: {
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
    getimg: function (img) {
      if (img != "") {
        return this.$config.send_url + "Upload/" + img;
      }
    },
    getdata() {
      let that = this
      that.$request.post(
        "api/UsersHold/Get",
        {
          hid: that.hid,
          token: that.$utils.getloc("token"),
		      userid: that.$utils.getloc("userid"),
			    uid: that.$utils.getloc("id"),
        },
        (res) => {
          if(res.data.code == 0) {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            })
            return 
          }
          let data = res.data.data
          that.data = data[0]
          // console.log(that.data)
        }
      )
    }
  }
}
</script>

<style scoped>
.cell-title {
  font-size: 14px;
  font-weight: 600;
  padding: 10px;
}
</style>