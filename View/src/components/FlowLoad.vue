<template>
  <div class="flowload">
    <van-empty v-if="datalist.length == 0" description="暂无数据" />
    <van-list
      v-if="datalist.length > 0"
      v-model="loading"
      :finished="finished"
      finished-text="没有更多了"
      @load="onLoad"
    >
      <div v-for="(item, index) in list" :key="index">
        <slot v-bind:item="item"></slot>
      </div>
    </van-list>
  </div>
</template>

<script>
import Vue from "vue";
import { List } from "vant";
Vue.use(List);
import { Empty } from "vant";
Vue.use(Empty);
export default {
  name: "FlowLoad",
  props: {
    datalist: {
      type: null,
      value: []
    }
  },
  data() {
    return {
      list: [],
      index: 0,
      loading: false,
      finished: false
    };
  },
  methods: {
    onLoad() {
      let _this = this;
      // 异步更新数据
      // setTimeout 仅做示例，真实场景中一般为 ajax 请求
      setTimeout(() => {
        for (
          let i = _this.index * _this.$utils.page_size;
          i < (_this.index + 1) * _this.$utils.page_size;
          i++
        ) {
          if (_this.$props.datalist[i]) {
            _this.list.push(_this.$props.datalist[i]);
          } else {
            this.finished = true;
          }
        }
        _this.index += 1;
        // 加载状态结束
        this.loading = false;
      }, 1000);
    }
  }
};
</script>

<style>
.flowload {
}
</style>
