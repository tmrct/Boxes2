namespace Boxes
{
    class Box
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public IBox? _Box;

        public Box(IBox? p)
        {
            _Box = new MonoBox();
            Height = _Box.Height;
            Width = _Box.Width;
        }

        public Box(string text)
        {
            _Box = new MonoBox(text);
            Height = _Box.Height;
            Width = _Box.Width;
        }
        public Box(Box box)
        {
            _Box = box._Box;
            Height = box._Box.Height;
            Width = box._Box.Width;
        }

        public Box(ComboHorizontal ch)
        {
            _Box = ch;
            Height = _Box.Height;
            Width = _Box.Width;
        }

        public Box(ComboVertical cv)
        {
            _Box = cv;
            Height = _Box.Height;
            Width = _Box.Width;
        }

        public override string ToString()
        {
            string res = $"+{new string('-', Width)}+\r\n";

            foreach (string s in _Box)
            {
                if(s != "")
                    res += $"|{s.PadRight(Width)}|\n";
            }

            res += $"+{new string('-', Width)}+";

            return res;
        }
    }
}

