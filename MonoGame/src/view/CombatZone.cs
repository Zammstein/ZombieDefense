using Microsoft.Xna.Framework;
using MonoGame.src.entity.airdrops;
using MonoGame.src.entity.block;
using MonoGame.src.entity.effect;
using MonoGame.src.entity.mob;
using MonoGame.src.entity.placeable;
using MonoGame.src.entity.projectile;
using MonoGame.src.hud;
using MonoGame.src.hud.button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZombDefense.src.hud;

namespace MonoGame.src.view {
    class CombatZone : View{

        public CombatZone() {
            InitializeEntities();
            InitializeHuds();
            this.backgroundImagePath = "textures/backgrounds/night_sky";
            this.levelBounds = new Rectangle(0,0,2000, 1000);
        }

        protected void InitializeHuds() {
            huds.Add(new PlayerHealthbar());
            huds.Add(new Score());
            huds.Add(new Wave());
            huds.Add(new HighScore());
            huds.Add(new ViewControls());
            huds.Add(new Play());
            huds.Add(new Credits());
        }

        public override void NextWave() { 
            Random random = new Random();
            int side = random.Next(2);
            int pack = random.Next(3);
            int item = random.Next(3);
            for (int i = 0; i < random.Next(2) + 3 * GameController.currentWave; i++ ) {
                Guard guard;
                if (side == 0) {
                    guard = new Guard(levelBounds.Width + random.Next(200), 655, 50);
                } else {
                    guard = new Guard(-(random.Next(200) + 140), 655, 50);
                }
                guard.SetSpeed(random.Next(2) + 1);
                if (random.Next(50) == 25) guard.SetSpeed(5);
                guard.SetHealth(random.Next(5) * (int) GameController.currentWave + 100);
                entities.Add(guard);
            }
            if (pack == 0) entities.Add(new Healthpack(random.Next(2000), 0, 50 + (random.Next(2) + 1) * (int)GameController.currentWave));
            if (pack == 1) entities.Add(new Fireratepack(random.Next(2000), 0, random.Next(3) + 2, 4));
            if (pack == 2) entities.Add(new Speedpack(random.Next(2000), 0, random.Next(5) + 5, random.Next(10) + 2));
            if (item == 0) entities.Add(new Airsupport(random.Next(2000), 0));
            if (item == 1) entities.Add(new Bardwirepack(random.Next(2000), 0));
            if (item == 2) entities.Add(new Turretpack(random.Next(2000), 0));
        }

        protected void InitializeEntities() {
            for (int i = 0; i < 40; i++) {
                entities.Add(new GrassBlock(-1000 + 90 * i, 750, 1, "grass_top"));
            }
            for (int i = 0; i < 23; i++) {
                entities.Add(new GrassBlock(0 + 90 * i, 810, 1, "dirt"));
            }
            for (int i = 0; i < 23; i++) {
                entities.Add(new GrassBlock(0 + 90 * i, 870, 1, "dirt"));
            }
            for (int i = 0; i < 23; i++) {
                entities.Add(new GrassBlock(0 + 90 * i, 930, 1, "dirt"));
            }
        }
    }
}
