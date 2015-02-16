using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.effect {
    public class Cloud : Effect {

        private Random random = new Random();
        private int speedCounter;

        public Cloud() {
            this.spritePath = "textures/props/cloud";
            this.frameHeight = 169;
            this.frameWidth = 330;
            this.speed = random.Next(5) + 1;
            this.y = random.Next(100) + 300;
            if (random.Next(2) == 0) {
                this.x = 2000;
                this.direction = entityDirection.Left;
            } else {
                this.x = -frameWidth;
                this.direction = entityDirection.Right;
            }
            this.bounds = new Rectangle(x, y, frameWidth, frameHeight);
            this.counter = 0;
            int shape = random.Next(3);
            if (shape == 0) {
                currentFrame = new Vector4(1, 0, 1, 0);
            } else if (shape == 1) {
                currentFrame = new Vector4(2, 0, 1, 0);
            } else {
                currentFrame = new Vector4(0, 0, 1, 0);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White);
        }

        public override void Update(GameTime gameTime) {
            if (direction == entityDirection.Left) {
                if (speedCounter % speed == 0) x--;
                if (x < -frameWidth) Remove();
            } else {
                if (speedCounter % speed == 0) x++;
                if (x > 2000) Remove();
            }
            speedCounter++;
        }
    }
}
