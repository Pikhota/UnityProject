using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace Assets.Scripts.Client
{
    public interface IServerConnector
    {
        void Connect();
        void DisconectFromServer();

        event Action<bool> OnServerConnectedResult;
        event Action<bool> OnDisonnectedFromServer;

        void Send(short msgId, MessageBase msg);
        void RegisterHandlers(IEnumerable<IServerMessageHandler> handlers);
    }


    public interface IServerMessageHandler
    {
        short MessageType { get; }
        void Handle(NetworkMessage msg);
    }
}
