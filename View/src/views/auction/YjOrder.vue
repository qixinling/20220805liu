<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>
	<div style="background-color: #f7b226;font-size: 13px;color: #fff;">
		<div style="padding: 10px;">今日业绩订单笔数：{{jinrinum}}，今日订单总金额：{{jinriprice}}</div>
	</div>
    <van-tabs title-active-color="#ff4500" :border="false" v-model="active" @click="onClick">
        <!-- <van-tab title="我的商品" name="0"></van-tab> -->
      <van-tab title="全部" name="99"></van-tab>
      <van-tab title="待付款" name="1"></van-tab>
      <van-tab title="待收款" name="2"></van-tab>
      <van-tab title="待上架" name="3"></van-tab>
      <van-tab title="已完成" name="4"></van-tab>
		
        <!-- <van-tab title="我的卖单(字画)" name="1"></van-tab> -->
    </van-tabs>
    <div class="fenge"></div>

    <div class="tg-order-content">
      <div class="cardBox">
        <div style="margin-bottom:10px" class="cardContent" v-for="(item,index) in data" :key="index">
        <van-row style="padding: 5px;font-size: 13px;">
          <van-col span="18" >单号:{{item.holdno}}</van-col>
        <van-col span="6" style="text-align: right;color: red;">{{item.statename}}</van-col>
        </van-row>
          <van-row>
            <van-col span="8">
              <img :src="getimg(item.jimg)" style="width:100%"/>
            </van-col>
            <van-col span="16">
              <div class="content">
                <div class="content-name">{{ item.jname }}</div>
                <div class="maijia-label"><span>买家</span>{{item.busername}}({{item.busertel}})</div>
                <div class="maijia-label2"><span>卖家</span>{{item.username}}({{item.usertel}})</div>
              </div>
            </van-col>
          </van-row>
          <div class="jine-wrap-row2">
            <div v-if="item.state > 0">抢购：{{item.qgdate}}</div>
            <div v-if="item.state > 2">确认：{{ item.skdate }}</div>
          </div>
          <div class="jine-wrap-row">
            <div>金额：{{ item.jprice }}</div>
            <div>上架费：{{ item.sjjine }}</div>
          </div>
    
        </div>
      </div>
      <van-empty description="暂无订单" v-if="data.length == 0" />
    </div>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";

import Vue from "vue";
import {
    Tab,
    Tabs,
    Dialog,
    Col,
    Row,
    Cell,
    Collapse,
    CollapseItem,
    Button,
    ActionSheet,
    Form,
    Empty,
	Field,
	CellGroup,
	CountDown
} from "vant";
Vue.use(Dialog);
Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Col);
Vue.use(Row);
Vue.use(Cell);
Vue.use(Collapse);
Vue.use(CollapseItem);
Vue.use(Button);
Vue.use(ActionSheet);
Vue.use(Form);
Vue.use(Empty);
Vue.use(Field);
Vue.use(CellGroup);
Vue.use(CountDown);

