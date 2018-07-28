using System;
using System.Collections.Generic;
using System.Text;
using TrendMobile.DataContracts.Enums;

namespace TrendMobile.DataContracts
{
    public class EntryType
    {
        public string Name { get; set; }
        public List<Entry> Entries { get; set; }
        public EntryTypeDataType DataType { get; set; }
    }
}
