using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.src.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.hud.button {
    public class Play : Button {

        private bool selectedIsDown = true;

        public Play() {
            this.position = new Vector2(300, 300);
            this.width = 200;
            this.height = 50;
            this.spritePath = "textures/props/button";
            this.isSelected = true;
            this.visible = true;
            this.fixedPosition = true;
            this.heightOrigin = height;
            this.widthOrigin = width;
            this.xOrigin = position.X;
            this.yOrigin = position.Y;
            this.text = "Play";
            GameController.gamePaused = true;
            GameController.mainMenu = true;
        }

        public override void Update(GameTime gameTime) {
            Breathe();
            if (!selectedIsDown && isSelected) {
                if (GameController.input.IsSelectDown()) {
                    GameController.NewGame();
                    Remove();
                }
            } else if (!GameController.input.IsSelectDown()) {
                selectedIsDown = false; 
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (sprite != null) {
                spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);
                spriteBatch.DrawString(GameController.spriteFont, text, new Vector2(xOrigin + widthOrigin / 2 - 20, yOrigin + heightOrigin / 2 - 10), Color.White);
            }
        }
    }
}
