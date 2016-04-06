using System;
using System.Linq;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Extensions
{
    public static class JsonResults
    {
        public static ModelAttributeError[] GetErrorsAsArray(this ModelStateDictionary modelState)
        {
            var errorsInModel =
                from keyValuePair in modelState
                let errors = keyValuePair.Value.Errors
                where errors.Any()
                select new ModelAttributeError
                {
                    Name = keyValuePair.Key,
                    Errors = errors
                };

            return errorsInModel.ToArray();
        }

        public static JsonResult JsonFailure(this Controller control, object obj)
        {
            return new JsonResult
            {
                Data = new JsonRequestResult
                {
                    Data = obj,
                    Success = false
                }
            };
        }

        public static JsonResult JsonSuccess(this Controller control, object obj)
        {
            return new JsonResult
            {
                Data = new JsonRequestResult
                {
                    Data = obj,
                    Success = true
                }
            };
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