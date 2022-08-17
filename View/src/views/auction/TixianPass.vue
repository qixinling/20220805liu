<template>
    <div>
        <HeadBar :title="title"></HeadBar>
		<van-tabs title-active-color="red" :border="false" v-model="active" @click="onClick">
		    <van-tab title="待审核" name="0"></van-tab>
			<van-tab title="已审核" name="1"></van-tab>	
		</van-tabs>
        <div v-if="list.length==0">
            <van-empty description="暂无记录" />
        </div>
        <div class="cardBox" v-for="(item, index) in list" :key="index" >
            <div class="allContent">
                <van-row>
                    <van-col span="12" style="font-size: 14px; color: #ee0a24">{{
            item.ispayname
          }}</van-col>
                    <van-col span="12" class="text" style="text-align: right">{{
            item.coinname
          }}</van-col>
                </van-row>

                <van-row style="padding-top: 12px">
                    <van-col span="5" class="text2">{{
            $utils.timestampFormat(item.tdate)
          }}</van-col>
                    <van-col span="19" style="text-align: right">
                        <div class="pricetext" style="">
                            {{ item.jine
              }}<span style="font-size: 8px; font-weight: 400">元</span>
                        </div>
                    </van-col>
                </van-row>
				<van-row style="padding-top: 8px">
				    <van-col span="5" class="text">账号:</van-col>
				    <van-col span="19" class="text">{{ item.userid }}</van-col>
				</van-row>
				<van-row style="padding-top: 8px">
				    <van-col span="5" class="text">姓名:</van-col>
				    <van-col span="19" class="text">{{ item.username }}</van-col>
				</van-row>
                <van-row style="padding-top: 8px">
                    <van-col span="5" class="text">收款账户:</van-col>
                    <van-col span="19" class="text">{{ item.bankname }}</van-col>
                </van-row>
                <van-row style="padding-top: 8px">
                    <van-col span="5" class="text">收款账号:</van-col>
                    <van-col span="19" class="text">{{ item.bankcard }}</van-col>
                </van-row>
				<van-row style="padding-top: 8px">
				    <van-col span="5" class="text">收款码：</van-col>
				    <van-col span="19" class="text"><img :src="getimg(item.bankimg)" style="width: 100%;"/> </van-col>
				</van-row>
                <van-row style="padding-top: 8px" v-if="item.beizhu != ''">
                    <van-col span="5" class="text">备注说明:</van-col>
                    <van-col span="19" class="text2">{{ item.beizhu }}</van-col>
                </van-row>
				<div style="text-align: right;padding-top: 10px;" v-if="item.ispay == 0">
					<van-button  type="danger" size="small" @click="tixian(item.id)">提现</van-button>
				</div>
				
            </div>
        </div>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import {
    Col,
    Row,
    Toast,
    NoticeBar,
    Empty,
    Cell,
    CellGroup,
	Tab,
	Tabs,
	Dialog
} from "vant";
Vue.use(Col);
Vue.use(Row);
Vue.use(Toast);
Vue.use(NoticeBar);
Vue.use(Empty);
Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Dialog);
export default {
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            list: [],
            lx: 0,
			active:0,
        };
    },
    created() {
       this.getdata(this.active)
    },
    methods: {
		onClick() {
		  this.getdata(this.active)
		},
		getimg: function (img) {
		  if (img != "") {
		    return this.$config.send_url + "Upload/" + img;
		  }
		},
        getdata: function (ispay) {
            let _this = this;
			
				  let _toast = _this.$toast.loading({
				      message: "正在加载...",
				      duration: 0, // 持续展示 toast
				  });
				  _this.$request.post(
				      "api/WalletsTixian/TxList",
				      {
				          token: _this.$utils.getloc("token"),
				          userid: _this.$utils.getloc("userid"),
				          uid: _this.$utils.getloc("id"),
				  		state:ispay
				      },
				      (res) => {
				          if (res.data.code == 0) {
				              _this.$dialog.alert({
				                  title: "提示",
				                  message: res.data.msg,
				              });
				              return;
				          }
				          _this.list = res.data.data;
				  		//console.log(res.data.data);
				      }
				  );
				  
			 
           
        },
		tixian: function (id) {
		    let _this = this;
			Dialog.confirm({
			  title: '提示',
			  message: '确定要审核通过吗？',
			})
			  .then(() => {
		    let _toast = _this.$toast.loading({
		        message: "正在加载...",
		        duration: 0, // 持续展示 toast
		    });
		    _this.$request.post(
		        "api/WalletsTixian/TxPass",
		        {
		            token: _this.$utils.getloc("token"),
		            userid: _this.$utils.getloc("userid"),
		            uid: _this.$utils.getloc("id"),
					tid:id
		        },
		        (res) => {
		            if (res.data.code == 0) {
		                _this.$dialog.alert({
		                    title: "提示",
		                    message: res.data.msg,
		                });
		                return;
		            }
					
						Toast.success(res.data.msg);
					
					setTimeout(()=>{
					_this.getdata(0);
		            },1500);
		        }
		    );
			 })
		},
    },
};
</script>

<style scoped>
.card {
    padding: 10px 10px;
    border-bottom: solid 1px rgba(128, 128, 128, 0.1);
}
.text {
    font-size: 13px;
    color: #838d94;
}
.text2 {
    font-size: 14px;
    color: #222;
}
.pricetext {
    padding-bottom: 10px;
    line-height: 15px;
    font-size: 20px;
    font-weight: 600;
}
.operationtype {
    width: 40px;
    height: 40px;
    border-radius: 40px;
    text-align: center;
    display: table-cell;
    vertical-align: middle;
    font-weight: 500;
}

.ot0 {
    background: #555;
    color: #ffffff;
}
.allContent {
    padding: 15px;
    background: white;
    /* border-radius: 10px; */
}

.newsTitle {
    padding-top: 5px;
    font-size: 18px;
    font-weight: 700;
    color: #2d3436;
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
}

.newsContent {
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 4;
    -webkit-box-orient: vertical;
    font-size: 12px;
    color: #949190;
}

.newsdate {
    color: #949190;
    padding: 10px 10px 5px 0;
    font-size: 12px;
    text-align: right;
}

.beizhu {
    font-size: 12px;
    padding: 6px 0 10px 15px;
}

.beizhuxq {
    font-size: 14px;
    padding: 5px 15px 10px 20px;
    text-align: right;
    color: #9d9f9f;
}

.datetext span {
    font-size: 12px;
}
</style>
