using System.Collections.Generic;
using System.Diagnostics;

namespace LostWarehouse.Messages
{
    /// <summary>
    /// Core mechanism for routing messages to arbitrary listeners.
    /// This allows components with unrelated responsibilities to interact without becoming coupled, since message senders don't
    /// need to know what (if anything) is receiving their messages.
    /// </summary>
    public class Messenger : IMessenger
    {
        private List<IReceiveMessage> m_receivers = new List<IReceiveMessage>();
        private const float k_durationToleranceMs = 10;

        /// <summary>
        /// Assume that you won't receive messages in a specific order.
        /// </summary>
        public virtual void Subscribe(IReceiveMessage receiver)
        {
            if (!m_receivers.Contains(receiver))
                m_receivers.Add(receiver);
        }

        public virtual void Unsubscribe(IReceiveMessage receiver)
        {
            m_receivers.Remove(receiver);
        }

        /// <summary>
        /// Send a message to any subscribers, who will decide how to handle the message.
        /// </summary>
        /// <param name="msg">If there's some data relevant to the recipient, include it here.</param>
        public virtual void OnReceiveMessage(MessageType type, object msg)
        {
            Stopwatch stopwatch = new Stopwatch();
            for (int r = 0; r < m_receivers.Count; r++)
            {
                stopwatch.Restart();
                m_receivers[r].OnReceiveMessage(type, msg);
                stopwatch.Stop();
                /*if (stopwatch.ElapsedMilliseconds > k_durationToleranceMs)
                    Debug.LogWarning($"Message recipient \"{m_receivers[r]}\" took too long to process message \"{msg}\" of type {type}");*/
            }
        }

        public void OnReProvided(IMessenger previousProvider)
        {
            if (previousProvider is Messenger)
                m_receivers.AddRange((previousProvider as Messenger).m_receivers);
        }
    }
}
