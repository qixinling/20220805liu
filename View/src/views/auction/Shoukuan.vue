<template>
  <div>
    <HeadBar :title="title"></HeadBar>
    <!-- <van-tabs title-active-color="red" :border="false" v-model="active" @click="onClick">     
        <van-tab title="待审上架" name="1"></van-tab>
		<van-tab title="已审上架" name="3"></van-tab>
		<van-tab title="未申请上架" name="2"></van-tab>
    </van-tabs> -->

    <div class="cardBox" >
      <div style="margin-bottom:10px" class="cardContent" v-for="(item,index) in data" :key="index">
		  <van-row>
			  <van-col span="20" style="font-size: 14px;padding: 5px;display: flex;align-items: center;">
				  <van-icon name="clock-o" size="16px"/>
				  <span style="padding-left: 5px;">{{item.cdate}}</span>
				  </van-col>
			  <van-col span="4"  style="font-size: 14px;padding: 5px;">
				  <span v-if="item.state == 0" style="color:red">{{item.stateName}}</span>
				  <span v-if="item.state == 1" style="color:green">{{item.stateName}}</span>
				  </van-col>
		  </van-row>
        <van-row>
          <van-col span="24">
            <div class="content">
              <div class="content-name">打款方：<span>{{ item.userid }}（{{item.username}}）</span></div>
              <div class="content-name">收款方：<span>{{ item.suserid }}（{{item.susername}}）</span></div>
            <div class="content-name">金额：<span>￥{{ item.jine }}</span></div>
			<!-- <div class="content-name">流水号：<span>￥{{item.liushuihao }}</span></div>
			<div class="content-name">支付凭证：<div style="text-align: center;"><img :src="getimg(item.dkimg)"/></div></div> -->
            </div>
			
          </van-col>
        </van-row>
        <!-- <div style="padding: 5px;">
           <van-field v-model.trim="item.liushuihao" type="tel" label="流水号"  placeholder="请输入流水号"  />
		   <div class="upload">
		     <div style="margin-bottom:5px">上传凭证</div>
		     <UploadPictures
		       :haveImgList="item.dkimg?[{url: getimg(item.dkimg)}]: haveImgList[index] ? [{url: getimg(haveImgList[index])}] : []" 
		       @getUploadPictures="getUploadPictures" @click.native="uploadClick(index)" title="" />
		   </div>
        </div> -->
        
		<van-row>
			<van-col span="24" style="padding: 5px;text-align: right;"><van-button size="small" style="border: 1px solid #FC4702; color: #FC4702"  @click="tolink(item.id)">查看详情</van-button></van-col>
			
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
      hid: "",
	  bbz:'',
	  time:30 * 60 * 60 * 1000
    }
  },
  created() { 
    this.getdata()
  },
  methods: {
    getimg: function (img) {
      if (img != "") {
        return this.$config.send_url + "Upload/" + img;
      }
    },
	tolink(id){
		this.$router.push({
			name:'Details',
			query:{
				id:id
			}
		})
	},
	
    getdata() {
      let that = this
	   that.data=[];
      that.$request.post(
        "api/JichaDakuan/List",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
		  uid: that.$utils.getloc("id"),
		  lx:2
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
</style>