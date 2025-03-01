
namespace Boites
{
    internal class BonusNonSupportéException : Exception
    {
        public BonusNonSupportéException(string? message) : base(message)
        {
        }
    }
}