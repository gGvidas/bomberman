﻿using Bomberman;
using BombermanClasses.Observer;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace BombermanClasses.BombNameSpace
{
    public class Bomb : IMapObject, IObserver
    {
        public int x { get; set; }
        public int y { get; set; }

        private int explosionTime = 2;
        private int currentTime = 0;

        private Subject Subject;

        public IBombRadiusStrategy strategy { get; set; }

        public Bomb(int x, int y, IBombRadiusStrategy strategy, Subject subject)
        {
            if (subject == null) return;
            this.x = x;
            this.y = y;
            this.strategy = strategy;
            Subject = subject;

            Subject.Attach(this);
        }

        public int explosionRadius(int a)
        {
            return strategy.calculateRadius(a);
        }

        public void Draw(SolidBrush color, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.FillRectangle(color, x, y, width, height);
        }

        public async Task Update()
        {
            
                currentTime++;
                if (currentTime == explosionTime)
                {
                    Subject.Detach(this);
                    await World.Instance.Explode(x, y);
                }
            
        }
    }

}
