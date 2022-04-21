using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common
{
    public abstract class BaseServiceHelper
    {
        public abstract TResult ExecuteServiceRequest<TRequest, TResult>(string apiUrl, TRequest request, string baseAddress = null, int cancelTime = 0);

    }
}
