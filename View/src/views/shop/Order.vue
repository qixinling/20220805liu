<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <van-tabs title-active-color="#e89715" :border="false" v-model="active" @click="onClick">
            <!-- <van-tab title="全部" name="999"></van-tab> -->
            <van-tab title="待付款" :badge="ordernum[0] === 0 ? '' : ordernum[0]" name="0"></van-tab>
            <van-tab title="待发货" :badge="ordernum[1] === 0 ? '' : ordernum[1]" name="1"></van-tab>
            <van-tab title="待收货" :badge="ordernum[2] === 0 ? '' : ordernum[2]" name="2"></van-tab>
            <van-tab title="已收货" :badge="ordernum[3] === 0 ? '' : ordernum[3]" name="3"></van-tab>
            <!-- <van-tab title="已退款" :badge="ordernum[4] === 0 ? '' : ordernum[4]" name="4"></van-tab> -->
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
							
                            <!-- 合计<span class="number" v-for="(item, index) in dd.bill.dbBillAmount" :key="index">￥{{ item.amount }}</span>
 -->
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
    Image as VanImage,
    Cell,
    CellGroup,
    CountDown,
    Empty,
} from "vant";

Vue.use(CountDown);
Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Tab);
Vue.use(Tabs);
Vue.use(VanImage);
Vue.use(Dialog);
Vue.use(Empty);
export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            active: 0,
            list: [],
            ordernum: [],
        };
    },
    created() {
        var state = this.$route.query.state;
        if (state == null || state == "") {
            this.active = 0;
        } else {
            this.active = state;
        }
        this.load();
    },
    methods: {
        load: function () {
            if (this.$store.state.LoginState) {
                this.getordernum();
            }
        },
        done() {
            let _this = this;
            setTimeout(function () {
                _this.$router.go(0);
            }, 3000);
        },
        onClick(event) {
            this.active = event;
            this.getordernum();
        },
        shouhuo: function (sid) {
            Dialog.confirm({
                title: "收货",
                message: "确定要收货吗？",
            }).then(() => {
                let _this = this;
                _this.$request.post(
                    "api/ShopOrder/Shouhuo",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                        sid: sid,
                    },
                    (res) => {
                        if (res.data.code == 0) {
                            _this.$dialog.alert({
                                title: "提示",
                                message: res.data.msg,
                            });
                            return;
                        }
                        _this.$notify({
                            type: "success",
                            message: res.data.msg,
                        });
                        _this.getordernum();
                    }
                );
            });
        },
        pay: function (id) {
            Dialog.confirm({
                title: "支付",
                message: "确定要支付此订单吗？",
            }).then(() => {
                let _this = this;
                _this.$request.post(
                    "api/ShopOrder/Pay",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                        id: id,
                    },
                    (res) => {
                        if (res.data.code == 0) {
                            _this.$dialog.alert({
                                title: "提示",
                                message: res.data.msg,
                            });
                            return;
                        }
                        _this.$notify({
                            type: "success",
                            message: res.data.msg,
                        });
                        _this.getordernum();
                    }
                );
            });
        },
        getdata: function () {
            let _this = this;
            _this.list = [];
            _this.$request.post(
                "api/ShopOrder/List",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    orderstate: _this.active,
					lx:1
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
        getordernum: function () {
            let _this = this;
            _this.$request.post(
                "api/ShopOrder/Allordernum",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
					lx:1
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.ordernum = JSON.parse(res.data.data[0].ocount);
                    _this.getdata();
                }
            );
        },
        getimg: function (img) {
            return this.$config.send_url + "Upload/" + img;
        },
    },
};
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
</style>
