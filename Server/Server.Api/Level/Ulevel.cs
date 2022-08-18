using Microsoft.EntityFrameworkCore;
using Server.Api.Method;
using Server.Api.Utils;
using Server.Api.Utils.Public;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Level
{
    public class Ulevel
    {
        public int Lx => 0;
        public int Level { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsOption { get; set; }

        public List<Ulevel> GetLevels(DbConnect dbConnect = null)
        {
            List<Ulevel> levelList = new List<Ulevel>
            {
                new Ulevel{
                    Level = 0,
                    Name = "会员",
                    IsOption = false
                },
                new Ulevel
                {
                    Level = 1,
                    Name = "初级",
                    IsOption = true
                },
                new Ulevel
                {
                    Level = 2,
                    Name = "中一级",
                    IsOption = true
                },
                new Ulevel
                {
                    Level = 3,
                    Name = "中二级",
                    IsOption = true
                },
                new Ulevel
                {
                    Level = 4,
                    Name = "中三级",
                    IsOption = true
                },
                new Ulevel
                {
                    Level = 5,
                    Name = "高级",
                    IsOption = false
                }

            };

            //if (dbConnect != null)
            //{
            //    List<DbSystemSettingBonus> lskList = dbConnect.DbSystemSettingBonus.Where(b => EF.Functions.Like(b.Code, "lsk%")).ToList();
            //    for (int i = 1; i < levelList.Count; i++)
            //    {
            //        levelList[i].Amount = lskList.FirstOrDefault(b => b.Code.Equals("lsk" + i.ToString())).Value;
            //    }
            //}
            return levelList;
        }

        public void LevelUp(Dictionary<string, decimal> bonusDic, DbConnect dbConnect = null)
        {
            dbConnect = dbConnect ?? new DbConnect();

           
        }
    }
}
