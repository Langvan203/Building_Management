using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Domain.Entities
{
    public class PayOSConfiguration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ClientId { get; set; } = string.Empty;

        [Required]
        public string ApiKey { get; set; } = string.Empty;

        [Required]
        public string ChecksumKey { get; set; } = string.Empty;

        [Required]
        public string WebhookKey { get; set; } = string.Empty;

        [Required]
        public string BaseUrl { get; set; } = "https://api-merchant.payos.vn";

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
