<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div style="padding-top: 10px;padding-bottom: 10px;">
            <van-row>
                <van-col span="24">
                    <van-swipe class="my-swipe" :autoplay="3000" indicator-color="white">
                        <van-swipe-item v-for="(item, index) in slidelist" :key="index" @click="tolink(item.gid, item.lx, item.url)">
                            <img :src="getimg(item.img)" />
                        </van-swipe-item>
                    </van-swipe>
                </van-col>
            </van-row>
        </div>

        <div class="cardBox" v-for="(item, index) in newslist" :key="index" @click="tz(item.id)">
            <van-row>
                <van-col span="24" v-if="item.news_cover != ''">
                    <img :src="getimg(item.news_cover)" class="newsimg" />
                </van-col>
            </van-row>
            <div class="allContent">
                <van-row>
                    <van-col span="24" class="newsTitle">{{ item.news_title }}</van-col>
                </van-row>
                <van-row>
                    <van-col span="24" class="newsContent" v-html="item.news_content">
                    </van-col>
                </van-row>
                <van-row>
                    <van-col span="24" class="newsdate">{{
            $utils.timestampFormat(item.news_time)
          }}</van-col>
                </van-row>
            </div>
        </div>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Col, Row, Toast, Swipe, SwipeItem, NoticeBar } from "vant";
Vue.use(Col);
Vue.use(Row);
Vue.use(Toast);
Vue.use(Swipe);
Vue.use(SwipeItem);
Vue.use(NoticeBar);
export default {
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            newslist: [],
            slidelist: [],
        };
    },
    created() {
        this.getdata();
        this.getslide();
        this.getgg();
    },
    methods: {
        tolink: function (id, lx, url) {
            if (lx == 2) {
                // 商品
                this.$router.push({
                    name: "Goods",
                    query: {
                        id: id,
                    },
                });
            } else if (lx == 3) {
                // 新闻
                this.$router.push({
                    name: "NewsContent",
                    query: {
                        id: id,
                    },
                });
            } else if (lx == 4) {
                // 外部网址
                window.open(url);
            } else if (lx == 5) {
                // 指定分类
                this.$router.push({
                    name: "GoodsSearch",
                    query: {
                        id: id,
                    },
                });
            }
        },
        getimg: function (img) {
            if (img != "" && img != null) {
                return this.$config.send_url + "Upload/" + img;
            }
        },

        tz: function (id) {
            this.$router.push({
                name: "NewsContent",
                query: {
                    id: id,
                },
            });
        },

        getgg: function () {
            var _this = this;
            _this.$request.post("api/News/Gonggao", {}, (res) => {
                if (res.data.code == 0) {
                    _this.$dialog.alert({
                        title: "提示",
                        message: res.data.msg,
                    });
                    return;
                }
                _this.marqueemsg = res.data.data.marqueemsg;
            });
        },

        getslide: function () {
            var _this = this;
            _this.$request.post(
                "api/Slide/List",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    lx: 2,
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

        getdata: function () {
            var _this = this;
            _this.$request.post("api/News/List", {}, (res) => {
                if (res.data.code == 0) {
                    _this.$dialog.alert({
                        title: "提示",
                        message: res.data.msg,
                    });
                    return;
                }
                var list = res.data.data;
                list.forEach((item) => {
                    var reg = /<img.+?src=('|")?([^'"]+)('|")?(?:\s+|>)/gim;
                    var nr = item.news_content;
                    nr = nr.replace(/<[^>]+>/g, ""); // 去除HTML tag
                    item.news_content = nr;
                });
                _this.newslist = list;

                // var data1 = JSON.parse(res.data.data.data1);
                // var data2 = JSON.parse(res.data.data.data2);
                // var data3 = JSON.parse(res.data.data.data3);

                // console.log("删除Newstitle");
                // console.log(data1);
                // console.log("新增nid");
                // console.log(data2);
                // console.log("新增nid并删除Newstime");
                // console.log(data3);
            });
        },
    },
};
</script>

<style scoped>
.allContent {
    margin-top: -12px;
    padding: 0 10px;
    background: white;

    /* border-bottom-left-radius: 10px;
		border-bottom-right-radius: 10px; */
    border-radius: 10px;
}

.newsTitle {
    padding-top: 5px;
    font-size: 18px;
    font-weight: 700;
    color: #2d3436;
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
}

.newsContent {
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 4;
    -webkit-box-orient: vertical;
    font-size: 12px;
    color: #949190;
}

.newsimg {
    width: 100%;
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
}

.newsdate {
    color: #949190;
    padding: 10px 10px 5px 0;
    font-size: 12px;
    text-align: right;
}

.my-swipe {
    text-align: center;
}

.van-swipe-item img {
    width: 95%;
    border-radius: 10px;
}

.van_btn_left {
    background: white;
    border: 1px solid white;
    width: 100%;
    height: 92px;
    font-size: 16px;
}

.van_btn_left1 {
    color: #284969;
    text-align: left;
}

.van_btn_left2 {
    color: #a3b7cb;
    font-size: 12px;
    padding-top: 5px;
}

.van_btn_right_up_row {
    padding: 0 0 5px 0;
}

.van_btn_right_bom_row {
}

.van_btn_right_up {
    color: #284969;
    background: white;
    border: 1px solid white;
}

.van_btn_right_bom {
    color: #284969;
    background: white;
    border: 1px solid white;
}

.img {
    background: white;
    height: 92px;
    padding: 15px 10px 0 0;
}

.img img {
    width: 100%;
    border-radius: 5px;
}

.van_icon {
    font-size: 16px;
    padding-top: 5px;
}
</style>
