CREATE TABLE [Sponsor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TournamentID] [int] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	PRIMARY KEY (ID)
	)

	
	CREATE TABLE [TournamentUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TournamentID] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[LicenceNumber] [nvarchar](400) NOT NULL UNIQUE,
	[RankNumber] [int] NOT NULL UNIQUE,
		PRIMARY KEY (ID)
	)
	
	
	CREATE TABLE [Tournament](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Discipline] [nvarchar](max) NULL,
	[Time] [datetime2](7) NULL,
	[Location] [nvarchar](max) NULL,
	[EntryLimit] [int] NOT NULL,
	[EntryDateLimit] [datetime2](7) NULL,
	[AssignedPlayersAmount] [int] NOT NULL,
		PRIMARY KEY (ID)
	)

		ALTER TABLE [Sponsor]  WITH CHECK ADD  CONSTRAINT [FK_Sponsor_Tournament_TournamentID] FOREIGN KEY([TournamentID])
REFERENCES [Tournament] ([ID])
ON DELETE CASCADE
GO

	
	ALTER TABLE [TournamentUser]  WITH CHECK ADD  CONSTRAINT [FK_TournamentUser_Tournament_TournamentID] FOREIGN KEY([TournamentID])
REFERENCES [Tournament] ([ID])
ON DELETE CASCADE
GO