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

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect:MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        public LogAspect(Type loggingType)
        {
            if (!typeof(LoggerServiceBase).IsAssignableFrom(loggingType))
            {
                throw new Exception(AspectMessages.WrongLoggingType);
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggingType);
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Type = invocation.Arguments[i].GetType().ToString(),
                    Value = invocation.Arguments[i]
                });
            }
            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
            return logDetail;
        }
    }
}
