using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.src.entity.mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame {
    class Camera {
        public Matrix transform;
        Viewport view;
        Vector2 centre;

        public Camera(Viewport newView) { 
            view = newView;
        }

        public void Update(GameTime gameTime) {
            Player player = GameController.view.GetPlayer();
            if (player.GetZ() != 0) {
                centre = new Vector2(player.GetX() + (player.GetFrameWidth() / 2 - view.Width / 2), player.GetY() + (player.GetFrameHeight() / 2 - (int)(view.Height / 1.3)));

                // check if camera touches left bound of current level
                if (centre.X <= GameController.view.GetLevelBounds().Left + view.Width / 2 && player.GetX() + player.GetFrameWidth() / 2 <= GameController.view.GetLevelBounds().Left + view.Width / 2) {
                    centre = new Vector2(GameController.view.GetLevelBounds().Left, player.GetY() + (player.GetFrameHeight() / 2 - (int)(view.Height / 1.3)));
                }

                // check if camera touches right bound of current level
                if (centre.X + view.Width >= GameController.view.GetLevelBounds().Right && player.GetX() + view.Width >= GameController.view.GetLevelBounds().Right) {
                    centre = new Vector2(GameController.view.GetLevelBounds().Right - view.Width, (player.GetY() + player.GetFrameHeight() / 2) - (int)(view.Height / 1.3));
                }
            } else {
                centre = new Vector2(view.Width / 2, view.Height / 2);
            }


            transform = Matrix.CreateScale(new Vector3(1,1,0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y,0));
        }
    }
}
