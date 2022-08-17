<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <van-collapse v-model="activeNames">
            <div v-for="(item, index) in data" :key="index">
                <van-collapse-item :title="item.question" :name="index">
                    {{item.answer}}
                </van-collapse-item>
            </div>
        </van-collapse>
    </div>
</template>

<script>
import Vue from "vue";
import { Collapse, CollapseItem } from "vant";
import HeadBar from "@/components/HeadBar";
Vue.use(Collapse);
Vue.use(CollapseItem);
export default {
    name: "Question",
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            activeNames: ["1"],
            data: [],
        };
    },
    created() {
        this.GetData();
    },
    methods: {
        GetData() {
            var _this = this;
            _this.$request.post(
                "api/Help/List",
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
                    console.log(res)
                    _this.data = res.data.data;
                }
            );
        },
    },
};
</script>

<style scoped>
</style>