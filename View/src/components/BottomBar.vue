<template>
	<div>
		<van-tabbar v-model="active" inactive-color="#B5B6B8" :border="false" :placeholder="true">
			<van-tabbar-item icon="wap-home" name="/Home" to="/Home">
			<template #icon="props">
				<img :src="props.active ? require('@/assets/img/t1_a.png') : require('@/assets/img/t1.png')" width="18" height="18" />
			</template>
				首页
			</van-tabbar-item>
			<van-tabbar-item icon="shop" name="/Mygoods" to="/Mygoods">
				<template #icon="props">
					<img :src="props.active ? require('@/assets/img/t2_a.png') : require('@/assets/img/t2.png')" width="18" height="18" />
				</template>
				订单
			</van-tabbar-item>
			<!-- <van-tabbar-item icon="shop-o" @click="ontips">商城</van-tabbar-item> -->
			<!-- <van-tabbar-item icon="apps-o" @click="ontips">矿池</van-tabbar-item> -->
			<!-- <van-tabbar-item icon="shopping-cart-o" name="/ShopCar" to="/ShopCar" :badge="goodsCount">
				购物车
			</van-tabbar-item> -->
			<!-- <van-tabbar-item icon="newspaper-o" name="/News" to="/News">公告</van-tabbar-item> -->
			<van-tabbar-item icon="bag" name="/Fenxiao" to="/Fenxiao">
				<template #icon="props">
					<img :src="props.active ? require('@/assets/img/t3_a.png') : require('@/assets/img/t3.png')" width="18" height="18" />
				</template>
				中心
			</van-tabbar-item>
			<van-tabbar-item icon="manager" name="/User" to="/User">
				<template #icon="props">
					<img :src="props.active ? require('@/assets/img/t4_a.png') : require('@/assets/img/t4.png')" width="18" height="18" />
				</template>
				我的
			</van-tabbar-item>
		</van-tabbar>
	</div>
</template>

<script>
	import Vue from "vue";
	import {
		Tabbar,
		TabbarItem,
		Dialog,
		Toast
	} from "vant";

	Vue.use(Tabbar);
	Vue.use(TabbarItem);
	Vue.use(Toast);
	

	export default {
		data() {
			return {
				active: "",
				goodsCount: ""
			};
		},
		created() {
			this.getcarcount();
			this.active = this.$route.path;
		},
		mounted() {},
		methods: {
			getcarcount: function() {
				if (
					JSON.parse(localStorage.getItem("goodslist")) != null &&
					JSON.parse(localStorage.getItem("goodslist")).length > 0
				) {
					this.goodsCount = JSON.parse(localStorage.getItem("goodslist")).length;
				}
			},
			ontips() {
				Toast.fail('暂未开放');
			}
		}
	};
</script>

<style>
	/* .van-tabbar {
		background-color: #3B3E47;
	} */

	.van-tabbar-item--active{
		/* font-weight: 600; */
		color: #89f9ff;
	}
	
</style>
