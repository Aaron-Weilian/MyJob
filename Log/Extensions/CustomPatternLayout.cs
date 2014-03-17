using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net.Core;
using log4net.Util;
using log4net.Layout.Pattern;
using System.Collections;
using log4net.Layout;

namespace Log.Extensions
{
    public class CustomPatternLayout : PatternLayout
    {
        public CustomPatternLayout()
        {
            this.AddConverter("property", typeof(XPatternLayoutConverter));
        }
    }
}
