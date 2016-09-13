using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KafkaMSSQL.Models
{
    [Table("KafkaProducerMessageArchive")]
    public partial class KafkaProducerMessageArchive
    {
        public int KafkaProducerMessageArchiveID { get; set; }

        public int KafkaProducerMessageID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Topic { get; set; }

        [Required]
        public string MessageContent { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ArchivedAt { get; set; }
    }
}
