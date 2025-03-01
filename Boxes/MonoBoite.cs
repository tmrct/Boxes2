using System.Collections;

namespace Boites
{
    internal class MonoBoite : IBoite
    {
        public int Hauteur { get; set; }
        public int Largeur { get; set; }
        public List<string> Lignes { get; } = [];

        protected MonoBoite(MonoBoite mb)
        {
            Hauteur = mb.Hauteur;
            Largeur = mb.Largeur;
            Lignes = new(mb.Lignes);
        }

        public MonoBoite(string text = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                Lignes.Add("");
                Largeur = Lignes.Max(line => line.Length);
                Hauteur = Lignes.Count;
            }
            else
            {
                Lignes = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();
                Largeur = Lignes.Max(line => line.Length);
                Hauteur = Lignes.Count;
            }
        }

        public IEnumerator<string> GetEnumerator() => new MonoBoxEnum(Lignes);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IBoite Clone() => new MonoBoite(this);

        public void Redimensionner(int largeur, int hauteur)
        {
            Largeur = largeur; 
            Hauteur = hauteur;
        }
        public void Accepter(IVisiteur<IBoite> visiteur)
        {
            visiteur.Entrer();
            visiteur.Visiter(this, () => Console.WriteLine($"Mono {Hauteur} x {Largeur}"));
            visiteur.Sortir();
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
