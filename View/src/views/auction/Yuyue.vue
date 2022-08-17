<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <div>
      <div class="cell-title">预约画室</div>
      <van-cell title="预约画室" :value="hsus.studioName" />
      <van-cell title="预约画室" value="营业中" />
      <van-cell title="营业时间" :value="yingyeDate" />

      <div class="cell-title">到账账户</div>
      <div class="fenqu">
        <van-radio-group v-model="radio">
          <van-cell-group>
            <van-cell v-for="item in list" :key="item.id" :title="item.name" :value="item.minprice != item.maxprice ? item.minprice + '-' + item.maxprice : item.maxprice+'以上'" clickable @click="radio = item.id" >
              <template #right-icon> 
                <van-radio :name="item.id" checked-color="#FC4702" />
              </template>
            </van-cell>
          </van-cell-group>
        </van-radio-group>
      </div>
    </div>

    <div style="margin:15px">
      <van-button block color="#FC4702" @click="yuyue">确定预约</van-button>
    </div>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";

import Vue from 'vue';
import { Cell, RadioGroup, Radio, CellGroup } from 'vant';

Vue.use(Cell)
Vue.use(RadioGroup)
Vue.use(Radio)
Vue.use(CellGroup)

export default {
  name: '',
  components: {
    HeadBar
  },
  data() {
    return {
      title: document.title,
      radio: "1",
      hsus: {},
      slist: [],
      list: []
    }
  },
  created() {
    this.getsite(),
    this.getPricerange()
  },
  methods: {
    getsite() {
				let that = this
				that.$request.post(
					"api/Site/Get", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						uid: that.$utils.getloc("id"),
					},
					(res) => {
						if (res.data.code == 0) {
							that.$dialog.alert({
								title: "提示",
								message: res.data.msg,
							})
							return
						}
            let data = res.data.data
            that.hsus = data.hsus
            that.slist = data.slist
					});
			},
    getPricerange() {
      let that = this
      that.$request.post(
        "api/Pricerange/List",
        {},
        (res) => {
          if (res.data.code == 0) {
            that.$dialog.alert({
							title: "提示",
							message: res.data.msg,
						})
						return
          } else {
            let data = res.data.data
            that.list = data
            if(that.list.length > 0) {
              that.radio = that.list[0].id
            }
          }
        }
      )
    },
    yuyue() {
        //console.log(this.radio);
				let that = this
				that.$request.post(
					"api/UsersHold/yuyue", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						uid: that.$utils.getloc("id"),
						pid: that.radio
					},
					(res) => {
						that.$dialog.alert({
							title: "提示",
							message: res.data.msg,
						});
					}
				)
    }
  },
  computed: { 
    yingyeDate() {
      if(this.slist.length > 0) {
        return this.slist[1].sdate + '-' + this.slist[1].edate
      } else {
        return ""
      }
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

.fenqu .van-cell__value {
  padding-right: 6px;
}

  /deep/.van-icon .van-icon-success {
		background-color: #FC4702;
		border-color: #FC4702;
	}
</style>