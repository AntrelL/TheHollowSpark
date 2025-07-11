using UnityEngine;

namespace RainyPlace.DI
{
    public abstract class Bootstrap : MonoBehaviour
    {
        public virtual void Init() { }
        
        public virtual void Init(GameObject world) { }
    }
}
