using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.hud.button {
    public class ViewControls : Button {
        
        private bool selectedIsDown = true;

        public ViewControls() {
            this.position = new Vector2(300, 200);
            this.width = 200;
            this.height = 50;
            this.spritePath = "textures/props/button";
            this.isSelected = false;
            this.visible = true;
            this.fixedPosition = true;
            this.heightOrigin = height;
            this.widthOrigin = width;
            this.xOrigin = position.X;
            this.yOrigin = position.Y;
            this.text = "Controls";
            GameController.gamePaused = true;
            GameController.mainMenu = true;
        }

        public override void Update(GameTime gameTime) {
            Breathe();
            if (!selectedIsDown && isSelected) {
                if (GameController.input.IsSelectDown()) {
                    visible = false;
                    for (int i = 0; i < GameController.view.huds.Count; i++) {
                        if (GameController.view.huds[i].GetType() == typeof(Play)) {
                            Button button = (Button)GameController.view.huds[i];
                            button.SetVisibility(false);
                        }
                        if (GameController.view.huds[i].GetType() == typeof(HighScore)) {
                            HUD hud = (HUD) GameController.view.huds[i];
                            hud.SetVisibility(false);
                        }
                    }
                    GameController.view.huds.Add(new Controls());
                }
            } else if (!GameController.input.IsSelectDown()) {
                selectedIsDown = false; 
            }

            if (!isSelected) {
                visible = true;
                for (int i = 0; i < GameController.view.huds.Count; i++) {
                    if (GameController.view.huds[i].GetType() == typeof(Play)) {
                        Button button = (Button)GameController.view.huds[i];
                        button.SetVisibility(true);
                    }
                    if (GameController.view.huds[i].GetType() == typeof(HighScore)) {
                        HUD hud = (HUD) GameController.view.huds[i];
                        hud.SetVisibility(true);
                    }
                    if (GameController.view.huds[i].GetType() == typeof(Controls)) {
                        HUD hud = (HUD) GameController.view.huds[i];
                        hud.Remove();
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (sprite != null && GameController.mainMenu) {
                spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);
                spriteBatch.DrawString(GameController.spriteFont, text, new Vector2(xOrigin + widthOrigin / 2 - 35, yOrigin + heightOrigin / 2 - 13), Color.White);
            }
        }
    }
}
