import config from './config.js';
import utils from './utils.js';
import Axios from "axios";
import {
	Loading
} from 'element-ui';
import {
	Notification
} from 'element-ui';

function post(api, data, func, nocode = false, showloading = true) {
	let _load = "";
	if (showloading) {
		_load = Loading.service({
			fullscreen: true
		});
	}

	let _url = config.send_url + api; //拼接url

	data.sign = utils.sort_ascii(data); //签名

	Axios({
			url: _url,
			method: 'post',
			headers: {
				'Content-Type': 'application/json; charset=utf-8'
			},
			data: JSON.stringify(data)
		})
		.then(function(res) {
			if (showloading) {
				_load.close();
			}
			//nocode默认false,传入true时,强制code=100
			if (res.status === 200) {
				if (res.data.code === 100 || nocode) {
					func(res);
				} else if (res.data.code === 0) {
					Notification.error({
						title: '操作失败',
						message: res.data.msg,
						offset: 100,
						duration: 0
					})
				} else if (res.data.code === -1) {
					Notification.error({
						title: '登录超时',
						message: '您的登录信息已失效,请重新登录',
						offset: 100
					})
					setTimeout(function() {
						window.location.href = "/admin/?action=out";
					}, 2000);
				} else {
					Notification.warning({
						title: '未知错误',
						message: '出现未知错误,请联系技术人员',
						offset: 100,
						duration: 0
					})
				}
			} else {
				Notification.warning({
					title: '未知错误',
					message: 'status:' + res.status + ' statusText:' + res.statusText,
					offset: 100,
					duration: 0
				})
				console.log(res);
			}
		})
		.catch(function(res) {
			console.log(res);
			if (showloading) {
				_load.close();
			}
			Notification.error({
				title: '未知错误',
				message: '出现未知错误,请联系技术人员',
				offset: 100
			})
		});
}

function get() {

}

export default {
	post,
	get
};
