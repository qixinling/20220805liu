using Newtonsoft.Json;
using Server.Models.DataBaseModels;

using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using Microsoft.EntityFrameworkCore;

namespace Server.Utils.Permission_Utils
{
    public static class PermissionUtils
    {
        /// <summary>
        /// 获取默认权限
        /// </summary>
        /// <returns></returns>
        public static List<PermissionMod> GetDefaultPermission()
        {
            PermissionMod pm;
            List<PermissionMod> pmlist = new List<PermissionMod>();


            #region 用户管理
            pm = new PermissionMod()
            {
                Name = "用户管理",
                Pclist = new List<List<PermissionChildrenMod>>()
                {
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "临时会员列表",
                            Path = "Users_Admin/List"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "激活",
                            Path = "Users_Admin/Jihuo"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "正式会员列表",
                            Path = "Users_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "万能账号",
                            Path = "Users_Admin/WnList"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除",
                            Path = "Users_Admin/Delete"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "锁定",
                            Path = "Users_Admin/Suoding"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "解锁",
                            Path = "Users_Admin/Jiesuo"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "登录会员前台",
                            Path = "Users_Admin/Login"
                        }
                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod
                        {
                            Name = "会员资料",
                            Path = "Users_Admin/Get"
                        },
                        new PermissionChildrenMod
                        {
                            Name = "修改资料",
                            Path = "Users_Admin/Update"
                        },
                        new PermissionChildrenMod
                        {
                            Name = "修改密码",
                            Path = "Users_Admin/Update_Password"
                        },
                        new PermissionChildrenMod
                        {
                            Name = "修改推荐人",
                            Path = "Users_Admin/Update_Rename"
                        },
                        new PermissionChildrenMod
                        {
                            Name = "会员业绩",
                            Path = "Users_Admin/QueryPerformance"
                        }
                    },
                    #endregion
                    
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod
                        {
                            Name = "推荐图谱",
                            Path = "Users_Admin/Tree"
                        }
                    },
                    #endregion
                }
            };
            pmlist.Add(pm);
            #endregion
            #region 奖金管理
            pm = new PermissionMod()
            {
                Name = "奖金管理",
                Pclist = new List<List<PermissionChildrenMod>>()
                {
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "分红列表",
                            Path = "BonusJiesuan_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "分红结算",
                            Path = "BonusJiesuan_Admin/Jiesuan"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "奖金总表",
                            Path = "Bonus_Admin/Time_List"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "奖金详情",
                            Path = "Bonus_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "奖金来源",
                            Path = "Bonus_Admin/Source_List"
                        }

                    },
                    #endregion
                }
            };
            pmlist.Add(pm);
            #endregion
            #region 财务管理
            pm = new PermissionMod()
            {
                Name = "财务管理",
                Pclist = new List<List<PermissionChildrenMod>>()
                {
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "货币管理",
                            Path = "WalletsCoin_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加货币",
                            Path = "WalletsCoin_Admin/Add"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除货币",
                            Path = "WalletsCoin_Admin/Delete"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "货币启用",
                            Path = "WalletsCoin_Admin/ChangeState"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "修改英文名称",
                            Path = "WalletsCoin_Admin/ChangeCoinName"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "修改中文名称",
                            Path = "WalletsCoin_Admin/ChangeCoinNameZh"
                        }
                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "充值货币",
                            Path = "WalletsChongzhi_Select_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加充值货币",
                            Path = "WalletsChongzhi_Select_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "修改充值货币",
                            Path = "WalletsChongzhi_Select_Admin/Update"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除充值货币",
                            Path = "WalletsChongzhi_Select_Admin/Delete"
                        }
                    },
                    #endregion
                     #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "提现货币",
                            Path = "WalletsTixian_Select_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加提现货币",
                            Path = "WalletsTixian_Select_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "修改提现货币",
                            Path = "WalletsTixian_Select_Admin/Update"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除提现货币",
                            Path = "WalletsTixian_Select_Admin/Delete"
                        }
                    },
                    #endregion
                     #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "转账货币",
                            Path = "WalletsZhuanzhang_Select_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加转账货币",
                            Path = "WalletsZhuanzhang_Select_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "修改转账货币",
                            Path = "WalletsZhuanzhang_Select_Admin/Update"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除转账货币",
                            Path = "WalletsZhuanzhang_Select_Admin/Delete"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "团队限制",
                            Path = "WalletsZhuanzhang_Select_Admin/IsState"
                        }
                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "转换货币",
                            Path = "WalletsZhuanhuan_Select_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加转换货币",
                            Path = "WalletsZhuanhuan_Select_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "修改转换货币",
                            Path = "WalletsZhuanhuan_Select_Admin/Update"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除转换货币",
                            Path = "WalletsZhuanhuan_Select_Admin/Delete"
                        }
                    },
                    #endregion

                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "货币增减列表",
                            Path = "WalletsZengjian_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "货币增减",
                            Path = "WalletsZengjian_Admin/Zengjian"
                        },
                        // new PermissionChildrenMod()
                        //{
                        //    Name = "删除增减记录",
                        //    Path = "WalletsZengjian_Admin/Delete"
                        //},
                        new PermissionChildrenMod()
                        {
                            Name = "货币查询",
                            Path = "Wallets_Admin/GetWallet"
                        }
                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "充值列表",
                            Path = "WalletsChongzhi_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "充值审核",
                            Path = "WalletsChongzhi_Admin/Pass"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "充值撤销",
                            Path = "WalletsChongzhi_Admin/Revoke"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "充值删除",
                            Path = "WalletsChongzhi_Admin/Delete"
                        },
                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "提现列表",
                            Path = "WalletsTixian_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "提现审核",
                            Path = "WalletsTixian_Admin/Pass"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "提现撤销",
                            Path = "WalletsTixian_Admin/Revoke"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "提现删除",
                            Path = "WalletsTixian_Admin/Delete"
                        },
                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "转换列表",
                            Path = "WalletsZhuanhuan_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "转换删除",
                            Path = "WalletsZhuanhuan_Admin/Delete"
                        }

                    },
                    #endregion
                     #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "转账列表",
                            Path = "WalletsZhuanzhang_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "转账删除",
                            Path = "WalletsZhuanzhang_Admin/Delete"
                        }

                    },
                    #endregion
                }
            };
            pmlist.Add(pm);
            #endregion
            #region 商城管理
            pm = new PermissionMod()
            {
                Name = "商城管理",
                Pclist = new List<List<PermissionChildrenMod>>()
                {
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "幻灯片列表",
                            Path = "Slide_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加幻灯片",
                            Path = "Slide_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "查看幻灯片",
                            Path = "Slide_Admin/Get"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "修改幻灯片",
                            Path = "Slide_Admin/Update"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除幻灯片",
                            Path = "Slide_Admin/Delete"
                        }

                    },
                    #endregion
                     #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "商城图片列表",
                            Path = "ShopImg_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加商城图片",
                            Path = "ShopImg_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "查看商城图片",
                            Path = "ShopImg_Admin/Get"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "修改商城图片",
                            Path = "ShopImg_Admin/Update"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除商城图片",
                            Path = "ShopImg_Admin/Delete"
                        }

                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "大类列表",
                            Path = "ShopGoodsSort_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加大类",
                            Path = "ShopGoodsSort_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "查看单个大类",
                            Path = "ShopGoodsSort_Admin/Get"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "修改大类",
                            Path = "ShopGoodsSort_Admin/Update"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除大类",
                            Path = "ShopGoodsSort_Admin/Delete"
                        }

                    },
                    #endregion
                     #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "小类列表",
                            Path = "ShopGoodsSortChild_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加小类",
                            Path = "ShopGoodsSortChild_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "修改小类",
                            Path = "ShopGoodsSortChild_Admin/Update"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "查看单个小类",
                            Path = "ShopGoodsSortChild_Admin/Get"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除小类",
                            Path = "ShopGoodsSortChild_Admin/Delete"
                        }

                    },
                    #endregion
                     #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "商品列表",
                            Path = "ShopGoods_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加商品",
                            Path = "ShopGoods_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "修改商品",
                            Path = "ShopGoods_Admin/Update"
                        },
                           new PermissionChildrenMod()
                        {
                            Name = "查看单个商品",
                            Path = "ShopGoods_Admin/Get"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除商品",
                            Path = "ShopGoods_Admin/Delete"
                        }

                    },
                    #endregion
                     #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "订单列表",
                            Path = "ShopOrder_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "查看订单详情",
                            Path = "ShopOrder_Admin/Child_List"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "填写快递单",
                            Path = "ShopOrder_Admin/Update"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "发货",
                            Path = "ShopOrder_Admin/Pass"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "退货",
                            Path = "ShopOrder_Admin/Revoke"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除",
                            Path = "ShopOrder_Admin/Delete"
                        }

                    },
                    #endregion
                }
            };
            pmlist.Add(pm);
            #endregion
            #region 信息管理
            pm = new PermissionMod()
            {
                Name = "信息管理",
                Pclist = new List<List<PermissionChildrenMod>>()
                {
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "客服消息列表",
                            Path = "Msg_Admin/List_Chating"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "发送消息",
                            Path = "Msg_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "获取聊天记录",
                            Path = "Msg_Admin/List_First"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "获取用户",
                            Path = "Msg_Admin/List_User"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "获取端口号",
                            Path = "WebSocket_Admin/GetPort"
                        }
                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "查看会员帮助",
                            Path = "Article_Admin/Get"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "修改帮助",
                            Path = "Article_Admin/Update"
                        }
                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "自动回复列表",
                            Path = "Help_Admin/Question_List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加问题回复",
                            Path = "Help_Admin/Add"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "修改问题回复",
                            Path = "Help_Admin/Update"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "排序",
                            Path = "Help_Admin/ChangeRank"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "是否显示",
                            Path = "Help_Admin/ChangeShow"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除问题回复",
                            Path = "Help_Admin/Delete"
                        }
                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "新闻列表",
                            Path = "News_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "新闻添加",
                            Path = "News_Admin/Add"
                        },

                         new PermissionChildrenMod()
                        {
                            Name = "新闻删除",
                            Path = "News_Admin/Delete"
                        },
                          new PermissionChildrenMod()
                        {
                            Name = "查看单个修改",
                            Path = "News_Admin/Get"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "新闻修改",
                            Path = "News_Admin/Update"
                        }

                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "业绩统计",
                            Path = "SystemAchievement_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "登录日志",
                            Path = "SystemLogLogin_Admin/Login_List"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "操作日志",
                            Path = "SystemLogOperation_Admin/Operation_List"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "删除操作日志",
                            Path = "SystemLogOperation_Admin/Operation_Delete"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "异常日志",
                            Path = "SystemLogError_Admin/Error_List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "删除异常日志",
                            Path = "SystemLogError_Admin/Error_Delete"
                        }

                    },
                    #endregion
                }
            };
            pmlist.Add(pm);
            #endregion
            #region 竞拍管理
            pm = new PermissionMod()
            {
                Name = "竞拍管理",
                Pclist = new List<List<PermissionChildrenMod>>()
                {
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "场次列表",
                            Path = "Site_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加场次",
                            Path = "Site_Admin/Add"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "修改场次",
                            Path = "Site_Admin/Update"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "删除场次",
                            Path = "Site_Admin/Delete"
                        },

                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "珠宝列表",
                            Path = "Jewellery_Admin/List"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "添加珠宝",
                            Path = "Jewellery_Admin/Add"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "修改珠宝",
                            Path = "Jewellery_Admin/Update"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "删除珠宝",
                            Path = "Jewellery_Admin/Delete"
                        },

                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "添加卖方",
                            Path = "UsersHold_Admin/Add"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "首单卖方列表",
                            Path = "UsersHold_Admin/SdList"
                        },


                    },
                    #endregion
            
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "竞拍大厅列表",
                            Path = "UsersHold_Admin/SellList"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "卖家删除",
                            Path = "UsersHold_Admin/Delete"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "批量修改场次",
                            Path = "UsersHold_Admin/UpdateSite"
                        },

                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "交易中列表",
                            Path = "UsersHold_Admin/SellList"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "撤销买家",
                            Path = "UsersHold_Admin/Revoke"
                        },

                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "交易结束列表",
                            Path = "UsersHold_Admin/SellList"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "恢复交易",
                            Path = "UsersHold_Admin/Recovery"
                        },

                    },
                    #endregion
                     #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "自由交割订单",
                            Path = "ShopOrder_Admin/List"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "强制交割订单",
                            Path = "ShopOrder_Admin/List"
                        },
                          new PermissionChildrenMod()
                        {
                            Name = "发货",
                            Path = "ShopOrder_Admin/Pass"
                        },
                            new PermissionChildrenMod()
                        {
                            Name = "删除",
                            Path = "ShopOrder_Admin/Delete"
                        },

                    },
                    #endregion
                    #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "申诉列表",
                            Path = "UsersHold_Admin/ShensuList"
                        },

                    },
                    #endregion
                     #region 换行
                    new List<PermissionChildrenMod>()
                    {
                        new PermissionChildrenMod()
                        {
                            Name = "拆单列表",
                            Path = "UsersHold_Admin/CaidanList"
                        },
                        new PermissionChildrenMod()
                        {
                            Name = "拆单",
                            Path = "UsersHold_Admin/Caidan"
                        },
                         new PermissionChildrenMod()
                        {
                            Name = "退回大厅",
                            Path = "UsersHold_Admin/Dating"
                        },

                    },
                    #endregion
                }
            };
            pmlist.Add(pm);
            #endregion
            return pmlist;
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="Userid_admin">管理员编号</param>
        /// <param name="Controller">控制器</param>
        /// <param name="Action">方法名</param>
        /// <returns>是否通过</returns>
        public static bool CheckPermission(string Userid_admin, string Controller = null, string Action = null)
        {

            bool Pass = false;
            try
            {
                using DbConnect _dbConnect = DbConnectUtils.GetDbContext();
                DbSystemAdmin ua = _dbConnect.DbSystemAdmin.Include(a => a.GidNavigation).FirstOrDefault(a => a.Userid.Equals(Userid_admin));
                if (ua == null) { return Pass; }
                //总管理员直接通过
                if (ua.Id == 1) { return true; }

                //验证非admin的管理权限
                if (ua.GidNavigation == null) { return Pass; }

                //不传值的直接视为无权限
                if (Controller == null || Action == null) { return Pass; }
                string Path = Controller + "/" + Action;

                List<PermissionMod> pmlist = JsonConvert.DeserializeObject<List<PermissionMod>>(ua.GidNavigation.Permission);

                foreach (PermissionMod pm in pmlist)
                {
                    foreach (List<PermissionChildrenMod> pclist in pm.Pclist)
                    {
                        foreach (PermissionChildrenMod pcm in pclist)
                        {
                            //返回对应路径的Display
                            if (pcm.Path.Contains(Path))
                            {
                                return pcm.Display;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper._.Error("验证权限出错", ex);
            }
            return Pass;
        }
    }

    public class PermissionMod
    {
        public string Name { get; set; }
        public bool Display { get; set; }

        public List<List<PermissionChildrenMod>> Pclist { get; set; }

        public PermissionMod()
        {
            Display = false;
            Pclist = new List<List<PermissionChildrenMod>>();
        }
    }

    public class PermissionChildrenMod
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool Display { get; set; }

        public PermissionChildrenMod()
        {
            Display = false;
        }
    }
}
