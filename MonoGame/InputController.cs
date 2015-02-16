using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.content {
    public class InputController {

        private bool useGamepad = GameController.useGamepad;
        private GamePadButtons button = GamePad.GetState(PlayerIndex.One).Buttons;
        private GamePadThumbSticks tSticks = GamePad.GetState(PlayerIndex.One).ThumbSticks;
        private KeyboardState key = Keyboard.GetState();

        public static bool 
            pauseDown,
            exitDown,
            rightShoot,
            leftShoot,
            rightMove,
            leftMove,
            nextWeapon,
            prevWeapon,
            selectDown,
            upDown,
            downDown,
            backDown,
            anyKey;

        public InputController() {
        }

        public void Update() {
            button = GamePad.GetState(PlayerIndex.One).Buttons;
            key = Keyboard.GetState();
            tSticks = GamePad.GetState(PlayerIndex.One).ThumbSticks;

            if (GamePad.GetState(PlayerIndex.One).IsConnected) useGamepad = true;
            if (GamePad.GetState(PlayerIndex.Two).IsConnected) useGamepad = true;
            if (GamePad.GetState(PlayerIndex.Three).IsConnected) useGamepad = true;
            if (GamePad.GetState(PlayerIndex.Four).IsConnected) useGamepad = true;

            if (useGamepad) {
                if (button.Start == ButtonState.Pressed) pauseDown = true;
                else pauseDown = false;
                if (button.Back == ButtonState.Pressed) exitDown = true;
                else exitDown = false;
                if (tSticks.Right.X > 0f) rightShoot = true;
                else rightShoot = false;
                if (tSticks.Right.X < 0f) leftShoot = true;
                else leftShoot = false;
                if (tSticks.Left.X > 0f) rightMove = true;
                else rightMove = false;
                if (tSticks.Left.X < 0f) leftMove = true;
                else leftMove = false;
                if (button.RightShoulder == ButtonState.Pressed) nextWeapon = true;
                else nextWeapon = false;
                if (button.LeftShoulder == ButtonState.Pressed) prevWeapon = true;
                else prevWeapon = false;
                if (button.A == ButtonState.Pressed) selectDown = true;
                else selectDown = false;
                if (tSticks.Left.Y > 0f) upDown = true;
                else upDown = false;
                if (tSticks.Left.Y < 0f) downDown = true;
                else downDown = false;
                if (button.B == ButtonState.Pressed) backDown = true;
                else backDown = false;
                if (button.B == ButtonState.Pressed) anyKey = true;
                else anyKey = false;
            } else {
                if (key.IsKeyDown(Keys.P)) pauseDown = true;
                else pauseDown = false;
                if (key.IsKeyDown(Keys.Escape)) exitDown = true;
                else exitDown = false;
                if (key.IsKeyDown(Keys.Right)) rightShoot = true;
                else rightShoot = false;
                if (key.IsKeyDown(Keys.Left)) leftShoot = true;
                else leftShoot = false;
                if (key.IsKeyDown(Keys.D)) rightMove = true;
                else rightMove = false;
                if (key.IsKeyDown(Keys.A)) leftMove = true;
                else leftMove = false;
                if (key.IsKeyDown(Keys.E)) nextWeapon = true;
                else nextWeapon = false;
                if (key.IsKeyDown(Keys.Q)) prevWeapon = true;
                else prevWeapon = false;
                if (key.IsKeyDown(Keys.Space)) selectDown = true;
                else selectDown = false;
                if (key.IsKeyDown(Keys.Up)) upDown = true;
                else upDown = false;
                if (key.IsKeyDown(Keys.Down)) downDown = true;
                else downDown = false;
                if (key.IsKeyDown(Keys.Back)) backDown = true;
                else backDown = false;
                if (key.GetPressedKeys().Length > 0) anyKey = true;
                else anyKey = false;
            }
        }

        public bool IsPauseDown() {
            return pauseDown;
        }

        public bool IsExitDown() {
            return exitDown;
        }

        public bool IsRightShoot() {
            return rightShoot;
        }

        public bool IsLeftShoot() {
            return leftShoot;
        }

        public bool IsRightMove() {
            return rightMove;
        }

        public bool IsLeftMove() {
            return leftMove;
        }

        public bool IsNextWeapon() {
            return nextWeapon;
        }

        public bool IsPrevWeapon() {
            return prevWeapon;
        }

        public bool IsSelectDown() {
            return selectDown;
        }

        public bool IsDownDown() {
            return downDown;
        }

        public bool IsUpDown() {
            return upDown;
        }

        public bool IsBackDown() {
            return backDown;
        }

        public bool IsAnyKey() {
            return anyKey;
        }
    }
}
