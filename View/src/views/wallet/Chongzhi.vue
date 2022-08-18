<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="cardBox" v-if="selectlist.length > 0">
            <div class="cardContent">
                <van-cell :title="item.bname" :value="item.jine" v-for="(item, index) in selectlist" v-bind:key="index" />
            </div>
        </div>

        <div class="cardBox">
            <div class="cardContent">
                <van-radio-group v-model="hblx">
                    <van-cell :title="item.bname" clickable @click="hblx = item.bid" v-for="(item, index) in selectlist" :key="index">
                        <template #right-icon>
                            <van-radio :name="item.bid" />
                        </template>
                    </van-cell>
                </van-radio-group>
                <van-field v-model="jine" type="number" label="金额" placeholder="请输入金额" />
            </div>
        </div>

        <div class="cardBox">
            <div class="cardContent">
                <UploadPictures @getUploadPictures="getUploadPictures" title="支付凭证"/>
            </div>
        </div>

        <div class="cardBox">
            <van-row>
                <van-col span="12" class="czjl" @click="tolink">充值记录</van-col>
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
            <van-button style="background-color: #f7b226; color: #fff" @click="Wallet_Chongzhi" block>申请充值</van-button>
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
import UploadPictures from "@/components/UploadPictures";
import Vue from "vue";
import { Cell, Field, Radio, RadioGroup, Uploader } from "vant";

Vue.use(Uploader);
Vue.use(Cell);
Vue.use(Field);
Vue.use(Radio);
Vue.use(RadioGroup);
export default {
    components: {
        HeadBar,
        Login,
        UploadPictures
    },
    data() {
        return {
            title: document.title,
            jine: "",
            beizhu: "",
            imgname: "",
            czimg: "",
            wallet: [],
            hblx: "",
            fuyanshow: false,
            fileList: [],
            articleData: "",
            selectlist: [],
            ewmimg: ""
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
                this.Wallet_ChongzhiSelect();
            }
        },
        getUploadPictures(img) {
            this.ewmimg = img;
        },
        tolink: function () {
            this.$router.push({
                name: "Chongzhirecord",
            });
        },
        Wallet_ChongzhiSelect: function () {
            let _this = this;
            let _toast = _this.$toast.loading({
                message: "正在加载...",
                duration: 0, // 持续展示 toast
            });
            _this.$request.post(
                "api/WalletsChongzhi/WalletsChongzhiSelect",
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
        Wallet_Chongzhi: function () {
            var _this = this;
            if (_this.$store.state.LoginState) {
                if (_this.hblx == "") {
                    _this.$notify({
                        type: "danger",
                        message: "请选择货币类型",
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
                if (_this.ewmimg == "") {
                    _this.$notify({
                        type: "danger",
                        message: "请上传支付凭证",
                    });
                    return;
                }
                let _toast = this.$toast.loading({
                    message: "正在提交...",
                    duration: 0, // 持续展示 toast
                });

                setTimeout(() => {
                    _this.$request.post(
                        "api/WalletsChongzhi/WalletsChongzhi",
                        {
                            token: _this.$utils.getloc("token"),
                            userid: _this.$utils.getloc("userid"),
                            uid: _this.$utils.getloc("id"),
                            cid: _this.hblx,
                            jine: _this.jine,
                            beizhu: _this.beizhu,
                            czimg: this.ewmimg
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
                            _this.jine = "";
                            _this.beizhu = "";
                            _this.fileList = [];
                        }
                    );
                    _this.jine = "";
                    _this.beizhu = "";
                }, 1000);
            }
        },
        getArticle: function () {
            var _this = this;
            _this.$request.post(
                "api/Article/Get",
                {
                    select_id: 3,
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

/deep/ img {
    width: 100%;
}

.czjl {
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
    background-color: #F7B226;
    border-color: #F7B226;
}
</style>