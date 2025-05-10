USE [master]
GO
/****** Object:  Database [Telefonszamok]    Script Date: 2025. 03. 31. 8:57:34 ******/
CREATE DATABASE [Telefonszamok]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Telefonszamok_NLA01', FILENAME = N'C:\Temp\Telefonszamok_NLA01.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Telefonszamok_NLA01_log', FILENAME = N'C:\Temp\Telefonszamok_NLA01_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Telefonszamok] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Telefonszamok].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Telefonszamok] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Telefonszamok] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Telefonszamok] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Telefonszamok] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Telefonszamok] SET ARITHABORT OFF 
GO
ALTER DATABASE [Telefonszamok] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Telefonszamok] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Telefonszamok] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Telefonszamok] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Telefonszamok] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Telefonszamok] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Telefonszamok] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Telefonszamok] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Telefonszamok] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Telefonszamok] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Telefonszamok] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Telefonszamok] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Telefonszamok] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Telefonszamok] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Telefonszamok] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Telefonszamok] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Telefonszamok] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Telefonszamok] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Telefonszamok] SET  MULTI_USER 
GO
ALTER DATABASE [Telefonszamok] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Telefonszamok] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Telefonszamok] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Telefonszamok] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Telefonszamok] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Telefonszamok] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Telefonszamok] SET QUERY_STORE = OFF
GO
USE [Telefonszamok]
GO
/****** Object:  Table [dbo].[Helyseg]    Script Date: 2025. 03. 31. 8:57:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Helyseg](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[IRSZ] [nvarchar](128) NOT NULL,
	[Nev] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Helyseg] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Szemely]    Script Date: 2025. 03. 31. 8:57:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Szemely](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Vezeteknev] [nvarchar](4000) NULL,
	[Utonev] [nvarchar](4000) NULL,
	[Lakcim] [nvarchar](4000) NULL,
	[enHelysegid] [int] NULL,
 CONSTRAINT [PK_Szemely] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Telefonszamok]    Script Date: 2025. 03. 31. 8:57:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefonszamok](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Szam] [nvarchar](4000) NULL,
	[enSzemelyid] [int] NULL,
 CONSTRAINT [PK_Telefonszamok] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Helyseg] ON 
GO
INSERT [dbo].[Helyseg] ([id], [IRSZ], [Nev]) VALUES (1, N'3400', N'Mezőkövesd')
GO
INSERT [dbo].[Helyseg] ([id], [IRSZ], [Nev]) VALUES (2, N'6000', N'Kecskemét')
GO
INSERT [dbo].[Helyseg] ([id], [IRSZ], [Nev]) VALUES (3, N'1183', N'Budapest')
GO
INSERT [dbo].[Helyseg] ([id], [IRSZ], [Nev]) VALUES (4, N'3500', N'Miskolc')
GO
SET IDENTITY_INSERT [dbo].[Helyseg] OFF
GO
SET IDENTITY_INSERT [dbo].[Szemely] ON 
GO
INSERT [dbo].[Szemely] ([id], [Vezeteknev], [Utonev], [Lakcim], [enHelysegid]) VALUES (1, N'Teszt', N'Elek', N'Valahol', 2)
GO
INSERT [dbo].[Szemely] ([id], [Vezeteknev], [Utonev], [Lakcim], [enHelysegid]) VALUES (2, N'Mekk', N'Elek', N'Futrinka utca', 4)
GO
INSERT [dbo].[Szemely] ([id], [Vezeteknev], [Utonev], [Lakcim], [enHelysegid]) VALUES (3, N'Piszkos', N'Fred', N'Balmoral', 1)
GO
SET IDENTITY_INSERT [dbo].[Szemely] OFF
GO
SET IDENTITY_INSERT [dbo].[Telefonszamok] ON 
GO
INSERT [dbo].[Telefonszamok] ([id], [Szam], [enSzemelyid]) VALUES (1, N'0612223333', 1)
GO
INSERT [dbo].[Telefonszamok] ([id], [Szam], [enSzemelyid]) VALUES (2, N'06203334444', 2)
GO
INSERT [dbo].[Telefonszamok] ([id], [Szam], [enSzemelyid]) VALUES (3, N'06704445555', 2)
GO
INSERT [dbo].[Telefonszamok] ([id], [Szam], [enSzemelyid]) VALUES (4, N'06901111111', 3)
GO
SET IDENTITY_INSERT [dbo].[Telefonszamok] OFF
GO
ALTER TABLE [dbo].[Szemely]  WITH CHECK ADD  CONSTRAINT [FK_Szemely_Helyseg_0] FOREIGN KEY([enHelysegid])
REFERENCES [dbo].[Helyseg] ([id])
GO
ALTER TABLE [dbo].[Szemely] CHECK CONSTRAINT [FK_Szemely_Helyseg_0]
GO
ALTER TABLE [dbo].[Telefonszamok]  WITH CHECK ADD  CONSTRAINT [FK_Telefonszamok_Szemely_0] FOREIGN KEY([enSzemelyid])
REFERENCES [dbo].[Szemely] ([id])
GO
ALTER TABLE [dbo].[Telefonszamok] CHECK CONSTRAINT [FK_Telefonszamok_Szemely_0]
GO
USE [master]
GO
ALTER DATABASE [Telefonszamok] SET  READ_WRITE 
GO
