using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbArticle
    {
        public int Id { get; set; }
        public string Articletitle { get; set; }
        public string Articlecontent { get; set; }
    }
}
