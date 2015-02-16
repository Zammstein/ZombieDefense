using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.hud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.mob {
    class Guard : Mob{

        private int fireCounter = 0;
        private Random random = new Random();

        public Guard(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.spritePath = "textures/characters/zombie";
            this.additionalSpritePath = "textures/props/healthbar";
            this.soundPath = "sounds/eating";
            this.speed = 2;
            this.frameHeight = 69;
            this.frameWidth = 74;
            this.bounds = new Rectangle(x, y, frameWidth / 2, frameHeight);
            this.walking = new Vector4(0 ,0, 7, 13 - (int) Math.Pow((int)speed,2));
            this.attack = new Vector4(0, 1, 4, 13 - (int)Math.Pow((int)speed, 2));
            this.currentFrame = walking;
            this.direction = entityDirection.Right;
            this.health = 100;
            this.attackDamage = 5;
            this.fireRate = 30;
            this.state = mobState.Attack;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (direction == entityDirection.Right) {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            } else {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White);
            }

            if (healthShow && additionalSprite != null) {
                spriteBatch.Draw(additionalSprite, new Rectangle(x, y, health, 6), Color.White);
            }
        }

        public override void Update(GameTime gameTime) {
            bounds = new Rectangle(x, y, frameWidth, frameHeight);
            int xa = 0, ya = 0;
            Player player = GameController.view.GetPlayer();
            Random random = new Random();

            if (health < 100) healthShow = true;
            if (health <= 0) {
                GameController.view.huds.Add(new Popup(x + frameWidth / 2, y, "+100", Color.White, 2f));
                GameController.scoreTotal += 100;
                GameController.PlayEnemyDeath();
                Remove();
            }

            if (bounds.X > player.GetBounds().X - random.Next(10) && !bounds.Intersects(player.GetBounds())) {
                this.direction = entityDirection.Left;
                Move(xa -= speed, 0);
                if (state != mobState.Fall || state != mobState.Jump) {
                    if (this.state != mobState.Walk) {
                        currentFrame = walking;
                        state = mobState.Walk;
                    }
                }
            }

            if (bounds.X < player.GetBounds().X - random.Next(10) && !bounds.Intersects(player.GetBounds())) {
                this.direction = entityDirection.Right;
                Move(xa += speed, 0);
                if (state != mobState.Fall || state != mobState.Jump) {
                    if (this.state != mobState.Walk) {
                        currentFrame = walking;
                        state = mobState.Walk;
                    }
                }
            }

            if (bounds.Intersects(player.GetBounds())) {
                if (state != mobState.Attack) {
                    this.state = mobState.Attack;
                    currentFrame = attack;
                }
            }

            Fall();
            UpdateFrames(currentFrame);
            UpdateAttack(gameTime);
        }

        private void UpdateAttack(GameTime gameTime) {
            if (state == mobState.Attack) {
                if (fireCounter % fireRate == 0 && fireCounter != 0) fireOnCooldown = false;
                fireCounter++;
                if (!fireOnCooldown) {
                    GameController.view.GetPlayer().DoDMG(attackDamage);
                    this.sound.Play();
                    fireOnCooldown = true;
                    fireCounter = 0;
                }
            }
        }
    }
}
