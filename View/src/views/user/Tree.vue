<template>
    <div class="tree">
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>

        <div class="tj-block" v-if="tjt">
            <div class="cardContent">
                <!-- <van-field v-model="filterText" label="用户名" placeholder="请输入要查询的用户" /> -->
                <van-row style="text-align: center">
                    <van-col span="8">
                        <div class="number">{{ teamcount }}</div>
                        <div class="text">团队人数</div>
                    </van-col>
					<van-col span="8">
					    <div class="number">{{ lsk }}</div>
					    <div class="text">个人业绩</div>
					</van-col>
                    <van-col span="8">
                        <div class="number">{{ riteamyeji }}</div>
                        <div class="text">今日团队业绩</div>
                    </van-col>
					
                </van-row>
            </div>
        </div>

        <div class="tree-block" v-if="tjt">
            <div class="cardContent">
                <van-cell center title="展开全部">
                    <template #right-icon>
                        <van-switch v-model="expandAll" @change="expandChange" size="24" />
                    </template>
                </van-cell>
                <el-tree class="filter-tree" :data="data" :props="defaultProps" node-key="Id" :filter-node-method="filterNode" ref="tree">
                    <span slot-scope="{ node, data }">
                        <i :class="data.Icon"></i>
                        <span style="padding-left: 4px">{{ node.label }}</span>
                    </span>
                </el-tree>
            </div>
        </div>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Field } from "vant";
import { Switch } from "vant";
import { Cell } from "vant";

Vue.use(Cell);
Vue.use(Switch);
Vue.use(Field);

import Tree from "element-ui/lib/tree";
import "element-ui/lib/theme-chalk/icon.css";
import "element-ui/lib/theme-chalk/tree.css";

Vue.use(Tree);

export default {
    name: "tree",
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            filterText: "",
            data: [],
            teamcount: 0,
            teamyeji: 0,
			riteamyeji:0,
			lsk:0,
            expandAll: false,
            defaultProps: {
                children: "Children",
                label: "Label",
            },
            tjt: true,
        };
    },
    created() {
        this.load();
    },
    methods: {
        load: function () {
            if (this.$store.state.LoginState) {
                this.getdata();
            }
        },
        filterNode(value, data) {
            if (!value) return true;
            return data.Label.indexOf(value) !== -1;
        },
        expandChange() {
            for (
                var i = 0;
                i < this.$refs.tree.store._getAllNodes().length;
                i++
            ) {
                this.$refs.tree.store._getAllNodes()[i].expanded =
                    this.expandAll;
            }
        },
        getdata: function () {
            var _this = this;
            _this.$request.post(
                "api/Users/Tree",
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
                    _this.data = JSON.parse(res.data.data[0].tree);
                    _this.teamcount = _this.data[0].Teamcount;
                    _this.teamyeji = _this.data[0].Teamyeji;
					_this.riteamyeji = _this.data[0].Riteamyeji;
					 _this.lsk = _this.data[0].Lsk;
                     console.log(_this.data)
                }
            );
        },
    },
    watch: {
        filterText(val) {
            this.$refs.tree.filter(val);
        },
    },
};
</script>

<style scoped>
	.el-tree{
		color: #ff4500;
	}
.tree {
    font-size: 14px;
}

.number {
    font-weight: 600;
    color: #ff4500;
    padding-top: 10px;
}

.text {
    padding-top: 3px;
    padding-bottom: 6px;
    color: #888;
    font-size: 12px;
}

.el-tree {
    background: none;
}

/deep/.ispay0 {
    color: #909399;
}

/deep/.ispay1 {
    color: #67c23a;
}

/deep/.ispay2 {
    color: #e6a23c;
}


/deep/.van-field__control {
    color: #fff;
}

/deep/.van-switch--on {
    background-color: #ff4500;
}

.tj-block {
    background-color: #fff;
    border-bottom: 1px solid #ccc;
}

.tree-block {
    min-height: calc(100vh - 100px);
    background-color: #fff;
}
</style>
