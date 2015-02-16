using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.hud {
    public class Controls : HUD {
        private String displayText = "";

        public Controls() {
            this.spritePath = "textures/props/controls";
            this.position.X = 0;
            this.position.Y = 10;
            this.width = 780;
            this.height = 400;
            this.visible = true;
            this.fixedPosition = true;
        }

        public override void Update(GameTime gameTime) {
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (GameController.mainMenu && sprite != null) {
                spriteBatch.Draw(sprite, new Rectangle((int) position.X, (int) position.Y, width, height), Color.White);
                spriteBatch.DrawString(GameController.spriteFont, "Press down to return", new Vector2(4, 450), Color.White);
            }
        }
    }
}
