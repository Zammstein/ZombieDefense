using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.effect {
    public class Weaponfire : Effect {

        private Entity owner;
        private int xAdjustment, yAdjustment;

        public Weaponfire(int xAdjustment, int yAdjustment, String leftOrRight, Entity owner) {
            this.x = 0;
            this.y = 0;
            this.xAdjustment = xAdjustment;
            this.yAdjustment = yAdjustment;
            this.owner = owner;
            this.spritePath = "textures/props/rifle_fire";
            this.frameHeight = 13;
            this.frameWidth = 14;
            this.bounds = new Rectangle(x, y, frameWidth, frameHeight);
            this.counter = 0;
            this.duration = 8f;
            if (leftOrRight == "Right") {
                this.direction = entityDirection.Right;
            } else {
                this.direction = entityDirection.Left;
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (direction == entityDirection.Right) {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), Color.White);
            } else {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle(0, 0, frameWidth, frameHeight), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
        }

        public override void Update(GameTime gameTime) {
            if (direction == entityDirection.Left) {
                x = owner.GetX() - xAdjustment;
            } else {
                x = owner.GetX() + xAdjustment;
            }
            y = owner.GetY() + yAdjustment;
            
            if (counter >= duration) Remove();
            UpdateFrames(currentFrame);
            counter++;
        }
    }
}
