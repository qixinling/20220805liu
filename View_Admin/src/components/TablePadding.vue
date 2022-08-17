<template>
    <div>
        <template v-if="displayKey.length">
            <el-collapse v-model="activeNames" :class="activeNames.length>0 ? 'search-card' : ''">
                <el-collapse-item name="search">
                    <template slot="title">
                        <i class="el-icon-search" style="font-size: 14px; margin-right: 5px;"></i> <span style="font-size: 14px;font-weight: 300;">搜索</span>
                    </template>
                    <div class="search-form">
                        <el-form ref="form" label-width="60px">
                            <el-form-item label="时间" v-if="isdate">
                                <el-col :span="24">
                                    <el-date-picker type="date" placeholder="开始日期" v-model="date1"></el-date-picker>
                                    -
                                    <el-date-picker type="date" placeholder="结束日期" v-model="date2"></el-date-picker>
                                </el-col>
                            </el-form-item>

                            <el-form-item label="筛选">
                                <el-radio v-for="item in searchOptions" :key="item.key" v-model="radio" :label="item.key" @change="onSelect(item)">{{item.name}}</el-radio>
                            </el-form-item>
                            <el-form-item>
                                <el-input v-model="input" placeholder="请输入查询内容"></el-input>
                            </el-form-item>
                            <el-form-item v-show="radioObjList.length > 0">
                                <el-tag style="margin-right:5px;" v-for="(item,index) in radioObjList" :key="item.name" closable @close="onClose(index)">
                                    {{item.name}}:{{item.value}}
                                </el-tag>
                            </el-form-item>

                            <el-form-item v-if="$slots.search_slot">
                                <slot name="search_slot" label="拓展筛选"></slot>
                            </el-form-item>

                            <el-form-item>
                                <el-button icon="el-icon-search" type="primary" size="mini" @click="addFilter">添加条件并搜索
                                </el-button>
                                <el-button v-show="radioObjList.length > 0" icon="el-icon-refresh" type="primary" size="mini" @click="resetFilter">清空条件
                                </el-button>
                            </el-form-item>
                        </el-form>
                    </div>
                </el-collapse-item>
            </el-collapse>
        </template>

        <el-row slot="btn_solt">
            <el-row style="margin-bottom: 5px;">
                <el-col :md="12" :sm="24" :span="24">
                    <slot name="btn_solt"></slot> <span v-html="'&#12288;'"></span>
                </el-col>
                <el-col :md="12" :sm="24" :span="24" style="text-align: right;" v-if="isexcel">
                    <el-button icon="el-icon-document" size="mini" @click="exportExcel" v-if="list.length>0">导出Excel</el-button>
                </el-col>
            </el-row>
        </el-row>

        <template>
            <el-table id="export-table1" :header-cell-style="{background:'#F5F7FA',color:'#606266'}" border ref="multipleTable" :data="tableData" tooltip-effect="dark" max-height="540" style="width: 100%" @selection-change="handleSelectionChange" :show-summary="summary" :summary-method="getSummaries">
                <el-table-column v-if="ischeckbox" type="selection" width="55"></el-table-column>
                <slot></slot>
				<!--<el-table-column></el-table-column>--><!-- 在表格的每条数据最后一列占位,使x、y轴的线不断 -->
            </el-table>
        </template>

        <div class="padding">
            <el-pagination :page-size="pagesize" :page-sizes="pagesizes" layout="total, sizes, prev, pager, next" :total="searchData.length" @size-change="handleSizeChange" @current-change="handleCurrentChange">
            </el-pagination>
        </div>
    </div>
</template>

<script>
import Vue from "vue";
import FileSaver from "file-saver";
import XLSX from "xlsx";
import {
    DatePicker,
    Form,
    FormItem,
    Table,
    TableColumn,
    Pagination,
    Collapse,
    CollapseItem,
    Input,
    Checkbox,
    CheckboxButton,
    CheckboxGroup,
    Tag,
} from "element-ui";

