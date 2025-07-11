using RainyPlace.DI;
using UnityEngine;

namespace TheHollowSpark
{
    public class MenuBootstrap : Bootstrap
    {
        public override void Init(GameObject world)
        {
            world.SetActive(true);
        }
    }
}
