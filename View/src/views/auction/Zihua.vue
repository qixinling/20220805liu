<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <van-tabs title-active-color="#FC4702" v-model="active" @change="change">
      <van-tab title="正常字画" name="0"></van-tab>
      <van-tab title="封存字画" name="1"></van-tab>
    </van-tabs>

    <div style="border-top: 2px solid #ccc;">
      <div class="item-block" v-for="(item,index) in list" :key="index">
        <div class="item-header">
          <div>编号：{{ item.holdno }}</div>
          <div style="color:#FC4702">{{ item.statename }}</div>
        </div>
        <div class="goods">
          <img :src="getimg(item.jimg)" />
          <div class="goods-content">
            <div>{{ item.jname }}</div>
            <div>持有人 {{item.username}}({{ item.usertel }})</div>
            <!-- <div>买入价值 1025.00</div> -->
            <!-- <div>卖出价值 1025.00</div> -->
            <div>价值 {{ item.jprice }}</div>
          </div>
        </div>
        <div class="btn-wrap">
          <div class="btn" v-if="active == 0" @click="showChaifenDialog(item.id)">拆分</div>
          <!-- <div class="btn" v-if="active == 0">转场</div> -->
          <div class="btn" v-if="active == 0" @click="fengcun(item.id)">封存</div>
          <div class="btn" v-if="active == 1"  @click="Jiefeng(item.id)">解封</div>
        </div>
      </div>
      <van-empty description="暂无数据" v-if="list.length == 0" />
    </div>

    <!-- 拆分弹窗 -->
    <van-dialog v-model="show" title="拆分" show-cancel-button confirmButtonText="确定拆分" @confirm="chaifen">
      <div style="padding:10px">
        <van-field v-model="chaifenNum" :border="true" label="拆分数量" placeholder="请输入要拆分的数量" />
      </div>
    </van-dialog>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";

import Vue from 'vue';
import { Tab, Tabs, Empty, Dialog, Field } from 'vant';

Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Empty);
Vue.use(Field);
Vue.use(Dialog);

export default {
  name: '',
  components: {
    HeadBar
  },
  data() {
    return {
      title: document.title,
      active: "0",
      list: [],
      show: false,
      chaifenNum: "",
      hid: ""
    }
  },
  created() {
    this.getData(this.active)
  },
  methods: {
    change() {
      this.list = []
      this.getData(this.active)
    },
    getimg: function(img) {
			if (img != "") {
				return this.$config.send_url + "Upload/" + img;
			}
		},
    showChaifenDialog(id) {
      this.hid = id
      this.show = true
    },
    getData(lx) {
      let that = this
      that.$request.post(
        "api/UsersHold/ZhglList",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
          uid: that.$utils.getloc("id"),
          lx: lx
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
          that.list = data
          console.log(that.list)
        }
      )
    },
    // 封存
    fengcun(id) {
      let that = this
      Dialog.confirm({
      title: '提示',
      message: '确定要封存吗？',
    })
      .then(() => {
        that.$request.post(
          "api/UsersHold/Fengcun",
          {
            token: that.$utils.getloc("token"),
            userid: that.$utils.getloc("userid"),
            uid: that.$utils.getloc("id"),
            hid: id
          },
          (res) => {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
            that.getData(that.active)
          }
        )
      })
    },
	Jiefeng(id) {
	  let that = this
	  Dialog.confirm({
	  title: '提示',
	  message: '确定要解封吗？',
	})
	  .then(() => {
	    that.$request.post(
	      "api/UsersHold/Jiefeng",
	      {
	        token: that.$utils.getloc("token"),
	        userid: that.$utils.getloc("userid"),
	        uid: that.$utils.getloc("id"),
	        hid: id
	      },
	      (res) => {
	        that.$dialog.alert({
	          title: "提示",
	          message: res.data.msg,
	        });
	        that.getData(that.active)
	      }
	    )
	  })
	},
	
    // 拆分
    chaifen() {
      let that = this
      if(that.chaifenNum == "") {
        that.$dialog.alert({
          title: "提示",
          message: "请输入要拆分的数量",
        });
        return
      }
      that.$request.post(
        "api/UsersHold/chaifen",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
          uid: that.$utils.getloc("id"),
          hid: that.hid,
		  num:that.chaifenNum
        },
        (res) => {
          that.$dialog.alert({
            title: "提示",
            message: res.data.msg,
          });
          that.chaifenNum = ""
          that.getData(that.active)
        }
      )
    }
  }
}
</script>

<style scoped>
.item-block {
  padding: 10px;
  background-color: #fff;
  border-bottom: 4px solid #ccc;
  font-size: 14px;
}

.item-header {
  margin-bottom: 10px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.goods {
  padding: 10px 0;
  display: flex;
  align-items: center;
  border-top: 1px solid #ccc;
  border-bottom: 1px solid #ccc;
  font-size: 14px;
}

.goods img {
  width: 120px;
  border-radius: 6px;
}

.goods-content {
  flex: 1;
  padding-left: 6px;
}

.btn-wrap {
  margin-top: 10px;
  display: flex;
  justify-content: flex-end;
}

.btn {
  margin-left: 6px;
  padding: 2px 6px;
  border-radius: 6px;
  background-color: #FC4702;
  color: #fff;
}

/deep/.van-tabs__line {
    background-color: #FC4702;
}
</style>