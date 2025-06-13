using UnityEngine;

namespace Utils
{
    public class RegisteredMonoBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            ControllerLocator.RegisterService(this);
        }
    }
}