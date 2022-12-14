USE [master]
GO
/****** Object:  Database [CooperativaDatabase]    Script Date: 04/09/2022 12:21:19 ******/
CREATE DATABASE [CooperativaDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CooperativaDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CooperativaDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CooperativaDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CooperativaDatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CooperativaDatabase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CooperativaDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CooperativaDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CooperativaDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CooperativaDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CooperativaDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CooperativaDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CooperativaDatabase] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CooperativaDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CooperativaDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [CooperativaDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CooperativaDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CooperativaDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CooperativaDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CooperativaDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CooperativaDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CooperativaDatabase] SET QUERY_STORE = OFF
GO
USE [CooperativaDatabase]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 04/09/2022 12:21:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 04/09/2022 12:21:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](500) NOT NULL,
	[PasswordHash] [varbinary](max) NULL,
	[Estado] [varchar](2) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Genero] [varchar](1) NOT NULL,
	[Edad] [int] NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Direccion] [varchar](500) NOT NULL,
	[Telefono] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[FechaNacimiento] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 04/09/2022 12:21:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[CuentaId] [int] IDENTITY(1,1) NOT NULL,
	[NumeroCuenta] [nvarchar](450) NOT NULL,
	[TipoCuenta] [nvarchar](max) NOT NULL,
	[SaldoInicial] [decimal](18, 2) NOT NULL,
	[Estado] [nvarchar](max) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[Creacion] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[CuentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 04/09/2022 12:21:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[MovimientoId] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[TipoMovimiento] [nvarchar](max) NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Saldo] [decimal](18, 2) NOT NULL,
	[CuentaId] [int] NOT NULL,
	[Observacion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Movimientos] PRIMARY KEY CLUSTERED 
(
	[MovimientoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 04/09/2022 12:21:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](500) NOT NULL,
	[PasswordHash] [varbinary](max) NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Genero] [varchar](1) NOT NULL,
	[Edad] [int] NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Direccion] [varchar](500) NOT NULL,
	[Telefono] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[FechaNacimiento] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Cuentas_ClienteId]    Script Date: 04/09/2022 12:21:19 ******/
CREATE NONCLUSTERED INDEX [IX_Cuentas_ClienteId] ON [dbo].[Cuentas]
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Cuentas_NumeroCuenta]    Script Date: 04/09/2022 12:21:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cuentas_NumeroCuenta] ON [dbo].[Cuentas]
(
	[NumeroCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Movimientos_CuentaId]    Script Date: 04/09/2022 12:21:19 ******/
CREATE NONCLUSTERED INDEX [IX_Movimientos_CuentaId] ON [dbo].[Movimientos]
(
	[CuentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [FK_Cuentas_Clientes_ClienteId] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([ClienteId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [FK_Cuentas_Clientes_ClienteId]
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Cuentas_CuentaId] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuentas] ([CuentaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Movimientos] CHECK CONSTRAINT [FK_Movimientos_Cuentas_CuentaId]
GO
USE [master]
GO
ALTER DATABASE [CooperativaDatabase] SET  READ_WRITE 
GO
