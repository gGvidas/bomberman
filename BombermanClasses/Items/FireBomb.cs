

using BombermanClasses.BombNameSpace;
using System.Drawing;
/**
* @(#) FireBomb.cs
*/
namespace BombermanClasses.Items
{
    public class FireBomb : Bomb
    {
        public FireBomb(int x, int y, IBombRadiusStrategy strategy, World subject) : base(x, y, strategy, subject)
        {
        }
    }

}
