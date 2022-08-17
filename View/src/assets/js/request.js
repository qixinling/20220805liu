import config from './config.js';
import utils from "./utils.js";
import Axios from "axios";
import store from '../../store'


import {
	Toast
} from 'vant';

import {
	Dialog
} from 'vant';

function post(api, params, func, showloading = true) {
	if (showloading) {
		Toast.loading({
			duration: 0,
			message: '正在加载...',
			forbidClick: true,
		});
	}
	
	let _url = config.send_url + api; //拼接url
	
	params.sign = utils.sort_ascii(params); //签名
	Axios({
		url: _url,
		method: 'post',
		headers: {
			'Content-Type': 'application/json; charset=utf-8'
		},
		data: JSON.stringify(params)
	}).then((res) => {
		if (showloading) {
			Toast.clear();
		} //关闭加载提示
		if (res.status === 200) {
			if (res.data.code == -1) {
				Dialog.alert({
					title: '登录信息失效',
					message: '请重新登录',
				}).then(() => {
					utils.setloc("token", "");
					store.commit("setLoginState", false);
					//跳转登录页
				});
			} else if (res.data.code == 500) {
				Dialog.alert({
					title: '服务端异常',
					message: res.data.msg,
				});
			} else {
				func(res);
			}
		} else {
			Dialog.alert({
				title: '未知错误',
				message: 'status:' + res.status + ' statusText:' + res.statusText,
			});
		}
	}).catch((err) => {
		if (showloading) {
			Toast.clear();
		} //关闭加载提示
		Dialog.alert({
			title: '功能异常',
			message: err,
		});
		console.log(err);
	});
}

function get() {

}

export default {
	post,
	get
};
