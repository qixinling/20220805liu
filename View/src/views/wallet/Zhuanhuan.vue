<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>

        <!-- <div class="cardBox" v-if="selectlist.length > 0">
			<div class="cardContent">
				<van-cell :title="item.bname1" :value="item.jine1" v-for="(item, index) in selectlist" v-bind:key="index" />
			</div>
		</div> -->

        <div class="cardBox" v-if="list.length > 0">
            <div class="cardContent">
                <van-cell :title="item.cnameZh" :value="item.jine" v-for="(item, index) in list" v-bind:key="index" />
            </div>
        </div>

        <div class="cardBox">
            <div class="cardBox cardContent">
                <!-- <van-cell v-model="lxname" title="转换类型" value="选择类型" @click="showPopup" is-link />
        <van-popup v-model="show" round position="bottom" :style="{ height: '30%' }">
          <van-picker show-toolbar :columns="slots" value-key="lxname" @confirm="onConfirm" />
        </van-popup> -->
                <van-radio-group v-model="zhid">
                    <van-cell :title="item.bname1 +'转'+ item.bname2" clickable @click="Select(item)" v-for="(item, index) in selectlist" :key="index">
                        <template #right-icon>
                            <van-radio :name="item.id" />
                        </template>
                    </van-cell>
                </van-radio-group>
                <van-field v-model="jine" label="金额" type="number" placeholder="请输入金额" />
            </div>
        </div>

        <div class="cardBox">
            <van-button style="background-color: #f7b226; color: #fff" @click="Wallet_Zhuanhuan" block>转换</van-button>
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
import Vue from "vue";
import { Cell, Field, Popup, Picker, Radio, RadioGroup } from "vant";

Vue.use(Cell);
Vue.use(Field);
Vue.use(Popup);
Vue.use(Picker);
Vue.use(Radio);
Vue.use(RadioGroup);
export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            lx: "",
            zhid: "",
            jine: "",
            show: false,
            wallet: [],
            articleData: "",
            selectlist: [],
            list: [],
        };
    },
    created() {
		if(this.$route.query.cid != ""){
			this.zhid = this.$route.query.cid;
		}
        this.load();
        this.getArticle();
    },
    methods: {
        load: function () {
            if (this.$store.state.LoginState) {
                this.Wallet_GetWallet();
                this.Wallet_ZhuanhuanSelect();
            }
        },
        Wallet_GetWallet: function () {
            let _this = this;
            let _toast = _this.$toast.loading({
                message: "正在加载...",
                duration: 0, // 持续展示 toast
            });
            _this.$request.post(
                "api/Wallets/GetWallet",
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
                    _toast.clear();
                    _this.list = res.data.data;
					//console.log(_this.list);
                }
            );
        },
        Wallet_ZhuanhuanSelect: function () {
            let _this = this;
            _this.$request.post(
                "api/WalletsZhuanhuan/ZhuanhuanSelect",
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
                    let data = res.data.data;
					//console.log(data);
                    // if (data.length > 0) {
                    //     _this.zhid = data[0].id;
                    // }
                    _this.selectlist = data;
                }
            );
        },
        Select(item) {
            this.zhid = item.id;
            this.lx = item.bid1;
        },
        Wallet_Zhuanhuan: function () {
            var _this = this;
            if (_this.jine < 1) {
                _this.$notify({
                    type: "danger",
                    message: "请输入大于0的金额",
                });
                return;
            }

            let _toast = _this.$toast.loading({
                message: "正在提交...",
                duration: 0, // 持续展示 toast
            });
            setTimeout(() => {
                _this.$request.post(
                    "api/WalletsZhuanhuan/Zhuanhuan",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                        uid: _this.$utils.getloc("id"),
                        lx: _this.lx,
                        zhid: _this.zhid,
                        jine: _this.jine,
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
                        _this.Wallet_GetWallet();
                        _this.jine = "";
                    }
                );
            }, 1000);
        },
        getArticle: function () {
            var _this = this;
            _this.$request.post(
                "api/Article/Get",
                {
                    select_id: 6,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.articleData = res.data.data.articlecontent;
                }
            );
        },
    },
};
</script>

<style scoped>
.tishi {
    margin-top: 30px;
    padding-left: 15px;
    padding-right: 15px;
    color: #888;
    line-height: 28px;
    font-size: 12px;
}

.van-button--default {
    border: 0;
}

/deep/ .van-radio__icon--checked .van-icon {
    color: #fff;
    background-color: #F7B226;
    border-color: #F7B226;
}
</style>