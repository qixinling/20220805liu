<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="wallet-list" v-if="selectlist.length > 0">
            <!-- <div class="cardContent">
                <van-cell v-for="(item, index) in selectlist" v-bind:key="index" :title="item.bname" :value="item.jine"  />
            </div> -->
            <div class="wallet-item" v-for="(item, index) in selectlist" v-bind:key="index">
                <div>可提现{{item.bname}}: ￥{{item.jine}}</div>
                <div style="color:red" @click="jine = item.jine">全部提现</div>
            </div>
        </div>

        <div v-if="bankdata.length == 0">
            <div>
                <van-cell title="您还没有账户请先添加" @click="addbank" is-link />
            </div>
        </div>

        <div style="padding:6px 10px;font-size:13px">提现金额</div>
        <div>
            <div>
                <van-radio-group v-model="hblx">
                    <van-cell :title="item.bname" clickable @click="hblx = item.bid" v-for="(item, index) in selectlist" :key="index">
                        <template #right-icon>
                            <van-radio :name="item.bid" />
                        </template>
                    </van-cell>
                </van-radio-group>
                <van-field v-model="jine" label="金额" type="number" placeholder="请输入金额" />
            </div>
        </div>

        <div style="padding:6px 10px;font-size:13px">到账账户</div>
        <div v-if="bankdata.length > 0">
            <div>
                <van-cell title="收款账户" v-model="bankname" @click="zhshowPopup" is-link />
                <van-popup v-model="zhshow" round position="bottom" :style="{ height: '30%' }">
                    <van-picker show-toolbar value-key="bankname" @confirm="onConfirmzh" :columns="columns" />
                </van-popup>
                <van-field v-model="username" label="姓名" placeholder="请输入收款人姓名" readonly />
                <van-field v-model="bankcar" label="卡号" placeholder="请输入收款人卡号" readonly />
            </div>
        </div>

        <div class="cardBox">
            <van-row>
                <van-col span="12" class="txjl" @click="tolink">提现记录</van-col>
                <van-col span="12">
                    <div class="fuyanBtn" v-on:click="fuyanshow = !fuyanshow">
                        我要附言
                        <van-icon name="arrow-down" v-if="!fuyanshow" />
                        <van-icon name="arrow-up" v-if="fuyanshow" />
                    </div>
                </van-col>
            </van-row>
        </div>

        <div class="cardBox" v-if="fuyanshow">
            <div class="cardContent">
                <van-field v-model="beizhu" label="附言" placeholder="可不填" />
            </div>
        </div>

        <div class="cardBox">
            <van-button style="background-color: #ff4500; color: #fff" @click="txk=true" block>提现</van-button>
        </div>

        <div class="tishi">
            <div class="article" v-html="articleData"></div>
        </div>

        <van-action-sheet v-model="txk" title="请输入支付密码" :close-on-click-overlay="false">
            <div class="content">
                <van-form @submit="Wallet_Tixian">
                    <van-field v-model="password2" type="password" name="支付密码" label="支付密码" placeholder="支付密码" :rules="[{ required: true, message: '请填写支付密码' }]" />
                    <div style="margin: 16px">
                        <van-button round block style="background-color: #ff4500; color: #fff">确定提现</van-button>
                    </div>
                </van-form>
            </div>
        </van-action-sheet>

        <Login></Login>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Login from "@/components/Login";
import Vue from "vue";
import {
    Cell,
    Field,
    Popup,
    Picker,
    Radio,
    RadioGroup,
    CollapseItem,
    Collapse,
} from "vant";

Vue.use(Cell);
Vue.use(Field);
Vue.use(Popup);
Vue.use(Picker);
Vue.use(Radio);
Vue.use(RadioGroup);
Vue.use(CollapseItem);
Vue.use(Collapse);

export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            value: "",
            txk: false,
            bankdata: [],
            bankname: "请选择账户",
            bankid: "",
            username: "",
            bankcar: "",
            columns: [
                {
                    flex: 1,
                    values: [
                        {
                            id: 0,
                            bankname: "",
                        },
                    ],
                    className: "slot1",
                    textAlign: "center",
                    defaultIndex: 0,
                },
            ],
            zhshow: false,
            fuyanshow: false,
            hblx: "",
            jine: "",
            password2: "",
            beizhu: "",
            activeNames: [],
            articleData: "",
            wallet: [],
            selectlist: [],
        };
    },
    created() {
		if(this.$route.query.cid != ""){
			this.hblx = this.$route.query.cid;
		}
        this.load();
        this.getArticle();
    },
    methods: {
        load: function () {
            if (this.$store.state.LoginState) {
                this.Wallet_TixianSelect();
                this.getbank();
            }
        },
        tolink: function () {
            this.$router.push({
                name: "Tixianrecord",
            });
        },
        Wallet_TixianSelect: function () {
            let _this = this;
            _this.$request.post(
                "api/WalletsTixian/TixianSelect",
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
                    if (data.length > 0) {
                        _this.hblx = data[0].bid;
                    }
                    _this.selectlist = data;
                }
            );
        },
        addbank: function () {
            this.$router.push({
                name: "Bank",
            });
        },
        zhshowPopup: function () {
            this.zhshow = true;
        },
        onConfirmzh(e) {
            this.bankid = e[0].id;
            this.bankname = e[0].bankname;
            this.username = e[0].username;
            this.bankcar = e[0].bankcard;
            this.zhshow = false;
        },
        getbank: function () {
            var _this = this;
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
                    _this.bankdata = res.data.data;
                    if (_this.bankdata.length > 0) {
                        //清空默认值
                        _this.columns[0].values = [];
                        //添加响应数据
                        _this.bankdata.forEach((item) => {
                            _this.columns[0].values.push(item);
                        });
                    }
                }
            );
        },
        Wallet_Tixian: function () {
            var _this = this;
            if (_this.$store.state.LoginState) {
                if (_this.bankid == "") {
                    _this.$notify({
                        type: "danger",
                        message: "请选择账户",
                    });
                    return;
                }
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
                        "api/WalletsTixian/Tixian",
                        {
                            token: _this.$utils.getloc("token"),
                            userid: _this.$utils.getloc("userid"),
                            uid: _this.$utils.getloc("id"),
                            cid: _this.hblx,
                            jine: _this.jine,
                            beizhu: _this.beizhu,
                            bid: _this.bankid,
                            password2: _this.password2,
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
                            _this.Wallet_TixianSelect();

                            _this.txk = false;
                            _this.password2 = "";
                            _this.jine = "";
                            _this.beizhu = "";
                        }
                    );
                }, 1000);
            }
        },
        getArticle: function () {
            var _this = this;
            _this.$request.post(
                "api/Article/Get",
                {
                    select_id: 4,
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
.fuyanBtn {
    padding: 10px;
    text-align: right;
    color: #1989fa;
    font-size: 12px;
}

.tishi {
    margin-top: 30px;
    padding-left: 15px;
    padding-right: 15px;
    color: #ccc;
    line-height: 28px;
    font-size: 12px;
}

.wrapper {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100%;
}

.block {
    width: 120px;
    height: 120px;
    background-color: #fff;
}

.txjl {
    padding-top: 10px;
    padding-left: 10px;
    font-size: 12px;
    color: #1989fa;
}

.van-button--default {
    border: 0;
}

/deep/ .van-radio__icon--checked .van-icon {
    color: #fff;
    background-color: #ff4500;
    border-color: #ff4500;
}

.wallet-list {
    background-color: #fff;
    border-bottom: 1px solid #ccc;
}

.wallet-item {
    padding: 10px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
}
</style>