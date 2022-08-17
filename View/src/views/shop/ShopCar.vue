<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div style="padding: 15px;">
            <span style="color:#641602;font-size: 30px;font-weight: 600;">购物车</span>
            <div style="font-size: 13px;color: #B5B6B8;">共{{goodsnum}}件商品</div>
        </div>
        <div class="cardBox">
            <div class="cardContent">
                <van-cell title="收货地址" is-link value="选择地址" v-if="arslist.length > 0" @click="addressclick()" />
                <van-cell title="收货地址" is-link value="添加地址" v-if="arslist.length == 0" to="/Address" />
                <div style="padding: 10px;" v-if="address != '' ">
                    <van-row>
                        <van-col span="4" style="text-align: center;padding-left: 5px;padding-top: 5px;">
                            <div class="shou shou-color">收</div>
                        </van-col>
                        <van-col span="20">
                            <div class="user">{{ address.username }} {{ address.usertel }}</div>
                            <div class="address">{{address.sheng+address.shi+address.xian+address.address }}</div>
                        </van-col>
                    </van-row>
                </div>
            </div>
        </div>

        <van-popup position="bottom" round v-model="show" :style="{ height: '90%' }">
            <van-row style="padding: 15px;">
                <van-col span="12">
                    <van-icon name="arrow-left" color="#B5B6B8" @click="show=false" />
                </van-col>
                <van-col span="12">
                    <div style="text-align: right;font-size: 14px;color: #B5B6B8;"><span @click="toaddress">添加新地址</span></div>
                </van-col>
            </van-row>
            <div style="padding-top: 13px;">
                <div v-for="(ars,index) in arslist" :key="index" style="border-bottom: 1px solid #ebedf0;">
                    <van-row @click="arsclick(ars)" class="arsrow">
                        <van-col span="4" style="text-align: center;padding-left: 10px;">
                            <div class="shou shou-color">收</div>
                        </van-col>
                        <van-col span="20">
                            <div>{{ ars.username }} {{ ars.usertel }}</div>
                            <div class="address">
                                <van-tag v-if="ars.isDefault==1" type="warning" style="height: 13px;">默认</van-tag>
                                <span style="padding-left: 5px;">{{ ars.sheng+ars.shi+ars.xian+ars.address }}</span>
                            </div>
                        </van-col>
                    </van-row>
                </div>
            </div>
        </van-popup>
        <div class="cardBox" v-for="(item, index) in goodslist" :key="index" style="padding-bottom: 0px;">
            <div class="cardContent" style="padding-top: 10px;">
                <van-row>
                    <van-col span="2" style="display: flex;justify-content: center;align-items: center;">
                        <van-checkbox v-model="item.gchecked" class="goodscheck" @click="jsPrice"></van-checkbox>
                    </van-col>
                    <van-col span="6" class="goodsimg">
                        <img :src="getimg(item.goodsimg)" />
                    </van-col>
                    <van-col span="16">
                        <van-col span="24" class="goodsname">{{ item.goodsname }}</van-col>
                        <van-col span="24" class="goodscontent van-ellipsis">{{item.prompt}}</van-col>
                        <van-col span="24" class="goodsprice">¥{{ item.sjine }}</van-col>
                    </van-col>

                </van-row>

                <van-row>
                    <van-col span="21" class="count">
                        <van-stepper v-model="item.num" @change="jsPrice" />
                    </van-col>
                    <van-col span="3" class="delete">
                        <van-icon name="delete" size="20" @click="delGoods(index)" />
                    </van-col>
                </van-row>
            </div>
        </div>
        <van-submit-bar :price="sumPrice" button-text="提交订单" @submit="onSubmit">
            <van-checkbox v-model="checked" @click="allChecked">全选</van-checkbox>
        </van-submit-bar>

        <!-- <div style="height:50px;"></div> -->
        <Login></Login>
        <!-- <BottomBar></BottomBar> -->
    </div>
</template>

<script>
import Login from "@/components/Login";
import BottomBar from "@/components/BottomBar";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import {
    Checkbox,
    CheckboxGroup,
    Stepper,
    Dialog,
    Tag,
    Icon,
    Popup,
    Cell,
    CellGroup,
    SubmitBar,
} from "vant";

Vue.use(SubmitBar);
Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Checkbox);
Vue.use(CheckboxGroup);
Vue.use(Stepper);
Vue.use(Dialog);
Vue.use(Tag);
Vue.use(Icon);
Vue.use(Popup);

