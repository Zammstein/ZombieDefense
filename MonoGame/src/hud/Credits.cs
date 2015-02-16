using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using MonoGame.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombDefense.src.hud {
    class Credits : HUD {
        
        public Credits() {
            this.position.X = 690;
            this.position.Y = 460;
            this.width = 101;
            this.height = 101;
            this.visible = true;
            this.fixedPosition = true;
        }

        public override void Update(GameTime gameTime) {
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(GameController.spriteFont, "Code by Sam Meyer", position, Color.White, 0f, new Vector2(0, 0), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0f);
        }
    }
}
