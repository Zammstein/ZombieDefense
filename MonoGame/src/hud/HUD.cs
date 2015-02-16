using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src {
    public abstract class HUD {

        protected Texture2D sprite;
        protected String spritePath;
        protected Vector2 position;
        protected int width, height;
        protected bool removed;
        protected bool visible;
        protected bool fixedPosition;

        public virtual void Update(GameTime gameTime) {
        }

        public virtual void Draw(SpriteBatch spriteBatch) { 
        }

        public Texture2D GetSprite() {
            return sprite;
        }

        public void SetSprite(Texture2D sprite) {
            this.sprite = sprite;
        }

        public String GetSpritePath() {
            return spritePath;
        }

        public void SetSpritePath(String path) {
            this.spritePath = path;
        }

        public Vector2 GetPosition() {
            return position;
        }

        public void SetPosition(Vector2 position) {
            this.position = position;
        }

        public int GetWidth() {
            return width;
        }

        public void SetWidth(int width) {
            this.width = width;
        }

        public int GetHeight() {
            return height;
        }

        public void SetHeight(int height) {
            this.height = height;
        }

        public bool IsRemoved() {
            return removed;
        }

        public void Remove() {
            this.removed = true;
        }

        public bool IsVisible() {
            return visible;
        }

        public void SetVisibility(bool visible) {
            this.visible = visible;
        }

        public bool IsFixed() {
            return fixedPosition;
        }
    }
}
