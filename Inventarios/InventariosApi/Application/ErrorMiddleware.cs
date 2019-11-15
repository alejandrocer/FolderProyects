using InventariosApi.Helper;
using InventariosApi.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InventariosApi.Application
{
    public class ErrorMiddleware : IErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _UrlLog;
        public ErrorMiddleware(RequestDelegate next, string UrlLog)
        {
            _next = next;
            _UrlLog = UrlLog;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var Contexto = await new StreamReader(context.Request.Body).ReadToEndAsync();
                var injectedRequestStream = new MemoryStream();
                var bytesToWrite = Encoding.UTF8.GetBytes(Contexto);
                injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                injectedRequestStream.Seek(0, SeekOrigin.Begin);
                context.Request.Body = injectedRequestStream;

                SaveLogInitCall(Contexto.ToString());

                await _next(context);

            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var httpCode = (int)HttpStatusCode.InternalServerError;
            var path = context.Request.Method + " - " + context.Request.Path;
            var transactionId = System.Guid.NewGuid().ToString();
            var code = exception.HResult;
            var stackTrace = (exception.StackTrace == null) ? "null" : exception.StackTrace.ToString();
            var innerError = (exception.InnerException == null) ? "null" : exception.InnerException.ToString();
            string description = "InnerError : " + innerError + " | stackTrace : " + stackTrace;
            var message = exception.Message;

            Error cerror = new Error
            {
                Message = message,
                HttpCode = httpCode,
                Description = description,
                Code = code,
                Path = path,
                Timestamp = DateTime.Now,
                TransactionId = transactionId
            };

            var result = JsonConvert.SerializeObject(cerror);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpCode;
            context.Response.AddCustomHeaderError(result);

            SaveLogInitCall(result, context);

            return context.Response.WriteAsync(result);
        }

        public void SaveLogInitCall(string datos, HttpContext context = null)
        {
            var Fecha = DateTime.Now;
            var path = String.Format(@"\LogFile{0}{1}{2}.txt", Fecha.Year, (Fecha.Month < 10 ? "0" + Fecha.Month : Fecha.Month.ToString()), (Fecha.Day < 10 ? "0" + Fecha.Day : Fecha.Day.ToString()));
            if (datos != null)
            {
                using (StreamWriter w = File.AppendText(_UrlLog + path))
                {
                    w.Write("\n-----------------------{0}---------------------------", Fecha.ToString());
                    w.WriteLine("\n {0}", datos);
                    w.WriteLine("\n-------------------------------");
                }
            }
        }
    }
}
