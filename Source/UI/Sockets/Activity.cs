using System;

namespace FabrikamWidgets.UI
{
    public class Activity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Verb { get; set; }
        public string Url { get; set; }
        public ActivityObject Actor { get; set; }
        public ActivityObject @Object { get; set; }
        public ActivityObject Target { get; set; }
        public MediaLink Icon { get; set; }
        public ActivityObject Generator { get; set; }
        public ActivityObject Provider { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }
    }
}
