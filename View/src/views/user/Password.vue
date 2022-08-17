<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <!-- <van-tabs v-model="active" title-active-color="#e89715" :border="false">
            <van-tab title="登录密码"></van-tab>
            <van-tab title="支付密码"></van-tab>
        </van-tabs> -->
        <div v-if="lx == 1">
            <div>
                <div>
                    <van-field v-model="password" type="password" label="原密码" placeholder="请输入原密码" />
                    <van-field v-model="password_new" type="password" label="新密码" placeholder="请输入新密码" style="margin-top:10px" />

                    <van-field v-model="password_new2" type="password" label="确认密码" placeholder="请确认新密码" style="margin-top:10px" />
                </div>
            </div>
			<!-- <div style="text-align: right;font-size: 12px;padding-right: 14px;"><router-link to="/PasswordRetrieve?lx=1">忘记密码</router-link></div> -->
            <div style="margin:30px 20px">
                <van-button style="background-color: #ff4500; color: #fff; border-radius:8px" @click="update(1)" block="">确认修改</van-button>
            </div>
        </div>
        <div v-if="lx == 2">
            <div>
                <div>
                    <van-field v-model="password2" type="password" label="原密码" placeholder="请输入原密码" />
                    <van-field v-model="password2_new" type="password" label="新密码" placeholder="请输入新密码" style="margin-top:10px" />

                    <van-field v-model="password2_new2" type="password" label="确认密码" placeholder="请确认新密码" style="margin-top:10px" />
                </div>
            </div>
			<!-- <div style="text-align: right;font-size: 12px;padding-right: 14px;"><router-link to="/PasswordRetrieve?lx=2">忘记密码</router-link></div> -->
            <div style="margin:30px 20px">
                <van-button style="background-color: #ff4500; color: #fff; border-radius:8px" @click="update(2)" block="">确认修改</van-button>
            </div>
        </div>
        <Login></Login>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Login from "@/components/Login";
import Vue from "vue";

import { Cell, Field, Tab, Tabs } from "vant";
Vue.use(Cell);
Vue.use(Field);
Vue.use(Tab);
Vue.use(Tabs);
export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: "",
            active: 0,
            password: "",
            password_new: "",
            password_new2: "",
            password2: "",
            password2_new: "",
            password2_new2: "",
        };
    },
    created() {
        if(this.$route.query.lx) {
            this.lx = this.$route.query.lx
        }
        if(this.lx == 1) this.title = "修改密码"
        if(this.lx == 2) this.title = "修改支付密码"
    },
    methods: {
        update: function (lx) {
            var _this = this;
            let _toast = this.$toast.loading({
                message: "正在修改...",
                duration: 0, // 持续展示 toast
            });
            if (lx == 1) {
                if (this.password == "") {
                    this.$toast({
                        message: "原密码不能为空",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }

                if (this.password_new == "") {
                    this.$toast({
                        message: "新密码不能为空",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }

                if (this.password == this.password_new) {
                    this.$toast({
                        message: "原密码与新密码一致",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }

                if (this.password_new2 == "") {
                    this.$toast({
                        message: "确认新密码不能为空",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }
                if (this.password_new != this.password_new2) {
                    this.$toast({
                        message: "新密码和确认密码不一致",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }
                _this.$request.post(
                    "api/Users/Update_Password",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                        password_old: _this.password,
                        password_new: _this.password_new2,
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
                        _this.password = "";
                        _this.password_new = "";
                        _this.password_new2 = "";
                    }
                );
            } else if (lx == 2) {
                if (this.password2 == "") {
                    this.$toast({
                        message: "原密码不能为空",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }

                if (this.password2_new == "") {
                    this.$toast({
                        message: "新密码不能为空",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }

                if (this.password2 == this.password2_new) {
                    this.$toast({
                        message: "原密码与新密码一致",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }

                if (this.password2_new2 == "") {
                    this.$toast({
                        message: "确认密码不能为空",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }

                if (this.password2_new != this.password2_new2) {
                    this.$toast({
                        message: "新密码和确认密码不一致",
                        position: "middle",
                        duration: 2000,
                    });
                    return;
                }
                _this.$request.post(
                    "api/Users/Update_Password2",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                        password2_old: _this.password2,
                        password2_new: _this.password2_new,
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
                        _this.password2 = "";
                        _this.password2_new = "";
                        _this.password2_new2 = "";
                    }
                );
            }
        },
    },
};
</script>

<style scoped>
.czbtn {
    width: 170px;
    height: 43px;
    border-radius: 15px;
    font-size: 16px !important;
    margin-top: 30px;
}
/deep/.van-tabs__line {
    background-color: #e89715;
}

.van-button--default {
    border: 0;
}

.cardBox .van-button--default {
    border: 0;
}
</style>
