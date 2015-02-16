using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.effect {
    public class Blood : Effect {
        public Blood(int x, int y, String leftOrRight) {
            this.x = x;
            this.y = y;
            this.spritePath = "textures/props/blood";
            this.frameHeight = 15;
            this.frameWidth = 16;
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
            if (counter >= duration) Remove();
            UpdateFrames(currentFrame);
            counter++;
        }
    }
}
