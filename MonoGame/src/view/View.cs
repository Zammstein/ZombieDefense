using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity;
using MonoGame.src.entity.block;
using MonoGame.src.entity.effect;
using MonoGame.src.entity.mob;
using MonoGame.src.hud;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.view {
    public class View {

        public Texture2D background;
        public String backgroundImagePath;
        public Rectangle levelBounds;
        public ArrayList entities = new ArrayList();
        public ArrayList huds = new ArrayList();
        protected Random random = new Random();

        public void Update(GameTime gameTime) {
            if (!GameController.gamePaused) {
                for (int i = 0; i < entities.Count; i++) {
                    Entity entity = (Entity)entities[i];
                    if (entity.IsRemoved()) entities.RemoveAt(i);
                }
                for (int i = 0; i < entities.Count; i++) {
                    Entity entity = (Entity)entities[i];
                    entity.Update(gameTime);
                }
            }
            for (int i = 0; i < huds.Count; i++) {
                HUD hud = (HUD)huds[i];
                if (hud.IsRemoved()) huds.RemoveAt(i);
            }
            for (int i = 0; i < huds.Count; i++) {
                HUD hud = (HUD)huds[i];
                hud.Update(gameTime);
            }
            if (random.Next(500) == 0) entities.Add(new Cloud());
        }

        protected void InitializeEntities() {
        }

        protected void InitializeHuds() {
        }

        public virtual void NextWave() { 
        }

        public Player GetPlayer() {
            foreach (Entity entity in entities) {
                if (entity is Player) return (Player) entity;
            }
            Player player = new Player(0,0,0);
            return player;
        }

        public ArrayList GetEntities() {
            return entities;
        }

        public Entity GetEntity(int index) {
            return (Entity) entities[index];
        }

        public ArrayList GetHuds() {
            return huds;
        }

        public Rectangle GetLevelBounds() {
            return levelBounds;
        }
    }
}
