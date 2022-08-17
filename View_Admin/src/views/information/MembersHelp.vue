<template>
	<div>
		<PageTitle title="会员帮助"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<el-tabs v-model="active" :tab-position="tabPosition" @tab-click="ChangeTab" style="height: 100%;">
					<el-tab-pane label="公司简介" name="1">
						<WangEditor v-model="article_content" :isClear="isClear" @change="change"></WangEditor>
					</el-tab-pane>
					<el-tab-pane label="会员帮助" name="2">
						<WangEditor v-model="article_content" :isClear="isClear" @change="change"></WangEditor>
					</el-tab-pane>
					<el-tab-pane label="充值帮助" name="3">
						<WangEditor v-model="article_content" :isClear="isClear" @change="change"></WangEditor>
					</el-tab-pane>
					<el-tab-pane label="提现帮助" name="4">
						<WangEditor v-model="article_content" :isClear="isClear" @change="change"></WangEditor>
					</el-tab-pane>
					<el-tab-pane label="转账帮助" name="5">
						<WangEditor v-model="article_content" :isClear="isClear" @change="change"></WangEditor>
					</el-tab-pane>
					<el-tab-pane label="转换帮助" name="6">
						<WangEditor v-model="article_content" :isClear="isClear" @change="change"></WangEditor>
					</el-tab-pane>
					<el-tab-pane label="服务中心" name="7">
						<WangEditor v-model="article_content" :isClear="isClear" @change="change"></WangEditor>
					</el-tab-pane>
					<el-tab-pane label="注册协议" name="8">
						<WangEditor v-model="article_content" :isClear="isClear" @change="change"></WangEditor>
					</el-tab-pane>
				</el-tabs>
				<div class="btn">
					<el-button icon="el-icon-edit" size="mini" type="primary" v-on:click="Update">修改</el-button>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import {
		Notification,
		Tabs,
		TabPane
	} from "element-ui";
	import PageTitle from "../../components/PageTitle";
	import WangEditor from '../../components/WangEditor';
	Vue.prototype.$notify = Notification;
	Vue.use(Tabs);
	Vue.use(TabPane);
	export default {
		name: "MembersHelp",
		components: {
			PageTitle,
			WangEditor
		},
		data() {
			return {
				isClear: false,
				tabPosition: "left",
				update_id: 1,
				active: "1",
				article_title: "",
				article_content: ""
			};
		},
		created() {
			this.GetData();
		},
		methods: {
			change(val) {
				//console.log(val)
			},
			ChangeTab: function(e) {
				this.update_id = e.name;
				this.GetData();
			},
			Update: function() {
				this.$request.post(
					"Api/Article_Admin/Update", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id: this.update_id,
						article_title: this.article_title,
						article_content: this.article_content
					},
					res => {
						Notification({
							title: "修改成功",
							message: res.data.msg,
							offset: 100,
							type: "success"
						});
					}
				);
			},
			GetData: function() {
				this.$request.post(
					"Api/Article_Admin/Get", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						id: this.update_id
					},
					res => {
						this.article_title = res.data.data.articletitle;
						this.article_content = res.data.data.articlecontent;
					}
				);
			}
		}
	};
</script>

<style scoped>
	.btn {
		text-align: center;
		margin-top: 20px;
	}
</style>
