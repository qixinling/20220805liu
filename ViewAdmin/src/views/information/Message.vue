<template>
	<div class="page">
		<PageTitle title="客服消息" :excel="false" :btn="false"></PageTitle>
		<div class="cardbox">
			<div class="cardcontent">
				<div class="chatbox-left-btn" @click="ShowLeft">> 切换</div>
				<div class="chatbox">
					<div class="chatbox-left" :style="leftstyle">
						<el-tabs v-model="activeName" stretch>
							<el-tab-pane label="当前消息" name="1"></el-tab-pane>
							<el-tab-pane label="用户列表" name="2"></el-tab-pane>
						</el-tabs>
						<!-- 当前消息 -->
						<div class="userslist" v-if="activeName == '1'">
							<div class="loading-userslist" v-if="uloading">
								<i class="el-icon-loading"></i>
							</div>
							<div class="users" v-for="(item, index) in sessioninguserslist" :key="index"
								v-on:click="ChangeSession(item)">
								<div class="thebox">
									<img class="users-tx" :src="GetImgUrl(item.Tx)" />
									<span class="msgnum" v-if="item.Noreadnum > 0">{{
                    item.Noreadnum
                  }}</span>
								</div>

								<span class="users-name">
									{{ item.Userid }}
									<small>{{ item.Username }}</small>
								</span>
							</div>
						</div>
						<!-- 用户列表 -->
						<div class="userslist" v-if="activeName == '2'">
							<div class="search-box">
								<input class="search-input" type="text" v-model="query_userid" placeholder="用户列表查询" />
								<el-button type="primary" size="mini" @click="GetUsers()" style="margin-left:10px;">查询
								</el-button>
							</div>
							<div class="msglist-null" v-if="query_state">
								<i class="eicon el-icon-magic-stick"></i>
								<div>没有找到该用户</div>
							</div>
							<div class="users" v-for="(item, index) in userslist" :key="index"
								v-on:click="ChangeSession(item)">
								<div class="thebox">
									<img class="users-tx" :src="GetImgUrl(item.Tx)" />
								</div>

								<span class="users-name">
									{{ item.Userid }}
									<small>{{ item.Username }}</small>
								</span>
							</div>
						</div>
					</div>
					<!-- 对话记录 -->
					<div class="chatbox-right">
						<div class="not-msg" v-if="!chatShow">
							<i class="el-icon-s-comment" style="font-size:100px;" />
							<h2>当前没有对话...</h2>
						</div>
						<template v-if="chatShow">
							<div class="title-session">正在与{{ users.Userid }}对话......</div>
							<div class="message">
								<div class="msglist-null" v-if="msglist.length == 0">
									<i class="eicon el-icon-magic-stick"></i>
									<div>暂无记录</div>
								</div>
								<ul v-if="msglist.length != 0">
									<li v-for="(item, index) in msglist" :key="index">
										<p class="time">
											<span>{{ new Date().TransDate(item.Mdate)}}</span>
										</p>
										<div class="main" :class="{ self: item.Fid == 0 }">
											<img class="tx" :src="GetImgUrl(users.Userimg)" />
											<div class="usersmsg" v-html="item.Msgcontent"></div>
										</div>
									</li>
								</ul>
							</div>
							<!-- 输入框 -->
							<div class="text">
								<textarea placeholder="按 Ctrl + Enter 发送" v-model="content" @keyup="onKeyup"></textarea>
								<button class="text-send" v-on:click="Send">发送</button>
							</div>
						</template>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import Vue from "vue";
	import {
		config
	} from "../../assets/js/config";
	import PageTitle from "../../components/PageTitle";
	import {
		Tabs,
		TabPane
	} from "element-ui";
	Vue.use(Tabs);
	Vue.use(TabPane);
	export default {
		name: "Message",
		components: {
			PageTitle,
		},
		data() {
			return {
				port: "",
				users: {
					Id: 1,
					Userid: "admin",
					Userimg: "tx.png",
				},
				msg: {
					Fid: 0,
					Fuserid: "kefu",
					Lx: 1,
					Title: "客服消息",
					Msgcontent: "",
					Sid: "",
					Suserid: "",
					Mdate: "",
				},
				content: "",
				msglist: [],
				chatShow: false,
				timer: null,
				userslist: [],
				sessioninguserslist: [],
				query_userid: "",
				query_state: false,
				issend: true, //发送开关
				activeName: "1",
				uloading: false,

				leftstyle: "display: block;",
			};
		},
		created() {
			this.uloading = true;
			this.GetSessioningUsers();  // 消息记录
			this.GetPort();  // 获取端口号

			//加载动画
			var _this = this;
			const serverload = setInterval(function() {
				if (_this.sessioninguserslist.length > 0) {
					_this.uloading = false;
					clearInterval(serverload);
				}
			}, 1000);
		},
		methods: {
			GetPort() {  // 获取端口号
				let _this = this;
				this.$request.post(
					"Api/WebSocket_Admin/GetPort", {
						token_admin: _this.$utils.getloc("token_admin"),
						userid_admin: _this.$utils.getloc("userid_admin"),
					},
					(res) => {
						_this.port = res.data.data.port;
						_this.Start();
					}
				);
			},
			Start() {
				let _this = this;
				var wsImpl = window.WebSocket || window.MozWebSocket;
				console.log("正在连接到服务器...");

				let ws_url = "";
				let roomid = _this.users.Id + _this.users.Userid;
				let userid = "kefu";
				ws_url = _this.$config.ws_url + _this.port;

				ws_url += "?roomid=" + roomid + "&userid=" + userid;

				// 创建一个新的websocket并连接
				window.ws = new wsImpl(ws_url);
				// 当数据从服务器连接时，这个metod被调用
				window.ws.onmessage = function(evt) {
					_this.Centre(evt.data); //接线员
				};
				// 连接建立后，调用此方法
				window.ws.onopen = function(e) {
					console.log("成功连接到服务器！");
				};

				// 当连接关闭时，将调用此方法
				window.ws.onclose = function() {
					console.log("无法连接服务器或服务器已关闭！");
				};

				// 路由跳转时结束websocket链接
				_this.$router.afterEach(function() {
					window.ws.close();
				});
			},
			//接线员
			Centre(dataPackage) {
				//接线员拿到包裹，根据类型进行操作
				let _this = this;
				dataPackage = JSON.parse(dataPackage);
				if (dataPackage.Type == 0) { //对话消息
					if ((dataPackage.Fuid == _this.users.Id && dataPackage.Fuserid == _this.users.Userid) || (dataPackage
							.Fuid == 0 && dataPackage.Fuserid == "kefu")) {
						_this.msglist.push(dataPackage.MsgData);
						_this.scrollToBottom();
					}
					_this.GetSessioningUsers();
				} else if (dataPackage.Type == 1) { //心跳保持连接
					console.log(dataPackage.Msg)
					let time = dataPackage.ActiveTime; //多久触发一次心跳
					_this.timer = setInterval(() => {
						let msgitem = _this.CreateMsgItem({
							Id: 0,
							Userid: "kefu"
						})
						msgitem.Type = 1;
						let jmsg = JSON.stringify(msgitem); //转成json格式
						window.ws.send(jmsg);
						console.log("心跳中...");
					}, time);
				} else if (dataPackage.Type == 2) { //切换房间
					// console.log(dataPackage.Msg)
				} else if (dataPackage.Type == 3) { //未读消息通知(客服)
					_this.GetSessioningUsers();
				}
			},
			// 创建发送对象
			CreateMsgItem(users) {
				let _this = this;
				let item = {
					Fuid: 0,
					Fuserid: "kefu",
					Suid: users.Id,
					Suserid: users.Userid,
					Rid: users.Id + users.Userid,
					Type: 0,
					MsgData: {
						Fid: 0,
						Fuserid: "kefu",
						Lx: 1,
						Title: "客服消息",
						Msgcontent: _this.content,
						Sid: users.Id,
						Suserid: users.Userid,
						Mdate: _this.GetNowDate()
					}
				};
				return item;
			},
			//更改对话用户
			ChangeSession(users) {
				let _this = this;
				_this.chatShow = true;
				for (let i = 0; i < _this.sessioninguserslist.length; i++) {
					if (_this.sessioninguserslist[i].Userid == users.Userid) {
						_this.sessioninguserslist[i].Noreadnum = "";
						break;
					}
				}
				_this.users.Id = users.Id;
				_this.users.Userid = users.Userid;
				_this.users.Userimg = users.Tx;
				let msgitem = _this.CreateMsgItem(users);
				msgitem.Type = 2;
				window.ws.send(JSON.stringify(msgitem));
				_this.ListFirst();
				_this.ShowLeft();
			},
			//获取聊天记录
			ListFirst: function() {
				this.msglist = [];
				this.$request.post(
					"Api/Msg_Admin/List_First", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						userid: this.users.Userid,
					},
					(res) => {
						this.msglist = res.data.data;
						this.scrollToBottom();
					}, false, false
				);
			},
			//心跳防断开
			Xintiao() {
				console.log("开始心跳...");
				let _this = this;
				let time = 60000; //多久触发一次心跳
				_this.timer = setInterval(() => {
					let msgitem = _this.CreateMsgItem(_this.users)
					msgitem.Type = 1;
					let jmsg = JSON.stringify(msgitem); //转成json格式
					window.ws.send(jmsg);
				}, time);
			},
			Xintiaojieshu() {
				//清除定时器
				clearInterval(this.timer);
				console.log("心跳结束");
			},
			//快捷键发送消息
			onKeyup(e) {
				if (e.ctrlKey && e.keyCode === 13 && this.content != "") {
					this.Send();
				}
			},
			Send() {
				let _this = this;
				if (_this.content != "" && _this.issend) {
					_this.issend = false; //禁止发消息
					let jmsg = JSON.stringify(_this.CreateMsgItem(_this.users)); //转成json格式
					window.ws.send(jmsg); //发送消息至后端保存
					let newitem = JSON.parse(jmsg); //转回对象,后面发送的消息就不会被覆盖
					// _this.msglist.push(newitem.MsgData); //将发送消息插入对话列表
					_this.content = ""; //清空已经发送的消息内容
					_this.issend = true; //允许发送消息
					_this.scrollToBottom(); //滚轮拉至底部
				} else {
					console.log("不能为空");
				}
			},
			//获取用户
			GetUsers: function() {
				this.$request.post(
					"Api/Msg_Admin/List_User", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
						query_userid: this.query_userid,
					},
					(res) => {
						this.userslist = res.data.data;
						this.query_state = this.userslist.length > 0 ? false : true;
					},
					false,
					false
				);
			},
			//获取对话中的用户
			GetSessioningUsers: function() {
				this.$request.post(
					"Api/Msg_Admin/List_Chating", {
						token_admin: this.$utils.getloc("token_admin"),
						userid_admin: this.$utils.getloc("userid_admin"),
					},
					(res) => {
						let data = res.data.data;
						data = data.sort(this.compare('Noreadnum'));
						this.sessioninguserslist = data;
					},
					false,
					false
				);
			},
			//根据未读倒序
			compare(property) {
				return function(a, b) {
					var value1 = a[property];
					var value2 = b[property];
					return value2 - value1;
				}
			},
			//获取当前时间
			GetNowDate: function() {
				var nowdate = new Date();
				var redate =
					nowdate.toLocaleDateString() +
					" " +
					nowdate.getHours() +
					":" +
					nowdate.getMinutes() +
					":" +
					nowdate.getSeconds();
				return redate;
			},
			//获取图片完整路径
			GetImgUrl: function(img) {
				var url = this.$config.img_url + img;
				return url;
			},
			//滑动到底部
			scrollToBottom: function() {
				this.$nextTick(() => {
					var container = this.$el.querySelector(".message");
					container.scrollTop = container.scrollHeight;
				});
			},
			//显示左侧用户列表
			ShowLeft() {
				if (
					document.body.clientWidth < 500 &&
					this.leftstyle == "display: none;"
				) {
					this.leftstyle = "display: block;";
				} else if (
					document.body.clientWidth < 500 &&
					this.leftstyle == "display: block;"
				) {
					this.leftstyle = "display: none;";
				}
			},

		},
		beforeDestroy() {
			//页面关闭时清除定时器
			clearInterval(this.timer);
			this.timer = null;
			console.log("心跳结束");
		},
		// watch: {
		//   query_userid: function () {
		//     this.GetUsers();
		//   }
		// },
	};
