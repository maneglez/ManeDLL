using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Mane.BD;

namespace Mane.Security.Models
{
    public class Token : WebModel<Token>
    {
        protected override string ConnName => TokenConfig.TokenConnectionName;
        protected override string NombreTabla => "Tokens";
        public DateTime CreatedDate { get; set; }
        public DateTime LastUse { get; set; }
        public string TokenText { get; set; }
        public string User_id { get; set; }
        public bool Active { get; set; }
        public int IntUserId() {
            if (int.TryParse(User_id, out int id))
                return id;
            return 0;
        }
        public new void Save()
        {
            if (!exists())
            {
                CreatedDate = DateTime.Now;
                LastUse = DateTime.Now;
            }
            base.Save();
        }

        

        public static Token FindByToken(string token)
        {
            return Query().Where("TokenText", token).First();
        }
       
    }
}
