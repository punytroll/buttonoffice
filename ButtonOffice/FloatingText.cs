namespace ButtonOffice
{
    internal class FloatingText
    {
        private System.Drawing.Color _Color;
        private System.Drawing.PointF _Offset;
        private System.Drawing.PointF _Origin;
        private System.String _Text;
        private System.Single _Timeout;

        public System.Drawing.Color Color
        {
            get
            {
                return _Color;
            }
        }

        public System.Drawing.PointF Offset
        {
            get
            {
                return _Offset;
            }
        }

        public System.Drawing.PointF Origin
        {
            get
            {
                return _Origin;
            }
        }

        public System.String Text
        {
            get
            {
                return _Text;
            }
        }

        public System.Single Timeout
        {
            get
            {
                return _Timeout;
            }
        }

        public void SetColor(System.Drawing.Color Color)
        {
            _Color = Color;
        }

        public void SetOffset(System.Drawing.PointF Offset)
        {
            _Offset = Offset;
        }

        public void SetOrigin(System.Drawing.PointF Origin)
        {
            _Origin = Origin;
        }

        public void SetText(System.String Text)
        {
            _Text = Text;
        }

        public void SetTimeout(System.Single Timeout)
        {
            _Timeout = Timeout;
        }
    }
}
