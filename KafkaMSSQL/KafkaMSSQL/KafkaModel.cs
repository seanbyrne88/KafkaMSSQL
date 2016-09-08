using System;
using System.Data.Entity;
using KafkaMSSQL.Models;

namespace KafkaMSSQL
{
    public partial class KafkaModel : DbContext
    {
        public KafkaModel()
            : base("name=KafkaModel")
        {
        }

        public virtual DbSet<KafkaConsumerMessage> KafkaConsumerMessage { get; set; }
        public virtual DbSet<KafkaProducerMessage> KafkaProducerMessage { get; set; }
        public virtual DbSet<KafkaProducerMessageArchive> KafkaProducerMessageArchive { get; set; }
        public virtual DbSet<vwMaxOffsetByPartitionAndTopic> vwMaxOffsetByPartitionAndTopic { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KafkaConsumerMessage>()
                .Property(e => e.Topic)
                .IsUnicode(false);

            modelBuilder.Entity<KafkaProducerMessage>()
                .Property(e => e.Topic)
                .IsUnicode(false);

            modelBuilder.Entity<KafkaProducerMessageArchive>()
                .Property(e => e.Topic)
                .IsUnicode(false);

            modelBuilder.Entity<vwMaxOffsetByPartitionAndTopic>()
                .Property(e => e.Topic)
                .IsUnicode(false);
        }
    }
}
