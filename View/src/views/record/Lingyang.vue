<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <van-tabs 
      v-model="active" 
      title-active-color="#e89715" 
      :border="false"
      @change="change"
    >
      <van-tab title="竞拍中" :name="1"/>
      <van-tab title="已竞拍" :name="0"/>
	  <van-tab title="申诉/取消申述" :name="2"/>
    </van-tabs>

    <div class="cardBox" v-if="active == 1">
      <div class="cardContent" v-for="(item,index) in data" :key="index">
		  <div v-if="item.item.state == 1">
			  <div style="text-align: right;font-size: 12px;padding-right: 10px;color: red;">待匹配</div>
			  <van-row>
			      <van-col span="8">
			        <img :src="getimg(item.item.cimg)"/>
			      </van-col>
			      <van-col span="16" style="padding-top: 10px;">
			        <div class="textcolor">{{item.item.cname}}</div>
			        <div>价值：{{item.item.cminprice}}-{{item.item.cmaxprice}}</div>
			       <div>日收益：{{item.item.crishouri}}%</div>
			       <div>周期：{{item.item.czhouqi}}</div>
			      </van-col>
			    </van-row>
		  </div>
		  <div v-if="item.item.state == 2">
			  <div style="text-align: right;font-size: 12px;padding-right: 10px;color: red;">匹配中</div>
			  <van-row>
			      <van-col span="8">
			        <img :src="getimg(item.item.cimg)"/>
			      </van-col>
			      <van-col span="16" >
			        <div class="textcolor">{{item.item.cname}}</div>
			        <div>价值：{{item.item.cminprice}}-{{item.item.cmaxprice}}</div>
			  		<div>当前价值：{{item.item.cprice}}</div>
			        <div>日收益：{{item.item.rishouyi}}%</div>
			        <div>周期：{{item.item.zhouqi}}</div>
			      </van-col>
			    </van-row>
			    <div class="cell-box">
			      <van-cell title="卖家姓名" :value="item.item.username"/>
			      <van-cell title="卖家电话" :value="item.item.usertel" />
			      <van-collapse v-model="activeNames" :border="false">
			      <van-collapse-item title="卖家账户" :name="index" :border="false">
			          <div class="bank-item" v-for="(bank,index2) in item.bank" :key="index2">
			            <van-cell title="账户名称" :value="bank.bankname"/>
			            <van-cell title="账号" :value="bank.bankcard" />
			            <van-cell title="姓名" :value="bank.username" />
			            <van-cell title="电话" :value="bank.usertel" />
			          </div>
			        </van-collapse-item>
			      </van-collapse>
			    </div>
			    <div class="upload">
			      <div>上传支付凭证</div>
			      <UploadPictures 
			        :haveImgList="item.item.zhimg?[{url: getimg(item.item.zhimg)}]: ewmimg != '' ? [{url: getimg(ewmimg)}] : []" 
			        @getUploadPictures="getUploadPictures" 
			        title=""
					:deletable="false"
			      />
			    </div>
			  		<div @click="ssclick()" style="font-size: 13px;color: red;float: left;padding-top: 10px;padding-left: 10px;">申诉留言</div>
			    <div class="flex-box">
			  	  <van-button style="background-color: red; color: #fff;margin-right: 20px;" size="small" @click="shenshu(item.item.id,item.item.bbz)" >申述</van-button>
			      <van-button v-if="item.item.zhimg == '' || item.item.zhimg ==null " style="background-color: #f7b226; color: #fff" size="small" @click="fkclick(item.item.id)">我已付款</van-button>
			    </div>
			  		<div class="cardBox" v-if="fuyanshow">
			  		    <div class="cardContent">
			  		        <van-field v-model="item.item.bbz" label="附言" placeholder="可不填" />
			  		    </div>
			  		</div>
			  
			  <van-action-sheet v-model="show" title="请输入支付密码" :close-on-click-overlay="false">
			      <div class="content">
			          <van-form @submit="fukuan">
			              <van-field v-model="password2" type="password" name="支付密码" label="支付密码" placeholder="支付密码"  />
			              <div style="margin: 16px">
			                  <van-button round block type="info">确定已付款</van-button>
			              </div>
			          </van-form>
			      </div>
			  </van-action-sheet>
		  </div>
        </div>
    </div>

    <div class="cardBox" v-if="active == 0">
      <div class="cardContent" v-for="(item,index) in data" :key="index">
        <van-row>
          <!-- <van-col span="6">
            <img :src="getimg(item.cimg)"/>
          </van-col> -->
		  <van-col span="24" style="border-bottom: 1px solid #6c6c6e;padding: 10px;">
			  编号：{{item.holdno}}
			  <span v-if="item.state == 0" style="float: right;color:#E6A23C;">收益中</span>
			  <span v-if="item.state == 4" style="float: right;color:#E6A23C;">收益结束</span>
		  </van-col>
          <van-col span="24" style="padding-top: 5px;">
            <div>名称：<span class="textcolor">{{item.cname}}</span></div>
            <div>价值：<span class="textcolor">{{item.cminprice}}-{{item.cmaxprice}}</span></div>
			<div>拍单原价：<span class="textcolor">{{item.pprice}}</span></div>
            <div>当前价值：<span class="textcolor">{{item.cprice}}</span></div>
            <div>收益：<span class="textcolor">{{item.zhouqi}}/{{Number(item.rishouyi) * Number(item.zhouqi) }}%</span></div>
            <div>获得收益：<span class="textcolor">{{item.zshouyi}}</span></div>
			<div>领养时间：<span class="textcolor">{{item.hdate}}</span></div>
			<div v-if="item.state == 0" style="display: flex;justify-content:start;align-items: center;">
				出售倒计时：
				<van-count-down :time="item.edate" format="DD 天 HH 时 mm 分 ss 秒" />
			</div>
			<div   style="text-align: right;padding: 10px;">
				<van-button v-if="item.issj == 0" @click="shangjia(item.id)" style="background: #db8a37;color: #fff;" size="small">上架</van-button>
				<van-button v-if="item.issj == 1"  style="background: #db8a37;color: #fff;" size="small" disabled>已上架</van-button>
			</div>
          </van-col>
        </van-row>
      </div>
    </div>
	<div class="cardBox" v-if="active == 2">
	  <div class="cardContent" v-for="(item,index) in data" :key="index">
	    <van-row>
	      <van-col span="8">
	        <img :src="getimg(item.item.cimg)"/>
	      </van-col>
	      <van-col span="16">
	        <div class="textcolor">{{item.item.cname}}</div>
	        <div>价值：{{item.item.cminprice}}-{{item.item.cmaxprice}}</div>
	  			 <div>当前价值：{{item.item.cprice}}</div>
	        <div>日收益：{{item.item.rishouyi}}</div>
	        <div>周期：{{item.item.zhouqi}}</div>
	      </van-col>
	    </van-row>
	    <div class="cell-box">
	      <van-cell title="卖家姓名" :value="item.item.username"/>
	      <van-cell title="卖家电话" :value="item.item.usertel" />
	      <van-collapse v-model="activeNames" :border="false">
	      <van-collapse-item title="卖家账户" :name="index" :border="false">
	          <div class="bank-item" v-for="(bank,index2) in item.bank" :key="index2">
	            <van-cell title="账户名称" :value="bank.bankname"/>
	            <van-cell title="账号" :value="bank.bankcard" />
	            <van-cell title="姓名" :value="bank.username" />
	            <van-cell title="电话" :value="bank.usertel" />
	          </div>
	        </van-collapse-item>
	      </van-collapse>
		  <div style="font-size: 13px;padding-left: 5px;padding-top: 10px;">申诉留言：{{item.item.bbz}}</div>
	    </div>
	   
	  		
	    <div class="flex-box">		
	  	  <van-button style="background-color: red; color: #fff;margin-right: 20px;" size="small" @click="qxshenshu(item.item.id)" >取消申诉</van-button>
	    </div>	
	  </div>

	</div>

    <van-empty description="暂无记录" v-if="data.length == 0"/>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import UploadPictures from "@/components/UploadPictures";

