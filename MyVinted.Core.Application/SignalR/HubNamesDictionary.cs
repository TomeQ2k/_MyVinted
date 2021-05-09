using System;
using System.Collections.Generic;

namespace MyVinted.Core.Application.SignalR
{
    public class HubNamesDictionary : Dictionary<Type, string>
    {
        private const string NotifierHub = "NOTIFIER";
        private const string MessengerHub = "MESSENGER";

        public static HubNamesDictionary Build() => new HubNamesDictionary
        {
            { typeof(NotifierHub), NotifierHub },
            { typeof(MessengerHub), MessengerHub }
        };
    }
}