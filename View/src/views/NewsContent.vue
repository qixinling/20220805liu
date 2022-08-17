<template>
    <div>
        <HeadBar :bg="'transparent'"></HeadBar>
        <div class="cardBox">
            <div v-if="news.news_cover != '' && news.news_cover != null">
                <img :src="news.news_cover" alt="" />
            </div>
            <div v-else style="height: 70px;"></div>
            <div class="xq">
                <div class="xqn">
                    <van-row>
                        <van-col span="24" class="newsTitle">{{ news.news_title }}</van-col>
                    </van-row>
                    <van-row>
                        <van-col span="24" class="newsdate">{{$utils.timestampFormat(news.news_time)}}</van-col>
                    </van-row>
                </div>
                <div class="xqc">
                    <van-row>
                        <van-col span="24" class="newcontent">
                            <div v-html="news.news_content"></div>
                        </van-col>
                    </van-row>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Field, Button, Toast } from "vant";
Vue.use(Field);
Vue.use(Button);
Vue.use(Toast);
export default {
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            news: {},
            nid: 0,
        };
    },
    created() {
        if (this.$route.query.id != "") {
            this.nid = this.$route.query.id;
            this.getdata();
        }
    },
    methods: {
        getdata: function () {
            //查询新闻详情
            var _this = this;
            _this.$request.post(
                "api/News/Get",
                {
                    select_id: _this.nid,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.news = res.data.data[0];
                    _this.news.news_content = _this.news.news_content.replace(
                        /src="/g,
                        'src="' + _this.$config.send_url + "Upload/"
                    );
                }
            );
        },
    },
};
</script>

<style scoped>
.cardBox {
    padding-top: 50px;
}

.xq {
    padding-bottom: 20px;
    position: relative;
}

.xqn {
    width: 100%;
    margin-top: -70px;
    padding-top: 20px;
    border-top-left-radius: 20px;
    border-top-right-radius: 20px;
    /* background: linear-gradient(
        rgba(255, 255, 255, 0.8),
        rgba(255, 255, 255, 1)
    ); */
    background-color: #3B3E47;
    z-index: 999;
}

.xqc {
    background: #3B3E47;
    border-bottom-left-radius: 20px;
    border-bottom-right-radius: 20px;
}

.newsTitle {
    font-size: 18px;
    font-weight: 700;
    padding: 0 10px 5px 10px;
}

.newsimg {
    width: 100%;
    border-radius: 10px;
}

.newsdate {
    color: #888;
    padding-left: 10px;
}

/deep/ img {
    width: 100%;
}

.newcontent div {
    padding: 0 10px;
}

.newcontent /deep/ img {
    border-radius: 10px;
}
</style>
