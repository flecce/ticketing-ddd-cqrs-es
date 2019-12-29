USE [master]
GO

IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = 'EventStore' 
OR name = 'EventStore')))
	set noexec on               -- prevent creation when already exists
  
CREATE DATABASE [EventStore];
GO

USE [EventStore]
GO

CREATE TABLE Events(
  Id uniqueidentifier not null,
  AggregateType nvarchar(100) not null,
  AggregateId uniqueidentifier not null,
  Version int not null,
  Event nvarchar(MAX) not null,
  MetaData nvarchar(MAX) null,
  Dispatched bit not null default(0),
  Created datetime not null,
  Constraint PKEvents PRIMARY KEY(ID)
)
GO

CREATE INDEX Idx_Events_AggregateId
ON Events(AggregateId)
GO

CREATE INDEX Idx_Events_Dispatched
ON Events(Dispatched)
GO