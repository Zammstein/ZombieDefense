using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.hud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.airdrops {
    class Healthpack : Airdrop {

        private int healthBoost;

        public Healthpack(int x, int y, int health) {
            this.x = x;
            this.y = y;
            this.healthBoost = health;
            this.frameWidth = 96;
            this.frameHeight = 160;
            this.spritePath = "textures/props/package";
            this.bounds = new Rectangle(23, 112, 48, 48);
            this.additionalSpritePath = "textures/props/redcross";
            this.soundPath = "sounds/power_up";
            currentImagePosition = parachuteImagePosition;
        }

        public override void Update(GameTime gameTime) {
            if (IsGrounded()) {
                currentImagePosition = crateImagePosition;
            } else {
                Fall();
            }

            if (PickedUp()) {
                GameController.view.GetPlayer().IncreaseHealth(healthBoost);
                GameController.view.huds.Add(new Popup(x - frameWidth / 2, y + frameHeight / 2, "+" + healthBoost + " Health", Color.IndianRed, 3.5f));
                sound.Play();
                Remove();
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentImagePosition.X * frameWidth, (int)currentImagePosition.Y * frameHeight, frameWidth, frameHeight), Color.White);
            if (additionalSprite != null) spriteBatch.Draw(additionalSprite, new Rectangle(x + 34, y + 125, 23, 23), Color.White);
        }
    }
}
