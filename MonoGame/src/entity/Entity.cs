using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity {
    public abstract class Entity {

        protected int x, y, z;
        protected int speed;
        protected int health;
        protected Texture2D additionalSprite;
        protected String additionalSpritePath;
        protected bool healthShow;
        protected String spritePath;
        protected String soundPath;
        protected Texture2D sprite;
        protected SoundEffect sound;
        protected bool isSolid;
        protected Rectangle bounds;
        protected bool removed;
        protected bool visible = true;
        protected int frameWidth, frameHeight;
        protected int animColumn, animRow;
        protected Vector4 currentFrame;
        protected int gravity = 1;
        protected entityDirection direction = entityDirection.Right;
        protected enum entityDirection {
            Left,
            Right
        }

        private int timeCount;


        public String GetDirection() {
            return direction.ToString();
        }

        public void SetDirection(String dir) {
            if (dir == "Right") {
                direction = entityDirection.Right;
            } else if (dir == "Left") {
                direction = entityDirection.Left;
            }
        }

        public Texture2D GetAdditionalSprite() {
            return additionalSprite;
        }

        public void SetAdditionalSprite(Texture2D sprite) {
            this.additionalSprite = sprite;
        }

        public string GetAdditionalSpritePath() {
            return additionalSpritePath;
        }

        public int GetX() {
            return this.x;
        }

        public void SetX(int x) {
            this.x = x;
        }

        public int GetY() {
            return this.y;
        }

        public void SetY(int y) {
            this.y = y;
        }

        public int GetZ() {
            return this.z;
        }

        public void SetZ(int z) {
            this.z = z;
        }

        public String GetSpritePath() {
            return this.spritePath;
        }

        public void SetSpritePath(String spritePath) {
            this.spritePath = spritePath;
        }

        public int GetHealth() {
            return health;
        }

        public void SetHealth(int health) {
            this.health = health;
        }

        public virtual void Update(GameTime gameTime) {
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
        }

        protected virtual void LoadContent() { }

        protected virtual void Initialize() { }

        public void Remove() {
            this.removed = true;
        }

        public bool IsRemoved() {
            return removed;
        }

        public void SetVisibility(bool visable) {
            this.visible = visable;
        }

        public bool IsVisible() {
            return visible;
        }

        public void SetSprite(Texture2D sprite) {
            this.sprite = sprite;
        }

        public Texture2D GetSprite() {
            return sprite;
        }

        public int GetFrameWidth() {
            return frameWidth;
        }

        public int GetFrameHeight() {
            return frameHeight;
        }

        public int GetCurrentFrameX() {
            return (int) currentFrame.X;
        }

        public int GetCurrentFrameY() {
            return (int) currentFrame.Y;
        }

        public bool IsSolid() {
            return isSolid;
        }

        public void SetSolid(bool solid) {
            this.isSolid = solid;
        }

        public Rectangle GetBounds() {
            return bounds;
        }

        public void DoDMG(int amount) {
            this.health -= amount;  
        }

        public void IncreaseHealth(int amount) {
            if (health + amount > 100) {
                health = 100;
            } else {
                health += amount;
            } 
        }

        public void Move(double xa, double ya) {
            if (!CheckCollision(xa, ya)) {
                x += (int)xa;
                y += (int)ya;
            }
        }

        protected void Fall() {
            if (!IsGrounded()) {
                for (int i = 0; i < GameController.gravity; i++) {
                    Move(0, 1);
                }
            }
        }

        protected bool IsGrounded() {
            if (CheckCollision(0, 1)) {
                return true;
            } else {
                return false;
            }
        }

        protected void UpdateFrames(Vector4 frames) {
            timeCount++;
            if (timeCount % currentFrame.W == 0) {
                if (currentFrame.X == currentFrame.Z - 1) {
                    currentFrame.X = 0;
                } else {
                    currentFrame.X++;
                }
            }
        }

        protected bool CheckCollision(double xa, double ya) {
            bool solid = false;
            Entity movingEntity = this;
            movingEntity.bounds = new Rectangle(movingEntity.GetX() + (int)xa, movingEntity.GetY() + (int)ya, movingEntity.GetFrameWidth(), movingEntity.GetFrameHeight());
            foreach (Entity entity in GameController.view.GetEntities()) {    
                if (entity != null && entity.GetBounds().Intersects(movingEntity.GetBounds()) && entity.GetType() != this.GetType() && entity.isSolid) {
                    solid = true;
                }   
            }

            if (!GameController.view.GetLevelBounds().Contains(movingEntity.GetX() + movingEntity.GetFrameWidth() + (int)xa, movingEntity.GetY() + (int)ya) || !GameController.view.GetLevelBounds().Contains(movingEntity.GetX() + (int)xa, movingEntity.GetY() + (int)ya)) {
                if (movingEntity.GetType() != typeof(Guard)) solid = true;
            } 

            return solid;
        }

        protected void ShowHealth() { 
        }

        public String GetSoundPath() {
            return soundPath;
        }

        public SoundEffect GetSound() {
            return sound;
        }

        public void SetSound(SoundEffect sound) {
            this.sound = sound;
        }
    }
}
