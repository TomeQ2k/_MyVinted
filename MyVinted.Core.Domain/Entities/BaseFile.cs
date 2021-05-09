using System;
using MyVinted.Core.Common.Helpers;

namespace MyVinted.Core.Domain.Entities
{
    public abstract class BaseFile
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Path { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;

        public static T Create<T>(string path) where T : BaseFile, new() => new T
        {
            Path = path
        };
    }
}