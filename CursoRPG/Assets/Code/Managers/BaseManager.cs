using UnityEngine;

namespace Managers
{
    public class BaseManager : SingletonMonoBehaviour<BaseManager>
    {
        public virtual void ConfigureManager() { }
    }
}
