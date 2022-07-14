using System;
using System.Linq;

namespace UnityEngine
{
    public static class Generics
    {
        public static T GetComponentInChildren<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponentsInChildren<T>().FirstOrDefault();
        }
        public static void NotifySiblings<I>(this Transform transform, Action<I> OnInterface) where I : class
        {
            transform.parent.GetComponentsInChildren<I>().ToList().ForEach(OnInterface);
        }
        public static void NotifyParent<I>(this Transform transform, Action<I> OnInterface) where I : class
        {
            transform.GetComponentsInParent<I>().ToList().ForEach(OnInterface);
        }
        public static void NotifyChildren<I>(this Transform transform, Action<I> OnInterface) where I : class
        {
            transform.GetComponentsInChildren<I>().ToList().ForEach(OnInterface);
        }
    }
}