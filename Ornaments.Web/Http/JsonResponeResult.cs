using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ornaments.Web.Http
{
    public static class JsonResponeResult
    {
        public static JsonResult SendStatus(JsonReqponseStatusType type,string message,object data,bool isFinally)
        {
            return new JsonResult(new { status = type.ToString(), message = message, data = data ,isFinally=isFinally});
        }
      
    }
    public enum JsonReqponseStatusType
    {
        Success,
        Warning,
        Error,
        Info
    }
}
