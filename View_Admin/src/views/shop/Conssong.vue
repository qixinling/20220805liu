<template>
	<div style="margin-bottom: 200px">
		<PageTitle title="卖家添加星座" :btn="false" :excel="false"></PageTitle>

		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="150px">
					<el-form-item label="名称">
						<el-select v-model="cid" placeholder="请选择" style="width: 400px;" @change="conschange()">
							<el-option  v-for="item in list" :key="item.id" :label="item.name+' （'+item.minprice +'-'+item.maxprice +'）'" :value="item.id" ></el-option>
						</el-select>
					</el-form-item>
					
					<el-form-item label="会员编号">
						<el-input type="text" v-model="suserid" placeholder="请输入押金单" style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="售卖价格">
						<el-input type="text" v-model="cprice" placeholder="售卖价格" style="width:200px;"></el-input>
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
				list:[],
				cid:"",
				suserid:'',
				cprice:""
			
			};
		},
		created() {
			this.getdata();
		},
		methods: {
			conschange(){
				console.log(this.cid);
			},
			//获取数据
			getdata: function () {
				this.$request.post(
					"Api/Constellation_Admin/List",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						//console.log(res.data);
						this.list = res.data.data;

					}
				);
			},
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
				if (_this.suserid.length == 0) {
					mod.msg = "用户名不能为空";
				}  else if (_this.cprice.length == 0) {
					mod.msg = "售卖价格不能为空";
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
							"Api/UsersHold_Admin/Add",
							{
								token_admin: _this.$utils.getloc("token_admin"),
								userid_admin: _this.$utils.getloc("userid_admin"),
								cid:_this.cid,
								suserid:_this.suserid,
								cprice:_this.cprice
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