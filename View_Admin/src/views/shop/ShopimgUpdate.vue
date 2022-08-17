<template>
	<div class="shopimgupdate">
		<PageTitle title="修改商城图片" :btn="false" :excel="false"></PageTitle>
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
						<el-upload class="upload-demo" :action="UploadFile" :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload"
						 :on-remove="handleRemove" :file-list="fileList" :limit="fileCount" list-type="picture">
							<el-button size="small" type="primary">点击上传</el-button>
							<div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过5MB</div>
							<div slot="tip" v-if="lx==1">宽高比1.2 : 1</div>
							<div slot="tip" v-if="lx==2" style="width: 150%;">第一张宽高比1.5 : 1,第二第三张宽高比1.2 : 1</div>
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
						<el-button type="success" size="mini" @click="update" icon="el-icon-edit">修改</el-button>
						<el-button size="mini" icon="el-icon-back" @click="tolink('ShopimgList')">返回</el-button>
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
		name: 'shopimgupdate',
		components: {
			PageTitle
		},
		data() {
			return {
				id: 0,
				goodslist: [],
				gname1: '',
				gname2: '',
				gname3: '',
				gid1: 0,
				gid2: 0,
				gid3: 0,
				mrlx: '单张',
				newslist: [],
				blx1: 0,
				blx2: 0,
				blx3: 0,
				mrblx1: '',
				mrblx2: '',
				mrblx3: '',
				lx: 1,
				img3show: false,
				UploadFile: this.$config.send_url + 'Api/Upload/UploadImagesAdmin?token_admin=' + encodeURIComponent(this.$utils.getloc(
					'token_admin')) + '&userid_admin=' + this.$utils.getloc('userid_admin'),  //  + "&width=" + 600 + "&height=" + 300
				fileList: [],
				fileCount: 1
			}
		},
		created() {
			if (this.$route.query.id != "") {
				this.id = this.$route.query.id;
				this.getgoods();
				this.getnews();
			}
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
			handleAvatarSuccess(res, file, fileList) { // 新图片
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
			getgoods: function() {  // 查询所有商品
				this.$request.post("Api/ShopGoods_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.goodslist = res.data.data;
					this.getdata();
				});
			},
			getnews: function() {  // 查询所有新闻
				this.$request.post("Api/News_Admin/List", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin')
				}, (res) => {
					this.newslist = res.data.data;
				});
			},
			getdata: function() {  // 查询图片详情
				this.$request.post("Api/Shopimg_Admin/Get", {
					token_admin: this.$utils.getloc('token_admin'),
					userid_admin: this.$utils.getloc('userid_admin'),
					select_id: this.id
				}, (res) => {
					var data = res.data.data;
					this.lx = data.lx;
					if (data.lx == 2) {
						this.mrlx = '三张';
						var imglist = data.img.split(",");
						if (imglist.length != 3) {
							Notification({
								message: '图片信息错误',
								offset: 100,
								type: 'warning'
							});
							return;
						}
						this.fileCount = 3;
						imglist.forEach(item => {
							var img = {
								url: this.$config.img_url + item
							}
							this.fileList.push(img);
						})

						var gid = data.gid.split(",");
						var bdlx = data.bdlx.split(",");
						var gidlist = [];
						var gnlist = [];
						var bdlxlist = [];
						var mrblxlist = [];

						gid.forEach((item, index) => { // 关联的id
							if (item > 0) {
								if (bdlx[index] == 1) { // 关联的是商品
									bdlxlist.push(1);
									mrblxlist.push("商品");
									for (var i = 0; i < this.goodslist.length; i++) {
										if (item == this.goodslist[i].id) {
											gidlist.push(this.goodslist[i].id);
											gnlist.push(this.goodslist[i].goodsname);
										}
									}
								} else { // 关联的是新闻
									bdlxlist.push(2);
									mrblxlist.push("新闻");
									for (var j = 0; j < this.newslist.length; j++) {
										if (item == this.newslist[j].id) {
											gidlist.push(this.newslist[j].id);
											gnlist.push(this.newslist[j].news_title);
										}
									}
								}
							} else {
								gidlist.push("");
								gnlist.push("");
								bdlxlist.push(0);
								mrblxlist.push("");
							}
						})

						this.gname1 = gnlist[0];
						this.gname2 = gnlist[1];
						this.gname3 = gnlist[2];
						this.gid1 = gidlist[0];
						this.gid2 = gidlist[1];
						this.gid3 = gidlist[2];
						this.blx1 = bdlxlist[0];
						this.blx2 = bdlxlist[1];
						this.blx3 = bdlxlist[2];
						this.mrblx1 = mrblxlist[0];
						this.mrblx2 = mrblxlist[1];
						this.mrblx3 = mrblxlist[2];
					} else {
						var img = {
							url: this.$config.img_url + data.img
						}
						this.fileList.push(img);
						if (data.bdlx == 1) {
							this.goodslist.forEach(item => {
								if (data.gid == item.id) {
									this.gname1 = item.goodsname;
								}
							})
							this.mrblx1 = "商品";
							this.blx1 = 1;
						} else if (data.bdlx == 2) {
							this.newslist.forEach(item => {
								if (data.gid == item.id) {
									this.gname1 = item.news_title;
								}
							})
							this.mrblx1 = "新闻";
							this.blx1 = 2;
						}
					}
				}, false);
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
			update: function() {
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

				_this.$confirm('确定要修改吗?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					_this.$request.post("Api/Shopimg_Admin/Update", {
						token_admin: _this.$utils.getloc('token_admin'),
						userid_admin: _this.$utils.getloc('userid_admin'),
						select_id: _this.id,
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
	.shopimgupdate {
		
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

	.bdselect {
		margin-top: 20px;
	}
</style>
