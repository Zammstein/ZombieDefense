using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.hud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.airdrops {
    class Fireratepack : Airdrop {
        
        private int multiplier;
        private float durationInSeconds;
        private int counter = 0;

        public Fireratepack(int x, int y, int multiplier, float durationInSeconds) {
            this.x = x;
            this.y = y;
            this.multiplier = multiplier;
            this.durationInSeconds = durationInSeconds * 60;
            this.frameWidth = 96;
            this.frameHeight = 160;
            this.spritePath = "textures/props/package";
            this.soundPath = "sounds/power_up";
            this.bounds = new Rectangle(23, 112, 48, 48);
            this.additionalSpritePath = "textures/props/ammo";
            currentImagePosition = parachuteImagePosition;
        }

        public override void Update(GameTime gameTime) {
            if (IsGrounded()) {
                currentImagePosition = crateImagePosition;
            } else {
                Fall();
            }

            if (PickedUp() && IsVisible()) {
                GameController.view.GetPlayer().SetFireRate(10 / multiplier);
                GameController.view.huds.Add(new Popup(x - frameWidth / 2, y + frameHeight / 2, "x" + multiplier + " Firerate", Color.Black, 3.5f));
                sound.Play();
                SetVisibility(false);
            }

            if (!IsVisible()) {
                if (counter >= durationInSeconds) {
                    GameController.view.GetPlayer().SetFireRate(10);
                    Remove();
                }
                    counter++;
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentImagePosition.X * frameWidth, (int)currentImagePosition.Y * frameHeight, frameWidth, frameHeight), Color.White);
            spriteBatch.Draw(additionalSprite, new Rectangle(x + 33, y + 123, 25, 25), Color.White);
        }
    }
}
