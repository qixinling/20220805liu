<template>
	<div class="slide">
		<PageTitle title="修改幻灯片" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="80px">
					<el-form-item label="展示页面">
						<el-select v-model="mrpagelx" placeholder="请选择" @change="pagelxChange">
							<el-option value="1" label="商城"></el-option>
							<el-option value="2" label="公告"></el-option>
						</el-select>
					</el-form-item>
					<el-form-item label="关联类型">
						<el-select v-model="mrlx" placeholder="请选择" @change="lxChange">
							<el-option value="1" label="无关联"></el-option>
							<el-option value="2" label="商品"></el-option>
							<el-option value="3" label="新闻"></el-option>
							<el-option value="4" label="外部网址"></el-option>
							<el-option value="5" label="指定商品分类"></el-option>
						</el-select>
					</el-form-item>
					<el-form-item label="商品选择" v-if="lx==2">
						<el-select v-model="mrgid" placeholder="请选择" @change="goodsChange">
							<el-option v-for="(item, index) in goodslist" :key="index" :label="item.goodsname" :value="index">
							</el-option>
						</el-select>
					</el-form-item>
					<el-form-item label="新闻选择" v-if="lx==3">
						<el-select v-model="mrgid" placeholder="请选择" @change="newsChange">
							<el-option v-for="(item, index) in newslist" :key="index" :label="item.news_title" :value="index">
							</el-option>
						</el-select>
					</el-form-item>
					<el-form-item label="外部网址" v-if="lx==4">
						<el-input v-model="url"></el-input>
					</el-form-item>
					<el-form-item label="商品分类" v-if="lx==5">
						<el-select v-model="mrgid" placeholder="请选择" @change="goodsclassChange">
							<el-option v-for="(item, index) in goodsclasslist" :key="index" :label="item.xiaoleiname" :value="index">
							</el-option>
						</el-select>
					</el-form-item>
					<el-form-item label="图片">
						<el-upload class="avatar-uploader" :action="UploadFile" :show-file-list="false" :on-success="handleAvatarSuccess"
						 :before-upload="beforeAvatarUpload">
							<img v-if="imageUrl" :src="getimg(imageUrl)" class="avatar" />
							<i v-else class="el-icon-plus avatar-uploader-icon"></i>
						</el-upload>
						<!-- <span style="font-size: 12px;color: #777;">{{$config.ImgRatio.Slide.Tip}}</span> -->
					</el-form-item>
				</el-form>
				<el-row class="elrow">
					<el-col :md="6" :sm="24" :span="24" style="margin-bottom: 10px;">
						<el-button type="success" size="mini" @click="update" icon="el-icon-edit">修改</el-button>
						<el-button size="mini" icon="el-icon-back" @click="tolink('Slide')">返回</el-button>
					</el-col>
				</el-row>
			</div>
		</div>
	</div>
</template>

