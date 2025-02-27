using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    internal class ComboHorizontal : IBox
    {
        public int Height { get; set; }
        public int Width { get; set;}
        private readonly List<(string, Position)> Lines = [];

        public ComboHorizontal(Box left, Box right)
        {
            Width = left.Width + right.Width + 1;
            Height = Math.Max(left.Height, right.Height);

            var leftClone = new Box(left);
            var rightClone = new Box(right);

            leftClone.Height = Height;
            rightClone.Height = Height;
            leftClone.Width = Width;
            rightClone.Width = Width;

            IEnumerator<string> leftEnum = leftClone._Box!.GetEnumerator();
            IEnumerator<string> rightEnum = rightClone._Box!.GetEnumerator();

            for (int i = 0; i < Height; i++)
            {
                string leftPart = leftEnum.MoveNext() ? leftEnum.Current.PadRight(left.Width) : new string(' ', left.Width);
                string rightPart = rightEnum.MoveNext() ? rightEnum.Current.PadRight(right.Width) : new string(' ', right.Width);

                Lines.Add(($"{leftPart}|{rightPart}", Position.Middle));
            }
        }

        public IEnumerator<string> GetEnumerator() => new ComboHorizontalEnumerator(Lines);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class ComboHorizontalEnumerator : IEnumerator<string>
        {
            private readonly List<(string, Position)> Source;
            private int Index = -1;

            public ComboHorizontalEnumerator(List<(string, Position)> src)
            {
                Source = src;
            }

            public string Current => Source[Index].Item1;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (Index >= Source.Count - 1)
                    return false;

                Index++;
                return true;
            }

            public void Reset()
            {
                Index = -1;
            }

            public void Dispose() { }
        }
    }
}
