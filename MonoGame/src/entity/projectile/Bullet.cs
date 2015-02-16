using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.effect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.projectile {
    class Bullet : Projectile {

        private String dir;
        private bool soundHasPlayed;

        public Bullet(int x, int y, String leftOrRight) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.spritePath = "textures/props/bullet";
            this.soundPath = "sounds/bullet";
            this.speed = 12;
            this.damage = 20;
            this.frameHeight = 3;
            this.frameWidth = 6;
            dir = leftOrRight;
            this.bounds = new Rectangle(x, y, frameWidth, frameHeight);
            if (leftOrRight == "Right") {
                this.direction = entityDirection.Right;
            } else {
                this.direction = entityDirection.Left;              
            }
            this.currentFrame = new Vector4(0, 0, 1, 0);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), Color.White);
        }

        public override void Update(GameTime gameTime) {
            int xa = 0, ya = 0;
            CheckHit();
            if (!soundHasPlayed && this.sound != null) {
                sound.Play(0.1f, 0.5f, 0f);
                soundHasPlayed = true;
            }
            if (direction == entityDirection.Right) {
                if (IsHit()) GameController.view.entities.Add(new Blood(x + 33, y, dir));
                if (x >= GameController.view.GetLevelBounds().X + GameController.view.levelBounds.Right - 30) Remove();
                Move(xa += speed, 0);
            } else {
                if (IsHit()) GameController.view.entities.Add(new Blood(x - 33, y, dir));
                if (x <= GameController.view.GetLevelBounds().X + 10) Remove();
                Move(xa -= speed, 0);
            }
        }
    }
}
