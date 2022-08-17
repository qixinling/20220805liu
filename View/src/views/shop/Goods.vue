<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <van-swipe class="my-swipe">
            <van-swipe-item v-for="(item, index) in imglist" :key="index">
                <img :src="getimg(item)" />
            </van-swipe-item>
        </van-swipe>

        <div style="background-color: #fff;">
            <div style="padding: 10px;">
                <van-row>
                    <van-col span="24" class="goodsname">{{ goods.goodsname }}</van-col>
                </van-row>
                <van-row>
                    <van-col span="24" class="sjprompt van-multi-ellipsis--l2">{{ goods.prompt }}</van-col>
                </van-row>
                <van-row>
                    <van-col span="12" class="goodsprice">
                        ¥{{ goods.sjine }}
                        <span v-if="goods.sjine < goods.yjine" style="font-size: 12px;color: #888;">
                            <s>¥{{ goods.yjine }}</s>
                        </span>
                    </van-col>
                    <van-col span="12" class="count">
                        <van-stepper v-model="goods.num" />
                    </van-col>
                </van-row>
                <van-row>
                    <van-col span="12" class="kucun">库存: {{ goods.stock }}</van-col>
                    <van-col span="12" class="sales">销售: {{ goods.sales }}</van-col>
                </van-row>
            </div>
        </div>

        <div>
            <van-divider>商品详情</van-divider>
        </div>
        <van-row>
            <van-col span="24" class="xq">
                <div v-html="goods.goodscontent"></div>
            </van-col>
        </van-row>

        <van-goods-action>
            <van-goods-action-icon icon="cart-o" text="购物车" :badge="goodsCount" :to="'/Shopcar?gid=' + gid" />
            <van-goods-action-button type="warning" text="加入购物车" @click="addcar(1)" />
            <van-goods-action-button type="danger" text="立即购买" @click="addcar(2)" />
        </van-goods-action>
        <GoodsAddCar ref="mychild"></GoodsAddCar>
    </div>
</template>

<script>
import GoodsAddCar from "@/components/GoodsAddCar";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import {
    GoodsAction,
    GoodsActionIcon,
    GoodsActionButton,
    Stepper,
    Swipe,
    SwipeItem,
    Divider,
} from "vant";
Vue.use(GoodsAction);
Vue.use(GoodsActionButton);
Vue.use(GoodsActionIcon);
Vue.use(Stepper);
Vue.use(Swipe);
Vue.use(SwipeItem);
Vue.use(Divider);

export default {
    components: {
        HeadBar,
        GoodsAddCar,
    },
    data() {
        return {
            title: document.title,
            value: 1,
            gid: 0,
            goods: [],
            imglist: [],
            aCar: true,
            goodsCount: "",
        };
    },
    created() {
        if (this.$route.query.id != "") {
            this.gid = this.$route.query.id;
            this.getdata(); // 查询商品详情
            this.getcarcount(); // 查询购物车商品数量
        }
    },
    methods: {
        getdata: function () {
            // 查询商品详情
            let _this = this;
            _this.$request.post(
                "api/ShopGoods/Get",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    id: _this.gid,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.goods = res.data.data[0];
                    //_this.goods.goodscontent = _this.goods.goodscontent.replace(/src="/g, 'src="' + _this.$config.send_url + "Upload/");
                    _this.goods["num"] = 1;
                    _this.imglist = _this.goods.goodsimg.split(",");
                    var arrEntities = {
                        lt: "<",
                        gt: ">",
                        nbsp: " ",
                        amp: "&",
                        quot: '"',
                    };
                    _this.goods.goodscontent = _this.goods.goodscontent.replace(
                        /&(lt|gt|nbsp|amp|quot);/gi,
                        function (all, t) {
                            return arrEntities[t];
                        }
                    );
                }
            );
        },

        getimg: function (img) {
            if (img != "" && img != null) {
                return this.$config.send_url + "Upload/" + img;
            }
        },

        getcarcount: function () {
            // 查询购物车商品数量
            if (
                JSON.parse(localStorage.getItem("goodslist")) != null &&
                JSON.parse(localStorage.getItem("goodslist")).length > 0
            ) {
                this.goodsCount = JSON.parse(
                    localStorage.getItem("goodslist")
                ).length;
            }
        },

        addcar: function (e) {
            // 添加商品
            if (e == 2) {
                this.$refs.mychild.goodsAddCar(this.goods);
                this.$router.push({
                    name: "ShopCar",
                    query: {
                        gid: this.gid,
                    },
                });
            } else {
                this.$refs.mychild.goodsAddCar(this.goods);
                this.getcarcount(); // 查询购物车商品数量
            }
        },
    },
};
</script>

<style scoped>
.goodsname {
    font-size: 20px;
    padding-left: 10px;
    font-weight: 700;
}

.xq {
    padding: 0 10px 40px 10px;
}

/deep/ .xq img {
    width: 100%;
}

.goodsprice {
    padding: 5px 0 0 10px;
    color: red;
}

.sjprompt {
    text-align: left;
    padding: 5px 0 0 10px;
    font-size: 14px;
    color: #888;
}

.kucun {
    text-align: left;
    padding: 5px 10px 0 0;
    font-size: 14px;
    color: #888;
}

.sales {
    text-align: right;
    padding: 5px 10px 0 0;
    font-size: 14px;
    color: #888;
}

.count {
    padding: 5px 10px 10px 0;
    text-align: right;
}

.contenttext {
    text-align: center;
    padding-top: 15px;
}

.van-swipe-item img {
    width: 100%;
}

/deep/.van-stepper__input {
    margin: initial;
}

/deep/ .van-divider {
    color: #313131;
    border-color: #dad9d9;
}

/* /deep/.van-goods-action-button--danger {
    background: linear-gradient(to right, #259a66, #18c37c);
} */
</style>
