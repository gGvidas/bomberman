

using BombermanClasses.BombNameSpace;
using System.Drawing;
using System.Threading.Tasks;
/**
* @(#) IceBomb.cs
*/
namespace BombermanClasses.Items
{
    public class IceBomb : Bomb
    {
        public IceBomb()
        {
        }

        public IceBomb(string playerId, int x, int y, IBombRadiusStrategy strategy, World subject) : base(playerId, x, y, strategy, subject)
        {
        }

    }

}
