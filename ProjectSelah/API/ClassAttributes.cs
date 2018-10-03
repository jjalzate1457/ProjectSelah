using System;

namespace ProjectSelah.API
{
    public class PropertyAttr : Attribute
    {
        public bool DoNotCompare { get; set; }
    }
}
