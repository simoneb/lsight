using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Text.RegularExpressions;
using lsight.Model;
using System.Linq;

namespace lsight.Services
{
    [Export(typeof(ITimestampingService))]
    class TimestampingService : ITimestampingService
    {
        private readonly IEnumerable<string> tokens = new[] {"d", "M", "y", "H", "h", "m", "s", "f"};

        public IEnumerable<TimestampedLine> Timestamp(IEnumerable<string> lines, string timestampPattern)
        {
            var regexPattern = tokens.Aggregate(timestampPattern, (current, token) => current.Replace(token, @"\d"));
            var regex = new Regex(string.Format(@"(?<timestamp>{0})", regexPattern));

            return lines.Select(l => new TimestampedLine
            {
                Timestamp = DateTime.ParseExact(regex.Match(l).Groups["timestamp"].Value, timestampPattern, CultureInfo.InvariantCulture),
                Line = l
            });
        }
    }
}