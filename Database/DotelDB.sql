
USE [DotelDB]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 5/25/2024 4:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[adminId] [int] IDENTITY(1,1) NOT NULL,
	[phoneNumber] [varchar](200) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[fullname] [nvarchar](50) NOT NULL,
	[password] [nvarchar](32) NOT NULL,
	[status] [bit] NOT NULL,
	[roleId] [int] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[adminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rental]    Script Date: 5/25/2024 4:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rental](
	[rentalId] [int] IDENTITY(1,1) NOT NULL,
	[rentalTitle] [nvarchar](300) NOT NULL,
	[description] [nvarchar](max) NULL,
	[price] [decimal](18, 0) NULL,
	[roomArea] [decimal](18, 0) NULL,
	[maxPeople] [int] NULL,
	[contactPhoneNumber] [varchar](20) NULL,
	[userId] [int] NULL,
	[viewNumber] [int] NULL,
	[bathroom] [bit] NULL,
	[kitchen] [bit] NULL,
	[bedroomNumber] [int] NULL,
	[location] [nvarchar](500) NULL,
	[googleMap] [varchar](500) NULL,
	[approval] [bit] NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_Rental] PRIMARY KEY CLUSTERED 
(
	[rentalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentalListImage]    Script Date: 5/25/2024 4:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentalListImage](
	[imageId] [int] IDENTITY(1,1) NOT NULL,
	[rentalId] [int] NOT NULL,
	[sourse] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_RentalListImage] PRIMARY KEY CLUSTERED 
(
	[imageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentalVideo]    Script Date: 5/25/2024 4:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentalVideo](
	[videoId] [int] IDENTITY(1,1) NOT NULL,
	[sourse] [nvarchar](1000) NOT NULL,
	[rentalId] [int] NOT NULL,
 CONSTRAINT [PK_RentalVideo] PRIMARY KEY CLUSTERED 
(
	[videoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/25/2024 4:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorRental]    Script Date: 5/25/2024 4:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorRental](
	[sponsorRentalId] [int] IDENTITY(1,1) NOT NULL,
	[rentalId] [int] NOT NULL,
	[sponsorId] [int] NOT NULL,
 CONSTRAINT [PK_SponsorRental] PRIMARY KEY CLUSTERED 
(
	[sponsorRentalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sponsorship]    Script Date: 5/25/2024 4:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sponsorship](
	[sponsorId] [int] IDENTITY(1,1) NOT NULL,
	[sponsorName] [nvarchar](50) NOT NULL,
	[sponsorDes] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Sponsorship] PRIMARY KEY CLUSTERED 
(
	[sponsorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/25/2024 4:16:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[mainPhoneNumber] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](200) NOT NULL,
	[fullname] [nvarchar](50) NOT NULL,
	[secondaryPhoneNumber] [varchar](50) NULL,
	[status] [bit] NOT NULL,
	[roleId] [int] NOT NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Role] FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([roleId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_Role]
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Rental_UserId] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Rental_UserId]
GO
ALTER TABLE [dbo].[RentalListImage]  WITH CHECK ADD  CONSTRAINT [FK_RentalListImage_Rental] FOREIGN KEY([rentalId])
REFERENCES [dbo].[Rental] ([rentalId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RentalListImage] CHECK CONSTRAINT [FK_RentalListImage_Rental]
GO
ALTER TABLE [dbo].[RentalVideo]  WITH CHECK ADD  CONSTRAINT [FK_RentalVideo_Rental] FOREIGN KEY([rentalId])
REFERENCES [dbo].[Rental] ([rentalId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RentalVideo] CHECK CONSTRAINT [FK_RentalVideo_Rental]
GO
ALTER TABLE [dbo].[SponsorRental]  WITH CHECK ADD  CONSTRAINT [FK_SponsorRental_Rental] FOREIGN KEY([rentalId])
REFERENCES [dbo].[Rental] ([rentalId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SponsorRental] CHECK CONSTRAINT [FK_SponsorRental_Rental]
GO
ALTER TABLE [dbo].[SponsorRental]  WITH CHECK ADD  CONSTRAINT [FK_SponsorRental_Sponsor] FOREIGN KEY([sponsorId])
REFERENCES [dbo].[Sponsorship] ([sponsorId])
GO
ALTER TABLE [dbo].[SponsorRental] CHECK CONSTRAINT [FK_SponsorRental_Sponsor]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([roleId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [DotelDB] SET  READ_WRITE 
GO
