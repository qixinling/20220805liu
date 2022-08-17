
<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>

        <div class="cardBox" v-if="wlt">
            <div class="cardContent">
                <van-field v-model="userid" label="用户名" placeholder="请输入要查询的用户" />
                <van-row class="text-center">
                    <van-col span="12">
                        <div class="number">{{ Teamcount }}</div>
                        <div class="text">团队人数</div>
                    </van-col>
                    <van-col span="12">
                        <div class="number">{{ Teamyeji }}</div>
                        <div class="text">团队业绩</div>
                    </van-col>
                </van-row>
                <van-row class="text-center">
                    <van-col :span="colspan">
                        <div class="number">{{ Area1 }}</div>
                        <div class="text">A区</div>
                    </van-col>
                    <van-col :span="colspan" v-if="xian >= 2">
                        <div class="number">{{ Area2 }}</div>
                        <div class="text">B区</div>
                    </van-col>
                    <van-col :span="colspan" v-if="xian >= 3">
                        <div class="number">{{ Area3 }}</div>
                        <div class="text">C区</div>
                    </van-col>
                    <van-col :span="colspan" v-if="xian >= 4">
                        <div class="number">{{ Area4 }}</div>
                        <div class="text">D区</div>
                    </van-col>
                    <van-col :span="colspan" v-if="xian >= 5">
                        <div class="number">{{ Area5 }}</div>
                        <div class="text">E区</div>
                    </van-col>
                </van-row>
                <van-cell center title="展开全部">
                    <template #right-icon>
                        <van-switch v-model="expandAll" @change="expandChange" size="24" />
                    </template>
                </van-cell>
            </div>
        </div>

        <div class="scrollBox text-center" v-if="wlt">
            <vue2-org-tree name="test" :data="data" :horizontal="horizontal" :collapsable="collapsable" :label-class-name="labelClassName" :render-content="renderContent" @on-expand="onExpand" @on-node-click="onNodeClick" />
        </div>
    </div>
</template>
<script>
import HeadBar from "@/components/HeadBar";
//网络图组件
import Vue2OrgTree from "vue2-org-tree/dist/index.js";
Vue.use(Vue2OrgTree);
import "vue2-org-tree/dist/style.css";

import Vue from "vue";
import { Field } from "vant";
import { Switch } from "vant";
import { Cell } from "vant";

