<template>
	<div style="margin-bottom: 200px">
		<PageTitle title="添加卖家" :btn="false" :excel="false"></PageTitle>

		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" label-width="150px">
					<el-form-item label="名称">
						<el-select v-model="cid" placeholder="请选择" style="width: 400px;" @change="conschange()">
							<el-option  v-for="item in list" :key="item.id" :label="item.name" :value="item.id" ></el-option>
						</el-select>
					</el-form-item>
					
					<el-form-item label="会员编号">
						<el-input type="text" v-model="suserid" placeholder="请输入会员编号" style="width:200px;"></el-input>
					</el-form-item>
					<el-form-item label="售卖价格">
						<el-input type="text" v-model="cprice" placeholder="售卖价格" style="width:200px;"></el-input>
					</el-form-item>
					<!-- <el-form-item label="打款金额">
						<el-input type="text" v-model="zshouyi" placeholder="打款金额" style="width:200px;"></el-input>
					</el-form-item> -->
					<!-- <el-form-item label="参与场次">
						<el-select v-model="sid" placeholder="请选择" style="width: 400px;" >
							<el-option  v-for="item in sitelist" :key="item.id" :label="item.name" :value="item.id" ></el-option>
						</el-select>
					</el-form-item> -->
					
					<el-form-item>
						<el-button type="primary" @click="Add">添加</el-button>
						<!-- <el-button @click="back">返回</el-button> -->
					</el-form-item>
				</el-form>
				
				<TablePadding title="首单列表" :list="slist" :key="refresh" :ischeckbox="true" :isexcel="true" :summary="true"
					ref="tablepadding">
					<!--操作按钮插槽-->
					<div slot="btn_solt" >
						
					</div>
					<el-table-column prop="cdate" label-class-name="cdate" label="日期"></el-table-column>
					<el-table-column prop="suserid" label-class-name="suserid" label="会员编号"></el-table-column>
					<el-table-column prop="jname" label-class-name="jname" label="产品"></el-table-column>
					<el-table-column prop="zprice" label-class-name="zprice" label="总售卖价格"></el-table-column>
					<el-table-column prop="znum" label="总数量"></el-table-column>
					<!-- <el-table-column prop="zyijia" label="总溢价"></el-table-column> -->
				</TablePadding>
			</div>
		</div>
	</div>
</template>

<script>
	import PageTitle from "../../components/PageTitle";
		import TablePadding from '../../components/TablePadding';
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
			TablePadding
		},
		data() {
			return {
				list:[],
				cid:"",
				suserid:'',
				cprice:"",
				zshouyi:"",
				slist:[],
				sitelist:[],
				sid:'',
				refresh:false
			
			};
		},
		created() {
			this.getdata();
			this.getsddata();
			this.getsitedata()
		},
		methods: {
			conschange(){
				console.log(this.cid);
			},
			//获取数据
			getdata: function () {
				this.$request.post(
					"Api/Jewellery_Admin/List",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						var date = res.data.data;
						date.forEach(item=>{
							if(item.state == 1){
								this.list.push(item);
							}
						})
					}
				);
			},
			//获取数据
			getsitedata: function () {
				this.$request.post(
					"Api/Site_Admin/List",
					{
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						var date = res.data.data;
						this.sitelist = date;
					}
				);
			},
			getsddata: function() { // 
				var _this = this;
				_this.$request.post("Api/UsersHold_Admin/SdList", {
					token_admin: _this.$utils.getloc('token_admin'),
					userid_admin: _this.$utils.getloc('userid_admin')
				}, (res) => {
					this.slist = res.data.data;
					this.refresh = !this.refresh;
				});
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
				//console.log(_this.cons);
				if (_this.suserid.length == 0) {
					mod.msg = "用户名不能为空";
				}  else if (_this.cprice.length == 0) {
					mod.msg = "售卖价格不能为空";
				}else if (_this.cid == "") {
					mod.msg = "请选择产品";
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
								cprice:_this.cprice,
								zshouyi:_this.zshouyi
							},
							(res) => {
								_this.$notify({
									title: "提示",
									message: res.data.msg,
									offset: 100,
									type: "success",
								});
								_this.getsddata();
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