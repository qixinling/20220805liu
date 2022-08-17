<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="cardBox" :key="index" v-for="(item, index) in list">
            <div class="cardContent">
                <van-cell icon="card" :title="item.bankname" is-link :to="'/BankUpdate?id=' + item.id" value="修改" :border="false" />
                <van-cell icon="label" title="账号" :value="item.bankcard" :border="false" />
                <van-cell icon="manager" title="姓名" :value="item.username" :border="false" />
                <van-cell icon="phone" title="电话" :value="item.usertel" :border="false" />
                <van-collapse v-model="activeNames">
                    <van-collapse-item title="收款码" :name="index" icon="qr">
                        <div v-if="item.bankimg ==''">暂无收款二维码</div>
                        <img v-else :src="getimg(item.bankimg)" class="ewmimg" />
                    </van-collapse-item>
                </van-collapse>
            </div>
        </div>

        <div class="cardBox">
            <van-button style="background-color:#ff4500;color: #fff;" block to="/BankAdd">添加账户</van-button>
        </div>

        <Login></Login>
    </div>
</template>

<script>
import Login from "@/components/Login";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Cell, CellGroup, Collapse, CollapseItem } from "vant";

Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Collapse);
Vue.use(CollapseItem);
export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            list: [],
            activeNames: [],
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
        getimg: function (img) {
            if (img != "" && img != null) {
                return this.$config.send_url + "Upload/" + img;
            }
        },
        getdata: function () {
            //获取服务端数据
            this.list = [];
            let _this = this;
            _this.$request.post(
                "api/UsersBank/List",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    select_uid: _this.$utils.getloc("id"),
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.list = res.data.data;
                }
            );
        },
    },
};
</script>

<style scoped>
/deep/ .van-cell::after,
.van-hairline--top-bottom::after {
    border: none;
}

.ewmimg {
    width: 100%;
}

/* /deep/.van-cell {
    background-color: #3B3E47;
    color: #fff;
} */

.van-button--default {
    border: 0;
}

.van-cell__value {
    color: #B5B6B8;
}

/* /deep/.van-collapse-item__content {
    background-color: #3B3E47;
} */
</style>
