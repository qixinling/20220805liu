using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace Server.Models
{
    public static class BackupUtils
    {
        public static bool Backup(string Server, string User, string Pwd, string Database, string Path1, string FileName)
        {
            bool Done = true;
            try
            {
                if (!Directory.Exists(Path1))
                {
                    Directory.CreateDirectory(Path1);
                }

                string Constring = string.Format("Server={0};User={1};Pwd={2};Database={3};", Server, User, Pwd, Database);

                // Important Additional Connection Options
                Constring += "Charset=utf8;Convertzerodatetime=true;";

                string FilePath = Path.Combine(Path1, FileName);

                using MySqlConnection conn = new MySqlConnection(Constring);
                using MySqlCommand cmd = new MySqlCommand();
                using MySqlBackup mb = new MySqlBackup(cmd);
                cmd.Connection = conn;
                conn.Open();
                mb.ExportToFile(FilePath);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return Done;
        }

        public static bool Restore(string Server, string User, string Pwd, string Database, string Path1, string FileName)
        {
            bool Done = true;
            try
            {
                string Constring = string.Format("Server={0};user={1};Pwd={2};Database={3};", Server, User, Pwd, Database);

                // Important Additional Connection Options
                Constring += "Charset=utf8;Convertzerodatetime=true;";

                string filePath = Path.Combine(Path1, FileName);

                using MySqlConnection conn = new MySqlConnection(Constring);
                using MySqlCommand cmd = new MySqlCommand();
                using MySqlBackup mb = new MySqlBackup(cmd);
                cmd.Connection = conn;
                conn.Open();
                mb.ImportFromFile(filePath);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return Done;
        }

        public static bool Delete(string Path1, string DelName)
        {
            bool Done = true;
            try
            {
                string FilePath = Path.Combine(Path1, DelName);
                FileInfo file = new FileInfo(FilePath);
                if ((file.Attributes & FileAttributes.ReadOnly) > 0)
                {
                    file.Attributes ^= FileAttributes.ReadOnly; // 必须去除只读属性才能进行设置
                }
                file.Delete();
            }
            catch (Exception)
            {
                throw;
            }
            return Done;
        }
    }
}
