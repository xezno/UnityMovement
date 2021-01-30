using UnityEngine;

namespace Assets.Scripts.Player
{
    public class BasePlayerModule
    {
        public virtual void FixedUpdate(PlayerController parent, float deltaTime) { }
        public virtual void Update(PlayerController parent, float deltaTime) { }
    }
}
