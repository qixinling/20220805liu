<template>
    <div>
    </div>
</template>

<script>
import config from "../assets/js/config.js"
export default {
    name: "start",
    data() {
        return {
            path: "/Home",
            getVersion: config.send_url +"Api/Version/Getver",
            download_url: "http://122.51.141.221/20220307liu/com.yfzb.apk",
            sleep: 1000, //等待时间-毫秒
        };
    },
    created() {
        var _this = this;
		//console.log(this.getVersion);
        //非安卓浏览器不检测升级
        var userAgent = navigator.userAgent.toLowerCase();
        if (!/android/.test(userAgent)) {
            setTimeout(function () {
                _this.$router.push(_this.path);
            }, _this.sleep);
        }
    },
    mounted() {
        // var _this = this;
        // //安卓浏览器检测升级
        // _this.onPlusReady(function() {
        // 	setTimeout(function() {
        // 		_this.updateAndroidApp();
        // 	}, _this.sleep);
        // });

        var _this = this;
        //安卓浏览器检测升级
        /* _this.onPlusReady(function() {
				
			}); */

        setTimeout(function () {
            _this.updateAndroidApp();
        }, _this.sleep);
    },
    methods: {
        updateAndroidApp: function () {
            var _this = this;
            try {
                plus.runtime.getProperty(
                    plus.runtime.appid,
                    function (wgtinfo) {
                        var userAgent = navigator.userAgent.toLowerCase();
                        let _ver = wgtinfo.version;
                        if (!_ver) {
                            _this.$router.push(_this.path);
                        }
                        _this.axios
                            .post(_this.getVersion)
                            .then(function (res) {
                                if (res.data.code == 0) {
                                    _this.$dialog.alert({
                                        title: "提示",
                                        message: res.data.msg,
                                    });
                                    return;
                                }
                                let _version = res.data;
                                if (_version != _ver) {
                                    plus.nativeUI.confirm(
                                        "应用有新版本，是否立即下载更新？",
                                        function (event) {
                                            if (event.index == 1) {
                                                /* 用原生等待框显示进度,可自行更换其他UI */
                                                var waiting =
                                                    plus.nativeUI.showWaiting(
                                                        "0 %"
                                                    );
                                                //创建下载任务
                                                var dtask =
                                                    plus.downloader.createDownload(
                                                        _this.download_url
                                                    );
                                                //添加下载监听器
                                                dtask.addEventListener(
                                                    "statechanged",
                                                    function (
                                                        download,
                                                        status
                                                    ) {
                                                        //计算下载进度
                                                        var appTotalSize =
                                                            download.totalSize;
                                                        var appDownLoadedSize =
                                                            download.downloadedSize;
                                                        var j = parseInt(
                                                            (appDownLoadedSize /
                                                                appTotalSize) *
                                                                100
                                                        );
                                                        if (j > 0) {
                                                            waiting.setTitle(
                                                                j.toString() +
                                                                    " %"
                                                            );
                                                        }
                                                        // 下载完成
                                                        if (
                                                            download.state ==
                                                                4 &&
                                                            status == 200
                                                        ) {
                                                            waiting.close(); //关闭等待框
                                                            // 发起安装apk
                                                            plus.runtime.install(
                                                                download.filename
                                                            );
                                                            console.log(
                                                                "Download success: " +
                                                                    download.filename
                                                            );
                                                        }
                                                    },
                                                    false
                                                );
                                                dtask.start();
                                            }
                                        },
                                        "更新提醒",
                                        ["取消", "确认"]
                                    );
                                } else {
                                    _this.$router.push(_this.path);
                                }
                            })
                            .catch(function (error) {
                                alert("出错了");
                                console.log(error);
                            });
                    }
                );
            } catch {
                _this.$router.push(_this.path);
            }
        },
    },
};
</script>

<style scoped>
</style>
