using System;
using System.Drawing;

namespace ButtonOffice
{
    internal class FloatingText
    {
        private Color _Color;
        private PointF _Offset;
        private PointF _Origin;
        private String _Text;
        private Single _Timeout;

        public Color Color
        {
            get
            {
                return _Color;
            }
        }

        public PointF Offset
        {
            get
            {
                return _Offset;
            }
        }

        public PointF Origin
        {
            get
            {
                return _Origin;
            }
        }

        public String Text
        {
            get
            {
                return _Text;
            }
        }

        public Single Timeout
        {
            get
            {
                return _Timeout;
            }
        }

        public void SetColor(Color Color)
        {
            _Color = Color;
        }

        public void SetOffset(PointF Offset)
        {
            _Offset = Offset;
        }

        public void SetOrigin(PointF Origin)
        {
            _Origin = Origin;
        }

        public void SetText(String Text)
        {
            _Text = Text;
        }

        public void SetTimeout(Single Timeout)
        {
            _Timeout = Timeout;
        }
    }
}
