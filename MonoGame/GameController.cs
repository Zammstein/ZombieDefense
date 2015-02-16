#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Collections;
using MonoGame.src.entity;
using MonoGame.src.view;
using Microsoft.Xna.Framework.Input;
using MonoGame.src;
using MonoGame.content;
using MonoGame.src.entity.mob;
using MonoGame.src.hud;
using MonoGame.src.entity.block;
using MonoGame.src.hud.button;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace MonoGame {

    public class GameController : Game {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static View view;
        private Camera camera;
        private ArrayList textures = new ArrayList();
        private ArrayList sounds = new ArrayList();
        public static SpriteFont spriteFont;
        public static InputController input = new InputController();
        public static Texture2D treeSmall, treeLarge, lantern, lanternLight, darkMap;
        public static SoundEffect moan, brains, gong, theme, deathOne, deathTwo;
        public static ContentManager contentManager = new ContentManager();
        
        public static double gravity = 3;
        public static bool gamePaused;
        public static float scoreTotal = 0f;
        public static float highScore = 0f;
        public static float currentWave = 0f;
        public static bool useGamepad;
        public static int waveCountDown = 600;
        public static bool mainMenu, gameOver;
       
        private bool isPauseKeyUp = true;
        private bool isPauseKeyDown = false;

        public GameController() : base() {
            this.Components.Add(new GamerServicesComponent(this));
            this.Window.Title = "ZombieDefense";
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "content";
        }

        protected override void Initialize() {
            graphics.IsFullScreen = false;
            camera = new Camera(GraphicsDevice.Viewport);
            view = new CombatZone();

            if (GamePad.GetState(PlayerIndex.One).IsConnected) useGamepad = true;
           
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            contentManager.InitiateLoad();
            Texture2D levelBackground = Content.Load<Texture2D>(view.backgroundImagePath);
            treeSmall = Content.Load<Texture2D>("textures/backgrounds/tree_small");
            treeLarge = Content.Load<Texture2D>("textures/backgrounds/tree_large");
            darkMap = Content.Load<Texture2D>("textures/backgrounds/dark_map");
            lantern = Content.Load<Texture2D>("textures/backgrounds/lantern");
            lanternLight = Content.Load<Texture2D>("textures/props/lantern_light");

            moan = Content.Load<SoundEffect>("sounds/moan");
            brains = Content.Load<SoundEffect>("sounds/brains");
            gong = Content.Load<SoundEffect>("sounds/gong");
            theme = Content.Load<SoundEffect>("sounds/theme");
            deathOne = Content.Load<SoundEffect>("sounds/death_one");
            deathTwo = Content.Load<SoundEffect>("sounds/death_two");
            PlayTheme();
            view.background = levelBackground;

            foreach (Entity entity in view.entities) {
                if (entity.GetSpritePath() != null) {
                    Texture2D sprite = Content.Load<Texture2D>(entity.GetSpritePath());
                    textures.Add(sprite);
                    entity.SetSprite(sprite);

                    if (entity.GetAdditionalSpritePath() != null) {
                        Texture2D adSprite = Content.Load<Texture2D>(entity.GetAdditionalSpritePath());
                        textures.Add(adSprite);
                        entity.SetAdditionalSprite(adSprite);
                    }

                    if (entity.GetSoundPath() != null) {
                        SoundEffect sound = Content.Load<SoundEffect>(entity.GetSoundPath());
                        sounds.Add(sound);
                        entity.SetSound(sound);
                    }
                }

            }

            foreach (HUD hud in view.huds) {
                if (hud.GetSpritePath() != null) {
                    Texture2D sprite = Content.Load<Texture2D>(hud.GetSpritePath());
                    textures.Add(sprite);
                    hud.SetSprite(sprite);
                }
            }

            spriteFont = Content.Load<SpriteFont>("textures/fonts/Font");
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            input.Update();
            if (mainMenu) UpdateMenuControls();
            if (input.IsExitDown() && mainMenu) {
                Exit();
            }
            if (input.IsPauseDown() && !isPauseKeyDown && !mainMenu && !gameOver) {
                isPauseKeyDown = true;
                if (isPauseKeyUp) {
                    gamePaused = !gamePaused;
                    isPauseKeyUp = false;
                }
            }
            if (!gamePaused) UpdateWave();

            if (!input.IsPauseDown()) {
                isPauseKeyUp = true;
                isPauseKeyDown = false;
            }
           
            for (int i = 0; i < view.entities.Count; i++) {
                Entity entity = (Entity)view.entities[i];
                if (entity.GetSprite() == null) { 
                    Texture2D sprite = Content.Load<Texture2D>(entity.GetSpritePath());
                    textures.Add(sprite);
                    entity.SetSprite(sprite);
                }

                if (entity.GetAdditionalSprite() == null && entity.GetAdditionalSpritePath() != null) {
                    Texture2D sprite = Content.Load<Texture2D>(entity.GetAdditionalSpritePath());
                    textures.Add(sprite);
                    entity.SetAdditionalSprite(sprite);
                }

                if (entity.GetSound() == null && entity.GetSoundPath() != null) {
                    SoundEffect sound = Content.Load<SoundEffect>(entity.GetSoundPath());
                    sounds.Add(sound);
                    entity.SetSound(sound);
                }
            }

            for (int i = 0; i < view.huds.Count; i++) {
                HUD hud = (HUD)view.huds[i];
                if (hud.GetSpritePath() != null) {
                    Texture2D sprite = Content.Load<Texture2D>(hud.GetSpritePath());
                    textures.Add(sprite);
                    hud.SetSprite(sprite);
                }
            }

            view.Update(gameTime);
            camera.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            spriteBatch.Draw(view.background, view.GetLevelBounds(), Color.White);
            for (int i = 0; i < 20; i++) {
                spriteBatch.Draw(treeLarge, new Rectangle(i * 300, 250, 359, 562), Color.White);
                spriteBatch.Draw(treeSmall, new Rectangle(i * 300 - 150, 300, 362, 567), Color.White);
            }

            for (int i = 0; i < 10; i++) {
                spriteBatch.Draw(lantern, new Rectangle(i * 300, 540, 28, 282), Color.White);
            }

            foreach (Entity entity in view.entities) {
                if (entity.IsVisible() && entity.GetSprite() != null) {
                    entity.Draw(spriteBatch);
                }
            }

            foreach (HUD hud in view.huds) {
                if (hud.IsVisible() && !hud.IsFixed()) {
                    hud.Draw(spriteBatch);
                }
            }
            
            if (gamePaused || gameOver) spriteBatch.Draw(darkMap, view.GetLevelBounds(), Color.White);
        
            spriteBatch.End();

            // Draw HUD
            spriteBatch.Begin();
            foreach (HUD hud in view.huds) {
                if (hud.IsVisible() && hud.IsFixed()) {
                    hud.Draw(spriteBatch);
                }
            }
            if (gamePaused && !mainMenu) {
                spriteBatch.DrawString(spriteFont, "Game paused", new Vector2(350, 240), Color.White);
                spriteBatch.DrawString(spriteFont, "Press P or Start to continue", new Vector2(320, 260), Color.White, 0f, new Vector2(0, 0), new Vector2(0.7f, 0.7f), SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateWave() {
            bool enemyPresent = false;
            for (int i = 0; i < view.GetEntities().Count; i++) {
                var entity = view.GetEntities()[i];
                if (entity.GetType() == typeof(Guard) && entity.GetType() != typeof(Player)) {
                    enemyPresent = true;
                }
            }
            if (!enemyPresent) {
                WaveCountDown();
            }
        }

        private void WaveCountDown() {
            if (waveCountDown % 60 == 0 && waveCountDown != 0) view.huds.Add(new Popup(view.GetPlayer().GetX() + view.GetPlayer().GetFrameWidth() / 2, view.GetPlayer().GetY() - view.GetPlayer().GetFrameHeight() / 2, (waveCountDown / 60).ToString(), Color.White, 2f));
            if (waveCountDown <= 0) {
                currentWave++;
                view.NextWave();
                brains.Play();
                moan.Play();
                waveCountDown = 601;
            }
            if (waveCountDown == 600) gong.Play();
            waveCountDown--;
        }

        public static void NewGame() {
            for (int i = 0; i < view.huds.Count; i++) {
                if (view.huds[i].GetType() == typeof(Wave) || view.huds[i].GetType() == typeof(PlayerHealthbar)) {
                    HUD hud = (HUD) view.huds[i];
                    hud.SetVisibility(true);
                } else if (view.huds[i].GetType() == typeof(Score)) {
                    HUD hud = (HUD)view.huds[i];
                    hud.SetPosition(new Vector2(20, 20));
                }
            }
            mainMenu = false;
            currentWave = 0;
            scoreTotal = 0;
            gamePaused = false;
            view.entities.Add(new Player(1000, 675, 50));
        }

        private void UpdateMenuControls() {
            if (input.IsUpDown()) {
                for (int i = 0; i < view.huds.Count; i++) {
                    if (view.huds[i].GetType() == typeof(ViewControls)) {
                        Button button = (Button) view.huds[i];
                        button.Select();
                    }
                    if (view.huds[i].GetType() == typeof(Play)) {
                        Button button = (Button)view.huds[i];
                        button.DeSelect();
                    }
                }
            }
            if (input.IsDownDown()) {
                for (int i = 0; i < view.huds.Count; i++) {
                    if (view.huds[i].GetType() == typeof(ViewControls)) {
                        Button button = (Button)view.huds[i];
                        button.DeSelect();
                    }
                    if (view.huds[i].GetType() == typeof(Play)) {
                        Button button = (Button)view.huds[i];
                        button.Select();
                    }
                }
            }
        }

        private void PlayTheme() {
            SoundEffectInstance themeInstance = theme.CreateInstance();
            themeInstance.IsLooped = true;
            themeInstance.Volume = 0.75f;
            themeInstance.Play();
        }

        public static void PlayEnemyDeath() {
            Random random = new Random();
            int r = random.Next(2);
            if (r == 0) {
                deathOne.Play();
            } else {
                deathTwo.Play();
            }
        }
    }
}
