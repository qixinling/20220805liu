<template>
	<div>
		<HeadBar :title="title" :placeholder="false" :bg="'transparent'"></HeadBar>
		<!-- <img style="width: 100%;" :src="require('@/assets/img/1231.jpg')" /> -->
		<!-- <van-swipe @change="onChange">
			<van-swipe-item v-for="(item, index) in ulnamelist" :key="index">
				<div class="cardBox">
					<van-row>
						<van-col span="24" class="ulevelcol">
							<img draggable="false" :src="require('@/assets/img/re' + item.ul + '.png')" />
						</van-col>
					</van-row>
				</div>
			</van-swipe-item>
			<template #indicator>
				<div class="custom-indicator">
					当前级别 {{ ul }}/{{ ulnamelist.length }} 可选
				</div>
			</template>
		</van-swipe> -->
		<div class="form">
			<div style="padding: 10px;">
				<div >
					<!-- <van-field label="级别" :value="ulname" readonly />
					<van-field v-model.trim="bdname" label="服务中心" placeholder="请输入服务中心" /> -->
					<van-field v-model.trim="recode" label="推广码" placeholder="请输入推广码" />
					<!-- <van-field v-model.trim="fname" label="接点人" placeholder="请输入接点人" />
					<van-field label="区域" v-model="tpname" placeholder="请选择所在区域" is-link readonly
						@click="areaShow = true" />
					<van-popup v-model="areaShow" round position="bottom" :style="{ height: '30%' }">
						<van-picker title="所在区域" show-toolbar :columns="areacolumns" value-key="tpname"
							@cancel="AreaCancel" @confirm="AreaConfirm" />
					</van-popup> -->
					<!-- <van-field v-model.trim="userid" label="会员编号" placeholder="请输入会员编号" /> -->
					<van-field v-model.trim="usertel" label="手机号码" placeholder="请输入手机号码" />

					<!-- <van-field v-if="ss.czbs == 1" v-model="code" center clearable label="验证码" placeholder="请输入短信验证码">
					<template #button>
						<van-button size="small" type="primary" @click="sendcode()">{{send}}</van-button>
					</template>
          </van-field> -->

					<van-field v-model.trim="password" type="password" placeholder="请输入登录密码" label="登录密码" />
					<!-- <van-field v-model.trim="password_com" type="password" placeholder="请输入密码" label="确认密码" /> -->
					<van-field v-model.trim="password2" type="password" placeholder="请输入支付密码" label="支付密码" />
					<!-- <van-field v-model.trim="password2_com" type="password" placeholder="请输入密码" label="确认密码" /> -->
					<!-- <van-field v-model.trim="username" label="真实姓名" placeholder="请输入姓名" /> -->
				</div>
			</div>
		</div>
		<van-submit-bar :price="0" button-text="注册" @submit="Users_Register">
			<van-checkbox v-model="checked">
				我已清楚并同意
				<span class="regist-protocol" @click="show = true">注册协议</span>
			</van-checkbox>
		</van-submit-bar>
		<van-dialog v-model="show" title="注册协议" show-cancel-button @cancel="DoNotAgree" @confirm="Agree"
			confirm-button-text="同意" cancel-button-text="不同意">
			<div class="reginfo" v-html="registInfo"></div>
		</van-dialog>
	</div>
</template>

