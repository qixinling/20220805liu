<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <van-tabs 
      v-model="active" 
      title-active-color="#e89715" 
      :border="false"
      @change="change"
    >
      <van-tab title="待转让" name="1"/>
      <van-tab title="出售中" name="2"/>
      <van-tab title="已出售" name="3"/>
    </van-tabs>

    <div class="cardBox" v-if="active == 1">
      <div class="cardContent" v-for="(item,index) in data" :key="index">
        <van-row>
			<van-col span="24" style="border-bottom: 1px solid #6c6c6e;padding: 10px;">
				编号：{{item.holdno}}			 
			</van-col>
          <van-col span="8" style="padding-top: 10px;">
            <img :src="getimg(item.cimg)"/>
          </van-col>
          <van-col span="16" style="padding-top: 10px;">
            <div>名称：<span class="textcolor">{{item.cname}}</span></div>
            <div>价值：<span class="textcolor">{{item.cminprice}}-{{item.cmaxprice}}</span></div>
            <div>当前价值：<span class="textcolor">{{item.cprice}}</span></div>
			<div>获得收益：<span class="textcolor">{{item.zshouyi}}</span></div> 
            <div>收益：<span class="textcolor">{{item.zhouqi}}/{{item.rishouyi * item.zhouqi}}%</span></div>
          </van-col>
        </van-row>
      </div>
    </div>

    <div class="cardBox" v-if="active == 2">
      <div class="cardContent" v-for="(item,index) in data" :key="index">
        <van-row>
			<van-col span="24" style="border-bottom: 1px solid #6c6c6e;padding: 10px;">
				编号：{{item.holdno}}			 
			</van-col>
          <van-col span="8" style="padding-top: 10px;">
            <img :src="getimg(item.cimg)"/>
          </van-col>
          <van-col span="16" style="padding-top: 10px;">
            <div class="textcolor">{{item.cname}}</div>
            <div>价值：{{item.cminprice}}-{{item.cmaxprice}}</div>
            <div>当前价值：{{item.cprice}}</div>
            <div>日收益：{{item.rishouyi}}%</div>
            <div>周期：{{item.zhouqi}}</div>
          </van-col>
        </van-row>
        <div class="cell-box">
          <van-cell title="买家姓名" :value="item.busername"/>
          <van-cell title="买家电话" :value="item.busertel" />
        </div>
        <div class="upload">
          <div>买家支付凭证</div>
          <img :src="getimg(item.zhimg)" style="width: 100%;height: 250px;"/>
        </div>
        <div class="flex-box">
          <van-button style="background-color: #f7b226; color: #fff" size="small" @click="skclick(item.id)">确认收款</van-button>
        </div>
      </div>
    </div>
	<van-action-sheet v-model="txk" title="请输入支付密码" :close-on-click-overlay="false">
	    <div class="content">
	        <van-form @submit="shoukuan">
	            <van-field v-model="password2" type="password" name="支付密码" label="支付密码" placeholder="支付密码" :rules="[{ required: true, message: '请填写支付密码' }]" />
	            <div style="margin: 16px">
	                <van-button round block type="info">确认收款</van-button>
	            </div>
	        </van-form>
	    </div>
	</van-action-sheet>

    <div class="cardBox" v-if="active == 3">
      <div class="cardContent" v-for="(item,index) in data" :key="index">
        <van-row>
			<van-col span="24" style="border-bottom: 1px solid #6c6c6e;padding: 10px;">
				编号：{{item.holdno}}			 
			</van-col>
          <van-col span="8" style="padding-top: 10px;">
            <img :src="getimg(item.cimg)"/>
          </van-col>
          <van-col span="16" style="padding-top: 10px;">
            <div >名称：<span class="textcolor">{{item.cname}}</span></div>
            <div>价值：<span class="textcolor">{{item.cminprice}}-{{item.cmaxprice}}</span></div>
            <div>出售价值：<span class="textcolor">{{item.cprice}}</span></div>
			<div>获得收益：<span class="textcolor">{{item.zshouyi}}</span></div>
            <div>买家姓名：<span class="textcolor">{{item.busername}}</span></div>
			<div>转让时间：<span class="textcolor">{{item.zrdate}}</span></div>
          </van-col>
        </van-row>
      </div>
    </div>

    <van-empty description="暂无记录" v-if="data.length == 0"/>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import UploadPictures from "@/components/UploadPictures";

import Vue from 'vue';
import { Tab, Tabs, Collapse, CollapseItem, Cell, Empty } from 'vant';
import { ImagePreview,Field,Popup,Picker} from 'vant'

Vue.use(Field);
Vue.use(Popup);
Vue.use(Picker);
Vue.use(ImagePreview);
Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Collapse);
Vue.use(CollapseItem);
Vue.use(Cell);
Vue.use(Empty);


export default {
  name: '',
  components: {
    HeadBar,
    UploadPictures
  },
  data () {
    return {
      title: document.title,
      active: 1,
      ewmimg: "",
      activeNames: [],
      data: [],
	  txk:false,
	  password2:'',
	  hid:''
    }
  },
  created() {
    this.getdata(this.active)
  },
  methods: {
	  skclick:function(id){
		  this.hid = id,
		  this.txk = true;
	  },
    getimg: function (img) {
      if (img != "") {
				console.log(img)
        return this.$config.send_url + "Upload/" + img;
      }
    },
    change(name) {
      this.data = []
      this.getdata(name)
    },
    getdata(active) {
      let _this = this
      this.$request.post("Api/UsersHold/List",
        {
          lx: active,
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
         // console.log(_this.data)
        }
      )
    },
    // 收款
    shoukuan() {
      let _this = this
     // console.log(id)
      this.$request.post("Api/UsersHold/Shoukuan",
        {
          hid: _this.hid,
		  password2:_this.password2,
          uid: _this.$utils.getloc("id"),
          token: _this.$utils.getloc("token"),
          userid: _this.$utils.getloc("userid"),
        },
        (res) => {
			//console.log(res);
          _this.$dialog.alert({
            title: "提示",
            message: res.data.msg,
          })
		  _this.getdata(2);
        }
      )
    }
  }
}
</script>

<style scoped>
	/deep/.van-action-sheet{
		background: #323233;
		color:#f7f8fa;
	}
	/deep/.van-field__control{
		color: #f7f8fa;
	}
	/deep/.van-field__label{
		padding-top: 10px;
		padding-left: 10px;
	}
/deep/.van-tabs__line {
  background-color: #e89715;
}

.cardContent {
  margin-bottom: 10px;
}

.cardContent .van-row img {
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

.van-button--default {
    border: 0;
}

.flex-box {
  display: flex;
  justify-content: flex-end;
  padding: 4px;
}

/deep/ .van-cell {
  background-color: #3B3E47;
  color: #fff;
  padding: 5px;
  font-size: 13px;
  border: 0;
}

/deep/ .van-collapse-item__content {
  background-color: #3B3E47;
}

/deep/ .van-cell::after {
  border: 0;
}

.van-cell__value {
  color: #ccc;
}

.cell-box .van-cell {
  padding: 5px;
}

.upload {
  font-size: 13px;
  padding: 5px;
}

.upload img {
  margin-top: 10px;
  width: 100%;
}

/deep/ .van-field__control {
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>