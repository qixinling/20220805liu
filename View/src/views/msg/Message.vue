<template>
	<div>
		<div v-if="msglist.length == 0">
			<van-empty description="暂无消息" />
		</div>
		<div class="system-msg" v-for="(item, index) in msglist" :key="index">
			<van-notice-bar left-icon="envelop-o" background="#fff" color="#000" wrapable :scrollable="false">
				<div>{{$utils.timestampFormat(item.Mdate)}}</div>
				<div>{{item.Msgcontent}}</div>
			</van-notice-bar>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import {
		NoticeBar, Col, Row
	} from "vant";
	Vue.use(NoticeBar);
	Vue.use(Col);
	Vue.use(Row);
	export default {
		name: "Message",
		components: {},
		data() {
			return {
				msglist: [],
				mcount: ""
			};
		},
		created() {
			this.GetData();
		},
		methods: {
			GetData() {
				let _this = this;
				_this.$request.post(
					"Api/Msg/List_First", {
						token: _this.$utils.getloc("token"),
						userid: _this.$utils.getloc("userid"),
						lx: 0
					},
					(res) => {
						if (res.data.code == 0) {
                        _this.$dialog.alert({
                            title: "提示",
                            message: res.data.msg,
                        });
                        return;
                    }
						_this.msglist = res.data.data;
					}
				);
			}
		}
	};
</script>

<style scoped>
	.system-msg {
		padding: 5px 10px;
	}

	.system-msg /deep/.van-notice-bar__content {
		font-size: 12px;
	}
</style>
