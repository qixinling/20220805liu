<template>
    <div id="app">
        <div class="login" v-if="!islogin">
            <router-view name="login" />
        </div>
        <div class="main" v-if="islogin">
            <div class="header">
                <div>
                    <img src="./assets/logo.png" />
                </div>
                <div>
                    <div class="headericonda">
                        <!-- <el-badge :value="12" class="item">
									<i class="el-icon-s-comment headericon"></i>
								</el-badge>
								<i class="el-icon-s-tools headericon"></i> -->
                        <i class="el-icon-switch-button headericon" @click="logout"></i>
                    </div>
                    <div class="headericonxiao">
                        <i class="el-icon-menu headericon" @click="qie"></i>
                        <i class="el-icon-switch-button headericon" @click="logout"></i>
                        <!-- <el-dropdown trigger="click">
									<span class="el-dropdown-link">
										<i class="el-icon-more headericon"></i>
									</span>
									<el-dropdown-menu slot="dropdown" :split-button="true">
										<el-dropdown-item icon="el-icon-s-comment">
											消息 <span style="background: red;color: #fff;padding-left:5px;padding-right:5px;font-size: 12px;border-radius: 3px;">12</span>
										</el-dropdown-item>
										<el-dropdown-item icon="el-icon-s-tools">设置</el-dropdown-item>
										<el-dropdown-item divided icon="el-icon-switch-button" command="logout">退出
										</el-dropdown-item>
									</el-dropdown-menu>
								</el-dropdown> -->
                    </div>
                </div>
            </div>
            <div id="left" :class="show ? 'left-show' : 'left'" ref="left">
                <el-menu default-active="Home" :router="true" :class="'menu' + show ? 'el-menu-vertical-demo' : 'menuclose'" :default-openeds="openeds">
                    <el-menu-item index="/" @click="openeds = []">
                        <i class="el-icon-s-home"></i>
                        <span slot="title">首页</span>
                    </el-menu-item>
                    <el-submenu index="2" v-if="permission('用户管理')">
                        <template slot="title">
                            <i class="el-icon-user-solid"></i>
                            <span>用户管理</span>
                        </template>
                        <!-- <el-menu-item index="Network" v-if="permissionChildren('Users_Admin/Network')">网络图谱
                        </el-menu-item> -->
                        <el-menu-item index="Tree" v-if="permissionChildren('Users_Admin/Tree')">推荐图谱
                        </el-menu-item>
                       <!-- <el-menu-item index="Jihuo" v-if="permissionChildren('Users_Admin/List')">临时会员
                        </el-menu-item> -->
						<el-menu-item index="UniversalUser" v-if="permissionChildren('Users_Admin/HsList')">画室长
						</el-menu-item>
                        <el-menu-item index="User" v-if="permissionChildren('Users_Admin/List')">会员列表
                        </el-menu-item>
						<el-menu-item index="Teachers" v-if="permissionChildren('Teachers_Admin/List')">画家列表
						</el-menu-item>
						<el-menu-item index="UlevelupRecord" v-if="permissionChildren('UsersLevelup_Admin/List')">画室长申请列表
						</el-menu-item>
					
                    </el-submenu>
                    <el-submenu index="3" v-if="permission('奖金管理')">
                        <template slot="title">
                            <i class="el-icon-s-data"></i>
                            <span>奖金管理</span>
                        </template>
                       <!-- <el-menu-item index="Jiesuan" v-if="permissionChildren('BonusJiesuan_Admin/List')">
                            分红结算</el-menu-item> -->
                        <el-menu-item index="BonusTime" v-if="permissionChildren('Bonus_Admin/Time_List')">
                            奖金总表</el-menu-item>
                        <el-menu-item index="BonusSource" v-if="permissionChildren('Bonus_Admin/Source_List')">奖金来源</el-menu-item>
                    </el-submenu>

                    <el-submenu index="4" v-if="permission('财务管理')">
                        <template slot="title">
                            <i class="el-icon-s-finance"></i>
                            <span>财务管理</span>
                        </template>
                        <!--<el-submenu index="4-1">
                            <template slot="title">货币管理</template>
                             <el-menu-item index="Coin" v-if="permissionChildren('WalletsCoin_Admin/List')">
                                货币管理</el-menu-item>
                            <el-menu-item index="ChongzhiSelect" v-if="
                  permissionChildren(
                    'WalletsChongzhi_Select_Admin/List'
                  )
                ">充值货币</el-menu-item>
                            <el-menu-item index="TixianSelect" v-if="
                  permissionChildren(
                    'WalletsTixian_Select_Admin/List'
                  )
                ">提现货币</el-menu-item>
                            <el-menu-item index="ZhuanzhangSelect" v-if="
                  permissionChildren(
                    'WalletsZhuanzhang_Select_Admin/List'
                  )
                ">转账货币</el-menu-item>
                            <el-menu-item index="ZhuanhuanSelect" v-if="
                  permissionChildren(
                    'WalletsZhuanhuan_Select_Admin/List'
                  )
                ">转换货币</el-menu-item>
                        </el-submenu> -->
						<el-menu-item index="TixianSelect" v-if="
						  permissionChildren(
						    'WalletsTixian_Select_Admin/List'
						  )
						">提现货币</el-menu-item>
                        <el-menu-item index="ZengjianSelect" v-if="permissionChildren('WalletsZengjian_Admin/List')">货币增减</el-menu-item>
                        <!-- 	<el-menu-item index="Zengjian" v-if="permissionChildren('Zengjian_Admin/List')">货币增减</el-menu-item> -->
                      <!--  <el-menu-item index="Chongzhi" v-if="permissionChildren('WalletsChongzhi_Admin/List')">充值管理</el-menu-item> -->
                        <el-menu-item index="Tixian" v-if="permissionChildren('WalletsTixian_Admin/List')">
                            提现管理</el-menu-item>
                       <!-- <el-menu-item index="Zhuanzhang" v-if="
                permissionChildren('WalletsZhuanzhang_Admin/List')
              ">转账管理</el-menu-item> -->
                        <!-- <el-menu-item index="Zhuanhuan" v-if="permissionChildren('WalletsZhuanhuan_Admin/List')">转换管理</el-menu-item> -->
                    </el-submenu>

                    <el-submenu index="5" v-if="permission('竞拍管理')">
                        <template slot="title">
                            <i class="el-icon-s-goods"></i>
                            <span>竞拍管理</span>
                        </template>
                        <el-menu-item index="SiteList" v-if="permissionChildren('Site_Admin/List')">时间列表</el-menu-item>
						<el-menu-item index="PricerangeList" v-if="permissionChildren('Pricerange_Admin/List')">价格区间</el-menu-item>
						<el-menu-item index="JewelleryList" v-if="permissionChildren('Jewellery_Admin/List')">产品列表</el-menu-item>
						<el-menu-item index="AddUserJewellery" v-if="permissionChildren('UsersHold_Admin/Add')">添加卖方</el-menu-item>
						<el-menu-item index="BuySellList" v-if="permissionChildren('UsersHold_Admin/SellList')">竞拍大厅</el-menu-item>
						<el-menu-item index="PipeiList" v-if="permissionChildren('UsersHold_Admin/SellList')">交易中列表</el-menu-item>
						<el-menu-item index="EndList" v-if="permissionChildren('UsersHold_Admin/SellList')">交易结束列表</el-menu-item>
						<!-- <el-menu-item index="ShensuList" v-if="permissionChildren('UsersHold_Admin/ShensuList')">申诉列表</el-menu-item> -->
						<!-- <el-menu-item index="JiaogeList" v-if="permissionChildren('ShopOrder_Admin/List')">自由交割订单</el-menu-item>
						<el-menu-item index="QjiaogeList" v-if="permissionChildren('ShopOrder_Admin/List')">强制交割订单</el-menu-item> -->
						<!-- <el-menu-item index="ChaifenList" v-if="permissionChildren('UsersHold_Admin/CaidanList')">拆单列表</el-menu-item> -->
                    </el-submenu>

                    <!-- <el-submenu index="6" v-if="permission('商城管理')">
                        <template slot="title">
                            <i class="el-icon-s-goods"></i>
                            <span>商城管理</span>
                        </template> -->
						<!-- <el-menu-item index="ConsList" v-if="permissionChildren('Constellation_Admin/List')">星座列表</el-menu-item>
						<el-menu-item index="YuyueList" v-if="permissionChildren('UsersYuyue_Admin/BuyList')">预约列表</el-menu-item>
						<el-menu-item index="BuySellList"  v-if="permissionChildren('UsersHold_Admin/ManualMatch')">匹配</el-menu-item>
						<el-menu-item index="PipeiList"  v-if="permissionChildren('UsersHold_Admin/SellList')">匹配中</el-menu-item>
						<el-menu-item index="FenhongList" v-if="permissionChildren('UsersHold_Admin/SellList')">分红列表</el-menu-item>
						<el-menu-item index="JieguoList" v-if="permissionChildren('UsersHold_Admin/JieguoList')">交易结果</el-menu-item>
						<el-menu-item index="Conssong" >卖方添加星座</el-menu-item> -->
                       
                      <!-- <el-menu-item index="ShopimgList" v-if="permissionChildren('ShopImg_Admin/List')">商城图片
                        </el-menu-item>
                        <el-menu-item index="SortManagement" v-if="permissionChildren('ShopGoodsSort_Admin/List')">分类管理</el-menu-item>
                         <el-menu-item index="GoodsList" v-if="permissionChildren('ShopGoods_Admin/List')">商品列表
                        </el-menu-item>
                        <el-menu-item index="OrderList" v-if="permissionChildren('ShopOrder_Admin/List')">商城订单列表
                        </el-menu-item>
						<el-menu-item index="JorderList" v-if="permissionChildren('ShopOrder_Admin/List')">激活订单列表
						</el-menu-item>
                    </el-submenu> -->

                    <el-submenu index="7" v-if="permission('信息管理')">
                        <template slot="title">
                            <i class="el-icon-s-order"></i>
                            <span>信息管理</span>
                        </template>
						<el-menu-item index="Slide" v-if="permissionChildren('Slide_Admin/List')">幻灯片
						</el-menu-item>
                        <el-menu-item index="Message" v-if="permissionChildren('Msg_Admin/List_Chating')">客服消息
                        </el-menu-item>
                        <el-menu-item index="MembersHelp" v-if="permissionChildren('Article_Admin/Get')">会员帮助
                        </el-menu-item>
                        <el-menu-item index="AutoReturnList" v-if="permissionChildren('Help_Admin/Question_List')">常见问题</el-menu-item>
                        <el-menu-item index="News" v-if="permissionChildren('News_Admin/List')">新闻管理
                        </el-menu-item>
                        <el-menu-item index="Achievement" v-if="
                permissionChildren('SystemAchievement_Admin/List')
              ">业绩统计</el-menu-item>
                        <el-menu-item index="LogLogin" v-if="
                permissionChildren('SystemLogLogin_Admin/Login_List')
              ">登录日志</el-menu-item>
                        <el-menu-item index="LogOperation" v-if="
                permissionChildren(
                  'SystemLogOperation_Admin/Operation_List'
                )
              ">操作日志</el-menu-item>
                        <!-- <el-menu-item index="LogAbnormity" v-if="permissionChildren('Log_Admin/Error_List')">异常日志</el-menu-item> -->
                    </el-submenu>

                    <el-submenu index="8" v-if="permission('权限管理')">
                        <template slot="title">
                            <i class="el-icon-s-check"></i>
                            <span>权限管理</span>
                        </template>
                        <el-menu-item index="AdministratorList">管理员列表</el-menu-item>
                        <el-menu-item index="Permission">职位列表</el-menu-item>
                    </el-submenu>

                    <el-submenu index="9" v-if="permission('系统管理')">
                        <template slot="title">
                            <i class="el-icon-s-tools"></i>
                            <span>系统管理</span>
                        </template>
                        <el-menu-item index="SystemParameter">系统参数</el-menu-item>
                        <el-menu-item index="BonusParameter">奖金参数</el-menu-item>
                    </el-submenu>

                    <el-menu-item index="Database" v-if="permission('数据管理')">
                        <i class="el-icon-upload"></i>
                        <span>数据管理</span>
                    </el-menu-item>

                    <!-- 		<el-menu-item index="User_afei">
						<i class="el-icon-upload"></i>
						<span>测试分页</span>
					</el-menu-item>
					<el-submenu>
						<template slot="title">
							<i class="el-icon-s-tools"></i>
							<span>测试</span>
						</template>
						<el-menu-item>
							<div ref="acp" @mouseover="mouseOver" @mouseleave="mouseLeave" :style="active">测试变色龙</div>
						</el-menu-item>
						<el-menu-item>测试</el-menu-item>
					</el-submenu> -->
                </el-menu>
            </div>
            <div class="content">
                <div id="roof"></div>
                <router-view />
                <!-- <div style="height: 200px; width: 100%"></div> -->
                <div id="bottom"></div>
            </div>
            <div class="footer">
                <div class="footer-container">
                    <div>Copyright © 2015-2020 版权所有</div>
                    <div class="div_i_box">
                        <i style="font-size: 24px" class="el-icon-caret-top" @click="dingbu" />
                        <i style="font-size: 24px" class="el-icon-caret-bottom" @click="dibu" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Vue from "vue";
