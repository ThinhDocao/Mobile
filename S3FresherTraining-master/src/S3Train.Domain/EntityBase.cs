using System;

namespace S3Train.Domain
{
    /// <summary>
    /// Base class for database domain entity
    /// </summary>
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }
}