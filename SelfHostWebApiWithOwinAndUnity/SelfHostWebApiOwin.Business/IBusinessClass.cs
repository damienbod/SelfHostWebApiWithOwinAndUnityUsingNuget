using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SelfHostWebApiOwin.Business
{
    public interface IBusinessClass : IDisposable
    {
        string Hello();
    }
}