import {
    Menu,
    Submenu,
    MenuItem,
    MenuItemGroup,
    Row,
    Col,
    Option,
    Dropdown,
    DropdownMenu,
    DropdownItem,
    Badge,
    Button,
    MessageBox,
    Radio,
    RadioGroup,
    RadioButton,
} from "element-ui";
Vue.use(Option);
Vue.use(Menu);
Vue.use(Submenu);
Vue.use(MenuItem);
Vue.use(MenuItemGroup);
Vue.use(Row);
Vue.use(Col);
Vue.use(Dropdown);
Vue.use(DropdownMenu);
Vue.use(DropdownItem);
Vue.use(Badge);
Vue.use(Button);
Vue.use(Radio);
Vue.use(RadioGroup);
Vue.use(RadioButton);
Vue.prototype.$prompt = MessageBox.prompt;
Vue.prototype.$confirm = MessageBox.confirm;
export default {
    name: "index",
    data() {
        return {
            islogin: false,
            show: false,
            active: "",
            openeds: [],
        };
    },
    created() {
        //在页面加载时读取sessionStorage里的状态信息
        if (this.$utils.getloc("store")) {
            this.$store.replaceState(
                Object.assign(
                    {},
                    this.$store.state,
                    JSON.parse(this.$utils.getloc("store"))
                )
            );
        }
        //在页面刷新或关闭时将vuex里的信息保存到sessionStorage里
        //IOS浏览器没有beforeunload，需要使用pagehide
        var isiOS = !!navigator.userAgent.match(
            /\(i[^;]+;( U;)? CPU.+Mac OS X/
        ); //ios终端
        let _action = "beforeunload";
        if (isiOS) {
            _action = "pagehide";
        }
        window.addEventListener(_action, () => {
            this.$utils.setloc("store", JSON.stringify(this.$store.state));
        });
        this.islogin = this.$store.state.LoginState;
        if (this.$utils.GetQueryString("action") == "out") {
            this.out();
        }
    },
    methods: {
        dingbu: function () {
            document.getElementById("roof").scrollIntoView();
        },
        dibu: function () {
            document.getElementById("bottom").scrollIntoView(true);
        },
        // 移入
        mouseOver() {
            this.active = "background-color:red"; // 背景颜色
            // 操作dom 获取p标签改变其样式
            // var acps = this.$refs.acp;
            // acps.style.color = "red";  // 字体颜色
        },
        // 移出
        mouseLeave() {
            this.active = "";
            this.$refs.acp.style = "";
        },
        qie: function () {
            this.show = !this.show;
        },
        logout: function () {
            if (confirm("确定要退出吗?")) {
                this.out();
            }
        },
        out: function () {
            this.$utils.setloc("token_admin", "");
            this.$utils.setloc("id_admin", "");
            this.$utils.setloc("userid_admin", "");
            this.$utils.setloc("permission", "");
            this.$store.commit("setLoginState", false);
            window.location.href = "/admin/";
        },
        permission: function (name) {
            let _pass = false;
            if (this.$utils.getloc("id_admin")) {
                if (this.$utils.getloc("id_admin") == 1) {
                    return true;
                } else {
                    if (this.$utils.getloc("permission")) {
                      
						//console.log(this.$utils.getloc("permission"));
						 let _permission =JSON.parse(this.$utils.getloc("permission")) 
						 
                        _permission.forEach((item, index) => {
                            if (item.Name == name) {
                                _pass = item.Display;
                            }
                        });
                    }
                }
            }
            return _pass;
        },
        permissionChildren: function (path) {
            let _pass = false;
            if (this.$utils.getloc("id_admin")) {
                if (this.$utils.getloc("id_admin") == 1) {
                    return true;
                } else {
                    if (this.$utils.getloc("permission")) {
                        let _permission = JSON.parse(
                            this.$utils.getloc("permission")
                        );
                        _permission.forEach((item, index) => {
                            item.Pclist.forEach((item2, index2) => {
                                item2.forEach((item3, index3) => {
                                    if (item3.Path == path) {
                                        _pass = item3.Display;
                                    }

                                    // if (item3.Path.search(path) != -1) {
                                    // 	_pass = item3.Display;
                                    // }
                                });
                            });
                        });
                    }
                }
            }
            return _pass;
        },
    },
};
</script>

<style lang="less">
html,
body,
#app {
    height: 100%;
}

