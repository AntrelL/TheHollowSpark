using RainyPlace.DI;
using UnityEngine.SceneManagement;

namespace TheHollowSpark
{
    public class InitBootstrap : Bootstrap
    {
        public override void Init()
        {
            SceneManager.LoadScene(SceneName.Main);
        }
    }
}
