using Microsoft.Xna.Framework;
using MonoGame.src.entity.mob;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.projectile {
    public abstract class Projectile : Entity {
        protected Vector2 origin;
	    protected int distance;
	    protected int range, damage;

        protected void CheckHit() {
            ArrayList entities = GameController.view.GetEntities();
            for (int i = 0; i < entities.Count; i++) {
                Entity entity = (Entity)entities[i];
                if (entity.GetBounds().Intersects(bounds) && entity.GetType() == typeof(Guard)) {
                    entity.DoDMG(damage);
                    Remove();
                    break;
                }
            }
        }

        protected bool IsHit() {
            ArrayList entities = GameController.view.GetEntities();
            for (int i = 0; i < entities.Count; i++) {
                Entity entity = (Entity)entities[i];
                if (entity.GetBounds().Intersects(bounds) && entity.GetType() == typeof(Guard)) {
                    return true;
                }
            }
            return false;
        }
    }
}
