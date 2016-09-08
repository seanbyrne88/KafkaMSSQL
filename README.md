# KafkaMSSQL

KafkaMSSQL provides a simple way to produce and consume messages to and from kafka topics. It uses Entity Framework and [KafkaNet](https://github.com/Jroland/kafka-net) by JRowland.

## Setup
2. Run DBSetup/setup.sql.

  This will create the following tables
  - KafkaConsumerMessage: The application writes consumed messages to this table.
  - KafkaProducerMessage: The application will pick up messages and write them to a kafka topic. (there is a column Topic on the table)
  - KafkaProducerMessageArchive: When a message is picked up from KafkaProducerMessage it is archived in this table.

3. Edit App.Config file

  - Broker List (string): List of Kafka Brokers, defaults to localhost:9092.
  - Topic (string): This is only used for the consumer. Topic you want to consume from.
  - FromBeginning (bool): Specifies if you want to start consuming at the lowest offset or the max offset by topic in KafkaConsumerMessage.
  - Connection Settings/KafkaModel: "data source" should be set to the db server your tables are on and "intial catalog" will be the database you want to use.