<script>
	import "@vant/touch-emulator";
	import HeadBar from "@/components/HeadBar";
	import Vue from "vue";
	import {
		Stepper,
		Field,
		SubmitBar,
		Button,
		Area,
		Popup,
		Picker,
		AddressEdit,
		Checkbox,
		Dialog,
		Swipe,
		SwipeItem,
	} from "vant";

	Vue.use(Stepper);
	Vue.use(Field);
	Vue.use(SubmitBar);
	Vue.use(Button);
	Vue.use(AddressEdit);
	Vue.use(Area);
	Vue.use(Popup);
	Vue.use(Picker);
	Vue.use(Checkbox);
	Vue.use(Dialog);
	Vue.use(Swipe);
	Vue.use(SwipeItem);
	export default {
		components: {
			HeadBar,
		},
		data() {
			return {
				title: document.title,
				cacheToken: "",
				bdname: "",
				recode: "",
				fname: "",
				tp: 1,
				tpname: "A区",
				areaShow: false,
				areacolumns: [{
					flex: 1,
					values: [{
							tp: 1,
							tpname: "A区",
						},
						{
							tp: 2,
							tpname: "B区",
						},
						{
							tp: 3,
							tpname: "C区",
						},
						{
							tp: 4,
							tpname: "D区",
						},
						{
							tp: 5,
							tpname: "E区",
						},
					],
					defaultIndex: 0,
				}, ],
				userid: "",
				username: "",
				usertel: "",
				password: "",
				password_com:"",
				password2: "",
				password2_com:"",
				code: "",
				send: "发送",
				checked: false,
				show: false,
				registInfo: "",
				ulnamelist: [], //等级名称集合
				ul: "", //选择的等级
				ulname: "", //选择的等级名称
				ulindex: 0, //用于注册金额集合与等级名称相匹配的下标
				ss:""
			};
		},
		created() {
			this.cacheToken = Math.random();
			if (this.$route.query.rr) {
				this.recode = this.$route.query.rr;
			}
			if (this.$utils.GetQueryString("rr")) {
				this.recode = this.$utils.GetQueryString("rr");
			}
			// if (this.recode) {
			// 	this.Users_Leader();
			// }
			//this.getulevel();
			this.GetRegistInfo();
			//this.Getss();
		},
		methods: {
			GetRegistInfo() {
				var _this = this;
				_this.$request.post(
					"api/Article/Get", {
						select_id: 8,
					},
					(res) => {
						if (res.data.code == 0) {
							_this.$dialog.alert({
								title: "提示",
								message: res.data.msg,
							});
							return;
						}
						_this.registInfo = res.data.data.articlecontent;
					}
				);
			},
			Getss() {
				var _this = this;
				_this.$request.post(
					"api/Users/GetSystemSetting", {
					},
					(res) => {
						if (res.data.code == 0) {
							_this.$dialog.alert({
								title: "提示",
								message: res.data.msg,
							});
							return;
						}
						_this.ss = res.data.data;
						console.log(_this.ss);
					}
				);
			},
			sendcode: function() {
				let _this = this;
				if (_this.send == "发送") {
					let i = 60;
					_this.send = i + "S";
					var timer = setInterval(function() {
						i--;
						_this.send = i + "S";
						if (i <= 0) {
							clearInterval(timer);
							_this.send = "发送";
						}
					}, 1000);

					if (this.usertel.length != 11) {
						this.$toast.fail("请输入11位手机号码");
						return;
					}

					let _usertel = "aaa" + this.usertel + "bbb";

					_this.$request.post(
						"api/Sms/SendCode", {
							token: _this.$utils.getloc("token"),
							userid: _this.$utils.getloc("userid"),
							usertel: _usertel,
						},
						(res) => {
							if (res.data.code == 0) {
								_this.$dialog.alert({
									title: "提示",
									message: res.data.msg,
								});
								return;
							}
							_this.$toast({
								message: res.data.msg,
								position: "middle",
								duration: 2000,
							});
						}
					);
				}
			},
			Users_Register: function() {
				if (this.recode == "") {
					this.$toast.fail("推广码不能为空");
					return;
				}
				// if (this.userid == "") {
				// 	this.$toast.fail("用户名不能为空");
				// 	return;
				// }
				// if (this.username == "") {
				// 	this.$toast.fail("姓名不能为空");
				// 	return;
				// }
				if (this.usertel == "") {
					this.$toast.fail("手机号不能为空");
					return;
				}
				if (this.password == "") {
					this.$toast.fail("登录密码不能为空");
					return;
				}
				// if (this.password_com == "") {
				// 	this.$toast.fail("确认密码不能为空");
				// 	return;
				// }
				// if (this.password != this.password_com) {
				// 	this.$toast.fail("登录密码与确认密码不一致");
				// 	return;
				// }
				if (this.password2 == "") {
					this.$toast.fail("支付密码不能为空");
					return;
				}
				// if (this.password2_com == "") {
				// 	this.$toast.fail("确认密码不能为空");
				// 	return;
				// }
				// if (this.password2 != this.password2_com) {
				// 	this.$toast.fail("支付密码与确认密码不一致");
				// 	return;
				// }
				// if (this.password2 == "") {
				// 	this.$toast.fail("支付密码不能为空");
				// 	return;
				// }
				if (this.checked == false) {
					this.$toast.fail("请先同意注册协议");
					return;
				}

				let _this = this;
				let _loadtoast = _this.$toast.loading({
					message: "正在注册...",
					duration: 0, // 持续展示 toast
				});
				_this.$request.post(
					"api/Users/Register", {
						cacheToken: _this.cacheToken,
						recode: _this.recode,
					    userid: _this.usertel,
						password: _this.password,
						password2: _this.password2,
						// username: _this.username,
						usertel: _this.usertel,
					},
					(res) => {
						if (res.data.code == 0) {
							_this.$dialog.alert({
								title: "提示",
								message: res.data.msg,
							});
							return;
						}
						_this.userid = "";
						_this.password = "";
						_this.password2 = "";
						_this.username = "";
						_this.usertel = "";
						_loadtoast.clear();

						_this.$toast({
							message: res.data.msg,
							position: "middle",
							duration: 2000,
						});
					}
				);
			},
			//同意协议
			Agree() {
				this.checked = true;
			},
			//不同意协议
			DoNotAgree() {
				this.checked = false;
			},
			onChange(index) {
				// console.log(index);
				this.ulindex = index;
				this.ul = this.ulnamelist[index].ul;
				this.ulname = this.ulnamelist[index].ulname;
				// console.log("ul：",this.ul)
			},
		},
	};
</script>

<style scoped>
	/deep/ .van-cell{
		line-height: 34px;
	}
	
	.form {
		position: relative;
	}

	.form-list {
		position: absolute;
		top: -55px;
		padding: 0;
		width: 100%;
	}

	.border-radius {
		border-top-left-radius: 30px;
		border-top-right-radius: 30px;
		border-bottom-left-radius: 0;
		border-bottom-right-radius: 0;
		overflow: hidden;
	}

	/deep/.van-nav-bar .van-icon {
		color: #000;
	}

	/* 隐藏价格 */
	/deep/ .van-submit-bar__text>* {
		display: none;
	}

	.regist-protocol {
		color: #ea4747;
		text-decoration: underline;
	}

	.reginfo {
		max-height: 400px;
		padding: 10px;
		font-size: 14px;
		overflow: scroll;
	}

	.ulevelcol {
		position: relative;
	}

	.ulevelcol img {
		width: 100%;
		border-radius: 10px;
		float: left;
	}

	.custom-indicator {
		color: white;
		position: absolute;
		right: 14px;
		top: 10px;
		padding: 2px 5px;
		font-size: 12px;
		background: rgba(0, 0, 0, 0.1);
	}

	.van-swipe {
		background-image: url(../../assets/img/2.png);
		background-size: 100%;
	}

	/deep/.van-submit-bar__button--danger {
		background: linear-gradient(to right, #FC4702, #FC4702);
	}

	/deep/.van-checkbox__icon--checked .van-icon {
		background-color: #FC4702;
		border-color: #FC4702;
	}
</style>
