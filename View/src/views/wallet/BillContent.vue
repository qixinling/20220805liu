<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="cardBox">
            <div class="cardContent">
                <van-row style="padding-top: 20px">
                    <van-col span="11"></van-col>
                    <van-col span="13">
                        <div class="operationtype ot0" v-if="blist.operationtype == 0">
                            收益
                        </div>
                        <div class="operationtype ot1" v-if="blist.operationtype == 1">
                            购物
                        </div>
                        <div class="operationtype ot2" v-if="blist.operationtype == 2">
                            充值
                        </div>
                        <div class="operationtype ot3" v-if="blist.operationtype == 3">
                            提现
                        </div>
                        <div class="operationtype ot4" v-if="blist.operationtype == 4">
                            转账
                        </div>
                        <div class="operationtype ot5" v-if="blist.operationtype == 5">
                            转换
                        </div>
                        <div class="operationtype ot6" v-if="blist.operationtype == 6">
                            增减
                        </div>
                        <div class="operationtype ot7" v-if="blist.operationtype == 7">
                            升级
                        </div>
                        <div class="operationtype ot8" v-if="blist.operationtype == 8">
                            投资
                        </div>
                        <div class="operationtype ot8" v-if="blist.operationtype == 9">
                            激活
                        </div>
                    </van-col>
                </van-row>
                <van-row>
                    <van-col span="24" class="dpname">{{ blist.hblxname }}</van-col>
                </van-row>
                <van-row>
                    <van-col span="24" class="jine">{{ blist.jine }}</van-col>
                </van-row>

                <div class="btntext">
                    <van-row v-if="blist.operationtype == 2 && blist.operationtype == 3">
                        <van-col span="8" class="lefttext">当前状态</van-col>
                        <van-col span="16" class="righttext" v-if="blist.ispay == 0">待审核</van-col>
                        <van-col span="16" class="righttext" v-if="blist.ispay == 1">已审核</van-col>
                        <van-col span="16" class="righttext" v-if="blist.ispay == 2">已撤销</van-col>
                    </van-row>
                    <van-row v-if="blist.bdate != ''">
                        <van-col span="8" class="lefttext">时间</van-col>
                        <van-col span="16" class="righttext">{{
              $utils.timestampFormat(blist.bdate)
            }}</van-col>
                    </van-row>

                    <van-row v-if="blist.operationtype == 0">
                        <van-col span="8" class="lefttext">来源</van-col>
                        <van-col span="16" class="righttext">{{ blist.yuserid }}</van-col>
                    </van-row>
                    <van-row v-if="blist.operationtype == 0">
                        <van-col span="8" class="lefttext">详情</van-col>
                        <van-col span="16" class="righttext">{{ blist.bsbz }}</van-col>
                    </van-row>

                    <van-row v-if="blist.bz != ''">
                        <van-col span="8" class="lefttext">备注</van-col>
                        <van-col span="16" class="righttext">{{ blist.bz }}</van-col>
                    </van-row>
                    <van-row v-if="blist.operationtype == 3">
                        <van-col span="8" class="lefttext">提现账户</van-col>
                        <van-col span="16" class="righttext">{{
              blist.txbankname
            }}</van-col>
                        <van-col span="8" class="lefttext">提现账号</van-col>
                        <van-col span="16" class="righttext">{{
              blist.txbankcard
            }}</van-col>
                        <van-col span="8" class="lefttext">手续</van-col>
                        <van-col span="16" class="righttext">{{ blist.txshouxu }}</van-col>
                    </van-row>
                    <van-row v-if="blist.operationtype == 4">
                        <van-col span="8" class="lefttext">转款人账号</van-col>
                        <van-col span="16" class="righttext">{{ blist.zkuserid }}</van-col>
                        <van-col span="8" class="lefttext">转款人姓名</van-col>
                        <van-col span="16" class="righttext">{{
              blist.zkusername
            }}</van-col>
                        <van-col span="8" class="lefttext">收款人账号</van-col>
                        <van-col span="16" class="righttext">{{ blist.skuserid }}</van-col>
                        <van-col span="8" class="lefttext">收款人姓名</van-col>
                        <van-col span="16" class="righttext">{{
              blist.skusername
            }}</van-col>
                    </van-row>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { GoodsAction } from "vant";
Vue.use(GoodsAction);

export default {
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            bid: "",
            blist: [],
            tx: "",
        };
    },
    created() {
        this.bid = this.$route.query.id;
        this.load();
    },
    methods: {
        load: function () {
            if (this.$store.state.LoginState) {
                this.get();
            }
        },
        get: function () {
            let _this = this;
            _this.$request.post(
                "api/Bill/Get",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    bid: _this.bid,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.blist = res.data.data;
                }
            );
        },
    },
};
</script>

<style scoped>
.operationtype {
    width: 40px;
    height: 40px;
    border-radius: 40px;
    text-align: center;
    display: table-cell;
    vertical-align: middle;
    font-weight: 500;
}

.ot0 {
    background: palevioletred;
    color: #ffffff;
}

.ot1 {
    background: #37a2da;
    color: #ffffff;
}

.ot2 {
    background: #ff9f7f;
    color: #ffffff;
}

.ot3 {
    background: #67e0e3;
    color: #ffffff;
}

.ot4 {
    background: #ffdb5c;
    color: #ffffff;
}

.ot5 {
    background: #32c5e9;
    color: #ffffff;
}

.ot6 {
    background: #37a2da;
    color: #ffffff;
}

.ot7 {
    background: #a7a2da;
    color: #ffffff;
}

.ot8 {
    background: #bbcda0;
    color: #ffffff;
}

.dpname {
    text-align: center;
    padding: 15px 0 20px 0;
    font-size: 20px;
}

.jine {
    text-align: center;
    font-size: 20px;
    font-weight: 700;
}

.lefttext {
    padding: 10px 0 0 30px;
    font-size: 14px;
    color: #888;
}

.righttext {
    padding: 10px 0 0 0;
    font-size: 14px;
    text-align: left;
}

.btntext {
    border-top: 1px solid #f4f5f5;
    margin-top: 50px;
    padding: 10px 0;
}
</style>
