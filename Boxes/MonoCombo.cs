namespace Boites
{
    internal class MonoCombo : Boite
    {
        public MonoCombo(Boite boite) : base(boite)
        {
            throw new BonusNonSupportéException("Bonus pas supporté");
        }
    }
}