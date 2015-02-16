using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.placeable {
    public abstract class Placeable : Entity {
        protected bool placed;
        protected Vector2 position;
        protected int damage;


    }
}
