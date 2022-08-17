<template>
	<div>
		<div style="position: fixed; padding-top: 10px;padding-left: 16px;">
			<router-link to="User" style="font-size: 16px;color: #FFF;">
				<van-icon name="arrow-left" color="#000" size="18px" />
			</router-link>
		</div>
		<vue-canvas-poster :widthPixels="1000" :painting="painting" @success="success" @fail="fail"></vue-canvas-poster>
		<img v-show="img" style="width: 100%;height: 100%;z-index: 1;" :src="img" />
		<div style="z-index: 999;text-align: center;margin-top: -260px;">
			<!-- <van-button type="primary" icon="down" @click="download">保存推广码</van-button> -->
			<van-button round color="linear-gradient(to right, #9CC485, #66A54E)" style="width:80%" @click="CopyLink">复制链接</van-button>
		</div>
	</div>
</template>

<script>
	import {
		VueCanvasPoster
	} from "vue-canvas-poster";
	import Vue from "vue";
	import {
		Button
	} from "vant";

	Vue.use(Button);
	export default {
		name: "qrcode",
		components: {
			VueCanvasPoster
		},
		data() {
			return {
				title: document.title,
				windowWidth: document.documentElement.clientWidth, //实时屏幕宽度
				windowHeight: document.documentElement.clientHeight, //实时屏幕高度
				usertel: "",
				recode:"",
				img: "",
				url: "",
				tx: "",
				painting: {}
			};
		},
		created() {
			this.usertel = this.$route.query.usertel;
			this.recode = this.$route.query.recode;
			this.url = this.$config.send_url + "#/Register?rr=" + this.$route.query.recode;
			this.tx = this.$config.send_url + "Upload/" + this.$route.query.tx;
			setTimeout(()=>{
			    this.draw(this.windowWidth,this.windowHeight);
			},500)
			//禁用微信分享
			if (typeof WeixinJSBridge == "undefined") { 
			    if (document.addEventListener) { 
			        // eslint-disable-next-line no-undef
			        document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false); 
			    } else if (document.attachEvent) { 
					// eslint-disable-next-line no-undef
			        document.attachEvent('WeixinJSBridgeReady', onBridgeReady); 
					// eslint-disable-next-line no-undef
			        document.attachEvent('onWeixinJSBridgeReady', onBridgeReady); 
			    } 
			} else { 
			    this.onBridgeReady(); 
			}
		},
		methods: {
			onBridgeReady:function(){
				// eslint-disable-next-line no-undef
				WeixinJSBridge.call('hideOptionMenu');
			},
			download: function() {
				const save_link = document.createElementNS('http://www.w3.org/1999/xhtml', 'a');
				save_link.href = this.img;
				let _filename = new Date().toLocaleString();
				save_link.download = _filename;

				const event = document.createEvent('MouseEvents');
				event.initMouseEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
				save_link.dispatchEvent(event);
			},
			draw: function(windowWidth,windowHeight) {
				this.painting = {
					width: windowWidth+ "px",
					height: windowHeight+ "px",
					background: require("@/assets/canvas/bg.png"),
					views: [
						// {
						// 	type: "image",
						// 	url: require("@/assets/canvas/logo.png"),
						// 	css: {
						// 		top: "50px",
						// 		left: "105px",
						// 		width: "200px",
						// 		height: "120px"
						// 	}
						// },
						{
							type: "image",
							url: require("@/assets/canvas/box.png"),
							css: {
								top: "180px",
								left: this.windowWidth / 2 - 140 + "px",
								borderRadius: "10px",
								width: "280px",
								height: "280px"
							}
						},
						{
							type: "image",
							url: require("@/assets/canvas/kuang2.png"),
							css: {
								top: "170px",
								left: this.windowWidth / 2 - 155 + "px",
								width: "310px",
								height: "300px"
							}
						},
						{
							type: "qrcode",
							content: this.url,
							css: {
								top: "210px",
								left: this.windowWidth / 2 - 70 + "px",
								color: "#000",
								width: "140px",
								height: "140px"
							}
						},
						{
							type: "image",
							url: this.tx,
							css: {
								top: "265px",
								left: this.windowWidth / 2 - 15 + "px",
								borderRadius: "10px",
								width: "30px",
								height: "30px"
							}
						},
						{
							type: "text",
							text: "用户 " + this.usertel,
							css: {
								fontSize: "16px",
								color: "#AE7258",
								top: "360px",
								left: 170 - this.usertel.length * 5 + "px",
								textAlign: "center"
							}
						},
						{
							type: "text",
							text: "推广码" + this.recode,
							css: {
								color: "#AE7258",
								fontSize: "16px",
								top: "390px",
								left: 155 - this.recode.length * 4 + "px",
								textAlign: "center"
							}
						},
						{
							type: "text",
							text: "专属分享二维码",
							css: {
								color: "#AE7258",
								fontSize: "16px",
								top: "420px",
								left: "125px",
							}
						},
						{
							type: "text",
							text: "专属分享链接",
							css: {
								color: "#368B39",
								fontWeight: "600",
								fontSize: "16px",
								width: "300px",
								top: "480px",
								left: this.windowWidth / 2 - 150 + "px",
								textAlign: "center"
							}
						},
						{
							type: "text",
							text: this.url,
							css: {
								fontSize: "16px",
								width: "300px",
								top: "510px",
								left: this.windowWidth / 2 - 150 + "px",
								textAlign: "center"
							}
						}
					]
				};
			},
			success(src) {
				this.img = src;
				//console.log(src);
			},
			fail(err) {
				//console.log("fail", err);
			},
			CopyLink() {
				if (this.url != null && this.url != "") {
					var domUrl = document.createElement("input");
					domUrl.value = this.url;
					domUrl.id = "creatDom";
					document.body.appendChild(domUrl);
					domUrl.select(); // 选择对象
					document.execCommand("Copy"); // 执行浏览器复制命令
					let creatDom = document.getElementById("creatDom");
					creatDom.parentNode.removeChild(creatDom);
					this.$toast.success("复制成功");
				}
		},
		}
	};
</script>

<style scoped></style>
