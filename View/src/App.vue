<template>
	<div id="app">
		<router-view />
	</div>
</template>

<script>
	import "@/assets/font/pf.css";
	export default {
		name: "app",
		created() {
			//在页面加载时读取sessionStorage里的状态信息
			if (this.$utils.getloc("store")) {
				this.$store.replaceState(
					Object.assign({},
						this.$store.state,
						JSON.parse(this.$utils.getloc("store"))
					)
				);
			}
			if (this.$utils.GetQueryString('action') == 'login') {
				var t = this.$utils.GetQueryString('t');
				if (t != null) {
					this.$utils.setloc("token", t);
					this.$utils.setloc("id", this.$utils.GetQueryString('id'));
					this.$utils.setloc("userid", this.$utils.GetQueryString('userid'));
					this.$utils.setloc("username", this.$utils.GetQueryString('username'));
					this.$utils.setloc("recode", this.$utils.GetQueryString('recode'));
					this.$utils.setloc(
						"tx",
						this.$config.send_url + "Upload/" + this.$utils.GetQueryString('tx')
					);
					this.show = false;
					this.$store.commit("setLoginState", true);
					
					setTimeout(function () {
					    //重定向,清除掉地址栏的参数,防止退出时反复刷新
					    window.location.href = '?';
					}, 1000);
					
				}
			}
			
			try {
				window.addEventListener("beforeunload", () => {
					this.$utils.setloc("store", JSON.stringify(this.$store.state));
				});
			} catch {
				console.log('beforeunload error');
				try {
					window.addEventListener("pagehide", () => {
						this.$utils.setloc("store", JSON.stringify(this.$store.state));
					});
				} catch {
					console.log('pagehide error');
					try {
						window.addEventListener("unload", () => {
							this.$utils.setloc("store", JSON.stringify(this.$store.state));
						});
					} catch {
						console.log('unload error');
					}
				}
			}
		},
		mounted() {
			//解决安卓返回键退出APP问题
			let _this = this;
			document.addEventListener('plusready', function(a) {
				var first = '';
				// eslint-disable-next-line no-undef
				plus.key.addEventListener('backbutton', function() {
					//获取地址栏目中的url
					var urls = location.hash.split('/')[1]
					//判断是一级页面的时候点击两次退出app
					if (urls === 'ShopIndex' || urls === '') {
						if (new Date().getTime() - first < 3000) {
							// eslint-disable-next-line no-undef
							plus.runtime.quit(); //表示退出app
						} else {
							// eslint-disable-next-line no-undef
							plus.nativeUI.toast('再按一次退出应用')
							first = new Date().getTime();
						}
					} else {
						_this.$router.go(-1);
					}
				}, false)
			});
		}
	};
</script>

<style lang="scss">
	body {
		background: #f4f5f5;
	}

	#app {
		// font-family: "苹方黑体";
		color: #222426;
		letter-spacing: 0.5px;
		width: 100%;
		max-width: 600px;
		/* 这个表示最大宽度 */
		min-height: 100vh;
		margin: 0 auto;
		/* 这个用于居中 */
		background: #f7f7f7;
	}

	.cardBox {
		padding: 10px;
	}

	.cardBox .cardContent {
		padding: 5px;
		background: #fff;
		border-radius: 10px;
	}

	.cardShadow {
		box-shadow: 0 1px 5px rgba(0, 0, 0, 0.15);
	}

	.van-cell::after {
		/*cell下划线左侧*/
		left: 0px !important;
	}

	/*红色按钮*/
	.van-button--danger {
		color: #fff;
		background-color: #ee0a24;
		border: 1px solid #ee0a24;
	}
	
	/* 主题色 */
	.maincolor{
		background-image: linear-gradient(to right, #3B3E47, #3B3E47);
	}
	/* 按钮颜色 */
	.btncolor{
		background-color:#F7B226;
	}
	
	/* 渐变按钮颜色 */
	.jbtncolor{
		background-image: linear-gradient(to right, #60c597, #38ab77);
	}
	
	/* 字体颜色 */
	.textcolor{
		color:#3fceb3;
	}
	.textcolor2{
		color:#fff;
	}
</style>
