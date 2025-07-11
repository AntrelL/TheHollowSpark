using UnityEngine;

namespace RainyPlace.DI
{
    [DefaultExecutionOrder(-9000)]
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _bootstrap;
        [SerializeField] private GameObject _world;

        private void Awake()
        {
            if (_world == null)
            {
                _bootstrap.Init();
                return;
            }

            _world.SetActive(false);
            _bootstrap.Init(_world);
        }
    }
}
