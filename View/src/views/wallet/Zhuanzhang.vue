<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="cardBox" v-if="selectlist.length > 0">
            <div class="cardContent">
                <van-cell :title="item.coinname+'余额'" :value="item.jine" v-for="(item, index) in selectlist" v-bind:key="index" />
            </div>
        </div>

        <div class="cardBox">
            <div class="cardBox cardContent">
                <van-field v-model="suserid" label="收款方" placeholder="请输入收款方手机号" />
                <!-- <van-field v-model="susername" label="姓名" readonly /> -->
            </div>
        </div>

        <div class="cardBox">
            <div class="cardBox cardContent">
                <van-radio-group v-model="hblx">
                    <van-cell :title="item.coinname" clickable @click="hblx = item.bid" v-for="(item, index) in selectlist" :key="index">
                        <template #right-icon>
                            <van-radio :name="item.bid" />
                        </template>
                    </van-cell>
                </van-radio-group>
                <van-field v-model="jine" label="金额" type="number" placeholder="请输入金额" />
            </div>
        </div>

        <div class="cardBox">
            <van-row>
                <van-col span="12" class="zzjl" @click="tolink">转账记录</van-col>
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
            <van-button style="background-color: #f7b226; color: #fff" @click="zzk=true" block>转账</van-button>
        </div>

        <div class="tishi">
            <div class="article" v-html="articleData"></div>
           <!-- <div class="article" v-for="(item, index) of selectlist" :key="index">
                <div v-if="item.shouxu > 0">
                    {{ item.coinname }} 手续费 {{ item.shouxu }}%
                </div>
                <div v-else>{{ item.coinname }} 无手续费</div>
            </div> -->
        </div>

        <van-action-sheet v-model="zzk" title="请输入支付密码" :close-on-click-overlay="false">
            <div class="content">
                <van-form @submit="Wallet_Zhuanzhang">
                    <van-field v-model="password2" name="支付密码" type="password" label="支付密码" placeholder="支付密码"  />
                    <div style="margin: 16px">
                        <van-button round block type="info">确认转账</van-button>
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
    Button,
    Radio,
    RadioGroup,
    CollapseItem,
    Collapse,
} from "vant";

Vue.use(Cell);
Vue.use(Field);
Vue.use(Button);
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
            password2: "",
            zzk: false,
            suserid: "",
            susername: "",
            hblx: "",
            jine: "",
            beizhu: "",
            fuyanshow: false,
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
        this.getArticle();
        this.load();
    },
    methods: {
        load: function () {
            if (this.$store.state.LoginState) {
                this.Wallet_ZhuanzhangSelect();
            }
        },
        tolink: function () {
            this.$router.push({
                name: "Zhuanzhangrecord",
            });
        },
        Wallet_ZhuanzhangSelect: function () {
            let _this = this;
            _this.$request.post(
                "api/WalletsZhuanzhang/ZhuanzhangSelect",
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
                    _this.selectlist = res.data.data;
                    let data = res.data.data;
                    // if (data.length > 0) {
                    //     _this.hblx = data[0].bid;
                    // }
                    _this.selectlist = data;
                    console.log();
                }
            );
        },
        Wallet_QueryUname: function() {
			var _this = this;
			_this.$request.post(
				"api/Wallets/QueryUname", {
					token: _this.$utils.getloc("token"),
					userid: _this.$utils.getloc("userid"),
					suserid: _this.suserid,
				},
				(res) => {
					_this.susername = res.data.data;
				}, false
			);
		},
        Wallet_Zhuanzhang: function () {
            var _this = this;
            if (_this.suserid == "") {
                _this.$notify({
                    type: "danger",
                    message: "请输入收款人用户名",
                });
            } else if (_this.jine < 1) {
                _this.$notify({
                    type: "danger",
                    message: "请输入大于0的金额",
                });
            } else {
                let _toast = this.$toast.loading({
                    message: "正在提交...",
                    duration: 0, // 持续展示 toast
                });
                setTimeout(() => {
                    _this.$request.post(
                        "api/WalletsZhuanzhang/Zhuanzhang",
                        {
                            token: _this.$utils.getloc("token"),
                            userid: _this.$utils.getloc("userid"),
                            uid: _this.$utils.getloc("id"),
                            cid: _this.hblx,
                            jine: _this.jine,
                            beizhu: _this.beizhu,
                            password2: _this.password2,
                            suserid: _this.suserid,
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
                            _this.Wallet_ZhuanzhangSelect();

                            _this.zzk = false;
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
                    select_id: 5,
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
    color: #ccc;
    line-height: 28px;
    font-size: 12px;
}

.fuyanBtn {
    padding: 10px;
    text-align: right;
    color: #1989fa;
    font-size: 12px;
}

.zzjl {
    padding-top: 10px;
    padding-left: 10px;
    color: #1989fa;
    font-size: 12px;
}

/deep/ .van-radio__icon--checked .van-icon {
    color: #fff;
    background-color: #F7B226;
    border-color: #F7B226;
}

.van-button--default {
    border: 0;
}
</style>