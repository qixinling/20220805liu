<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <van-address-list v-model="chosenAddressId" :list="adddresslist" default-tag-text="默认" @add="onAdd" @edit="onEdit" :switchable="false" />
        <Login></Login>
    </div>
</template>

<script>
import Login from "@/components/Login";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { AddressList } from "vant";

Vue.use(AddressList);

export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            chosenAddressId: "1",
            list: [],
            alx: "",
            adddresslist: []
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
                this.getdata();
            }
        },
        onAdd() {
            this.$router.push({
                name: "AddressAdd",
            });
        },
        onEdit(item) {
            this.$router.push({
                name: "AddressUpdate",
                query: {
                    id: item.id,
                },
            });
        },

        getdata() {
            let _this = this;
            _this.list = [];
            _this.$request.post(
                "Api/UsersAddress/List",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    uid: _this.$utils.getloc("id"),
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
                    let  adddresslist= []
                    _this.list.forEach((item) => {
                        adddresslist.push({
                            id: item.id,
                            name: item.username,
                            tel: item.usertel,
                            address: item.sheng+item.shi+item.shi+item.xian+item.address,
                            isDefault: item.isdefault,
                        })
                    });
                    _this.adddresslist = adddresslist
                }
            );
        },
    },
};
</script>

<style scoped>
.van-address-item__address {
    color: #888;
    font-size: 12px;
}
.van-address-list__bottom {
    background-color: transparent;
    position: inherit;
    margin-top: 30px;
}
/deep/.van-button--danger {
    background-color: #ff4500;
    border: 1px solid #ff4500;
}
/deep/.van-address-list__bottom {
    padding: 5px 1px;
}
/deep/.van-tag--danger {
    background-color: #ff4500;
}

.van-address-item__edit {
    color: #B5B6B8;
}
</style>
