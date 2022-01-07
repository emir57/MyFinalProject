using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
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
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = exception.Message;
            _loggerServiceBase.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetGenericArguments()[i].Name,
                    Type = invocation.Arguments[i].GetType().ToString(),
                    Value = invocation.Arguments[i]
                });
            }
            var logDetailWithException = new LogDetailWithException
            {
                LogParameters = logParameters,
                MethodName = invocation.Method.Name
            };
            return logDetailWithException;
        }
    }
}
