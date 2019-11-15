using Microsoft.AspNetCore.Http;
using System;

namespace InventariosApi.Helper
{
    public static class Extensions
    {
        public static void AddCustomHeaderError(this HttpResponse response, string error)
        {
            bool addHeather = false;

            try
            {
                response.Headers.Add("Application-Error", error); //Add to the headers Application-Error
                addHeather = true;
            }
            catch (Exception)
            {
                addHeather = false;
            }
            finally
            {
                if (addHeather)
                {
                    response.Headers.Add("Access-Control-Expose-Headers", "Application-Error"); //we expose the header that we just created "application-Error"
                    response.Headers.Add("Access-Control-Allow-Origin", "*"); //any origin will have access to this header
                }

            }

        }
    }
}
