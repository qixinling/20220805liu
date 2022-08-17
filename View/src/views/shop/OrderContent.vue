<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div :id="dd.id" v-for="(dd, index) in list" :key="index">
            <div class="cardBox">
                <div class="cardContent">
                    <van-field label="订单编号" :value="dd.orderNo" readonly :border="false" />
                    <van-field label="下单时间" :value="dd.odate" readonly :border="false" />
                    <van-field label="发货时间" :value="dd.fdate" readonly :border="false" v-if="dd.orderstate >= 2" />
                    <van-field label="状态" :value="dd.orderstatename" readonly :border="false" />
                    <!-- <van-field label="订单金额" :value="dd.yjine" readonly :border="false" /> -->
                    <!-- <van-field label="实付金额" :value="dd.sjine" readonly :border="false" /> -->
                </div>
            </div>

            <div class="cardBox">
                <div class="cardBox cardContent" style="padding: 10px;">
                    <van-field label="快递名" :value="dd.kuaidiname" readonly :border="false" v-if="dd.orderstate >= 2" />
                    <van-field label="快递单号" :value="dd.kuaidiNo" readonly :border="false" v-if="dd.orderstate >= 2" />
                    <van-field label="收货人" :value="dd.username" readonly :border="false" />
                    <van-field label="联系电话" :value="dd.usertel" readonly :border="false" />
                    <van-field label="地址" type="textarea" :value="dd.sheng + dd.shi + dd.xian + dd.useraddress" readonly :border="false" />
                </div>
            </div>

            <div class="cardBox">
                <div class="cardBox cardContent" style="padding: 10px;">
                    <van-row v-for="(item, index) in oclist[index]" :key="index">
                        <van-col span="8">
                            <img style="width: 100px;height: 100px;" :src="getimg(item.goodsimg)" />
                        </van-col>
                        <van-col span="16">
                            <van-col span="24" class="goodsname">{{ item.goodsname }}</van-col>
                            <van-col span="24" class="jiage">
                                <!-- <span style="color: red">￥{{ item.sjine }}</span> -->
                                <span style="float: right;font-size: 12px;">x {{ item.num }}</span>
                            </van-col>
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
import { Field } from "vant";

Vue.use(Field);
export default {
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            oid: "",
            list: [],
            oclist: [],
        };
    },
    created() {
        this.oid = this.$route.query.id;
        if (this.$store.state.LoginState) {
            this.getdata();
        }
    },
    methods: {
        getdata: function () {
            let _this = this;
            this.$request.post(
                "Api/ShopOrder/Get",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    id: _this.oid,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.list = res.data.data;
                    _this.list.forEach((item) => {
                        _this.oclist.push(JSON.parse(item.oclist));
                        for (var i = 0; i < _this.oclist[0].length; i++) {
                            _this.oclist[0][i].goodsimg =
                                _this.oclist[0][i].goodsimg.split(",")[0];
                        }
                    });
                }
            );
        },
        getimg: function (img) {
            return this.$config.send_url + "Upload/" + img;
        },
    },
};
</script>

<style scoped>
.goodsname {
    font-size: 14px;
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: 2;
    overflow: hidden;
}

.jiage {
    padding-top: 20px;
}
</style>
