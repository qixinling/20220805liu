<template>
	<div>
		<PageTitle title="商品修改" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="150px">
					<el-form-item label="编号">
						<el-input type="text" v-model="goods.GoodsNo" placeholder="请输入编号"></el-input>
					</el-form-item>
					<el-form-item label="名称">
						<el-input type="text" v-model="goods.Goodsname" placeholder="请输入名称"></el-input>
					</el-form-item>
					<el-form-item label="提示语">
						<el-input type="text" v-model="goods.Prompt" placeholder="请输入提示语"></el-input>
					</el-form-item>
					<el-form-item label="类型">
						<el-select v-model="goods.Goodstype" placeholder="请选择类型">
							<el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value"></el-option>
						</el-select>
					</el-form-item>
					<el-form-item label="分类">
						<el-select v-model="goods.Xlid" placeholder="请选择分类">
							<el-option v-for="item in sort_child_list" :key="item.id" :label="item.xiaoleiname" :value="item.id"></el-option>
						</el-select>
					</el-form-item>
					<el-form-item label="零售价">
						<el-input-number size="mini" v-model="goods.Goodsprice" :min="1" :precision="2">
						</el-input-number>
					</el-form-item>
					<el-form-item label="信用值" >
						<el-input-number size="mini" v-model="goods.Xyzjine" :min="1" :precision="2">
						</el-input-number>
					</el-form-item>
					<el-form-item label="库存">
						<el-input-number size="mini" v-model="goods.Stock" :min="1"></el-input-number>
					</el-form-item>
					<el-form-item label="排序">
						<el-input-number size="mini" v-model="goods.Sort" :min="1"></el-input-number>
					</el-form-item>
					<el-form-item label="上架">
						<el-switch v-model="goods.Ispay" :inactive-value="0" :active-value="1"></el-switch>
					</el-form-item>
					<el-form-item label="首页展示">
						<el-radio v-model="goods.Ishome" :label="0">不展示</el-radio>
						<el-radio v-model="goods.Ishome" :label="1">展示</el-radio>
					</el-form-item>
					<el-form-item label="图片">
						<el-upload class="upload-demo" :action="UploadFile" :on-success="handleAvatarSuccess1" :before-upload="beforeAvatarUpload"
						 :on-remove="handleRemove" :file-list="fileList" :limit="6" list-type="picture">
							<el-button size="small" type="primary">点击上传</el-button>
							<div slot="tip" class="el-upload__tip">
								只能上传jpg/png文件，且不超过5MB
							</div>
							<div slot="tip" class="el-upload__tip">
								{{$config.ImgRatio.Goods.Tip}}
							</div>
						</el-upload>
					</el-form-item>
					<el-form-item label="详情">
						<WangEditor v-model="goods.Goodscontent" :isClear="isClear" @change="change"></WangEditor>
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
	import WangEditor from "../../components/WangEditor";
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
			WangEditor,
		},
		data() {
			return {
				gid:'',
				goods: {},
				isClear: false,
				sort_child_list: [],
				options: [
					{
						value: 1,
						label: "商城产品",
					},
					{
						value: 0,
						label: "激活商品",
					},
					
				],
				UploadFile: this.$config.send_url +
					"Api/Upload/UploadImagesAdmin?token_admin=" +
					encodeURIComponent(this.$utils.getloc("token_admin")) +
					"&userid_admin=" +
					this.$utils.getloc("userid_admin") + "&width=" + this.$config.ImgRatio.Goods.Width + "&height=" + this.$config.ImgRatio.Goods.Height,
				fileList: [],
				contentcl: "",
			};
		},
		created() {
			this.gid = this.$route.query.Id;
			this.getdata();
			this.GetXiaolei();
		},
		methods: {
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
			handleRemove(file, fileList) {
				this.fileList = fileList;
			},
			handleAvatarSuccess1(res, file, fileList) {
				// 新图片
				if (this.fileList.length >= 6) {
					this.$notify({
						title: "提示",
						message: "每个商品只能添加6张图片",
						offset: 100,
						type: "warning",
					});
					return;
				}

				var img = {
					url: this.$config.img_url + res.data.imgname,
				};
				this.fileList.push(img);
			},
			back() {
				this.$router.go(-1);
			},
			GetXiaolei() {
				let _this = this;
				_this.$request.post(
					"Api/ShopGoodsSortChild_Admin/List_Name", {
						token_admin: _this.$utils.getloc("token_admin"),
						userid_admin: _this.$utils.getloc("userid_admin"),
					},
					(res) => {
						_this.sort_child_list = res.data.data;
					}
				);
			},
			getdata: function() {
				let _this = this;
				_this.$request.post(
					"Api/ShopGoods_Admin/Get", {
						token_admin: _this.$utils.getloc("token_admin"),
						userid_admin: _this.$utils.getloc("userid_admin"),
						gid: _this.gid
					},
					(res) => {
						_this.goods = JSON.parse(res.data.data.data);
						var imglist = _this.goods.Goodsimg.split(",");
						var newlist = [];
						imglist.forEach((item) => {
							var img = {
								url: _this.$config.img_url + item
							};
							newlist.push(img);
						});
						_this.fileList = newlist;

						let nr = _this.goods.Goodscontent;
						let num = nr.split('img').length - 1;
						for (let i = 1; i <= num; i++) {
							nr = nr.replace('"admin', '"' + _this.$config.img_url + 'admin');
						}
						_this.goods.Goodscontent = nr;
					}
				);
			},
			Panduan() {
				let _this = this;
				let mod = {
					pass: false,
					msg: "未知错误"
				}
				if (_this.goods.GoodsNo.length == 0) {
					mod.msg = "编号不能为空";
				} else if (_this.goods.Goodsname.length == 0) {
					mod.msg = "名称不能为空";
				} else if (_this.goods.Goodsprice.length == 0) {
					mod.msg = "零售价不能为空";
				} else if (_this.goods.Stock.length == 0) {
					mod.msg = "库存不能为空";
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
					_this.$confirm('确定要修改吗?', '提示', {
						confirmButtonText: '确定',
						cancelButtonText: '取消',
						type: 'warning'
					}).then(() => {
						var imglist = "";
						_this.fileList.forEach((item, index) => {
							var img_url = _this.$config.img_url.length;
							var url = item.url.substring(img_url, item.url.length);
							imglist = index == 0 ? imglist += url : imglist += "," + url;
						});
						_this.goods.Goodsimg = imglist;
						_this.goods.Goodscontent.length == 0 ? "-" : _this.goods.Goodscontent;
						_this.$request.post(
							"Api/ShopGoods_Admin/Update", {
								token_admin: _this.$utils.getloc("token_admin"),
								userid_admin: _this.$utils.getloc("userid_admin"),
								goods: JSON.stringify(_this.goods)
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
