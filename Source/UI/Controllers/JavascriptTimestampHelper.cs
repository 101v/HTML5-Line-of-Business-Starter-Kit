using System;

namespace FabrikamWidgets.UI
{
    ///<summary>
    ///</summary>
    public static class JavascriptTimestampHelper
    {
        private const string InvalidEpochErrorMessage = "Javascript epoc starts January 1st, 1970";

        /// <summary>
        ///   Convert a long into a DateTime
        /// </summary>
        public static DateTime FromJavascriptTimestamp(this Int64 self)
        {
            var ret = new DateTime(1970, 1, 1);
            return ret.AddMilliseconds(self);
        }

        /// <summary>
        ///   Convert a DateTime into a long
        /// </summary>
        public static Int64 ToJavascriptTimestamp(this DateTime self)
        {

            if (self == DateTime.MinValue)
            {
                return 0;
            }

            var epoc = new DateTime(1970, 1, 1);
            var delta = self - epoc;

            if (delta.TotalMilliseconds < 0) throw new ArgumentOutOfRangeException(InvalidEpochErrorMessage);

            return (long)delta.TotalMilliseconds;
        }
    }
}
