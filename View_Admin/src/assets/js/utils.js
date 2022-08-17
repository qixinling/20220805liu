import config from './config.js';


function GetQueryString(name) {
	var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
	var r = window.location.search.substr(1).match(reg);
	if (r != null) return unescape(r[2]);
	return null;
}

function sort_ascii(obj) { //签名
	let _sign = "";
	if (JSON.stringify(obj) != '{}') {

		let arr = new Array();
		let num = 0;
		for (let i in obj) {
			arr[num] = i;
			num++;
		}
		let sortArr = arr.sort();
		let str = config.Project;
		for (let i in sortArr) {
			str += sortArr[i] + '=' + obj[sortArr[i]] + '&';
		}

		//去除两侧字符串
		let char = '&'
		str = str.replace(new RegExp('^\\' + char + '+|\\' + char + '+$', 'g'), '');
		str += config.Project;
		//onsole.log(str);
		const utilMd5 = require('./md5.js');
		_sign = utilMd5(str).toUpperCase();
	}

	return _sign;
}

function setloc(key, value) {
	localStorage.setItem(key, value);
	setCookie(key, value, 365);
}

function getloc(key) {
	var val = localStorage.getItem(key);
	if (val == "") {
		val = getCookie(key);
	}
	return val;
}

function setCookie(c_name, value, expiredays) {
	var exdate = new Date();
	exdate.setDate(exdate.getDate() + expiredays);
	document.cookie =
		c_name +
		"=" +
		escape(value) +
		(expiredays == null ? "" : ";expires=" + exdate.toGMTString());
}

//取回cookie
function getCookie(c_name) {
	var name = c_name + "=";
	var ca = document.cookie.split(";");
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i].trim();
		if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
	}
	return null;
}

function timestampFormat(date) {
	function zeroize(num) {
		return (String(num).length == 1 ? '0' : '') + num;
	}

	date = new Date(date);
	date = parseInt(date.getTime() / 1000);

	var curTimestamp = parseInt(new Date().getTime() / 1000); //当前时间戳
	var timestampDiff = curTimestamp - date; // 参数时间戳与当前时间戳相差秒数

	var curDate = new Date(curTimestamp * 1000); // 当前时间日期对象
	var tmDate = new Date(date * 1000); // 参数时间戳转换成的日期对象

	var Y = tmDate.getFullYear(),
		m = tmDate.getMonth() + 1,
		d = tmDate.getDate();
	var H = tmDate.getHours(),
		i = tmDate.getMinutes(),
		s = tmDate.getSeconds();

	if (timestampDiff < 60) { // 一分钟以内
		return "刚刚";
	} else if (timestampDiff < 3600) { // 一小时前之内
		return Math.floor(timestampDiff / 60) + "分钟前";
	} else if (curDate.getFullYear() == Y && curDate.getMonth() + 1 == m && curDate.getDate() == d) {
		return '今天' + zeroize(H) + ':' + zeroize(i);
	} else {
		var newDate = new Date((curTimestamp - 86400) * 1000); // 参数中的时间戳加一天转换成的日期对象
		if (newDate.getFullYear() == Y && newDate.getMonth() + 1 == m && newDate.getDate() == d) {
			return '昨天' + zeroize(H) + ':' + zeroize(i);
		} else if (curDate.getFullYear() == Y) {
			return zeroize(m) + '月' + zeroize(d) + '日 ' + zeroize(H) + ':' + zeroize(i);
		} else {
			return Y + '年' + zeroize(m) + '月' + zeroize(d) + '日 ' + zeroize(H) + ':' + zeroize(i);
		}
	}
}

export default {
	sort_ascii,
	setloc,
	getloc,
	GetQueryString,
	timestampFormat
};
