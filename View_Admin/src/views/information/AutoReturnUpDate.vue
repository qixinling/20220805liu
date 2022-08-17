<template>
	<div>
		<PageTitle title="修改" :excel="false" :btn="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" :model="form" label-width="120px">
					<!-- <el-form-item label="赞">
            <el-input v-model="form.zan"></el-input>
          </el-form-item>
          <el-form-item label="踩">
            <el-input v-model="form.cai"></el-input>
          </el-form-item>-->
					<el-form-item label="首次提问">
						<el-switch v-model="form.hlevel"></el-switch>
					</el-form-item>
					<el-form-item label="是否显示">
						<el-switch v-model="form.show"></el-switch>
					</el-form-item>
					<el-form-item label="关联问题">
						<el-transfer v-model="form.value" :data="qidlist" :titles="titles"></el-transfer>
					</el-form-item>
					<el-form-item label="提问内容">
						<el-input v-model="form.question" placeholder="请输入提问内容"></el-input>
					</el-form-item>
					<el-form-item label="回复内容">
						<el-input v-model="form.answer" placeholder="请输入回复内容"></el-input>
					</el-form-item>

					<el-form-item>
						<el-button icon="el-icon-edit" type="primary" size="mini" @click="UpDate">修改</el-button>
						<el-button icon="el-icon-back" @click="Back" size="mini">返回</el-button>
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
		Switch,
		Notification,
		Form,
		FormItem,
		Input,
		Transfer,
	} from "element-ui";
	Vue.use(Switch);
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Input);
	Vue.use(Transfer);
	Vue.prototype.$notify = Notification;
	export default {
		name: "AutoReturnUpDate",
		components: {
			PageTitle,
		},
		data() {
			return {
				titles: ["未关联提问", "已关联提问"],
				qid: "",
				data: {},
				qidlist: [],
				form: {
					zan: 0,
					cai: 0,
					hlevel: false,
					gpath: ",",
					value: [],
					show: false,
					question: "",
					answer: "",
					rank: 0,
					hpath: ","
				},
			};
		},
		created() {
			this.qid = this.$route.query.id;
			this.GetData();
			this.GetSelect();
		},
		methods: {
			UpDate: function() {
				if (this.form.zan == null || this.form.zan == "") {
					this.form.zan = 0;
				}
				if (this.form.cai == null || this.form.cai == "") {
					this.form.cai = 0;
				}
				if (this.form.question == "") {
					this.$notify({
						title: "提示",
						message: "提问内容不能为空",
						offset: 100,
						type: "warning",
					});
					return;
				}
				this.form.hlevel = this.form.hlevel ? 1 : 0;
				this.form.show = this.form.show ? 1 : 0;
				this.form.gpath = this.form.value.join(',');
				this.$request.post(
					"Api/Help_Admin/Update", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						jsonhelp: JSON.stringify(this.form)
					},
					(res) => {
						this.GetSelect();
						this.$notify({
							title: "提示",
							message: res.data.msg,
							offset: 100,
							type: "success",
						});
					}
				);
			},
			GetData: function() {
				this.$request.post(
					"Api/Help_Admin/Question_Get", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						qid: this.qid,
					},
					(res) => {
						this.form = res.data.data;
						var idArray = this.form.gpath.split(",");
						this.form.value = [];
						for (let i = 0; i < idArray.length; i++) {
							if (idArray[i] != "") {
								this.form.value.push(parseInt(idArray[i]));
							}
						}
						this.form["id"] = this.qid;
						this.form.show = this.form.show == 1 ? true : false;
						this.form.hlevel = this.form.hlevel == 1 ? true : false;
					}
				);
			},
			GetSelect: function() {
				this.$request.post("Api/Help_Admin/Select", {}, (res) => {
					this.qidlist = res.data.data;
					var data = [];
					for (let i = 0; i < this.qidlist.length; i++) {
						data.push({
							key: parseInt(this.qidlist[i].id),
							label: this.qidlist[i].question,
						});
					}
					this.qidlist = data;
				}, true);
			},
			Back: function() {
				this.$router.push({
					name: "AutoReturnList"
				});
			},
		}
	};
</script>

<style scoped>
</style>
