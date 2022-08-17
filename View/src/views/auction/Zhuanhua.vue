<template>
  <div>
    <HeadBar :title="title"></HeadBar>
    <van-tabs title-active-color="red" :border="false" v-model="active" @click="onClick">     
        <van-tab title="待转画" name="0"></van-tab>
		<van-tab title="转出记录" name="1"></van-tab>
		<van-tab title="转入记录" name="2"></van-tab>
		
    </van-tabs>

    <div class="cardBox" >
      <div style="margin-bottom:10px" class="cardContent" v-for="(item,index) in data" :key="index">
		  <van-row >
			  <van-col span="20" style="font-size: 13px;padding: 5px;">单号：{{item.holdno}}</van-col>
			  <van-col span="4"  style="font-size: 13px;padding: 5px;color: red;"></van-col>
		  </van-row>
        <van-row>
          <van-col span="8">
            <img :src="getimg(item.jimg)" style="width:100%"/>
          </van-col>
          <van-col span="16">
            <div class="content">
              <div class="content-name">{{ item.jname }}</div>
              <div class="content-jine">竞拍金额：<span>￥{{ item.jprice }}</span></div>
              <div class="content-jine">上架费：<span>￥{{ item.sjjine }}</span></div>
            
            </div>
          </van-col>
        </van-row>
        <div class="suser-info">
          <van-cell title="卖家姓名" :value="item.username"/>
		  <van-cell title="卖家电话" :value="item.usertel" />
        </div>
       <!-- <div class="upload">
          <div style="margin-bottom:5px">上架支付凭证</div>
		  <img v-if="item.sjimg" :src="getimg(item.sjimg)" style="width: 100%;height: 250px;"/>
		 <div v-else style="padding: 20px;color: #888;text-align: center;">暂未上传凭证</div> 
        </div> -->
		<van-row>
			<!-- <van-col span="12" style="padding: 5px;"><van-button size="normal" style="background-color: red; color: #fff" block @click="Sellshensu(item.id)">申诉</van-button></van-col> -->
			<van-col span="24" v-if="item.state == 0 && active == 0" style="padding: 5px;text-align: right;">
				<van-button size="small" type="danger"  @click="zhclick(item.id)">转画</van-button>
				</van-col>
				<van-field
				v-if="show && item.id == hid"
				  v-model="suserid"
				  center
				  clearable
				  label="画室长账号"
				  placeholder="请输入画室长账号"
				>
				  <template #button>
				    <van-button size="small" type="primary" @click="zhuanhua">确定</van-button>
				  </template>
				</van-field>
		   <!-- <span v-if="item.sellissu==1" style="font-size: 12px;color: red;float: right;padding: 5px;">温馨提示：已提交申诉，请等待审核</span> -->
		</van-row>

      </div>
    </div>

    <van-empty description="暂无订单" v-if="data.length == 0" />

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
     title: document.title,
      data: {},
      active: 0,
      password2: "",
      hid: "",
	  suserid:'',
	  show:false
	  
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
	zhclick(id){
		this.hid = id;
		this.show = true;
	},
    onClick() {
      this.getdata(this.active)
    },
    getdata(lx) {
      let that = this
	   that.data=[];
      that.$request.post(
        "api/UsersHold/ZhList",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
		  uid: that.$utils.getloc("id"),
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
	//转画
	zhuanhua() {
	  let that = this
    Dialog.confirm({
      title: '提示',
      message: '确定要转画吗？',
    })
      .then(() => {
        that.$request.post(
          "api/UsersHold/Zhuanhua",
          {
            token: that.$utils.getloc("token"),
            userid: that.$utils.getloc("userid"),
            uid: that.$utils.getloc("id"),
			hid:that.hid,
            suserid: that.suserid
          },
          (res) => {
			  if(res.data.code == 0) {
			    that.$dialog.alert({
			      title: "提示",
			      message: res.data.msg,
			    })
			    return 
			  }
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
			that.show = false;
            that.getdata(that.active)
          }
        )
      })
      .catch(() => {
        // on cancel
      });
	},
  }
}
</script>

<style scoped>
/* /deep/.van-tabs__line {
    background-color: #e89715;
} */

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
</style>