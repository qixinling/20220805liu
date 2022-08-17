<template>
	<div></div>
</template>

<script>
	import Vue from "vue";
	import {
		Button
	} from "vant";
	Vue.use(Button);
	export default {
		data() {
			return {};
		},
		methods: {
			goodsAddCar: function(goods) {
				
				if (goods.stock <= 0) {
					this.$toast({
						message: "库存不足",
						position: "bottom",
						duration: 1500
					});
					return;
				}

				let nohas = true;
				let _goodslist = JSON.parse(this.$utils.getloc('goodslist'));
				if (_goodslist == null) {
					_goodslist = [];
				} else {
					_goodslist.forEach(item => {
						if (item.id == goods.id) {
							item.num = item.num + goods.num;
							nohas = false;
						}
					});
				}

				if (nohas) {
					
					var good = {
						id: goods.id,
						goodsname: goods.goodsname,
						goodsimg: goods.goodsimg.split(",")[0],
						sjine: goods.sjine,
						yjine: goods.yjine,
						num: goods.num,
						prompt: goods.prompt,
						cost: goods.cost,
						gchecked: true
					};
					_goodslist.push(good);
				}

				this.$utils.setloc('goodslist', JSON.stringify(_goodslist));
				this.$toast({
					message: "成功加入购物车",
					position: "bottom",
					duration: 1500
				});
			}
		}
	};
</script>

<style scoped></style>
