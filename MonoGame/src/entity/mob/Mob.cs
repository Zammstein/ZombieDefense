using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.mob {
    public abstract class Mob : entity.Entity {

        protected int attackDamage;

        protected Vector4 walking, idle, crouching, attack, shoot;
        protected enum mobState { 
            Walk,
            Idle,
            Run,
            Crouch,
            Attack,
            Climb,
            Jump,
            Fall
        }

        protected bool fireOnCooldown;
        protected int fireRate;
        protected mobState state;

        public void SetSpeed(int amount) {
            speed = amount;
        }

        public int GetSpeed() {
            return speed;
        }

        public void SetFireRate(int amount) {
            fireRate = amount;
        }

        public int GetFireRate() {
            return fireRate;
        }
    }
}
