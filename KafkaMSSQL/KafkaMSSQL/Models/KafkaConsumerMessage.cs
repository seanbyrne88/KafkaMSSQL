namespace KafkaMSSQL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KafkaConsumerMessage")]
    public partial class KafkaConsumerMessage
    {
        public int KafkaConsumerMessageID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Topic { get; set; }

        public int Partition { get; set; }

        public long Offset { get; set; }

        [Required]
        public string MessageContent { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
