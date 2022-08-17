<template>
    <div>
        <HeadBar :title="title"></HeadBar>
        <div class="page">
            <span class="msg-count" v-if="mcount > 0">{{
        mcount > 99 ? "99+" : mcount
      }}</span>
            <van-tabs v-model="active" v-if="ready" sticky offset-top="46">
                <van-tab title="系统消息">
                    <Message></Message>
                </van-tab>
                <van-tab title="我的客服">
                    <ServiceMsg></ServiceMsg>
                </van-tab>
            </van-tabs>
        </div>

        <!-- <Message/> -->
        <Login></Login>
    </div>
</template>

<script>
import Message from "@/views/msg/Message";
import ServiceMsg from "@/views/msg/ServiceMsg";
import Login from "@/components/Login";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Tab, Tabs } from "vant";
Vue.use(Tab);
Vue.use(Tabs);
export default {
    name: "Msg",
    components: {
        Login,
        HeadBar,
        Message,
        ServiceMsg,
    },
    data() {
        return {
            title: document.title,
            active: 0,
            mcount: "",
            ready: false,
        };
    },
    created() {
        this.load();
    },
    watch: {
        active() {
            this.$nextTick(function () {
                this.mcount = "";
            });
        },
    },
    methods: {
        load: function () {
            //登录组件登录成功后会触发该函数,刷新父窗体数据
            if (this.$store.state.LoginState) {
                //子组件调用加载
                this.GetMsgCount();
                this.ready = true;
            }
        },
        GetMsgCount() {
            let _this = this;
            this.$request.post(
                "Api/Msg/getMsgCount",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    lx: 1,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.mcount = res.data.data.msgCount;
                }
            );
        },
    },
};
</script>

<style scoped>
.page {
    position: relative;
}

.msg-count {
    z-index: 9999999;
    position: fixed;
    top: 50px;
    right: 40px;
    height: 20px;
    border-radius: 10px;
    background: red;
    color: white;
    display: flex;
    justify-content: center;
    font-size: 14px;
    padding: 0 5px;
}

/deep/.van-nav-bar--fixed {
    background-color: #057d8d;
}

/* /deep/.van-tabs__line {
    background-color: #e89715;
} */
</style>
