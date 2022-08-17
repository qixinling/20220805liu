<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'" toback="User"></HeadBar>
    <van-tabs :border="false" v-model="active" @click="onClick">     
      <van-tab title="全部" name="99"></van-tab>
      <van-tab title="持有中" name="0"></van-tab>
      <van-tab title="待交易" name="1"></van-tab>
      <van-tab title="交易中" name="2"></van-tab>
	  <van-tab title="已封存" name="3"></van-tab>
    </van-tabs>
    <div class="fenge"></div>

    <div class="mygoods-content">
      <div class="cardBox" >
        <div style="margin-bottom:10px" class="cardContent" v-for="(item,index) in data" :key="index">
          <div style="border-bottom:1px solid #ccc">
        <van-row >
          <van-col span="19" style="font-size: 13px;padding: 5px;">单号：{{item.holdno}}</van-col>
          <van-col span="5"  style="font-size: 13px;padding: 5px;color: red;text-align: right;">{{item.statename}}</van-col>
        </van-row>
          <van-row>
            <van-col span="8">
              <img :src="getimg(item.jimg)" style="width:100%"/>
            </van-col>
            <van-col span="16">
              <div class="content">
                <div class="content-name">{{ item.jname }}</div>
                <div class="content-jine" style="color: #e9a853;">入手价值：{{ item.jprice - item.zshouyi }}</div>
                <div class="content-jine">出手价值：{{ item.jprice  }}</div>
              </div>
            </van-col>
          </van-row>
          <!-- <div class="suser-info">
            <van-cell title="买家姓名" :value="item.busername"/>
        <van-cell title="买家电话" :value="item.busertel" />
          </div> -->
          <!-- <div class="upload">
            <div style="margin-bottom:5px">买家支付凭证</div>
        <img v-if="item.zhimg" :src="getimg(item.zhimg)" style="width: 100%;height: 250px;"/>
      <div v-else style="padding: 20px;color: #888;text-align: center;">暂未上传凭证</div> 
          </div> -->
      <van-row>
        <!-- <van-col span="12" style="padding: 5px;"><van-button size="normal" style="background-color: red; color: #fff" block @click="Sellshensu(item.id)">申诉</van-button></van-col> -->
        <van-col span="24" v-if="item.state==2" style="padding: 5px;text-align: right;"><van-button size="small" style="background-color: #FC4702; color: #fff" @click="shoukuanSubmit(item.id)">确认收款</van-button></van-col>
        <!-- <span v-if="item.sellissu==1" style="font-size: 12px;color: red;float: right;padding: 5px;">温馨提示：已提交申诉，请等待审核</span> -->
      </van-row>
      <!-- <van-dialog v-model:show="sushow" title="申诉" show-cancel-button @confirm="suconfirm">
      <div style="padding: 10px;">
        <span style="font-size: 14px;">申述原因：</span>
        <van-field
          v-model="bbz"
          rows="1"
          autosize
          type="textarea"
          placeholder="请输入申诉理由"
        />
      </div>
      </van-dialog> -->
          
        </div>
        </div>
      </div>

      <van-empty description="暂无订单" v-if="data.length == 0" />
    </div>

    <van-action-sheet v-model="showsk" title="请输入支付密码" :close-on-click-overlay="false">
	    <div class="content">
	        <van-form @submit="shoukuanSubmit">
	            <van-field v-model="password2" type="password" name="支付密码" label="支付密码" placeholder="支付密码" :rules="[{ required: true, message: '请填写支付密码' }]" />
	            <div style="margin: 16px">
	                <van-button round block type="info">确认收款</van-button>
	            </div>
	        </van-form>
	    </div>
	</van-action-sheet>
  <van-action-sheet v-model="showfk" title="请输入支付密码" :close-on-click-overlay="false">
	    <div class="content">
	        <van-form @submit="fukuanSubmit">
	            <van-field v-model="password2" type="password" name="支付密码" label="支付密码" placeholder="支付密码" :rules="[{ required: true, message: '请填写支付密码' }]" />
	            <div style="margin: 16px">
	                <van-button round block type="info">确定已付款</van-button>
	            </div>
	        </van-form>
	    </div>
	</van-action-sheet>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import UploadPictures from "@/components/UploadPictures";
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
    UploadPictures
  },
  data () {
    return {
      title: "我的字画",
      data: {},
      active: 99,
      activeNames: [],
      haveImgList: [],
      zfimg: "",
      showsk: false,
      showfk: false,
	  sushow:false,
      password2: "",
      hid: "",
	  bbz:'',
	  time:30 * 60 * 60 * 1000
    }
  },
  created() {
	  
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
        "api/UsersHold/MyOrderList2",
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
          let data = res.data.data
          that.data = data;
		  console.log(that.data);
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
    shoukuanSubmit(id) {
      let that = this
      that.hid = id
      that.$dialog.confirm({
        title: "提示",
        message: "你确认收款吗？"
      }).then(() => {
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
      })
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
    Dialog.confirm({
      title: '提示',
      message: '确定要转售吗？',
    })
      .then(() => {
        that.$request.post(
          "api/UsersHold/shangjia",
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
    background-color: #e57516;
}
/deep/.van-tab--active{
	font-weight: 600;
	color:#e57516 !important;
}
/deep/.van-tab{
	color: #3b3e47;
	font-weight:600;
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
.mygoods-content {
  min-height: calc(100vh - 97px);
  background-color: #fff;
}
.fenge {
  padding-top: 6px;
  border-bottom: 1px solid #ccc;
  background-color: #fff;
}
</style>