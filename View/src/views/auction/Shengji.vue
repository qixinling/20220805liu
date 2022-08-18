<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <div>
      <div class="cell-title">申请画室</div>
	  <van-cell title="当前级别" :value="us.ulevelname" />
      <van-cell title="今日团队交易额" :value="us.riteamyeji" />
    

      <div class="cell-title">选择级别</div>
      <div class="fenqu">
        <van-radio-group v-model="newulevel">
          <van-cell-group>
            <van-cell v-for="(item,index) in ulevellist" :key="index" :title="item"  clickable @click="newulevel = index" >
              <template #right-icon> 
                <van-radio :name="index" checked-color="#FC4702" />
              </template>
            </van-cell>
          </van-cell-group>
        </van-radio-group>
      </div>
    </div>

    <div style="margin:15px">
      <van-button block color="#FC4702" @click="shenqing">申请</van-button>
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
      newulevel: 0,
      us: {},
      ulevellist: [],
      list: []
    }
  },
  created() {
    this.getuser(),
    this.getulevel();
  },
  methods: {
    getuser() {
				let that = this
				that.$request.post(
					"api/Users/Get", {
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
            that.us = res.data.data
          console.log(that.us);
					});
			},
    getulevel: function () {
        var _this = this;
        _this.$request.post(
            "api/Level/GetUlevelList",
            {},
            (res) => {
                if (res.data.code == 0) {
                    _this.$dialog.alert({
                        title: "提示",
                        message: res.data.msg,
                    });
                    return;
                }
                res.data.data.forEach((item, index) => {
                    if (index > 0) {
                        _this.ulevellist.push(item.name);
                    }
					console.log(_this.ulevellist);
                });
               
            },
            true
        );
    },
    shenqing() {
        //console.log(this.radio);
				let that = this
				that.$request.post(
					"api/UsersLevelup/Add", {
						token: that.$utils.getloc("token"),
						userid: that.$utils.getloc("userid"),
						uid: that.$utils.getloc("id"),
						newulevel:that.newulevel
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