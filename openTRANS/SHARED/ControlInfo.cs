﻿using System;
using System.Xml.Serialization;

namespace openTRANS
{
    public partial class ControlInfo
    {
        [XmlElement("GENERATOR_INFO")]
        public string GeneratorInfo;

        [XmlElement("GENERATION_DATE")]
        public DateTime GenerationDate = DateTime.Now;
    }
}
