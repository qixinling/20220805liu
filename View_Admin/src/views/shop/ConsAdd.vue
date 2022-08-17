<template>
	<div style="margin-bottom: 200px">
		<PageTitle title="添加产品" :btn="false" :excel="false"></PageTitle>

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
					<el-form-item label="预约金丹">
						<el-input type="text" v-model="cons.Yyjindou" placeholder="请输入预约金丹" style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="领养金丹">
						<el-input type="text" v-model="cons.Lyjindou" placeholder="请输入领养金丹" style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="押金单">
						<el-input type="text" v-model="cons.Jindan" placeholder="请输入押金单" style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="周期">
						<el-input type="text" v-model="cons.Zhouqi" placeholder="请输入周期"  style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="日收益">
						<el-input type="text" v-model="cons.Rishouyi" placeholder="请输入日收益"  style="width:200px;"></el-input>%
					</el-form-item>
					<el-form-item label="抢单开始时间">
						<el-time-picker v-model="cons.Qdate" value-format="HH:mm:ss"></el-time-picker>
					</el-form-item>
					<el-form-item label="抢单结束时间">
						<el-time-picker v-model="cons.Edate" value-format="HH:mm:ss"></el-time-picker>
					</el-form-item>
					<el-form-item label="每日限购">
						<el-input type="text" v-model="cons.Kucun" placeholder="请输入每日限购"  style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="上架">
						<el-switch v-model="cons.State" :inactive-value="0" :active-value="1"></el-switch>
					</el-form-item>
					<el-form-item label="开放">
						<el-switch v-model="cons.Iskf" :inactive-value="0" :active-value="1"></el-switch>
					</el-form-item>
					
					<el-form-item label="图片">
						<el-upload class="upload-demo" :action="UploadFile" :on-success="handleAvatarSuccess1"
							:before-upload="beforeAvatarUpload" :on-remove="handleRemove" :file-list="fileList"
							:limit="6" list-type="picture">
							<el-button size="small" type="primary">点击上传</el-button>
							<div slot="tip" class="el-upload__tip">
								只能上传jpg/png文件，且不超过5MB
							</div>
							
						</el-upload>
					</el-form-item>
					
					<el-form-item>
						<el-button type="primary" @click="Add">添加</el-button>
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
				cons: {
					Name:"",
					Img:"",
					Minprice:"",
					Maxprice:"",
					Yyjindou:0,
					Lyjindou:0,
					Jindan:'',
					Zhouqi:'',
					Rishouyi:"",
					Qdate:"",
					Edate:'',
					State:1,
					Iskf:1,
					Kucun:''
					
				},
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
				this.$router.push({
					name: "ConsList",
				});
			},
			
			Panduan() {
				let _this = this;
				let mod = {
					pass: false,
					msg: "未知错误"
				}
				console.log(_this.cons);
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
				}else if (_this.cons.Kucun.length == 0) {
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
			Add() {
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
							"Api/Constellation_Admin/Add",
							{
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