using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class Service<T>
    {
        public bool Success { get; private set; }   
        public string? Message { get; private set; }
        public T? data { get; set; }

        Service(bool Success, string? Message, T? data)
        {
            this.Success=Success;
            this.Message=Message;
            this.data= data;
        }

        public static Service<T> success (T data)
        {
            return new Service<T> (true,null,data);
        }
        public static Service<T> failure (string message)
        {
            return new Service<T> (false,message,default);
        }
    }
}