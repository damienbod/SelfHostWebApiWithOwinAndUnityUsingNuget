using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfHostWebApiOwin.Business.Logging;
using SelfHostWebApiOwin.Business.Attributes;

namespace SelfHostWebApiOwin.Business
{
    [UnityIoCTransientLifetimeAttribute]
    public class BusinessClass : IBusinessClass
    {
        private IUnitOfWorkExample _unitOfWorkExample;

        public BusinessClass(IUnitOfWorkExample unitOfWorkExample)
        {
            _unitOfWorkExample = unitOfWorkExample;
            UnityEventLogger.Log.CreateUnityMessage("BusinessClass");
        }

        private bool _disposed = false;

        public string Hello()
        {
            return _unitOfWorkExample.HelloFromUnitOfWorkExample();
        }

        public void Dispose()
        {
            _unitOfWorkExample.Dispose();
            UnityEventLogger.Log.DisposeUnityMessage("BusinessClass");
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}