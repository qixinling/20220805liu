using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Utils
{
    public static class WalletsCoinUtils
    {
        /// <summary>
        /// 删除货币方法
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="_dbConnect"></param>
        public static void CoinDelete(int Id,DbConnect _dbConnect)
        {
            DbWalletsChongzhiSelect czs = _dbConnect.DbWalletsChongzhiSelect.FirstOrDefault(z => z.Cid == Id);
            if (czs != null)
            {
                _dbConnect.DbWalletsChongzhiSelect.Remove(czs);
            }
            DbWalletsTixianSelect txs = _dbConnect.DbWalletsTixianSelect.FirstOrDefault(z => z.Cid == Id);
            if (txs != null)
            {
                _dbConnect.DbWalletsTixianSelect.Remove(txs);
            }
            DbWalletsZhuanzhangSelect zzs = _dbConnect.DbWalletsZhuanzhangSelect.FirstOrDefault(z => z.Cid == Id);
            if (zzs != null)
            {
                _dbConnect.DbWalletsZhuanzhangSelect.Remove(zzs);
            }
            List<DbWalletsZhuanhuanSelect> zhs = _dbConnect.DbWalletsZhuanhuanSelect.Where(z => z.Cid1 == Id || z.Cid2 == Id).ToList();
            for (int i = 0; i < zhs.Count; i++)

            {
                _dbConnect.DbWalletsZhuanhuanSelect.Remove(zhs[i]);
            }

            List<DbWallets> ub = _dbConnect.DbWallets.Where(u => u.Cid == Id).ToList();
            for (int i = 0; i < ub.Count; i++)
            {
                _dbConnect.DbWallets.Remove(ub[i]);
            }
            _dbConnect.SaveChanges();
        }

        /// <summary>
        /// 修改货币英文名称方法
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="_dbConnect"></param>
        public static void CodenameUpdate(int Id, string Name,DbConnect _dbConnect)
        {
            List<DbWallets> ulist = _dbConnect.DbWallets.Where(u => u.Cid == Id).ToList();
            foreach (DbWallets uw in ulist)
            {
                uw.Cname = Name;
            }
            List<DbWalletsChongzhiSelect> czlist = _dbConnect.DbWalletsChongzhiSelect.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsChongzhiSelect cz in czlist)
            {
                cz.Codename = Name;
            }
            List<DbWalletsTixianSelect> txlist = _dbConnect.DbWalletsTixianSelect.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsTixianSelect tx in txlist)
            {
                tx.Codename = Name;
            }
            List<DbWalletsZhuanzhangSelect> zzlist = _dbConnect.DbWalletsZhuanzhangSelect.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsZhuanzhangSelect zz in zzlist)
            {
                zz.Codename = Name;
            }
            List<DbWalletsZhuanhuanSelect> zhlist = _dbConnect.DbWalletsZhuanhuanSelect.Where(c => c.Cid1 == Id || c.Cid2 == Id).ToList();
            foreach (DbWalletsZhuanhuanSelect zh in zhlist)
            {
                if (zh.Cid1 == Id)
                {
                    zh.Codename1 = Name;
                }
                else
                {
                    zh.Codename2 = Name;
                }
            }

            //记录
            List<DbWalletsChongzhi> clist = _dbConnect.DbWalletsChongzhi.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsChongzhi c in clist)
            {
                c.Codename = Name;
            }
            List<DbWalletsTixian> tlist = _dbConnect.DbWalletsTixian.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsTixian t in tlist)
            {
                t.Codename = Name;
            }
            List<DbWalletsZhuanzhang> zlist = _dbConnect.DbWalletsZhuanzhang.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsZhuanzhang z in zlist)
            {
                z.Codename = Name;
            }
            List<DbWalletsZhuanhuan> hlist = _dbConnect.DbWalletsZhuanhuan.Where(c => c.Cid1 == Id || c.Cid2 == Id).ToList();
            foreach (DbWalletsZhuanhuan h in hlist)
            {
                if (h.Cid1 == Id)
                {
                    h.Codename1 = Name;
                }
                else
                {
                    h.Codename2 = Name;
                }
            }

            _dbConnect.SaveChanges();
        }

        /// <summary>
        /// 修改货币中文名称方法
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="_dbConnect"></param>
        public static void CoinnameUpdate(int Id, string Name, DbConnect _dbConnect)
        {
            List<DbWallets> ulist = _dbConnect.DbWallets.Where(u => u.Cid == Id).ToList();
            foreach (DbWallets uw in ulist)
            {
                uw.CnameZh = Name;
            }
            List<DbWalletsChongzhiSelect> czlist = _dbConnect.DbWalletsChongzhiSelect.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsChongzhiSelect cz in czlist)
            {
                cz.Coinname = Name;
            }
            List<DbWalletsTixianSelect> txlist = _dbConnect.DbWalletsTixianSelect.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsTixianSelect tx in txlist)
            {
                tx.Coinname = Name;
            }
            List<DbWalletsZhuanzhangSelect> zzlist = _dbConnect.DbWalletsZhuanzhangSelect.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsZhuanzhangSelect zz in zzlist)
            {
                zz.Coinname = Name;
            }
            List<DbWalletsZhuanhuanSelect> zhlist = _dbConnect.DbWalletsZhuanhuanSelect.Where(c => c.Cid1 == Id || c.Cid2 == Id).ToList();
            foreach (DbWalletsZhuanhuanSelect zh in zhlist)
            {
                if (zh.Cid1 == Id)
                {
                    zh.Coinname1 = Name;
                }
                else
                {
                    zh.Coinname2 = Name;
                }
            }

            //记录
            List<DbWalletsChongzhi> clist = _dbConnect.DbWalletsChongzhi.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsChongzhi c in clist)
            {
                c.Coinname = Name;
            }
            List<DbWalletsTixian> tlist = _dbConnect.DbWalletsTixian.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsTixian t in tlist)
            {
                t.Coinname = Name;
            }
            List<DbWalletsZhuanzhang> zlist = _dbConnect.DbWalletsZhuanzhang.Where(c => c.Cid == Id).ToList();
            foreach (DbWalletsZhuanzhang z in zlist)
            {
                z.Coinname = Name;
            }
            List<DbWalletsZhuanhuan> hlist = _dbConnect.DbWalletsZhuanhuan.Where(c => c.Cid1 == Id || c.Cid2 == Id).ToList();
            foreach (DbWalletsZhuanhuan h in hlist)
            {
                if (h.Cid1 == Id)
                {
                    h.Coinname1 = Name;
                }
                else
                {
                    h.Coinname2 = Name;
                }
            }

            _dbConnect.SaveChanges();
        }
    }
}
