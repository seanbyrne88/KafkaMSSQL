using System;
using System.Linq;
using System.Collections.Generic;
using KafkaMSSQL.Models;

namespace KafkaMSSQL.Repositories
{
    public class KafkaConsumerRepository
    {
        private KafkaModel context;

        public KafkaConsumerRepository ()
        {
            this.context = new KafkaModel();
        }

        public List<KafkaConsumerMessage> GetKafkaConsumerMessages()
        {
            return context.KafkaConsumerMessage.ToList();
        }

        public KafkaConsumerMessage GetKafkaConsumerMessageByID(int MessageID)
        {
            return context.KafkaConsumerMessage.Find(MessageID);
        }

        public List<KafkaConsumerMessage> GetKafkaConsumerMessageByTopic(string TopicName)
        {
            return context.KafkaConsumerMessage
                .Where(a => a.Topic == TopicName)
                .ToList();
        }

        public List<vwMaxOffsetByPartitionAndTopic> GetOffsetPositionByTopic(string TopicName)
        {
            return context.vwMaxOffsetByPartitionAndTopic
                .Where(a => a.Topic == TopicName)
                .ToList();
        }

        public void InsertKafkaConsumerMessage(KafkaConsumerMessage Message)
        {
            Console.WriteLine(String.Format("Insert {0}: {1}", Message.KafkaConsumerMessageID.ToString(), Message.MessageContent));
            context.KafkaConsumerMessage.Add(Message);
            context.SaveChanges();

            Console.WriteLine(String.Format("Saved {0}: {1}", Message.KafkaConsumerMessageID.ToString(), Message.MessageContent));
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
