<template>
	<div>
		<PageTitle title="添加" :excel="false" :btn="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-form ref="form" :model="form" label-width="120px">
					<!-- <el-form-item label="赞">
            <el-input v-model="form.zan"></el-input>
          </el-form-item>
          <el-form-item label="踩">
            <el-input v-model="form.cai"></el-input>
          </el-form-item>-->
					<!-- <el-form-item label="首次提问">
            <el-switch v-model="form.hlevel"></el-switch>
          </el-form-item> -->
					<el-form-item label="是否显示">
						<el-switch v-model="form.show"></el-switch>
					</el-form-item>
					<!-- <el-form-item label="关联问题">
            <el-transfer v-model="form.value" :data="qidlist" :titles="titles"></el-transfer>
          </el-form-item> -->
					<el-form-item label="提问内容">
						<el-input v-model="form.question" placeholder="请输入提问内容"></el-input>
					</el-form-item>
					<el-form-item label="回复内容">
						<el-input v-model="form.answer" placeholder="请输入回复内容"></el-input>
					</el-form-item>
					<el-form-item label="排序">
						<el-input-number v-model="form.rank" size="mini" :min="0"></el-input-number>
					</el-form-item>
					<el-form-item>
						<el-button type="primary" size="mini" @click="Add">添加</el-button>
						<el-button @click="Back" size="mini">返回</el-button>
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
		InputNumber,
	} from "element-ui";
	Vue.use(Switch);
	Vue.use(Form);
	Vue.use(FormItem);
	Vue.use(Input);
	Vue.use(Transfer);
	Vue.use(InputNumber);
	Vue.prototype.$notify = Notification;
	export default {
		name: "AutoReturnAdd",
		components: {
			PageTitle,
		},
		data() {
			return {
				titles: ["未关联提问", "已关联提问"],
				data: [],
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
			this.GetSelect();
		},
		methods: {
			Add: function() {
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
				this.form.gpath = this.form.value.join(",");
				this.$request.post(
					"Api/Help_Admin/Add", {
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
						
						this.form.question = "";
						this.form.answer = "";
						this.form.rank = 0;
						this.form.show = false;
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
