using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbConnect : DbContext
    {
        public DbConnect()
        {
        }

        public DbConnect(DbContextOptions<DbConnect> options)
            : base(options)
        {
        }

        public virtual DbSet<DbArticle> DbArticle { get; set; }
        public virtual DbSet<DbBill> DbBill { get; set; }
        public virtual DbSet<DbBillAmount> DbBillAmount { get; set; }
        public virtual DbSet<DbBonus> DbBonus { get; set; }
        public virtual DbSet<DbBonusJiesuan> DbBonusJiesuan { get; set; }
        public virtual DbSet<DbBonusSource> DbBonusSource { get; set; }
        public virtual DbSet<DbCheckcode> DbCheckcode { get; set; }
        public virtual DbSet<DbHelp> DbHelp { get; set; }
        public virtual DbSet<DbHold> DbHold { get; set; }
        public virtual DbSet<DbJewellery> DbJewellery { get; set; }
        public virtual DbSet<DbJichaDakuan> DbJichaDakuan { get; set; }
        public virtual DbSet<DbMsg> DbMsg { get; set; }
        public virtual DbSet<DbNews> DbNews { get; set; }
        public virtual DbSet<DbPricerange> DbPricerange { get; set; }
        public virtual DbSet<DbRenzheng> DbRenzheng { get; set; }
        public virtual DbSet<DbShopCollection> DbShopCollection { get; set; }
        public virtual DbSet<DbShopGoods> DbShopGoods { get; set; }
        public virtual DbSet<DbShopGoodsSort> DbShopGoodsSort { get; set; }
        public virtual DbSet<DbShopGoodsSortChild> DbShopGoodsSortChild { get; set; }
        public virtual DbSet<DbShopImg> DbShopImg { get; set; }
        public virtual DbSet<DbShopOrder> DbShopOrder { get; set; }
        public virtual DbSet<DbShopOrderChild> DbShopOrderChild { get; set; }
        public virtual DbSet<DbShoudan> DbShoudan { get; set; }
        public virtual DbSet<DbSite> DbSite { get; set; }
        public virtual DbSet<DbSlide> DbSlide { get; set; }
        public virtual DbSet<DbSystemAchievement> DbSystemAchievement { get; set; }
        public virtual DbSet<DbSystemAdmin> DbSystemAdmin { get; set; }
        public virtual DbSet<DbSystemAdminGroup> DbSystemAdminGroup { get; set; }
        public virtual DbSet<DbSystemLog> DbSystemLog { get; set; }
        public virtual DbSet<DbSystemSetting> DbSystemSetting { get; set; }
        public virtual DbSet<DbSystemSettingBonus> DbSystemSettingBonus { get; set; }
        public virtual DbSet<DbTeachers> DbTeachers { get; set; }
        public virtual DbSet<DbToken> DbToken { get; set; }
        public virtual DbSet<DbUsers> DbUsers { get; set; }
        public virtual DbSet<DbUsersAddress> DbUsersAddress { get; set; }
        public virtual DbSet<DbUsersBank> DbUsersBank { get; set; }
        public virtual DbSet<DbUsersDelete> DbUsersDelete { get; set; }
        public virtual DbSet<DbUsersFteam> DbUsersFteam { get; set; }
        public virtual DbSet<DbUsersFwzxApply> DbUsersFwzxApply { get; set; }
        public virtual DbSet<DbUsersJihuoRecord> DbUsersJihuoRecord { get; set; }
        public virtual DbSet<DbUsersLevelup> DbUsersLevelup { get; set; }
        public virtual DbSet<DbWallets> DbWallets { get; set; }
        public virtual DbSet<DbWalletsChongzhi> DbWalletsChongzhi { get; set; }
        public virtual DbSet<DbWalletsChongzhiSelect> DbWalletsChongzhiSelect { get; set; }
        public virtual DbSet<DbWalletsCoin> DbWalletsCoin { get; set; }
        public virtual DbSet<DbWalletsTixian> DbWalletsTixian { get; set; }
        public virtual DbSet<DbWalletsTixianSelect> DbWalletsTixianSelect { get; set; }
        public virtual DbSet<DbWalletsZengjian> DbWalletsZengjian { get; set; }
        public virtual DbSet<DbWalletsZhuanhuan> DbWalletsZhuanhuan { get; set; }
        public virtual DbSet<DbWalletsZhuanhuanSelect> DbWalletsZhuanhuanSelect { get; set; }
        public virtual DbSet<DbWalletsZhuanzhang> DbWalletsZhuanzhang { get; set; }
        public virtual DbSet<DbWalletsZhuanzhangSelect> DbWalletsZhuanzhangSelect { get; set; }
        public virtual DbSet<DbYuyue> DbYuyue { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=43.154.170.212;user id=20220805liu_gxyh;password=SXyhBNrER8FCwfzY;database=20220805liu_gxyh", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.39-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<DbArticle>(entity =>
            {
                entity.ToTable("db_article");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Articlecontent)
                    .IsRequired()
                    .HasColumnName("articlecontent");

                entity.Property(e => e.Articletitle)
                    .IsRequired()
                    .HasColumnName("articletitle");
            });

            modelBuilder.Entity<DbBill>(entity =>
            {
                entity.ToTable("db_bill");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bdate)
                    .HasColumnType("datetime")
                    .HasColumnName("bdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("时间");

                entity.Property(e => e.Blx)
                    .HasColumnType("int(11)")
                    .HasColumnName("blx")
                    .HasComment("操作类型");

                entity.Property(e => e.Bname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("bname")
                    .HasComment("操作名称");

                entity.Property(e => e.Bz)
                    .HasColumnType("text")
                    .HasColumnName("bz")
                    .HasComment("备注");

                entity.Property(e => e.Isdel)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdel")
                    .HasComment("是否删除  0:否  1:是");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasComment("状态,0待支付,1已支付,2退款");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbBill)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("db_bill_ibfk_1");
            });

            modelBuilder.Entity<DbBillAmount>(entity =>
            {
                entity.ToTable("db_bill_amount");

                entity.HasIndex(e => e.Bid, "bid");

                entity.HasIndex(e => e.Cid, "cid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2)
                    .HasColumnName("amount")
                    .HasComment("金额");

                entity.Property(e => e.Bid)
                    .HasColumnType("int(11)")
                    .HasColumnName("bid")
                    .HasComment("账单id");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("币种id");

                entity.Property(e => e.Cname)
                    .HasMaxLength(100)
                    .HasColumnName("cname");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasComment("预留状态0无事发生");

                entity.HasOne(d => d.BidNavigation)
                    .WithMany(p => p.DbBillAmount)
                    .HasForeignKey(d => d.Bid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("db_bill_amount_ibfk_1");

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.DbBillAmount)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("db_bill_amount_ibfk_2");
            });

            modelBuilder.Entity<DbBonus>(entity =>
            {
                entity.ToTable("db_bonus");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Btdate)
                    .HasColumnType("datetime")
                    .HasColumnName("btdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<DbBonusJiesuan>(entity =>
            {
                entity.ToTable("db_bonus_jiesuan");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Jdate)
                    .HasColumnType("datetime")
                    .HasColumnName("jdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.State)
                    .HasColumnType("int(2)")
                    .HasColumnName("state")
                    .HasComment("0-正在结算,1-结算成功,2-结算失败");

                entity.Property(e => e.Userid)
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Wdate)
                    .HasColumnType("datetime")
                    .HasColumnName("wdate");
            });

            modelBuilder.Entity<DbBonusSource>(entity =>
            {
                entity.ToTable("db_bonus_source");

                entity.HasIndex(e => e.Btid, "btid");

                entity.HasIndex(e => e.State, "state");

                entity.HasIndex(e => e.Uid, "uid");

                entity.HasIndex(e => e.Yid, "yid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Btid)
                    .HasColumnType("int(11)")
                    .HasColumnName("btid");

                entity.Property(e => e.Bz)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("bz");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasDefaultValueSql("'1'")
                    .HasComment("币种id,通常为1=奖金");

                entity.Property(e => e.Edate)
                    .HasColumnType("datetime")
                    .HasColumnName("edate")
                    .HasComment("结算日期");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine")
                    .HasComment("金额");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx")
                    .HasComment("获奖类型");

                entity.Property(e => e.Sdate)
                    .HasColumnType("datetime")
                    .HasColumnName("sdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("写入日期");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasComment("类型0-未结算,1已结算");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid")
                    .HasComment("获奖uid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(50)
                    .HasColumnName("userid")
                    .HasComment("获奖userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Yid)
                    .HasColumnType("int(11)")
                    .HasColumnName("yid")
                    .HasComment("来源uid");

                entity.Property(e => e.Yuserid)
                    .HasMaxLength(50)
                    .HasColumnName("yuserid")
                    .HasComment("来源userid");

                entity.Property(e => e.Yusername)
                    .HasMaxLength(50)
                    .HasColumnName("yusername")
                    .HasComment("来源username");

                entity.HasOne(d => d.Bt)
                    .WithMany(p => p.DbBonusSource)
                    .HasForeignKey(d => d.Btid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("db_bonus_source_ibfk_1");
            });

            modelBuilder.Entity<DbCheckcode>(entity =>
            {
                entity.ToTable("db_checkcode");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("cdate");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasColumnName("code");

                entity.Property(e => e.Usertel)
                    .HasMaxLength(50)
                    .HasColumnName("usertel");
            });

            modelBuilder.Entity<DbHelp>(entity =>
            {
                entity.ToTable("db_help");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Answer).HasColumnName("answer");

                entity.Property(e => e.Cai)
                    .HasColumnType("int(11)")
                    .HasColumnName("cai")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Gpath)
                    .HasColumnName("gpath")
                    .HasComment("记录别的问题里hpath有此问题的id");

                entity.Property(e => e.Hlevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("hlevel")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Hpath)
                    .HasColumnName("hpath")
                    .HasComment("点击时出现的问题");

                entity.Property(e => e.Question)
                    .HasMaxLength(100)
                    .HasColumnName("question");

                entity.Property(e => e.Rank)
                    .HasColumnType("int(11)")
                    .HasColumnName("rank")
                    .HasComment("排序");

                entity.Property(e => e.Show)
                    .HasColumnType("int(11)")
                    .HasColumnName("show")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Zan)
                    .HasColumnType("int(11)")
                    .HasColumnName("zan")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<DbHold>(entity =>
            {
                entity.ToTable("db_hold");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Buid)
                    .HasColumnType("int(11)")
                    .HasColumnName("buid");

                entity.Property(e => e.Buserid)
                    .HasMaxLength(255)
                    .HasColumnName("buserid");

                entity.Property(e => e.Busername)
                    .HasMaxLength(255)
                    .HasColumnName("busername");

                entity.Property(e => e.Busertel)
                    .HasMaxLength(255)
                    .HasColumnName("busertel");

                entity.Property(e => e.Buyissu)
                    .HasColumnType("int(11)")
                    .HasColumnName("buyissu")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Dkdate)
                    .HasColumnType("datetime")
                    .HasColumnName("dkdate")
                    .HasComment("打款时间");

                entity.Property(e => e.Edate)
                    .HasColumnType("datetime")
                    .HasColumnName("edate");

                entity.Property(e => e.Hbjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("hbjine");

                entity.Property(e => e.Hdate)
                    .HasColumnType("datetime")
                    .HasColumnName("hdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.Holdno)
                    .HasMaxLength(255)
                    .HasColumnName("holdno");

                entity.Property(e => e.Hsuid)
                    .HasColumnType("int(11)")
                    .HasColumnName("hsuid");

                entity.Property(e => e.Iscf)
                    .HasColumnType("int(11)")
                    .HasColumnName("iscf");

                entity.Property(e => e.Isdelete)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdelete");

                entity.Property(e => e.Isfc)
                    .HasColumnType("int(11)")
                    .HasColumnName("isfc");

                entity.Property(e => e.Issd)
                    .HasColumnType("int(11)")
                    .HasColumnName("issd");

                entity.Property(e => e.Issj)
                    .HasColumnType("int(11)")
                    .HasColumnName("issj")
                    .HasComment("是否上架");

                entity.Property(e => e.Isyy)
                    .HasColumnType("int(11)")
                    .HasColumnName("isyy");

                entity.Property(e => e.Jid)
                    .HasColumnType("int(11)")
                    .HasColumnName("jid");

                entity.Property(e => e.Jimg)
                    .HasMaxLength(255)
                    .HasColumnName("jimg");

                entity.Property(e => e.Jname)
                    .HasMaxLength(255)
                    .HasColumnName("jname");

                entity.Property(e => e.Jprice)
                    .HasPrecision(10, 2)
                    .HasColumnName("jprice")
                    .HasComment("竞拍价格");

                entity.Property(e => e.Jsjbili)
                    .HasPrecision(18, 2)
                    .HasColumnName("jsjbili");

                entity.Property(e => e.Jsybili)
                    .HasPrecision(18, 2)
                    .HasColumnName("jsybili");

                entity.Property(e => e.Oldhsuid)
                    .HasColumnType("int(11)")
                    .HasColumnName("oldhsuid");

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .HasColumnName("path");

                entity.Property(e => e.Pid)
                    .HasColumnType("int(11)")
                    .HasColumnName("pid");

                entity.Property(e => e.Qgdate)
                    .HasColumnType("datetime")
                    .HasColumnName("qgdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("用于判断抢购");

                entity.Property(e => e.Repath)
                    .HasMaxLength(255)
                    .HasColumnName("repath");

                entity.Property(e => e.Rishouyi)
                    .HasPrecision(10, 2)
                    .HasColumnName("rishouyi")
                    .HasComment("当前收益");

                entity.Property(e => e.Scdate)
                    .HasColumnType("datetime")
                    .HasColumnName("scdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("用于查询捡漏区");

                entity.Property(e => e.Sellissu)
                    .HasColumnType("int(11)")
                    .HasColumnName("sellissu")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sjdate)
                    .HasColumnType("datetime")
                    .HasColumnName("sjdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Sjimg)
                    .HasMaxLength(255)
                    .HasColumnName("sjimg");

                entity.Property(e => e.Sjjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("sjjine");

                entity.Property(e => e.Skdate)
                    .HasColumnType("datetime")
                    .HasColumnName("skdate")
                    .HasComment("收款时间");

                entity.Property(e => e.Ssdate)
                    .HasColumnType("datetime")
                    .HasColumnName("ssdate")
                    .HasComment("申述时间");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasComment("1买家锁定交易，2买家完成打款，3卖家收款，4结束交易");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(255)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.Property(e => e.Usertel)
                    .HasMaxLength(255)
                    .HasColumnName("usertel");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("version")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Yajin)
                    .HasPrecision(18, 2)
                    .HasColumnName("yajin");

                entity.Property(e => e.Zhimg)
                    .HasMaxLength(255)
                    .HasColumnName("zhimg");

                entity.Property(e => e.Zrdate)
                    .HasColumnType("datetime")
                    .HasColumnName("zrdate")
                    .HasComment("抢购时间");

                entity.Property(e => e.Zsdate)
                    .HasColumnType("datetime")
                    .HasColumnName("zsdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("用于判断转售");

                entity.Property(e => e.Zshouyi)
                    .HasPrecision(10, 2)
                    .HasColumnName("zshouyi")
                    .HasComment("累计总收益");

                entity.Property(e => e.Zskdate)
                    .HasColumnType("datetime")
                    .HasColumnName("zskdate");

                entity.Property(e => e.Zuid)
                    .HasColumnType("int(11)")
                    .HasColumnName("zuid")
                    .HasComment("指定抢单人uid");

                entity.Property(e => e.Zuserid)
                    .HasMaxLength(255)
                    .HasColumnName("zuserid")
                    .HasComment("指定抢单人userid");
            });

            modelBuilder.Entity<DbJewellery>(entity =>
            {
                entity.ToTable("db_jewellery");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Img)
                    .HasMaxLength(255)
                    .HasColumnName("img");

                entity.Property(e => e.Maxprice)
                    .HasPrecision(18, 2)
                    .HasColumnName("maxprice")
                    .HasComment("最高价格");

                entity.Property(e => e.Minprice)
                    .HasPrecision(18, 2)
                    .HasColumnName("minprice")
                    .HasComment("最低价格");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("名称");

                entity.Property(e => e.Pjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("pjine")
                    .HasComment("竞拍价格");

                entity.Property(e => e.Prompt)
                    .HasMaxLength(255)
                    .HasColumnName("prompt")
                    .HasComment("简介");

                entity.Property(e => e.Sjbili)
                    .HasPrecision(18, 2)
                    .HasColumnName("sjbili")
                    .HasComment("上架比例");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasDefaultValueSql("'1'")
                    .HasComment("0下架1上架");

                entity.Property(e => e.Sybili)
                    .HasPrecision(18, 2)
                    .HasColumnName("sybili")
                    .HasComment("收益比例");
            });

            modelBuilder.Entity<DbJichaDakuan>(entity =>
            {
                entity.ToTable("db_jicha_dakuan");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("cdate")
                    .HasComment("创建时间");

                entity.Property(e => e.Ddate)
                    .HasColumnType("datetime")
                    .HasColumnName("ddate")
                    .HasComment("打款时间");

                entity.Property(e => e.Dkimg)
                    .HasMaxLength(255)
                    .HasColumnName("dkimg")
                    .HasComment("打款图片");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine")
                    .HasComment("打款金额");

                entity.Property(e => e.Liushuihao)
                    .HasMaxLength(255)
                    .HasColumnName("liushuihao")
                    .HasComment("流水号");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasComment("0=等待打款,1=已打款,2=预留(可能会出现需要申诉");

                entity.Property(e => e.Suid)
                    .HasColumnType("int(11)")
                    .HasColumnName("suid")
                    .HasComment("收款人");

                entity.Property(e => e.Suserid)
                    .HasMaxLength(255)
                    .HasColumnName("suserid");

                entity.Property(e => e.Susername)
                    .HasMaxLength(255)
                    .HasColumnName("susername");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid")
                    .HasComment("打款人");

                entity.Property(e => e.Userid)
                    .HasMaxLength(255)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<DbMsg>(entity =>
            {
                entity.ToTable("db_msg");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Fid)
                    .HasColumnType("int(11)")
                    .HasColumnName("fid")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Fuserid)
                    .HasMaxLength(50)
                    .HasColumnName("fuserid");

                entity.Property(e => e.Isread)
                    .HasColumnType("int(11)")
                    .HasColumnName("isread")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Mdate)
                    .HasColumnType("datetime")
                    .HasColumnName("mdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Msgcontent).HasColumnName("msgcontent");

                entity.Property(e => e.Sid)
                    .HasColumnType("int(11)")
                    .HasColumnName("sid")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sisdelete)
                    .HasColumnType("int(11)")
                    .HasColumnName("sisdelete")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Suserid)
                    .HasMaxLength(50)
                    .HasColumnName("suserid");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<DbNews>(entity =>
            {
                entity.ToTable("db_news");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Clicks)
                    .HasColumnType("int(11)")
                    .HasColumnName("clicks")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Display)
                    .HasColumnType("int(11)")
                    .HasColumnName("display")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Newscontent).HasColumnName("newscontent");

                entity.Property(e => e.Newscover)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("newscover")
                    .HasComment("封面图");

                entity.Property(e => e.Newsoperator)
                    .HasMaxLength(50)
                    .HasColumnName("newsoperator");

                entity.Property(e => e.Newstime)
                    .HasColumnType("datetime")
                    .HasColumnName("newstime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Newstitle)
                    .HasMaxLength(255)
                    .HasColumnName("newstitle");
            });

            modelBuilder.Entity<DbPricerange>(entity =>
            {
                entity.ToTable("db_pricerange");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Maxprice)
                    .HasPrecision(18, 2)
                    .HasColumnName("maxprice");

                entity.Property(e => e.Minnum)
                    .HasPrecision(18, 2)
                    .HasColumnName("minnum")
                    .HasComment("最低信用值");

                entity.Property(e => e.Minprice)
                    .HasPrecision(18, 2)
                    .HasColumnName("minprice");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DbRenzheng>(entity =>
            {
                entity.ToTable("db_renzheng");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Fimg)
                    .IsRequired()
                    .HasColumnName("fimg")
                    .HasComment("身份证反面照");

                entity.Property(e => e.Sdate)
                    .HasColumnType("datetime")
                    .HasColumnName("sdate")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'")
                    .HasComment("申请时间");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasComment("状态： 0:未审核 1:已审核 2:审核不通过");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Usercode)
                    .HasMaxLength(255)
                    .HasColumnName("usercode");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Zimg)
                    .IsRequired()
                    .HasColumnName("zimg")
                    .HasComment("身份证正面照");
            });

            modelBuilder.Entity<DbShopCollection>(entity =>
            {
                entity.ToTable("db_shop_collection");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Spath)
                    .IsRequired()
                    .HasColumnName("spath");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");
            });

            modelBuilder.Entity<DbShopGoods>(entity =>
            {
                entity.ToTable("db_shop_goods");

                entity.HasIndex(e => e.Xlid, "xlid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bili)
                    .HasMaxLength(255)
                    .HasColumnName("bili");

                entity.Property(e => e.Cost)
                    .HasPrecision(18, 2)
                    .HasColumnName("cost")
                    .HasComment("运费");

                entity.Property(e => e.Dlid)
                    .HasColumnType("int(11)")
                    .HasColumnName("dlid");

                entity.Property(e => e.Dlname)
                    .HasMaxLength(100)
                    .HasColumnName("dlname");

                entity.Property(e => e.GoodsNo)
                    .HasMaxLength(50)
                    .HasColumnName("goodsNo");

                entity.Property(e => e.Goodscontent).HasColumnName("goodscontent");

                entity.Property(e => e.Goodsdate)
                    .HasColumnType("datetime")
                    .HasColumnName("goodsdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Goodsimg).HasColumnName("goodsimg");

                entity.Property(e => e.Goodslabel).HasColumnName("goodslabel");

                entity.Property(e => e.Goodsname)
                    .HasMaxLength(255)
                    .HasColumnName("goodsname");

                entity.Property(e => e.Goodsprice)
                    .HasPrecision(18, 2)
                    .HasColumnName("goodsprice");

                entity.Property(e => e.Goodspv)
                    .HasPrecision(18, 2)
                    .HasColumnName("goodspv");

                entity.Property(e => e.Goodstype)
                    .HasColumnType("int(11)")
                    .HasColumnName("goodstype");

                entity.Property(e => e.Ishome)
                    .HasColumnType("int(10)")
                    .HasColumnName("ishome")
                    .HasComment("是否首页展示");

                entity.Property(e => e.Ispay)
                    .HasColumnType("int(11)")
                    .HasColumnName("ispay");

                entity.Property(e => e.Prompt)
                    .HasMaxLength(100)
                    .HasColumnName("prompt");

                entity.Property(e => e.Sales)
                    .HasColumnType("int(11)")
                    .HasColumnName("sales")
                    .HasComment("销量");

                entity.Property(e => e.Sort)
                    .HasColumnType("int(11)")
                    .HasColumnName("sort")
                    .HasComment("排序");

                entity.Property(e => e.Stock)
                    .HasColumnType("int(11)")
                    .HasColumnName("stock")
                    .HasComment("库存");

                entity.Property(e => e.Tj1)
                    .HasPrecision(10)
                    .HasColumnName("tj1");

                entity.Property(e => e.Tj2)
                    .HasPrecision(10)
                    .HasColumnName("tj2");

                entity.Property(e => e.Tj3)
                    .HasPrecision(10)
                    .HasColumnName("tj3");

                entity.Property(e => e.Tj4)
                    .HasPrecision(10)
                    .HasColumnName("tj4");

                entity.Property(e => e.Tj5)
                    .HasPrecision(10)
                    .HasColumnName("tj5");

                entity.Property(e => e.Xlid)
                    .HasColumnType("int(11)")
                    .HasColumnName("xlid");

                entity.Property(e => e.Xlname)
                    .HasMaxLength(100)
                    .HasColumnName("xlname");

                entity.Property(e => e.Xyzjine)
                    .HasPrecision(10, 2)
                    .HasColumnName("xyzjine")
                    .HasComment("信用值金额");
            });

            modelBuilder.Entity<DbShopGoodsSort>(entity =>
            {
                entity.ToTable("db_shop_goods_sort");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Adate)
                    .HasColumnType("datetime")
                    .HasColumnName("adate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Daleiimg).HasColumnName("daleiimg");

                entity.Property(e => e.Daleiname)
                    .HasMaxLength(50)
                    .HasColumnName("daleiname");

                entity.Property(e => e.Daleiorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("daleiorder")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pagemark)
                    .HasColumnType("int(11)")
                    .HasColumnName("pagemark")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pagemarklx)
                    .HasColumnType("int(11)")
                    .HasColumnName("pagemarklx")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Putaway)
                    .HasColumnType("int(11)")
                    .HasColumnName("putaway")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<DbShopGoodsSortChild>(entity =>
            {
                entity.ToTable("db_shop_goods_sort_child");

                entity.HasIndex(e => e.Sid, "sid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Adate)
                    .HasColumnType("datetime")
                    .HasColumnName("adate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Pagemark)
                    .HasColumnType("int(11)")
                    .HasColumnName("pagemark");

                entity.Property(e => e.Putaway)
                    .HasColumnType("int(11)")
                    .HasColumnName("putaway")
                    .HasComment("上下架");

                entity.Property(e => e.Sid)
                    .HasColumnType("int(11)")
                    .HasColumnName("sid")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Xiaoleiimg).HasColumnName("xiaoleiimg");

                entity.Property(e => e.Xiaoleiname)
                    .HasMaxLength(50)
                    .HasColumnName("xiaoleiname");

                entity.Property(e => e.Xiaoleiorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("xiaoleiorder");

                entity.HasOne(d => d.SidNavigation)
                    .WithMany(p => p.DbShopGoodsSortChild)
                    .HasForeignKey(d => d.Sid)
                    .HasConstraintName("db_shop_goods_sort_child_ibfk_1");
            });

            modelBuilder.Entity<DbShopImg>(entity =>
            {
                entity.ToTable("db_shop_img");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bdlx)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("bdlx");

                entity.Property(e => e.Gid)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("gid");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasColumnName("img");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx")
                    .HasDefaultValueSql("'1'")
                    .HasComment("展示类型: 1单张.2三张");
            });

            modelBuilder.Entity<DbShopOrder>(entity =>
            {
                entity.ToTable("db_shop_order");

                entity.HasIndex(e => e.BillId, "bill_id");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Beizhu)
                    .HasMaxLength(50)
                    .HasColumnName("beizhu");

                entity.Property(e => e.BillId)
                    .HasColumnType("int(11)")
                    .HasColumnName("bill_id");

                entity.Property(e => e.Cost)
                    .HasPrecision(18, 2)
                    .HasColumnName("cost");

                entity.Property(e => e.Fdate)
                    .HasColumnType("datetime")
                    .HasColumnName("fdate");

                entity.Property(e => e.Goodslist).HasColumnName("goodslist");

                entity.Property(e => e.Goodsnum)
                    .HasColumnType("int(11)")
                    .HasColumnName("goodsnum")
                    .HasComment("商品总数");

                entity.Property(e => e.Isdelete)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdelete");

                entity.Property(e => e.KuaidiNo)
                    .HasMaxLength(50)
                    .HasColumnName("kuaidiNo");

                entity.Property(e => e.Kuaidiname)
                    .HasMaxLength(50)
                    .HasColumnName("kuaidiname");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx")
                    .HasComment("1商城订单2交割订单3激活订单");

                entity.Property(e => e.Odate)
                    .HasColumnType("datetime")
                    .HasColumnName("odate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.OrderNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("orderNo");

                entity.Property(e => e.Orderstate)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderstate");

                entity.Property(e => e.Pv)
                    .HasPrecision(18, 2)
                    .HasColumnName("pv");

                entity.Property(e => e.Sheng)
                    .HasMaxLength(50)
                    .HasColumnName("sheng");

                entity.Property(e => e.Shi)
                    .HasMaxLength(50)
                    .HasColumnName("shi");

                entity.Property(e => e.Sjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("sjine")
                    .HasComment("实价");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Useraddress)
                    .HasMaxLength(255)
                    .HasColumnName("useraddress");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Usertel)
                    .HasMaxLength(50)
                    .HasColumnName("usertel");

                entity.Property(e => e.Xian)
                    .HasMaxLength(50)
                    .HasColumnName("xian");

                entity.Property(e => e.Yjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("yjine")
                    .HasComment("原价");

                entity.Property(e => e.Zsjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("zsjine")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("交割单赠送信用值");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.DbShopOrder)
                    .HasForeignKey(d => d.BillId)
                    .HasConstraintName("db_shop_order_ibfk_2");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbShopOrder)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_shop_order_ibfk_1");
            });

            modelBuilder.Entity<DbShopOrderChild>(entity =>
            {
                entity.ToTable("db_shop_order_child");

                entity.HasIndex(e => e.Oid, "oid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("cdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Gid)
                    .HasColumnType("int(11)")
                    .HasColumnName("gid");

                entity.Property(e => e.Goodsimg).HasColumnName("goodsimg");

                entity.Property(e => e.Goodsname)
                    .HasMaxLength(255)
                    .HasColumnName("goodsname");

                entity.Property(e => e.Num)
                    .HasColumnType("int(11)")
                    .HasColumnName("num");

                entity.Property(e => e.Oid)
                    .HasColumnType("int(11)")
                    .HasColumnName("oid");

                entity.Property(e => e.OrderNo)
                    .HasMaxLength(100)
                    .HasColumnName("orderNo");

                entity.Property(e => e.Sjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("sjine");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Yjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("yjine");

                entity.Property(e => e.Zjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("zjine");

                entity.HasOne(d => d.OidNavigation)
                    .WithMany(p => p.DbShopOrderChild)
                    .HasForeignKey(d => d.Oid)
                    .HasConstraintName("db_shop_order_child_ibfk_1");
            });

            modelBuilder.Entity<DbShoudan>(entity =>
            {
                entity.ToTable("db_shoudan");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("cdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Jid)
                    .HasColumnType("int(11)")
                    .HasColumnName("jid");

                entity.Property(e => e.Jname)
                    .HasMaxLength(255)
                    .HasColumnName("jname");

                entity.Property(e => e.Suserid)
                    .HasMaxLength(255)
                    .HasColumnName("suserid");

                entity.Property(e => e.Znum)
                    .HasColumnType("int(11)")
                    .HasColumnName("znum");

                entity.Property(e => e.Zprice)
                    .HasPrecision(18, 2)
                    .HasColumnName("zprice");

                entity.Property(e => e.Zyijia)
                    .HasPrecision(18, 2)
                    .HasColumnName("zyijia");
            });

            modelBuilder.Entity<DbSite>(entity =>
            {
                entity.ToTable("db_site");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Edate)
                    .HasColumnType("time")
                    .HasColumnName("edate")
                    .HasComment("结束时间");

                entity.Property(e => e.Iskf)
                    .HasColumnType("int(11)")
                    .HasColumnName("iskf")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ispay)
                    .HasColumnType("int(11)")
                    .HasColumnName("ispay")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0未开放1开放中");

                entity.Property(e => e.Isyy)
                    .HasColumnType("int(11)")
                    .HasColumnName("isyy")
                    .HasDefaultValueSql("'0'")
                    .HasComment("1开启预约功能");

                entity.Property(e => e.Maxnum)
                    .HasColumnType("int(11)")
                    .HasColumnName("maxnum")
                    .HasDefaultValueSql("'0'")
                    .HasComment("最大抢购数");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Prompt)
                    .HasMaxLength(255)
                    .HasColumnName("prompt");

                entity.Property(e => e.Sdate)
                    .HasColumnType("time")
                    .HasColumnName("sdate")
                    .HasComment("开始时间");
            });

            modelBuilder.Entity<DbSlide>(entity =>
            {
                entity.ToTable("db_slide");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Gid)
                    .HasColumnType("int(11)")
                    .HasColumnName("gid");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasColumnName("img");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx");

                entity.Property(e => e.Pagelx)
                    .HasColumnType("int(11)")
                    .HasColumnName("pagelx");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url");
            });

            modelBuilder.Entity<DbSystemAchievement>(entity =>
            {
                entity.ToTable("db_system_achievement");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Adate)
                    .HasColumnType("datetime")
                    .HasColumnName("adate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Bonus)
                    .HasPrecision(18, 2)
                    .HasColumnName("bonus");

                entity.Property(e => e.Chongzhijine)
                    .HasPrecision(18, 2)
                    .HasColumnName("chongzhijine");

                entity.Property(e => e.Gouwucount)
                    .HasColumnType("int(11)")
                    .HasColumnName("gouwucount");

                entity.Property(e => e.Gouwujine)
                    .HasPrecision(18, 2)
                    .HasColumnName("gouwujine");

                entity.Property(e => e.Ordercount)
                    .HasColumnType("int(11)")
                    .HasColumnName("ordercount");

                entity.Property(e => e.Shangjiajine)
                    .HasPrecision(18, 2)
                    .HasColumnName("shangjiajine")
                    .HasComment("全网已消耗的上架费");

                entity.Property(e => e.Tixianjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("tixianjine");

                entity.Property(e => e.Userscount)
                    .HasColumnType("int(11)")
                    .HasColumnName("userscount");

                entity.Property(e => e.Usersjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("usersjine");

                entity.Property(e => e.Xyzjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("xyzjine");

                entity.Property(e => e.Zhuanjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("zhuanjine")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("全网消费券转换为信用值的总额；");
            });

            modelBuilder.Entity<DbSystemAdmin>(entity =>
            {
                entity.ToTable("db_system_admin");

                entity.HasIndex(e => e.Gid, "system_admin_ibfk_1");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Adate)
                    .HasColumnType("datetime")
                    .HasColumnName("adate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Gid)
                    .HasColumnType("int(11)")
                    .HasColumnName("gid");

                entity.Property(e => e.Loginip)
                    .HasMaxLength(50)
                    .HasColumnName("loginip");

                entity.Property(e => e.Logintime)
                    .HasMaxLength(50)
                    .HasColumnName("logintime");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Permissionname)
                    .HasMaxLength(50)
                    .HasColumnName("permissionname");

                entity.Property(e => e.Userid)
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.GidNavigation)
                    .WithMany(p => p.DbSystemAdmin)
                    .HasForeignKey(d => d.Gid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("db_system_admin_ibfk_1");
            });

            modelBuilder.Entity<DbSystemAdminGroup>(entity =>
            {
                entity.ToTable("db_system_admin_group");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Groupname)
                    .HasMaxLength(50)
                    .HasColumnName("groupname");

                entity.Property(e => e.Permission).HasColumnName("permission");
            });

            modelBuilder.Entity<DbSystemLog>(entity =>
            {
                entity.ToTable("db_system_log");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bz)
                    .HasColumnName("bz")
                    .HasComment("备注");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("ip")
                    .HasComment("操作人IP地址");

                entity.Property(e => e.IsDel)
                    .HasColumnType("int(11)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除");

                entity.Property(e => e.Ldate)
                    .HasColumnType("datetime")
                    .HasColumnName("ldate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("操作日期");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx")
                    .HasComment("操作类型");

                entity.Property(e => e.LxName)
                    .HasMaxLength(255)
                    .HasColumnName("lx_name")
                    .HasComment("操作类型名称");

                entity.Property(e => e.NewData)
                    .HasColumnName("new_data")
                    .HasComment("新数据json");

                entity.Property(e => e.OldData)
                    .HasColumnName("old_data")
                    .HasComment("旧数据json");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid")
                    .HasComment("关联的会员uid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(50)
                    .HasColumnName("userid")
                    .HasComment("管理员userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username")
                    .HasComment("管理员姓名");
            });

            modelBuilder.Entity<DbSystemSetting>(entity =>
            {
                entity.ToTable("db_system_setting");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Bank)
                    .IsRequired()
                    .HasColumnName("bank");

                entity.Property(e => e.Czbs)
                    .HasColumnType("int(11)")
                    .HasColumnName("czbs");

                entity.Property(e => e.Czmax)
                    .HasPrecision(18, 2)
                    .HasColumnName("czmax");

                entity.Property(e => e.Czmin)
                    .HasPrecision(18, 2)
                    .HasColumnName("czmin");

                entity.Property(e => e.Marqueemsg)
                    .IsRequired()
                    .HasColumnName("marqueemsg");

                entity.Property(e => e.Switchchongzhi)
                    .HasColumnType("int(11)")
                    .HasColumnName("switchchongzhi");

                entity.Property(e => e.Switchsystem)
                    .HasColumnType("int(11)")
                    .HasColumnName("switchsystem");

                entity.Property(e => e.Switchtixian)
                    .HasColumnType("int(11)")
                    .HasColumnName("switchtixian");

                entity.Property(e => e.Switchtjt)
                    .HasColumnType("int(11)")
                    .HasColumnName("switchtjt");

                entity.Property(e => e.Switchwlt)
                    .HasColumnType("int(11)")
                    .HasColumnName("switchwlt");

                entity.Property(e => e.Switchzhuanhuan)
                    .HasColumnType("int(11)")
                    .HasColumnName("switchzhuanhuan");

                entity.Property(e => e.Switchzhuanzhang)
                    .HasColumnType("int(11)")
                    .HasColumnName("switchzhuanzhang");

                entity.Property(e => e.Systemclosemsg)
                    .IsRequired()
                    .HasColumnName("systemclosemsg");

                entity.Property(e => e.Timeclosemsg)
                    .IsRequired()
                    .HasColumnName("timeclosemsg");

                entity.Property(e => e.Timeend)
                    .HasColumnType("int(11)")
                    .HasColumnName("timeend");

                entity.Property(e => e.Timestart)
                    .HasColumnType("int(11)")
                    .HasColumnName("timestart");

                entity.Property(e => e.Txbs)
                    .HasColumnType("int(11)")
                    .HasColumnName("txbs");

                entity.Property(e => e.Txmax)
                    .HasPrecision(18, 2)
                    .HasColumnName("txmax");

                entity.Property(e => e.Txmin)
                    .HasPrecision(18, 2)
                    .HasColumnName("txmin");

                entity.Property(e => e.Txsl)
                    .HasPrecision(18, 2)
                    .HasColumnName("txsl");

                entity.Property(e => e.Zhbs)
                    .HasColumnType("int(11)")
                    .HasColumnName("zhbs");

                entity.Property(e => e.Zzbs)
                    .HasColumnType("int(11)")
                    .HasColumnName("zzbs");

                entity.Property(e => e.Zzmax)
                    .HasPrecision(18, 2)
                    .HasColumnName("zzmax");

                entity.Property(e => e.Zzmin)
                    .HasPrecision(18, 2)
                    .HasColumnName("zzmin");
            });

            modelBuilder.Entity<DbSystemSettingBonus>(entity =>
            {
                entity.ToTable("db_system_setting_bonus");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bz)
                    .HasMaxLength(255)
                    .HasColumnName("bz");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("code")
                    .HasComment("代号");

                entity.Property(e => e.Index)
                    .HasColumnType("int(11)")
                    .HasColumnName("index")
                    .HasComment("排序");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("名称");

                entity.Property(e => e.Value)
                    .HasPrecision(18, 2)
                    .HasColumnName("value")
                    .HasComment("参数");
            });

            modelBuilder.Entity<DbTeachers>(entity =>
            {
                entity.ToTable("db_teachers");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("cdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Introduce)
                    .HasColumnType("text")
                    .HasColumnName("introduce")
                    .HasComment("介绍");

                entity.Property(e => e.Ispay)
                    .HasColumnType("int(11)")
                    .HasColumnName("ispay")
                    .HasDefaultValueSql("'1'")
                    .HasComment("0不展示1展示");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("姓名");

                entity.Property(e => e.Tximg)
                    .HasMaxLength(255)
                    .HasColumnName("tximg");
            });

            modelBuilder.Entity<DbToken>(entity =>
            {
                entity.ToTable("db_token");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("ip");

                entity.Property(e => e.Isa)
                    .HasColumnType("int(11)")
                    .HasColumnName("isa");

                entity.Property(e => e.Odate)
                    .HasColumnType("datetime")
                    .HasColumnName("odate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Os).HasColumnName("os");

                entity.Property(e => e.Tokenstr)
                    .IsRequired()
                    .HasColumnName("tokenstr");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<DbUsers>(entity =>
            {
                entity.ToTable("db_users");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bdaddress).HasColumnName("bdaddress");

                entity.Property(e => e.Bddate)
                    .HasColumnType("datetime")
                    .HasColumnName("bddate")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.Bdid)
                    .HasColumnType("int(11)")
                    .HasColumnName("bdid");

                entity.Property(e => e.Bdlevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("bdlevel");

                entity.Property(e => e.Bdsheng)
                    .HasMaxLength(50)
                    .HasColumnName("bdsheng");

                entity.Property(e => e.Bdshi)
                    .HasMaxLength(50)
                    .HasColumnName("bdshi");

                entity.Property(e => e.Bduserid)
                    .HasMaxLength(50)
                    .HasColumnName("bduserid");

                entity.Property(e => e.Bdusername)
                    .HasMaxLength(50)
                    .HasColumnName("bdusername");

                entity.Property(e => e.Bdxian)
                    .HasMaxLength(50)
                    .HasColumnName("bdxian");

                entity.Property(e => e.Dan)
                    .HasColumnType("int(11)")
                    .HasColumnName("dan");

                entity.Property(e => e.Daquyeji)
                    .HasPrecision(18, 2)
                    .HasColumnName("daquyeji");

                entity.Property(e => e.Djjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("djjine")
                    .HasComment("动静态累计金额，5000封顶");

                entity.Property(e => e.Djxyz)
                    .HasPrecision(18, 2)
                    .HasColumnName("djxyz")
                    .HasComment("冻结信用值");

                entity.Property(e => e.Hyd)
                    .HasColumnType("int(11)")
                    .HasColumnName("hyd");

                entity.Property(e => e.Isbd)
                    .HasColumnType("int(11)")
                    .HasColumnName("isbd");

                entity.Property(e => e.Islock)
                    .HasColumnType("int(11)")
                    .HasColumnName("islock");

                entity.Property(e => e.Ispay)
                    .HasColumnType("int(11)")
                    .HasColumnName("ispay");

                entity.Property(e => e.Isrz)
                    .HasColumnType("int(11)")
                    .HasColumnName("isrz");

                entity.Property(e => e.Istq)
                    .HasColumnType("int(11)")
                    .HasColumnName("istq");

                entity.Property(e => e.Iswn)
                    .HasColumnType("int(11)")
                    .HasColumnName("iswn")
                    .HasComment("是否万能账户");

                entity.Property(e => e.Iswx)
                    .HasColumnType("int(11)")
                    .HasColumnName("iswx");

                entity.Property(e => e.Jid)
                    .HasColumnType("int(11)")
                    .HasColumnName("jid");

                entity.Property(e => e.Juserid)
                    .HasMaxLength(50)
                    .HasColumnName("juserid");

                entity.Property(e => e.Jusername)
                    .HasMaxLength(50)
                    .HasColumnName("jusername");

                entity.Property(e => e.Lastyjjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("lastyjjine")
                    .HasComment("上次结算的金额,记录用于防止扯皮");

                entity.Property(e => e.Lsk)
                    .HasPrecision(18, 2)
                    .HasColumnName("lsk");

                entity.Property(e => e.Monthyeji)
                    .HasPrecision(18, 2)
                    .HasColumnName("monthyeji");

                entity.Property(e => e.Msgdate)
                    .HasColumnType("datetime")
                    .HasColumnName("msgdate")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.Mystudioid)
                    .HasColumnType("int(11)")
                    .HasColumnName("mystudioid");

                entity.Property(e => e.Mystudioname)
                    .HasMaxLength(255)
                    .HasColumnName("mystudioname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Password2)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password2");

                entity.Property(e => e.Pdt)
                    .HasColumnType("datetime")
                    .HasColumnName("pdt")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.Pv)
                    .HasPrecision(18, 2)
                    .HasColumnName("pv");

                entity.Property(e => e.Rdt)
                    .HasColumnType("datetime")
                    .HasColumnName("rdt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Recode)
                    .HasMaxLength(50)
                    .HasColumnName("recode")
                    .HasComment("推广码");

                entity.Property(e => e.Recount)
                    .HasColumnType("int(11)")
                    .HasColumnName("recount");

                entity.Property(e => e.Reid)
                    .HasColumnType("int(11)")
                    .HasColumnName("reid");

                entity.Property(e => e.Relevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("relevel");

                entity.Property(e => e.Rename)
                    .HasMaxLength(50)
                    .HasColumnName("rename");

                entity.Property(e => e.Repath).HasColumnName("repath");

                entity.Property(e => e.Reusername)
                    .HasMaxLength(50)
                    .HasColumnName("reusername");

                entity.Property(e => e.Reyeji)
                    .HasPrecision(18, 2)
                    .HasColumnName("reyeji");

                entity.Property(e => e.Riteamyeji)
                    .HasPrecision(18, 2)
                    .HasColumnName("riteamyeji")
                    .HasComment("当日的团队业绩");

                entity.Property(e => e.Shouru)
                    .HasPrecision(18, 2)
                    .HasColumnName("shouru");

                entity.Property(e => e.Sjjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("sjjine")
                    .HasComment("用于在场次结束后统计消费的信用值");

                entity.Property(e => e.StudioCard)
                    .HasMaxLength(255)
                    .HasColumnName("studio_card");

                entity.Property(e => e.StudioName)
                    .HasMaxLength(255)
                    .HasColumnName("studio_name");

                entity.Property(e => e.Tdan)
                    .HasColumnType("int(11)")
                    .HasColumnName("tdan");

                entity.Property(e => e.Teamcount)
                    .HasColumnType("int(11)")
                    .HasColumnName("teamcount");

                entity.Property(e => e.Teamyeji)
                    .HasPrecision(18, 2)
                    .HasColumnName("teamyeji");

                entity.Property(e => e.Tx).HasColumnName("tx");

                entity.Property(e => e.Ulevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("ulevel");

                entity.Property(e => e.Usercode)
                    .HasMaxLength(50)
                    .HasColumnName("usercode");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Usertel)
                    .HasMaxLength(50)
                    .HasColumnName("usertel");

                entity.Property(e => e.Wxheadimgurl).HasColumnName("wxheadimgurl");

                entity.Property(e => e.Wxnickname)
                    .HasMaxLength(100)
                    .HasColumnName("wxnickname");

                entity.Property(e => e.Wxtoken).HasColumnName("wxtoken");

                entity.Property(e => e.Xiaoquyeji)
                    .HasPrecision(18, 2)
                    .HasColumnName("xiaoquyeji");

                entity.Property(e => e.Xlevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("xlevel");

                entity.Property(e => e.Yjjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("yjjine")
                    .HasComment("用于在场次结束后结算奖金");

                entity.Property(e => e.Ylevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("ylevel");

                entity.Property(e => e.Ylsk)
                    .HasPrecision(18, 2)
                    .HasColumnName("ylsk");

                entity.Property(e => e.Zzxz)
                    .HasColumnType("int(11)")
                    .HasColumnName("zzxz")
                    .HasDefaultValueSql("'1'")
                    .HasComment("转账限制，1限制，0不限制");
            });

            modelBuilder.Entity<DbUsersAddress>(entity =>
            {
                entity.ToTable("db_users_address");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.AreaCode)
                    .HasMaxLength(50)
                    .HasColumnName("areaCode");

                entity.Property(e => e.Isdefault)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdefault");

                entity.Property(e => e.Odate)
                    .HasColumnType("datetime")
                    .HasColumnName("odate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Sheng)
                    .HasMaxLength(50)
                    .HasColumnName("sheng");

                entity.Property(e => e.Shi)
                    .HasMaxLength(50)
                    .HasColumnName("shi");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Usertel)
                    .HasMaxLength(50)
                    .HasColumnName("usertel");

                entity.Property(e => e.Xian)
                    .HasMaxLength(50)
                    .HasColumnName("xian");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbUsersAddress)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_users_address_ibfk_1");
            });

            modelBuilder.Entity<DbUsersBank>(entity =>
            {
                entity.ToTable("db_users_bank");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bankaddress)
                    .HasMaxLength(100)
                    .HasColumnName("bankaddress");

                entity.Property(e => e.Bankcard)
                    .HasMaxLength(50)
                    .HasColumnName("bankcard");

                entity.Property(e => e.Bankimg)
                    .HasMaxLength(200)
                    .HasColumnName("bankimg")
                    .HasComment("收款图（二维码）");

                entity.Property(e => e.Bankname)
                    .HasMaxLength(50)
                    .HasColumnName("bankname");

                entity.Property(e => e.Bdate)
                    .HasColumnType("datetime")
                    .HasColumnName("bdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Isdefault)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdefault");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Usertel)
                    .HasMaxLength(50)
                    .HasColumnName("usertel");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbUsersBank)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_users_bank_ibfk_1");
            });

            modelBuilder.Entity<DbUsersDelete>(entity =>
            {
                entity.ToTable("db_users_delete");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Ddate)
                    .HasColumnType("datetime")
                    .HasColumnName("ddate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Delip)
                    .HasMaxLength(50)
                    .HasColumnName("delip");

                entity.Property(e => e.Dellx)
                    .HasColumnType("int(11)")
                    .HasColumnName("dellx");

                entity.Property(e => e.Deluid)
                    .HasColumnType("int(11)")
                    .HasColumnName("deluid");

                entity.Property(e => e.Deluserid)
                    .HasMaxLength(50)
                    .HasColumnName("deluserid");

                entity.Property(e => e.Recount)
                    .HasColumnType("int(11)")
                    .HasColumnName("recount");

                entity.Property(e => e.Reid)
                    .HasColumnType("int(11)")
                    .HasColumnName("reid");

                entity.Property(e => e.Relevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("relevel");

                entity.Property(e => e.Rename)
                    .HasMaxLength(50)
                    .HasColumnName("rename");

                entity.Property(e => e.Repath).HasColumnName("repath");

                entity.Property(e => e.Teamcount)
                    .HasColumnType("int(11)")
                    .HasColumnName("teamcount");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<DbUsersFteam>(entity =>
            {
                entity.ToTable("db_users_fteam");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Area1)
                    .HasPrecision(18, 2)
                    .HasColumnName("area1");

                entity.Property(e => e.Area2)
                    .HasPrecision(18, 2)
                    .HasColumnName("area2");

                entity.Property(e => e.Area3)
                    .HasPrecision(18, 2)
                    .HasColumnName("area3");

                entity.Property(e => e.Area4)
                    .HasPrecision(18, 2)
                    .HasColumnName("area4");

                entity.Property(e => e.Area5)
                    .HasPrecision(18, 2)
                    .HasColumnName("area5");

                entity.Property(e => e.Ch1)
                    .HasColumnType("int(11)")
                    .HasColumnName("ch1");

                entity.Property(e => e.Ch2)
                    .HasColumnType("int(11)")
                    .HasColumnName("ch2");

                entity.Property(e => e.Ch3)
                    .HasColumnType("int(11)")
                    .HasColumnName("ch3");

                entity.Property(e => e.Ch4)
                    .HasColumnType("int(11)")
                    .HasColumnName("ch4");

                entity.Property(e => e.Ch5)
                    .HasColumnType("int(11)")
                    .HasColumnName("ch5");

                entity.Property(e => e.Fatherid)
                    .HasColumnType("int(11)")
                    .HasColumnName("fatherid");

                entity.Property(e => e.Fathername)
                    .HasMaxLength(50)
                    .HasColumnName("fathername");

                entity.Property(e => e.Fcount)
                    .HasColumnType("int(11)")
                    .HasColumnName("fcount");

                entity.Property(e => e.Fdate)
                    .HasColumnType("datetime")
                    .HasColumnName("fdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Flevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("flevel");

                entity.Property(e => e.Fpath).HasColumnName("fpath");

                entity.Property(e => e.Ftreeplace)
                    .HasColumnType("int(11)")
                    .HasColumnName("ftreeplace");

                entity.Property(e => e.Narea1)
                    .HasColumnType("int(11)")
                    .HasColumnName("narea1");

                entity.Property(e => e.Narea2)
                    .HasColumnType("int(11)")
                    .HasColumnName("narea2");

                entity.Property(e => e.Narea3)
                    .HasColumnType("int(11)")
                    .HasColumnName("narea3");

                entity.Property(e => e.Narea4)
                    .HasColumnType("int(11)")
                    .HasColumnName("narea4");

                entity.Property(e => e.Narea5)
                    .HasColumnType("int(11)")
                    .HasColumnName("narea5");

                entity.Property(e => e.Teamcount)
                    .HasColumnType("int(11)")
                    .HasColumnName("teamcount");

                entity.Property(e => e.Teamyeji)
                    .HasPrecision(18, 2)
                    .HasColumnName("teamyeji");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Yarea1)
                    .HasPrecision(18, 2)
                    .HasColumnName("yarea1");

                entity.Property(e => e.Yarea2)
                    .HasPrecision(18, 2)
                    .HasColumnName("yarea2");

                entity.Property(e => e.Yarea3)
                    .HasPrecision(18, 2)
                    .HasColumnName("yarea3");

                entity.Property(e => e.Yarea4)
                    .HasPrecision(18, 2)
                    .HasColumnName("yarea4");

                entity.Property(e => e.Yarea5)
                    .HasPrecision(18, 2)
                    .HasColumnName("yarea5");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbUsersFteam)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_users_fteam_ibfk_1");
            });

            modelBuilder.Entity<DbUsersFwzxApply>(entity =>
            {
                entity.ToTable("db_users_fwzx_apply");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bdaddress).HasColumnName("bdaddress");

                entity.Property(e => e.Bdlevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("bdlevel");

                entity.Property(e => e.Bdsheng)
                    .HasMaxLength(50)
                    .HasColumnName("bdsheng");

                entity.Property(e => e.Bdshi)
                    .HasMaxLength(50)
                    .HasColumnName("bdshi");

                entity.Property(e => e.Bdxian)
                    .HasMaxLength(50)
                    .HasColumnName("bdxian");

                entity.Property(e => e.Bz)
                    .HasColumnType("text")
                    .HasColumnName("bz");

                entity.Property(e => e.Fdate)
                    .HasColumnType("datetime")
                    .HasColumnName("fdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Ispay)
                    .HasColumnType("int(11)")
                    .HasColumnName("ispay");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sdate)
                    .HasColumnType("datetime")
                    .HasColumnName("sdate");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbUsersFwzxApply)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_users_fwzx_apply_ibfk_1");
            });

            modelBuilder.Entity<DbUsersJihuoRecord>(entity =>
            {
                entity.ToTable("db_users_jihuo_record");

                entity.HasIndex(e => e.Jid, "jid");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("货币id");

                entity.Property(e => e.Codename)
                    .HasMaxLength(50)
                    .HasColumnName("codename")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Coinname)
                    .HasMaxLength(50)
                    .HasColumnName("coinname")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Hblx)
                    .HasColumnType("int(11)")
                    .HasColumnName("hblx");

                entity.Property(e => e.Jdate)
                    .HasColumnType("datetime")
                    .HasColumnName("jdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Jid)
                    .HasColumnType("int(11)")
                    .HasColumnName("jid");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine");

                entity.Property(e => e.Juserid)
                    .HasMaxLength(100)
                    .HasColumnName("juserid");

                entity.Property(e => e.Jusername)
                    .HasMaxLength(100)
                    .HasColumnName("jusername");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(100)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.HasOne(d => d.JidNavigation)
                    .WithMany(p => p.DbUsersJihuoRecordJidNavigation)
                    .HasForeignKey(d => d.Jid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_users_jihuo_record_ibfk_2");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbUsersJihuoRecordUidNavigation)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_users_jihuo_record_ibfk_1");
            });

            modelBuilder.Entity<DbUsersLevelup>(entity =>
            {
                entity.ToTable("db_users_levelup");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine")
                    .HasComment("升级花费金额");

                entity.Property(e => e.Level)
                    .HasColumnType("int(11)")
                    .HasColumnName("level")
                    .HasComment("新级别");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx")
                    .HasComment("升级类型,0-ulevel,1-xlevel");

                entity.Property(e => e.Sdate)
                    .HasColumnType("datetime")
                    .HasColumnName("sdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasComment("状态,0未审核,1已审核");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Ylevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("ylevel")
                    .HasComment("原级别");
            });

            modelBuilder.Entity<DbWallets>(entity =>
            {
                entity.ToTable("db_wallets");

                entity.HasIndex(e => e.Uid, "users_wallets_ibfk_1");

                entity.HasIndex(e => e.Cid, "wallets_ibfk_2");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("货币id");

                entity.Property(e => e.Cname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cname")
                    .HasComment("货币英文名");

                entity.Property(e => e.CnameZh)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cname_zh")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine")
                    .HasComment("金额");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Wdate)
                    .HasColumnType("datetime")
                    .HasColumnName("wdate")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'")
                    .HasComment("生成记录时间");

                entity.Property(e => e.Yjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("yjine");

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.DbWallets)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("db_wallets_ibfk_2");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbWallets)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_wallets_ibfk_1");
            });

            modelBuilder.Entity<DbWalletsChongzhi>(entity =>
            {
                entity.ToTable("db_wallets_chongzhi");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Beizhu)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("beizhu");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("cdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("货币id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Coinname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Czimg)
                    .HasMaxLength(200)
                    .HasColumnName("czimg");

                entity.Property(e => e.Fdate)
                    .HasColumnType("datetime")
                    .HasColumnName("fdate");

                entity.Property(e => e.Isdelete)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdelete");

                entity.Property(e => e.Ispay)
                    .HasColumnType("int(11)")
                    .HasColumnName("ispay");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Usertel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("usertel");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbWalletsChongzhi)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_wallets_chongzhi_ibfk_1");
            });

            modelBuilder.Entity<DbWalletsChongzhiSelect>(entity =>
            {
                entity.ToTable("db_wallets_chongzhi_select");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("货币id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Coinname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Jinebs)
                    .HasColumnType("int(11)")
                    .HasColumnName("jinebs")
                    .HasDefaultValueSql("'1'")
                    .HasComment("充值倍数");

                entity.Property(e => e.Jinemax)
                    .HasPrecision(18, 2)
                    .HasColumnName("jinemax")
                    .HasDefaultValueSql("'10000.00'")
                    .HasComment("最高充值金额");

                entity.Property(e => e.Jinemin)
                    .HasPrecision(18, 2)
                    .HasColumnName("jinemin")
                    .HasDefaultValueSql("'1.00'")
                    .HasComment("最低充值金额");
            });

            modelBuilder.Entity<DbWalletsCoin>(entity =>
            {
                entity.ToTable("db_wallets_coin");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Coinname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname")
                    .HasComment("货币中文名称");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasDefaultValueSql("'1'")
                    .HasComment("是否使用: 1:使用  0:不使用");
            });

            modelBuilder.Entity<DbWalletsTixian>(entity =>
            {
                entity.ToTable("db_wallets_tixian");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bankaddress)
                    .HasMaxLength(255)
                    .HasColumnName("bankaddress");

                entity.Property(e => e.Bankcard)
                    .HasMaxLength(50)
                    .HasColumnName("bankcard");

                entity.Property(e => e.Bankimg).HasColumnName("bankimg");

                entity.Property(e => e.Bankname)
                    .HasMaxLength(50)
                    .HasColumnName("bankname");

                entity.Property(e => e.Beizhu)
                    .HasMaxLength(255)
                    .HasColumnName("beizhu");

                entity.Property(e => e.Chexiaoyuanyin)
                    .HasMaxLength(255)
                    .HasColumnName("chexiaoyuanyin");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("货币id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Coinname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Fdate)
                    .HasColumnType("datetime")
                    .HasColumnName("fdate");

                entity.Property(e => e.Isdelete)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdelete");

                entity.Property(e => e.Ispay)
                    .HasColumnType("int(11)")
                    .HasColumnName("ispay");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx");

                entity.Property(e => e.Shouxu)
                    .HasPrecision(18, 2)
                    .HasColumnName("shouxu");

                entity.Property(e => e.Tdate)
                    .HasColumnType("datetime")
                    .HasColumnName("tdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Usertel)
                    .HasMaxLength(50)
                    .HasColumnName("usertel");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.DbWalletsTixian)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("db_wallets_tixian_ibfk_1");
            });

            modelBuilder.Entity<DbWalletsTixianSelect>(entity =>
            {
                entity.ToTable("db_wallets_tixian_select");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("货币id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Coinname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Jinebs)
                    .HasColumnType("int(11)")
                    .HasColumnName("jinebs")
                    .HasDefaultValueSql("'1'")
                    .HasComment("提现倍数");

                entity.Property(e => e.Jinemax)
                    .HasPrecision(18, 2)
                    .HasColumnName("jinemax")
                    .HasDefaultValueSql("'10000.00'")
                    .HasComment("最高提现金额");

                entity.Property(e => e.Jinemin)
                    .HasPrecision(18, 2)
                    .HasColumnName("jinemin")
                    .HasDefaultValueSql("'1.00'")
                    .HasComment("最低提现金额");

                entity.Property(e => e.Shouxu)
                    .HasPrecision(18, 2)
                    .HasColumnName("shouxu")
                    .HasComment("提现手续费");
            });

            modelBuilder.Entity<DbWalletsZengjian>(entity =>
            {
                entity.ToTable("db_wallets_zengjian");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bz)
                    .HasMaxLength(50)
                    .HasColumnName("bz")
                    .HasComment("备注");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("货币id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Coinname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Isdelete)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdelete");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Xjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("xjine");

                entity.Property(e => e.Yjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("yjine");

                entity.Property(e => e.Zdate)
                    .HasColumnType("datetime")
                    .HasColumnName("zdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<DbWalletsZhuanhuan>(entity =>
            {
                entity.ToTable("db_wallets_zhuanhuan");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Beizhu)
                    .HasMaxLength(255)
                    .HasColumnName("beizhu");

                entity.Property(e => e.Cid1)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid1")
                    .HasComment("货币id");

                entity.Property(e => e.Cid2)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid2")
                    .HasComment("货币id");

                entity.Property(e => e.Codename1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename1")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Codename2)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename2")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Coinname1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname1")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Coinname2)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname2")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Isdelete)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdelete");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Zdate)
                    .HasColumnType("datetime")
                    .HasColumnName("zdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<DbWalletsZhuanhuanSelect>(entity =>
            {
                entity.ToTable("db_wallets_zhuanhuan_select");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Bili)
                    .HasPrecision(18, 2)
                    .HasColumnName("bili")
                    .HasDefaultValueSql("'100.00'")
                    .HasComment("转换比例");

                entity.Property(e => e.Cid1)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid1")
                    .HasComment("货币id,cid1转换cid2");

                entity.Property(e => e.Cid2)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid2")
                    .HasComment("货币id,cid1转换cid2");

                entity.Property(e => e.Codename1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename1")
                    .HasComment("货币英文名称1");

                entity.Property(e => e.Codename2)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename2")
                    .HasComment("货币英文名称2");

                entity.Property(e => e.Coinname1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname1")
                    .HasComment("货币中文名称1");

                entity.Property(e => e.Coinname2)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname2")
                    .HasComment("货币中文名称2");

                entity.Property(e => e.Jinebs)
                    .HasColumnType("int(11)")
                    .HasColumnName("jinebs")
                    .HasDefaultValueSql("'1'")
                    .HasComment("转换倍数");

                entity.Property(e => e.Jinemax)
                    .HasPrecision(18, 2)
                    .HasColumnName("jinemax")
                    .HasDefaultValueSql("'1.00'")
                    .HasComment("最大转换金额");

                entity.Property(e => e.Jinemin)
                    .HasPrecision(18, 2)
                    .HasColumnName("jinemin")
                    .HasDefaultValueSql("'1.00'")
                    .HasComment("最小转换金额");
            });

            modelBuilder.Entity<DbWalletsZhuanzhang>(entity =>
            {
                entity.ToTable("db_wallets_zhuanzhang");

                entity.HasIndex(e => e.Sid, "sid");

                entity.HasIndex(e => e.Uid, "uid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Beizhu)
                    .HasMaxLength(255)
                    .HasColumnName("beizhu");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("货币id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Coinname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Isdelete)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdelete");

                entity.Property(e => e.Jine)
                    .HasPrecision(18, 2)
                    .HasColumnName("jine");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx");

                entity.Property(e => e.Sid)
                    .HasColumnType("int(11)")
                    .HasColumnName("sid");

                entity.Property(e => e.Suserid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("suserid");

                entity.Property(e => e.Susername)
                    .HasMaxLength(50)
                    .HasColumnName("susername");

                entity.Property(e => e.Szhjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("szhjine");

                entity.Property(e => e.Szqjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("szqjine");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Zdate)
                    .HasColumnType("datetime")
                    .HasColumnName("zdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Zhjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("zhjine");

                entity.Property(e => e.Zqjine)
                    .HasPrecision(18, 2)
                    .HasColumnName("zqjine");
            });

            modelBuilder.Entity<DbWalletsZhuanzhangSelect>(entity =>
            {
                entity.ToTable("db_wallets_zhuanzhang_select");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid")
                    .HasComment("货币id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("codename")
                    .HasComment("货币英文名称");

                entity.Property(e => e.Coinname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("coinname")
                    .HasComment("货币中文名称");

                entity.Property(e => e.Jinebs)
                    .HasColumnType("int(11)")
                    .HasColumnName("jinebs")
                    .HasDefaultValueSql("'1'")
                    .HasComment("转账倍数");

                entity.Property(e => e.Jinemax)
                    .HasPrecision(18, 2)
                    .HasColumnName("jinemax")
                    .HasDefaultValueSql("'10000.00'")
                    .HasComment("最高转账金额");

                entity.Property(e => e.Jinemin)
                    .HasPrecision(18, 2)
                    .HasColumnName("jinemin")
                    .HasDefaultValueSql("'1.00'")
                    .HasComment("最低转账金额");

                entity.Property(e => e.Shouxu)
                    .HasPrecision(18, 2)
                    .HasColumnName("shouxu")
                    .HasComment("手续费");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasComment("团队限制，0否1是");
            });

            modelBuilder.Entity<DbYuyue>(entity =>
            {
                entity.ToTable("db_yuyue");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cedate)
                    .HasColumnType("time")
                    .HasColumnName("cedate");

                entity.Property(e => e.Cid)
                    .HasColumnType("int(11)")
                    .HasColumnName("cid");

                entity.Property(e => e.Cimg)
                    .HasMaxLength(255)
                    .HasColumnName("cimg");

                entity.Property(e => e.Cjindan)
                    .HasPrecision(10)
                    .HasColumnName("cjindan");

                entity.Property(e => e.Cmaxprice)
                    .HasPrecision(10, 2)
                    .HasColumnName("cmaxprice");

                entity.Property(e => e.Cminprice)
                    .HasPrecision(10, 2)
                    .HasColumnName("cminprice");

                entity.Property(e => e.Cname)
                    .HasMaxLength(255)
                    .HasColumnName("cname");

                entity.Property(e => e.Cqdate)
                    .HasColumnType("time")
                    .HasColumnName("cqdate");

                entity.Property(e => e.Crishouri)
                    .HasPrecision(18, 2)
                    .HasColumnName("crishouri")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.Cyyjindou)
                    .HasPrecision(18, 2)
                    .HasColumnName("cyyjindou")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.Czhouqi)
                    .HasColumnType("int(50)")
                    .HasColumnName("czhouqi")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Iscx)
                    .HasColumnType("int(11)")
                    .HasColumnName("iscx");

                entity.Property(e => e.Isdelete)
                    .HasColumnType("int(11)")
                    .HasColumnName("isdelete");

                entity.Property(e => e.Isly)
                    .HasColumnType("int(11)")
                    .HasColumnName("isly");

                entity.Property(e => e.Isyy)
                    .HasColumnType("int(11)")
                    .HasColumnName("isyy");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasColumnName("state")
                    .HasComment("0预约中，1预约成功，2与卖家交易中,3交易结束");

                entity.Property(e => e.Uid)
                    .HasColumnType("int(11)")
                    .HasColumnName("uid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(255)
                    .HasColumnName("userid");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.Property(e => e.Usertel)
                    .HasMaxLength(255)
                    .HasColumnName("usertel");

                entity.Property(e => e.Ydate)
                    .HasColumnType("datetime")
                    .HasColumnName("ydate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
