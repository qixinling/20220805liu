import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

const routes = [{
		path: "/",
		name: "Start",
		component: resolve => require(["@/views/Start"], resolve),
		meta: {
			title: "启动"
		}
	},
	{
		path: "/Home",
		name: "Home",
		component: resolve => require(["@/views/Home"], resolve),
		meta: {
			title: "首页"
		}
	},
	
	{
		path: "/ShopIndex",
		name: "ShopIndex",
		component: resolve => require(["@/views/shop/ShopIndex"], resolve),
		meta: {
			title: "商城"
		}
	},
	{
		path: "/Login",
		name: "Login",
		component: resolve => require(["@/components/Login"], resolve),
		meta: {
			title: "登录"
		}
	},

	{
		path: "/Bill",
		name: "Bill",
		component: resolve => require(["@/views/wallet/Bill"], resolve),
		meta: {
			title: "账单"
		}
	},
	{
		path: "/Qrcode",
		name: "Qrcode",
		component: resolve => require(["@/views/Qrcode"], resolve),
		meta: {
			title: "分享"
		}
	},
	{
		path: "/Jihuo",
		name: "Jihuo",
		component: resolve => require(["@/views/user/Jihuo"], resolve),
		meta: {
			title: "激活"
		}
	},
	{
		path: "/Attestation",
		name: "Attestation",
		component: resolve => require(["@/views/user/Attestation"], resolve),
		meta: {
			title: "实名认证"
		}
	},
	{
		path: "/Tree",
		name: "Tree",
		component: resolve => require(["@/views/user/Tree"], resolve),
		meta: {
			title: "我的粉丝"
		}
	},
	{
		path: "/Network",
		name: "Network",
		component: resolve => require(["@/views/user/Network"], resolve),
		meta: {
			title: "网络图谱"
		}
	},
	{
		path: "/User",
		name: "User",
		component: resolve => require(["@/views/user/User"], resolve),
		meta: {
			title: "我的"
		}
	},
	{
		path: "/Wallet",
		name: "Wallet",
		component: resolve => require(["@/views/wallet/Wallet"], resolve),
		meta: {
			title: "钱包"
		}
	},
	{
		path: "/Chongzhi",
		name: "Chongzhi",
		component: resolve => require(["@/views/wallet/Chongzhi"], resolve),
		meta: {
			title: "充值"
		}
	},
	{
		path: "/Chongzhirecord",
		name: "Chongzhirecord",
		component: resolve => require(["@/views/wallet/Chongzhirecord"], resolve),
		meta: {
			title: "充值记录"
		}
	},
	{
		path: "/Zhuanhuan",
		name: "Zhuanhuan",
		component: resolve => require(["@/views/wallet/Zhuanhuan"], resolve),
		meta: {
			title: "转换"
		}
	},

	{
		path: "/Tixian",
		name: "Tixian",
		component: resolve => require(["@/views/wallet/Tixian"], resolve),
		meta: {
			title: "提现"
		}
	},
	{
		path: "/Tixianrecord",
		name: "Tixianrecord",
		component: resolve => require(["@/views/wallet/Tixianrecord"], resolve),
		meta: {
			title: "提现记录"
		}
	},
	{
		path: "/Zhuanzhang",
		name: "Zhuanzhang",
		component: resolve => require(["@/views/wallet/Zhuanzhang"], resolve),
		meta: {
			title: "转账"
		}
	},
	{
		path: "/Zhuanzhangrecord",
		name: "Zhuanzhangrecord",
		component: resolve => require(["@/views/wallet/Zhuanzhangrecord"], resolve),
		meta: {
			title: "转账记录"
		}
	},
	{
		path: "/Order",
		name: "Order",
		component: resolve => require(["@/views/shop/Order"], resolve),
		meta: {
			title: "商城订单"
		}
	},
	{
		path: "/OrderContent",
		name: "OrderContent",
		component: resolve => require(["@/views/shop/OrderContent"], resolve),
		meta: {
			title: "订单详情"
		}
	},
	{
		path: "/Goods",
		name: "Goods",
		component: resolve => require(["@/views/shop/Goods"], resolve),
		meta: {
			title: "商品详情"
		}
	},
	{
		path: "/ShopCar",
		name: "ShopCar",
		component: resolve => require(["@/views/shop/ShopCar"], resolve),
		meta: {
			title: "购物车"
		}
	},
	{
		path: "/Register",
		name: "Register",
		component: resolve => require(["@/views/user/Register"], resolve),
		meta: {
			title: "注册"
		}
	},
	{
		path: "/News",
		name: "News",
		component: resolve => require(["@/views/News"], resolve),
		meta: {
			title: "新闻公告"
		}
	},
	{
		path: "/NewsContent",
		name: "NewsContent",
		component: resolve => require(["@/views/NewsContent"], resolve),
		meta: {
			title: "新闻内容"
		}
	},
	{
		path: "/Bank",
		name: "Bank",
		component: resolve => require(["@/views/bank/Bank"], resolve),
		meta: {
			title: "账户"
		}
	},
	{
		path: "/BankAdd",
		name: "BankAdd",
		component: resolve => require(["@/views/bank/BankAdd"], resolve),
		meta: {
			title: "添加账户"
		}
	},
	{
		path: "/Address",
		name: "Address",
		component: resolve => require(["@/views/address/Address"], resolve),
		meta: {
			title: "地址"
		}
	},
	{
		path: "/AddressAdd",
		name: "AddressAdd",
		component: resolve => require(["@/views/address/AddressAdd"], resolve),
		meta: {
			title: "添加地址"
		}
	},
	{
		path: "/Bouns",
		name: "Bouns",
		component: resolve => require(["@/views/wallet/Bouns"], resolve),
		meta: {
			title: "收益"
		}
	},
	{
		path: "/Bounssource",
		name: "Bounssource",
		component: resolve => require(["@/views/wallet/Bounssource"], resolve),
		meta: {
			title: "明细"
		}
	},
	{
		path: "/Msg",
		name: "Msg",
		component: resolve => require(["@/views/msg/Msg"], resolve),
		meta: {
			title: "消息"
		}
	},
	{
		path: "/ShopClass",
		name: "ShopClass",
		component: resolve => require(["@/views/shop/ShopClass"], resolve),
		meta: {
			title: "分类"
		}
	},
	{
		path: "/UserInfo",
		name: "UserInfo",
		component: resolve => require(["@/views/user/UserInfo"], resolve),
		meta: {
			title: "修改资料"
		}
	},
	{
		path: "/Kongzhi",
		name: "Kongzhi",
		component: resolve => require(["@/views/user/Kongzhi"], resolve),
		meta: {
			title: "画室控制台"
		}
	},
	{
		path: "/Fenxiao",
		name: "Fenxiao",
		component: resolve => require(["@/views/user/Fenxiao"], resolve),
		meta: {
			title: "分销中心"
		}
	},
	{
		path: "/AddressUpdate",
		name: "AddressUpdate",
		component: resolve => require(["@/views/address/AddressUpdate"], resolve),
		meta: {
			title: "修改地址"
		}
	},
	{
		path: "/BankUpdate",
		name: "BankUpdate",
		component: resolve => require(["@/views/bank/BankUpdate"], resolve),
		meta: {
			title: "修改账户"
		}
	},
	{
		path: "/GoodsSearch",
		name: "GoodsSearch",
		component: resolve => require(["@/views/shop/GoodsSearch"], resolve),
		meta: {
			title: "搜索"
		}
	},
	{
		path: "/Password",
		name: "Password",
		component: resolve => require(["@/views/user/Password"], resolve),
		meta: {
			title: "密码"
		}
	},
	{
		path: "/GoodsAddCar",
		name: "GoodsAddCar",
		component: resolve => require(["@/components/GoodsAddCar"], resolve),
		meta: {
			title: "添加商品"
		}
	},
	{
		path: "/BillContent",
		name: "BillContent",
		component: resolve => require(["@/views/wallet/BillContent"], resolve),
		meta: {
			title: "账单详情"
		}
	},
	{
		path: "/UserLevelup",
		name: "UserLevelup",
		component: resolve => require(["@/views/user/UserLevelup"], resolve),
		meta: {
			title: "升级"
		}
	},
	{
		path: "/UserLevelGoods",
		name: "UserLevelGoods",
		component: resolve => require(["@/views/user/UserLevelGoods"], resolve),
		meta: {
			title: "选择商品"
		}
	},
	{
		path: "/PasswordRetrieve",
		name: "PasswordRetrieve",
		component: resolve => require(["@/views/user/PasswordRetrieve"], resolve),
		meta: {
			title: "找回密码"
		}
	},
	{
		path: "/FwzxAdd",
		name: "FwzxAdd",
		component: resolve => require(["@/views/fwzx/FwzxAdd"], resolve),
		meta: {
			title: "申请服务中心"
		}
	},
	{
		path: "/Question",
		name: "Question",
		component: resolve => require(["@/views/Question"], resolve),
		meta: {
			title: "常见问题"
		}
	},
	{
		path: "/Record",
		name: "Record",
		component: resolve => require(["@/views/wallet/Record"], resolve),
		meta: {
			title: ""
		}
	},
	{
		path: "/Lingyang",
		name: "Lingyang",
		component: resolve => require(["@/views/record/Lingyang"], resolve),
		meta: {
			title: "竞拍记录"
		}
	},
	{
		path: "/Zhuanrang",
		name: "Zhuanrang",
		component: resolve => require(["@/views/record/Zhuanrang"], resolve),
		meta: {
			title: "转让记录"
		}
	},
	// {
	// 	path: "/Yuyue",
	// 	name: "Yuyue",
	// 	component: resolve => require(["@/views/record/Yuyue"], resolve),
	// 	meta: {
	// 		title: "预约记录"
	// 	}
	// },
	{
		path: "/Buy",
		name: "Buy",
		component: resolve => require(["@/views/wallet/Buy"], resolve),
		meta: {
			title: "购买星座"
		}
	},
	{
		path: "/Shengji",
		name: "Shengji",
		component: resolve => require(["@/views/auction/Shengji"], resolve),
		meta: {
			title: "申请画室"
		}
	},
	{
		path: "/Details",
		name: "Details",
		component: resolve => require(["@/views/auction/Details"], resolve),
		meta: {
			title: "详情"
		}
	},
	{
		path: "/Dakuan",
		name: "Dakuan",
		component: resolve => require(["@/views/auction/Dakuan"], resolve),
		meta: {
			title: "打款"
		}
	},
	{
		path: "/Shoukuan",
		name: "Shoukuan",
		component: resolve => require(["@/views/auction/Shoukuan"], resolve),
		meta: {
			title: "收款"
		}
	},
	{
		path: "/TgOrder",
		name: "TgOrder",
		component: resolve => require(["@/views/auction/TgOrder"], resolve),
		meta: {
			title: "推广订单"
		}
	},
	{
		path: "/Zhuanhua",
		name: "Zhuanhua",
		component: resolve => require(["@/views/auction/Zhuanhua"], resolve),
		meta: {
			title: "转画管理"
		}
	},
	{
		path: "/HsInfo",
		name: "HsInfo",
		component: resolve => require(["@/views/auction/HsInfo"], resolve),
		meta: {
			title: "设置登船编码"
		}
	},
	{
		path: "/TixianPass",
		name: "TixianPass",
		component: resolve => require(["@/views/auction/TixianPass"], resolve),
		meta: {
			title: "提现审核"
		}
	},
	{
		path: "/Shangjia",
		name: "Shangjia",
		component: resolve => require(["@/views/auction/Shangjia"], resolve),
		meta: {
			title: "上架审核"
		}
	},
	{
		path: "/Fenqu",
		name: "Fenqu",
		component: resolve => require(["@/views/auction/Fenqu"], resolve),
		meta: {
			title: ""
		}
	},
	{
		path: "/QiangPaiQu",
		name: "QiangPaiQu",
		component: resolve => require(["@/views/auction/QiangPaiQu"], resolve),
		meta: {
			title: ""
		}
	},
	{
		path: "/Mygoods",
		name: "Mygoods",
		component: resolve => require(["@/views/auction/Mygoods"], resolve),
		meta: {
			title: ""
		}
	},
	{
		path: "/Mygoods2",
		name: "Mygoods2",
		component: resolve => require(["@/views/auction/Mygoods2"], resolve),
		meta: {
			title: "我的字画"
		}
	},
	{
		path: "/Tihuo",
		name: "Tihuo",
		component: resolve => require(["@/views/auction/Tihuo"], resolve),
		meta: {
			title: "提货订单"
		}
	},
	{
		path: "/Safe",
		name: "Safe",
		component: resolve => require(["@/views/user/Safe"], resolve),
		meta: {
			title: "安全中心"
		}
	},
	{
		path: "/TeachersDetails",
		name: "TeachersDetails",
		component: resolve => require(["@/views/TeachersDetails"], resolve),
		meta: {
			title: "画家资料"
		}
	},
	{
		path: "/Tongji",
		name: "Tongji",
		component: resolve => require(["@/views/auction/Tongji"], resolve),
		meta: {
			title: "画室统计"
		}
	},
	{
		path: "/Zihua",
		name: "Zihua",
		component: resolve => require(["@/views/auction/Zihua"], resolve),
		meta: {
			title: "字画管理"
		}
	},
	{
		path: "/Huiyuan",
		name: "Huiyuan",
		component: resolve => require(["@/views/auction/Huiyuan"], resolve),
		meta: {
			title: "画室成员"
		}
	},
	{
		path: "/YuyueRecord",
		name: "YuyueRecord",
		component: resolve => require(["@/views/auction/YuyueRecord"], resolve),
		meta: {
			title: "预约记录"
		}
	},
	{
		path: "/HuashiYuyue",
		name: "HuashiYuyue",
		component: resolve => require(["@/views/auction/HuashiYuyue"], resolve),
		meta: {
			title: "画室预约"
		}
	},
	{
		path: "/Yuyue",
		name: "Yuyue",
		component: resolve => require(["@/views/auction/Yuyue"], resolve),
		meta: {
			title: "预约"
		}
	},
	{
		path: "/OrderDetails",
		name: "OrderDetails",
		component: resolve => require(["@/views/auction/OrderDetails"], resolve),
		meta: {
			title: "订单详情"
		}
	}
];

//解决重复点击报错问题
const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(location) {
	return originalPush.call(this, location).catch(err => err)
}

const router = new VueRouter({
	/* mode: "history",
	base: process.env.BASE_URL, */
	routes
});

export default router;
