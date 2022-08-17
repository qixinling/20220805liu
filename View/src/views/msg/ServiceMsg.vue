<template>
	<div>
		<div class="ServiceMsg">
			<div v-if="msglist.length == 0">
				<van-empty description="暂无消息" />
			</div>
			<div class="chat-room" v-for="(item, index) in msglist" :key="index">
				<div v-if="item.Lx == 1">
					<div class="msgdate">{{item.Mdate}}</div>
					<div :class="['item', item.Fid == 0 ? 'left' : 'right']">
						<img class="tx" :src="item.Fid == 0 ? require('@/assets/img/kefu.png') : tx" />
						<div class="message">
							<div v-html="item.Msgcontent" />
						</div>
					</div>
				</div>
			</div>
		</div>

		<div style="height: 55px"></div>
		<div class="flex-container">
			<div class="flex-container j-center footer">
				<van-field v-model="content" placeholder="请输入您的疑问" />
				<van-icon name="photo-o" color="#000" size="30" @click="Upload" />
				<van-icon name="upgrade" color="#000" size="30" @click="Send" />
			</div>
		</div>
		<van-uploader id="upload" style="display: none" :after-read="afterRead" :max-count="1" :max-size="5 * 1024 * 1024"
		 :before-read="beforeRead" @oversize="onOversize" />
	</div>
</template>

