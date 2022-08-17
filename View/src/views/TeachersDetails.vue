<template>
  <div>
    <HeadBar :title="title"></HeadBar>

    <div class="details-content">
      <img :src="getimg(data.tximg)" style="width:100%" />
      <div style="margin-top:10px">{{ data.name }}</div>
      <div class="introduce">
        {{data.introduce}}
      </div>
    </div>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";

export default {
  name: "",
  components: {
    HeadBar,
  },
  data() {
    return {
      title: document.title,
      tid: "",
      data: {},
    };
  },
  created() {
    if (this.$route.query.tid) {
      this.tid = this.$route.query.tid;
      this.getteachers();
    }
  },
  methods: {
    getimg: function (img) {
      if (img != "") {
        return this.$config.send_url + "Upload/" + img;
      }
    },
    getteachers: function () {
      var _this = this;
      _this.$request.post(
        "api/Teachers/Get",
        {
          tid: _this.tid,
          token: _this.$utils.getloc("token"),
          userid: _this.$utils.getloc("userid"),
        },
        (res) => {
          if (res.data.code == 0) {
            _this.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
            return;
          }
          _this.data = res.data.data[0];
        }
      );
    },
  },
};
</script>

<style scoped>
.details-content {
  /* margin-top: 10px; */
  padding: 10px;
  min-height: calc(100vh - 56px);
  text-align: center;
  background-color: #fff;
}

.introduce {
  margin-top: 10px;
  font-size: 14px;
}
</style>