using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CtrlBox.UI.Web.Extensions
{
    public class CustomExceptionHandler
    {
        /// <summary>
        /// Just sample
        /// IExceptionHandler interface
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        private void OnException(Exception context, HttpResponse response)
        {
            var result = JsonConvert.SerializeObject(GetResultException(context, response));
            #region Logging  
            //if (ConfigurationHelper.GetConfig()[ConfigurationHelper.environment].ToLower() != "dev")  
            //{  
            //    LogMessage objLogMessage = new LogMessage()  
            //    {  
            //        ApplicationName = ConfigurationHelper.GetConfig()["ApplicationName"].ToString(),  
            //        ComponentType = (int) ComponentType.Server,  
            //        ErrorMessage = errorMessage,  
            //        LogType = (int) LogType.EventViewer,  
            //        ErrorStackTrace = stackTrace,  
            //        UserName = Common.GetAccNameDev(context.HttpContext)  

            //    };  
            //    LogError(objLogMessage, LogEntryType.Error);  
            //}  
            #endregion Logging  
            response.ContentType = "application/json";
            response.ContentLength = result.Length;
            response.WriteAsync(result);
        }

        public enum Exceptions
        {
            NullReferenceException = 1,
            FileNotFoundException = 2,
            OverflowException = 3,
            OutOfMemoryException = 4,
            InvalidCastException = 5,
            ObjectDisposedException = 6,
            UnauthorizedAccessException = 7,
            NotImplementedException = 8,
            NotSupportedException = 9,
            InvalidOperationException = 10,
            TimeoutException = 11,
            ArgumentException = 12,
            FormatException = 13,
            StackOverflowException = 14,
            SqlException = 15,
            IndexOutOfRangeException = 16,
            IOException = 17
        }

        private static HttpStatusCode getErrorCodeStatic(Type exceptionType)
        {
            Exceptions tryParseResult;
            if (Enum.TryParse<Exceptions>(exceptionType.Name, out tryParseResult))
            {
                switch (tryParseResult)
                {
                    case Exceptions.NullReferenceException:
                        return HttpStatusCode.LengthRequired;

                    case Exceptions.FileNotFoundException:
                        return HttpStatusCode.NotFound;

                    case Exceptions.OverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Exceptions.OutOfMemoryException:
                        return HttpStatusCode.ExpectationFailed;

                    case Exceptions.InvalidCastException:
                        return HttpStatusCode.PreconditionFailed;

                    case Exceptions.ObjectDisposedException:
                        return HttpStatusCode.Gone;

                    case Exceptions.UnauthorizedAccessException:
                        return HttpStatusCode.Unauthorized;

                    case Exceptions.NotImplementedException:
                        return HttpStatusCode.NotImplemented;

                    case Exceptions.NotSupportedException:
                        return HttpStatusCode.NotAcceptable;

                    case Exceptions.InvalidOperationException:
                        return HttpStatusCode.MethodNotAllowed;

                    case Exceptions.TimeoutException:
                        return HttpStatusCode.RequestTimeout;

                    case Exceptions.ArgumentException:
                        return HttpStatusCode.BadRequest;

                    case Exceptions.StackOverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Exceptions.FormatException:
                        return HttpStatusCode.UnsupportedMediaType;

                    case Exceptions.IOException:
                        return HttpStatusCode.NotFound;

                    case Exceptions.IndexOutOfRangeException:
                        return HttpStatusCode.ExpectationFailed;

                    default:
                        return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static object AjaxException(Exception context, HttpResponse response)
        {
            return GetResultException(context, response);
        }

        private static object GetResultException(Exception context, HttpResponse response)
        {
            HttpStatusCode statusCode = (context as WebException != null &&
                        ((HttpWebResponse)(context as WebException).Response) != null) ?
                         ((HttpWebResponse)(context as WebException).Response).StatusCode
                         : getErrorCodeStatic(context.GetType());
            string errorMessage = context.Message;
            string customErrorMessage = "ERROR";
            string stackTrace = context.StackTrace;

            response.StatusCode = (int)statusCode;
            return new
            {
                message = customErrorMessage,
                isError = true,
                errorMessage,
                errorCode = statusCode,
                model = string.Empty,
                InnerEx = context.InnerException.Message
            };
        }
    }
}
