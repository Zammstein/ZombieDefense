using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.hud.button {
    public abstract class Button : HUD {
        protected bool isSelected, isBreathing;
        private int counter;
        protected float xOrigin, yOrigin;
        protected int widthOrigin, heightOrigin;
        protected string text;

        protected void Breathe() {
            if (isSelected) {
                if (!isBreathing) {
                    isBreathing = true;
                    xOrigin = position.X;
                    yOrigin = position.Y;
                    widthOrigin = width;
                    heightOrigin = height;
                }
                if (counter % 5 == 0 && counter < 61) {
                    position.X--;
                    position.Y--;
                    width += 2;
                    height += 2;
                }
                if (counter % 5 == 0 && counter > 65) {
                    position.X++;
                    position.Y++;
                    width -= 2;
                    height -= 2;
                }
                if (counter > 125) counter = 0;
                counter++;
            } else {
                position.X = xOrigin;
                position.Y = yOrigin;
                width = widthOrigin;
                height = heightOrigin;
                isBreathing = false;
                counter = 0;
            }
        }

        public void Select() {
            this.isSelected = true;
        }

        public void DeSelect() {
            this.isSelected = false;
        }
    }
}
