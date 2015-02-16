using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.block {
    class GrassBlock : Block {
        public GrassBlock(int x, int y, int z, String image) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.isSolid = true;
            this.frameHeight = 60;
            this.frameWidth = 90;
            this.bounds = new Rectangle(x, y + 17, frameWidth, frameHeight + 17);
            this.currentFrame = new Vector4(0, 0, 1, 0);
            this.spritePath = "textures/blocks/" + image;
            this.color = new Color();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (direction == entityDirection.Right) {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            } else {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White);
            }
        }
    }
}
