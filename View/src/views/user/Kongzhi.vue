<template>
    <div>
<HeadBar :title="title" :bg="'transparent'" toback="User"></HeadBar>
<div class="header-row">
	<van-row style="text-align: center;padding: 15px">
		<van-col span="12" @click="tolink('Zihua')">
			<div style="font-size: 18px;color:#ff976a;font-weight: 600;">{{ Glpanel.dscount ? Glpanel.dscount : 0 }}</div>
			<div style="font-size: 14px;padding-top: 10px;">待售字画</div>
		</van-col>
		<van-col span="12">
			<div style="font-size: 18px;color:#ff976a;font-weight: 600;" @click="tolink('YuyueRecord')">{{ Glpanel.yycount ? Glpanel.yycount : 0 }}</div>
			<div style="font-size: 14px;padding-top: 10px;">预约数量</div>
		</van-col>
	</van-row>
</div>
				<div>
					<van-cell title="上架审核" icon="friends " value="" is-link to="./Shangjia" />
					<van-cell title="提现审核" icon="card" value="" is-link to="./TixianPass" />
					<van-cell title="画室统计" icon="star" value="" is-link to="./Tongji" />
					<van-cell title="字画管理" icon="photo " value="" is-link to="Zihua" />
					<van-cell title="转画管理" icon="share" value="" is-link to="./Zhuanhua" />
					<van-cell title="画室编码" icon="friends-o" value="" is-link to="./HsInfo">
                        <template #icon>
                            <img src="@/assets/img/kongzhi6.svg" width="24.5" height="24" style="margin-right:9px" />
                        </template>
                    </van-cell>
                    <van-cell title="画室会员" icon="friends-o" value="" is-link to="./Huiyuan">
                        <template #icon>
                            <img src="@/assets/img/kongzhi6.svg" width="24.5" height="24" style="margin-right:9px" />
                        </template>
                    </van-cell>
					<van-cell title="我的打款" icon="friends" value="" is-link to="./DaKuan" />
					<van-cell title="我的收款" icon="friends" value="" is-link to="./ShouKuan" />
                    <van-cell icon="diamond" title="会员预约">
                        <template #right-icon>
                            <van-switch v-model="checked" active-color="#FC4702" size="24px" @change="change" />
                        </template>
                    </van-cell>
				</div>


        <Login></Login>
        
    </div>
</template>

<script>
import Login from "@/components/Login";
import HeadBar from "@/components/HeadBar";

import Vue from "vue";
import { Grid, GridItem } from "vant";
import { Cell, CellGroup, Tag, NoticeBar, Col, Row, Switch } from "vant";

Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Grid);
Vue.use(GridItem);
Vue.use(Tag);
Vue.use(NoticeBar);
Vue.use(Col);
Vue.use(Row);
Vue.use(Switch);

