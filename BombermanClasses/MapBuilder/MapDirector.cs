using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.MapBuilder
{
    class MapDirector
    {
        private AbstractMapBuilder _builder;

        public MapDirector(AbstractMapBuilder builder)
        {
            _builder = builder;
        }

        public void Construct()
        {
            _builder.InitEmpty();
            _builder.BuildIndestructableWalls();
            _builder.BuildDestructableWalls();
            _builder.BuildItemDropWalls();
        }
    }
}
