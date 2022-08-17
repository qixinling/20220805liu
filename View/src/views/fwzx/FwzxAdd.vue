<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="cardBox">
            <div class="cardContent">
                <van-field label="代理级别" v-model="dlname" @click="showPopup" placeholder="请选择代理级别" is-link />
                <van-popup v-model="show" round position="bottom" :style="{ height: '30%' }">
                    <van-picker show-toolbar :columns="slots" value-key="name" @confirm="onConfirm" @cancel="cancel" />
                </van-popup>
                <van-field label="所在地区" v-model="shengshixian" @click="showPopup1" placeholder="请选择所在地区" is-link />
                <van-popup v-model="show1" round position="bottom" :style="{ height: '30%' }">
                    <van-area title="" :area-list="areaList" @confirm="addressChange" />
                </van-popup>
                <van-field v-model.trim="address" label="详细地址" placeholder="请输入详细地址" />
            </div>
        </div>
        <div class="cardBox">
            <van-button style="background-color:#f7b226;color: #fff;" @click="shenqing" block>申请</van-button>
        </div>
        <div class="cardBox" v-if="apply != ''" style="padding-top: 20px;">
            <div class="cardContent">
                <van-notice-bar wrapable background="#fff" :scrollable="false" :text="apply" />
            </div>
        </div>

        <div class="cardBox">
            <div class="fuyanBtn" v-on:click="fuyanshow = !fuyanshow">
                我要附言
                <van-icon name="arrow-down" v-if="!fuyanshow" />
                <van-icon name="arrow-up" v-if="fuyanshow" />
            </div>
        </div>
        <div class="cardBox" v-if="fuyanshow">
            <div class="cardContent">
                <van-field v-model="beizhu" label="附言" placeholder="可不填" />
            </div>
        </div>
        <div class="tishi">
            <div class="article" v-html="articleData"></div>
        </div>

        <Login></Login>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Login from "@/components/Login";
import areaList from "@/assets/js/area.js";
import Vue from "vue";
import { Field, Popup, Picker, Area, Button, Col, Row, NoticeBar } from "vant";
Vue.use(Area);
Vue.use(Field);
Vue.use(Popup);
Vue.use(Picker);
Vue.use(Button);
Vue.use(Col);
Vue.use(Row);
Vue.use(NoticeBar);
export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            areaList,
            searchResult: [],
            username: "",
            usertel: "",
            sheng: "",
            shi: "",
            xian: "",
            address: "",
            shengshixian: "",
            isdefu: "",
            areaCode: "",
            show: false,
            show1: false,
            dlname: "",
            bl: "",
            slots: [
                {
                    values: [
                        {
                            bl: 0,
                            name: "级别",
                        },
                    ],
                },
            ],
            apply: "",
            fuyanshow: false,
            beizhu: "",
            articleData: "",
        };
    },
    created() {
        this.getbdlevel();
        this.getdata();
        this.getArticle();
    },
    methods: {
        showPopup: function () {
            this.show = true;
        },
        showPopup1: function () {
            this.show1 = true;
        },
        onConfirm(e) {
            this.dlname = e[0].name;
            this.bl = e[0].bl;
            this.show = false;
        },
        addressChange: function (e) {
            this.sheng = e[0].name;
            this.shi = e[1].name;
            this.xian = e[2].name;
            this.shengshixian = this.sheng + this.shi + this.xian;
            this.show1 = false;
        },
        cancel() {
            this.show = false;
            this.shou1 = false;
        },
        getbdlevel: function () {
            let _this = this;
            this.$request.post(
                "Api/Level/GetBdlevelList",
                {},
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.slots[0].values = [];
                    res.data.data.forEach((item, index) => {
                        if (index > 0) {
                            var data = {
                                bl: index,
                                name: item.name,
                            };
                            _this.slots[0].values.push(data);
                        }
                    });
                    _this.bl = _this.slots[0].values[0].bl;
                    _this.dlname = _this.slots[0].values[0].name;
                },
                true
            );
        },
        getdata: function () {
            if (this.$store.state.LoginState) {
                let _this = this;
                this.$request.post(
                    "Api/UsersFwzxApply/Get",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                    },
                    (res) => {
                        if (res.data.code == 0) {
                            _this.$dialog.alert({
                                title: "提示",
                                message: res.data.msg,
                            });
                            return;
                        }
                        if (res.data.data.length > 0) {
                            var fw = res.data.data;
                            _this.apply =
                                fw.bdlevelname +
                                "申请  " +
                                fw.bdsheng +
                                fw.bdshi +
                                fw.bdxian +
                                fw.bdaddress +
                                "     " +
                                fw.ispayname;
                        }
                    }
                );
            }
        },
        panduan() {
            let mod = {
                pass: false,
                msg: "未知错误",
            };
            if (!this.$store.state.LoginState) {
                mod.msg = "请先登录再添加地址";
            } else if (
                (this.sheng == "" || this.sheng == null) &&
                (this.shi == "" || this.shi == null) &&
                (this.xian == "" || this.xian == null)
            ) {
                mod.msg = "请选择所在地区";
            } else if (this.address == "" || this.address == null) {
                mod.msg = "请输入详细地址";
            } else {
                mod.pass = true;
                mod.msg = "已通过";
            }
            return mod;
        },
        shenqing: function () {
            let mod = this.panduan();
            if (mod.pass) {
                let _this = this;
                this.$request.post(
                    "Api/UsersFwzxApply/Add",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                        add_sheng: _this.sheng,
                        add_shi: _this.shi,
                        add_xian: _this.xian,
                        add_address: _this.address,
                        bdlevel: _this.bl,
                        beizhu: _this.beizhu,
                    },
                    (res) => {
                        _this.$notify({
                            type: "success",
                            message: res.data.msg,
                        });
                        _this.getdata();
                    }
                );
            } else {
                this.$toast({
                    message: mod.msg,
                    position: "bottom",
                    duration: 2000,
                });
            }
        },
        getArticle: function () {
            let _this = this;
            this.$request.post(
                "Api/Article/Get",
                {
                    select_id: 7,
                },
                (res) => {
                    if (res.data.code == 100) {
                        _this.articleData = res.data.data.article_content;
                    } else {
                        _this.$notify({
                            message: res.data.msg,
                        });
                    }
                }
            );
        },
    },
};
</script>

<style scoped>
.applycol {
    padding-bottom: 10px;
}

.tishi {
    margin-top: 30px;
    padding-left: 15px;
    padding-right: 15px;
    color: #888;
    line-height: 28px;
    font-size: 12px;
}

.fuyanBtn {
    padding: 10px;
    text-align: right;
    color: #1989fa;
    font-size: 12px;
}

/deep/ img {
    width: 100%;
}

/deep/ .van-cell {
    background-color: #3B3E47;
    color: #fff;
}

/deep/ .van-field__control {
    color: #fff;
}

.van-cell::after {
    border: 1px solid rgba(88, 88, 88, .6);
}
</style>