Vue.use(Checkbox);
Vue.use(CheckboxButton);
Vue.use(CheckboxGroup);
Vue.use(DatePicker);
Vue.use(Form);
Vue.use(FormItem);
Vue.use(Input);
Vue.use(Collapse);
Vue.use(CollapseItem);
Vue.use(Pagination);
Vue.use(Table);
Vue.use(TableColumn);
Vue.use(Tag);
export default {
    name: "tablepadding",
    props: {
        title: {
            type: String,
            default: "表格",
        },
        list: {
            type: Array,
            default: new Array(),
        },
        ischeckbox: {
            type: Boolean,
            default: true,
        },
        isexcel: {
            type: Boolean,
            default: true,
        },
        summary: {
            type: Boolean,
            default: false,
        },
    },
    data() {
        return {
            index: 1,
            pagesize: 10,
            pagesizes: [10, 20, 50, 100, 500, 1000],
            tableData: [],
			searchData:[],
            select: [],
            expands: false,
            /*查询参数*/
            input: "",
            date1: "",
            date2: "",
            searchOptions: [],
            displayKey: [],
            isdate: false,

            radio: "",
            radioObj: "",
            radioObjList: [],
            /*查询参数*/

            activeNames: [],
			
			refresh:false
        };
    },
    created() {
        //绑定回车事件
        window.addEventListener("keydown", this.keyDown);
    },
    mounted() {
		this.searchData = this.list;
		this.padding();
		this.pagesizes.push(this.list.length);
		
        this.$refs.multipleTable.$children.forEach((item) => {
            if (item.labelClassName) {
                this.searchOptions.push({
                    name: item.label,
                    key: item.labelClassName,
                });
                this.displayKey.push(item.labelClassName);
            }
        });

        if (this.list.length > 0) {
            this.displayKey.forEach((key) => {
                if (
                    isNaN(this.list[0][`${key}`]) &&
                    !isNaN(Date.parse(this.list[0][`${key}`]))
                ) {
                    this.isdate = true;
                }
            });
        }
    },
    methods: {
        onSelect(selectData) {
            this.radioObj = selectData;
            this.input = "";
        },
        onClose(index) {
            this.radioObjList.splice(index, 1);
            this.input = "";
            this.search();
        },
        addFilter() {
            let input = this.input;
            let radioObj = this.radioObj;
            let radioObjList = this.radioObjList;
            if (radioObj != "") {
                radioObj["value"] = input;
                if (radioObjList.length > 0) {
                    radioObjList.filter(() => {
                        if (radioObjList.indexOf(radioObj) == -1) {
                            radioObjList.push(radioObj);
                        }
                    });
                } else {
                    radioObjList.push(radioObj);
                }
            }
            this.search();
        },
        resetFilter() {
            this.radioObjList = [];
            this.tableData = this.list;
        },
        search: function () {
            //筛选
            let _list = this.list;
//console.log(_list);
            //如需手动过滤数据,需在父组件创建ExpandSearch函数,在该函数过滤后再传递回子组件
            try {
                _list = this.$parent.ExpandSearch(_list);
            } catch (err) {
                console.log(err);
            }
			
            //日期筛选
            if (this.date1 && this.date2) {
                let dateList = _list;
                _list = [];

                let start = this.date1.format("yyyy-MM-dd");
                let end = this.date2.format("yyyy-MM-dd") + " 23:59:59";

                dateList.forEach((item) => {
                    for (let key in item) {
                        //获取此条数据所有字段名
                        if (this.displayKey.indexOf(key) > -1) {
                            if (item[`${key}`]) {
                                //保证字段名的值不为null
                                if (
                                    isNaN(item[`${key}`]) &&
                                    !isNaN(Date.parse(item[`${key}`]))
                                ) {
                                    if (
                                        item[`${key}`] >= start &&
                                        item[`${key}`] <= end
                                    ) {
                                        _list.push(item);
                                    }
                                }
                            }
                        }
                    }
                });
            }
            //多条件筛选
            let radioObjList = this.radioObjList;
            radioObjList.forEach((factor) => {
                _list = _list.filter((item) => {
                    //只有字符串才能使用indexOf,比较源时必须toString
                    if (item[`${factor.key}`].toString().indexOf(factor.value) > -1) {
                        return item;
                    }
                });
            });
			
			this.searchData = _list;
			this.padding();
        },
        keyDown() {
            let _key = window.event.keyCode;
            //!this.clickState是防止用户重复点击回车
            if (_key === 13 && !this.clickState) {
                this.addFilter();
            }
        },
        getSummaries(param) {
            if (this.list != null) {
                param.data = this.list;
                const { columns, data } = param;
                const sums = [];
                columns.forEach((column, index) => {
                    if (index === 0) {
                        sums[index] = "总计";
                        return;
                    }
                    const values = data.map((item) =>
                        Number(item[column.property])
                    );
                    if (!values.every((value) => isNaN(value))) {
                        sums[index] = values.reduce((prev, curr) => {
                            const value = Number(curr);
                            if (!isNaN(value)) {
                                return prev + curr;
                            } else {
                                return prev;
                            }
                        }, 0);
                    } else {
                        sums[index] = "-";
                    }
                });
                return sums;
            }
        },
        padding: function () {
            this.tableData = this.searchData.filter(
                (item, index) =>
                    index >= (this.index - 1) * this.pagesize &&
                    index < this.index * this.pagesize
            );
        },
        handleSizeChange(val) {
            this.pagesize = val;
            this.index = 1;
            this.padding();
        },
        handleCurrentChange(val) {
            this.index = val;
            this.padding();
        },
        handleSelectionChange(val) {
            this.select = val;
        },
        exportExcel: function () {
            // 导出的内容只做解析，不进行格式转换
            let xlsxParam = {
                raw: true,
            };
            let wb = null;
            let tableName = "";
            let randomString = this.randomString(6);

            wb = XLSX.utils.table_to_book(
                document.querySelector("#export-table1"),
                xlsxParam
            );
            // 这里的randomString非必须，只是生成一串随机码
            // 便于下载多个文件而不重名
            //tableName = `用户表-${randomString}.xlsx`
            tableName = `${this.title}.xlsx`;

            /* get binary string as output */
            let wbout = XLSX.write(wb, {
                bookType: "xlsx",
                bookSST: true,
                type: "array",
            });
            try {
                // eslint-disable-next-line no-undef
                FileSaver.saveAs(
                    new Blob([wbout], {
                        type: "application/octet-stream",
                    }),
                    tableName
                );
            } catch (e) {
                if (typeof console !== "undefined") {
                    console.log(e, wbout);
                }
            }
            return wbout;
        },
        randomString: function (len) {
            len = len || 32;
            // 屏蔽了容易让人混淆的字符，比如数字1和字母l,，数字0和字母o……
            var chars = "ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678";
            var maxPos = chars.length;
            var str = "";
            for (let i = 0; i < len; i++) {
                str += chars.charAt(Math.floor(Math.random() * maxPos));
            }
            return str;
        },
    },
};
</script>

<style scoped="scoped">
.el-table{
	font-size: 12px;
}

.el-collapse {
    border: 0;
}

/deep/.el-collapse-item__header {
    border: 0;
}

/deep/.el-collapse-item__content {
    padding-bottom: 0;
}

/deep/.el-collapse-item__wrap {
    border-bottom: none;
}

.padding {
    margin-top: 20px;
    width: 100%;
    overflow-x: auto;
}
.search-form {
    width: 600px;
}
@media screen and (max-width: 900px) {
    .search-form {
        width: 100%;
        overflow-x: auto;
    }
}
.search-card {
    /* margin-bottom: 20px;
    padding: 0 20px;
    box-shadow: 0 2px 12px 0 rgb(0 0 0 / 10%);
    border-radius: 10px; */
}
</style>
