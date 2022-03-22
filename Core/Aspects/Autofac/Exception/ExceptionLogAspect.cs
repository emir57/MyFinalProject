using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect: MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        private IHttpContextAccessor _httpContextAccessor;
        public ExceptionLogAspect(Type loggerService)
        {
            if (!typeof(LoggerServiceBase).IsAssignableFrom(loggerService))
            {
                throw new System.Exception(AspectMessages.WrongLoggingType);
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
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
            List<string> claims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            string userEmail = _httpContextAccessor.HttpContext.User.UserEmail();
            var logDetailWithException = new LogDetailWithException
            {
                LogParameters = logParameters,
                MethodName = invocation.Method.Name,
                UserEmail = userEmail,
                UserRoles = claims,
                DateTime = DateTime.Now
            };
            return logDetailWithException;
        }
    }
}
