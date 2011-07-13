namespace ButtonOffice
{
    internal class FloatingText
    {
        private System.Drawing.Color _Color;
        private System.Drawing.PointF _Location;
        private System.String _Text;
        private System.DateTime _Timeout;

        public System.Drawing.Color Color
        {
            get
            {
                return _Color;
            }
        }

        public System.Drawing.PointF Location
        {
            get
            {
                return _Location;
            }
        }

        public System.String Text
        {
            get
            {
                return _Text;
            }
        }

        public System.DateTime Timeout
        {
            get
            {
                return _Timeout;
            }
        }

        public FloatingText()
        {
        }

        public void SetColor(System.Drawing.Color Color)
        {
            _Color = Color;
        }

        public void SetLocation(System.Drawing.PointF Location)
        {
            _Location = Location;
        }

        public void SetText(System.String Text)
        {
            _Text = Text;
        }

        public void SetTimeout(System.DateTime Timeout)
        {
            _Timeout = Timeout;
        }
    }
}