export default {
    name: "user",
    components: {
        Login,
        HeadBar,
    },
    data() {
        return {
			title: document.title,
            active: "",
            userid: "您还没有登录,请先登录.",
            usinfo: {},
           wallet:[],
            msgCount: 0,
            marqueemsg: "",
            defaulttx: require("../../assets/img/tx.png"),
			zjine:0,
            checked: false,
            Glpanel: {}
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
                this.getGlpanel()
				this.Wallet_GetWallet();
                // this.getordernum();
            }
        },
        getimg: function (imgname) {
            let path = this.$config.send_url + "Upload/" + imgname;
            if (typeof imgname == "undefined") {
                path = this.defaulttx;
            }
            return path;
        },
		Wallet_GetWallet: function () {
		    let _this = this;
		    let _toast = _this.$toast.loading({
		        message: "正在加载...",
		        duration: 0, // 持续展示 toast
		    });
		    _this.$request.post(
		        "api/Wallets/GetWallet",
		        {
		            token: _this.$utils.getloc("token"),
		            userid: _this.$utils.getloc("userid"),
		            id: _this.$utils.getloc("id"),
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
		            _this.wallet = res.data.data.ulist[0];
					
		        }
		    );
		},
        getdata: function () {
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
                    _this.usinfo = res.data.data;
                   // _this.zjine = Number(_this.usinfo.mey) + Number(_this.usinfo.jindou);
                    _this.userid = _this.usinfo.userid;
                    _this.msgCount = _this.usinfo.msgcount;
                }
            );
        },
 
        tolink: function (path) {
            this.$router.push({
                name: path,
				query:{
					lx:2
				}
            });
        },
		CopyLink() {
			if (this.usinfo.usertel != null && this.usinfo.usertel != "") {
				var domUrl = document.createElement("input");
				domUrl.value = this.usinfo.usertel;
				domUrl.id = "creatDom";
				document.body.appendChild(domUrl);
				domUrl.select(); // 选择对象
				document.execCommand("Copy"); // 执行浏览器复制命令
				let creatDom = document.getElementById("creatDom");
				creatDom.parentNode.removeChild(creatDom);
				this.$toast.success("复制成功");
			}
		},
        change(val) {
            let that = this
            that.$request.post(
                "api/users/Updateyy",
                {
                    token: that.$utils.getloc("token"),
		            userid: that.$utils.getloc("userid"),
		            id: that.$utils.getloc("id"),
                },
                (res) => {
                    that.$dialog.alert({
		                title: "提示",
		                message: res.data.msg,
		            });
                    that.getGlpanel()
                }
            )
        },
        getGlpanel() {
            let that = this
            that.$request.post(
                "api/users/Glpanel",
                {
                    token: that.$utils.getloc("token"),
		            userid: that.$utils.getloc("userid"),
		            id: that.$utils.getloc("id"),
                },
                (res) => {
                    if (res.data.code == 0) {
		                that.$dialog.alert({
		                    title: "提示",
		                    message: res.data.msg,
		                });
		                return;
		            }
                    let data = res.data.data
                    that.Glpanel = data
                    that.checked = that.Glpanel.isyy == 0 ? false : true
                }
            )
        },
    },
};
</script>

<style scoped>
.van-cell__value {
    font-size: 12px;
}

/deep/.van-cell__left-icon {
    font-size: 24px;
    padding-right: 4px;
}

/deep/.van-icon-underway::before,
/deep/.van-icon-send-gift::before,
/deep/.van-icon-invition::before,
/deep/.van-icon-warn-o::before
{
    color: #BB982D;
}

.a /deep/.van-cell {
    padding: 5px 16px;
}

/deep/.topbtn .van-grid-item__content {
    background-color: transparent;
}

/deep/.topbtn .van-grid-item__icon {
    color: #fff;
}

/deep/.topbtn .van-icon-underway::before,
/deep/.topbtn .van-icon-send-gift::before,
/deep/.topbtn .van-icon-invition::before {
    color: #fff;
}

/deep/.topbtn .van-grid-item__text {
    color: #fff;
}

/* .van-cell {
    background-color: #3B3E47;
    color: #fff;
} */

/* /deep/ .van-grid-item__content {
    background-color: #3B3E47;
}

/deep/ .van-grid-item__text {
    color: #fff;
} */

.van-button--default {
    border: 0;
}

/* .van-cell::after {
    border: 1px solid rgba(88, 88, 88, .6);
} */

/deep/ .van-cell__value {
    color: #B5B6B8 !important;
}

.card-info {
    background-color: #3B3E47;
    margin: 10px 10px 0 10px;
    border-radius: 10px;
}

.card-info .van-col {
  padding: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.card-info .value {
  color: #f4c40b;
  font-size: 16px;
  font-weight: 600;
}

.card-info .key {
  font-size: 12px;
  margin-top: 4px;
}

.tool-box {
    margin: 0 10px 10px 10px;
    border-radius: 10PX;
    overflow: hidden;
}

.user-header {
    padding-bottom: 10px;
	background-color: #f5cf9c;
    /* color: rgb(100, 22, 2);
    background: url("../../assets/img/user.png");
    background-size: 100% 100%; */
	
}

.van-icon-friends {
    color: #FC4702;
}

.van-icon-card {
    color: #FC4702;
}

.van-icon-share {
    color: #FC4702;
}

.van-icon-star {
    color: #FC4702;
}

.van-icon-photo {
    color: #FC4702;
}

.van-icon-diamond {
    color: #FC4702;
}

.header-row {
    border-top: 1px solid #E5E5E5;
    border-bottom: 8px solid #E5E5E5;
}
</style>
