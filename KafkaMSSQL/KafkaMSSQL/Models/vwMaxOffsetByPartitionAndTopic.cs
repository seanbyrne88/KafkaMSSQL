namespace KafkaMSSQL.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("vwMaxOffsetByPartitionAndTopic")]
    public partial class vwMaxOffsetByPartitionAndTopic
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1000)]
        public string Topic { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Partition { get; set; }

        public long? MaxOffset { get; set; }
    }
}
