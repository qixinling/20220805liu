import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";

import config from "./assets/js/config.js";
Vue.prototype.$config = config;
import utils from "./assets/js/utils.js";
Vue.prototype.$utils = utils;

import request from "./assets/js/request.js";
Vue.prototype.$request = request;

import { Button, Col, Row, Icon, Toast, Notify, Dialog } from "vant";

Vue.use(Dialog);
Vue.use(Notify);
Vue.use(Toast);
Vue.use(Icon);
Vue.use(Col);
Vue.use(Row);
Vue.use(Button);

//loading时不让点击其他位置
Toast.setDefaultOptions("loading", {
  forbidClick: true,
});

import Axios from "axios";

Vue.prototype.axios = Axios;
//解决axios post数据接收不到
import qs from "qs";
Vue.prototype.qs = qs;

// 根据路由设置标题
router.beforeEach((to, from, next) => {
  if (to.meta.title) {
    document.title = to.meta.title;
  }
  next();
});

Vue.config.productionTip = false;

//集成html5 plus
var onPlusReady = function(callback, context = this) {
  if (window.plus) {
    callback.call(context);
  } else {
    document.addEventListener("plusready", callback.bind(context));
  }
};
Vue.mixin({
  beforeCreate() {
    onPlusReady(() => {
      this.plusReady = true;
    }, this);
  },
  methods: {
    onPlusReady: onPlusReady,
  },
});

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount("#app");
