using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Layout.Pattern;

namespace Log.Layout.Pattern
{
    internal class UserIPPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            if (loggingEvent.MessageObject is Log.Extensions.CustomPatternMessage)
            {
                Log.Extensions.CustomPatternMessage cpm = loggingEvent.MessageObject as Log.Extensions.CustomPatternMessage;
                writer.Write(cpm.UserIP);
            }
        }
    }
}
