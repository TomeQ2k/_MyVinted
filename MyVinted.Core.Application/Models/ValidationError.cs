using System.Collections.Generic;

namespace MyVinted.Core.Application.Models
{
    public record ValidationError
    {
        public string Field { get; }
        public IEnumerable<string> Message { get; }

        public ValidationError(string field, IEnumerable<string> messages)
            => (Field, Message) = (field != string.Empty ? field : null, messages);
    }
}