export default {
    components: {
        BottomBar,
        Login,
        HeadBar
    },
    data() {
        return {
            title: document.title,
            checked: true,
            goodslist: [],
            goodsnum: 0,
            address: [],
            sumPrice: 0,
            gid: 0,
            addressid: 0,
            arslist: [],
            show: false,
        };
    },
    created() {
        this.load();

        if (
            JSON.parse(localStorage.getItem("goodslist")) != null &&
            JSON.parse(localStorage.getItem("goodslist")).length > 0
        ) {
            // 查询购物车商品
            this.goodslist = JSON.parse(localStorage.getItem("goodslist"));
            this.jsPrice();
        }

        if (this.$route.query.gid != null) {
            this.gid = this.$route.query.gid;
        }
    },
    methods: {
        load: function () {
            //登录组件登录成功后会触发该函数,刷新父窗体数据
            if (this.$store.state.LoginState) {
                //子组件调用加载
                this.getdata();
            }
        },
        toaddress() {
            this.$router.push({
                name: "Address",
            });
        },
        addressclick() {
            this.show = true;
        },
        arsclick(item) {
            this.address = [];
            this.address = item;
            this.addressid = item.id;
            this.show = false;
        },
        getdata: function () {
            // 查询收货地址
            let _this = this;
            this.$request.post(
                "Api/UsersAddress/List",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    uid: _this.$utils.getloc("id"),
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.arslist = res.data.data;
                    console.log(_this.arslist)
                    _this.arslist.forEach((item) => {
                        if (item.isDefault == 1) {
                            _this.address = item;
                            _this.addressid = item.id;
                        }
                    });
                    if (_this.address == "" && _this.arslist.length > 0) {
                        _this.address = _this.arslist[0];
                        _this.addressid = _this.arslist[0].id;
                    }
                }
            );
        },

        getimg: function (img) {
            if (img != "" && img != null) {
                return this.$config.send_url + "Upload/" + img;
            }
        },

        allChecked: function () {
            // 全选
            if (this.checked) {
                this.goodslist.forEach((item) => {
                    item.gchecked = true;
                });
            } else {
                this.goodslist.forEach((item) => {
                    item.gchecked = false;
                });
            }
            this.jsPrice();
        },

        onSubmit: function () {
            let _this = this;
            // 提交订单
            if (!_this.$store.state.LoginState) {
                _this.$toast({
                    message: "请先登录再下单",
                    position: "bottom",
                    duration: 1500,
                });
                return;
            }

            if (_this.address.length <= 0) {
                _this.$toast({
                    message: "请添加收货地址",
                    position: "bottom",
                    duration: 1500,
                });
                return;
            }

            var list = _this.goodslist;
            var qlist = [];
            var hlist = [];
            list.forEach((item) => {
                if (item.gchecked) {
                    qlist.push(item);
                } else {
                    hlist.push(item);
                }
            });

            if (qlist.length <= 0) {
                _this.$toast({
                    message: "请选择要下单的商品",
                    position: "bottom",
                    duration: 1500,
                });
                return;
            }

            Dialog.confirm({
                title: "下订单",
                message: "确定要下订单吗？",
            })
                .then(() => {
                    this.$request.post(
                        "Api/ShopOrder/Add",
                        {
                            token: _this.$utils.getloc("token"),
                            userid: _this.$utils.getloc("userid"),
                            order_childlist: JSON.stringify(qlist),
                            aid: _this.addressid,
                        },
                        (res) => {
                            if (res.data.code == 0) {
                                _this.$dialog.alert({
                                    title: "提示",
                                    message: res.data.msg,
                                });
                                return;
                            }
                            _this.$toast({
                                message: res.data.msg,
                                position: "bottom",
                                duration: 1500,
                            });
                            _this.goodslist = hlist;
                            localStorage.setItem(
                                "goodslist",
                                JSON.stringify(hlist)
                            );

                            _this.$router.push({
                                name: "Order",
                            });
                        }
                    );
                })
                .catch(() => {});
        },

        jsPrice: function () {
            // 计算总价
            var i = 0;
            this.sumPrice = 0;
            this.goodsnum = 0;

            this.goodslist.forEach((item) => {
                this.goodsnum += 1;
                if (item.gchecked) {
                    i++;
                    this.sumPrice += item.sjine * item.num * 100;
                }
            });

            localStorage.setItem("goodslist", JSON.stringify(this.goodslist));

            if (i == this.goodslist.length) {
                this.checked = true;
            } else {
                this.checked = false;
            }
        },

        delGoods: function (e) {
            // 删除商品
            this.goodslist.splice(e, 1);

            localStorage.setItem("goodslist", JSON.stringify(this.goodslist));
            this.jsPrice();
            this.$toast({
                message: "删除成功",
                position: "bottom",
                duration: 1000,
            });
        },
    },
};
</script>

<style scoped>
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

.van-cell__value {
    font-size: 12px;
}

.user {
    font-size: 14px;
    font-weight: 300;
}

.address {
    margin-top: 10px;
    font-size: 12px;
    color: #B5B6B8;
}

.goodsimg {
    text-align: center;
    padding: 5px;
}

.goodsimg img {
    border-radius: 10px;
    width: 70px;
}

.goodsname {
    font-size: 14px;
    padding-left: 10px;
    font-weight: 600;
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: 2;
    overflow: hidden;
}

.goodscontent {
    font-size: 14px;
    padding: 5px 0 0 10px;
    color: #888;
}

.goodsprice {
    padding: 5px 0 0 10px;
    color: red;
    font-weight: 600;
}

.count {
    padding-top: 5px;
    text-align: right;
}

.delete {
    text-align: right;
    padding: 10px 10px 0 0;
}

.goodscheck {
    margin-top: 25px;
}

/deep/.van-checkbox__icon--checked .van-icon {
    background-color: #F7B226;
    border-color: #F7B226;
}

/deep/.van-stepper__input {
    margin: initial;
}

/* /deep/.van-submit-bar__button--danger {
    background: linear-gradient(to right, #BB982D, #BB982D);
} */

/* /deep/.van-submit-bar {
    margin-bottom: 50px;
} */

/* .cardContent .van-cell {
    background-color: #3B3E47;
    color: #B5B6B8;
} */

/* .van-stepper {
    background-color: #3B3E47 !important;
} */

/* .van-submit-bar {
    background-color: #3B3E47;
    border-bottom: 1px solid rgba(88, 88, 88, .6);
} */

/deep/ .van-checkbox__label {
    color: #fff;
}

.van-submit-bar__text {
    color: #fff;
}

/* .van-cell::after {
    border: 1px solid rgba(88, 88, 88, .6);
} */

/* .van-popup {
    background-color: #3B3E47;
} */

/deep/ .van-popup .van-cell__value {
    color: #3B3E47 !important;
}
</style>
