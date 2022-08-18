<template>
	<div>
		<PageTitle title="添加大类" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="150px">
					<el-form-item label="大类名称">
						<el-input type="text" v-model="form.daleiname" placeholder="请输入大类名称"></el-input>
					</el-form-item>
					<el-form-item label="排序">
						<el-input-number size="mini" v-model="form.daleiorder" :min="1"></el-input-number>
					</el-form-item>

					<el-form-item label="是否上架">
						<el-switch v-model="form.putaway"></el-switch>
					</el-form-item>
					<el-form-item label="是否展示">
						<el-switch v-model="form.pagemark"></el-switch>
					</el-form-item>
					<el-form-item label="展示类型">
						<el-radio v-model="form.pagemarklx" label="1">显示图片</el-radio>
						<el-radio v-model="form.pagemarklx" label="2">显示名称</el-radio>
						<el-radio v-model="form.pagemarklx" label="3">只显示商品</el-radio>
					</el-form-item>

					<el-form-item label="图片">
						<el-upload class="avatar-uploader" :action="UploadFile" :show-file-list="false" :on-success="handleAvatarSuccess"
						 :before-upload="beforeAvatarUpload">
							<img v-if="imageUrl" :src="getimg(imageUrl)" class="avatar" />
							<i v-else class="el-icon-plus avatar-uploader-icon"></i>
							<div slot="tip" class="el-upload__tip">
								{{$config.ImgRatio.Dalei.Tip}}
							</div>
						</el-upload>
					</el-form-item>

					<el-form-item>
						<el-button type="primary" size="mini" @click="Add">添加</el-button>
						<el-button @click="back" size="mini">返回</el-button>
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
		InputNumber,
		Switch,
		Radio,
		Notification,
	} from "element-ui";
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Button);
	Vue.use(Input);
	Vue.use(Upload);
	Vue.use(InputNumber);
	Vue.use(Switch);
	Vue.use(Radio);
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
		},

		data() {
			return {
				form: {
					daleiname: "",
					daleiorder: "",
					putaway: true,
					pagemark: true,
					pagemarklx: "1",
					daleiimg: "",
				},
				imageUrl: "",
				UploadFile: this.$config.send_url +
					"Api/Upload/UploadImagesAdmin?token_admin=" +
					encodeURIComponent(this.$utils.getloc("token_admin")) +
					"&userid_admin=" +
					this.$utils.getloc("userid_admin") + "&width=" + this.$config.ImgRatio.Dalei.Width + "&height=" + this.$config.ImgRatio.Dalei.Height,
			};
		},
		methods: {
			back() {
				this.$router.go(-1);
			},
			Add: function() {
				this.$confirm('确定要添加吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					this.$request.post(
						"Api/ShopGoodsSort_Admin/Add", {
							token_admin: this.$utils.getloc("token_admin"),
							userid_admin: this.$utils.getloc("userid_admin"),
							daleiname: this.form.daleiname,
							daleiorder: this.form.daleiorder,
							putaway: this.form.putaway ? 1 : 0,
							pagemark: this.form.pagemark ? 1 : 0,
							pagemarklx: this.form.pagemarklx,
							daleiimg: this.form.daleiimg,
						},
						(res) => {
							this.$notify({
								title: "提示",
								message: res.data.msg,
								offset: 100,
								type: "success",
							});
						}
					);
				}).catch(() => {

				});
			},

			getimg: function(img) {
				return this.$config.img_url + img;
			},
			handleAvatarSuccess(res) {
				this.imageUrl = res.data.imgname;
				this.form.daleiimg = this.imageUrl;
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
		width: 200px;
		line-height: 250px;
		text-align: center;
	}

	.avatar {
		width: 200px;
		display: block;
	}
</style>
