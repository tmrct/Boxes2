using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    enum Position
    {
        Left, Middle, Right, Top, Bottom, End
    }

    internal class ComboVertical : IBox
    {
        public int Height { get; set; }
        public int Width { get; set; }
        private readonly List<(string, Position)> Lines = new();

        public ComboVertical(Box top, Box bottom)
        {
            Width = Math.Max(top.Width, bottom.Width);
            Height = top.Height + bottom.Height + 1;

            IEnumerator<string> topEnum = top._Box!.GetEnumerator();
            IEnumerator<string> bottomEnum = bottom._Box!.GetEnumerator();

            // Iterate over top box (Top Position)
            while (topEnum.MoveNext())
            {
                Lines.Add((topEnum.Current.PadRight(Width), Position.Top));
            }

            // Add separator line (Middle Position)
            Lines.Add((new string('-', Width), Position.Middle));

            // Iterate over bottom box (Bottom Position)
            while (bottomEnum.MoveNext())
            {
                Lines.Add((bottomEnum.Current.PadRight(Width), Position.Bottom));
            }

            // Mark the end (End Position)
            Lines.Add((new string(' ', Width), Position.End)); // Could be another visual separator
        }

        public IEnumerator<string> GetEnumerator() => new ComboVerticalEnumerator(Lines);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class ComboVerticalEnumerator : IEnumerator<string>
        {
            private readonly List<(string, Position)> Source;
            private int Index = -1;

            public ComboVerticalEnumerator(List<(string, Position)> src)
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
