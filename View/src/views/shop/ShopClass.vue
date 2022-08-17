<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <van-tree-select height="100%" :items="items" :active-id.sync="items.activeId" :main-active-index.sync="active" @click-nav="onNavClick">
            <template #content>
                <div class="xlclass" v-for="(Xl, index) in xllist" :key="index">
                    <router-link :to="'goodssearch?id=' + Xl.id">
                        <img :src="getimg(Xl.xiaoleiimg)" style="width: 50px;height: 50px; border-radius: 50px;" />
                        <span style="font-size: 12px;color: #888;">
                            {{
              Xl.xiaoleiname
              }}
                        </span>
                    </router-link>
                </div>
            </template>
        </van-tree-select>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { TreeSelect } from "vant";

Vue.use(TreeSelect);
export default {
    components: {
        HeadBar,
    },
    data() {
        return {
            title: document.title,
            active: 0,
            items: [],
            goods_list: [],
            popupVisible: false,
            xlname: "",
            xlid: 0,
            data: [],
            list: [],
            xllist: [],
        };
    },
    created() {
        this.getdata();
    },
    methods: {
        getdata: function () {
            // 查询大小类
            let _this = this;
            this.$request.post("Api/ShopGoods/Daxiaolei_List", {}, (res) => {
                if (res.data.code == 0) {
                    _this.$dialog.alert({
                        title: "提示",
                        message: res.data.msg,
                    });
                    return;
                }
                _this.list = res.data.data;
                console.log(_this.list)
                _this.list.forEach((item) => {
                    var data = {
                        activeId: item.id,
                        text: item.daleiname,
                    };
                    _this.items.push(data);
                });
                 _this.lxlist = [];
                 let dlid = _this.items[0].activeId;
                
                _this.list.forEach((item) => {
                    item.dbShopGoodsSortChild.forEach((item2) => {
                        if (dlid == item2.sid && item2.pagemark == 0) {
                            item2.xiaoleiimg =
                                _this.$config.send_url +
                                "Upload/" +
                                item2.xiaoleiimg;
                            _this.xllist.push(item2);
                        }
                    });
                });
                _this.onNavClick(0);
            });
        },
        onNavClick(index) {
            this.xllist = [];
            let dlid = this.items[index].activeId;
            this.list.forEach((item) => {
                item.dbShopGoodsSortChild.forEach((item2) => {
                    if (dlid == item2.sid && item2.pagemark == 1) {
                        this.xllist.push(item2);
                    }
                });
            });
        },
        getimg: function (img) {
            if (img != "" && img != null) {
                return this.$config.send_url + "Upload/" + img;
            }
        },
    },
};
</script>

<style scoped>
.xlclass {
    float: left;
    width: 25%;
    height: 70px;
    text-align: center;
    margin-bottom: 15px;
    padding: 10px;
}

.xlclass img {
    display: block;
    margin: 0 auto;
    width: 40px;
    height: 40px;
    margin-top: 5px;
    border-radius: 50px;
}

/deep/.van-sidebar-item--select::before {
    height: 40px;
    background-color: #4fc08d;
}
</style>
