using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.hud {
    public class HighScore : HUD {

        public HighScore() {
            this.position.X = 320;
            this.position.Y = 80;
            this.width = 101;
            this.height = 101;
            this.visible = true;
            this.fixedPosition = true;
        }

        public override void Update(GameTime gameTime) {
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (GameController.mainMenu) {
                spriteBatch.DrawString(GameController.spriteFont, "Highscore:", position, Color.White, 0f, new Vector2(0, 0), new Vector2(1.5f, 1.5f), SpriteEffects.None, 0f);
                spriteBatch.DrawString(GameController.spriteFont, GameController.highScore.ToString(), new Vector2(380, 140), Color.White, 0f, new Vector2(0, 0), new Vector2(1.3f, 1.3f), SpriteEffects.None, 0f);
            }
        }
    }
}