<script>
	import Vue from "vue";
	import {
		Field,
		Uploader,
		ImagePreview,
		Empty
	} from "vant";
	Vue.use(Field);
	Vue.use(Uploader);
	Vue.use(ImagePreview);
	Vue.use(Empty);
	Vue.use(Field);
	export default {
		name: "ServiceMsg",
		components: {},
		data() {
			return {
				users: {
					Id: this.$utils.getloc("id"),
					Userid: this.$utils.getloc("userid"),
				},
				msglist: [],
				content: "",
				imglist: [],
				timer: null,
				issend: true,
				tx: "",
			};
		},
		created() {
			this.GetPort();
			this.List_First();
			//放大图片需要的
			window.Getimg = this.ShowBigImg;
		},
		methods: {
			GetPort() {
				let _this = this;
				_this.$request.post(
					"api/WebSocket/GetPort", {
					},
					(res) => {
						let port = res.data.data[0].port;
						_this.Start(port);
					}
				);
				
			},
			Start(port) {
				let _this = this;
				var wsImpl = window.WebSocket || window.MozWebSocket;
				console.log("正在连接到服务器...");
				let ws_url = "";
				let roomid = _this.$utils.getloc("id") + _this.$utils.getloc("userid");
				let userid = _this.$utils.getloc("userid");
				ws_url = _this.$config.ws_url + port;
				ws_url += "?roomid=" + roomid + "&userid=" + userid;
				// 创建一个新的websocket并连接
				window.ws = new wsImpl(ws_url);
				// 当数据从服务器连接时，这个metod被调用
				window.ws.onmessage = function(evt) {
					_this.Centre(evt.data); //接线员
				};
				// 连接建立后，调用此方法
				window.ws.onopen = function(e) {
					// _this.Xintiao();
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
				if (dataPackage.Type == 0) {
					//对话消息
					_this.msglist.push(dataPackage.MsgData);
					_this.toBottom();
				} else if (dataPackage.Type == 1) {
					//心跳保持连接
					console.log(dataPackage.Msg);
					let time = dataPackage.ActiveTime; //多久触发一次心跳
					_this.timer = setInterval(() => {
						let msgitem = _this.CreateMsgItem({
							Id: 0,
							Userid: "kefu",
						});
						msgitem.Type = 1;
						let jmsg = JSON.stringify(msgitem); //转成json格式
						window.ws.send(jmsg);
						console.log("心跳中...");
					}, time);
				} else if (dataPackage.Type == 2) {
					//切换房间
					console.log(dataPackage.Msg);
				}
			},
			// 创建发送对象
			CreateMsgItem(users) {
				let _this = this;
				let item = {
					Fuid: users.Id,
					Fuserid: users.Userid,
					Suid: 0,
					Suserid: "kefu",
					Rid: users.Id + users.Userid,
					Type: 0,
					MsgData: {
						Fid: users.Id,
						Fuserid: users.Userid,
						Lx: 1,
						Title: "用户消息",
						Msgcontent: _this.content,
						Sid: 0,
						Suserid: "kefu",
						Mdate: _this.GetNowDate(),
					},
				};
				return item;
			},
			//发送消息
			Send() {
				let _this = this;
				if (_this.content != "" && _this.issend) {
					_this.issend = false; //禁止发消息
					let msgitem = _this.CreateMsgItem(_this.users);
					let jmsg = JSON.stringify(msgitem); //转成json格式
					window.ws.send(jmsg); //发送消息至后端保存
					let newmsgitem = JSON.parse(jmsg); //转回对象,后面发送的消息就不会被覆盖
					// _this.msglist.push(newmsgitem.MsgData); //将发送消息插入对话列表
					_this.content = ""; //清空已经发送的消息内容
					_this.toBottom(); //滚轮拉至底部
					_this.issend = true; //允许发送消息
				} else {
					_this.$toast({
						message: "消息内容不能为空",
						position: "bottom",
						duration: 1000,
					});
				}
			},
			//第一次执行查询所有消息
			List_First: function() {
				let _this = this; // eslint-disable-line no-unused-vars
				_this.$request.post(
					"api/Msg/List_First", {
						token: _this.$utils.getloc("token"),
						userid: _this.$utils.getloc("userid"),
						lx: 1,
					},
					(res) => {
						_this.msglist = res.data.data.reverse();
						_this.toBottom();
					}
				);
			},
			//心跳防断开
			Xintiao() {
				console.log("开始心跳...");
				let _this = this;
				let time = 60000; //多久触发一次心跳
				_this.timer = setInterval(() => {
					let msgitem = _this.CreateMsgItem(_this.users);
					msgitem.Type = 1;
					let jmsg = JSON.stringify(msgitem); //转成json格式
					window.ws.send(jmsg);
				}, time);
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
			//至底部
			toBottom: function() {
				setTimeout(function() {
					var ele = document.getElementsByClassName("ServiceMsg");
					window.scrollTo(0, ele[0].clientHeight);
				}, 1000);
			},
			//放大图片
			ShowBigImg(imgurl) {
				this.imglist = []; //清空图片集合
				this.imglist.push(this.$config.send_url + "Upload/" + imgurl); //将图片放入图片集合
				ImagePreview({
					images: this.imglist, //存放图片集合
					showIndex: false, //显示图片数量页码
					loop: false, //是否轮播
					startPosition: 0, //放大集合中下标为0的图片
				});
			},
			//触发上传
			Upload() {
				let upload = document.getElementById("upload");
				upload.click();
			},
			//上传图片
			afterRead(file) {
				// console.log(file);
				let _this = this;
				let _formdata = new FormData(); //创建form对象
				_formdata.append("file", file.file);
				let _posturl =
				    _this.$config.send_url +
				    "Api/Upload/UploadImages?token=" +
				    encodeURIComponent(_this.$utils.getloc("token")) +
				    "&userid=" +
				    _this.$utils.getloc("userid");
				let _config = {
					headers: {
						"Content-Type": "multipart/form-data",
					},
				};
				_this.axios
					.post(_posturl, _formdata, _config)
					.then(function(res) {
						if (res.data.code == 100) {
							_this.content =
								`<img onclick="Getimg('` +
								res.data.data.imgname +
								`')" src="` +
								_this.$config.send_url +
								"Upload/" +
								res.data.data.imgname +
								`"/>`;
							_this.Send();
						} else {
							_this.$toast({
								message: "上传图片失败",
								position: "bottom",
								duration: 2000,
							});
						}
					})
					.catch(function(error) {
						console.log(error);
					});
			},
			onOversize(file) {
				// 图片大小校验
				this.$toast({
					message: "请上传不5M内的图片",
					position: "bottom",
					duration: 2000,
				});
			},
			beforeRead(file) {
				// 图片格式校验
				if (
					file.type == "image/jpeg" ||
					file.type == "image/png" ||
					file.type == "image/gif"
				) {
					//console.log("上传成功");
					return true;
				} else {
					this.$toast({
						message: "请上传 jpg 格式图片",
						position: "bottom",
						duration: 2000,
					});
					return false;
				}
			},
			TransDate(time) {
				var date = new Date(time);
				var Y = date.getFullYear() + "-";
				var M =
					(date.getMonth() + 1 < 10 ?
						"0" + (date.getMonth() + 1) :
						date.getMonth() + 1) + "-";
				var D = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
				var hh = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
				var mm =
					date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
				var ss =
					date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

				var result = Y + M + D + " " + hh + ":" + mm + ":" + ss;
				return result;
			},
		},
		beforeDestroy() {
			//页面关闭时清除定时器
			clearInterval(this.timer);
			this.timer = null;
			console.log("心跳结束");
		},
	};
</script>

<style scoped>
	.chat-room {
		padding: 10px;
	}

	.item {
		display: flex;
		margin-bottom: 10px;
	}

	.left {
		flex-direction: row;
	}

	.right {
		flex-direction: row-reverse;
		flex-wrap: wrap;
	}

	.right .message {
		margin-right: 10px;
	}

	.left .message {
		margin-left: 10px;
	}

	.tx {
		margin-top: 5px;
		width: 35px;
		height: 35px;
		border-radius: 50%;
		max-width: 20%;
	}

	.msgdate {
		color: #888;
		font-size: 12px;
		text-align: center;
		margin-bottom: 5px;
	}

	.message {
		color: #000;
		font-size: 14px;
		border-radius: 10px;
		display: flex;
		background: white;
		min-height: 25px;
		padding: 9px 10px;
		align-items: center;
		max-width: 70%;
		word-wrap: break-word;
		word-break: break-all;
		overflow: hidden;
	}

	/deep/ .message img {
		width: 100%;
		max-width: 100%;
		height: 100%;
		max-height: 100%;
	}

	/deep/ .van-cell {
		padding: 10px 16px !important;
	}

	/deep/ .van-cell::after {
		border: none;
	}

	.flex-container {
		display: flex;
		justify-content: space-around;
		align-items: center;
		flex-direction: row;
	}

	.flex-container>* {
		padding: 5px;
	}

	.j-center {
		justify-content: center !important;
	}

	.a-start {
		align-items: flex-start !important;
	}

	.footer {
		padding-top: 5px;
		padding-bottom: 5px;
		border-top: 1px solid #eee;
		background: #fff;
		position: fixed;
		bottom: 0;
		width: 100%;
	}
</style>
