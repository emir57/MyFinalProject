using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect:MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        public ExceptionLogAspect(Type loggerService)
        {
            if (!typeof(LoggerServiceBase).IsAssignableFrom(loggerService))
            {
                throw new System.Exception(AspectMessages.WrongLoggingType);
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }
        protected override void OnException(IInvocation invocation, System.Exception exception)
        {
            var logDetailWithException = GetLogDetail(invocation);

        }
    }
}
