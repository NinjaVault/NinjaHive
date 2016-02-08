using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Extensions
{
    public static class JsonResults
    {
        public static ModelAttributeError[] GetErrorsAsArray(this ModelStateDictionary modelState)
        {
            var errors = modelState.Where(c => c.Value.Errors.Any())
                                   .Select(c => new ModelAttributeError{ Name = c.Key, Errors = c.Value.Errors });
            return errors.ToArray();
        }
        public static JsonResult JsonFailure(this Controller control, object obj, JsonRequestBehavior behaviour = JsonRequestBehavior.DenyGet)
        {
            var res = new JsonResult();
            res.Data = new JsonRequestResult { Data = obj, Success = false };
            return res;
        }
        public static JsonResult JsonSuccess(this Controller control, object obj, JsonRequestBehavior behaviour = JsonRequestBehavior.DenyGet)
        {
            var res = new JsonResult();
            res.Data = new JsonRequestResult { Data = obj, Success = true };
            return res;
        }

        [Serializable]
        public struct JsonRequestResult
        {
            public bool Success { get; set; }
            public object Data { get; set; }
        }
        public struct ModelAttributeError
        {
            public string Name { get; set; }
            public ModelErrorCollection Errors { get; set; }
        }
    }
}