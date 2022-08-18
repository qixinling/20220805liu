<template>
	<div class="newsadd">
		<PageTitle title="添加新闻" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="80px">
					<el-form-item label="标题">
						<el-input v-model="title"></el-input>
					</el-form-item>
					<el-form-item label="是否发布">
						<el-select v-model="mrdisplay" placeholder="请选择" @change="displayChange">
							<el-option value="1" label="是"></el-option>
							<el-option value="0" label="否"></el-option>
						</el-select>
					</el-form-item>

					<el-form-item label="封面" style="width: 40%;">
						<el-upload class="upload-demo" :action="UploadFile" :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload"
						 :on-remove="handleRemove" :file-list="fileList" :limit="fileCount" list-type="picture">
							<el-button size="small" type="primary">点击上传</el-button>
							<div slot="tip" class="el-upload__tip">宽高比2 : 1,只能上传jpg/png文件，且不超过5MB</div>
						</el-upload>
					</el-form-item>

					<el-form-item label="新闻内容">
						<WangEditor v-model="content" :isClear="isClear" @change="change"></WangEditor>
					</el-form-item>
					<el-form-item>
						<el-button type="success" size="mini" @click="add" icon="el-icon-circle-plus-outline">添加</el-button>
						<el-button size="mini" icon="el-icon-back" @click="tolink('News')">返回</el-button>
					</el-form-item>
				</el-form>
			</div>
		</div>
	</div>
</template>

<script>
	import WangEditor from '../../components/WangEditor';
	import Vue from 'vue';
	import PageTitle from '../../components/PageTitle';
	import {
		Select,
		Option,
		Input,
		Form,
		FormItem,
		Notification,
		Upload
	} from 'element-ui';
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Input);
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Upload);
	Vue.prototype.$notify = Notification;
	export default {
		name: 'newsadd',
		components: {
			PageTitle,
			WangEditor
		},
		data() {
			return {
				mrdisplay: "是",
				display: 1,
				title: "",
				content: "",
				isClear: false,
				fileList: [],
				fileCount: 1,
				contentcl: '',
				UploadFile: this.$config.send_url + 'Api/Upload/UploadImagesAdmin?token_admin=' + encodeURIComponent(this.$utils
					.getloc('token_admin')) + '&userid_admin=' + this.$utils.getloc('userid_admin') + "&width=" + 600 + "&height=" + 300
			}
		},
		methods: {
			handleRemove(file, fileList) {
				this.fileList = fileList;
			},
			handleAvatarSuccess(res, file, fileList) { // 新图片
				if (this.fileList.length >= 1) {
					this.$notify({
						title: '提示',
						message: '只能添加一张图片',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				
				var img = {
					url: this.$config.img_url + res.data.imgname
				}
				this.fileList.push(img);
			},
			tolink: function(_path) {
				this.$router.push({
					path: _path
				})
			},
			change(val) {
				let num = val.split('img').length - 1;
				let content = val;
				for (let i = 0; i < num; i++) {
					let q = content.substring(0, content.indexOf('https'));
					let h = content.substring(content.indexOf('d/') + 2, content.length);
					content = q + h;
				}
				this.contentcl = content;
			},
			displayChange: function(e) {
				this.display = e;
			},
			add: function() {
				var _this = this;
				if (_this.title == "") {
					Notification({
						message: "请填写新闻标题",
						offset: 100,
						type: 'warning'
					});
					return;
				}
				if (_this.content == "") {
					Notification({
						message: "请填写新闻内容",
						offset: 100,
						type: 'warning'
					});
					return;
				}
				
				
				var cover = "";
				if (_this.fileList.length > 0) {
					var img_url = this.$config.img_url.length;
					cover = _this.fileList[0].url.substring(img_url, _this.fileList[0].url.length);
				}
				

				this.$request.post("Api/News_Admin/Add", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin'),
					news_cover: cover,
					news_title: _this.title,
					// news_content: _this.content,
					news_content: _this.contentcl,
					display: _this.display
				}, (res) => {
					Notification({
						title: '操作成功',
						message: res.data.msg,
						offset: 100,
						type: 'success'
					});

					_this.mrdisplay = "是";
					_this.display = 1;
					_this.title = "";
					_this.content = "";
					_this.fileList = [];
				});
			},
			getimg: function(img) {
				return this.$config.img_url + img;
			},
			beforeAvatarUpload(file) {
				const isJPG = file.type === 'image/jpeg';
				const isPNG = file.type === 'image/png';
				var isIMG = false;
				const isLt5M = file.size / 1024 / 1024 < 5;
				if (isJPG || isPNG) {
					isIMG = true;
				} else {
					this.$message.error('上传头像图片只能是 JPG/PNG 格式!');
				}
				if (!isLt5M) {
					this.$message.error('上传头像图片大小不能超过 5MB!');
				}
				return isIMG && isLt5M;
			}
		}
	}
</script>

<style scoped="scoped">
	.avatar-uploader-icon {
		font-size: 28px;
		color: #8c939d;
		width: 300px;
		height: 250px;
		line-height: 250px;
		text-align: center;
	}

	/deep/.avatar-uploader /deep/.el-upload {
		border: 1px dashed #d9d9d9;
		border-radius: 6px;
		cursor: pointer;
		position: relative;
		overflow: hidden;
	}

	.avatar-uploader .el-upload:hover {
		border-color: #409EFF;
	}

	.avatar-uploader-icon {
		font-size: 28px;
		color: #8c939d;
		width: 300px;
		height: 250px;
		line-height: 250px;
		text-align: center;
	}

	.avatar {
		width: 300px;
		display: block;
	}

	.el-upload__tip {
		width: 300px;
	}
</style>
