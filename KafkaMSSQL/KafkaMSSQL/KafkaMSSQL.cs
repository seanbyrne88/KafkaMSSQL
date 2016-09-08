using System;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using KafkaMSSQL.Repositories;
using KafkaMSSQL.Models;


namespace KafkaMSSQL
{
    class KafkaMSSQL
    {
        private static string Topic;
        static void Main(string[] args)
        {
            string invalidArgErrorMessage = "Valid args are: produce or consume";

            if (args.Length != 1)
            {
                throw (new Exception(invalidArgErrorMessage));
            }

            string intent = args[0];
            
//#if DEBUG
//                intent = "consume";
//#endif

            Topic = ConfigurationManager.AppSettings["Topic"];

            if (String.Equals(intent, "consume", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Starting Consumer Service");
                Consume();
            }
            else if (String.Equals(intent, "produce", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Starting Producer Service");
                Produce();
            }
            else
            {
                throw (new Exception(invalidArgErrorMessage));
            }
        }

        private static BrokerRouter InitDefaultConfig()
        {
            List<Uri> ZKURIList = new List<Uri>();
            //building UriList
            foreach (string s in ConfigurationManager.AppSettings["BrokerList"].Split(','))
            {
                ZKURIList.Add(new Uri(s));
            }

            var Options = new KafkaOptions(ZKURIList.ToArray());
            var Router = new BrokerRouter(Options);
            return Router;
        }

        private static void Consume()
        {
            KafkaConsumerRepository KafkaRepo = new KafkaConsumerRepository();
            bool FromBeginning = Boolean.Parse(ConfigurationManager.AppSettings["FromBeginning"]);

            var Router = InitDefaultConfig();
            var Consumer = new Consumer(new ConsumerOptions(Topic, Router));

            //if we don't want to start from beginning, use latest offset.
            if (!FromBeginning)
            {
                var MaxOffsetByPartition = KafkaRepo.GetOffsetPositionByTopic(Topic);
                //if we get a result use it, otherwise default
                if (MaxOffsetByPartition.Count != 0)
                {
                    List<OffsetPosition> offsets = new List<OffsetPosition>();
                    foreach (var m in MaxOffsetByPartition)
                    {
                        OffsetPosition o = new OffsetPosition(m.Partition, (long)m.MaxOffset + 1);
                        offsets.Add(o);
                    }
                    Consumer.SetOffsetPosition(offsets.ToArray());
                }
                else
                {
                    Consumer.SetOffsetPosition(new OffsetPosition());
                }
            }

            //Consume returns a blocking IEnumerable (ie: never ending stream)
            foreach (var message in Consumer.Consume())
            {
                string MessageContent = Encoding.Default.GetString(message.Value);

                Console.WriteLine(String.Format("Processing message with content: {0}", MessageContent));

                KafkaRepo = new KafkaConsumerRepository();

                KafkaConsumerMessage ConsumerMessage = new KafkaConsumerMessage()
                {
                    Topic = Topic,
                    Offset = (int)message.Meta.Offset,
                    Partition = message.Meta.PartitionId,
                    MessageContent = MessageContent,
                    CreatedAt = DateTime.UtcNow
                };

                KafkaRepo.InsertKafkaConsumerMessage(ConsumerMessage);
                KafkaRepo.Dispose();
            }
        }

        private static void Produce()
        {
            KafkaProducerRepository KafkaRepo = new KafkaProducerRepository();

            var Router = InitDefaultConfig();
            var Client = new Producer(Router);

            List<Message> Messages = new List<Message>();

            foreach (KafkaProducerMessage message in KafkaRepo.GetKafkaProducerMessageByTopic(Topic))
            {
                Messages.Add(new Message(message.MessageContent));
                KafkaRepo.ArchiveKafkaProducerMessage(message.KafkaProducerMessageID);
            }

            Client.SendMessageAsync(Topic, Messages).Wait();

            KafkaRepo.Dispose();
        }
    }
}