</script>

<style lang="less" scoped>
	.page{
		width: 70%;
	}
	.cardbox {
		background: #2e3238;
	}

	//以下是本页面的css
	.chatbox {
		display: flex;
		flex-wrap: wrap;

		width: 100%;
		height: 600px;

		overflow: hidden;
		border-radius: 3px;
	}

	.chatbox-left,
	.chatbox-right {
		height: 100%;
	}

	.chatbox-left {
		padding-right: 15px;
		float: left;
		width: 300px;
		color: #f4f4f4;
		background-color: #2e3238;
	}

	.chatbox-right {
		flex: 2;
		position: relative;
		overflow: hidden;
		background-color: #eee;
	}

	.title-session {
		text-align: center;
		font-size: 12px;
		color: #636e72;
	}

	.chatbox-left-btn {
		display: none;
		height: 30px;
		margin-bottom: 5px;
		color: white;
		padding: 5px;
	}

	@media only screen and (max-width: 500px) {
		.page{
			width: inherit;
		}
		.chatbox-left {
			display: none;
		}

		.chatbox-left-btn {
			display: flex;
			justify-content: flex-start;
			align-items: center;
		}
	}

	// 搜索框
	.search-box {
		display: flex;
		justify-content: center;
		align-items: center;
		padding: 10px;
	}

	.search-input {
		box-sizing: border-box;
		padding: 0 10px;
		width: 100%;
		font-size: 9pt;
		color: #fff;
		height: 30px;
		line-height: 30px;
		border: 1px solid #3a3a3a;
		border-radius: 4px;
		outline: 0;
		background-color: #26292e;
	}

	//加载用户列表
	.loading-userslist {
		height: 100%;
		font-size: 20px;
		display: flex;
		justify-content: center;
		align-items: center;
	}

	//用户列表
	.userslist {
		box-sizing: border-box;
		height: ~"calc(100% - 55px)";
		overflow-y: scroll;
		border-top: solid 1px #292c33;
	}

	//用户列表滚动条整体样式
	.userslist::-webkit-scrollbar {
		width: 10px;
		background: #26292e;
	}

	//用户列表滚动条中滑动的按钮
	.userslist::-webkit-scrollbar-thumb {
		background: #636e72;
	}

	//用户列表滚动条两端按钮
	.userslist::-webkit-scrollbar-button {
		display: none;
	}

	.users {
		height: 50px;
		padding: 10px;
		background: #2e3238;
		display: flex;
		align-items: center;
		border-bottom: solid 1px #292c33;
	}

	.users:hover {
		background: #43474c;
		cursor: pointer;
	}

	.users-tx {
		width: 50px;
		height: 50px;
		border-radius: 50%;
	}

	.users-name {
		margin-left: 10px;
	}

	.thebox {
		position: relative;
	}

	.msgnum {
		position: absolute;
		top: -5px;
		right: -10px;
		background: #ff4a20;
		border-radius: 50%;
		font-size: 12px;
		padding: 5px 8px;
		text-align: center;
	}

	//输入框
	.text {
		border: none;
		box-sizing: border-box;
		position: absolute;
		width: 100%;
		bottom: 0;
		left: 0;
		height: 140px;
		min-height: 140px;
		display: flex;
	}

	.text-send {
		width: 70px;
		background: #409eff;
		border: none;
		color: #fff;
		outline: none;
	}

	.text-send:hover {
		cursor: pointer;
	}

	textarea {
		box-sizing: border-box;
		padding: 10px;
		height: 100%;
		width: 100%;
		border: none;
		outline: none;
		font-family: "Micrsofot Yahei";
		resize: none;
		border: solid 1px #ddd;
	}

	//聊天记录
	.msglist-null {
		display: flex;
		justify-content: center;
		align-items: center;
		flex-direction: column;
		width: 100%;
		height: 100%;
		color: #888;
	}

	.msglist-null .eicon {
		font-size: 100px;
	}

	.message {
		height: ~"calc(100% - 180px)";
		padding: 10px 15px;
		overflow-y: scroll;
	}

	li {
		list-style: none;
		margin-bottom: 15px;
	}

	ul {
		padding-left: 0;
	}

	.time {
		margin: 7px 0;
		text-align: center;
	}

	.time>span {
		display: inline-block;
		padding: 0 18px;
		font-size: 12px;
		color: #fff;
		border-radius: 2px;
		background-color: #dcdcdc;
	}

	.tx {
		float: left;
		width: 30px;
		height: 30px;
		border-radius: 50%;
	}

	.usersmsg {
		display: inline-block;
		position: relative;
		padding: 5px 10px;
		max-width: calc(100% - 100px);
		min-height: 30px;
		line-height: 2.5;
		font-size: 9pt;
		text-align: left;
		word-break: break-all;
		background-color: #fafafa;
		border-radius: 4px;
		margin-left: 10px;
		word-wrap: break-word;
		word-break: break-all;
	}

	.usersmsg:before {
		content: " ";
		position: absolute;
		top: 9px;
		right: 100%;
		border: 6px solid transparent;
		border-right-color: #fafafa;
	}

	/deep/ .usersmsg img {
		width: 200px;
	}

	//发出消息样式
	.self {
		text-align: right;
	}

	.self .tx {
		float: right;
		margin: 0 0 0 10px;
	}

	.self .usersmsg {
		background-color: #b2e281;
	}

	.self .usersmsg:before {
		right: inherit;
		left: 100%;
		border-right-color: transparent;
		border-left-color: #b2e281;
	}

	.not-msg {
		display: flex;
		justify-content: center;
		align-items: center;
		flex-direction: column;
		height: 100%;
		color: #888;
	}

	//饿了么样式
	/deep/ .el-tabs__item {
		color: #fff;
	}

	/deep/ .el-tabs__item.is-active {
		color: #fff;
	}
</style>
