using System.Collections;

namespace Boxes
{
    internal class MonoBoite : IBoite
    {
        public int Hauteur { get; set; }
        public int Largeur { get; set; }
        public List<string> Lines { get; } = [];

        protected MonoBoite(MonoBoite mb)
        {
            Hauteur = mb.Hauteur;
            Largeur = mb.Largeur;
            Lines = new(mb.Lines);
        }

        public MonoBoite(string text = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                Lines.Add("");
                Largeur = Lines.Max(line => line.Length);
                Hauteur = Lines.Count;
            }
            else
            {
                Lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();
                Largeur = Lines.Max(line => line.Length);
                Hauteur = Lines.Count;
            }
        }

        public IEnumerator<string> GetEnumerator() => new MonoBoxEnum(Lines);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IBoite Clone() => new MonoBoite(this);

        public void Redimensionner(int width, int height)
        {
            Largeur = width; 
            Hauteur = height;
        }
        public void Accepter(IVisiteur<IBoite> visitor)
        {
            visitor.Entrer();
            visitor.Visiter(this, () => Console.WriteLine($"Mono {Hauteur} x {Largeur}"));
            visitor.Sortir();
        }
        private class MonoBoxEnum : IEnumerator<string>
        {
            private readonly List<string> Source;
            private int Index = -1;

            public MonoBoxEnum(List<string> src)
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
