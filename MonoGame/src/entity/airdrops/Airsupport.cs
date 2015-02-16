using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.hud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.airdrops {
    class Airsupport : Airdrop {

        public Airsupport(int x, int y) {
            this.x = x;
            this.y = y;
            this.frameWidth = 96;
            this.frameHeight = 160;
            this.spritePath = "textures/props/package";
            this.soundPath = "sounds/pick_up";
            this.bounds = new Rectangle(23, 112, 48, 48);
            this.additionalSpritePath = "textures/props/airsupport";
            currentImagePosition = parachuteImagePosition;
        }

        public override void Update(GameTime gameTime) {
            if (IsGrounded()) {
                currentImagePosition = crateImagePosition;
            } else {
                Fall();
            }

            if (PickedUp() && IsVisible()) {
                GameController.view.huds.Add(new Popup(x - frameWidth / 2, y + frameHeight / 2, "+1 Airsupport", Color.Yellow, 3.5f));
                GameController.view.GetPlayer().AddBombcalls(1);
                sound.Play();
                Remove();
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentImagePosition.X * frameWidth, (int)currentImagePosition.Y * frameHeight, frameWidth, frameHeight), Color.White);
            spriteBatch.Draw(additionalSprite, new Rectangle(x + 32, y + 121, 30, 30), Color.White);
        }
    }
}
