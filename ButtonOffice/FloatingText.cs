using System;
using System.Drawing;

namespace ButtonOffice
{
    internal class FloatingText
    {
        private Color _Color;
        private PointF _Offset;
        private Vector2 _Origin;
        private String _Text;

        public Double Timeout
        {
            get;
            set;
        }

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

        public Vector2 Origin
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

        public void SetColor(Color Color)
        {
            _Color = Color;
        }

        public void SetOffset(PointF Offset)
        {
            _Offset = Offset;
        }

        public void SetOrigin(Vector2 Origin)
        {
            _Origin = Origin;
        }

        public void SetText(String Text)
        {
            _Text = Text;
        }
    }
}
