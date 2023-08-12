using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model.Respone.ResponseAccount
{
    public class ResponseLogin
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public long Expiration { get; set; }
    }
}
