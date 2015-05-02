using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofTop.Core.ApplicationServices;

namespace RoofTop.Test.Fakes
{
    public class FakeCurrentUser: ICurrentUserService
    {
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserID { get; set; }
   
    }
}