body {
    margin: 0;
}

body .el-table th.gutter {
	display: table-cell !important;
}

#app {
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    color: #2c3e50;
    letter-spacing: 0.5px;
}

.shadow {
    -webkit-box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
    -moz-box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
    box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
}

.cardbox {
    background: #fff;
    border-radius: 5px;
    padding: 10px;
    box-shadow: 0px 5px 10px rgba(0, 0, 0, 0.1);
}

.cardbox .cardcontent {
    padding: 10px;
}

.search-col {
    margin-bottom: 10px;
}

.header {
    text-align: left;
    position: fixed;
    padding: 10px;
    top: 0;
    left: 0;
    right: 0;
    height: 50px;
    z-index: 998;
    background: #f5f5f5;
    -webkit-box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
    -moz-box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
    box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);

    display: flex;
    justify-content: space-between;
    align-items: center;
}

.header .headericon {
    font-size: 30px;
    margin-left: 10px;
    margin-right: 10px;
}

.headericonda {
    display: inherit;
}

.el-badge__content.is-fixed {
    right: 25px;
}

.headericonxiao {
    display: none;
}

#left {
    position: fixed;
    top: 70px;
    left: 0;
    width: 250px;
    height: 100%;
    background: #ffffff;
    overflow-y: auto;
}

