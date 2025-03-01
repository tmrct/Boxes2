using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    internal class ComboHorizontal : IBox
    {
        public int Height { get; set; }
        public int Width { get; set; }
        private IBox LeftBox { get; set; }
        private IBox RightBox { get; set; }

        public ComboHorizontal(IBox left, IBox right)
        {
            Width = left.Width + right.Width + 1;
            Height = Math.Max(left.Height, right.Height);

            LeftBox = left.Clone();
            RightBox = right.Clone();

            LeftBox.Resize(LeftBox.Width, Height);
            RightBox.Resize(RightBox.Width, Height);
        }

        public IEnumerator<string> GetEnumerator() => new ComboHorizontalEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IBox Clone() => new ComboHorizontal(LeftBox, RightBox);

        public void Resize(int width, int height)
        {
            Width = width;
            Height = height;
            LeftBox.Resize(LeftBox.Width, Height);
            RightBox.Resize(Width - LeftBox.Width - 1, Height);
        }

        private class ComboHorizontalEnumerator : IEnumerator<string>
        {
            private IEnumerator<string> LeftBoxEnum;
            private IEnumerator<string> RightBoxEnum;
            private ComboHorizontal Ch { get; set; }

            public ComboHorizontalEnumerator(ComboHorizontal ch)
            {
                LeftBoxEnum = ch.LeftBox.GetEnumerator();
                RightBoxEnum = ch.RightBox.GetEnumerator();
                Ch = ch;
            }

            public string Current
            {
                get
                {
                    string left = LeftBoxEnum.Current ?? new string(' ', Ch.LeftBox.Width);
                    string right = RightBoxEnum.Current ?? new string(' ', Ch.RightBox.Width);
                    left = left.PadRight(Ch.LeftBox.Width);
                    if (gaucheFini)
                        left = new string(' ', Ch.LeftBox.Width);
                    if (droiteFini)
                        right = new string(' ', Ch.RightBox.Width);
                    return left + "|" + right;
                }
            }


            object IEnumerator.Current => Current;
            private bool gaucheFini = false;
            private bool droiteFini = false;
            private int counter = 0;
            public bool MoveNext()
            {
                gaucheFini = !LeftBoxEnum.MoveNext();
                droiteFini = !RightBoxEnum.MoveNext();

                counter++;

                return counter <= Ch.Height;
            }

            public void Reset()
            {
                LeftBoxEnum.Reset();
                RightBoxEnum.Reset();
            }

            public void Dispose() { }
        }
    }
}