<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="cardBox">
            <div class="cardContent">
                <van-row style="text-align: center; padding-top: 10px">
                    <van-col span="12">
                        <img :src="tx" class="tx" />
                    </van-col>
                    <van-col span="12">
                        <van-uploader v-model="fileList" :after-read="afterRead" :max-count="1" :max-size="5 * 1024 * 1024" :before-read="beforeRead" @oversize="onOversize" />
                    </van-col>
                </van-row>
                <van-field v-model.trim="userName" label="姓名" placeholder="请输入姓名" />
               <!-- <van-field v-model.trim="userTel" type="tel" placeholder="请输入手机号" label="手机号" /> -->
            </div>
        </div>

        <div class="cardBox">
            <van-button style="background-color: #ff4500; color: #fff" @click="updata" block>修改</van-button>
        </div>

        <!-- <div class="cardBox">
            <div class="cardContent">
                <van-cell title="密码" value="修改密码" icon="edit" is-link to="/Password" />
            </div>
        </div> -->
         <div class="cardBox">
            <div class="cardContent">
                <van-cell title="账户" value="管理收款账户" icon="idcard" is-link to="/Bank" />
               <!-- <van-cell title="收货地址" value="管理收货地址" icon="location-o" is-link to="/Address" /> -->
            </div>
        </div>
        

        <Login></Login>
    </div>
</template>

<script>
import config from "../../assets/js/config.js";
import Login from "@/components/Login";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Field, Uploader } from "vant";
import { Cell, CellGroup } from "vant";

Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Field);
Vue.use(Uploader);
export default {
    name: "UserInfo",
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            fileList: [],
            tx: "",
            userName: "",
            userTel: "",
        };
    },
    created() {
        this.load();
    },
    methods: {
        load: function () {
            //登录组件登录成功后会触发该函数,刷新父窗体数据
            if (this.$store.state.LoginState) {
                //子组件调用加载
                this.getdata();
            }
        },
        afterRead(file) {
            let _this = this;
            _this.fileList[0].message = "上传中...";
            _this.fileList[0].status = "uploading";

            let _formdata = new FormData(); //创建form对象
            _formdata.append("file", file.file);
            let _posturl =
                config.send_url +
                "Api/Upload/UploadImages?token=" +
                encodeURIComponent(_this.$utils.getloc("token")) +
                "&userid=" +
                _this.$utils.getloc("userid") +
                "&width=" +
                120 +
                "&height=" +
                120;
            let _config = {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            };
            this.axios
                .post(_posturl, _formdata, _config)
                .then(function (res) {
                    if (res.data.code == 100) {
                        //console.log(res.data.data.imgname);
                        _this.fileList[0].message = "";
                        _this.fileList[0].status = "";
                        _this.Users_Update_Tx(res.data.data.imgname);
                    } else {
                        _this.fileList[0].message = "上传失败";
                        _this.fileList[0].status = "failed";
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        onOversize(file) {
            // 图片大小校验
            console.log(file);
            this.$toast({
                message: "请上传不5M内的图片",
                position: "bottom",
                duration: 2000,
            });
        },
        beforeRead(file) {
            // 图片格式校验
            if (
                file.type == "image/jpeg" ||
                file.type == "image/png" ||
                file.type == "image/gif"
            ) {
                //console.log("上传成功");
                return true;
            } else {
                this.$toast({
                    message: "请上传 jpg 格式图片",
                    position: "bottom",
                    duration: 2000,
                });
                return false;
            }
        },
        Users_Update_Tx: function (img) {
            var _this = this;
            _this.$request.post(
                "Api/Users/Update_Tx",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    tx: img,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.tx = _this.getimg(img);
                }
            );
        },
        getimg: function (img) {
            if (img != "") {
                return this.$config.send_url + "Upload/" + img;
            }
        },
        getdata: function () {
            // 查询用户资料
            var _this = this;
            _this.$request.post(
                "api/Users/Get",
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
                    _this.tx = res.data.data.tx;
                    console.log(_this.tx)
                    if(_this.tx == "" || _this.tx == "tx.png") {
                        _this.tx = require('@/assets/img/tx.png')
                    } else {
                        _this.tx = _this.getimg(_this.tx)
                    }
                    _this.userName = res.data.data.username;
                    _this.userTel = res.data.data.usertel;
                }
            );
        },
        updata: function () {
            // 修改用户资料
            var _this = this;
            let _toast = _this.$toast.loading({
                message: "正在修改...",
                duration: 0, // 持续展示 toast
            });
            //let regs = /^((13[0-9])|(17[0-1,6-8])|(15[^4,\\D])|(18[0-9]))\d{8}$/;
            // !regs.test(this.usertel)

            if (_this.userName == "") {
                _this.$toast.fail("姓名不能为空");
                return;
            }

            // if (_this.userTel == "") {
            //     _this.$toast.fail("手机号不能为空");
            //     return;
            // }

            _this.$request.post(
                "api/Users/Update",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    update_username: this.userName,
                    //update_usertel: this.userTel,
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
                        position: "bottom",
                        duration: 1000,
                    });
                }
            );
        },
    },
};
</script>

<style scoped>
.tx {
    width: 80px;
    height: 80px;
    border-radius: 80px;
}

.van-cell__value {
    font-size: 12px;
}

/* .van-cell {
    background-color: #3B3E47;
    color: #fff;
} */

/* /deep/.van-field__control {
    color: #fff;
} */

.van-cell__value {
    color: #B5B6B8;
}

.van-button--default {
    border: 0;
}

/* .van-cell::after {
    border: 1px solid rgba(88, 88, 88, .6);
} */
</style>