<script>
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
		name: 'slideupdate',
		components: {
			PageTitle
		},
		data() {
			return {
				goodsclasslist: [],
				goodslist: [],
				newslist: [],
				mrpagelx: "商城",
				pagelx: 1,
				mrlx: "无关联",
				lx: 1,
				mrgid: "",
				gid: "",
				url: '',
				sid: 0,
				imageUrl: "",
				UploadFile: this.$config.send_url +
					"Api/Upload/UploadImagesAdmin?token_admin=" +
					encodeURIComponent(this.$utils.getloc("token_admin")) +
					"&userid_admin=" +
					this.$utils.getloc("userid_admin"),
			}
		},
		created() {
			if (this.$route.query.id != "") {
				this.sid = this.$route.query.id;
				this.getgoodslist();
				this.getgoodsclasslist();
				this.getnewslist();
				this.getdata();
			}
		},
		methods: {
			tolink: function(_path) {
				this.$router.push({
					path: _path
				})
			},
			goodsclassChange: function(e) {
				this.mrgid = e;
				this.gid = this.goodsclasslist[e].id;
			},
			goodsChange: function(e) {
				this.mrgid = e;
				this.gid = this.goodslist[e].id;
			},
			newsChange: function(e) {
				this.mrgid = e;
				this.gid = this.newslist[e].id;
			},
			pagelxChange: function(e) {
				this.pagelx = e;
			},
			lxChange: function(e) {
				this.lx = e;

				if (e == 2) { // 商品
					this.getgoods(this.goodslist[0].id);
					this.gid = this.goodslist[0].id;
				} else if (e == 3) { // 新闻
					this.getnews(this.newslist[0].id);
					this.gid = this.newslist[0].id;
				} else if (e == 4) {
					this.lx = e;
					this.gid = '';
					this.url = '';
				} else if (e == 5) { // 指定分类
					this.getgoodsclass(this.goodsclasslist[0].id);
					this.gid = this.goodsclasslist[0].id;
				}
			},
			getgoods: function(id) { // 查询商品
				var _this = this;
				_this.$request.post("Api/ShopGoods_Admin/Get", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin'),
					gid: id
				}, (res) => {
					//_this.mrgid = res.data.data.goodsname;
					var goods = JSON.parse(res.data.data.data);
					_this.mrgid = goods.Goodsname;
				});
			},
			getgoodsclass: function(id) { // 查询商品分类
				var _this = this;
				_this.$request.post("Api/ShopGoodsSortChild_Admin/Get", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin'),
					id: id
				}, (res) => {
					_this.mrgid = res.data.data.xiaoleiname;
				});
			},
			getnews: function(id) { // 查询新闻
				var _this = this;
				_this.$request.post("Api/News_Admin/Get", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin'),
					id: id
				}, (res) => {
					_this.mrgid = res.data.data.news_title;
				});
			},
			getdata: function() { // 查询幻灯片
				var _this = this;
				_this.$request.post("Api/Slide_Admin/Get", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin'),
					select_id: _this.sid
				}, (res) => {
					var slide = res.data.data;
					_this.mrlx = slide.lx;
					_this.lx = slide.lx;
					_this.mrpagelx = slide.pagelx;
					_this.pagelx = slide.pagelx;
					_this.imageUrl = slide.img;

					if (slide.lx == 2) {
						_this.getgoods(slide.gid);
						_this.gid = slide.gid;
					} else if (slide.lx == 3) {
						_this.getnews(slide.gid);
						_this.gid = slide.gid;
					} else if (slide.lx == 4) {
						_this.url = slide.url;
					} else if (slide.lx == 5) {
						_this.getgoodsclass(slide.gid);
						_this.gid = slide.gid;
					}
				});
			},
			getgoodslist: function() { // 商品集合
				var _this = this;
				_this.$request.post("Api/ShopGoods_Admin/List", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin')
				}, (res) => {
					//_this.goodslist = res.data.data;
					_this.goodslist = res.data.data;
				});
			},
			getgoodsclasslist: function() { // 商品分类集合
				var _this = this;
				_this.$request.post("Api/ShopGoodsSortChild_Admin/List", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin')
				}, (res) => {
					_this.goodsclasslist = res.data.data;
				});
			},
			getnewslist: function() { // 新闻集合
				var _this = this;
				_this.$request.post("Api/News_Admin/List", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin')
				}, (res) => {
					_this.newslist = res.data.data;
				});
			},

			update: function() {
				var _this = this;
				var gid = _this.gid;
				var url = _this.url;
				if (_this.lx == 1) {
					gid = 0;
				} else if (_this.lx == 2) {
					if (gid == "") {
						Notification({
							message: '请选择商品',
							offset: 100,
							type: 'warning'
						});
						return;
					}
				} else if (_this.lx == 3) {
					if (gid == "") {
						Notification({
							message: '请选择新闻',
							offset: 100,
							type: 'warning'
						});
						return;
					}
				} else if (_this.lx == 4) {
					gid = 0;
					if (_this.url == "") {
						Notification({
							message: '请输入外部网址',
							offset: 100,
							type: 'warning'
						});
						return;
					}
				} else if (_this.lx == 5) {
					if (gid == "") {
						Notification({
							message: '请选择商品分类',
							offset: 100,
							type: 'warning'
						});
						return;
					}
				}

				if (_this.lx != 4) {
					url = "/";
				}

				if (_this.imageUrl == "" || _this.imageUrl == "null.jpg") {
					Notification({
						message: '请上传图片',
						offset: 100,
						type: 'warning'
					});
					return;
				}
				_this.$confirm('确定要修改吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Slide_Admin/Update", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						id: _this.sid,
						lx: _this.lx,
						pagelx: _this.pagelx,
						gid: gid,
						url: url,
						img: _this.imageUrl
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
					});
				}).catch(() => {

				});
			},
			getimg: function(img) {
				return this.$config.img_url + img;
			},
			handleAvatarSuccess(res) {
				if (res.code == 100) {
					this.imageUrl = res.data.imgname;
				} else {
					this.$message.error(res.msg);
				}
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
	.slide {
		
	}

	.elrow {
		margin-bottom: 20px;
		padding-left: 15px;
		font-size: 14px;
		color: #606266;
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

	.el-input {
		width: 30%;
	}
</style>
