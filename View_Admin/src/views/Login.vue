<template>
	<div>
		<div class="colorbg"></div>
		<!-- <img src="../assets/img/bg.jpg" class="imgbg" /> -->
		<el-row>
			<el-col :xs="2" :sm="2" :md="9" :lg="9" style="min-height:1px;">
			</el-col>
			<el-col :xs="20" :sm="20" :md="6" :lg="6">
				<div class="cardbox shadow" style="margin-top: 100px;">
					<div class="title">
						<div class="bg-overlay"></div>
						<div class="titletext">Sign Up</div>
					</div>
					<div class="cardcontent">
						<el-form ref="form" label-width="60px" style="margin-top: 10px;">
							<el-form-item label="账号">
								<el-input v-model="userid" prefix-icon="el-icon-user" placeholder="请输入用户名">
								</el-input>
							</el-form-item>
							<el-form-item label="密码">
								<el-input v-model="password" type="password" prefix-icon="el-icon-lock" placeholder="请输入密码"></el-input>
							</el-form-item>
							<el-form-item>
								<el-button type="primary" size="medium" @click="login" icon="el-icon-thumb">登录</el-button>
							</el-form-item>
						</el-form>
					</div>
				</div>
			</el-col>
			<el-col :xs="2" :sm="2" :md="9" :lg="9" style="min-height:1px;"></el-col>
		</el-row>
	</div>
</template>

<script>
	import AES from "@/assets/js/aes.js";
	import MD5 from 'js-md5';
	import Axios from "axios";
	import qs from "qs";
	import md5 from 'js-md5';
	import Vue from 'vue';
	import {
		Select,
		Option,
		Input,
		Form,
		FormItem,
		Notification
	} from 'element-ui';
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Input);
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.prototype.$notify = Notification;
	export default {
		name: 'login',
		data() {
			return {
				userid: '',
				password: ''
			}
		},
		methods: {
			login: function() {
				let key = MD5(this.$config.Project).toUpperCase();
				let password = AES.encrypt(this.password,key);
				this.$request.post("Api/SystemAdmin_Admin/Login", {
					userid: this.userid,
					password: password
				}, (res) => {
					this.$utils.setloc('token_admin', res.data.data.token);
					this.$utils.setloc('id_admin', res.data.data.id);
					this.$utils.setloc('userid_admin', res.data.data.userid);
					this.$utils.setloc('permission', res.data.data.permission);

					this.$store.commit("setLoginState", true);

					location.reload();
				});
			}
		}
	}
</script>

<style scoped="scoped">
	.colorbg {
		position: absolute;
		width: 100%;
		height: 100%;
		background: #26d9b3;
		background: -moz-linear-gradient(right, #26d9b3 0%, #5a84fd 100%);
		background: -webkit-linear-gradient(right, #26d9b3 0%, #5a84fd 100%);
		background: linear-gradient(to right, #26d9b3 0%, #5a84fd 100%);
		filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#26d9b3', endColorstr='#5a84fd', GradientType=1);
	}

	.imgbg {
		width: 100%;
		position: fixed;
		right: 0;
		bottom: 0;
		min-width: 100%;
		min-height: 100%;
		height: auto;
		z-index: -100;
		background-size: cover;
	}

	.cardbox {
		padding: 0;
		border-radius: 10px;
	}

	.title {
		background-image: url(../assets/img/bg-title-01.jpg);
		position: relative;
		padding: 40px;
		padding-top: 45px;
		padding-bottom: 47px;
		-webkit-background-size: cover;
		-moz-background-size: cover;
		-o-background-size: cover;
		background-size: cover;
		background-position: center center;
		background-repeat: no-repeat;
		-webkit-border-top-left-radius: 10px;
		-moz-border-radius-topleft: 10px;
		border-top-left-radius: 10px;
		-webkit-border-top-right-radius: 10px;
		-moz-border-radius-topright: 10px;
		border-top-right-radius: 10px;
		-webkit-box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
		-moz-box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
		box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.1);
	}

	.titletext {
		font-size: 30px;
		position: relative;
		z-index: 2;
		color: #fff;
		font-weight: 400;
	}

	.bg-overlay {
		/* background: rgba(49, 89, 253, 0.9); */
		background: rgba(0, 0, 0, 0.7);
		position: absolute;
		width: 100%;
		height: 100%;
		top: 0;
		left: 0;
		z-index: 0;
		-webkit-border-top-left-radius: 10px;
		-moz-border-radius-topleft: 10px;
		border-top-left-radius: 10px;
		-webkit-border-top-right-radius: 10px;
		-moz-border-radius-topright: 10px;
		border-top-right-radius: 10px;
	}
</style>
