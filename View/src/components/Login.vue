<template>
	<div class="login">
		<van-action-sheet v-model="show" title="" :round="false" >
			<div class="content">
				<div class="logintop">
					<div style="text-align: right;padding:5px 5px">
						<van-icon name="cross" size="20" color="#f2f2f2" @click="gbclick()" />
					</div>

					<div style="text-align: center;padding-top: 5px;">
						<!-- <van-icon name="user-circle-o" size="50" color="#f2f2f2" /> -->
						<img src="@/assets/img/logo.png" width="50" />
					</div>
					<div class="logintext">登录</div>
				</div>

				<van-form @submit="onSubmit" style="">
					<div style="padding: 10px;">
						<van-field v-model="userid" name="用户名" label="用户名" placeholder="用户名"
							:rules="[{ required: true, message: '请填写用户名' }]" />
						<van-field v-model="password" type="password" name="密码" label="密码" placeholder="密码"
							:rules="[{ required: true, message: '请填写密码' }]" />
						<van-field v-model="card" name="登船编码" label="登船编码" placeholder="登船编码"
								:rules="[{ required: true, message: '登船编码' }]" />
						<van-row style="font-size: 12px;margin: 16px;">
							<van-col span="12">
								<van-checkbox v-model="checked" icon-size="14px" shape="square">记住密码</van-checkbox>
							</van-col>
							<van-col span="12" style="text-align: right;">
								<!-- <router-link to="/PasswordRetrieve?lx=1">忘记密码</router-link>
								<span> | </span> -->
								<router-link to="/register">注册新账户</router-link>
							</van-col>
						</van-row>
					</div>

					<div style="margin: 10px 20px 40px 20px;">
						<van-button round block color="linear-gradient(to right, #ff4500, #ff4500)" class="textcolor2">
							登录
						</van-button>
					</div>
				</van-form>
			</div>
		</van-action-sheet>
	</div>
</template>

<script>
	import AES from "@/assets/js/aes.js";
	import MD5 from 'js-md5';
	import Vue from "vue";
	import {
		Form
	} from "vant";
	import {
		Field
	} from "vant";
	import {
		ActionSheet
	} from "vant";
	import {
		Checkbox,
		CheckboxGroup
	} from "vant";
	import {
		Icon
	} from "vant";

	Vue.use(Icon);
	Vue.use(Checkbox);
	Vue.use(CheckboxGroup);
	Vue.use(ActionSheet);
	Vue.use(Field);
	Vue.use(Form);
	export default {
		name: "login",
		data() {
			return {
				show: false,
				userid: "",
				password: "",
				card:'',
				toast: "",
				checked: false
			};
		},
		mounted() {
			this.show = !this.$store.state.LoginState;

			if (JSON.parse(this.$utils.getloc("userdata")) != null) {
				var userdata = JSON.parse(this.$utils.getloc("userdata"));
				this.userid = userdata.userid;
				this.password = userdata.password;
				this.checked = true;
			}
		},
		methods: {
			gbclick() {

				this.show = false;
			},
			onSubmit: function() {
				var _this = this;
				let _loadtoast = _this.$toast.loading({
					message: "正在登录...",
					duration: 0 // 持续展示 toast
				});
				let key = MD5(_this.$config.Project).toUpperCase();
				let password = AES.encrypt(_this.password, key);
				_this.$request.post("Api/Users/Login", {
					userid: _this.userid,
					password: password,
					card:_this.card
				}, (res) => {
					var data = res.data.data;

					if (res.data.code == 0) {
						let _loadtoast = _this.$toast({
							message: res.data.msg,
							duration: 3000
						});
						return;
					}

					_this.$utils.setloc("token", res.data.data.token);
					_this.$utils.setloc("id", res.data.data.id);
					_this.$utils.setloc("userid", res.data.data.userid);
					_this.$utils.setloc("recode", res.data.data.recode);
					_this.$utils.setloc("username", res.data.data.username);
					_this.$utils.setloc(
						"tx",
						_this.$config.send_url + "Upload/" + res.data.data.tx
					);
					_this.show = false;
					_this.$store.commit("setLoginState", true);

					_loadtoast.clear();

					//登录成功,触发父窗体加载内容
					_this.$parent.load();
					
					//location.reload();

					if (_this.checked) {
						var userdata = {
							userid: _this.userid,
							password: _this.password
						};
						_this.$utils.setloc("userdata", JSON.stringify(userdata));
					} else {
						if (JSON.parse(_this.$utils.getloc("userdata")) != null) {
							localStorage.removeItem("userdata");
						}
					}
				});
			}
		},

	};
</script>

<style scoped>
	/deep/.van-action-sheet{
		height: 100% !important;
		max-height: inherit;
	}
	/deep/.van-checkbox__icon--checked .van-icon {
		background-color: #F7B226;
		border-color: #F7B226;
	}

	/deep/.van-field__label {
		padding-left: 10px;
	}

	.logintop {
		height: 150px;
		/* background: #4FC08D; */
		border-radius: 0px 0px 70% 70%;
		background-image: linear-gradient(to right top, #ff4500, #e27249);
	}

	.logintext {
		text-align: center;
		font-size: 30px;
		color: #F2F3F5;

	}
</style>
