<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <van-address-edit :area-list="areaList" :address-info="AddressInfo" show-set-default :area-columns-placeholder="['请选择', '请选择', '请选择']" @save="onSave" />
        <Login></Login>
    </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Login from "@/components/Login";
import areaList from "@/assets/js/area.js";
import Vue from "vue";
import { AddressEdit } from "vant";

Vue.use(AddressEdit);
export default {
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            areaList,
            searchResult: [],
            username: "",
            usertel: "",
            sheng: "",
            shi: "",
            xian: "",
            address: "",
            isdefu: "",
            areaCode: "",
            AddressInfo: {
                //收货人信息初始值
                name: "", //姓名
                tel: "", //电话
                province: "", //省份
                city: "", //城市
                country: "", //区县
                areaCode: "", //地址code：ID
                addressDetail: "", //详细地址
                isDefault: false, //是否选择默认
            },
        };
    },
    methods: {
        onSave(e) {
            this.username = e.name;
            this.usertel = e.tel;
            this.sheng = e.province;
            this.shi = e.city;
            this.xian = e.county;
            this.address = e.addressDetail;
            this.isdefu = e.isDefault;
            this.areaCode = e.areaCode;
            this.Add();
        },

        Add: function () {
            if (this.$store.state.LoginState) {
                var _this = this;
                let _toast = _this.$toast.loading({
                    message: "正在添加...",
                    duration: 0, // 持续展示 toast
                });
                _this.$request.post(
                    "api/UsersAddress/Add",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                        add_username: this.username,
                        add_usertel: this.usertel,
                        add_sheng: this.sheng,
                        add_shi: this.shi,
                        add_xian: this.xian,
                        add_address: this.address,
                        isdefault: this.isdefu,
                        areaCode: this.areaCode,
                    },
                    (res) => {
                        if (res.data.code == 0) {
                            _this.$dialog.alert({
                                title: "提示",
                                message: res.data.msg,
                            });
                            return;
                        }
                        _this.$toast({
                            message: res.data.msg,
                            position: "middle",
                            duration: 2000,
                            icon: "passed",
                        });

                        _this.AddressInfo = {
                            //收货人信息初始值
                            name: "", //姓名
                            tel: "", //电话
                            province: "", //省份
                            city: "", //城市
                            country: "", //区县
                            areaCode: "", //地址code：ID
                            addressDetail: "", //详细地址
                            isDefault: false, //是否选择默认
                        };
                    }
                );
            } else {
                _this.$toast({
                    message: "请先登录再添加地址",
                    position: "middle",
                    duration: 2000,
                    icon: "warning-o",
                });
            }
        },
    },
};
</script>

<style scoped>
/deep/.van-button--danger {
    background-color: #ff4500;
    border: 1px solid #ff4500;
}

/deep/.van-switch--on {
    background-color: #ff4500;
}
</style>
