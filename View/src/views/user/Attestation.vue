<template>
	<div>
		<HeadBar :fixed="true" :title="title" :placeholder="true" :bg="'transparent'"></HeadBar>
		<div class="box">
			<van-row>
				 <van-field v-model.trim="username" label="姓名" placeholder="请输入姓名" />
				 <van-field v-model.trim="usercode" label="身份证号" placeholder="请输入身份证号" />
			</van-row>

			<!-- <van-row class="img_smtxt color-hui-qian">
				<van-col span="24">手持身份证正面照</van-col>
			</van-row>
			<div class="div_img">
				<van-uploader v-model="fileList1" :after-read="afterRead1" :max-count="1" :max-size="5 * 1024 * 1024" :before-read="beforeRead"
				 @oversize="onOversize" />
			</div>

			<van-row class="img_smtxt color-hui-qian">
				<van-col span="24">手持身份证反面照</van-col>
			</van-row>
			<div class="div_img">
				<van-uploader v-model="fileList2" :after-read="afterRead2" :max-count="1" :max-size="5 * 1024 * 1024" :before-read="beforeRead"
				 @oversize="onOversize" />
			</div> -->
		</div>
		<van-row>
			<van-col span="24" class="van_btn">
				<van-button type="info" @click="attestation">实名认证</van-button>
			</van-col>
		</van-row>
		<Login></Login>
	</div>
</template>

<script>
	import HeadBar from "@/components/HeadBar";
	import Login from "@/components/Login";
	import Vue from "vue";
	import {
		Button,
		Field,
		Col,
		Row,
		Uploader
	} from "vant";

	Vue.use(Button);
	Vue.use(Field);
	Vue.use(Col);
	Vue.use(Row);
	Vue.use(Uploader);
	export default {
		components: {
			HeadBar,
			Login
		},
		data() {
			return {
				title: document.title,
				username: '',
				usercode:'',
				fileList1: [],
				fileList2: [],
				sfzz: '',
				sfzf: ''
			};
		},
		created() {
			this.load();
		},
		methods: {
			load: function() {
				//登录组件登录成功后会触发该函数,刷新父窗体数据
				if (this.$store.state.LoginState) {
					//子组件调用加载
					//this.getdata();
				}
			},
			attestation: function() {
				var _this = this;
				if (_this.usercode == "" || _this.username == "") {
					_this.$notify({
						type: "danger",
						background: "#4481ff",
						message: "请填写完整信息"
					});
					return;
				}
				var _this = this;
				_this.$request.post(
				    "api/Users/renzheng",
				    {
				        token: _this.$utils.getloc("token"),
				        userid: _this.$utils.getloc("userid"),
						sfzz:_this.sfzz,
						sfzf:_this.sfzf,
						usercode:_this.usercode,
						username:_this.username
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
							    position: "bottom",
							    duration: 2000,
							});
				        setTimeout(() => {
						    _this.fileList1 = [],
							_this.fileList2 = [],
							_this.sfzz = "";
						    _this.sfzf = "";
						},2000)
				    }
				);
				
			},

			afterRead1(file) {
				let _this = this;
				_this.fileList1[0].message = "上传中...";
				_this.fileList1[0].status = "uploading";
				let _formdata = new FormData(); //创建form对象
				_formdata.append("file", file.file);
				let _posturl =
				    _this.$config.send_url +
				    "Api/Upload/UploadImages?token=" +
				    encodeURIComponent(_this.$utils.getloc("token")) +
				    "&userid=" +
				    _this.$utils.getloc("userid");
				let _config = {
					headers: {
						"Content-Type": "multipart/form-data"
					}
				};
				this.axios
					.post(_posturl, _formdata, _config)
					.then(function(res) {
						if (res.data.code == 100) {
							_this.fileList1[0].message = "";
							_this.fileList1[0].status = "";
							_this.sfzz = res.data.data.imgname;
							console.log(res.data);
						} else {
							_this.fileList1[0].message = "上传失败";
							_this.fileList1[0].status = "failed";
						}
					})
					.catch(function(error) {
						console.log(error);
					});
			},
			afterRead2(file) {
				let _this = this;
				_this.fileList2[0].message = "上传中...";
				_this.fileList2[0].status = "uploading";
				let _formdata = new FormData(); //创建form对象
				_formdata.append("file", file.file);
				let _posturl =
				    _this.$config.send_url +
				    "Api/Upload/UploadImages?token=" +
				    encodeURIComponent(_this.$utils.getloc("token")) +
				    "&userid=" +
				    _this.$utils.getloc("userid");
				let _config = {
					headers: {
						"Content-Type": "multipart/form-data"
					}
				};
				this.axios
					.post(_posturl, _formdata, _config)
					.then(function(res) {
						if (res.data.code == 100) {
							_this.fileList2[0].message = "";
							_this.fileList2[0].status = "";
							_this.sfzf = res.data.data.imgname;
						} else {
							_this.fileList2[0].message = "上传失败";
							_this.fileList2[0].status = "failed";
						}
					})
					.catch(function(error) {
						console.log(error);
					});
			},
			beforeRead(file) {
				// 图片格式校验
				if (
					file.type == "image/jpeg" ||
					file.type == "image/png" ||
					file.type == "image/gif"
				) {
					//console.log("上传成功");
					return true;
				} else {
					this.$toast({
						message: "请上传 jpg 格式图片",
						position: "bottom",
						duration: 2000
					});
					return false;
				}
			},
			onOversize(file) {
				// 图片大小校验
				this.$toast({
					message: "请上传不大于5M内的图片",
					position: "bottom",
					duration: 2000
				});
			},
		},
	};
</script>

<style scoped>
	.van-cell {
	    background-color: #3B3E47;
	    color: #fff;
	}
/deep/.van-field__control {
    color: #fff;
}

	/deep/ .van-nav-bar {
		background-color: #1d223d;
	}
	
	.img_smtxt {
		padding-top: 30px;
		font-size: 14px;
	}

	.van_btn {
		padding: 30px 10px 10px 10px;
	}

	.van-button {
		width: 100%;
	}

	.div_img {
		text-align: center;
		padding-top: 20px;
		width: 100%;
	}

	/deep/ .van-image {
		width: 100%;
		height: 200px;
	}
	
	.box {
		padding: 10px 10px 0 10px;
	}
	
	/deep/ .van-uploader__upload {
		background: #1d223d;
	}
	
	/deep/ .van-uploader__upload i {
		color: #4481ff;
	}
</style>
