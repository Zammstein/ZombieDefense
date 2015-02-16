using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.src.entity.effect;
using MonoGame.src.entity.placeable;
using MonoGame.src.entity.projectile;
using MonoGame.src.hud;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.mob {
    public class Player : Mob {

        public int bombCalls = 0;
        public int barbWires = 0;
        public int turrets = 0;
        public placeables selectedPlaceable = placeables.barbedWire;
        public placeables airsupport = placeables.airsupport;
        public placeables barbedWire = placeables.barbedWire;
        public placeables turretIcon = placeables.turretIcon;
        public enum placeables {
            barbedWire,
            airsupport,
            turretIcon
        };

        private Popup icon;
        private int listPosition = 1;
        private ArrayList placeableList;
        private bool isJumping;
        private int jumpCounter = 0;
        private int fireCounter = 0;
        private int placeCounter = 0;
        private bool keyDown = false;
        private bool keyUp = true;

        public Player(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.spritePath = "textures/characters/armyguy";
            this.speed = 3;
            this.frameHeight = 68;
            this.frameWidth = 83;
            this.bounds = new Rectangle(x + 10, y, frameWidth - 20, frameHeight);
            this.walking = new Vector4(0 ,0, 4, 15 - (int) Math.Pow((int)speed,2));
            this.crouching = new Vector4(0, 10, 2, 20);
            this.shoot = new Vector4(0, 2, 2, 20);
            this.idle = new Vector4(0, 1, 3, 20);
            this.currentFrame = idle;
            this.direction = entityDirection.Right;
            this.fireRate = 10;
            this.health = 100;
            placeableList = new ArrayList();
            placeableList.Add(airsupport);
            placeableList.Add(barbedWire);
            placeableList.Add(turretIcon);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (direction == entityDirection.Right) {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            } else {
                spriteBatch.Draw(sprite, new Rectangle(x, y, frameWidth, frameHeight), new Rectangle((int)currentFrame.X * frameWidth, (int)currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White);
            }
        }

        public override void Update(GameTime gameTime) {
            KeyboardState keyState = Keyboard.GetState();
            bounds = new Rectangle(x, y, frameWidth, frameHeight);
            int xa = 0, ya = 0;
            bool keyPressed = false;

            if (health <= 0) {
                Remove();
                for (int i = 0; i < GameController.view.huds.Count; i++) {
                    if (GameController.view.huds[i].GetType() == typeof(Wave) || GameController.view.huds[i].GetType() == typeof(PlayerHealthbar)) {
                        HUD hud = (HUD) GameController.view.huds[i];
                        hud.SetVisibility(false);
                    } else if (GameController.view.huds[i].GetType() == typeof(Score)) {
                        HUD hud = (HUD)GameController.view.huds[i];
                        hud.SetPosition(new Vector2(350, 200));
                    }
                }
                GameController.view.huds.Add(new GameOver());
            }

            if (GameController.input.IsLeftMove()) {
                keyPressed = true;
                if (GameController.input.IsRightShoot()) {
                    this.direction = entityDirection.Right;
                } else {
                    this.direction = entityDirection.Left;
                }
                Move(xa -= speed, 0);
                if (state != mobState.Fall || state != mobState.Jump) {
                    if (this.state != mobState.Walk) {
                        currentFrame = walking;
                        state = mobState.Walk;
                    }
                }
            }

            if (GameController.input.IsRightMove()) {
                keyPressed = true;
                if (GameController.input.IsLeftShoot()) {
                    this.direction = entityDirection.Left;
                } else {
                    this.direction = entityDirection.Right;
                }
                Move(xa += speed, 0);
                if (state != mobState.Fall || state != mobState.Jump) {
                    if (this.state != mobState.Walk) {
                        currentFrame = walking;
                        state = mobState.Walk;
                    }
                }
            }

            if (isJumping && state != mobState.Fall) {
                if (jumpCounter != 20) {
                    state = mobState.Jump;
                    for (int i = 0; i < ((int)Math.Sqrt(jumpCounter)); i++) {
                        Move(0, -i);
                    }
                    jumpCounter++;
                } else {
                    jumpCounter = 0;
                    isJumping = false;
                }
            } 
            if (!keyPressed && state != mobState.Fall && state != mobState.Jump) {
                if (this.state != mobState.Idle && currentFrame != idle && this.state != mobState.Attack) {
                    this.state = mobState.Idle;
                    currentFrame = idle;
                }
            }
            UpdateShooting(gameTime);
            Fall();
            UpdateFrames(currentFrame);
            UpdatePlaceables();
        }

        private void UpdatePlaceables() {
            if ((GameController.input.IsNextWeapon() || GameController.input.IsPrevWeapon()) && !keyDown) {
                keyDown = true;
                if (keyUp) {
                    if (GameController.input.IsNextWeapon()) {
                        if (listPosition == placeableList.Count - 1) {
                            listPosition = 0;
                        } else {
                            listPosition++;
                        }
                    } else {
                        if (listPosition == 0) {
                            listPosition = placeableList.Count - 1;
                        } else {
                            listPosition--;
                        }
                    }
                    if (GameController.view.huds.Contains(icon)) icon.Remove();
                    selectedPlaceable = (placeables) placeableList[listPosition];
                    icon = new Popup(selectedPlaceable.ToString());
                    GameController.view.huds.Add(icon);
                    if (selectedPlaceable == placeables.airsupport) {
                        GameController.view.huds.Add(new Popup(x + frameWidth / 3 + 5, y - frameHeight, bombCalls.ToString(), Color.Yellow, 1f));
                    } else if (selectedPlaceable == placeables.barbedWire) {
                        GameController.view.huds.Add(new Popup(x + frameWidth / 3 + 5, y - frameHeight, barbWires.ToString(), Color.Yellow, 1f));
                    } else {
                        GameController.view.huds.Add(new Popup(x + frameWidth / 3 + 5, y - frameHeight, turrets.ToString(), Color.Yellow, 1f));
                    }
                    keyUp = false;
                }
            }

            if (!GameController.input.IsNextWeapon() && !GameController.input.IsPrevWeapon()) {
                keyUp = true;
                keyDown = false;
            }

            if (GameController.input.IsSelectDown() && placeCounter >= 60 && bombCalls != 0 && selectedPlaceable == placeables.airsupport) {
                placeCounter = 0;
                bombCalls--;
                GameController.view.entities.Add(new Bombmark(x));
            }

            if (GameController.input.IsSelectDown() && placeCounter >= 60 && turrets != 0 && selectedPlaceable == placeables.turretIcon) {
                placeCounter = 0;
                turrets--;
                GameController.view.entities.Add(new Turret(x));
            }

            if (GameController.input.IsSelectDown() && placeCounter >= 60 && barbWires != 0 && selectedPlaceable == placeables.barbedWire) {
                placeCounter = 0;
                barbWires--;
                ArrayList entities = GameController.view.entities;
                bool canPlace = true;
                for (int i = 0; i < entities.Count; i++) {
                    Entity entity = (Entity) entities[i];
                    if (entity.GetType() == typeof(BarbedWire)) {
                        if (entity.GetBounds().Intersects(bounds)) canPlace = false; ;
                    }
                }
                if (canPlace) GameController.view.entities.Add(new BarbedWire(x, y, 100, 5)); 
            }
            placeCounter++;
        }

        private void UpdateShooting(GameTime gameTime) {
            if (fireCounter % fireRate == 0 && fireCounter != 0) fireOnCooldown = false;
            fireCounter++;
            if (!fireOnCooldown) {
                if (GameController.input.IsRightShoot()) {
                    GameController.view.entities.Add(new Bullet(x + 40, y + 37, "Right"));
                    GameController.view.entities.Add(new Weaponfire(76, 29, "Right", this));
                    fireOnCooldown = true;
                    fireCounter = 0;
                    this.currentFrame = shoot;
                    this.state = mobState.Attack;
                    this.direction = entityDirection.Right;
                } else if (GameController.input.IsLeftShoot()) {
                    GameController.view.entities.Add(new Bullet(x + 15, y + 37, "Left"));
                    GameController.view.entities.Add(new Weaponfire(7, 29, "Left", this));
                    fireOnCooldown = true;
                    fireCounter = 0;
                    this.currentFrame = shoot;
                    this.state = mobState.Attack;
                    this.direction = entityDirection.Left;
                }
            }
        }

        public int GetBombcalls() {
            return bombCalls;
        }

        public void AddBombcalls(int amount) {
            bombCalls += amount;
        }

        public void UseBombcall(int amount) {
            bombCalls -= amount;
        }

        public int GetBarbwire() {
            return barbWires;
        }

        public void AddBarbwire(int amount) {
            barbWires += amount;
        }

        public void UseBarbwire(int amount) {
            barbWires -= amount;
        }

        public int GetTurrets() {
            return turrets;
        }

        public void AddTurrets(int amount) {
            turrets += amount;
        }

        public void UseTurrets(int amount) {
            turrets -= amount;
        }
    }
}
