using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.MapBuilder
{
    public abstract class AbstractMapBuilder
    {
        protected Map Map = new Map();

        public abstract void InitEmpty();

        public abstract void BuildIndestructableWalls();

        public abstract void BuildDestructableWalls();

        public abstract void BuildItemDropWalls();


        public Map Build()
        {
            return Map;
        }
    }
}
