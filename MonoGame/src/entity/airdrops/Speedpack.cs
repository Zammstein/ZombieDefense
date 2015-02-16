using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.hud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.airdrops {
    class Speedpack : Airdrop{

        private int speedBoost;
        private float durationInSeconds;
        private float counter;
        private int originalSpeed;

        public Speedpack(int x, int y, int speedBoost, float durationInSeconds) {
            this.x = x;
            this.y = y;
            this.speedBoost = speedBoost;
            this.durationInSeconds = durationInSeconds * 60;
            this.frameWidth = 96;
            this.frameHeight = 160;
            this.spritePath = "textures/props/package";
            this.soundPath = "sounds/power_up";
            this.bounds = new Rectangle(23, 112, 48, 48);
            this.additionalSpritePath = "textures/props/speed";
            currentImagePosition = parachuteImagePosition;
        }

        public override void Update(GameTime gameTime) {
            if (IsGrounded()) {
                currentImagePosition = crateImagePosition;
            } else {
                Fall();
            }

            if (PickedUp() && IsVisible()) {
                originalSpeed = GameController.view.GetPlayer().GetSpeed();
                GameController.view.GetPlayer().SetSpeed(speedBoost);
                GameController.view.huds.Add(new Popup(x - frameWidth / 2, y + frameHeight / 2, "+" + speedBoost + " Speed", Color.Yellow, 3.5f));
                sound.Play();
                SetVisibility(false);
            }

            if (!IsVisible()) {
                if (counter >= durationInSeconds) {
                    GameController.view.GetPlayer().SetSpeed(originalSpeed);
                    Remove();    
                }
                counter++;
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentImagePosition.X * frameWidth, (int)currentImagePosition.Y * frameHeight, frameWidth, frameHeight), Color.White);
            spriteBatch.Draw(additionalSprite, new Rectangle(x + 34, y + 125, 23, 23), Color.White);
        }
    }
}
