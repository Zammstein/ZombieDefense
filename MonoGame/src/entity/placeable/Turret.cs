using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.effect;
using MonoGame.src.entity.mob;
using MonoGame.src.entity.projectile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.placeable {
    public class Turret :Placeable {

        private int counter = 0;
        private int dmgRate = 15;
        private int range = 500;
        private SpriteEffects flipSprite;
        private int duration = 600;
        private Boolean isTurning;
        private Vector4 facingLeft, facingRight, turnAnim;

        public Turret(int x) {
            this.position.X = x;
            this.position.Y = 710;
            this.x = x;
            this.y = 710;
            this.damage = damage;
            this.spritePath = "textures/props/turret";
            this.frameWidth = 81;
            this.frameHeight = 56;
            this.bounds = new Rectangle(x, y, frameWidth, frameHeight);
            this.facingLeft = new Vector4(0, 0, 1, 0);
            this.facingRight = new Vector4(4, 0, 1, 0);
            this.turnAnim = new Vector4(0, 0, 4, 10);
            this.currentFrame = facingLeft;
            this.isSolid = false;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, new Rectangle((int) position.X, (int) position.Y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White, 0, new Vector2(0, 0), flipSprite, 0);
            spriteBatch.DrawString(GameController.spriteFont, (duration / 60).ToString(), new Vector2(position.X + frameWidth / 3, position.Y - frameHeight), Color.DarkSeaGreen);
        }

        public override void Update(GameTime gameTime) {
            if (duration < 0) Remove();
            if (counter % dmgRate == 0) {
                ArrayList entities = GameController.view.entities;
                for (int i = 0; i < entities.Count; i++) {
                    if (entities[i].GetType() == typeof(Guard)) {
                        Entity guard = (Entity)entities[i];
                        if (guard.GetX() >= position.X - range && guard.GetX() <= position.X + range) {
                            Shoot(guard);
                            break;
                        }
                    }
                }
            }

            if (currentFrame.X == 3) {
                isTurning = false;
                if (direction == entityDirection.Left) {
                    direction = entityDirection.Right;
                    flipSprite = SpriteEffects.None;
                    currentFrame = facingRight;
                } else {
                    direction = entityDirection.Left;
                    currentFrame = facingLeft;
                    flipSprite = SpriteEffects.None;
                }
            }
            UpdateFrames(currentFrame);
            counter++;
            duration--;
        }

        private void Shoot(Entity entity) {
            if (entity.GetX() < position.X) {
                if (direction == entityDirection.Right) {
                    Turn();
                } else {
                    currentFrame = facingLeft;
                    GameController.view.entities.Add(new Bullet((int) position.X, (int) position.Y, "Left"));
                    GameController.view.entities.Add(new Weaponfire(9, 4, "Left", this));
                }
            } else {
                if (direction == entityDirection.Left) {
                    Turn();
                } else {
                    currentFrame = facingRight;
                    GameController.view.entities.Add(new Bullet((int)position.X + 10, (int)position.Y + 10, "Right"));
                    GameController.view.entities.Add(new Weaponfire(81, 4, "Right", this));
                }
            }
        }

        private void Turn() {
            if (!isTurning) {
                isTurning = true;
                if (direction == entityDirection.Left) {
                    flipSprite = SpriteEffects.None;
                } else {
                    flipSprite = SpriteEffects.FlipHorizontally;
                }
                currentFrame = turnAnim;
            } 
        }
    }
}
