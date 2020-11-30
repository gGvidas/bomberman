using BombermanClasses.BombNameSpace;
using BombermanClasses.Items;
using BombermanClasses.TemplateMethod;
using System;

namespace BombermanClasses
{
    [Serializable]
    public class Tile
    {
        public Bomb bomb { get; set; } = null;
        public Item item { get; set; } = null;

        public FirePlayer firetype { get; set; } = null;
        public OnFirePlayer onfiretype { get; set; } = null;
        public IcePlayer icetype { get; set; } = null;

        public DestructionTemplate entity { get; set; }

        public DestructionTemplate destroy()
        {
            if (entity != null)
            {
                bool canDestroy = entity.Destroy();
                if (canDestroy)
                {
                    Type type = entity.GetType();
                    DestructionTemplate returnObject = (DestructionTemplate) Activator.CreateInstance(type);
                    entity = null;
                    return returnObject;
                }
            }
            return null;
        }

        public void clear()
        {
            bomb = null;
            item = null;
            firetype = null;
            onfiretype = null;
            icetype = null;
            entity = null;
        }
    }
}
