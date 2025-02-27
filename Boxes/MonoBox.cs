using System.Collections;

namespace Boxes
{
    internal class MonoBox : IBox
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public List<string> Lines { get; } = [];

        public MonoBox(string text = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                Lines.Add("");
                Width = Lines.Max(line => line.Length);
                Height = Lines.Count;
            }
            else
            {
                Lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();
                Width = Lines.Max(line => line.Length);
                Height = Lines.Count;
            }
        }

        public IEnumerator<string> GetEnumerator() => new Enum(Lines);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        

        // Custom Enumerator Class
        private class Enum : IEnumerator<string>
        {
            private readonly List<string> Source;
            private int Index = -1;

            public Enum(List<string> src)
            {
                Source = src;
            }

            public string Current => Source[Index];

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
