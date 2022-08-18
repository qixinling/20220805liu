<template>
    <div>
        <PageTitle :title="users.userid+' 资料'" :btn="false" :excel="false"></PageTitle>
        <div class="cardbox">
            <div class="cardcontent">
                <el-form ref="form" label-width="80px">
                    <el-tabs v-model="value" @tab-click="handleClick">
                        <el-tab-pane v-for="(item, index) in options" :key="index" :label="item.label" :name="item.value"></el-tab-pane>
                    </el-tabs>
                    <div v-if="value == 1">
                        <el-form-item label="注册时间">{{users.rdt}}</el-form-item>
                       <!-- <el-form-item label="激活时间">{{users.pdt}}</el-form-item> -->
                        <el-form-item label="投资金额">{{users.lsk}}</el-form-item>
                       <!-- <el-form-item label="姓名">
                            <el-input type="text" v-model="users.username"></el-input>
                        </el-form-item> -->
                        <!-- <el-form-item label="身份证号">
							<el-input type="text" v-model="users.usercode"></el-input>
						</el-form-item>-->
                        <el-form-item label="手机号">
                            <el-input type="text" v-model="users.usertel"></el-input>
                        </el-form-item>
                        <el-form-item label="推荐人">
                            <el-input type="text" style="padding-right: 10px;" v-model="users.rename" placeholder="输入新的推荐人"></el-input>
                            <el-button type="primary" size="mini" @click="updatereman">修改</el-button>
                        </el-form-item>
                       <el-form-item label="会员级别">
                            <el-select v-model="ulevel" placeholder="请选择会员级别" @change="ulevelChange" style="padding-right: 10px;">
                                <el-option v-for="(item,index) in ulevellist" :key="index" :label="item.name" :value="index">
                                </el-option>
                            </el-select>
							<el-button type="primary" size="mini" @click="updatehuashi">修改</el-button>
                        </el-form-item>
						<el-form-item label="画室名" v-if="ulevel > 0">
						    <el-input type="text" v-model="users.studioname" placeholder="设置画室名"></el-input>
						</el-form-item>
						<el-form-item label="画室编号" v-if="ulevel > 0">
						    <el-input type="text" v-model="users.studiocard" placeholder="设置画室编号"></el-input>
						</el-form-item>
						<!-- <el-form-item label="万能账号">
						    <el-switch v-model="users.iswn" active-value="1" inactive-value="0">
						    </el-switch>
						</el-form-item>
						<el-form-item label="提前抢单">
						    <el-switch v-model="users.istq" active-value="1" inactive-value="0">
						    </el-switch>
						</el-form-item> -->
                        <!-- <el-form-item label="级别">
                            <el-select v-model="xlevel" placeholder="请选择级别" @change="xlevelChange">
                                <el-option v-for="(item,index) in xlevellist" :key="index" :label="item.name" :value="index">
                                </el-option>
                            </el-select>
                        </el-form-item> -->
                        <el-form-item label="推荐人数">
                            <el-input type="number" v-model="users.recount"></el-input>
                        </el-form-item>
                        <el-form-item label="团队人数">
                            <el-input type="number" v-model="users.teamcount"></el-input>
                        </el-form-item>
                       <el-form-item label="团队业绩">
                            <el-input type="number" v-model="users.teamyeji"></el-input>
                        </el-form-item>
						<el-form-item label="个人业绩">
						     <el-input type="number" v-model="users.lsk"></el-input>
						 </el-form-item>
                        <el-form-item label="登录密码">
                            <el-input type="password" style="padding-right: 10px;" v-model="password" placeholder="请输入新的登录密码"></el-input>
                            <el-button type="primary" size="mini" @click="updatepassword(1)">修改</el-button>
                        </el-form-item>
                       <el-form-item label="支付密码">
                            <el-input type="password" style="padding-right: 10px;" v-model="password2" placeholder="请输入新的支付密码"></el-input>
                            <el-button type="primary" size="mini" @click="updatepassword(2)">修改</el-button>
                        </el-form-item>
                       <!-- <el-form-item label="转账限制">
                            <el-switch v-model="users.zzxz" active-value="1" inactive-value="0">
                            </el-switch>
                        </el-form-item> -->
                    </div>

                    <div v-if="value == 2">
                        <el-form-item v-for="(item,index) in wallet" v-bind:key="index" v-bind:label="item.cnameZh">
                            {{item.jine}}
                        </el-form-item>
                    </div>

                    <div v-if="value == 3">
                        <div class="form-menu" v-for="(bank, index) in users.ubanklist" :key="index">
                            <el-form :inline="true" label-width="80px">
                                <el-form-item class="form-menu-item" label="账户">
                                    <el-input type="text" v-model="bank.bankname"></el-input>
                                </el-form-item>
                                <el-form-item class="form-menu-item" label="账号">
                                    <el-input type="text" v-model="bank.bankcard"></el-input>
                                </el-form-item>
                            </el-form>
                            <el-form :inline="true" label-width="80px">
                                <el-form-item class="form-menu-item" label="姓名">
                                    <el-input type="text" v-model="bank.username"></el-input>
                                </el-form-item>
                                <el-form-item class="form-menu-item" label="手机号">
                                    <el-input type="text" v-model="bank.usertel"></el-input>
                                </el-form-item>
                            </el-form>
                            <el-form :inline="true" label-width="80px">
                                <el-form-item class="form-menu-item" label="收款码">
                                    <img :src="getimg(bank.bankimg)" />
                                </el-form-item>
                            </el-form>
                            <el-divider></el-divider>
                        </div>
                    </div>

                    <div v-if="value == 4">
                        <div class="form-menu" v-for="(address, index) in users.uaddresslist" :key="index">
                            <el-form :inline="true" label-width="80px">
                                <el-form-item class="form-menu-item" label="姓名">
                                    <el-input type="text" v-model="address.username"></el-input>
                                </el-form-item>
                                <el-form-item class="form-menu-item" label="手机号">
                                    <el-input type="text" v-model="address.usertel"></el-input>
                                </el-form-item>
                            </el-form>
                            <el-form :inline="true" label-width="80px">
                                <el-form-item class="form-menu-item" label="省">
                                    <el-input type="text" v-model="address.sheng"></el-input>
                                </el-form-item>
                                <el-form-item class="form-menu-item" label="市">
                                    <el-input type="text" v-model="address.shi"></el-input>
                                </el-form-item>
                                <el-form-item class="form-menu-item" label="县">
                                    <el-input type="text" v-model="address.xian"></el-input>
                                </el-form-item>
                            </el-form>
                            <el-form :inline="true" label-width="80px">
                                <el-form-item class="form-menu-item" label="详细地址">
                                    <el-input type="text" v-model="address.address"></el-input>
                                </el-form-item>
                            </el-form>
                            <el-divider></el-divider>
                        </div>
                    </div>
                    <div v-show="value == 5">
                        <el-form-item label="查询时间">
                            <el-date-picker class="gap" v-model="staffPerformance.sdate" type="date" placeholder="开始日期" />
                            <el-date-picker class="gap" v-model="staffPerformance.edate" type="date" placeholder="结束日期" />
                            <div>
                                <el-button type="primary" size="mini" @click="queryPerformance">查询</el-button>
                            </div>
                        </el-form-item>
                        <el-form-item class="form-menu-item" label="团队人数">
                            {{staffPerformance.count}}
                        </el-form-item>
                        <el-form-item class="form-menu-item" label="团队业绩">
                            {{staffPerformance.performance}}
                        </el-form-item>
                    </div>

                    <div v-show="value == 6">
                        <TablePadding v-if="bonusSource.ready" :list="bonusSource.list" :key="bonusSource.refresh" :ischeckbox="false" :isexcel="false" :summary="true" ref="bonusSource_tablepadding">
                            <el-table-column prop="sdate"  label-class-name="sdate" label="日期"></el-table-column>
                            <el-table-column prop="userid" label-class-name="userid" label="会员编号"></el-table-column>
                            <el-table-column prop="bonusName" label-class-name="bonusName" label="奖项"></el-table-column>
                            <el-table-column prop="jine" label-class-name="jine" label="金额"></el-table-column>
                            <el-table-column prop="yuserid" label-class-name="yuserid" label="来源"></el-table-column>
                            <el-table-column prop="bz" label-class-name="bz" label="备注"></el-table-column>
                        </TablePadding>
                    </div>
                    <div v-show="value == 7">
						<TablePadding v-if="bill.ready" :list="bill.list" :key="bill.refresh" :ischeckbox="false" :isexcel="false" :summary="true" ref="bill_tablepadding">
                            <el-table-column prop="bdate"  label-class-name="bdate" label="日期"></el-table-column>
                            <el-table-column prop="bname" label-class-name="bname" label="类型"></el-table-column>
                            <el-table-column label="金额">
								<template slot-scope="scope">
									<div v-for="(item, index) in scope.row.dbBillAmount" :key="index">{{item.amount}}{{item.cname}}</div>
								</template>
							</el-table-column>
                            <el-table-column prop="bz" label-class-name="bz" label="备注"></el-table-column>
                        </TablePadding>
                    </div>
                    <el-form-item>
                        <el-button type="primary" size="mini" @click="users_update" v-if="value < 5">确认修改</el-button>
                        <el-button type="danger" size="mini" @click="$router.back(-1)">返回</el-button>
                    </el-form-item>
                </el-form>
            </div>
        </div>
    </div>
