using System.Collections;
using System.Collections.Generic;

namespace Boxes
{
    class Box : IBox
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public IBox? _Box;
        public Box()
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
            _Box = box._Box.Clone();
            Height = box.Height;
            Width = box.Width;
        }


        public Box(IBox box)
        {
            _Box = box.Clone();
            Height = _Box.Height;
            Width = _Box.Width;
        }

        public override string ToString()
        {
            string res = $"+{new string('-', Width)}+\r\n";

            foreach (string s in _Box)
            {
                if (s != "") 
                    res += $"|{s.PadRight(Width)}|\n";
            }

            res += $"+{new string('-', Width)}+";

            return res;
        }
        public IBox Clone() => new Box(this);

        public IEnumerator<string> GetEnumerator() => _Box.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _Box.GetEnumerator();

        public void Resize(int width, int height)
        {
            Width = width;
            Height = height;
            _Box.Resize(width, height);
        }
    }
}