.menu {
    margin-bottom: 100px;
}

.el-menu {
    border-right: none;
    text-align: left;
}

// 所有展开的主菜单样式
.is-opened .el-submenu__title,
.is-opened i {
    background: #636e72;
    color: #ffffff;
    // background: red;
}

// 点击打开主菜单时的主菜单样式,鼠标滑动到打开的主菜单的样式,子菜单样式不受影响
.is-opened .el-submenu__title:focus,
.is-opened .el-submenu__title:hover {
    //background: #dfe6e9;
    background: #636e72;
    //background: yellow;
}

// 鼠标滑动到子菜单时候的背景色   #ecf5ff

// 测试变色
.demo {
    background-color: lightcoral;
}

// 点击关闭主菜单时的主菜单样式，鼠标滑动到关闭着的菜单上时的菜单样式
.el-submenu__title:focus,
.el-submenu__title:hover {
    background-color: #b2bec3;
    //background: blue;
}

// 选中的子菜单样式
.el-menu-item.is-active {
    background: #ecf5ff;
}

.content {
    position: absolute;
    top: 70px;
    left: 250px;
    right: 0;
    height: calc(100% - 140px);
    background: #e5e5e5;
    overflow-y: auto;
    overflow-x: auto;
    padding: 20px 40px 50px 40px;
}

