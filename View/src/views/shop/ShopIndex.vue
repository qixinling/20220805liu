<template>
    <div>
        <div class="shop-header" style="width: 100%;">
            <van-row>
                <van-col span="22">
                    <van-search class="search" v-model="value" placeholder="请输入搜索关键词" background="transparent" shape="round" @search="onSearch" />
                </van-col>
                <van-col span="2" style="padding-top: 12px;">
                    <router-link to="ShopCar">
                        <van-icon name="shopping-cart-o" size="28" color="#F7B226"/>
                    </router-link>
                </van-col>
            </van-row>
            <!-- <van-search class="search" v-model="value" placeholder="请输入搜索关键词" background="transparent" shape="round" @search="onSearch" /> -->
            <!-- <van-swipe class="my-swipe" :autoplay="3000" indicator-color="white">
                <van-swipe-item v-for="(item, index) in slidelist" :key="index">
                    <div @click="slideTo(item.gid, item.lx, item.url)">
                        <img :src="getimg(item.img)" />
                    </div>
                </van-swipe-item>
            </van-swipe> -->
        </div>
        <!-- <div class="cardBox" v-if="xllist.length>0">
            <div class="cardContent">
                <van-cell icon="label" title="分类" value="全部" :is-link="true" to="ShopClass" size="small" />
                <van-grid :border="false" :column-num="5" class="classify">
                    <van-grid-item v-for="(item, index) in xllist" :key="index" :to="'goodssearch?id=' + item.id">
                        <img :src="getimg(item.xiaoleiimg)" class="classifyimg" style="box-shadow: 2px 3px 2px #888888;" />
                        <div style="font-size: 12px;margin-top: 10px;">{{ item.xiaoleiname }}</div>
                    </van-grid-item>
                </van-grid>
            </div>
        </div> -->

        <div style="padding:0 5px 10px">
            <van-row>
                <van-col style="padding:5px" span="12" v-for="(item, index) in goodslist" :key="index">
                    <div class="goods-card" @click="toDetails(item.id)">
                        <div class="goodsimg">
                            <img :src="getimg(item.goodsimg)" style="width:100%" />
                        </div>
                        <div class="goods-content">
                            <div class="goodsname">{{ item.goodsname }}</div>
                            <div class="goodsprice">
                                <span>￥{{ item.sjine }}</span>
                                <span v-if="item.sjine < item.yjine" style="font-size: 12px;color: #888;">
                                    <s>¥{{ item.yjine }}</s>
                                </span>
                            </div>
                            <div class="goodsother">
                                <div>库存：{{ item.stock }}</div>
                                <div>销量：{{ item.sales }}</div>
                            </div>
                        </div>
                    </div>
                </van-col>
            </van-row>
        </div>

        <GoodsAddCar ref="mychild"></GoodsAddCar>
        <BottomBar :key="bottombar"></BottomBar>
    </div>
</template>

<script>
import GoodsAddCar from "@/components/GoodsAddCar";
import BottomBar from "@/components/BottomBar";
import Vue from "vue";
import { Stepper, Swipe, SwipeItem, Search, Grid, GridItem, Cell } from "vant";
Vue.use(Stepper);
Vue.use(Swipe);
Vue.use(SwipeItem);
Vue.use(Search);
Vue.use(Grid);
Vue.use(GridItem);
Vue.use(Cell);
export default {
    components: {
        BottomBar,
        GoodsAddCar,
    },
    data() {
        return {
            title: document.title,
            slidelist: [],
            shopimg: [],
            xllist: [],
            goodslist: [],
            value: "",
            bottombar: false,
        };
    },
    created() {
        if (this.$utils.GetQueryString("rr")) {
            this.$utils.setloc("recode", this.$utils.GetQueryString("rr"));
        }
        this.getdata();
        this.getslide();
        this.Shop_Img();
        this.getclassify();
    },
    methods: {
        toDetails(id) {
            this.$router.push({
                path: "/Goods",
                query: {
                    id,
                },
            });
        },
        slideTo: function (id, lx, url) {
            if (lx == 2) {
                this.$router.push({
                    name: "Goods",
                    query: {
                        id: id,
                    },
                });
            } else if (lx == 3) {
                this.$router.push({
                    name: "NewsContent",
                    query: {
                        id: id,
                    },
                });
            } else if (lx == 4) {
                window.open(url);
            } else if (lx == 5) {
                this.$router.push({
                    name: "GoodsSearch",
                    query: {
                        id: id,
                    },
                });
            }
        },
        toGoods: function (gid, bdlx) {
            // 点击商城图片跳转对应商品或者新闻
            if (gid == 0) {
                return;
            }
            if (bdlx == 1) {
                this.$router.push({
                    name: "Goods",
                    query: {
                        id: gid,
                    },
                });
            } else {
                this.$router.push({
                    name: "NewsContent",
                    query: {
                        id: gid,
                    },
                });
            }
        },
        onSearch: function (e) {
            //console.log(e);
            this.value = e;
            // 搜索
            this.$router.push({
                name: "GoodsSearch",
                query: {
                    goodsname: this.value,
                },
            });
        },
        getimg: function (img) {
            if (img != "") {
				//console.log(img)
                return this.$config.send_url + "Upload/" + img;
            }
        },
        addCar: function (id) {
            // 添加商品
            this.goodslist.forEach((item) => {
                if (item.id == id) {
                    this.$refs.mychild.goodsAddCar(item);
                    item.num = 1;
                    this.bottombar = !this.bottombar;
                }
            });
        },
        getslide: function () {
            let _this = this;
            this.$request.post(
                "Api/Slide/List",
                {
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
                    _this.slidelist = res.data.data;
                }
            );
        },
        Shop_Img: function () {
            let _this = this;
            this.$request.post("Api/Shop/Img", {}, (res) => {
                if (res.data.code == 0) {
                    _this.$dialog.alert({
                        title: "提示",
                        message: res.data.msg,
                    });
                    return;
                }
                var list = res.data.data;
                list.forEach((item) => {
                    item.Imgs = item.img.split(",");
                    item.Gids = item.gid.split(",");
                    item.Bdlxs = item.bdlx.split(",");
                });
                _this.shopimg = list;
				//console.log(_this.shopimg)
            });
        },
         getclassify: function () {
            let _this = this;
            this.$request.post("Api/ShopGoods/Daxiaolei_List", {}, (res) => {
                if (res.data.code == 0) {
                    _this.$dialog.alert({
                        title: "提示",
                        message: res.data.msg,
                    });
                    return;
                }
                var Dl = res.data.data;
                var xllist = [];
                Dl.forEach((dItem) => {
                    if (dItem.dbShopGoodsSortChild.length > 0) {
                        dItem.dbShopGoodsSortChild.forEach((xItem) => {
                            xllist.push(xItem);
                        });
                    }
                });
                _this.xllist = xllist;
            });
        },
        getdata: function () {
            let _this = this;
            this.$request.post(
                "Api/ShopGoods/List",
                {
                    token: _this.$utils.getloc("token"),
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
                    var glist = [];
                    goodslist.forEach((item) => {
                        if (item.ishome == 1) {
                            item["num"] = 1;
                            item.goodsimg = item.goodsimg.split(",")[0];
                            glist.push(item);
                        }
                    });
                    _this.goodslist = glist;
                }
            );
        },
    },
};
</script>

