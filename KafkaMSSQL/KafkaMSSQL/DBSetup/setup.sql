IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'KafkaConsumerMessage')
BEGIN

	CREATE TABLE [dbo].[KafkaConsumerMessage](
		[KafkaConsumerMessageID] [int] IDENTITY(1,1) NOT NULL,
		[Topic] [varchar](1000) NOT NULL,
		[Partition] [int] NOT NULL,
		[Offset] [bigint] NOT NULL,
		[MessageContent] [nvarchar](max) NOT NULL,
		[CreatedAt] [datetime] NOT NULL,
		CONSTRAINT [PK_Kafka_Consumer_Message_KafkaConsumerMessageID] PRIMARY KEY CLUSTERED ([KafkaConsumerMessageID])
	)
END

GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'KafkaProducerMessage')
BEGIN
	CREATE TABLE [dbo].[KafkaProducerMessage](
		[KafkaProducerMessageID] [int] IDENTITY(1,1) NOT NULL,
		[Topic] [varchar](1000) NOT NULL,
		[MessageContent] [nvarchar](max) NOT NULL,
		[CreatedAt] [datetime] NOT NULL,
		CONSTRAINT [PK_KafkaProducerMessage_KafkaProducerMessageID] PRIMARY KEY CLUSTERED ([KafkaProducerMessageID])
	)
END

GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'KafkaProducerMessageArchive')
BEGIN
	CREATE TABLE [dbo].[KafkaProducerMessageArchive](
		[KafkaProducerMessageArchiveID] [int] IDENTITY(1,1) NOT NULL,
		[KafkaProducerMessageID] [int] NOT NULL,
		[Topic] [varchar](1000) NOT NULL,
		[MessageContent] [nvarchar](max) NOT NULL,
		[CreatedAt] [datetime] NOT NULL,
		[ArchivedAt] [datetime] NOT NULL,
		CONSTRAINT [PK_KafkaProducerMessageArchive_KafkaProducerMessageArchiveID] PRIMARY KEY CLUSTERED ([KafkaProducerMessageArchiveID])
	)
END

GO
