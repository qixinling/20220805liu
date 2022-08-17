<template>
  <div>
    <van-nav-bar
      :fixed="fixed"
      :placeholder="placeholder"
      :border="false"
      :style="'background:'+ bg"
      :title="title"
      :left-arrow="leftarrow"
      @click-left="onClickLeft"
    />
  </div>
</template>

<script>
import Vue from "vue";
import { NavBar } from "vant";

Vue.use(NavBar);
export default {
  props: {
    title: {
      type: String,
      default() {
        return "";
      }
    },
    toback: {
      type: String,
      default() {
        return "";
      }
    },
    bg: {
      type: String,
      default() {
        return "#fff";
      }
    },
    fixed: {
      type: Boolean,
      default() {
        return false;
      }
    },
    placeholder: {
      type: Boolean,
      default() {
        return false;
      }
    },
    leftarrow: {
      type: Boolean,
      default() {
        return true
      }
    }
  },
  data() {
    return {};
  },
  methods: {
    onClickLeft: function() {
      if (this.toback) {
        this.$router.replace({ path: this.toback });
      } else {
        this.$router.back(-1);
      }
    },
    onClickRight: function() {}
  }
};
</script>

<style scoped>
	/deep/.van-nav-bar .van-icon {
	    color: #000;
	}
	/deep/.van-nav-bar__title {
		color: #000; 
	}
</style>
