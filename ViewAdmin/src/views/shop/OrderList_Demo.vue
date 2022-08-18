<template>
  <div>
    <PageTitle title="订单列表"></PageTitle>
    <div class="cardbox">
      <div class="cardcontent">
        <TableSearch>
          <el-row class="search-row">
            <el-col class="search-col" :md="6" :sm="24">
              <el-date-picker v-model="date1" type="date" placeholder="开始日期"></el-date-picker>
            </el-col>
            <el-col class="search-col" :md="6" :sm="24">
              <el-date-picker v-model="date2" type="date" placeholder="结束日期"></el-date-picker>
            </el-col>
          </el-row>
          <el-row class="search-row">
            <el-col class="search-col" :md="6" :sm="24">
              <el-select v-model="query_state" placeholder="请选择">
                <el-option
                  v-for="item in options"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-col>
            <el-col class="search-col" :md="6" :sm="24">
              <el-input v-model="input" placeholder="根据用户名查询"></el-input>
            </el-col>
          </el-row>
          <el-row class="search-row">
            <el-col class="search-col" :md="6" :sm="24">
              <el-button icon="el-icon-search" type="primary" size="mini" @click="search(1)">查询</el-button>
              <el-button icon="el-icon-refresh" type="primary" size="mini" @click="search(2)">重置</el-button>
            </el-col>
          </el-row>
        </TableSearch>

        <TablePadding
          :list="list"
          :zk="emd"
          :key="refresh"
          :ischeckbox="false"
          ref="tablepadding"
        >
          <el-table-column label="订单编号">
            <el-table-column prop="odate" label="时间" width="160"></el-table-column>
            <el-table-column prop="orderNo" label="编号" width="160"></el-table-column>
            <el-table-column label="商品" width="180">
              <el-table-column label="图片" width="80">
                <template slot-scope="scope">
                  <img :src="getimg(scope.row.img)" width="50px" height="50px" />
                </template>
              </el-table-column>
              <el-table-column prop="goodslist" label="名称" width="160"></el-table-column>
              <el-table-column prop="goodsnum" label="数量" width="50"></el-table-column>
            </el-table-column>
          </el-table-column>
          <el-table-column label="用户信息">
            <el-table-column prop="userid" label="会员编号" width="80"></el-table-column>
            <el-table-column prop="username" label="姓名" width="80"></el-table-column>
            <el-table-column prop="usertel" label="手机号" width="120"></el-table-column>
            <el-table-column label="地址">
              <el-table-column prop="sheng" label="省份" width="100"></el-table-column>
              <el-table-column prop="shi" label="市区" width="100"></el-table-column>
              <el-table-column prop="xian" label="县" width="100"></el-table-column>
              <el-table-column prop="useraddress" label="地址" width="220"></el-table-column>
            </el-table-column>
          </el-table-column>
          <el-table-column label="快递信息">
            <el-table-column label="快递公司" width="100">
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.kuaidiname"
                  @focus="Remember(scope.row)"
                  @change="UpDateKuaidiName(scope.row)"
                ></el-input>
              </template>
            </el-table-column>
            <el-table-column label="快递编号" width="100">
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.kuaidiNo"
                  @focus="Remember(scope.row)"
                  @change="UpDateKuaidiNo(scope.row)"
                ></el-input>
              </template>
            </el-table-column>
          </el-table-column>
          <el-table-column label="状态" width="80">
            <template slot-scope="scope">
              <div v-if="scope.row.orderstate==1" style="color: #409eff;">待发货</div>
              <div v-else-if="scope.row.orderstate==2" style="color: #e6a23c;">待收货</div>
              <div v-else-if="scope.row.orderstate==3" style="color: #67c23a;">已收货</div>
              <div v-else-if="scope.row.orderstate==4" style="color: #909399;">已退款</div>
            </template>
          </el-table-column>
          <el-table-column label="操作">
            <template slot-scope="scope">
              <el-button
                class="jiange"
                type="text"
                size="small"
                @click="Fahuo(scope.row,scope.$index)"
                v-if="scope.row.orderstate < 2"
              >发货</el-button>
              <el-button
                class="jiange"
                type="text"
                size="small"
                @click="Tuikuan(scope.row,scope.$index)"
                v-if="scope.row.orderstate != 4"
              >退款</el-button>
              <el-button
                style="color:#f56c6c;"
                class="jiange"
                type="text"
                size="small"
                @click="Delete(scope.row,scope.$index)"
              >删除</el-button>
            </template>
          </el-table-column>
        </TablePadding>
      </div>
    </div>
  </div>
</template>

