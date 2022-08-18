import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    LoginState: false //登录状态
  },
  mutations: {
    setLoginState: function(state, val) {
      state.LoginState = val;
    }
  },
  actions: {},
  modules: {}
});
