USE [Inventory]
GO
/****** Object:  Table [dbo].[things]    Script Date: 2/22/2016 2:40:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[things](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[things] ON 

INSERT [dbo].[things] ([id], [description]) VALUES (1, N'apple')
INSERT [dbo].[things] ([id], [description]) VALUES (2, N'bigfoot')
SET IDENTITY_INSERT [dbo].[things] OFF
