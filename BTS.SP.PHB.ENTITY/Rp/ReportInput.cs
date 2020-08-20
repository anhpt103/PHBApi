using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp
{
    public class ReportInput
    {
        public string store_name;
        public List<ReportInputParam> param;
    }
    public class ReportInputParam
    {
        public string name;
        public OracleDbType type;
        public string value;
    }
}
