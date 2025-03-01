using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    enum Position { Top, Middle, Bottom, End }

    internal class ComboVertical : IBox
    {
        public int Height { get; set; }
        public int Width { get; set; }
        private IBox TopBox { get; set; }
        private IBox BottomBox { get; set; }

        public ComboVertical(IBox top, IBox bottom)
        {
            Width = Math.Max(top.Width, bottom.Width);
            Height = top.Height + bottom.Height + 1; // +1 pour ligne separateur

            TopBox = top.Clone();
            BottomBox = bottom.Clone();

            TopBox.Resize(Width, TopBox.Height);
            BottomBox.Resize(Width, BottomBox.Height);
        }

        public IEnumerator<string> GetEnumerator() => new ComboVerticalEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IBox Clone() => new ComboVertical(TopBox, BottomBox);

        public void Resize(int width, int height)
        {
            Width = width;
            Height = height;
            TopBox.Resize(Width, TopBox.Height);
            BottomBox.Resize(Width, Height - TopBox.Height - 1);
        }

        private class ComboVerticalEnumerator : IEnumerator<string>
        {
            private IEnumerator<string> EnumTop { get; set; }
            private IEnumerator<string> EnumBottom { get; set; }
            private Position _Position { get; set; }
            private ComboVertical Cv { get; set; }

            public ComboVerticalEnumerator(ComboVertical cv)
            {
                EnumTop = cv.TopBox.GetEnumerator();
                EnumBottom = cv.BottomBox.GetEnumerator();
                Cv = cv;
                _Position = Position.Top;
            }

            public string Current 
            { 
                get 
                {
                    switch (_Position)
                    {
                        case Position.Top:
                            return EnumTop.Current.PadRight(Cv.Width, ' ');
                        case Position.Middle:
                            return new string('-', Cv.Width);
                        case Position.Bottom:
                            return EnumBottom.Current.PadRight(Cv.Width, ' ');
                        case Position.End:
                            return new string(' ', Cv.Width);
                        default: return "";
                    }
                } 
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                switch (_Position)
                {
                    case Position.Top:
                        if(!EnumTop.MoveNext())
                            _Position = Position.Middle;
                        return true;
                    case Position.Middle:
                        _Position = Position.Bottom;
                        EnumBottom.MoveNext();
                        return true;
                    case Position.Bottom:
                        return EnumBottom.MoveNext();
                    case Position.End:
                        return false;
                    default: return false;
                }
            }

            public void Reset() { }

            public void Dispose() { }
        }
    }
}
