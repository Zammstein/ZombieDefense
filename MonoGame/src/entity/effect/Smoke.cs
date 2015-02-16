using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.effect {
    public class Smoke : Effect {
        public Smoke(int x, int y) {
            this.x = x;
            this.y = y;
            this.spritePath = "textures/props/smoke";
            this.frameHeight = 128;
            this.frameWidth = 128;
            this.bounds = new Rectangle(x, y, frameWidth, frameHeight);
            this.currentFrame = new Vector4(0, 0, 40, 5);
            this.counter = 0;
            this.duration = currentFrame.Z * currentFrame.W;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White);
        }

        public override void Update(GameTime gameTime) {
            y--;
            if (counter >= duration) Remove();
            UpdateFrames(currentFrame);
            counter++;
        }
    }
}
