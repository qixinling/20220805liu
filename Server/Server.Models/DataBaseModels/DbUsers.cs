using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbUsers
    {
        public DbUsers()
        {
            DbBill = new HashSet<DbBill>();
            DbShopOrder = new HashSet<DbShopOrder>();
            DbUsersAddress = new HashSet<DbUsersAddress>();
            DbUsersBank = new HashSet<DbUsersBank>();
            DbUsersFteam = new HashSet<DbUsersFteam>();
            DbUsersFwzxApply = new HashSet<DbUsersFwzxApply>();
            DbUsersJihuoRecordJidNavigation = new HashSet<DbUsersJihuoRecord>();
            DbUsersJihuoRecordUidNavigation = new HashSet<DbUsersJihuoRecord>();
            DbWallets = new HashSet<DbWallets>();
            DbWalletsChongzhi = new HashSet<DbWalletsChongzhi>();
            DbWalletsTixian = new HashSet<DbWalletsTixian>();
        }

        public int Id { get; set; }
        public string Userid { get; set; }
        public string Recode { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string Tx { get; set; }
        public string Username { get; set; }
        public string Usertel { get; set; }
        public string Usercode { get; set; }
        public DateTime Rdt { get; set; }
        public DateTime Pdt { get; set; }
        public int Ulevel { get; set; }
        public int Ylevel { get; set; }
        public int Xlevel { get; set; }
        public int Isbd { get; set; }
        public int Bdlevel { get; set; }
        public string Bdsheng { get; set; }
        public string Bdshi { get; set; }
        public string Bdxian { get; set; }
        public string Bdaddress { get; set; }
        public DateTime Bddate { get; set; }
        public decimal Lsk { get; set; }
        public decimal Ylsk { get; set; }
        public decimal Pv { get; set; }
        public int Dan { get; set; }
        public int Tdan { get; set; }
        public int Ispay { get; set; }
        public int Islock { get; set; }
        public int Reid { get; set; }
        public string Rename { get; set; }
        public string Reusername { get; set; }
        public string Repath { get; set; }
        public int Relevel { get; set; }
        public int Recount { get; set; }
        public int Teamcount { get; set; }
        public decimal Reyeji { get; set; }
        public decimal Teamyeji { get; set; }
        public decimal Riteamyeji { get; set; }
        public decimal Daquyeji { get; set; }
        public decimal Xiaoquyeji { get; set; }
        public int Bdid { get; set; }
        public string Bduserid { get; set; }
        public string Bdusername { get; set; }
        public int Iswx { get; set; }
        public string Wxtoken { get; set; }
        public string Wxnickname { get; set; }
        public string Wxheadimgurl { get; set; }
        public DateTime Msgdate { get; set; }
        public decimal Monthyeji { get; set; }
        public decimal Shouru { get; set; }
        public int Jid { get; set; }
        public string Juserid { get; set; }
        public string Jusername { get; set; }
        public int Zzxz { get; set; }
        public int Isrz { get; set; }
        public int Hyd { get; set; }
        public decimal Djjine { get; set; }
        public int Iswn { get; set; }
        public decimal Lastyjjine { get; set; }
        public decimal Yjjine { get; set; }
        public decimal Sjjine { get; set; }
        public decimal Djxyz { get; set; }
        public int Istq { get; set; }
        public string StudioName { get; set; }
        public string StudioCard { get; set; }
        public int Mystudioid { get; set; }
        public string Mystudioname { get; set; }

        public virtual ICollection<DbBill> DbBill { get; set; }
        public virtual ICollection<DbShopOrder> DbShopOrder { get; set; }
        public virtual ICollection<DbUsersAddress> DbUsersAddress { get; set; }
        public virtual ICollection<DbUsersBank> DbUsersBank { get; set; }
        public virtual ICollection<DbUsersFteam> DbUsersFteam { get; set; }
        public virtual ICollection<DbUsersFwzxApply> DbUsersFwzxApply { get; set; }
        public virtual ICollection<DbUsersJihuoRecord> DbUsersJihuoRecordJidNavigation { get; set; }
        public virtual ICollection<DbUsersJihuoRecord> DbUsersJihuoRecordUidNavigation { get; set; }
        public virtual ICollection<DbWallets> DbWallets { get; set; }
        public virtual ICollection<DbWalletsChongzhi> DbWalletsChongzhi { get; set; }
        public virtual ICollection<DbWalletsTixian> DbWalletsTixian { get; set; }
    }
}
