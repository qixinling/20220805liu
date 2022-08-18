import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
	{
		path: '/',
		components:{
        default: resolve => (require(["@/views/Home"], resolve)),
        login:resolve => (require(["@/views/Login"], resolve))
      }
	},
	{
		path: '/Home',
		components:{
	      default: resolve => (require(["@/views/Home"], resolve)),
	      login:resolve => (require(["@/views/Login"], resolve))
	    }
	},
	{
		path: '/Teachers',
		name: 'Teachers',
		component: resolve => (require(["@/views/teachers/Teachers"], resolve))
	},
	{
		path: '/TeachersAdd',
		name: 'TeachersAdd',
		component: resolve => (require(["@/views/teachers/TeachersAdd"], resolve))
	},
	{
		path: '/TeachersUpdate',
		name: 'TeachersUpdate',
		component: resolve => (require(["@/views/teachers/TeachersUpdate"], resolve))
	},
	{
		path: '/Teachers',
		name: 'Teachers',
		component: resolve => (require(["@/views/teachers/Teachers"], resolve))
	},
	{
		path: '/QjiaogeList',
		name: 'QjiaogeList',
		component: resolve => (require(["@/views/auction/QjiaogeList"], resolve))
	},
	{
		path: '/EndList',
		name: 'EndList',
		component: resolve => (require(["@/views/auction/EndList"], resolve))
	},
	{
		path: '/ChaifenList',
		name: 'ChaifenList',
		component: resolve => (require(["@/views/auction/ChaifenList"], resolve))
	},
	{
		path: '/ShensuList',
		name: 'ShensuList',
		component: resolve => (require(["@/views/auction/ShensuList"], resolve))
	},
	{
		path: '/PricerangeList',
		name: 'PricerangeList',
		component: resolve => (require(["@/views/auction/PricerangeList"], resolve))
	},
	{
		path: '/PricerangeAdd',
		name: 'PricerangeAdd',
		component: resolve => (require(["@/views/auction/PricerangeAdd"], resolve))
	},
	{
		path: '/JiaogeList',
		name: 'JiaogeList',
		component: resolve => (require(["@/views/auction/JiaogeList"], resolve))
	},
	{
		path: '/PipeiList',
		name: 'PipeiList',
		component: resolve => (require(["@/views/auction/PipeiList"], resolve))
	},
	{
		path: '/BuySellList',
		name: 'BuySellList',
		component: resolve => (require(["@/views/auction/BuySellList"], resolve))
	},
	{
		path: '/SiteList',
		name: 'SiteList',
		component: resolve => (require(["@/views/auction/SiteList"], resolve))
	},
	{
		path: '/SiteAdd',
		name: 'SiteAdd',
		component: resolve => (require(["@/views/auction/SiteAdd"], resolve))
	},
	{
		path: '/JewelleryList',
		name: 'JewelleryList',
		component: resolve => (require(["@/views/auction/JewelleryList"], resolve))
	},
	{
		path: '/JewelleryUpdate',
		name: 'JewelleryUpdate',
		component: resolve => (require(["@/views/auction/JewelleryUpdate"], resolve))
	},
	{
		path: '/JewelleryAdd',
		name: 'JewelleryAdd',
		component: resolve => (require(["@/views/auction/JewelleryAdd"], resolve))
	},
	{
		path: '/AddUserJewellery',
		name: 'AddUserJewellery',
		component: resolve => (require(["@/views/auction/AddUserJewellery"], resolve))
	},
	{
		path: '/UniversalUser',
		name: 'UniversalUser',
	  component: resolve => (require(["@/views/users/UniversalUser"], resolve))
	}, 
	{
		path: '/Renzheng',
		name: 'Renzheng',
	  component: resolve => (require(["@/views/users/Renzheng"], resolve))
	}, 
	{
		path: '/Jihuo',
		name: 'Jihuo',
	  component: resolve => (require(["@/views/users/Jihuo"], resolve))
	}, 
	{
		path: '/Slide',
		name: 'Slide',
		component: resolve => (require(["@/views/shop/Slide"], resolve))
	},
	{
		path: '/SlideUpdate',
		name: 'SlideUpdate',
		component: resolve => (require(["@/views/shop/SlideUpdate"], resolve))
	},
	{
		path: '/SlideAdd',
		name: 'SlideAdd',
		component: resolve => (require(["@/views/shop/SlideAdd"], resolve))
	},
	{
		path: '/UserEdit',
		name: 'UserEdit',
		 component: resolve => (require(["@/views/users/UserEdit"], resolve))
	},
	
	{
		path: '/PipeiList',
		name: 'PipeiList',
		component: resolve => (require(["@/views/shop/PipeiList"], resolve))
	},
	{
		path: '/FenhongList',
		name: 'FenhongList',
		component: resolve => (require(["@/views/shop/FenhongList"], resolve))
	},
	{
		path: '/JieguoList',
		name: 'JieguoList',
		component: resolve => (require(["@/views/shop/JieguoList"], resolve))
	},
	{
		path: '/YuyueList',
		name: 'YuyueList',
		component: resolve => (require(["@/views/shop/YuyueList"], resolve))
	},
	{
		path: '/BuySellList',
		name: 'BuySellList',
		component: resolve => (require(["@/views/shop/BuySellList"], resolve))
	},
	{
		path: '/Conssong',
		name: 'Conssong',
		component: resolve => (require(["@/views/shop/Conssong"], resolve))
	},
	{
		path: '/ConsAdd',
		name: 'ConsAdd',
		component: resolve => (require(["@/views/shop/ConsAdd"], resolve))
	},
	{
		path: '/ConsUpdate',
		name: 'ConsUpdate',
		component: resolve => (require(["@/views/shop/ConsUpdate"], resolve))
	},
	{
		path: '/ConsList',
		name: 'ConsList',
		component: resolve => (require(["@/views/shop/ConsList"], resolve))
	},
	{
		path: '/GoodsAdd',
		name: 'GoodsAdd',
		component: resolve => (require(["@/views/shop/GoodsAdd"], resolve))
	},
	{
		path: '/GoodsUpdate',
		name: 'GoodsUpdate',
		component: resolve => (require(["@/views/shop/GoodsUpdate"], resolve))
	},
	{
		path: '/GoodsList',
		name: 'GoodsList',
		component: resolve => (require(["@/views/shop/GoodsList"], resolve))
	},
	{
		path: '/JorderList',
		name: 'JorderList',
		component: resolve => (require(["@/views/shop/JorderList"], resolve))
	},
	{
		path: '/OrderList',
		name: 'OrderList',
		component: resolve => (require(["@/views/shop/OrderList"], resolve))
	},
	{
		path: '/OrderChild',
		name: 'OrderChild',
		component: resolve => (require(["@/views/shop/OrderChild"], resolve))
	},
	{
		path: '/DaleiAdd',
		name: 'DaleiAdd',
		component: resolve => (require(["@/views/shop/DaleiAdd"], resolve))
	},
	{
		path: '/DaleiUpdate',
		name: 'DaleiUpdate',
		component: resolve => (require(["@/views/shop/DaleiUpdate"], resolve))
	},
	{
		path: '/XiaoleiAdd',
		name: 'XiaoleiAdd',
		component: resolve => (require(["@/views/shop/XiaoleiAdd"], resolve))
	},
	{
		path: '/XiaoleiUpdate',
		name: 'XiaoleiUpdate',
		component: resolve => (require(["@/views/shop/XiaoleiUpdate"], resolve))
	},
	{
		path: '/Permission',
		name: 'Permission',
		component: resolve => (require(["@/views/admin/Permission"], resolve))
	},
	{
		path: '/AdministratorList',
		name: 'AdministratorList',
		component: resolve => (require(["@/views/admin/AdministratorList"], resolve))
	},
	{
		path: '/AdministratorAdd',
		name: 'AdministratorAdd',
		component: resolve => (require(["@/views/admin/AdministratorAdd"], resolve))
	},
	{
		path: '/AdministratorUpdate',
		name: 'AdministratorUpdate',
		component: resolve => (require(["@/views/admin/AdministratorUpdate"], resolve))
	},
	{
		path: '/BonusParameter',
		name: 'BonusParameter',
		component: resolve => (require(["@/views/system/BonusParameter"], resolve))
	},
	{
		path: '/SystemParameter',
		name: 'SystemParameter',
		component: resolve => (require(["@/views/system/SystemParameter"], resolve))
	},
	{
		path: '/Achievement',
		name: 'Achievement',
		component: resolve => (require(["@/views/information/Achievement"], resolve))
	},
	{
		path: '/LogAbnormity',
		name: 'LogAbnormity',
		component: resolve => (require(["@/views/information/LogAbnormity"], resolve))
	},
	{
		path: '/LogLogin',
		name: 'LogLogin',
		component: resolve => (require(["@/views/information/LogLogin"], resolve))
	},
	{
		path: '/LogOperation',
		name: 'LogOperation',
		component: resolve => (require(["@/views/information/LogOperation"], resolve))
	},
	{
		path: '/MembersHelp',
		name: 'MembersHelp',
		component: resolve => (require(["@/views/information/MembersHelp"], resolve))
	},
	{
		path: '/Message',
		name: 'Message',
		component: resolve => (require(["@/views/information/Message"], resolve))
	},
	{
		path: '/News',
		name: 'News',
		component: resolve => (require(["@/views/information/News"], resolve))
	},
	// {
	// 	path: '/Zengjian',
	// 	name: 'Zengjian',
	// 	component: resolve => (require(["@/views/wallet/Zengjian"], resolve))
	// },
	{
		path: '/Chongzhi',
		name: 'Chongzhi',
		component: resolve => (require(["@/views/wallet/Chongzhi"], resolve))
	},
	{
		path: '/Tixian',
		name: 'Tixian',
		component: resolve => (require(["@/views/wallet/Tixian"], resolve))
	},
	{
		path: '/Zhuanzhang',
		name: 'Zhuanzhang',
		component: resolve => (require(["@/views/wallet/Zhuanzhang"], resolve))
	},
	{
		path: '/Zhuanhuan',
		name: 'Zhuanhuan',
		component: resolve => (require(["@/views/wallet/Zhuanhuan"], resolve))
	},
	{
		path: '/Jiesuan',
		name: 'Jiesuan',
		component: resolve => (require(["@/views/bonus/Jiesuan"], resolve))
	},
	{
		path: '/BonusSource',
		name: 'BonusSource',
		component: resolve => (require(["@/views/bonus/BonusSource"], resolve))
	},
	{
		path: '/BonusTime',
		name: 'BonusTime',
		component: resolve => (require(["@/views/bonus/BonusTime"], resolve))
	},
	{
		path: '/Bonus',
		name: 'Bonus',
		component: resolve => (require(["@/views/bonus/Bonus"], resolve))
	},
	{
		path: '/FwzxApply',
		name: 'FwzxApply',
		component: resolve => (require(["@/views/users/FwzxApply"], resolve))
	},
	{
		path: '/FwzxRecord',
		name: 'FwzxRecord',
		component: resolve => (require(["@/views/users/FwzxRecord"], resolve))
	},
	{
		path: '/User',
		name: 'User',
		component: resolve => (require(["@/views/users/User"], resolve))
		
	},
	{
		path: '/NewsAdd',
		name: 'NewsAdd',
		component: resolve => (require(["@/views/information/NewsAdd"], resolve))
	},
	{
		path: '/NewsUpdate',
		name: 'NewsUpdate',
		component: resolve => (require(["@/views/information/NewsUpdate"], resolve))
	},
	{
		path: '/Database',
		name: 'Database',
		component: resolve => (require(["@/views/Database"], resolve))
	},
	{
		path: '/UlevelupRecord',
		name: 'UlevelupRecord',
		component: resolve => (require(["@/views/users/UlevelupRecord"], resolve))
	},
	{
		path: '/JihuoRecord',
		name: 'JihuoRecord',
		component: resolve => (require(["@/views/users/JihuoRecord"], resolve))
	},
	{
		path: '/AutoReturnList',
		name: 'AutoReturnList',
		component: resolve => (require(["@/views/information/AutoReturnList"], resolve))
	},
	{
		path: '/AutoReturnAdd',
		name: 'AutoReturnAdd',
		component: resolve => (require(["@/views/information/AutoReturnAdd"], resolve))
	},
	{
		path: '/AutoReturnUpDate',
		name: 'AutoReturnUpDate',
		component: resolve => (require(["@/views/information/AutoReturnUpDate"], resolve))
	},
	{
		path: '/ShopimgList',
		name: 'ShopimgList',
		component: resolve => (require(["@/views/shop/ShopimgList"], resolve))
	},
	{
		path: '/ShopimgAdd',
		name: 'ShopimgAdd',
		component: resolve => (require(["@/views/shop/ShopimgAdd"], resolve))
	},
	{
		path: '/ShopimgUpdate',
		name: 'ShopimgUpdate',
		component: resolve => (require(["@/views/shop/ShopimgUpdate"], resolve))
	},
	{
		path: '/SortManagement',
		name: 'SortManagement',
		component: resolve => (require(["@/views/shop/SortManagement"], resolve))
	},
	{
		path: '/Coin',
		name: 'Coin',
		component: resolve => (require(["@/views/wallet/Coin"], resolve))
	},
	{
		path: '/ChongzhiSelect',
		name: 'ChongzhiSelect',
		component: resolve => (require(["@/views/wallet/ChongzhiSelect"], resolve))
	},
	{
		path: '/TixianSelect',
		name: 'TixianSelect',
		component: resolve => (require(["@/views/wallet/TixianSelect"], resolve))
	},
	{
		path: '/ZhuanzhangSelect',
		name: 'ZhuanzhangSelect',
		component: resolve => (require(["@/views/wallet/ZhuanzhangSelect"], resolve))
	},
	{
		path: '/ZhuanhuanSelect',
		name: 'ZhuanhuanSelect',
		component: resolve => (require(["@/views/wallet/ZhuanhuanSelect"], resolve))
	},
	{
		path: '/ZengjianSelect',
		name: 'ZengjianSelect',
		component: resolve => (require(["@/views/wallet/ZengjianSelect"], resolve))
	},
	{
		path: '/Tree',
		name: 'Tree',
		component: resolve => (require(["@/views/users/Tree"], resolve))
	},
	{
		path: '/Network',
		name: 'Network',
		component: resolve => (require(["@/views/users/Network"], resolve))
	}
	
]

const router = new VueRouter({
	routes
})

export default router

const originalPush = VueRouter.prototype.push
   VueRouter.prototype.push = function push(location) {
   return originalPush.call(this, location).catch(err => err)
}