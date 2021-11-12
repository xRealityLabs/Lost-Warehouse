

using LostWarehouse.ProviderManager;

namespace LostWarehouse.Messages
{
    /// <summary>
    /// Something to which IReceiveMessages can send/subscribe for arbitrary messages.
    /// </summary>
    public interface IMessenger : IReceiveMessage, IProvider<IMessenger>
    {
        void Subscribe(IReceiveMessage receiver);
        void Unsubscribe(IReceiveMessage receiver);
    }
}
