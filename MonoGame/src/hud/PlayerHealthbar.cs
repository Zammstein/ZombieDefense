using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.hud {
    public class PlayerHealthbar : HUD {
        public PlayerHealthbar() {
            this.position.X = 580;
            this.position.Y = 30;
            this.width = 101;
            this.height = 10;
            this.spritePath = "textures/props/healthbar";
            this.visible = true;
            this.fixedPosition = true;
        }

        public override void Update(GameTime gameTime) {
            this.width = GameController.view.GetPlayer().GetHealth() * 2;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (!GameController.mainMenu) {
                spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);
                spriteBatch.DrawString(GameController.spriteFont, "Health:", new Vector2(position.X + 1, position.Y - 13), Color.White, 0f, new Vector2(0, 0), new Vector2(0.7f, 0.7f), SpriteEffects.None, 0f);
            }
        }
    }
}
