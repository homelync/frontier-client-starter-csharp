using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontier.Models
{
    /* This is a stub only. Add other properties you might need. */
    public class ReadingResult
    {
        public ReadingResult()
        {
            Devices = new List<DeviceReadingResult>();
        }
        public string Type { get; set; }
        public int Count { get; set; }

        public List<DeviceReadingResult> Devices { get; set; }

        public override string ToString()
        {
            return $"{Type} {String.Join(",", Devices.Select(x => x.ToString()))}";
        }
    }

    /* This is a stub only. Add other properties you might need. */
    public class DeviceReadingResult
    {
        public DeviceReadingResult()
        {
            Values = new List<Reading>();
        }

        public string SerialNumber { get; set; }
        public List<Reading> Values { get; set; }

        public override string ToString()
        {
            return $"{SerialNumber} {String.Join(",", Values.Select(x => x.ToString()))}";
        }
    }

    /* This is a stub only. Add other properties you might need. */
    public class Reading
    {
        public decimal Value { get; set; }
        public DateTime ReadingDate { get; set; }

        public override string ToString()
        {
            return $"{Value} {ReadingDate}";
        }
    }
}