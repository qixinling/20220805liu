var send_url;
var img_url;
var ws_url;
var Project = "20220805liu";
if (process.env.NODE_ENV === "development") {
	//开发环境
	send_url = "https://localhost:44332/";
	img_url = send_url + "Upload/";
	ws_url = "ws://localhost:";
} else if (process.env.NODE_ENV === "production") {
	//生产环境
	send_url = "http://20220805liu.gxyhttest.com/";
	img_url = send_url + "Upload/";
	ws_url = "ws://20220805liu.gxyhttest.com:";
}

var ImgRatio = {
	Goods:{
		Width:500,
		Height:500,
		Tip:"建议图片比例 1:1 像素 500 * 500",
	},
	Slide:{
		Width:640,
		Height:360,
		Tip:"建议图片比例 16:9 像素 640 * 360",
	},
	Dalei:{
		Width:60,
		Height:60,
		Tip:"建议图片比例 1:1 像素 60 * 60",
	},
	Xiaolei:{
		Width:60,
		Height:60,
		Tip:"建议图片比例 1:1 像素 60 * 60",
	}
}
export default {
	send_url,
	img_url,
	Project,
	ws_url,
	ImgRatio
};