<script>
import TablePadding from "../../components/TablePadding";
import PageTitle from "../../components/PageTitle";
import TableSearch from "../../components/TableSearch";
import Vue from "vue";
import {
  Input,
  Notification,
  DatePicker,
  Select,
  Table,
  TableColumn,
  Form,
  FormItem,
  Dialog,
  Popover,
} from "element-ui";
Vue.use(Dialog);
Vue.use(Input);
Vue.use(DatePicker);
Vue.use(Select);
Vue.use(Table);
Vue.use(TableColumn);
Vue.use(Form);
Vue.use(FormItem);
Vue.use(Popover);
Vue.prototype.$notify = Notification;
export default {
  components: {
    PageTitle,
    TablePadding,
    TableSearch,
  },
  data() {
    return {
      id: "",
      emdata: "",
      emd: false,
      dialogFormVisible: false,
      date1: "",
      date2: "",
      query_state: "",
      input: "",
      searchlist: [],
      refresh: false,
      list: [],
      options: [
        {
          value: "99",
          label: "全部订单",
        },
        {
          value: "1",
          label: "待发货",
        },
        {
          value: "2",
          label: "待收货",
        },
        {
          value: "3",
          label: "已收货",
        },
        {
          value: "4",
          label: "已退款",
        },
      ],
      delids: [],
      beforeitem: {},
    };
  },
  created() {
    this.getdata();
  },
  methods: {
    //修改快递名
    UpDateKuaidiName(row) {
      if (this.beforeitem.kuaidiname != row.kuaidiname) {
        this.$request.post(
          "Api/ShopOrder_Admin/UpDateKuaidiName",
          {
            token_admin: this.$utils.getloc("token_admin"),
            userid_admin: this.$utils.getloc("userid_admin"),
            id: row.id,
            kuaidiname: row.kuaidiname,
          },
          (res) => {
            this.beforeitem = {};
            this.$notify({
              title: "提示",
              message: res.data.msg,
              offset: 100,
              type: "success",
            });
          }
        );
      }
    },
    //修改快递编号
    UpDateKuaidiNo(row) {
      if (this.beforeitem.kuaidiNo != row.kuaidiNo) {
        this.$request.post(
          "Api/ShopOrder_Admin/UpDateKuaidiNo",
          {
            token_admin: this.$utils.getloc("token_admin"),
            userid_admin: this.$utils.getloc("userid_admin"),
            id: row.id,
            kuaidiNo: row.kuaidiNo,
          },
          (res) => {
            this.beforeitem = {};
            this.$notify({
              title: "提示",
              message: res.data.msg,
              offset: 100,
              type: "success",
            });
          }
        );
      }
    },
    //暂存修改前的内容
    Remember(row) {
      //使用json将会开新的地址，直接赋值的话地址不变无法暂存修改前的对象
      this.beforeitem = JSON.parse(JSON.stringify(row));
    },
    //获取数据
    getdata: function () {
      this.$request.post(
        "Api/ShopOrder_Admin/List",
        {
          token_admin: this.$utils.getloc("token_admin"),
          userid_admin: this.$utils.getloc("userid_admin"),
        },
        (res) => {
          this.list = res.data.data;
          this.searchlist = this.list;
          this.refresh = !this.refresh;
          console.log(this.searchlist);
        }
      );
    },
    search: function (lx) {
      let _list = this.list;
      if (lx == 1) {
        //筛选
        //日期筛选
        if (this.date1 && this.date2) {
          let start = this.date1.format("yyyy-MM-dd");
          let end = this.date2.format("yyyy-MM-dd") + " 23:59:59";
          _list = _list.filter(
            (item) => item.cdate >= start && item.cdate <= end
          );
        }
        //用户名or姓名筛选
        if (this.input) {
          _list = _list.filter(
            (item) => item.userid == this.input || item.username == this.input
          );
        }
        //类型筛选
        if (this.query_state != 99) {
          _list = _list.filter(
            (item) => item.orderstate == this.query_state
          );
        }
      } else if (lx == 2) {
        this.date1 = "";
        this.date2 = "";
        this.input = "";
      }
      this.searchlist = _list;
      this.refresh = !this.refresh;
    },
    Delete: function (row, index) {
      this.$request.post(
        "Api/ShopOrder_Admin/Delete",
        {
          token_admin: this.$utils.getloc("token_admin"),
          userid_admin: this.$utils.getloc("userid_admin"),
          delete_id: row.id.toString(),
        },
        (res) => {
          this.getdata();
          this.$notify({
            title: "提示",
            message: res.data.msg,
            offset: 100,
            type: "success",
          });
        },
        false,
        false
      );
    },
    Fahuo: function (row, index) {
      this.$request.post(
        "Api/ShopOrder_Admin/Pass",
        {
          token_admin: this.$utils.getloc("token_admin"),
          userid_admin: this.$utils.getloc("userid_admin"),
          id: row.id.toString(),
        },
        (res) => {
          this.searchlist[index].orderstate = 3;
          this.$notify({
            title: "提示",
            message: res.data.msg,
            offset: 100,
            type: "success",
          });
        },
        false,
        false
      );
    },
    Tuikuan: function (row, index) {
      this.$request.post(
        "Api/ShopOrder_Admin/Revoke",
        {
          token_admin: this.$utils.getloc("token_admin"),
          userid_admin: this.$utils.getloc("userid_admin"),
          id: row.id.toString(),
        },
        (res) => {
          this.searchlist[index].orderstate = 4;
          this.$notify({
            title: "提示",
            message: res.data.msg,
            offset: 100,
            type: "success",
          });
        },
        false,
        false
      );
    },
    //获取图片
    getimg(img) {
      return this.$config.img_url + img;
    },
  },
};
</script>

<style scoped="scoped">
.btn {
  color: white;
  text-decoration: none;
}

.routerlink {
  color: #409eff;
  text-decoration: none;
}

.demo-table-expand {
  font-size: 10px;
}

.demo-table-expand label {
  width: 90px;
  color: #99a9bf;
}

.demo-table-expand .el-form-item {
  margin-right: 0;
  margin-bottom: 0;
  width: 50%;
}
.jiange {
  margin: 0 0.2em;
}
</style>
