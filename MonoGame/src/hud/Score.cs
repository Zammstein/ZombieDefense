using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.hud {
    class Score : HUD {

        private String displayText = "";

        public Score() {
            this.position.X = 20;
            this.position.Y = 20;
            this.width = 101;
            this.height = 101;
            this.visible = true;
            this.fixedPosition = true;
        }

        public override void Update(GameTime gameTime) {
            displayText = "Score:  " + GameController.scoreTotal;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (!GameController.mainMenu) {
                spriteBatch.DrawString(GameController.spriteFont, displayText, position, Color.White);
            }
        }
    }
}
