

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
        public IceBomb(int x, int y, IBombRadiusStrategy strategy, World subject) : base(x, y, strategy, subject)
        {
        }

    }

}