.content::-webkit-scrollbar {
    /*滚动条整体样式*/
    width: 5px;
    /*高宽分别对应横竖滚动条的尺寸*/
    height: 1px;
}

.content::-webkit-scrollbar-thumb {
    /*滚动条里面小方块*/
    border-radius: 5px;
    -webkit-box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.2);
    background: #9c9c9c;
}

.content::-webkit-scrollbar-track {
    /*滚动条里面轨道*/
    -webkit-box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.2);
    border-radius: 10px;
    background: #ededed;
}

.footer {
    text-align: center;
    font-size: 12px;
    color: #888;
    position: fixed;
    left: 250px;
    right: 0;
    bottom: 0;
    height: 50px;
    line-height: 50px;
    background: #e5e5e5;
    z-index: 99;
}

@media screen and (max-width: 900px) {
    .headericonda {
        display: none;
    }

    .headericonxiao {
        display: inherit;
    }

    .left {
        display: none;
    }

    .left-show {
        display: inline;
        z-index: 999;
    }

    .content {
        left: 0;
        padding: 20px 20px 50px 20px;
    }

    .menuclose {
        display: none;
    }

    .footer {
        left: 0;
        height: 50px;
        line-height: 50px;
    }
}

.footer-container {
    position: relative;
}

.div_i_box {
    padding: 10px 5px;
    width: 30px;
    height: 30px;
    position: absolute;
    right: 0;
    bottom: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    box-sizing: content-box;
}
</style>