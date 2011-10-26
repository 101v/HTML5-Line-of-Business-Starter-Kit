using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FabrikamWidgets.UI
{
    /// <remarks>
    /// http://kazimanzurrashid.com/posts/meet-spine-dot-js-my-framework-of-choice-for-client-side-mvc-part-2
    /// </remarks>
    public class JsonNetResult : JsonResult
    {
        public Formatting Formatting { get; set; }
        public int HttpStatusCode { get; set; }
        public JsonSerializerSettings Settings { get; private set; }

        public JsonNetResult()
        {
#if DEBUG
            Formatting = Newtonsoft.Json.Formatting.Indented;
#endif
            Settings = new JsonSerializerSettings();
            Settings.Converters.Add(new IsoDateTimeConverter());
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Get must be explicitly specified.");
            }

            var response = context.HttpContext.Response;

            response.ContentType = string.IsNullOrWhiteSpace(ContentType) ?
                                   "application/json" :
                                   ContentType;

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            using (var writer = new JsonTextWriter(response.Output)
            {
                Formatting = Formatting
            })
            {
                JsonSerializer.Create(Settings).Serialize(writer, Data);
            }

            response.StatusCode = HttpStatusCode;
        }
    }
}