</template>

<script>
import Vue from "vue";
import PageTitle from "../../components/PageTitle";
import TablePadding from "../../components/TablePadding";
import {
    Form,
    FormItem,
    Select,
    Option,
    Button,
    Input,
    Notification,
    Tabs,
    TabPane,
    Divider,
    DatePicker,
    Switch,
} from "element-ui";
Vue.use(Form);
Vue.use(FormItem);
Vue.use(Select);
Vue.use(Option);
Vue.use(Button);
Vue.use(Input);
Vue.use(Tabs);
Vue.use(TabPane);
Vue.use(Divider);
Vue.use(DatePicker);
Vue.use(Switch);
Vue.prototype.$notify = Notification;
export default {
    name: "UserEdit",
    components: {
        PageTitle,
        TablePadding,
    },
    data() {
        return {
            password: "",
            password2: "",
            wallet: [],
            id: 0,
            users: {},
            ulevellist: [],
            xlevellist: [],
            ulevel: "",
            xlevel: "",
            beizhu: "",
            options: [
                {
                    value: "1",
                    label: "资料",
                    tryGetData: true,
                },
                {
                    value: "2",
                    label: "资产",
                    tryGetData: true,
                },
                {
                    value: "3",
                    label: "账户",
                    tryGetData: true,
                },
                // {
                //     value: "4",
                //     label: "地址",
                //     tryGetData: true,
                // },
                // {
                //     value: "5",
                //     label: "业绩",
                //     tryGetData: false,
                // },
                {
                    value: "6",
                    label: "奖金来源",
                    tryGetData: true,
                },
                // {
                //     value: "7",
                //     label: "账单记录",
                //     tryGetData: true,
                // },
            ],
            value: "1",

            staffPerformance: {
                sdate: "-",
                edate: "-",
                count: 0,
                performance: 0,
            },

            bonusSource: {
                ready: false,
                list: [],
                refresh: false,
            },
			bill: {
                ready: false,
                list: [],
                refresh: false,
            },
        };
    },
    created() {
        if (this.$route.query.id != "") {
            this.id = this.$route.query.id;
            this.getdata();
        } else {
            Notification({
                message: "用户信息错误",
                offset: 100,
                type: "warning",
            });
        }
    },
    methods: {
        getUserBonusSourceData() {
            this.$request.post(
                "Api/Users_Admin/getUserBonusSourceData",
                {
                    token_admin: this.$utils.getloc("token_admin"),
                    userid_admin: this.$utils.getloc("userid_admin"),
                    uid: this.id,
                },
                (res) => {
                    if (res.data.code == 100) {
                        let data = res.data.data;
                        this.bonusSource.list = data;
                        this.bonusSource.ready = true;
                    } else {
                        Notification({
                            message: "查询失败",
                            offset: 100,
                            type: "warning",
                        });
                    }
                }
            );
        },
		getUserBillData() {
            this.$request.post(
                "Api/Users_Admin/getUserBillData",
                {
                    token_admin: this.$utils.getloc("token_admin"),
                    userid_admin: this.$utils.getloc("userid_admin"),
                    uid: this.id,
                },
                (res) => {
                    if (res.data.code == 100) {
                        let data = res.data.data;
                        this.bill.list = data;
                        this.bill.ready = true;
                    } else {
                        Notification({
                            message: "查询失败",
                            offset: 100,
                            type: "warning",
                        });
                    }
                }
            );
        },
        queryPerformance() {
            this.$request.post(
                "Api/Users_Admin/QueryPerformance",
                {
                    token_admin: this.$utils.getloc("token_admin"),
                    userid_admin: this.$utils.getloc("userid_admin"),
                    uid: this.id,
                    sdate: this.staffPerformance.sdate,
                    edate: this.staffPerformance.edate,
                },
                (res) => {
                    if (res.data.code == 100) {
                        this.staffPerformance = res.data.data;
                    } else {
                        Notification({
                            message: "查询失败",
                            offset: 100,
                            type: "warning",
                        });
                    }
                }
            );
        },
        handleClick(tab, event) {
            let name = tab.name;
            let index = tab.index;
            let options = this.options;
            if (options[index].tryGetData) {
                if (name == "5") {
                    this.queryPerformance();
                } else if (name == "6") {
                    this.getUserBonusSourceData();
                }else if (name == "7") {
                    this.getUserBillData();
                }
                options[index].tryGetData = false;
            }
        },
        users_update: function () {
            // 修改会员资料
            
            if (this.users.usertel == "" || this.users.usertel == null) {
                Notification({
                    message: "请输入手机号码",
                    offset: 100,
                    type: "warning",
                });
                return;
            }
            this.$confirm("确定要修改吗?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning",
            })
                .then(() => {
                    var usersjson = JSON.stringify(this.users);
                    var ubanklistjson = JSON.stringify(this.users.ubanklist);
                    var uaddresslistjson = JSON.stringify(
                        this.users.uaddresslist
                    );

                    this.$request.post(
                        "Api/Users_Admin/Update",
                        {
                            token_admin: this.$utils.getloc("token_admin"),
                            userid_admin: this.$utils.getloc("userid_admin"),
                            users_json: usersjson,
                            beizhu: this.beizhu,
                            ubanklistjson: ubanklistjson,
                            uaddresslistjson: uaddresslistjson,
                        },
                        (res) => {
                            Notification({
                                title: "操作成功",
                                message: res.data.msg,
                                offset: 100,
                                type: "success",
                            });
                            this.getdata();
                        }
                    );
                })
                .catch(() => {});
        },
        updatepassword: function (lx) {
            this.$confirm("确定要修改吗?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning",
            })
                .then(() => {
                    let _newpassword = "";
                    if (lx == 1) {
                        _newpassword = this.password;
                    } else if (lx == 2) {
                        _newpassword = this.password2;
                    }
                    this.$request.post(
                        "Api/Users_Admin/Update_Password",
                        {
                            token_admin: this.$utils.getloc("token_admin"),
                            userid_admin: this.$utils.getloc("userid_admin"),
                            select_id: this.id,
                            lx: lx,
                            password: _newpassword,
                        },
                        (res) => {
                            Notification({
                                title: "操作成功",
                                message: res.data.msg,
                                offset: 100,
                                type: "success",
                            });
                        }
                    );
                })
                .catch(() => {});
        },
        updatereman: function () {
            this.$confirm("确定要修改吗?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning",
            })
                .then(() => {
                    // 修改推荐人
                    this.$request.post(
                        "Api/Users_Admin/Update_Rename",
                        {
                            token_admin: this.$utils.getloc("token_admin"),
                            userid_admin: this.$utils.getloc("userid_admin"),
                            select_id: this.id,
                            updaterename: this.users.rename,
                        },
                        (res) => {
                            Notification({
                                title: "操作成功",
                                message: res.data.msg,
                                offset: 100,
                                type: "success",
                            });
                        }
                    );
                })
                .catch(() => {});
        },
		updatehuashi: function () {
			var _this = this;
		    _this.$confirm("确定要修改吗?", "提示", {
		        confirmButtonText: "确定",
		        cancelButtonText: "取消",
		        type: "warning",
		    })
		        .then(() => {
		            // 修改推荐人
		            _this.$request.post(
		                "Api/Users_Admin/UpdateHuashi",
		                {
		                    token_admin: _this.$utils.getloc("token_admin"),
		                    userid_admin: _this.$utils.getloc("userid_admin"),
		                    select_id: _this.id,
		                    ul: _this.users.ulevel,
		                },
		                (res) => {
		                    Notification({
		                        title: "操作成功",
		                        message: res.data.msg,
		                        offset: 100,
		                        type: "success",
		                    });
							this.getdata()
		                }
		            );
		        })
		        .catch(() => {});
		},
        getwallet: function () {
            // 查询用户金额
            this.$request.post(
                "Api/Wallets_Admin/GetWallet",
                {
                    token_admin: this.$utils.getloc("token_admin"),
                    userid_admin: this.$utils.getloc("userid_admin"),
                    userid: this.users.userid,
                },
                (res) => {
                    this.wallet = res.data.data;
                }
            );
        },
        getdata: function () {
            // 查询用户
            this.$request.post(
                "Api/Users_Admin/Get",
                {
                    token_admin: this.$utils.getloc("token_admin"),
                    userid_admin: this.$utils.getloc("userid_admin"),
                    id: this.id,
                },
                (res) => {
                    this.users = res.data.data;
                    this.users.ubanklist = JSON.parse(this.users.ubanklist);
                    this.users.uaddresslist = JSON.parse(
                        this.users.uaddresslist
                    );
                    this.getulevel();
					this.getxlevel();
                    this.getwallet();
                }
            );
        },
        getulevel: function () {
            // 查询可选级别
            this.$request.post(
                "Api/Level/GetUlevelList",
                {},
                (res) => {
                    this.ulevellist = res.data.data;
                    this.ulevel = this.ulevellist[this.users.ulevel].name;
                },
                true
            );
        },
		getxlevel: function () {
		    // 查询可选级别
		    this.$request.post(
		        "Api/Level/GetXlevelList",
		        {},
		        (res) => {
		            this.xlevellist = res.data.data;
		            this.xlevel = this.xlevellist[this.users.xlevel].name;
		        },
		        true
		    );
		},
        ulevelChange: function (e) {
            // 修改级别
			console.log(e);
            this.users.ulevel = e;
        },
        xlevelChange: function (e) {
            // 修改级别
            this.users.xlevel = e;
        },
        getimg: function (img) {
            return this.$config.img_url + img;
        },
        // dateFormat(time) {
        // 	var date = new Date(time);
        // 	var Y = date.getFullYear() + "-";
        // 	var M = (date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1) + "-";
        // 	var D = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        // 	var hh = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
        // 	var mm = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
        // 	var ss = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();

        // 	var result = Y + M + D + " " + hh + ":" + mm + ":" + ss;

        // 	return result;
        // }
    },
};
</script>

<style scoped="scoped">
.el-input {
    max-width: 200px;
}

.fhbtn {
    color: white;
    text-decoration: none;
}

.form-menu {
    padding: 5px;
}

.form-menu-item {
    /* 	padding: 10px; */
}

.gap {
    padding: 5px;
}
</style>
