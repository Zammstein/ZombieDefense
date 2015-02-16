using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.hud {
    public class Popup : HUD {

        private Color color;
        private String text;
        private float counter = 0f;
        private float duration;
        
        public Popup(int x, int y, String text, Color color, float durationInSeconds) {
            this.position.X = x;
            this.position.Y = y;
            this.width = 1000;
            this.height = 1000;
            this.visible = true;
            this.color = color;
            this.text = text;
            this.duration = durationInSeconds * 30;
            this.fixedPosition = false;
        }

        public Popup(String sprite) {
            this.position.X = GameController.view.GetPlayer().GetX() + GameController.view.GetPlayer().GetFrameWidth() / 2 + 20;
            this.position.Y = GameController.view.GetPlayer().GetY() - 40;
            this.spritePath = "textures/props/" + sprite;
            this.width = 25;
            this.height = 25;
            this.visible = true;
            this.duration = 2 * 30;
            this.fixedPosition = false;
        }

        public override void Update(GameTime gameTime) {
            if (counter >= duration) Remove();
            if (this.spritePath == null) {
                this.position.Y -= 1;
            } else {
                this.position.X = GameController.view.GetPlayer().GetX() + 27;
                this.position.Y = GameController.view.GetPlayer().GetY() - 40;
            }
            counter++;

        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (this.spritePath == null) {
                spriteBatch.DrawString(GameController.spriteFont, text, position, color);
            } else if (this.sprite != null) {
                spriteBatch.Draw(sprite, new Rectangle((int) position.X, (int) position.Y, width, height), Color.White);
            }
        }
    }
}
