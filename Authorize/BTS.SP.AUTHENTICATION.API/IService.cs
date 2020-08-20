using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BTS.SP.AUTHENTICATION.API
{
    public interface IService
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
