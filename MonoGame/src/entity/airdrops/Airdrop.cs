using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.airdrops {
    public class Airdrop : Entity {

        protected Vector2 currentImagePosition;
        protected Vector2 parachuteImagePosition = new Vector2(0, 0);
        protected Vector2 crateImagePosition = new Vector2(1, 0);

        protected bool PickedUp() {
            if (this.bounds.Intersects(GameController.view.GetPlayer().GetBounds())) {
                return true;
            } else {
                return false;
            }
        }
    }
}
