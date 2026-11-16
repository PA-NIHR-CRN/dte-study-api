using System.ComponentModel.DataAnnotations;

namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public class DbSettings
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Host { get; set; } = null!;
        [Range(0, 65535)]
        public uint? Port { get; set; }
        [Required]
        public string Database { get; set; } = null!;

        /// <summary>
        /// The default command timeout in seconds; if null then the connection string builer's default value is used.
        /// </summary>
        public uint? DefaultCommandTimeout { get; set; } = null;
    }
}
