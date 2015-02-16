using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.mob;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.effect {
    public class Fireball : Effect {

        private int damage;
        private bool dmgDealt, soundPlayed;

        public Fireball(int x, int y, int damage) {
            this.x = x;
            this.y = y;
            this.spritePath = "textures/props/fireball";
            this.soundPath = "sounds/explosion";
            this.damage = damage;
            this.frameHeight = 96;
            this.frameWidth = 96;
            this.bounds = new Rectangle(x, y, frameWidth, frameHeight);
            this.currentFrame = new Vector4(0, 0, 17, 4);
            this.counter = 0;
            this.duration = currentFrame.Z * currentFrame.W;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White);
        }

        public override void Update(GameTime gameTime) {
            if (counter >= duration) Remove();
            if (!soundPlayed && sound != null) {
                soundPlayed = true;
                sound.Play();
            }
            ArrayList entities = GameController.view.entities;
            if (!dmgDealt) { 
                for (int i = 0; i < entities.Count; i++) {
                    if (entities[i].GetType() == typeof(Guard)) {
                        Entity guard = (Entity) entities[i];
                        if (guard.GetBounds().Intersects(bounds)) guard.DoDMG(damage);
                    }
                }
                dmgDealt = true;
            }
            UpdateFrames(currentFrame);
            counter++;
        }
    }
}