<style scoped>
.shop-header {
    background-color: #fff;
}

.search {
    /* position: absolute;
    left: 0;
    right: 0;
    top: 0; */
    margin-left: auto;
    margin-right: auto;
    max-width: 600px;
}

.my-swipe {
    text-align: center;
}

.van-swipe-item img {
    width: 100%;
    /* border-radius: 10px; */
}

.goodsimg {
    text-align: center;
}

.goodsimg img {
    border-radius: 5px;
}

.goodsname {
    /* height: 38px; */
    margin-bottom: 4px;
    font-size: 13px;
    font-weight: 600;
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: 1;
    overflow: hidden;
    color: #333;
}

.jianjie {
    font-size: 14px;
    color: #B5B6B8;
    padding: 5px 0 5px 5px;
}

.goodsprice {
    margin-bottom: 4px;
}

.kucun {
    padding-top: 12px;
    font-size: 12px;
    color: #B5B6B8;
}

.count {
    padding: 10px 0 0 5px;
}

.shouchu {
    margin-top: 15px;
    font-size: 12px;
    color: #888;
}

.addcar {
    padding: 8px 10px 5px 0;
    text-align: right;
}

.addcar button {
    height: 30px;
    background-color: #F4C40B;
}

.classify {
    font-size: 14px;
    /* box-shadow: 2px 2px 5px #ccc; */
    background: #3B3E47;
}

.classifyimg {
    width: 40px;
    height: 40px;
    border-radius: 50px;
}

.allimg {
    padding: 0 10px;
}

.allimg img {
    width: 100%;
    border-radius: 10px;
}

.leftimg {
    height: 100%;
    padding: 5px 0 0 10px;
}

.leftimg img {
    width: 100%;
    border-radius: 10px;
}

.upimg,
.btimg {
    padding: 5px 10px 0 10px;
}

.upimg img,
.btimg img {
    width: 100%;
    border-radius: 10px;
}

.icon-classify {
    font-size: 27px;
    padding: 11px 0 6px 10px;
    background: white;
}

/deep/.van-stepper__input {
    margin: initial;
}

/deep/.van-grid-item__content {
    color: #fff;
    padding: 12px 8px;
    background-color: #3B3E47;
}

/* /deep/.van-search__content {
    background-color: #3B3E47;
    z-index: 999;
} */

/* /deep/ .van-field__control {
    color: #fff;
} */

/* /deep/.van-field__left-icon {
    color: #f4f5f5;
} */

/deep/.van-grid-item__icon {
    color: #38ab77;
}
/deep/.van-icon-underway::before,
/deep/.van-icon-send-gift::before,
/deep/.van-icon-invition::before {
    color: #38ab77;
}

.cardContent .van-cell {
    color: #B5B6B8;
    background-color: #3B3E47;
}

.goods-card {
    overflow: hidden;
    background-color: #fff;
    box-shadow: rgba(0, 0, 0, 0.04) 0px 3px 5px;
}

.goods-card:last-child {
    border-bottom: 0;
}

.goodsprice {
    margin-bottom: 4px;
}

.goodsprice span {
color: red;
    font-size: 13px;
    font-weight: 600;
    
}

.goodsother {
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 12px;
    color: #f0ad4e;
}

.goods-content {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding: 8px;
}
</style>
