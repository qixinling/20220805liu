<template>
	<div>
		<PageTitle title="小类修改" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="150px">
					<el-form-item label="小类名称">
						<el-input type="text" v-model="xiaoleiname" placeholder="请输入小类名称"></el-input>
					</el-form-item>

					<el-form-item label="大类">
						<el-select  placeholder="选择所属的大类">
							<el-option v-for="item in sort_child_list" :key="item.id" :label="item.daleiname" :value="item.id"></el-option>
						</el-select>
					</el-form-item>
					<el-form-item label="排序">
						<el-input-number size="mini" v-model="xiaoleiorder" :min="1"></el-input-number>
					</el-form-item>

					<el-form-item label="是否上架">
						<el-switch v-model="putaway"></el-switch>
					</el-form-item>
					<el-form-item label="是否展示">
						<el-switch v-model="pagemark"></el-switch>
					</el-form-item>

					<el-form-item label="图片">
						<el-upload class="avatar-uploader" :action="UploadFile" :show-file-list="false" :on-success="handleAvatarSuccess"
						 :before-upload="beforeAvatarUpload">
							<img v-if="imageUrl" :src="getimg(imageUrl)" class="avatar" />
							<i v-else class="el-icon-plus avatar-uploader-icon"></i>
							<div slot="tip" class="el-upload__tip">
								{{$config.ImgRatio.Xiaolei.Tip}}
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
	Vue.prototype.$notify = Notification;
	export default {
		components: {
			PageTitle,
		},
		data() {
			return {
				xid: "",
				sid: "",
				xiaoleiname: "",
				xiaoleiorder: "",
				sort_list: [{}],
				xiaoleiimg: "",
				pagemark: true,
				putaway: true,
				sort_child_list: [],
				imageUrl: "",
				UploadFile: this.$config.send_url +
					"Api/Upload/UploadImagesAdmin?token_admin=" +
					encodeURIComponent(this.$utils.getloc("token_admin")) +
					"&userid_admin=" +
					this.$utils.getloc("userid_admin") + "&width=" + this.$config.ImgRatio.Xiaolei.Width + "&height=" + this.$config.ImgRatio.Xiaolei.Height,
			};
		},
		created() {
			this.xid = this.$route.query.id;
			this.sortselect();
			this.getdata();
		},
		methods: {
			back() {
				this.$router.go(-1);
			},
			getdata: function() {
				this.$request.post(
					"Api/ShopGoodsSortChild_Admin/Get", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id: this.xid,
					},
					(res) => {
						this.sid = res.data.data.sid;
						this.xiaoleiname = res.data.data.xiaoleiname;
						this.xiaoleiorder = res.data.data.xiaoleiorder;
						this.pagemark = res.data.data.pagemark == 0 ? false : true;
						this.xiaoleiimg = res.data.data.xiaoleiimg;
						this.imageUrl = this.xiaoleiimg;
						this.putaway = this.putaway == 0 ? false : true;
					}
				);
			},
			Update: function() {
				this.$confirm('确定要修改吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					this.$request.post(
						"Api/ShopGoodsSortChild_Admin/Update", {
							token_admin: this.$utils.getloc("token_admin"),
							userid_admin: this.$utils.getloc("userid_admin"),
							id: this.xid,
							sid: this.sid,
							xiaoleiname: this.xiaoleiname,
							xiaoleiorder: this.xiaoleiorder,
							pagemark: this.pagemark ? 1 : 0,
							xiaoleiimg: this.xiaoleiimg,
							putaway: this.putaway ? 1 : 0,
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
			sortselect: function() {
				this.$request.post(
					"Api/ShopGoodsSort_Admin/List_Name", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						this.sort_child_list = res.data.data;
					}
				);
			},

			getimg: function(img) {
				return this.$config.img_url + img;
			},
			handleAvatarSuccess(res) {
				this.imageUrl = res.data.imgname;
				this.xiaoleiimg = this.imageUrl;
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
