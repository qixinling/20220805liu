<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>

        <van-search v-model="value" placeholder="请输入搜索关键词" @search="onSearch" />
        <div v-if="goodslist.length == 0">
            <van-empty description="暂无商品" />
        </div>
        <div class="cardBox" v-for="(item, index) in goodslist" :key="index">
            <div class="cardContent">
                <van-row>
                    <van-col span="24" class="goodsimg">
                        <router-link :to="{ name: 'Goods', query: { id: item.id } }">
                            <img :src="getimg(item.goodsimg)" style="width:100%;" />
                        </router-link>
                    </van-col>
                </van-row>
                <van-row>
                    <van-col span="24" class="goodsname">{{ item.goodsname }}</van-col>
                </van-row>

                <van-row>
                    <van-col span="24" class="jianjie">{{ item.prompt }}</van-col>
                </van-row>

                <van-row>
                    <van-col span="12" class="goodsprice">
                        ¥{{ item.sjine }}
                        <span v-if="item.sjine < item.yjine" style="font-size: 12px;color: #888;">
                            <s>¥{{ item.yjine }}</s>
                        </span>
                    </van-col>
                    <van-col span="12" class="kucun">库存: {{ item.stock }}</van-col>
                </van-row>
                <van-row>
                    <van-col span="12" class="count">
                        <van-stepper v-model="item.num" min="1" max="99" />
                    </van-col>
                    <van-col span="12" class="addcar">
                        <van-button type="warning" icon="shopping-cart" @click="addCar(item.id)">加入购物车</van-button>
                    </van-col>
                </van-row>
            </div>
        </div>
        <GoodsAddCar ref="mychild"></GoodsAddCar>
    </div>
</template>

<script>
import GoodsAddCar from "@/components/GoodsAddCar";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Button, Stepper, Search, Empty } from "vant";
Vue.use(Button);
Vue.use(Stepper);
Vue.use(Search);
Vue.use(Empty);
export default {
    components: {
        HeadBar,
        GoodsAddCar,
    },
    data() {
        return {
            title: document.title,
            glist: [],
            goodslist: [],
            goodsname: "",
            xlid: 0,
            value: "",
        };
    },
    created() {
        if (this.$route.query.goodsname != "") {
            this.goodsname = this.$route.query.goodsname;
            this.value = this.$route.query.goodsname;
            this.getdata(1); // 查询商品
        }
        if (this.$route.query.id != "") {
            this.xlid = this.$route.query.id;
            this.getdata(2); // 查询商品
        }
    },
    methods: {
        onSearch: function (e) {
            this.goodsname = e;
            this.getdata(1); // 查询商品
        },

        getimg: function (img) {
            if (img != "" && img != null) {
                return this.$config.send_url + "Upload/" + img;
            }
        },

        getdata: function (lx) {
            let _this = this;
            _this.goodslist = [];
            _this.$request.post(
                "api/ShopGoods/List",
                {
                    userid: _this.$utils.getloc("userid"),
                    goodstype: 1,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    var goodslist = res.data.data;
                    goodslist.forEach((item) => {
                        item.goodsimg = item.goodsimg.split(",")[0];
                        if (lx == 1) {
                            // 根据商品名称
                            if (item.goodsname.indexOf(_this.goodsname) != -1) {
                                item["num"] = 1;
                                _this.goodslist.push(item);
                            }
                        } else {
                            // 根据小类id
                            if (item.xlid == _this.xlid) {
                                item["num"] = 1;
                                _this.goodslist.push(item);
                            }
                        }
                    });
                }
            );
        },

        addCar: function (id) {
            // 添加商品
            this.goodslist.forEach((item) => {
                if (item.id == id) {
                    this.$refs.mychild.goodsAddCar(item);
                    item.num = 1;
                }
            });
        },
    },
};
</script>

<style scoped>
.goodsimg {
    text-align: center;
}

.goodsimg img {
    border-radius: 5px;
}

.goodsname {
    font-size: 20px;
    font-weight: 700;
    padding-left: 5px;
}

.jianjie {
    font-size: 14px;
    color: #888;
    padding: 5px 0 5px 5px;
}

.kucun {
    text-align: right;
    font-size: 14px;
    color: #888;
    padding-right: 10px;
}

.goodsprice {
    color: red;
    font-size: 18px;
    padding-left: 5px;
}

.count {
    padding: 10px 0 0 5px;
}

.shouchu {
    margin-top: 15px;
    font-size: 14px;
    color: #888;
}

.addcar {
    padding: 8px 10px 5px 0;
    text-align: right;
}

.addcar button {
    height: 30px;
}
</style>
