

namespace LostWarehouse.Messages
{
    /// <summary>
    /// Something that wants to subscribe to messages from arbitrary, unknown senders.
    /// </summary>
    public interface IReceiveMessage
    {
        void OnReceiveMessage(MessageType type, object msg);
    }
}
