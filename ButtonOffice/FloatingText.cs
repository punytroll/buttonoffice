using System;
using System.Drawing;

namespace ButtonOffice
{
    internal class FloatingText
    {
        public Double Timeout
        {
            get;
            set;
        }
        
        public Color Color
        {
            get;
            set;
        }
        
        public Vector2 Offset
        {
            get;
            set;
        }
        
        public Vector2 Origin
        {
            get;
            set;
        }
        
        public String Text
        {
            get;
            set;
        }
    }
}
