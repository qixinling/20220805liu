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
                <UploadPictures @getUploadPictures="getUploadPictures" :deletable="true"/>
            </div>
        </div>

        <div class="cardBox">
            <van-button style="background-color:#ff4500;color: #fff;" @click="add" block>添加</van-button>
        </div>

        <Login></Login>
    </div>
</template>

<script>
import UploadPictures from "@/components/UploadPictures";
import config from "../../assets/js/config.js";
import HeadBar from "@/components/HeadBar";
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
        UploadPictures,
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
        };
    },
    created() {
        if (this.$store.state.LoginState) {
            this.getselect();
        }
    },
    methods: {
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

        getUploadPictures(img) {
            this.ewmimg = img;
        },
        add: function () {
            if (this.bankname == "") {
                this.$toast({
                    message: "请选择账户",
                    position: "middle",
                    duration: 2000,
                });
            } else if (this.bankcard == "") {
                this.$toast({
                    message: "请输入卡号",
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
                var _this = this;
                let _toast = _this.$toast.loading({
                    message: "正在添加...",
                    duration: 0, // 持续展示 toast
                });
                setTimeout(() => {
                    _this.$request.post(
                        "api/UsersBank/Add",
                        {
                            token: _this.$utils.getloc("token"),
                            userid: _this.$utils.getloc("userid"),
                            add_bankname: _this.bankname,
                            add_bankcard: _this.bankcard,
                            add_username: _this.username,
                            add_usertel: _this.usertel,
                            add_bankimg: _this.ewmimg,
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
    },
};
</script>

<style scoped>
.van-button--default {
    border: 0;
}
</style>
