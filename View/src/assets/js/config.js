var send_url;
var ws_url;
var Project = "20220805liu";
if (process.env.NODE_ENV === "development") {
	//开发环境
	send_url = "https://localhost:44332/";
	ws_url = "ws://localhost:";
} else if (process.env.NODE_ENV === "production") {
	//生产环境
	send_url = "http://www.hnbc168.com/";
	ws_url = "ws://www.hnbc168.com:";
}

export default {
	send_url,
	ws_url,
	Project
};
