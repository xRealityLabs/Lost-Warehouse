using LostWarehouse.Messages;
using System;
using System.Collections.Generic;

namespace LostWarehouse.ProviderManager
{
    /// <summary>
     /// Base Locator behavior, without static access.
     /// </summary>
    public class LocatorBase
    {
        private Dictionary<Type, object> m_provided = new Dictionary<Type, object>();

        // To limit global access to only components that should have it, and to reduce programmer error, we'll declare explicit flavors of Provide and getters for them.
        public IMessenger Messenger => Locate<IMessenger>();
        public void Provide(IMessenger messenger) { ProvideAny(messenger); }

        // As you add more Provided types, be sure their default implementations are included in the constructor.
        /// <summary>
        /// On construction, we can prepare default implementations of any services we expect to be required. This way, if for some reason the actual implementations
        /// are never Provided (e.g. for tests), nothing will break.
        /// </summary>
        public LocatorBase()
        {
            Provide(new Messenger());

            FinishConstruction();
        }

        protected virtual void FinishConstruction() { }

        /// <summary>
        /// Call this to indicate that something is available for global access.
        /// </summary>
        private void ProvideAny<T>(T instance) where T : IProvider<T>
        {
            Type type = typeof(T);
            if (m_provided.ContainsKey(type))
            {
                var previousProvision = (T)m_provided[type];
                instance.OnReProvided(previousProvision);
                m_provided.Remove(type);
            }

            m_provided.Add(type, instance);
        }

        /// <summary>
        /// If a T has previously been Provided, this will retrieve it. Else, null is returned.
        /// </summary>
        private T Locate<T>() where T : class
        {
            Type type = typeof(T);
            if (!m_provided.ContainsKey(type))
                return null;
            return m_provided[type] as T;
        }
    }
}
