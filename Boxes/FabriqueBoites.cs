using System;
using System.Collections.Generic;
using System.Linq;

namespace Boites
{
    internal class FabriqueBoites
    {
        public IBoite? Créer(string input)
        {
            var lignes = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            return ManipulerLignes(lignes);
        }

        private IBoite? ManipulerLignes(List<string> lignes)
        {
            if (lignes.Count == 0) return null;

            string s = lignes[0];
            lignes.RemoveAt(0);

            var parties = s.Split(" ", 2);

            switch (parties[0])
            {
                case "mono":
                    return new MonoBoite(parties.Length > 1 ? parties[1] : "");

                case "cv":
                    IBoite? boiteHaut = ManipulerLignes(lignes);
                    IBoite? boiteBas = ManipulerLignes(lignes);
                    return (boiteHaut != null && boiteBas != null) ? new ComboVertical(new Boite(boiteHaut), new Boite(boiteBas)) : null;

                case "ch":
                    IBoite? gauche = ManipulerLignes(lignes);
                    IBoite? droite = ManipulerLignes(lignes);
                    return (gauche != null && droite != null) ? new ComboHorizontal(new Boite(gauche), new Boite(droite)) : null;

                default:
                    throw new InvalidOperationException($"Commande inconnue: {s}");
            }
        }
    }
}