export default {
  name: '',
  components: {
    HeadBar,
  },
  data () {
    return {
      title: "画室订单",
      data: [],
      active: 99,
      activeNames: [],
      haveImgList: [],
      zfimg: "",
	  sjimg: "",
      showsk: false,
      showfk: false,
	  sushow:false,
      password2: "",
      hid: "",
	  bbz:'',
	  time:30 * 60 * 60 * 1000,
	  jinrinum:0,
	  jinripeice:0
    }
  },
  created() {
	  if(this.$route.query.state != null){
		  this.active = this.$route.query.state;
	  }
	  
	  
    this.getdata(this.active)
  },
  methods: {
    getimg: function (img) {
      if (img != "") {
        return this.$config.send_url + "Upload/" + img;
      }
    },
	Sellshensu(id){
		this.hid = id;
		this.sushow = true;
	},
	suconfirm(){
		 let that = this
		that.$request.post(
		  "api/UsersHold/SellShenshu",
		  {
		    token: that.$utils.getloc("token"),
		    userid: that.$utils.getloc("userid"),
			uid: that.$utils.getloc("id"),
		    hid:that.hid,
			bbz:that.bbz
		  },
		  (res) => {
		      that.$dialog.alert({
		        title: "提示",
		        message: res.data.msg,
		      })
		      return 
		    that.bbz="";
			that.hid ="";
			that.sushow = false;
		    //console.log(that.data)
		  }
		)
	},
    onClick() {
      this.getdata(this.active)
    },
    getdata(lx) {
      let that = this
	   that.data=[];
      that.$request.post(
        "api/UsersHold/YjOrderList",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
          state:lx
        },
        (res) => {
          if(res.data.code == 0) {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            })
            return 
          }
          that.data = res.data.data.hlist;
		  that.jinrinum = res.data.data.jinrinum;
		  that.jinriprice = res.data.data.jinriprice;
		  console.log(res.data.data);
        }
      )
    },
    getUploadPictures(img) {
      this.haveImgList[this.uploadIndex] = img
    },
    fkClick(id) {
      this.showfk = true
      this.hid = id
    },
    skClick(id) {
      this.showsk = true
      this.hid = id
    },
    // 收款
    shoukuanSubmit() {
      let that = this
      that.$request.post(
        "api/UsersHold/Shoukuan",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
          uid: that.$utils.getloc("id"),
          hid: that.hid,
          password2: that.password2
        },
        (res) => {
          that.$dialog.alert({
            title: "提示",
            message: res.data.msg,
          });

          that.showsk = false
          that.password2 = ""
          that.getdata(that.active)
        }
      )
    },
	jiaogeClick(hid){
		let that = this
    Dialog.confirm({
      title: '提示',
      message: '确定要交割吗？',
    })
      .then(() => {
        that.$request.post(
          "api/UsersHold/Jiaoge",
          {
            token: that.$utils.getloc("token"),
            userid: that.$utils.getloc("userid"),
            uid: that.$utils.getloc("id"),
            hid: hid
          },
          (res) => {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
            that.getdata(that.active)
          }
        )
      })
      .catch(() => {
        // on cancel
      });
	},
	//转售
	zhuanshou(hid) {
	  let that = this
	  let sjimg = this.haveImgList[that.uploadIndex] ? this.haveImgList[that.uploadIndex] : "";
	  console.log(sjimg);
    Dialog.confirm({
      title: '提示',
      message: '确定要申请上架吗？',
    })
      .then(() => {
        that.$request.post(
          "api/UsersHold/Fukuan2",
          {
            token: that.$utils.getloc("token"),
            userid: that.$utils.getloc("userid"),
            uid: that.$utils.getloc("id"),
            hid: hid,
			sjimg
          },
          (res) => {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
            that.getdata(that.active)
          }
        )
      })
      .catch(() => {
        // on cancel
      });
	},
    // 付款
    fukuanSubmit() {
      let that = this
      let zfimg = this.haveImgList[that.uploadIndex] ? this.haveImgList[that.uploadIndex] : ""
      this.$request.post(
        "api/UsersHold/Fukuan",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
          uid: that.$utils.getloc("id"),
          password2: that.password2,
          hid: that.hid,
          zfimg
        },
        (res) => {
          that.$dialog.alert({
            title: "提示",
            message: res.data.msg,
          });
          that.showfk = false
          that.password2 = ""
          that.getdata(that.active)
        }
      )
    },
    uploadClick(index) {
      this.uploadIndex = index
    },
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
  }
}
</script>

<style scoped>
/deep/.van-tabs__line {
    background-color: #ff4500;
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

.jine-wrap-row {
  padding: 4px 10px;
  font-weight: 600;
  border-top: 1px solid #ccc;
  border-bottom: 1px solid #ccc;
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 14px;
  color: #FC4702;
}

.jine-wrap-row2 {
  font-size: 12px;
  padding: 4px 10px;
  border-top: 1px solid #ccc;
}
.content {
  font-size: 14px;
  padding-left: 10px;
  padding-top: 10px;
}

.content  div {
  margin-bottom: 10px;
}

.content-name {
  font-weight: 600;
}

.content-jine span {
  color: red;
  font-size: 15px;
  font-weight: 600;
}

/deep/ .van-cell {
  padding: 5px;
}

.upload {
  font-size: 13px;
  padding: 5px;
}

/deep/ .van-field__control {
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 10px 0;
}

.bnt-wrap {
  display: flex;
  justify-content: space-around;
}

.bnt-wrap .van-button {
  margin: 0 10px;
}

.tg-order-content {
  /* min-height: calc(100vh - 97px); */
  /* background-color: #efefef; */
}

.fenge {
  position: relative;
  padding: 8px 0 4px;
  background-color: #fff;
}

.fenge::before {
  content: "";
  position: absolute;
  top: 6px;
  left: 0;
  right: 0;
  border-bottom: 1px solid #ccc;
}
</style>