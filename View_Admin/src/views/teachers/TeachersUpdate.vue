<template>
	<div style="margin-bottom: 200px">
		<PageTitle title="添加" :btn="false" :excel="false"></PageTitle>

		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="150px">
					<el-form-item label="名称">
						<el-input type="text" v-model="name" placeholder="请输入名称"></el-input>
					</el-form-item>
					<el-form-item label="简介">
						<el-input type="text" v-model="introduce" placeholder="请输入简介"></el-input>
					</el-form-item>
	
				<!-- 	<el-form-item label="上架">
						<el-switch v-model="ispay" :inactive-value="0" :active-value="1"></el-switch>
					</el-form-item> -->
					
					<el-form-item label="图片">
						
						<el-upload class="upload-demo" :action="UploadFile" :show-file-list="false" :on-success="handleAvatarSuccess1"
							:before-upload="beforeAvatarUpload" 
							:limit="6" list-type="picture">
							<img v-if="tximg" :src="getimg(tximg)" class="avatar" />
							<i v-else class="el-icon-plus avatar-uploader-icon"></i>
						</el-upload>
					</el-form-item>
					
					<el-form-item>
						<el-button type="primary" @click="Update">修改</el-button>
						<el-button @click="back">返回</el-button>
					</el-form-item>
				</el-form>
			</div>
		</div>
	</div>
</template>

<script>
	import PageTitle from "../../components/PageTitle";
	import Vue from "vue";
	import {
		Form,
		FormItem,
		Select,
		Option,
		Button,
		Input,
		Upload,
		Switch,
		InputNumber,
		Notification,
		Radio,
		TimePicker
	} from "element-ui";
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Button);
	Vue.use(Input);
	Vue.use(Upload);
	Vue.use(Switch);
	Vue.use(InputNumber);
	Vue.use(Radio);
	Vue.use(TimePicker);
	Vue.prototype.$notify = Notification;
	export default {
		name: "ConsAdd",
		components: {
			PageTitle,
		},
		data() {
			return {
				tid:'',
				name:'',
				introduce:'',
				ispay:1,
				tximg:'',
				isClear: false,
				UploadFile:
					this.$config.send_url +
					"Api/Upload/UploadImagesAdmin?token_admin=" +
					encodeURIComponent(this.$utils.getloc("token_admin")) +
					"&userid_admin=" +
					this.$utils.getloc("userid_admin") + "&width=" + this.$config.ImgRatio.Goods.Width + "&height=" + this.$config.ImgRatio.Goods.Height,
				fileList: [],
			};
		},
		created() {
			this.tid = this.$route.query.id;
			if(this.tid != ''){
				this.getdata(this.tid);
			}
		},
		methods: {
			
			handleRemove(file, fileList) {
				this.fileList = fileList;
			},
			handleAvatarSuccess1(res, file, fileList) {
				
				var img = {
					url: this.$config.img_url + res.data.imgname,
				};
				this.fileList.push(img);
				this.tximg = res.data.imgname;
			},
			back() {
				this.$router.push({
					name: "Teachers",
				});
			},
			
			//获取数据
			getdata: function (tid) {
				this.$request.post(
					"Api/Teachers_Admin/Get",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id:tid
					},
					(res) => {
						var data = res.data.data;
						this.name = data.name;
						this.introduce = data.introduce;
						this.tximg = data.tximg;
						
					}
				);
			},
			
			Panduan() {
				let _this = this;
				let mod = {
					pass: false,
					msg: "未知错误"
				}
				
				if (_this.name.length == 0) {
					mod.msg = "名称不能为空";
				}  else if (_this.introduce.length == 0) {
					mod.msg = "简介不能为空";
				} else if (_this.fileList.length == 0) {
					mod.msg = "请添加图片";
				} else {
					mod.pass = true;
					mod.msg = "已通过所有判断";
				}
				return mod;
			},
			Update() {
				let _this = this;
				let mod = _this.Panduan();
				if (mod.pass) {
					//this.cons.Qdate =_this.cons.Qdate.format("hh:mm:ss");
					_this.$confirm('确定要添加吗?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						type: 'warning'
					}).then(() => {
						let imglist = "";
					
						_this.$request.post(
							"Api/Teachers_Admin/Update",
							{
								token_admin: _this.$utils.getloc("token_admin"),
								userid_admin: _this.$utils.getloc("userid_admin"),
								id:_this.tid,
								name:_this.name,
								introduce:_this.introduce,
								img:_this.tximg
							},
							(res) => {
								_this.$notify({
									title: "提示",
									message: res.data.msg,
									offset: 100,
									type: "success",
								});
							}
						);
					}).catch(() => { });
				} else {
					_this.$notify({
						title: "提示",
						message: mod.msg,
						offset: 100,
						type: "warning",
					});
				}
			},
			getimg: function (img) {
				return this.$config.img_url + img;
			},
			beforeAvatarUpload(file) {
				const isJPG = file.type === "image/jpeg";
				const isPNG = file.type === "image/png";
				var isIMG = false;
				const isLt5M = file.size / 1024 / 1024 < 5;
				if (isJPG || isPNG) {
					isIMG = true;
				} else {
					this.$message.error("上传头像图片只能是 JPG/PNG 格式!");
				}
				if (!isLt5M) {
					this.$message.error("上传头像图片大小不能超过 5MB!");
				}
				return isIMG && isLt5M;
			},
		},
	};
</script>

<style scoped="scoped">
	/deep/.avatar-uploader /deep/.el-upload {
		border: 1px dashed #d9d9d9;
		border-radius: 6px;
		cursor: pointer;
		position: relative;
		overflow: hidden;
	}

	.avatar-uploader .el-upload:hover {
		border-color: #409eff;
	}

	.avatar-uploader-icon {
		font-size: 28px;
		color: #8c939d;
		width: 400px;
		height: 250px;
		line-height: 250px;
		text-align: center;
	}

	.avatar {
		width: 400px;
		height: 250px;
		display: block;
	}
</style>