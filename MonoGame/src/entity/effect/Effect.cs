using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame.src.entity.effect {
    public abstract class Effect : Entity {

        protected float duration;
        protected float counter;

        public float GetDuration() {
            return duration;
        }

        public void SetDuration(float duration) {
            this.duration = duration;
        }
    }
}
