# KafkaMSSQL

KafkaMSSQL provides a simple way to produce and consume messages to and from kafka topics using SQL Server tables. It uses Entity Framework and [KafkaNet](https://github.com/Jroland/kafka-net) by JRowland.

## Kafka setup
This application assumes you have some familiarity with Apache Kafka. Here is a [Quick Start Guide](http://kafka.apache.org/090/documentation.html) to get up and running.

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

## Usage
Navigate to your build directory and run KafkaMSSQL.exe with either `produce` or `consume` as command line arguments.

  - Produce: Will attempt to produce everything in `KafkaProducerMessage` to the Kafka topic configured in the table, the topic is configured in the `Topic` column in the table. Successfully produced messages are written to `KafkaProducerMessageArchive`.
  - Consume: Will consume everything from the configured topic in App.Config and write it to `KafkaConsumerMessage`

## TODO
- Better Error Handling
- Possibly have configurable tables and columns be Produced to Kafka.
