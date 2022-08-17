<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>

        <div class="cardBox">
            <div class="cardContent">
                <!-- <van-field v-model="userid" label="用户名" placeholder="请输入用户名" /> -->
                <van-field v-model="usertel" label="手机号" placeholder="请输入手机号" />
                <van-field v-model="password" type="password" label="新密码" placeholder="请输入新密码" />

                <van-field v-model="yanzhengma" center clearable label="短信验证码" placeholder="请输入短信验证码">
                    <template #button>
                        <van-button size="small" type="primary" @click="sendcode">{{ send }}</van-button>
                    </template>
                </van-field>
            </div>
        </div>
        <div class="cardBox">
            <van-button style="background-color: #f7b226; color: #fff" block @click="update">找回密码</van-button>
        </div>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Field, Cell, Button } from "vant";

Vue.use(Field);
Vue.use(Cell);
Vue.use(Button);
export default {
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            userid: "",
            usertel: "",
            yanzhengma: "",
            password: "",
            send: "发送",
			lx:1
        };
    },
	created(){
		this.lx = this.$route.query.lx;
		console.log(this.lx);
	},
    methods: {
        fs: function () {
            console.log("发送成功");
        },

        update: function () {
            var _this = this;
            if (_this.usertel == "") {
                _this.$toast({
                    message: "请输入手机号",
                    position: "bottom",
                    duration: 1500,
                });
                return;
            }

            if (_this.yanzhengma == "") {
                _this.$toast({
                    message: "请输入验证码",
                    position: "bottom",
                    duration: 1500,
                });
                return;
            }

            if (_this.password == "") {
                _this.$toast({
                    message: "请输入新密码",
                    position: "bottom",
                    duration: 1500,
                });
                return;
            }
            _this.$request.post(
                "api/Users/RetrievePassword",
                {
                    userid: _this.usertel,
                    usertel: _this.usertel,
                    yanzhengma: _this.yanzhengma,
                    password: _this.password,
					lx:_this.lx
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

                    _this.userid = "";
                    _this.usertel = "";
                    _this.password = "";
                    _this.yanzhengma = "";
                }
            );
        },

        sendcode: function () {
            let _this = this;
            if (_this.send == "发送") {
                if (this.usertel.length != 11) {
                    this.$toast.fail("请输入11位手机号码");
                    return;
                }

                 let i = 60;
                _this.send = i + "S";
                var timer = setInterval(function () {
                    i--;
                    _this.send = i + "S";
                    if (i <= 0) {
                        clearInterval(timer);
                        _this.send = "发送";
                    }
                }, 1000);

                let _usertel = "aaa" + this.usertel + "bbb";

                _this.$request.post(
                    "api/Sms/SendCode",
                    {
                        usertel: _usertel,
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
                            position: "middle",
                            duration: 2000,
                        });
                    }
                );
            }
        },
    },
};
</script>

<style scoped>
</style>
