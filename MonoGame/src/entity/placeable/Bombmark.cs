using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.effect;
using MonoGame.src.entity.mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.placeable {
    public class Bombmark : Placeable {

        private bool planeActive;
        private Bomber bomber;
        private float counter;

        public Bombmark(int x) {
            this.position.X = x;
            this.position.Y = 750;
            this.health = health;
            this.damage = damage;
            this.spritePath = "textures/props/bullet";
            this.frameWidth = 1;
            this.frameHeight = 1;
            this.bounds = new Rectangle((int) position.X, (int) position.Y, frameWidth, frameHeight);
            this.currentFrame = new Vector4(0, 0, 1, 0);
            this.isSolid = false;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite, bounds, Color.White);
        }

        public override void Update(GameTime gameTime) {
            if (!planeActive) {
                bomber = new Bomber((int) position.X);
                GameController.view.entities.Add(bomber);
                planeActive = true;
            }
            if (bomber.GetX() + bomber.GetFrameWidth() / 2 >= position.X - 20 && bomber.GetX() + bomber.GetFrameWidth() / 2 <= position.X + 20) Remove();
            if (counter % 30 == 0) GameController.view.entities.Add(new Smoke((int) position.X, (int) position.Y - 100));
            counter++;
        }
    }
}
