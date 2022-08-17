<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <van-address-edit :address-info="AddressInfo" :area-list="areaList" show-delete show-set-default show-search-result :area-columns-placeholder="['请选择', '请选择', '请选择']" @save="onSave" @delete="onDelete" />
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
            username: "",
            usertel: "",
            sheng: "",
            shi: "",
            xian: "",
            address: "",
            isdefu: "",
            areaCode: "",
            aid: "",
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
    created() {
        this.aid = this.$route.query.id;
        //console.log(this.aid)
        this.get();
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
            //console.log(e);
            this.Update();
        },
        onDelete() {
            this.del();
        },

        Update: function () {
            if (this.$store.state.LoginState) {
                var _this = this;
                let _toast = _this.$toast.loading({
                    message: "正在修改...",
                    duration: 0, // 持续展示 toast
                });
                _this.$request.post(
                    "api/UsersAddress/Update",
                    {
                        token: _this.$utils.getloc("token"),
                        userid: _this.$utils.getloc("userid"),
                        aid: this.aid,
                        username: this.username,
                        usertel: this.usertel,
                        sheng: this.sheng,
                        shi: this.shi,
                        xian: this.xian,
                        address: this.address,
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
                        _toast.clear();
                        _this.$toast({
                            message: res.data.msg,
                            position: "middle",
                            duration: 2000,
                            icon: "passed",
                        });
                    }
                );
            }
        },
        get: function () {
            var _this = this;
            _this.$request.post(
                "api/UsersAddress/Get",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    aid: _this.aid,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    var isdef = true;
                    if (res.data.data[0].isdefault == 0) {
                        isdef = false;
                    }
                    var data = res.data.data[0];
                    _this.AddressInfo = {
                        name: data.username,
                        tel: data.usertel,
                        province: data.sheng,
                        city: data.shi,
                        country: data.xian,
                        areaCode: data.areaCode,
                        addressDetail: data.address,
                        isDefault: isdef,
                    };
                }
            );
        },
        del: function () {
            let _toast = this.$toast.loading({
                message: "正在删除...",
                duration: 0, // 持续展示 toast
            });
            var _this = this;
            _this.$request.post(
                "api/UsersAddress/Delete",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
                    delete_id: _this.aid,
                },
                (res) => {
                    if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
                    _toast.clear();
                    _this.$toast({
                        message: res.data.msg,
                        position: "middle",
                        duration: 2000,
                        icon: "passed",
                    });
                    setTimeout(function () {
                        _this.$router.push({
                            name: "Address",
                        });
                    }, 1000);
                }
            );
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
