using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.mob {
    public class Bomber : Mob {
        
        private int xMark;
        private Random random = new Random();
        private string directionString;
        private bool bombThrown;
        private SoundEffectInstance soundInstance;

        public Bomber(int xMark) {
            if (random.Next(2) == 1) {
                x = 3000;
                direction = entityDirection.Left;
                directionString = "Left";
            } else {
                x = -1000;
                direction = entityDirection.Right;
                directionString = "Right";
            }
            this.y = 300 + random.Next(100);
            this.xMark = xMark;
            this.spritePath = "textures/characters/bomber";
            this.soundPath = "sounds/plane";
            this.speed = 3 + random.Next(2);
            this.frameHeight = 126;
            this.frameWidth = 393;
            this.bounds = new Rectangle(x, y, frameWidth, frameHeight);
            this.currentFrame = new Vector4(0, 0, 1, 0);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (direction == entityDirection.Left) {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White);
            } else {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
        }

        public override void Update(GameTime gameTime) {
            bounds = new Rectangle(x, y, frameWidth, frameHeight);
            if (x < -1000 || x > 3000) {
                soundInstance.Stop();
                Remove();
            }
            if (this.direction == entityDirection.Right) {
                x += speed;
            } else {
                x -= speed;
            }
            CheckXmark();
            PlaySound();
        }

        private void CheckXmark() {
            Vector2 planeCentre = new Vector2(x + frameWidth / 2, y + frameHeight / 2);
            if (planeCentre.X <= xMark + speed * 2 && planeCentre.X >= xMark - speed * 2 && !bombThrown) {
                GameController.view.entities.Add(new Bomb((int) planeCentre.X, (int) planeCentre.Y, directionString));
                bombThrown = true;
            }
        }

        private void PlaySound() {
            if (sound != null) {
                if (soundInstance == null) {
                    soundInstance = sound.CreateInstance();
                    soundInstance.IsLooped = false;
                    soundInstance.Volume = 0.1f;
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
