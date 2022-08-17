<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="cardBox">
            <div class="cardContent">
                <van-cell title="收货地址" is-link value="选择地址" v-if="arslist.length > 0" @click="addressclick()" />
                <van-cell title="收货地址" is-link value="添加地址" v-if="arslist.length == 0" to="/Address" />
                <div style="padding: 10px" v-if="address != ''">
                    <van-row>
                        <van-col span="4" style="text-align: center; padding-left: 5px; padding-top: 5px">
                            <div class="shou">收</div>
                        </van-col>
                        <van-col span="20">
                            <div class="user">{{ address.name }} {{ address.tel }}</div>
                            <div class="address">{{ address.address }}</div>
                        </van-col>
                    </van-row>
                </div>
            </div>
        </div>

        <van-popup position="bottom" round v-model="show" :style="{ height: '90%' }">
            <van-row style="padding: 15px">
                <van-col span="12">
                    <van-icon name="arrow-left" @click="show = false" />
                </van-col>
                <van-col span="12">
                    <div style="text-align: right; font-size: 14px; color: #555">
                        <span @click="toaddress">添加新地址</span>
                    </div>
                </van-col>
            </van-row>
            <div style="padding-top: 13px">
                <div v-for="(ars, index) in arslist" :key="index" style="border-bottom: 1px solid #ebedf0">
                    <van-row @click="arsclick(ars)" class="arsrow">
                        <van-col span="4" style="text-align: center; padding-left: 10px">
                            <div class="shou">收</div>
                        </van-col>
                        <van-col span="20">
                            <div>{{ ars.name }} {{ ars.tel }}</div>
                            <div class="address">
                                <van-tag v-if="ars.isDefault == 1" type="warning" style="height: 13px">默认</van-tag>
                                <span style="padding-left: 5px">{{ ars.address }}</span>
                            </div>
                        </van-col>
                    </van-row>
                </div>
            </div>
        </van-popup>

        <div class="cardBox" v-for="(item, index) in goodslist" :key="index">
            <div class="cardContent">
                <van-row>
                    <van-col span="6">
                        <img :src="item.goodsimg" style="width: 100%" />
                    </van-col>
                    <van-col span="16" class="goodsname">{{ item.goodsname }}</van-col>
                    <van-col span="16" class="goodscontent van-ellipsis">{{
            item.prompt
          }}</van-col>
                    <van-col span="8" class="goodsprice">¥{{ item.sjine }}</van-col>
                    <van-col span="8" class="count">
                        <van-stepper v-model="item.num" min="0" @change="jsPrice" />
                    </van-col>
                </van-row>
            </div>
        </div>
        <van-submit-bar :price="sumPrice" button-text="升 级" @submit="onSubmit">
            <div>{{ ulevelname }} {{ lsk }}</div>
        </van-submit-bar>
        <Login></Login>
    </div>
</template>

<script>
import Login from "@/components/Login";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import {
    Stepper,
    SubmitBar,
    ContactCard,
    Dialog,
    Cell,
    Tag,
    Icon,
    Popup,
} from "vant";
Vue.use(Stepper);
Vue.use(SubmitBar);
Vue.use(ContactCard);
Vue.use(Dialog);
Vue.use(Cell);
Vue.use(Tag);
Vue.use(Icon);
Vue.use(Popup);

export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            goodslist: [],
            address: [],
            sumPrice: 0,
            gid: 0,
            addressid: 0,
            ulevel: "",
            ulevelname: "",
            lsk: "",
            arslist: [],
            show: false,
        };
    },
    created() {
        this.load();
        if (
            this.$route.query.ulevel != "" &&
            this.$route.query.lsk != "" &&
            this.$route.query.ulevelname != ""
        ) {
            this.ulevel = this.$route.query.ulevel;
            this.lsk = this.$route.query.lsk;
            this.ulevelname = this.$route.query.ulevelname;
        }
    },
    methods: {
        load: function () {
            //登录组件登录成功后会触发该函数,刷新父窗体数据
            if (this.$store.state.LoginState) {
                //子组件调用加载
                this.getaddress();
                this.getgoods();
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
        getaddress: function () {
            // 查询用户地址
            // 查询收货地址
            var _this = this;
            _this.$request.post(
                "api/Address/List",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    id: _this.$utils.getloc("id"),
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

        getgoods: function () {
            // 查询商品
            var _this = this;
            _this.$request.post(
                "api/Goods/List",
                {
                    userid: _this.$utils.getloc("userid"),
                    goodstype: 0,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    var list = res.data.data;
                    list.forEach((item) => {
                        item["num"] = 0;
                        item.goodsimg = item.goodsimg.split(",")[0];
                    });
                    _this.goodslist = list;
                    _this.jsPrice();
                }
            );
        },

        getimg: function (img) {
            if (img != "" && img != null) {
                return this.$config.send_url + "Upload/" + img;
            }
        },

        onSubmit: function () {
            // 升级
            var _this = this;
            // 提交订单
            if (!_this.$store.state.LoginState) {
                _this.$toast({
                    message: "请先登录再升级",
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

            var glist = [];
            var jine = 0;
            _this.goodslist.forEach((item) => {
                if (item.num > 0) {
                    jine += item.num * item.scprice;
                    var goods = {
                        id: item.id,
                        num: item.num,
                    };
                    glist.push(goods);
                }
            });
            if (jine <= 0) {
                _this.$toast({
                    message: "请选择商品再升级",
                    position: "bottom",
                    duration: 1500,
                });
                return;
            }

            Dialog.confirm({
                title: "升级",
                message: "确定要升级吗？",
            })
                .then(() => {
                    let _toast = _this.$toast.loading({
                        message: "正在提交...",
                        duration: 0, // 持续展示 toast
                    });
                    _this.$request.post(
                        "api/Levelup/Add",
                        {
                            token: _this.$utils.getloc("token"),
                            userid: _this.$utils.getloc("userid"),
                            select_uid: _this.$utils.getloc("id"),
                            ulevel: _this.ulevel,
                        },
                        (res) => {
                            if (res.data.code == 0) {
                                _this.$dialog.alert({
                                    title: "提示",
                                    message: res.data.msg,
                                });
                                return;
                            }
                            _toast.clear();
                            _this.$notify({
                                type: "success",
                                message: res.data.msg,
                            });
                            _this.goodslist.forEach((item) => {
                                item.num = 0;
                            });
                        }
                    );
                })
                .catch(() => {});
        },

        jsPrice: function () {
            // 计算总价
            this.sumPrice = 0;
            this.goodslist.forEach((item) => {
                this.sumPrice += item.sjine * item.num * 100;
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
    background: #37a2da;
    width: 35px;
    height: 35px;
    line-height: 35px;
    color: #fff;
    border-radius: 100%;
}
.goodsimg {
    text-align: center;
    padding: 0 5px;
}

.goodsimg img {
    border-radius: 10px;
}

.goodsname {
    font-size: 16px;
    padding-left: 10px;
    font-weight: 600;
}

.goodscontent {
    font-size: 14px;
    padding: 5px 0 0 10px;
    color: #888;
}

.goodsprice {
    padding: 5px 0 0 10px;
    color: red;
}

.count {
    padding-top: 5px;
    text-align: right;
}

.user {
    font-size: 14px;
    font-weight: 300;
}

.address {
    margin-top: 10px;
    font-size: 12px;
    color: #888;
}
</style>
