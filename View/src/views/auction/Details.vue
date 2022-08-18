<template>
  <div>
    <HeadBar :title="title"></HeadBar>
    <!-- <van-tabs title-active-color="red" :border="false" v-model="active" @click="onClick">     
        <van-tab title="待审上架" name="1"></van-tab>
		<van-tab title="已审上架" name="3"></van-tab>
		<van-tab title="未申请上架" name="2"></van-tab>
    </van-tabs> -->

    <div class="cardBox" >
      <div style="margin-bottom:10px" class="cardContent">
		  <van-row>
			  <van-col span="20" style="font-size: 16px;padding: 5px;display: flex;align-items: center;">
				  <van-icon name="clock-o" size="16px"/>
				  <span style="padding-left: 5px;">{{data.cdate}}</span>
				  </van-col>
			  <van-col span="4"  style="font-size: 14px;padding: 5px;">
				  <span v-if="data.state == 0" style="color:red">{{data.stateName}}</span>
				  <span v-if="data.state == 1" style="color:green">{{data.stateName}}</span>
				  </van-col>
		  </van-row>
        <van-row>
          <van-col span="24">
            <div class="content">
              <div class="content-name">打款方：<span>{{ data.userid }}（{{data.username}}）</span></div>
              <div class="content-name">收款方：<span>{{ data.suserid }}（{{data.susername}}）</span></div>
            <div class="content-name">金额：<span>￥{{ data.jine }}</span></div>
			 <div class="content-name" v-if="data.state == 1">打款时间：<span>{{ data.ddate }}</span></div>
			   <van-field v-model.trim="data.liushuihao" type="tel" label="流水号"  placeholder="请输入流水号"  :readonly="data.state == 0 ? false : true"/>
			   <div class="upload">
			     <div class="content-name" style="margin-bottom:5px">上传凭证</div>
			     <UploadPictures
			       :haveImgList="data.dkimg?[{url: getimg(data.dkimg)}]: haveImgList[0] ? [{url: getimg(haveImgList[0])}] : []" 
			       @getUploadPictures="getUploadPictures" @click.native="uploadClick(index)" title="" />
			   </div>
			
			 </div>
          </van-col>
        </van-row>
		<div style="padding: 5px;">
			<div class="content-name" style="padding: 5px;">收款账户</div>
			<div class="sk" style="border-bottom: 1px solid #E6A23C;" v-for="(bank,bankindex) in data.blist" :key="bankindex">
			  <van-cell center title="账户名称" :value="bank.bankname" />
			  <van-cell center title="账号" :value="bank.bankcard">
			    <template #right-icon>
			      <img style="padding-left: 10px;" src="../../assets/img/fz.png" @click="CopyLink(bank.bankcard)"/>
			    </template>
			  </van-cell>
			  <van-cell center title="姓名" :value="bank.username">
			    <template #right-icon>
			      <img style="padding-left: 10px;" src="../../assets/img/fz.png" @click="CopyLink(bank.username)"/>
			    </template>
			  </van-cell>
			  <van-cell center title="电话" :value="bank.usertel">
			    <template #right-icon>
			      <img style="padding-left: 10px;" src="../../assets/img/fz.png" @click="CopyLink(bank.usertel)"/>
			    </template>
			  </van-cell>
			  <div style="text-align:center">
			    <img style="width: 60%;" :src="getimg(bank.bankimg)" />
			  </div>
		</div>
		
		</div>
        
        
		<van-row>
			<van-col span="24" v-if="data.state == 0 && data.uid == uid " style="padding: 5px;"><van-button size="normal" style="background-color: #FC4702; color: #fff" block @click="dakuan(data)">提交</van-button></van-col>
			
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
	CountDown,
	Toast
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
Vue.use(Toast);

export default {
  name: '',
  components: {
    HeadBar,
    UploadPictures
  },
  data () {
    return {
      title:document.title,
      data: {},
      active: 1,
      activeNames: [],
      haveImgList: [],
      zfimg: "",
      showsk: false,
      showfk: false,
	  sushow:false,
      password2: "",
      jid: "",
	  uid:'',
	  time:30 * 60 * 60 * 1000
    }
  },
  created() { 
	  this.uid = this.$utils.getloc("id");
	  this.jid = this.$route.query.id;
	  console.log(this.jid);
	  if(this.jid != ''){
		  this.getdata()
	  }
    
  },
  methods: {
    getimg: function (img) {
      if (img != "") {
        return this.$config.send_url + "Upload/" + img;
      }
    },

    getdata() {
      let that = this
	   that.data=[];
      that.$request.post(
        "api/JichaDakuan/Get",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
		  uid: that.$utils.getloc("id"),
		  id:that.jid
        },
        (res) => {
          if(res.data.code == 0) {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            })
            return 
          }
           that.data = res.data.data
         
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

	dakuan(item){
		let that = this
		let img = this.haveImgList[that.uploadIndex] ? this.haveImgList[that.uploadIndex] : "";
		if(img == '' || img == null){
			Toast('请先上传凭证');
			return;
		}
		if(item.liushuihao == '' || item.liushuihao == null){
			Toast('请填写流水号');
			 return;
		}
		
    Dialog.confirm({
      title: '提示',
      message: '确定要提交吗？',
    })
      .then(() => {
        that.$request.post(
          "api/JichaDakuan/Dakuan",
          {
            token: that.$utils.getloc("token"),
            userid: that.$utils.getloc("userid"),
            uid: that.$utils.getloc("id"),
            jid: item.id,
			dkimg:img,
			liushui:item.liushuihao
          },
          (res) => {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
            that.getdata()
          }
        )
      })
      .catch(() => {
        // on cancel
      });
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
/* /deep/.van-tabs__line {
    background-color: #e89715;
} */
.upload /deep/.van-field__control {
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 10px 0;
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
  padding-left: 5px;
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
  font-size: 14px;
  
  
}
/deep/.van-field__label{
	font-weight: 600;
	font-size: 14px;
}
/* /deep/ .van-field__control {
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 10px 0;
} */

.bnt-wrap {
  display: flex;
  justify-content: space-around;
}

.bnt-wrap .van-button {
  margin: 0 10px;
}
.sk /deep/.van-cell {
  
}
</style>