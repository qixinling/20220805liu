<template>
	<div>
		<PageTitle title="产品修改" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="150px">
					<el-form-item label="名称">
						<el-input type="text" v-model="cons.Name" placeholder="请输入名称"></el-input>
					</el-form-item>
					
					<el-form-item label="价格">
						<el-input-number size="mini" v-model="cons.Minprice" :min="1" :precision="2"></el-input-number>
						-
						<el-input-number size="mini" v-model="cons.Maxprice" :min="1" :precision="2"></el-input-number>
					</el-form-item>
					<el-form-item label="押金单">
						<el-input type="text" v-model="cons.Jindan" placeholder="请输入押金单" style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="预约金丹">
						<el-input type="text" v-model="cons.Yyjindou" placeholder="请输入预约金丹" style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="领养金丹">
						<el-input type="text" v-model="cons.Lyjindou" placeholder="请输入领养金丹" style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="周期">
						<el-input type="text" v-model="cons.Zhouqi" placeholder="请输入周期" style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="日收益">
						<el-input type="text" v-model="cons.Rishouyi" placeholder="请输入日收益" style="width:200px;"></el-input> %
					</el-form-item>
					<el-form-item label="抢单开始时间">
						<el-time-picker v-model="cons.Qdate"  value-format="HH:mm:ss"></el-time-picker>
					</el-form-item>
					<el-form-item label="抢单结束时间">
						<el-time-picker v-model="cons.Edate"  value-format="HH:mm:ss"></el-time-picker>
					</el-form-item>
					<el-form-item label="每日限购">
						<el-input type="text" v-model="cons.Kucun" placeholder="请输入每日限购" style="width:200px;"></el-input> 
					</el-form-item>
					<el-form-item label="上架">
						<el-switch v-model="cons.State" :inactive-value="0" :active-value="1"></el-switch>
					</el-form-item>
					<el-form-item label="开放">
						<el-switch v-model="cons.Iskf" :inactive-value="0" :active-value="1"></el-switch>
					</el-form-item>
					
					<el-form-item label="图片">
						<el-upload class="upload-demo" :action="UploadFile" :on-success="handleAvatarSuccess1" :before-upload="beforeAvatarUpload"
						 :on-remove="handleRemove" :file-list="fileList" :limit="1" list-type="picture">
							<el-button size="small" type="primary">点击上传</el-button>
							<div slot="tip" class="el-upload__tip">
								只能上传jpg/png文件，且不超过5MB
							</div>
						</el-upload>
					</el-form-item>
					<el-form-item>
						<el-button icon="el-icon-edit" type="success" @click="Update" size="mini">修改</el-button>
						<el-button icon="el-icon-back" @click="back" size="mini">返回</el-button>
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
	Vue.use(TimePicker);
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
		},
		data() {
			return {
				cid:'',
				cons: {},
				isClear: false,
				UploadFile: this.$config.send_url +
					"Api/Upload/UploadImagesAdmin?token_admin=" +
					encodeURIComponent(this.$utils.getloc("token_admin")) +
					"&userid_admin=" +
					this.$utils.getloc("userid_admin") + "&width=" + this.$config.ImgRatio.Goods.Width + "&height=" + this.$config.ImgRatio.Goods.Height,
				fileList: [],
			};
		},
		created() {
			this.cid = this.$route.query.Id;
			this.getdata();
			
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
				this.cons.Img = res.data.imgname;
			},
			back() {
				this.$router.go(-1);
			},
			
			getdata: function() {
				let _this = this;
				_this.$request.post(
					"Api/Constellation_Admin/Get", {
						token_admin: _this.$utils.getloc("token_admin"),
						userid_admin: _this.$utils.getloc("userid_admin"),
						id: _this.cid
					},
					(res) => {
						_this.cons = JSON.parse(res.data.data.data);
						_this.fileList=[];
							var img = {
								url: _this.$config.img_url + _this.cons.Img
							};
						_this.fileList.push(img);
					}
				);
			},
			Panduan() {
				let _this = this;
				let mod = {
					pass: false,
					msg: "未知错误"
				}
				if (_this.cons.Name.length == 0) {
					mod.msg = "名称不能为空";
				}  else if (_this.cons.Minprice.length == 0) {
					mod.msg = "最低价格不能为空";
				} else if (_this.cons.Maxprice.length == 0) {
					mod.msg = "最高价格不能为空";
				}else if (_this.cons.Jindan.length == 0) {
				 	mod.msg = "押金丹不能为空";
				}else if (_this.cons.Zhouqi.length == 0) {
					mod.msg = "周期不能为空";
				}else if (_this.cons.Rishouyi.length == 0) {
					mod.msg = "日收益不能为空";
				}else if (_this.cons.kucun == "") {
					mod.msg = "每日限购不能为空";
				}else if (_this.cons.qdate == "") {
					mod.msg = "抢单时间不能为空";
				}else if (_this.fileList.length == 0) {
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
					_this.$confirm('确定要修改吗?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						type: 'warning'
					}).then(() => {
						_this.$request.post(
							"Api/Constellation_Admin/Update", {
								token_admin: _this.$utils.getloc("token_admin"),
								userid_admin: _this.$utils.getloc("userid_admin"),
								cons: JSON.stringify(_this.cons)
							},
							(res) => {
								_this.$notify({
									title: "提示",
									message: res.data.msg,
									offset: 100,
									type: "success",
								});
								_this.getdata();
							}
						);
					}).catch(() => {

					});
				} else {
					_this.$notify({
						title: "提示",
						message: mod.msg,
						offset: 100,
						type: "warning",
					});
				}
			},
			getimg: function(img) {
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
