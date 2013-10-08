using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostWebApiOwin.Business
{
    public interface IUnitOfWorkExample : IDisposable
    {
        string HelloFromUnitOfWorkExample();

        void Deposit(decimal depositAmount);
    }
}
