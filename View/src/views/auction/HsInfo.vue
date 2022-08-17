<template>
    <div>
        <HeadBar :title="title" :bg="'transparent'"></HeadBar>
        <div class="cardBox">
            <div class="cardContent">
                <van-field v-model.trim="studioname" label="画室名称" placeholder="请输入画室名称" :disabled="zdshow" />
				<van-field v-if="studiocard != null" v-model.trim="studiocard" type="tel" label="当前登船编码"  readonly  />
                <van-field v-model.trim="newstudiocard" type="tel" label="登船新编码"  placeholder="请输入登船新编码"  />
            </div>
        </div>
		<div style="font-size: 12px;padding-left: 10px;">
			备注：登船编码不可填写中文
		</div>

        <div class="cardBox">
            <van-button style="background-color: #ff4500; color: #fff" @click="updata" block>修改</van-button>
        </div>

        <Login></Login>
    </div>
</template>

<script>
import config from "../../assets/js/config.js";
import Login from "@/components/Login";
import HeadBar from "@/components/HeadBar";
import Vue from "vue";
import { Field, Uploader } from "vant";
import { Cell, CellGroup } from "vant";

Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Field);
Vue.use(Uploader);
export default {
    name: "UserInfo",
    components: {
        HeadBar,
        Login,
    },
    data() {
        return {
            title: document.title,
            fileList: [],
            tx: "",
            studioname: "",
            studiocard: "",
			newstudiocard:'',
			zdshow:false
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
        getdata: function () {
            // 查询用户资料
            var _this = this;
            _this.$request.post(
                "api/Users/Get",
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
                    _this.studioname = res.data.data.studioname;
					if(_this.studioname != null){
						_this.zdshow = true;
					}
                    _this.studiocard = res.data.data.studiocard;
                }
            );
        },
        updata: function () {
            // 修改用户资料
            var _this = this;
			if(/[\u4E00-\u9FA5]/g.test(_this.newstudiocard)){
				_this.$toast.fail("画室号不能输入中文");
				return;
			};
			if (_this.newstudiocard == "") {
			    _this.$toast.fail("画室号不能为空");
			    return;
			}
			if (_this.studioname == "") {
			    _this.$toast.fail("画室名称不能为空");
			    return;
			}
            let _toast = _this.$toast.loading({
                message: "正在修改...",
                duration: 0, // 持续展示 toast
            });
            //let regs = /^((13[0-9])|(17[0-1,6-8])|(15[^4,\\D])|(18[0-9]))\d{8}$/;
            // !regs.test(this.usertel)
            _this.$request.post(
                "api/Users/UpdateStudio",
                {
                    token: _this.$utils.getloc("token"),
                    userid: _this.$utils.getloc("userid"),
					uid: _this.$utils.getloc("id"),
                    card: _this.newstudiocard,
                    name: _this.studioname,
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
                        position: "bottom",
                        duration: 1000,
                    });
					_this.newstudiocard="";
					setTimeout(()=>{
						_this.getdata();
					})
                }
            );
        },
    },
};
</script>

<style scoped>
.tx {
    width: 80px;
    height: 80px;
    border-radius: 80px;
}

.van-cell__value {
    font-size: 12px;
}

/* .van-cell {
    background-color: #3B3E47;
    color: #fff;
} */

/* /deep/.van-field__control {
    color: #fff;
} */

.van-cell__value {
    color: #B5B6B8;
}

.van-button--default {
    border: 0;
}

/* .van-cell::after {
    border: 1px solid rgba(88, 88, 88, .6);
} */
</style>
