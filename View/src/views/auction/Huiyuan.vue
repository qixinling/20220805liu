<template>
  <div>
    <HeadBar :title="title" :bg="'transparent'"></HeadBar>

    <div class="list">
      <div class="item" v-for="(item,index) in list" :key="index">
        <div class="item-row" style="margin-bottom:4px">
          <div style="font-weight:600">{{ item.showTel }}</div>
          <div style="color:gray;font-size:13px">状态：正常</div>
        </div>
        <div class="item-row">
          {{ item.username }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import HeadBar from "@/components/HeadBar";

export default {
  name: '',
  components: {
    HeadBar
  },
  data() {
    return {
      title: document.title,
      list: []
    }
  },
  created() {
    this.getData()
  },
  methods: {
    getData() {
      let that = this
      that.$request.post(
        "api/Users/HsUser",
        {
          token: that.$utils.getloc("token"),
          userid: that.$utils.getloc("userid"),
          uid: that.$utils.getloc("id"),
        },
        (res) => {
          if (res.data.code == 0) {
            that.$dialog.alert({
              title: "提示",
              message: res.data.msg,
            });
            return;
          }
          let data = res.data.data
          data.forEach(item => {
            if(item.usertel.length >= 11) {
              item.showTel = item.usertel.replace(item.usertel.substring(3, item.usertel.length - 4), "****")
            } else {
              item.showTel = item.usertel
            }
          })
          that.list = data
        }
      )
    },
  }
}
</script>

<style scoped>
.item {
  padding: 14px 10px;
  font-size: 14px;
  border-bottom: 1px solid #ccc;
  background-color: #fff;
}

.item-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
</style>