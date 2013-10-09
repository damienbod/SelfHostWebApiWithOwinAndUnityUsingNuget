
using System;
using Microsoft.Practices.Unity;

namespace Unity.SelfHostWebApiOwin
{
    public class PerRequestLifetimeManager : LifetimeManager
    {
        private readonly object lifetimeKey = new object();

        /// <summary>
        /// Retrieves a value from the backing store associated with this lifetime policy.
        /// </summary>
        /// <returns>The desired object, or null if no such object is currently stored.</returns>
        public override object GetValue()
        {
            return UnityPerRequestHttpModule.GetValue(this.lifetimeKey);
        }

        /// <summary>
        /// Stores the given value into the backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            UnityPerRequestHttpModule.SetValue(this.lifetimeKey, newValue);
        }

        /// <summary>
        /// Removes the given object from the backing store.
        /// </summary>
        public override void RemoveValue()
        {
            var disposable = this.GetValue() as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }

            UnityPerRequestHttpModule.SetValue(this.lifetimeKey, null);
        }
    }
}
