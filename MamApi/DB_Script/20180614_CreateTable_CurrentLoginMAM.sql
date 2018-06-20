USE [HPCS]
GO

/****** Object:  Table [dbo].[CurrentLoginMAM]    Script Date: 14/6/2561 16:00:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CurrentLoginMAM](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](20) NULL,
	[MobileIMEI] [varchar](50) NULL,
	[FirebaseToken] [varchar](4000) NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[LastFirebaseToken] [varchar](4000) NULL,
 CONSTRAINT [PK_CurrentLoginMAM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


