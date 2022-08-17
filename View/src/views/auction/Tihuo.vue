<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>
    <van-tabs title-active-color="#e89715" :border="false" v-model="active" @click="onClick">
      <van-tab title="待发货" name="1"></van-tab>
      <van-tab title="已发货" name="2"></van-tab>
      <van-tab title="已收货" name="3"></van-tab>
    </van-tabs>

    <div>
      <div v-if="list.length == 0">
        <van-empty description="暂无订单" />
      </div>

      <div class="cardBox" :id="dd.id" v-for="(dd, index) in list" :key="index">
        <div class="cardContent">
          <van-cell :title="'' + dd.orderNo" :to="'/OrderContent?id=' + dd.id" is-link value="详情" />
          <van-row class="oclist" v-for="(item, index) in dd.oclist" :key="index">
            <van-col span="8" style="text-align: center;">
              <van-image width="80px" height="80px" :src="getimg(item.goodsimg)" />
            </van-col>
            <van-col span="16">
              <van-col class="van-ellipsis" span="24">{{ item.goodsname }}</van-col>
              <van-col span="12" class="sjine">￥{{ item.sjine }}</van-col>
              <van-col span="12" class="danjia" style="text-align: right;">
                x
                {{ item.num }}
              </van-col>
            </van-col>
          </van-row>
          <van-row>
            <van-col span="24" class="zongji">
              共
              <span class="number">{{ dd.goodsnum }}</span>
              件商品，
				      合计<span class="number" >￥{{ dd.sjine }}</span>
            </van-col>
          </van-row>
          <van-row class="btn" v-if="dd.orderstate == 0">
            <van-count-down style="background: #fffbe8;color: #ed6a0c;font-size: 14px;line-height: 20px;text-align: right;padding: 10px;" :time="dd.timer" format="订单将在 mm:ss 后取消,请尽快完成支付" @finish="done" />
            <van-col span="24" style="margin-top: 10px;">
              <van-button type="danger" size="small" @click="pay(dd.id)">支付</van-button>
            </van-col>
          </van-row>
          <van-row class="btn" v-if="dd.orderstate == 2">
            <van-col span="24">
              <van-button type="info" size="small" @click="shouhuo(dd.id)">收货</van-button>
            </van-col>
          </van-row>
          <van-row class="btn" v-if="!dd.sheng">
            <van-col span="24">
              <van-button type="info" size="small" @click="tianxie(dd.id)">填写地址</van-button>
            </van-col>
          </van-row>
          <!-- 选择地址 -->
        <van-dialog v-model="showAddress" title="地址" show-cancel-button @confirm="arsclick">
          <div style="padding-top: 13px;">
            <van-radio-group v-model="radio">
                <div v-for="(ars,index) in arslist" :key="index" style="border-bottom: 1px solid #ebedf0;">
                    <van-row class="arsrow">
                        <van-col span="4" style="text-align: center;padding-left: 10px;">
                            <div class="shou shou-color">收</div>
                        </van-col>
                        <van-col span="17">
                            <div>{{ ars.username }} {{ ars.usertel }}</div>
                            <div class="address">
                                <van-tag v-if="ars.isDefault==1" type="warning" style="height: 13px;">默认</van-tag>
                                <span style="padding-left: 5px;">{{ ars.sheng+ars.shi+ars.xian+ars.address }}</span>
                            </div>
                        </van-col>
                        <van-col span="3">
                            <van-radio :name="ars.id"></van-radio>
                        </van-col>
                    </van-row>
                </div>
            </van-radio-group>
            <div class="address-msg" @click="toaddress" v-if="arslist.length == 0">
              暂无地址，请添加地址
              <van-icon name="arrow" />
            </div>
            </div>
        </van-dialog>
        </div>
      </div>
    </div>

    <Login></Login>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Login from "@/components/Login";
