<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <div>
      <van-cell title="修改密码" is-link to="Password?lx=1" />
      <van-cell title="修改支付密码" is-link to="Password?lx=2" />
    </div>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";
import Vue from 'vue';
import { Cell } from 'vant';

Vue.use(Cell);

export default {
  name: '',
  components: {
    HeadBar
  },
  data() {
    return {
      title: "安全中心"
    }
  }
}
</script>

<style scoped>
</style>