using System;

namespace AvDe.Contracts.Models
{
    /// <summary>
    /// Base class for database entities
    /// </summary>
    public class DbObject
    {
        /// <summary>
        /// Gets or sets the database id
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
