<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>

        <div class="cardBox" :key="index" v-for="(item, index) in tablelist">
            <div class="cardContent">
                <van-row>
                    <van-col span="24" class="btime">
                        <van-icon name="clock" style="font-size: 14px; padding-right: 10px" />{{ item.bsdate }}
                    </van-col>
                    <van-col span="24">
                        <van-divider />
                    </van-col>
                </van-row>
                <van-row style="line-height: 30px">
                    <van-col span="10" class="bounstext">{{ item.lxname }}</van-col>
                    <van-col span="14" class="jine">{{ item.jine }}</van-col>
                    <van-col span="10" class="bounstext">来源</van-col>
                    <van-col span="14" class="bounstext" style="text-align: right">{{
            item.yuserid
          }}</van-col>
                </van-row>
            </div>
        </div>
        <Login></Login>
    </div>
</template>

<script>
import Login from "@/components/Login";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Tab, Tabs } from "vant";
import { Divider, Icon } from "vant";
Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Divider);
Vue.use(Icon);
export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            active: 0,
            loading: false,
            query_date: "",
            bonusname: [],
            bonusdisplay: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            sourcelist: [],
            sourcedata: [],
            tablelist: [],
            tabledata: [],
            bid: "",
        };
    },
    created() {
        if (this.$route.query.id != "") {
            this.bid = this.$route.query.id;
            this.load();
        }
    },
    methods: {
        load: function () {
            //登录组件登录成功后会触发该函数,刷新父窗体数据
            if (this.$store.state.LoginState) {
                //子组件调用加载
                this.getdate();
            }
        },

        getdate: function () {
            this.tablelist = [];
            var _this = this;
            _this.$request.post(
                "api/Bonus/Source_UsersList",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    select_uid: _this.$utils.getloc("id"),
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
                    _this.tabledata = res.data.data;
                    _this.tableloadMore();
                }
            );
        },
        tableloadMore: function () {
            if (this.tabledata.length <= 0) {
                if (this.tablelist.length > 0) {
                    this.nomore = "没有更多了...";
                }
            } else {
                this.loading = true;
                setTimeout(() => {
                    for (let i = 0; i < this.$utils.page_size; i++) {
                        if (this.tabledata.length <= 0) {
                            break;
                        } else {
                            //把第一条数据添加到展示列表
                            this.tablelist.push(this.tabledata[0]);
                            //移除data第一条数据
                            this.tabledata.splice(0, 1);
                        }
                    }
                    console.log(this.tablelist);
                    this.loading = false;
                }, 1000);
            }
        },
    },
};
</script>

<style scoped>
.btime {
    text-align: left;
    font-size: 12px;
    font-weight: bold;
    color: #888;
    display: flex;
    justify-content: left;
    align-items: center;
}
.bounstext {
    color: #555;
    font-size: 15px;
    text-align: left;
}
.jine {
    color: #ee0a24;
    /* color: #222; */
    font-size: 15px;
    /* font-weight: bold; */
    text-align: right;
}
.cardContent {
    border-radius: inherit !important;
    padding: 15px;
}
</style>
