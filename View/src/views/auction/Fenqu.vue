<template>
  <div class="fenqu">
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>
    <div class="fenqu-wrap">
      <div class="box">
        <div class="label">请选择你想要选择的分区</div>
        <div class="list">
          <div class="item" @click="itemClick(index)" v-for="(item,index) in fenquList" :key="index">
            <div>{{ item.name }} {{ item.minprice }}-{{ item.maxprice }}</div>
            <van-icon name="success" size="21px" color="#444" v-show="fenquIndex == index" />
          </div>
        </div>
      </div>

      <van-button block round color="linear-gradient(to right, #ff9700, #ed1c24)" @click="jinru">
        进入
      </van-button>
    </div>
  </div>
</template>

<script>
  import HeadBar from "@/components/HeadBar";
  import Vue from 'vue';
  import { Button, Icon } from 'vant';
  import { Toast } from 'vant';
  Vue.use(Button);
  Vue.use(Icon);
  Vue.use(Toast);

  export default {
    name: '',
    components: {
      HeadBar
    },
    data() {
      return {
        title: "选择分区",
        jine: "",
        fenquIndex: 0,
        sid: '',
        fenquList: []
      }
    },
    created() {
      if (this.$route.query.id != "") {
        this.sid = this.$route.query.id;
      }
      this.load();
      this.getPricerange()
    },
    methods: {
      itemClick(index) {
        this.fenquIndex = index
      },
      load: function () {
        //登录组件登录成功后会触发该函数,刷新父窗体数据
        /* if (this.$store.state.LoginState) {
          //子组件调用加载
          this.Wallet_GetWallet();
        } */
      },
      jinru: function () {
		  let min = this.fenquList[this.fenquIndex].minprice;
		  let max = this.fenquList[this.fenquIndex].maxprice;
		  let fqid = this.fenquList[this.fenquIndex].id;
		  this.$router.push("/QiangPaiQu?min=" + min + "&max=" + max + "&sid=" + this.sid + "&fqid=" + fqid)
		  
      //   let _this = this;
      //   let _toast = _this.$toast.loading({
      //     message: "正在加载...",
      //     duration: 0, // 持续展示 toast
      //   });
      //   _this.$request.post(
      //     "api/Wallets/GetWallet",
      //     {
      //       token: _this.$utils.getloc("token"),
      //       userid: _this.$utils.getloc("userid"),
      //       id: _this.$utils.getloc("id"),
      //     },
      //     (res) => {
      //       if (res.data.code == 0) {
      //         _this.$dialog.alert({
      //           title: "提示",
      //           message: res.data.msg,
      //         });
      //         return;
      //       }
      //       _toast.clear();
      //       var date = res.data.data.ulist;
      //       date.forEach(item => {
      //         if (item.cid == 2) {
      //           _this.jine = item.jine;
      //           var mimnum = 0;
      //           if (this.jine < this.fenquList[this.fenquIndex].minnum) {
      //             Toast.fail('信用值不足不能参与该区');
      //           } else {

      //             let min = this.fenquList[this.fenquIndex].minprice;
      //             let max = this.fenquList[this.fenquIndex].maxprice;
	 //              let fqid = this.fenquList[this.fenquIndex].id;
      //             this.$router.push("/QiangPaiQu?min=" + min + "&max=" + max + "&sid=" + this.sid + "&fqid=" + fqid)
      //           }
      //         }
      //       });
      //     }
      //   );
      },
      getPricerange() {
        let that = this
        that.$request.post(
          "api/Pricerange/List",
          {},
          (res) => {
            if (res.data.code == 0) {
              that.$dialog.alert({
                title: "提示",
                message: res.data.msg,
              });
              return;
            }
            that.fenquList = res.data.data
          }
        )
      }
    }
  }
</script>

<style scoped>
  .fenqu-wrap {
    min-height: calc(100vh - 90px);
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    padding: 20px;
    font-size: 15px;
  }

  .box {
    padding: 20px 10px;
    color: #fff;
    background: url("../../assets/img/fenqu.png");
    background-size: 100% 100%;
    border-radius: 10px;
    margin-bottom: 16px;
  }

  .label {
    padding: 10px 0;
  }

  .list .item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 20px;
    background-color: #fff;
    color: #000;
    margin-bottom: 16px;
    border-radius: 99rem;
  }
</style>