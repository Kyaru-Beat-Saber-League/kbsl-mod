using System;
using System.Collections;

namespace KBSL_MOD.Utils
{
    public class CoroutineUtils : PersistentSingleton<CoroutineUtils>
    {
        public new void StartCoroutine(IEnumerator coroutine)
        {
            gameObject.SetActive(true);
            base.StartCoroutine(coroutine);
        }

        public void StartCoroutine(IEnumerator coroutine, Action onFinished)
        {
            gameObject.SetActive(true);
            base.StartCoroutine(CallbackCoroutine(coroutine, onFinished));
        }

        private IEnumerator CallbackCoroutine(IEnumerator coroutine, Action callback)
        {
            yield return coroutine;
            callback?.Invoke();
        }
    }
}