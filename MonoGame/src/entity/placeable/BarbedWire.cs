using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.mob;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.placeable {
    public class BarbedWire :Placeable {

        private int counter = 0;
        private int dmgRate = 60;

        public BarbedWire(int x, int y, int health, int damage) {
            this.position.X = x;
            this.position.Y = y + 20;
            this.health = health;
            this.damage = damage;
            this.spritePath = "textures/props/barbwire_fence";
            this.frameWidth = 89;
            this.frameHeight = 46;
            this.bounds = new Rectangle(x, y, frameWidth, frameHeight);
            this.currentFrame = new Vector4(0, 0, 1, 0);
            this.isSolid = false;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle((int) position.X, (int) position.Y, frameWidth, frameHeight), Color.White);
        }

        public override void Update(GameTime gameTime) {
            ArrayList entities = GameController.view.entities;
            for (int i = 0; i < entities.Count; i++) {
                if (entities[i].GetType() == typeof(Guard)) {
                    Entity guard = (Entity)entities[i];
                    if (guard.GetBounds().Intersects(bounds) && counter % dmgRate == 0) {
                        guard.DoDMG(damage);
                        this.DoDMG(1);
                        if (health <= 0) Remove();
                    }
                }
            }
            counter++;
        }
    }
}