import Vue from "vue";
import {
    Tab,
    Tabs,
    Dialog,
    Cell,
    CellGroup,
    Image as VanImage,
    Empty,
    Popup,
    Radio,
    RadioGroup,
    Icon
} from "vant";
Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Dialog);
Vue.use(Empty);
Vue.use(VanImage);
Vue.use(Popup);
Vue.use(Radio);
Vue.use(RadioGroup);
Vue.use(Icon)

export default {
  name: '',
  components: {
    HeadBar,
    Login
  },
  data () {
    return {
      title: document.title,
      active: 1,
      list: [],
      showAddress: false,
      arslist: [],
      oid: "",
      radio: ""
    }
  },
  created() {
    this.load();
  },
  methods: {
    load: function () {
      if (this.$store.state.LoginState) {
        this.getdata()
        this.getAddressList()
      }
    },
    getimg: function (img) {
      return this.$config.send_url + "Upload/" + img;
    },
    onClick(event) {
      this.active = event;
      this.getdata();
    },
    done() {
      let _this = this;
      setTimeout(function () {
        _this.$router.go(0);
      }, 3000);
    },
    getdata() {
        let _this = this;
        _this.list = [];
        _this.$request.post(
            "api/ShopOrder/List",
            {
                token: _this.$utils.getloc("token"),
                userid: _this.$utils.getloc("userid"),
                orderstate: _this.active,
        		lx:2
            },
            (res) => {
                if (res.data.code == 0) {
                    _this.$dialog.alert({
                        title: "提示",
                        message: res.data.msg,
                    });
                    return;
                }
                let _list = res.data.data;
                console.log(_list);
                _list.forEach((item) => {
                    item.oclist = JSON.parse(item.oclist);
                    for (var i = 0; i < item.oclist.length; i++) {
                        item.oclist[i].goodsimg =
                            item.oclist[i].goodsimg.split(",")[0];
                    }
                    _this.list.push(item);
                });
            }
        );
    },
    // 获取地址
    getAddressList() {
      let that = this
      that.$request.post(
        "Api/UsersAddress/List",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
          uid: that.$utils.getloc("id"),
        },
        (res) => {
          if (res.data.code == 0) {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
            return;
          }
          that.arslist = res.data.data;
          console.log(that.arslist)
        }
      )
    },
    tianxie(id) {
      this.oid = id
      this.showAddress = true
    },
    // 选择地址
    arsclick() {
      var aid = this.radio
      var oid = this.oid
      let that = this
      that.$request.post(
        "api/ShopOrder/Dizhi",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
          oid: oid,
          aid: aid
        },
        (res) => {
          if (res.data.code == 0) {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
            return;
          }
          that.aid = ""
          that.oid = ""
          that.$dialog.alert({
            title: "提示",
            message: res.data.msg,
          });
          that.getdata()
        }
      )
    },
    // 跳转添加地址
    toaddress() {
      this.$router.push({
        name: "Address",
      });
    }
  }
}
</script>

<style scoped>
.van-ellipsis {
    font-size: 14px;
}

.oclist {
    margin-top: 10px;
    padding: 10px;
}

.sjine {
    padding-top: 40px;
    font-size: 16px;
    color: red;
}

.danjia {
    padding-top: 40px;
    font-size: 12px;
    color: #888;
}

.zongji {
    text-align: right;
    font-size: 12px;
    color: #888;
    margin-top: 10px;
    margin-bottom: 10px;
}

.number {
    font-size: 16px;
    font-weight: 500;
    color: orangered;
}

.btn {
    text-align: right;
    margin-bottom: 10px;
}

/deep/.van-tabs__line {
    background-color: #e89715;
}

.arsrow {
    display: flex;
    align-items: center;
    justify-content: center;
    padding-bottom: 12px;
    padding-top: 12px;
}

.shou {
    width: 35px;
    height: 35px;
    line-height: 35px;
    color: #fff;
    border-radius: 100%;
}

.shou-color {
    background-color: #F7B226;
}

.address {
    margin-top: 10px;
    font-size: 12px;
    color: #B5B6B8;
}

.address-msg {
  color: red;
  text-align: center;
  padding: 20px 0 30px;
  font-size: 14px;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>