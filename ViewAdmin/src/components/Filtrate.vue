<template>
  <div>
    <div class="row" v-if="config.DateQuery">
      <div class="chunk">
        <el-date-picker v-model="container.Filtrate.Sdate" type="date" placeholder="开始日期" />
      </div>
      <small class="chunk">~</small>
      <div class="chunk">
        <el-date-picker v-model="container.Filtrate.Edate" type="date" placeholder="结束日期" />
      </div>
    </div>
    <div class="row" v-if="config.KeywordQuery">
      <div class="chunk">
        <el-input v-model="container.Filtrate.Keyword" placeholder="会员编号/姓名查询" />
      </div>
    </div>
    <div class="row" v-if="config.DateQuery || config.KeywordQuery">
      <div class="chunk">
        <el-button icon="el-icon-search" type="primary" size="mini" @click="Query()">查询</el-button>
      </div>
      <div class="chunk">
        <el-button icon="el-icon-refresh" type="info" size="mini" @click="Reset()">重置</el-button>
      </div>
    </div>
    <slot></slot>
    <div class="row">
      <div class="chunk paging">
        <el-pagination :page-size="container.Paging.PageSize" layout="total, sizes, prev, pager, next"
          :total="container.Paging.TotalCount" :current-page="container.Paging.PageIndex"
          @size-change="handleSizeChange" @current-change="handleCurrentChange" />
      </div>
    </div>
  </div>
</template>

<script>
  import Vue from "vue";
  import { DatePicker, Input, Pagination } from "element-ui";
  Vue.use(DatePicker);
  Vue.use(Input);
  Vue.use(Pagination);
  export default {
    props: {
      //默认配置
      config: {
        type: Object,
        default() {
          return {
            DateQuery: true, //日期查询
            KeywordQuery: true, //关键字查询
          };
        },
      },
      container: {
        type: Object,
        default() {
          return {
            IsFiltrate:false,//是否进行查询
            Filtrate: {
              Sdate: "",//开始日期
              Edate: "",//结束日期
              Keyword: "",//关键字内容
            },
            Paging: {
              PageIndex: 1, //当前页
              PageSize: 10, //每页显示数量
              TotalPages: "", //总页数
              TotalCount: "", //总条数
            }
          };
        },
      }
    },
    components: {},
    data() {
      return {};
    },
    created() {},
    methods: {
      //将查询参数传到父组件
      GetQueryData() {
        let _this = this;
        //开始日期和结束日期不管在不在查询状态都不能为空
        if(!_this.container.Filtrate.Sdate){
          _this.container.Filtrate.Sdate = new Date().TransDate("2020-01-01 00:00:00");
        }
        if(!_this.container.Filtrate.Edate){
          _this.container.Filtrate.Edate = new Date().TransDate(new Date());
        }
        _this.$emit("GetNewContainer", _this.container);
      },
      //查询
      Query(){
        let _this = this;
        _this.container.IsFiltrate = true;//进入查询状态
        _this.container.Paging.PageIndex = 1;//回到第一页
        _this.GetQueryData();
      },
      //重置
      Reset() {
        let _this = this;
        _this.container.IsFiltrate = false;//退出查询状态
        _this.container.Filtrate = {};//清空筛选参数
        _this.container.Paging.PageIndex = 1;//回到第一页
        _this.GetQueryData();
      },
      //一页多少条数据
      handleSizeChange(val) {
        this.container.Paging.PageSize = val;
        this.container.Paging.PageIndex = 1;
        this.GetQueryData();
      },
      //翻页
      handleCurrentChange(val) {
        this.container.Paging.PageIndex = val;
        this.GetQueryData();
      },
    },
  };
</script>

<style scoped>
  .row {
    display: flex;
    justify-content: flex-start;
    align-items: center;
    flex-wrap: wrap;
    padding-bottom: 10px;
  }

  .chunk {
    padding: 10px 10px 0 0;
  }
  .paging {
    margin-top: 20px;
    width: 100%;
    overflow-x: auto;
  }
</style>