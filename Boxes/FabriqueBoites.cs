using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    internal class FabriqueBoites
    {
        public IBoite? Créer(string input)
        {
            var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            return ManipulerLignes(lines);
        }

        private IBoite? ManipulerLignes(List<string> lignes)
        {
            if (lignes.Count == 0) return null;

            string first = lignes[0];
            lignes.RemoveAt(0);

            var parts = first.Split(" ", 2);

            switch (parts[0])
            {
                case "mono":
                    return new MonoBoite(parts.Length > 1 ? parts[1] : "");

                case "cv":
                    IBoite? top = ManipulerLignes(lignes);
                    IBoite? bottom = ManipulerLignes(lignes);
                    return (top != null && bottom != null) ? new ComboVertical(new Boite(top), new Boite(bottom)) : null;

                case "ch":
                    IBoite? left = ManipulerLignes(lignes);
                    IBoite? right = ManipulerLignes(lignes);
                    return (left != null && right != null) ? new ComboHorizontal(new Boite(left), new Boite(right)) : null;

                default:
                    throw new InvalidOperationException($"Commande inconnue: {first}");
            }
        }
    }
}