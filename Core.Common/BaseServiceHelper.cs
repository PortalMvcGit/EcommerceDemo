using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common
{
    public abstract class BaseServiceHelper
    {
        public abstract TResult ExecuteServicePostRequest<TRequest, TResult>(string apiUrl, TRequest request, int cancelTime = 0);


        public abstract TResult ExecuteServiceGetRequest<TResult>(string apiUrl, int cancelTime = 0);

    }
}
