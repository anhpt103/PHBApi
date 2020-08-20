using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.AUTHENTICATION.API
{
    public class RoleState
    {
         
        public string STATE { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Approve { get; set; }

        public RoleState()
        {
            STATE = "";
            View = false;
            Add = false;
            Edit = false;
            Delete = false;
            Approve = false;
        }
    }
}
