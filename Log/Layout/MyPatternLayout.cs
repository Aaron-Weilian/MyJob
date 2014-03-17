using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Layout;
using log4net.Util;
using log4net.Layout.Pattern;
using System.Collections;
using System.IO;
using log4net.Core;

namespace Log.Layout
{
    public class MyPatternLayout : LayoutSkeleton
    {
        #region copy from PatternLayout
        		/// <summary>
		/// Wrapper class used to map converter names to converter types
		/// </summary>
		/// <remarks>
		/// <para>
		/// Pattern converter info class used during configuration to
		/// pass to the <see cref="M:log4net.Layout.PatternLayout.AddConverter(log4net.Layout.PatternLayout.ConverterInfo)" />
		/// method.
		/// </para>
		/// </remarks>
		public sealed class ConverterInfo
		{
			private string m_name;
			private Type m_type;
			/// <summary>
			/// Gets or sets the name of the conversion pattern
			/// </summary>
			/// <remarks>
			/// <para>
			/// The name of the pattern in the format string
			/// </para>
			/// </remarks>
			public string Name
			{
				get
				{
					return this.m_name;
				}
				set
				{
					this.m_name = value;
				}
			}
			/// <summary>
			/// Gets or sets the type of the converter
			/// </summary>
			/// <remarks>
			/// <para>
			/// The value specified must extend the 
			/// <see cref="T:log4net.Util.PatternConverter" /> type.
			/// </para>
			/// </remarks>
			public Type Type
			{
				get
				{
					return this.m_type;
				}
				set
				{
					this.m_type = value;
				}
			}
		}
		/// <summary>
		/// Default pattern string for log output. 
		/// </summary>
		/// <remarks>
		/// <para>
		/// Default pattern string for log output. 
		/// Currently set to the string <b>"%message%newline"</b> 
		/// which just prints the application supplied message. 
		/// </para>
		/// </remarks>
		public const string DefaultConversionPattern = "%message%newline";
		/// <summary>
		/// A detailed conversion pattern
		/// </summary>
		/// <remarks>
		/// <para>
		/// A conversion pattern which includes Time, Thread, Logger, and Nested Context.
		/// Current value is <b>%timestamp [%thread] %level %logger %ndc - %message%newline</b>.
		/// </para>
		/// </remarks>
		public const string DetailConversionPattern = "%timestamp [%thread] %level %logger %ndc - %message%newline";
		/// <summary>
		/// Internal map of converter identifiers to converter types.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This static map is overridden by the m_converterRegistry instance map
		/// </para>
		/// </remarks>
		private static Hashtable s_globalRulesRegistry;
		/// <summary>
		/// the pattern
		/// </summary>
		private string m_pattern;
		/// <summary>
		/// the head of the pattern converter chain
		/// </summary>
		private PatternConverter m_head;
		/// <summary>
		/// patterns defined on this PatternLayout only
		/// </summary>
		private Hashtable m_instanceRulesRegistry = new Hashtable();
		/// <summary>
		/// The pattern formatting string
		/// </summary>
		/// <remarks>
		/// <para>
		/// The <b>ConversionPattern</b> option. This is the string which
		/// controls formatting and consists of a mix of literal content and
		/// conversion specifiers.
		/// </para>
		/// </remarks>
		public string ConversionPattern
		{
			get
			{
				return this.m_pattern;
			}
			set
			{
				this.m_pattern = value;
			}
		}
        static MyPatternLayout()
		{
			MyPatternLayout.s_globalRulesRegistry = new Hashtable(45);
            MyPatternLayout.s_globalRulesRegistry.Add("message", typeof(Log.Layout.Pattern.MessagePatternConverter));
            MyPatternLayout.s_globalRulesRegistry.Add("event_name", typeof(Log.Layout.Pattern.EventNamePatternConverter));
            MyPatternLayout.s_globalRulesRegistry.Add("user_ip", typeof(Log.Layout.Pattern.UserIPPatternConverter));
		}
		/// <summary>
		/// Constructs a PatternLayout using the DefaultConversionPattern
		/// </summary>
		/// <remarks>
		/// <para>
		/// The default pattern just produces the application supplied message.
		/// </para>
		/// <para>
		/// Note to Inheritors: This constructor calls the virtual method
		/// <see cref="M:log4net.Layout.PatternLayout.CreatePatternParser(System.String)" />. If you override this method be
		/// aware that it will be called before your is called constructor.
		/// </para>
		/// <para>
		/// As per the <see cref="T:log4net.Core.IOptionHandler" /> contract the <see cref="M:log4net.Layout.PatternLayout.ActivateOptions" />
		/// method must be called after the properties on this object have been
		/// configured.
		/// </para>
		/// </remarks>
		public MyPatternLayout() : this("%message%newline")
		{
		}
		/// <summary>
		/// Constructs a PatternLayout using the supplied conversion pattern
		/// </summary>
		/// <param name="pattern">the pattern to use</param>
		/// <remarks>
		/// <para>
		/// Note to Inheritors: This constructor calls the virtual method
		/// <see cref="M:log4net.Layout.PatternLayout.CreatePatternParser(System.String)" />. If you override this method be
		/// aware that it will be called before your is called constructor.
		/// </para>
		/// <para>
		/// When using this constructor the <see cref="M:log4net.Layout.PatternLayout.ActivateOptions" /> method 
		/// need not be called. This may not be the case when using a subclass.
		/// </para>
		/// </remarks>
		public MyPatternLayout(string pattern)
		{
			this.IgnoresException = true;
			this.m_pattern = pattern;
			if (this.m_pattern == null)
			{
				this.m_pattern = "%message%newline";
			}
			this.ActivateOptions();
		}
		/// <summary>
		/// Create the pattern parser instance
		/// </summary>
		/// <param name="pattern">the pattern to parse</param>
		/// <returns>The <see cref="T:log4net.Util.PatternParser" /> that will format the event</returns>
		/// <remarks>
		/// <para>
		/// Creates the <see cref="T:log4net.Util.PatternParser" /> used to parse the conversion string. Sets the
		/// global and instance rules on the <see cref="T:log4net.Util.PatternParser" />.
		/// </para>
		/// </remarks>
		protected virtual PatternParser CreatePatternParser(string pattern)
		{
			PatternParser patternParser = new PatternParser(pattern);
			foreach (DictionaryEntry dictionaryEntry in MyPatternLayout.s_globalRulesRegistry)
			{
				patternParser.PatternConverters[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			foreach (DictionaryEntry dictionaryEntry in this.m_instanceRulesRegistry)
			{
				patternParser.PatternConverters[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return patternParser;
		}
		/// <summary>
		/// Initialize layout options
		/// </summary>
		/// <remarks>
		/// <para>
		/// This is part of the <see cref="T:log4net.Core.IOptionHandler" /> delayed object
		/// activation scheme. The <see cref="M:log4net.Layout.PatternLayout.ActivateOptions" /> method must 
		/// be called on this object after the configuration properties have
		/// been set. Until <see cref="M:log4net.Layout.PatternLayout.ActivateOptions" /> is called this
		/// object is in an undefined state and must not be used. 
		/// </para>
		/// <para>
		/// If any of the configuration properties are modified then 
		/// <see cref="M:log4net.Layout.PatternLayout.ActivateOptions" /> must be called again.
		/// </para>
		/// </remarks>
		public override void ActivateOptions()
		{
			this.m_head = this.CreatePatternParser(this.m_pattern).Parse();
			for (PatternConverter patternConverter = this.m_head; patternConverter != null; patternConverter = patternConverter.Next)
			{
				PatternLayoutConverter patternLayoutConverter = patternConverter as PatternLayoutConverter;
				if (patternLayoutConverter != null)
				{
					if (!patternLayoutConverter.IgnoresException)
					{
						this.IgnoresException = false;
						break;
					}
				}
			}
		}
		/// <summary>
		/// Produces a formatted string as specified by the conversion pattern.
		/// </summary>
		/// <param name="loggingEvent">the event being logged</param>
		/// <param name="writer">The TextWriter to write the formatted event to</param>
		/// <remarks>
		/// <para>
		/// Parse the <see cref="T:log4net.Core.LoggingEvent" /> using the patter format
		/// specified in the <see cref="P:log4net.Layout.PatternLayout.ConversionPattern" /> property.
		/// </para>
		/// </remarks>
		public override void Format(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			for (PatternConverter patternConverter = this.m_head; patternConverter != null; patternConverter = patternConverter.Next)
			{
				patternConverter.Format(writer, loggingEvent);
			}
		}
		/// <summary>
		/// Add a converter to this PatternLayout
		/// </summary>
		/// <param name="converterInfo">the converter info</param>
		/// <remarks>
		/// <para>
		/// This version of the method is used by the configurator.
		/// Programmatic users should use the alternative <see cref="M:log4net.Layout.PatternLayout.AddConverter(System.String,System.Type)" /> method.
		/// </para>
		/// </remarks>
		public void AddConverter(PatternLayout.ConverterInfo converterInfo)
		{
			this.AddConverter(converterInfo.Name, converterInfo.Type);
		}
		/// <summary>
		/// Add a converter to this PatternLayout
		/// </summary>
		/// <param name="name">the name of the conversion pattern for this converter</param>
		/// <param name="type">the type of the converter</param>
		/// <remarks>
		/// <para>
		/// Add a named pattern converter to this instance. This
		/// converter will be used in the formatting of the event.
		/// This method must be called before <see cref="M:log4net.Layout.PatternLayout.ActivateOptions" />.
		/// </para>
		/// <para>
		/// The <paramref name="type" /> specified must extend the 
		/// <see cref="T:log4net.Util.PatternConverter" /> type.
		/// </para>
		/// </remarks>
		public void AddConverter(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!typeof(PatternConverter).IsAssignableFrom(type))
			{
				throw new ArgumentException("The converter type specified [" + type + "] must be a subclass of log4net.Util.PatternConverter", "type");
			}
			this.m_instanceRulesRegistry[name] = type;
		}
        #endregion 
    }
}
