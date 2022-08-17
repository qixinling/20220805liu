<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <van-swipe class="my-swipe" indicator-color="white" @change="onChange">
            <van-swipe-item v-for="(item, index) in ulevellist" :key="index">
                <div class="cardBox">
                    <van-row>
                        <van-col span="24" class="ulevelcol">
                            <!-- <div class="dj">{{ item }} {{ lsklist[index] }}</div> -->
                            <!-- <img :src="require('@/assets/img/n' + index + '.jpg')" /> -->
                            <img :src="require('@/assets/img/re' + (index + 1) + '.png')" />
                        </van-col>
                    </van-row>
                </div>
            </van-swipe-item>
            <template #indicator>
                <div class="custom-indicator">
                    当前级别 {{ ulevel }}/{{ ulevellist.length }} 可选
                </div>
            </template>
        </van-swipe>
        <div class="form">
            <div class="cardBox form-list">
                <div class="cardContent border-radius">
                    <van-cell title="当前级别" :value="user.ulevelname" />
                    <van-cell title="选中级别" :value="ulevelname" label="左右滑动选择升级级别" center />
                    <van-cell title="升级金额" :value="cha" />
                </div>
                <van-button style="margin-top: 20px; background-color: #f7b226; color: #fff" size="large" @click="onSubmit">升级</van-button>
            </div>
        </div>
        <Login></Login>
    </div>
</template>

<script>
import Login from "@/components/Login";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Cell, Swipe, SwipeItem, Button, ContactCard, Dialog } from "vant";

Vue.use(Dialog);
Vue.use(Cell);
Vue.use(Swipe);
Vue.use(SwipeItem);
Vue.use(Button);
Vue.use(ContactCard);
export default {
    components: {
        Login,
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            user: [],
            ulevellist: [],
            lsklist: [],
            ulevel: 1,
            ulevelname: "",
            cha: 0,
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
                this.getdata(); // 获取用户数据
                this.selectulevel(); // 获取可选级别
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
                        "api/UsersLevelup/Add",
                        {
                            token: _this.$utils.getloc("token"),
                            userid: _this.$utils.getloc("userid"),
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
                        }
                    );
                })
                .catch(() => {});
        },
        getdata: function () {
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
                    _this.user = res.data.data;
                }
            );
        },
        selectulevel: function () {
            var _this = this;
            _this.$request.post(
                "api/Level/GetUlevelList",
                {},
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    res.data.data.forEach((item, index) => {
                        if (index > 0) {
                            _this.ulevellist.push(item.name);
                            _this.lsklist.push(item.amount);
                        }
                    });
                    _this.ulevelname = _this.ulevellist[0];
                },
                true
            );
        },
        onChange(index) {
            this.ulevel = index + 1;
            this.ulevelname = this.ulevellist[index];
            this.cha = this.lsklist[index] - this.user.lsk;
            if (this.cha < 0) {
                this.cha = 0;
            }
        },
    },
};
</script>

<style scoped>
.form {
    position: relative;
}

.form-list {
    position: absolute;
    top: -55px;
    padding: 0;
    width: 100%;
}

.border-radius {
    border-top-left-radius: 30px;
    border-top-right-radius: 30px;
    border-bottom-left-radius: 0;
    border-bottom-right-radius: 0;
    overflow: hidden;
}

/deep/.van-nav-bar .van-icon {
    color: #fff;
}

.ulevelcol .dj {
    position: absolute;
    color: white;
    padding: 50px 0 0 120px;
    font-size: 25px;
}

.ulevelcol img {
    width: 100%;
    border-radius: 10px;
    float: left;
}

.custom-indicator {
    color: white;
    position: absolute;
    right: 14px;
    top: 10px;
    padding: 2px 5px;
    font-size: 12px;
    background: rgba(0, 0, 0, 0.1);
}

.van-swipe {
    background-image: url(../../assets/img/2.png);
    background-size: 100%;
}
</style>
