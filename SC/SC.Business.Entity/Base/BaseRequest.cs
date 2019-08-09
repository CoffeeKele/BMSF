using System;

namespace KMHC.SLTC.Business.Entity
{
    public class BaseRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

    public class BaseRequest<T> : BaseRequest where T : class
    {
        public T Data { get; set; }

        public BaseRequest()
        {
            this.Data = (T)Activator.CreateInstance(typeof(T));
            this.CurrentPage = 1;
            this.PageSize = 10;
        }
    }
}
