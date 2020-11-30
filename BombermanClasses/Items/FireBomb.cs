

using BombermanClasses.BombNameSpace;
using System.Drawing;
/**
* @(#) FireBomb.cs
*/
namespace BombermanClasses.Items
{
    public class FireBomb : Bomb
    {
        public FireBomb()
        {
        }
        public FireBomb(string playerId, int x, int y, IBombRadiusStrategy strategy, World subject) : base(playerId, x, y, strategy, subject)
        {
        }
    }

}