Vue.use(Cell);
Vue.use(Switch);
Vue.use(Field);
export default {
    name: "network",
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            userid: "",
            data: {},
            rawdata: {},
            Teamcount: 0,
            Teamyeji: 0,
            Area1: 0,
            Area2: 0,
            Area3: 0,
            Area4: 0,
            Area5: 0,
            xian: 5, //控制有几条线
            colspan: 8,
            horizontal: false,
            collapsable: true,
            expandAll: false,
            labelClassName: "bg-white", //bg-white,bg-orange,bg-gold,bg-gray,bg-lightpink,bg-chocolate,bg-tomato
            wlt: true,
        };
    },
    created() {
        this.getdata();
        if (this.xian == 1) {
            this.colspan = 24;
        } else if (this.xian == 2) {
            this.colspan = 12;
        } else if (this.xian == 3) {
            this.colspan = 8;
        } else if (this.xian == 4) {
            this.colspan = 6;
        } else if (this.xian == 3) {
            this.colspan = 8;
        }
    },
    watch: {
        userid: function (val) {
            // 用户名查询
            var _this = this;
            var queryuserid = function (qdata) {
                if (qdata.Label == val) {
                    _this.data = qdata;
                    _this.Teamcount = qdata.Teamcount;
                    _this.Teamyeji = qdata.Teamyeji;
                    _this.Area1 = qdata.Area1;
                    _this.Area2 = qdata.Area2;
                    _this.Area3 = qdata.Area3;
                    _this.Area4 = qdata.Area4;
                    _this.Area5 = qdata.Area5;
                } else {
                    qdata.children.forEach((item) => {
                        if (item.Label == val) {
                            _this.data = item;
                            _this.Teamcount = item.Teamcount;
                            _this.Teamyeji = item.Teamyeji;
                            _this.Area1 = item.Area1;
                            _this.Area2 = item.Area2;
                            _this.Area3 = item.Area3;
                            _this.Area4 = item.Area4;
                            _this.Area5 = item.Area5;
                        } else {
                            if (item.Id != 0) {
                                queryuserid(item);
                            }
                        }
                    });
                }
            };
            queryuserid(_this.rawdata[0]);
        },
    },
    methods: {
        getdata: function () {
            var _this = this;
            _this.$request.post(
                "api/Users/Network",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    areacount: _this.xian,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _this.rawdata = JSON.parse(res.data.data.network);
                    _this.Teamcount = _this.rawdata[0].Teamcount;
                    _this.Teamyeji = _this.rawdata[0].Teamyeji;
                    _this.Area1 = _this.rawdata[0].Area1;
                    _this.Area2 = _this.rawdata[0].Area2;
                    _this.Area3 = _this.rawdata[0].Area3;
                    //递归点位下方添加注册

                    var pushregister = function (pdata) {
                        if (pdata.Id > 0) {
                            for (var i = 0; i < _this.xian; i++) {
                                if (pdata.children[i]) {
                                    pushregister(pdata.children[i]);
                                }
                            }
                        }
                    };
                    pushregister(_this.rawdata[0]);
                    _this.data = _this.rawdata[0];
                }
            );
        },

        renderContent(h, data) {
            return data.Label;
        },
        onExpand(e, data) {
            if ("expand" in data) {
                data.expand = !data.expand;
                if (!data.expand && data.children) {
                    this.collapse(data.children);
                }
            } else {
                this.$set(data, "expand", true);
            }
        },
        //点击选项执行的方法，可以用于跳转到其他链接，注意一定要写协议头
        onNodeClick(e, data) {
            //跳转注册页面
            if (data.Id == 0) {
                this.$utils.setloc("reg_fname", data.Fname);
                this.$utils.setloc("reg_tp", data.Treeplace);
                this.$router.push({
                    path: "/Register",
                });
            }
        },
        collapse(list) {
            var _this = this;
            list.forEach(function (child) {
                if (child.expand) {
                    child.expand = false;
                }
                child.children && _this.collapse(child.children);
            });
        },
        expandChange() {
            this.toggleExpand(this.data, this.expandAll);
        },
        toggleExpand(data, val) {
            var _this = this;
            if (Array.isArray(data)) {
                data.forEach(function (item) {
                    _this.$set(item, "expand", val);
                    if (item.children) {
                        _this.toggleExpand(item.children, val);
                    }
                });
            } else {
                this.$set(data, "expand", val);
                if (data.children) {
                    _this.toggleExpand(data.children, val);
                }
            }
        },
    },
};
</script>
<style scoped>
.scrollBox {
    overflow-y: scroll;
    overflow-x: scroll;
    width: 100%;
    height: 100%;
}

.text-center {
    text-align: center;
}

.number {
    font-weight: 600;
    color: orangered;
    padding-top: 10px;
}

.text {
    padding-top: 10px;
    padding-bottom: 10px;
    color: #888;
    font-size: 12px;
}

.org-tree-container {
    background: none;
}

/deep/ .org-tree-node-label {
    background: #fff;
}

.org-tree-node-label {
    white-space: nowrap;
}

/*颜色*/
.bg-white {
    background-color: white;
}

.bg-orange {
    background-color: orange;
}

.bg-gold {
    background-color: gold;
}

.bg-gray {
    background-color: gray;
}

.bg-lightpink {
    background-color: lightpink;
}

.bg-chocolate {
    background-color: chocolate;
}

.bg-tomato {
    background-color: tomato;
}
</style>
 