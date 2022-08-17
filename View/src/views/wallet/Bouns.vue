<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="list-content">
            <div class="item-block" :key="index" v-for="(item, index) in tablelist">
                <div class="cardContent">
                    <van-row>
                        <van-col span="12" style="font-size: 14px;color: #333;font-weight: 600;">
                            {{item.lxname}}
                        </van-col>
                        <van-col span="12" style="font-size: 14px;color: red;text-align: right;">
                            {{item.jine}}
                        </van-col>
                    </van-row>
                    <van-row style="margin-top: 10px;">
                        <van-col span="12" style="font-size: 12px;color: #777;">
                            {{item.yuserid}}
                        </van-col>
                        <van-col span="12" style="font-size: 12px;color: #777;text-align: right;">
                            {{$utils.timestampFormat(item.sdate)}}
                        </van-col>
                    </van-row>
                    <van-row style="margin-top: 10px;" v-if="item.bz != '-'">
                        <van-col span="24" style="font-size: 12px;color: #777;">
                            {{item.bz}}
                        </van-col>
                    </van-row>
                </div>
            </div>
            <van-empty description="暂无收益" v-if="tablelist.length == 0" />
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
import { Empty } from 'vant';

Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Divider);
Vue.use(Icon);
Vue.use(Empty);
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
            tablelist: [],
            tabledata: [],
        };
    },
    created() {
        this.load();
    },
    methods: {
        load: function () {
            //登录组件登录成功后会触发该函数,刷新父窗体数据
            if (this.$store.state.LoginState) {
                //子组件调用加载
               // this.getbonusmod();
                this.gettable();
            }
        },
        tolink: function (btid) {
            this.$router.push({
                name: "Bounssource",
                query: {
                    id: btid,
                },
            });
        },
        gettable: function () {
            this.tablelist = [];
            var _this = this;
            _this.$request.post(
                "api/Bonus/Source_List",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    select_uid: _this.$utils.getloc("id")
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
                    this.loading = false;
                }, 1000);
            }
        },
    },
};
</script>

<style scoped>
.mx {
    color: #0068f7;
    font-size: 12px;
    display: flex;
    justify-content: right;
    align-items: center;
}

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
.loading-text {
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 10px 0;
    color: #888;
}

.list-content {
    min-height: calc(100vh - 46px);
    background-color: #fff;
}

.item-block {
    border-bottom: 1px solid #ccc;
}
</style>
