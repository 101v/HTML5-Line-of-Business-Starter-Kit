using System;

namespace FabrikamWidgets.UI
{
    public class ActivityObject
    {
        public string Id { get; set; }
        public string ObjectType { get; set; }
        public string DisplayName { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string[] DownstreamDuplicates { get; set; }
        public string[] UpstreamDuplicates { get; set; }
        public MediaLink Image { get; set; }
        public ActivityObject Author { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }
    }
}
