<template>
	<div class="shopimgadd">
		<PageTitle title="添加商城图片" :btn="false" :excel="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="80px">
					<el-form-item label="类型">
						<el-select v-model="mrlx" placeholder="请选择" @change="lxChange">
							<el-option value="1" label="单张"></el-option>
							<el-option value="2" label="三张"></el-option>
						</el-select>
					</el-form-item>

					<el-form-item label="图片" style="width: 40%;float: left;">
						<el-upload class="upload-demo" :action="UploadFile" :on-success="handleAvatarSuccess6" :before-upload="beforeAvatarUpload"
						 :on-remove="handleRemove" :file-list="fileList" :limit="fileCount" list-type="picture">
							<el-button size="small" type="primary">点击上传</el-button>
							<div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过5MB</div>
							<div slot="tip" class="el-upload__tip" v-if="lx==1">像素大约(651*135)</div>
							<div slot="tip" class="el-upload__tip" v-if="lx==2" style="width: 150%;">第一张像素大约(280*360),第二第三张像素大约(198*135)</div>
						</el-upload>
					</el-form-item>

					<el-form-item style="margin-top: 105px;float: left;">
						<div v-if="fileList.length>0">
							<div class="bdselect">选择商品或新闻绑定图片,不绑定则不选择</div>
							<el-select v-model="mrblx1" placeholder="请选择" @change="blxChange1">
								<el-option value="0" label="请选择"></el-option>
								<el-option value="1" label="商品"></el-option>
								<el-option value="2" label="新闻"></el-option>
							</el-select>
							<el-select v-model="gname1" placeholder="请选择" @change="gChange1" class="selectbd" v-if="blx1==1">
								<el-option :value="item.id" :label="item.goodsname" v-for="(item, index) of goodslist" :key="index"></el-option>
							</el-select>
							<el-select v-model="gname1" placeholder="请选择" @change="gChange1" class="selectbd" v-if="blx1==2">
								<el-option :value="item.id" :label="item.news_title" v-for="(item, index) of newslist" :key="index"></el-option>
							</el-select>
						</div>

						<div v-if="lx==2 && fileList.length>=2">
							<div class="bdselect">选择商品或新闻绑定图片,不绑定则不选择</div>
							<el-select v-model="mrblx2" placeholder="请选择" @change="blxChange2">
								<el-option value="0" label="请选择"></el-option>
								<el-option value="1" label="商品"></el-option>
								<el-option value="2" label="新闻"></el-option>
							</el-select>
							<el-select v-model="gname2" placeholder="请选择" @change="gChange2" class="selectbd" v-if="blx2==1">
								<el-option :value="item.id" :label="item.goodsname" v-for="(item, index) of goodslist" :key="index"></el-option>
							</el-select>
							<el-select v-model="gname2" placeholder="请选择" @change="gChange2" class="selectbd" v-if="blx2==2">
								<el-option :value="item.id" :label="item.news_title" v-for="(item, index) of newslist" :key="index"></el-option>
							</el-select>
						</div>
						<div v-if="lx==2 && fileList.length==3">
							<div class="bdselect">选择商品或新闻绑定图片,不绑定则不选择</div>
							<el-select v-model="mrblx3" placeholder="请选择" @change="blxChange3">
								<el-option value="0" label="请选择"></el-option>
								<el-option value="1" label="商品"></el-option>
								<el-option value="2" label="新闻"></el-option>
							</el-select>
							<el-select v-model="gname3" placeholder="请选择" @change="gChange3" class="selectbd" v-if="blx3==1">
								<el-option :value="item.id" :label="item.goodsname" v-for="(item, index) of goodslist" :key="index"></el-option>
							</el-select>
							<el-select v-model="gname3" placeholder="请选择" @change="gChange3" class="selectbd" v-if="blx3==2">
								<el-option :value="item.id" :label="item.news_title" v-for="(item, index) of newslist" :key="index"></el-option>
							</el-select>
						</div>
					</el-form-item>

				</el-form>
				<el-row class="elrow" style="margin-top: 500px;">
					<el-col :md="6" :sm="24" :span="24" style="margin-bottom: 10px;">
						<el-button type="success" size="mini" @click="add" icon="el-icon-circle-plus-outline">添加</el-button>
						<el-button type="danger" size="mini" icon="el-icon-back" @click="tolink('ShopimgList')">返回</el-button>
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
		Upload,
		Notification
	} from 'element-ui';
	Vue.use(Select);
	Vue.use(Option);
	Vue.use(Input);
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Upload);
	Vue.prototype.$notify = Notification;
	export default {
		name: 'shopimgadd',
		components: {
			PageTitle
		},
		data() {
			return {
				goodslist: [],
				gname1: '',
				gname2: '',
				gname3: '',
				gid1: 0,
				gid2: 0,
				gid3: 0,
				newslist: [],
				blx1: 0,
				blx2: 0,
				blx3: 0,
				mrblx1: '',
				mrblx2: '',
				mrblx3: '',
				mrlx: '单张',
				lx: 1,
				img1label: '纵横比1:1.5', //  360*230
				UploadFile: this.$config.send_url + 'Api/Upload/UploadImagesAdmin?token_admin=' +
					encodeURIComponent(this.$utils.getloc('token_admin')) + '&userid_admin=' + this.$utils.getloc('userid_admin'),   //  + "&width=" + 600 + "&height=" + 300
				fileList: [],
				fileCount: 1
			}
		},
		created() {
			this.getgoods();
			this.getnews();
		},
		methods: {
			handleRemove(file, fileList) {
				this.fileList = fileList;
				if (fileList.length == 0) {
					this.mrblx1 = '';
					this.blx1 = 0;
					this.gname1 = '';
					this.gid1 = 0;
				} else if (fileList.length == 1) {
					this.mrblx2 = '';
					this.gname2 = '';
					this.gid2 = 0;
					this.blx2 = 0;

					this.mrblx3 = '';
					this.gname3 = '';
					this.gid3 = 0;
					this.blx3 = 0;
				} else if (fileList.length == 2) {
					this.mrblx3 = '';
					this.blx3 = 0;
					this.gname3 = '';
					this.gid3 = 0;
				}
			},
			handleAvatarSuccess6(res, file, fileList) { // 新图片
				if (this.fileList.length >= 3) {
					this.$notify({
						title: '提示',
						message: '最多只能添加三张图片',
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
			blxChange1: function(blx) {
				if (blx == 0) {
					this.gid1 = 0;
					this.gname1 = "";
				} else if (blx == 1) {
					this.blx1 = 1;
					this.gid1 = this.goodslist[0].id;
					this.gname1 = this.goodslist[0].goodsname;
				} else if (blx == 2) {
					this.blx1 = 2;
					this.gid1 = this.newslist[0].id;
					this.gname1 = this.newslist[0].news_title;
				}
			},
			blxChange2: function(blx) {
				if (blx == 0) {
					this.gid2 = 0;
					this.gname2 = "";
				} else if (blx == 1) {
					this.blx2 = 1;
					this.gid2 = this.goodslist[0].id;
					this.gname2 = this.goodslist[0].goodsname;
				} else if (blx == 2) {
					this.blx2 = 2;
					this.gid2 = this.newslist[0].id;
					this.gname2 = this.newslist[0].news_title;
				}
			},
			blxChange3: function(blx) {
				if (blx == 0) {
					this.gid3 = 0;
					this.gname3 = "";
				} else if (blx == 1) {
					this.blx3 = 1;
					this.gid3 = this.goodslist[0].id;
					this.gname3 = this.goodslist[0].goodsname;
				} else if (blx == 2) {
					this.blx3 = 2;
					this.gid3 = this.newslist[0].id;
					this.gname3 = this.newslist[0].news_title;
				}
			},
			gChange1: function(gid) { // 获取选中商品id
				this.gid1 = gid;
			},
			gChange2: function(gid) {
				this.gid2 = gid;
			},
			gChange3: function(gid) {
				this.gid3 = gid;
			},
			getgoods: function() {
				this.$request.post("Api/ShopGoods_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.goodslist = res.data.data;
					//console.log(this.goodslist);
				});
			},
			getnews: function() {
				this.$request.post("Api/News_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.newslist = res.data.data;
				});
			},
			tolink: function(_path) {
				this.$router.push({
					path: _path
				})
			},
			lxChange: function(e) { // 类型变化
				this.lx = e;
				if (e == 1) {
					this.fileCount = 1;
					this.fileList = this.fileList.reverse();
					var fileCount = this.fileList.length;
					this.fileList.forEach((item, index) => {
						var i = index + 1;
						if (i < fileCount) {
							this.fileList.splice(0, 1);
						}
					})
				} else {
					this.fileCount = 3;
				}
			},

			add: function() {
				var _this = this;
				var imgurl = "";
				var gid = "";
				var bdlx = "";
				var img_url = this.$config.img_url.length;
				if (_this.lx == 1) {
					if (_this.fileList.length == 0) {
						Notification({
							message: '请上传图片',
							offset: 100,
							type: 'warning'
						});
						return;
					}
					var url = _this.fileList[0].url.substring(img_url, _this.fileList[0].url.length);
					imgurl = url;

					gid = _this.gid1;
					bdlx = _this.blx1;
				} else {
					if (_this.fileList.length != 3) {
						Notification({
							message: '请上传3张图片',
							offset: 100,
							type: 'warning'
						});
						return;
					}

					var url1 = _this.fileList[0].url.substring(img_url, _this.fileList[0].url.length);
					var url2 = _this.fileList[1].url.substring(img_url, _this.fileList[1].url.length);
					var url3 = _this.fileList[2].url.substring(img_url, _this.fileList[2].url.length);

					imgurl = url1 + "," + url2 + "," + url3;

					gid = _this.gid1 + "," + _this.gid2 + "," + _this.gid3;
					if (_this.gid1 == 0) {
						_this.blx1 = 0;
					}
					if (_this.gid2 == 0) {
						_this.blx2 = 0;
					}
					if (_this.gid3 == 0) {
						_this.blx3 = 0;
					}
					bdlx = _this.blx1 + "," + _this.blx2 + "," + _this.blx3;
				}
				_this.$confirm('确定要添加吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Shopimg_Admin/Add", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						lx: _this.lx,
						imgurl: imgurl,
						gid: gid,
						bdlx: bdlx
					}, (res) => {
						Notification({
							title: '操作成功',
							message: res.data.msg,
							offset: 100,
							type: 'success'
						});
						_this.fileList = [];
						_this.mrlx = '单张';
						_this.lx = 1;
						_this.mrblx1 = '请选择';
						_this.gid1 = 0;
						_this.gname1 = '';
					});
				}).catch(() => {

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
	.shopimgadd {
		
	}

	.img1,
	.img2,
	.img3 {
		float: left;
		padding-right: 30px;
	}

	.img1 img,
	.img2 img,
	.img3 img {
		width: 200px;
	}

	.img1 i,
	.img2 i,
	.img3 i {
		width: 200px;
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

	.selectbd {
		margin-left: 20px;
	}

	.selectbd {
		margin-left: 20px;
	}

	.bdselect {
		margin-top: 20px;
	}
</style>
