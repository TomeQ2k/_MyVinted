using System;

namespace MyVinted.Core.Domain.Entities
{
    public class Connection
    {
        public string UserId { get; protected set; }
        public string ConnectionId { get; protected set; }
        public DateTime DateEstablished { get; protected set; } = DateTime.Now;
        public string HubName { get; protected set; }

        public virtual User User { get; protected set; }

        public static Connection Create(string userId, string connId, string hubName) => new Connection
        {
            UserId = userId,
            ConnectionId = connId,
            HubName = hubName.ToUpper()
        };
    }
}