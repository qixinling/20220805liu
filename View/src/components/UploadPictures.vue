<template>
    <div>
        <van-field name="uploader" :label="title">
            <template #input>
                <van-uploader :disabled="disabled" :deletable="deletable" v-model="fileList" :after-read="afterRead" :max-count="config.maxCount" :max-size="config.maxSize" :before-read="beforeRead" @oversize="onOversize" />
            </template>
        </van-field>
    </div>
</template>

<script>
//使用方法：在需要上传图片的地方加入组件代码
//默认用法： <UploadPictures @getUploadPictures="getUploadPictures" />
//在组件挂上这个点击事件监听方法,当点击数据时会返回相应数据.@getUploadPictures="在父组件中的方法名(叫什么名字可以自己定义)"
//接收数据：在父组件中的方法名(value){ this.data.value = value; }

//存在数据时：<UploadPictures :haveImgList="haveImgList" />
//需要在父组件处理好图片集合传过来即可
//例如：haveImgList:[{url:"图片访问路径"},{url:"http://appserver.gxyhttest.com/Upload/tx.png"}]

//修改上传组件标题：<UploadPictures :title="title" />

//修改上传限制：<UploadPictures :disabled="true或false" />

//上传多张图片时：<UploadPictures :config="{ maxSize: 5 * 1024 * 1024 , maxCount:9 }" />
//注意不要单单传 { maxCount:9 } maxSize限制条件也要传过来

import Vue from "vue";
import { Uploader, Dialog, Field } from "vant";
Vue.use(Uploader);
Vue.use(Dialog);
Vue.use(Field);
export default {
    props: {
        config: {
            //上传组件的配置，需要修改时传对应结构的config对象进来就行
            type: Object,
            default() {
                return {
                    maxSize: 5 * 1024 * 1024,
                    maxCount: 1,
                };
            },
        },
        title: {
            type: String,
            default: "图片上传",
        },
        haveImgList: {
            //已存在图片时，可以传已有图片集合过来
            type: Array,
            default() {
                return [];
            },
        },
		deletable:{
			type: Boolean,
			default: false,
		},
        disabled: {
            //是否禁止上传
            type: Boolean,
            default: false,
        },
    },
    watch: {
        //深度监听数组，该数组数据更新时将fileList也更新
        haveImgList(newVal, oldVal) {
            this.fileList = newVal;
        },
        deep: true,
    },
    data() {
        return {
            fileList: [],
        };
    },
    created() {
        this.fileList = this.haveImgList;
        console.log(this.fileList)
    },
    methods: {
        afterRead(file) {
            let _this = this;
            file.status = "uploading";
            file.message = "上传中...";
            let _formdata = new FormData(); //创建form对象
            _formdata.append("file", file.file);
            let _posturl =
                _this.$config.send_url +
                "Api/Upload/UploadImages?token=" +
                encodeURIComponent(_this.$utils.getloc("token")) +
                "&userid=" +
                _this.$utils.getloc("userid");
            let _config = {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            };
            _this.axios
                .post(_posturl, _formdata, _config)
                .then((res) => {
                    if (res.data.code == 100) {
                        file.status = "";
                        file.message = "";
                        let data = res.data.data.imgname;
                        _this.$emit("getUploadPictures", data); //将结果抛出
                    } else {
                        file.status = "failed";
                        file.message = "上传失败";
                        Dialog.alert({
                            message: res.data.msg,
                        });
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        onOversize(file) {
            // 图片大小校验
            Dialog.alert({
                message: "请上传不5M内的图片",
            });
        },
        beforeRead(file) {
            // 图片格式校验
            if (
                file.type == "image/jpeg" ||
                file.type == "image/png" ||
                file.type == "image/gif"
            ) {
                return true;
            } else {
                Dialog.alert({
                    message: "请上传 jpg,png,gif 格式图片",
                });
                return false;
            }
        },
    },
};
</script>

<style scoped>
</style>