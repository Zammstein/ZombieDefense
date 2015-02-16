using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.effect;
using MonoGame.src.entity.mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.projectile {
    public class Bomb : Projectile {

        private float angle = 0f;
        private SoundEffectInstance soundInstance;

        public Bomb(int x, int y, String leftOrRight) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.spritePath = "textures/props/bomb";
            this.soundPath = "sounds/bomb_falling";
            this.damage = 20;
            this.frameHeight = 34;
            this.frameWidth = 93;
            if (leftOrRight == "Right") {
                this.direction = entityDirection.Right;
            } else {
                this.direction = entityDirection.Left;
            }
            this.bounds = new Rectangle(x, y, frameWidth, frameHeight);
            this.currentFrame = new Vector4(0, 0, 1, 0);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (direction == entityDirection.Left) {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle(0, 0, frameWidth, frameHeight), Color.White, angle, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            } else {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle(0, 0, frameWidth, frameHeight), Color.White, -angle, new Vector2(0, 0), SpriteEffects.None, 0);
            }
        }

        public override void Update(GameTime gameTime) {
            angle += 0.015f;
            PlaySound();
            if (y - frameHeight / 2 >= 720) {
                GameController.view.entities.Add(new Fireball(x, y - frameHeight, 150));
                soundInstance.Stop();
                Remove();
            }
            y += (int) GameController.gravity * 2;
        }

        private void PlaySound() {
            if (sound != null) {
                if (soundInstance == null) {
                    soundInstance = sound.CreateInstance();
                    soundInstance.IsLooped = false;
                    soundInstance.Volume = 1.0f;
                    soundInstance.Play();
                } else {
                    Player player = GameController.view.GetPlayer();
                    if (player.GetX() + player.GetFrameWidth() / 2 < x + frameWidth / 2) {
                        soundInstance.Pan = -1.0f;
                    } else {
                        soundInstance.Pan = 1.0f;
                    }
                }
            }
        }
    }
}
