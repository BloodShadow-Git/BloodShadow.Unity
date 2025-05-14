using BloodShadow.GameCore.Entrypoint;
using R3;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.Entrypoint
{
    public abstract class EntrypointProvider<T> : MonoBehaviour, IEntrypoint where T : EnterParams
    {
        public Observable<ExitParams> Run(EnterParams enterParams)
        {
            Debug.Log($"Running {GetType().Name} entrypoint");
            return Init((T)enterParams);
        }

        protected abstract Observable<ExitParams> Init(T enterParams);
    }
}