import Vue from 'vue';
import { Tab, Tabs, Button, Collapse, CollapseItem, Cell, Popup ,Picker, Empty,CountDown,ActionSheet,Form} from 'vant';


Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Button);
Vue.use(Collapse);
Vue.use(CollapseItem);
Vue.use(Cell);
Vue.use(Popup);
Vue.use(ActionSheet);
Vue.use(Form);
Vue.use(Picker);
Vue.use(Empty);
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
      active: 1,
      ewmimg: "",
      activeNames: [],
      data: [],
	  hid:"",
	  show:false,
	  password2:'',
	  fuyanshow:false,
	  beizhu:''
    }
  },
  created() {
    this.getlingyangzhong(1);
	
  },
  methods: {
    getimg: function (img) {
      if (img != "") {
			//	console.log(img)
        return this.$config.send_url + "Upload/" + img;
      }
    },
	ssclick(){
		if(this.fuyanshow){
			this.fuyanshow = false;
		}else{
			this.fuyanshow = true;
		}
	},
	fkclick(id){
		this.hid = id;
		this.show = true;
	},
    getUploadPictures(img) {
      this.ewmimg = img;
	  //console.log(img);
    },
    change(name) {
      this.data = []
      if(name == 1) {
        this.getlingyangzhong(1)
      } else if(name == 0) {
        this.getyilingyang()
      }else if(name == 2) {
        this.getlingyangzhong(2)
      }
    },
    // 领养中
    getlingyangzhong(lx) {
      let _this = this
      this.$request.post("Api/UsersYuyue/List", 
        {
          lx: lx,
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
		 // console.log(res.data.data);
          let data = res.data.data;
		  this.data = data;
		   console.log( this.data);
          // if(data[0].data) {
          //   this.data = data
          // } else {
          //   this.data = []
          // }
          
        }
      )
    },
    // 已领养
    getyilingyang() {
      let _this = this
      this.$request.post("Api/UsersHold/List", 
        {
          lx: 0,
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
          this.data = res.data.data;
		  var date = new Date().getTime();
		  this.data.forEach(item=>{
			  
			  item.edate = Number(new Date(item.edate).getTime())  - Number(date);
		  })
		  console.log(this.data);
        }
      )
    },
    // 付款
    fukuan() {
      let _this = this
      this.$request.post("Api/UsersHold/Fukuan",
        {
          hid: _this.hid,
          zfimg: _this.ewmimg,
		  password2:_this.password2,
          uid: _this.$utils.getloc("id"),
          token: _this.$utils.getloc("token"),
          userid: _this.$utils.getloc("userid"),
        },
        (res) => {
          _this.$dialog.alert({
            title: "提示",
            message: res.data.msg,
          });
		  
		  _this.password2 = "";
		  _this.show = false;
		  _this.getlingyangzhong(1)
        }
      )
    },
	shenshu(id,bz) {
	  let _this = this
	  this.$request.post("Api/UsersHold/Shenshu",
	    {
		    uid: _this.$utils.getloc("id"),
			token: _this.$utils.getloc("token"),
			userid: _this.$utils.getloc("userid"),
	        hid: id,
	        bbz:bz,
	      
	    },
	    (res) => {
	      _this.$dialog.alert({
	        title: "提示",
	        message: res.data.msg,
	      });
	    }
	  )
	},
	qxshenshu(id) {
	  let _this = this
	  this.$request.post("Api/UsersHold/Qxshenshu",
	    {
		    uid: _this.$utils.getloc("id"),
			token: _this.$utils.getloc("token"),
			userid: _this.$utils.getloc("userid"),
	        hid: id,
	      
	    },
	    (res) => {
	      _this.$dialog.alert({
	        title: "提示",
	        message: res.data.msg,
	      });
	    }
	  )
	},
	shangjia(id) {
	  let _this = this
	  this.$request.post("Api/UsersHold/shangjia",
	    {
			token: _this.$utils.getloc("token"),
			userid: _this.$utils.getloc("userid"),
			uid: _this.$utils.getloc("id"),
			hid: id, 
	    },
	    (res) => {
	      _this.$dialog.alert({
	        title: "提示",
	        message: res.data.msg,
	      })
		  this.getyilingyang();
	    }
	  )
	}
  }
}
</script>

<style scoped>
	
	/deep/.van-field__label{
		padding-top: 10px;
		padding-left: 10px;
	}
	/deep/.van-action-sheet{
		background: #323233;
		color:#f7f8fa;
	}
	
	/deep/.van-field__control{
		color: #f7f8fa;
	}
	/deep/.van-count-down{
		color: #641602;
		font-size: 13px;
	}
/deep/.van-tabs__line {
  background-color: #e89715;
}

.cardContent {
  margin-bottom: 10px;
}

.cardContent img {
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

.cell-box .van-cell {
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

.van-overlay {
  background-color: rgba(0,0,0,.2);
}

.bank-item {
  font-size: 13px;
  color: #ccc;
  padding: 10px 0;
}

.bank-item:nth-of-type(n+2) {
  border-top: 1px solid rgba(104, 103, 103, 0.6);
}

.bank-item .van-cell {
  padding: 1px 0;
}

.van-cell__value {
  color: #ccc;
}
</style>