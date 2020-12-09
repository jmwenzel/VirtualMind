USE [master]
GO
/****** Object:  Database [CurrencyExchange]    Script Date: 8/12/2020 21:18:25 ******/
CREATE DATABASE [CurrencyExchange]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CurrencyExchange', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CurrencyExchange.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CurrencyExchange_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CurrencyExchange_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CurrencyExchange] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CurrencyExchange].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CurrencyExchange] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ARITHABORT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CurrencyExchange] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CurrencyExchange] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CurrencyExchange] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CurrencyExchange] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CurrencyExchange] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CurrencyExchange] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CurrencyExchange] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CurrencyExchange] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CurrencyExchange] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CurrencyExchange] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CurrencyExchange] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CurrencyExchange] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CurrencyExchange] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CurrencyExchange] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CurrencyExchange] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CurrencyExchange] SET  MULTI_USER 
GO
ALTER DATABASE [CurrencyExchange] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CurrencyExchange] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CurrencyExchange] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CurrencyExchange] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CurrencyExchange] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CurrencyExchange] SET QUERY_STORE = OFF
GO
USE [CurrencyExchange]
GO

/****** Object:  Table [dbo].[Transaction]    Script Date: 8/12/2020 21:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PesosAmount] [decimal](18, 2) NOT NULL,
	[Currency] [varchar](10) NOT NULL,
	[CurrencyExchange] [decimal](18, 3) NOT NULL,
	[ExchangedAmount] [decimal](18, 2) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  StoredProcedure [dbo].[GetTotalTransactions]    Script Date: 8/12/2020 21:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetTotalTransactions]
	@UserId	INT,
	@Currency VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @FirstDay DATE,
			@LastDay DATE

	SET @FirstDay = (SELECT DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0))
	SET @LastDay = (SELECT DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE()), -1))

	SELECT SUM(ExchangedAmount) AS TotalTransactions
	FROM [Transaction]
	WHERE UserId = @UserId 
		AND Currency = @Currency
		AND CreateDate BETWEEN @FirstDay AND @LastDay
	GROUP BY UserId, Currency

END
GO
USE [master]
GO
ALTER DATABASE [CurrencyExchange] SET  READ_WRITE 
GO
