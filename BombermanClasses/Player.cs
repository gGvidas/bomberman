﻿using BombermanClasses.Items;
using System.Drawing;

namespace BombermanClasses
{
    public class Player : IMapObject, IPlayer
    {
        public string Id { get; set; }
        public int x { get; set; } 
        public int y { get; set; }
        public Item item { get; set; } = null;

        public bool isDead { get; set; } = false;

        public Player(string id, int x, int y)
        {
            Id = id;
            this.x = x;
            this.y = y;
        }

        public void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.DrawImage(image, x, y, width, height);
        }

        public void moveUp()
        {
            y--;
        }
        public void moveDown()
        {
            y++;
        }
        public void moveRight()
        {
            x++;
        }
        public void moveLeft()
        {
            x--;
        }
    }
}
