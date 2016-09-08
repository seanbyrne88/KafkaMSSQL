namespace KafkaMSSQL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KafkaProducerMessage")]
    public partial class KafkaProducerMessage
    {
        public int KafkaProducerMessageID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Topic { get; set; }

        [Required]
        public string MessageContent { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
