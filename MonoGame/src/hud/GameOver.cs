using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity;
using MonoGame.src.entity.block;
using MonoGame.src.hud.button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.hud {
    public class GameOver : HUD {

        private String displayText = "Game over";
        private String instructions = "Press space or A to continue";

        public GameOver() {
            this.position.X = 310;
            this.position.Y = 100;
            this.width = 101;
            this.height = 101;
            this.visible = true;
            this.fixedPosition = true;
            GameController.gameOver = true;
            ClearLevel();
        }

        private void ClearLevel() {
            for (int i = 0; i < GameController.view.GetEntities().Count; i++) {
                Entity entity = (Entity) GameController.view.GetEntities()[i];
                if (entity.GetType() != typeof(GrassBlock)) {
                    entity.Remove();
                }
            }
        }

        public override void Update(GameTime gameTime) {
            if (GameController.input.IsSelectDown()) {
                if (GameController.highScore < GameController.scoreTotal) {
                    GameController.highScore = GameController.scoreTotal;
                    GameController.contentManager.InitiateSave();
                }
                GameController.view.huds.Add(new Play());
                GameController.gameOver = false;
                Remove();
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (!GameController.mainMenu) {
                spriteBatch.DrawString(GameController.spriteFont, displayText, position, Color.DarkRed, 0f, new Vector2(0, 0), new Vector2(1.6f, 1.6f), SpriteEffects.None, 0f);
                spriteBatch.DrawString(GameController.spriteFont, instructions, new Vector2(240, 400), Color.DarkRed, 0f, new Vector2(0, 0), new Vector2(1.1f, 1.1f), SpriteEffects.None, 0f);
            }
        }
    }
}
