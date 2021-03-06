﻿using BombermanClasses.Composite;
using BombermanClasses.Items;
using BombermanClasses.Memento;
using BombermanClasses.TemplateMethod;
using System.Drawing;

namespace BombermanClasses
{
    public class Player : DestructionTemplate, IPlayer
    {
        public string Id { get; set; }
        public int x { get; set; } 
        public int y { get; set; }
        public Item item { get; set; } = null;
        public int RoundsPlayed { get; set; } = 1;

        public bool isDead { get; set; } = false;

        private Originator originator = new Originator();
        private CareTaker careTaker = new CareTaker();
        public CompositeDirectory destroyedEntities { get; set; }

        public Player()
        {
            
        }

        public Player(string id, int x, int y)
        {
            Id = id;
            this.x = x;
            this.y = y;
            this.destroyedEntities = new CompositeDirectory();
        }

        public override void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
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

        public void SaveItem()
        {
            originator.SetState(item);
            careTaker.Add(originator.SaveStateToMemento());
        }

        public void SaveItem(Item i)
        {
            originator.SetState(i);
            careTaker.Add(originator.SaveStateToMemento());
        }

        public void ReturnItem()
        {
            originator.GetStateFromMemento(careTaker.Get());
            item = originator.GetState();
        }

        public override int calculateScore(int score)
        {
            return base.calculateScore(score + 300);
        }

        protected sealed override void setIsDead()
        {
            careTaker = new CareTaker();        //delete saved items after death
            isDead = true;
            base.setIsDead();
        }
    }
}
