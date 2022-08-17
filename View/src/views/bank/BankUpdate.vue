<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="cardBox">
            <div class="cardContent">
                <van-field v-model="bankname" label="账户" placeholder="请选择账户" @click="showPopup" readonly />
                <van-popup v-model="show" round position="bottom" :style="{ height: '30%' }">
                    <van-picker show-toolbar :columns="slots" @confirm="onConfirm" />
                </van-popup>
                <van-field v-model="bankcard" label="账号" placeholder="请输入账号" />
                <van-field v-model="username" label="姓名" placeholder="请输入姓名" />
                <van-field v-model="usertel" label="电话" placeholder="请输入电话号码" />
            </div>
        </div>
        <div class="cardBox">
            <div class="cardContent">
                <UploadPictures :haveImgList="haveImgList" :deletable="true" @getUploadPictures="getUploadPictures" />
            </div>
        </div>

        <div class="cardBox">
            <van-button style="background-color:#ff4500;color: #fff;" round @click="update" block>修改</van-button>
        </div>

        <div class="cardBox">
            <van-button type="default" round @click="del" block>删除</van-button>
        </div>

        <Login></Login>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import UploadPictures from "@/components/UploadPictures";
import Login from "@/components/Login";
import Vue from "vue";
import { Cell, Field, Popup, Picker } from "vant";
import { Uploader } from "vant";
Vue.use(Cell);
Vue.use(Field);
Vue.use(Popup);
Vue.use(Picker);
Vue.use(Uploader);
export default {
    components: {
        HeadBar,
        Login,
        UploadPictures
    },
    data() {
        return {
            title: document.title,
            bankname: "",
            bankcard: "",
            username: "",
            usertel: "",
            fileList: [],
            ewmimg: "",
            slots: [
                {
                    flex: 1,
                    values: [
                        {
                            lxname: "",
                        },
                    ],
                },
            ],
            show: false,
            bid: "",
            haveImgList: [],
        };
    },
    created() {
        this.bid = this.$route.query.id;
        if (this.$store.state.LoginState) {
            this.getselect();
            this.get();
        }
    },
    methods: {
        getimg: function (img) {
            if (img != "" && img != null) {
                return this.$config.send_url + "Upload/" + img;
            }
        },
        onConfirm(value) {
            this.bankname = value[0];
            this.show = false;
        },
        showPopup: function () {
            this.show = true;
        },

        getselect: function () {
            var _this = this;
            _this.$request.post(
                "api/UsersBank/Select",
                {},
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    let data = res.data;
                    //清空默认值
                    _this.slots[0].values = [];
                    //添加响应数据
                    data.forEach((item) => {
                        _this.slots[0].values.push(item);
                    });
                },
                true
            );
        },

        update: function () {
            if (this.bankname == "请选择账户") {
                this.$toast({
                    message: "请选择账户",
                    position: "middle",
                    duration: 2000,
                });
            } else if (this.bankcard == "") {
                this.$toast({
                    message: "请输入账号",
                    position: "middle",
                    duration: 2000,
                });
            } else if (this.username == "") {
                this.$toast({
                    message: "请输入姓名",
                    position: "middle",
                    duration: 2000,
                });
            } else if (this.usertel == "") {
                this.$toast({
                    message: "请输入电话",
                    position: "middle",
                    duration: 2000,
                });
            } else if ((this.bankname == "支付宝" || this.bankname == "微信") && this.ewmimg == "") {
                this.$toast({
                    message: "请上传收款二维码",
                    position: "middle",
                    duration: 2000,
                });
            } else {
                let _toast = this.$toast.loading({
                    message: "正在修改...",
                    duration: 0, // 持续展示 toast
                });
                var _this = this;
                setTimeout(() => {
                    _this.$request.post(
                        "api/UsersBank/Update",
                        {
                            token: _this.$utils.getloc("token"),
                            userid: _this.$utils.getloc("userid"),
                            up_uid: _this.$utils.getloc("id"),
                            up_bankname: _this.bankname,
                            up_bankcard: _this.bankcard,
                            up_username: _this.username,
                            up_usertel: _this.usertel,
                            up_id: _this.bid,
                            up_bankimg: _this.ewmimg,
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
                            _this.$toast({
                                message: res.data.msg,
                                position: "middle",
                                duration: 2000,
                            });
                        }
                    );
                }, 1000);
            }
        },
        getUploadPictures(img) {
            this.ewmimg = img;
        },
        get: function () {
            var _this = this;
            _this.$request.post(
                "api/UsersBank/Get",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    select_uid: _this.$utils.getloc("id"),
                    select_id: _this.bid,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    var data = res.data.data[0];
                    _this.bid = data.id;
                    _this.bankname = data.bankname;
                    _this.bankcard = data.bankcard;
                    _this.username = data.username;
                    _this.usertel = data.usertel;
                    if(this.bankname == "支付宝" || this.bankname == "微信" || data.bankimg != ""){
                        _this.ewmimg = data.bankimg;
                        _this.ewmimg = _this.getimg(_this.ewmimg);
                        _this.haveImgList = [{ url: _this.ewmimg }];
                    }
                }
            );
        },
        del: function () {
            let _toast = this.$toast.loading({
                message: "正在删除...",
                duration: 0, // 持续展示 toast
            });
            var _this = this;
            setTimeout(() => {
                _this.$request.post(
                    "api/UsersBank/Delete",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                        delete_uid: _this.$utils.getloc("id"),
                        delete_id: _this.bid,
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
                        _this.$toast({
                            message: res.data.msg,
                            position: "middle",
                            duration: 2000,
                        });
                        _this.$utils.to(-1);
                    }
                );
            }, 1000);
        },
    },
};
</script>

<style scoped>
.ewmimg {
    width: 150px;
}

.van-uploader {
    padding-top: 15px;
}

.flexbox {
    display: flex;
    justify-content: center;
    align-items: center;
    position: relative;
}

.flexbox-child {
    flex: 1;
    margin: 10px;
}

.van-button--default {
    border: 0;
}
</style>
