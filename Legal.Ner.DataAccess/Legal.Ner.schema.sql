USE [master]
GO
/****** Object:  Database [Legal.Ner]    Script Date: 12/09/2017 10:20:10 a.m. ******/
CREATE DATABASE [Legal.Ner]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Legal.Ner', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.LOCALDB\MSSQL\DATA\Legal.Ner.mdf' , SIZE = 204800KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Legal.Ner_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.LOCALDB\MSSQL\DATA\Legal.Ner_log.ldf' , SIZE = 204800KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Legal.Ner].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Legal.Ner] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Legal.Ner] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Legal.Ner] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Legal.Ner] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Legal.Ner] SET ARITHABORT OFF 
GO
ALTER DATABASE [Legal.Ner] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Legal.Ner] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Legal.Ner] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Legal.Ner] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Legal.Ner] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Legal.Ner] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Legal.Ner] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Legal.Ner] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Legal.Ner] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Legal.Ner] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Legal.Ner] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Legal.Ner] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Legal.Ner] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Legal.Ner] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Legal.Ner] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Legal.Ner] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Legal.Ner] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Legal.Ner] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Legal.Ner] SET  MULTI_USER 
GO
ALTER DATABASE [Legal.Ner] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Legal.Ner] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Legal.Ner] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Legal.Ner] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Legal.Ner] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Legal.Ner] SET QUERY_STORE = OFF
GO
USE [Legal.Ner]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Legal.Ner]
GO
/****** Object:  Table [dbo].[Entity]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entity](
	[Eid] [nvarchar](50) NOT NULL,
	[EntityType] [nvarchar](50) NULL,
	[FileKey_Id] [int] NOT NULL,
 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED 
(
	[Eid] ASC,
	[FileKey_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EntityBulk]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityBulk](
	[Eid] [nvarchar](100) NOT NULL,
	[EntityType] [nvarchar](100) NULL,
	[EntityName] [nvarchar](max) NULL,
	[FileKey_Id] [int] NOT NULL,
	[Added] [bit] NULL,
 CONSTRAINT [PK_EntityBulk] PRIMARY KEY CLUSTERED 
(
	[Eid] ASC,
	[FileKey_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EntityTerm]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityTerm](
	[Eid] [nvarchar](50) NOT NULL,
	[FileKey_id] [int] NOT NULL,
	[Tid] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EntityTerm] PRIMARY KEY CLUSTERED 
(
	[Eid] ASC,
	[FileKey_id] ASC,
	[Tid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileKey]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileKey](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](200) NULL,
	[UploadDate] [datetime] NULL,
	[DocumentTitle] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[ReleaseDate] [datetime] NULL,
	[Number] [int] NULL,
 CONSTRAINT [PK_FileKey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NonTerminal]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NonTerminal](
	[Id] [nvarchar](50) NOT NULL,
	[Label] [nvarchar](100) NULL,
	[FileKey_Id] [int] NOT NULL,
	[Tree_Id] [int] NOT NULL,
 CONSTRAINT [PK_NonTerminal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[FileKey_Id] ASC,
	[Tree_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Term]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Term](
	[Tid] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[Lemma] [nvarchar](100) NULL,
	[Pos] [nvarchar](50) NULL,
	[Morphofeat] [nvarchar](100) NULL,
	[Wid] [nvarchar](50) NULL,
	[FileKey_Id] [int] NOT NULL,
 CONSTRAINT [PK_Term] PRIMARY KEY CLUSTERED 
(
	[Tid] ASC,
	[FileKey_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Terminal]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Terminal](
	[Id] [nvarchar](50) NOT NULL,
	[Tid] [nvarchar](50) NULL,
	[FileKey_Id] [int] NOT NULL,
	[Tree_Id] [int] NOT NULL,
 CONSTRAINT [PK_Terminal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[FileKey_Id] ASC,
	[Tree_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tree]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tree](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileKey_Id] [int] NOT NULL,
 CONSTRAINT [PK_Tree] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[FileKey_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TreeEdge]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreeEdge](
	[Id] [nvarchar](50) NOT NULL,
	[FromNode] [nvarchar](50) NULL,
	[ToNode] [nvarchar](50) NULL,
	[Head] [bit] NULL,
	[FileKey_Id] [int] NOT NULL,
	[Tree_Id] [int] NOT NULL,
 CONSTRAINT [PK_TreeEdge] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[FileKey_Id] ASC,
	[Tree_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Word]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Word](
	[Wid] [nvarchar](50) NOT NULL,
	[Sent] [nvarchar](20) NULL,
	[Para] [nvarchar](20) NULL,
	[Offset] [nvarchar](20) NULL,
	[Length] [nvarchar](20) NULL,
	[Value] [nvarchar](100) NULL,
	[FileKey_Id] [int] NOT NULL,
 CONSTRAINT [PK_Word] PRIMARY KEY CLUSTERED 
(
	[Wid] ASC,
	[FileKey_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Entity]  WITH CHECK ADD  CONSTRAINT [FK_Entity_FileKey] FOREIGN KEY([FileKey_Id])
REFERENCES [dbo].[FileKey] ([Id])
GO
ALTER TABLE [dbo].[Entity] CHECK CONSTRAINT [FK_Entity_FileKey]
GO
ALTER TABLE [dbo].[EntityTerm]  WITH CHECK ADD  CONSTRAINT [FK_EntityTerm_FileKey] FOREIGN KEY([FileKey_id])
REFERENCES [dbo].[FileKey] ([Id])
GO
ALTER TABLE [dbo].[EntityTerm] CHECK CONSTRAINT [FK_EntityTerm_FileKey]
GO
ALTER TABLE [dbo].[Term]  WITH CHECK ADD  CONSTRAINT [FK_Term_FileKey] FOREIGN KEY([FileKey_Id])
REFERENCES [dbo].[FileKey] ([Id])
GO
ALTER TABLE [dbo].[Term] CHECK CONSTRAINT [FK_Term_FileKey]
GO
ALTER TABLE [dbo].[Tree]  WITH CHECK ADD  CONSTRAINT [FK_Tree_FileKey] FOREIGN KEY([FileKey_Id])
REFERENCES [dbo].[FileKey] ([Id])
GO
ALTER TABLE [dbo].[Tree] CHECK CONSTRAINT [FK_Tree_FileKey]
GO
ALTER TABLE [dbo].[Word]  WITH CHECK ADD  CONSTRAINT [FK_Word_FileKey] FOREIGN KEY([FileKey_Id])
REFERENCES [dbo].[FileKey] ([Id])
GO
ALTER TABLE [dbo].[Word] CHECK CONSTRAINT [FK_Word_FileKey]
GO
/****** Object:  StoredProcedure [dbo].[BulkEntitiesByFileKey]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BulkEntitiesByFileKey] 
	-- Add the parameters for the stored procedure here
	@FileKey int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO EntityBulk(Eid, EntityType, EntityName, FileKey_Id)

	SELECT 
	  Eid,
	  EntityType,
	  STUFF((
		SELECT '  ' + CAST(w.Value AS NVARCHAR(MAX))
		FROM Entity e
		INNER JOIN EntityTerm et ON et.Eid = e.Eid
			AND e.FileKey_Id = et.FileKey_id
		INNER JOIN Term t ON t.Tid = et.Tid
			AND t.FileKey_Id = et.FileKey_id
		INNER JOIN Word w ON w.Wid = t.Wid
			AND t.FileKey_Id = w.FileKey_id
		WHERE (e.Eid = Results.Eid
			AND e.FileKey_Id = @FileKey) 
		ORDER BY w.Wid
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)'),1,2,'') AS EntityName
		, @FileKey
	FROM Entity Results
	WHERE FileKey_Id = @FileKey
	GROUP BY Eid,
		EntityType
	ORDER BY CAST(SUBSTRING(Eid, 2, 10) AS INT)
END

GO
/****** Object:  StoredProcedure [dbo].[GetTreeEdgeByTreeAndFileKey]    Script Date: 12/09/2017 10:20:10 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTreeEdgeByTreeAndFileKey]
	-- Add the parameters for the stored procedure here
	@TreeId int,
	@FileKeyId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT Id
	, FromNode
	, Value_From
	, ToNode
	, Value_To
	, Head
	FROm
(
	SELECT t.Id
	, t.FromNode
	, n.Label AS Value_From
	, t.ToNode
	, n1.Label AS Value_To
	, t.Head
	FROM TreeEdge t
	INNER JOIN NonTerminal n ON n.Id = t.FromNode
		AND t.Tree_Id = n.Tree_Id
		AND t.FileKey_Id = n.FileKey_Id
	INNER JOIN NonTerminal n1 ON n1.Id = t.ToNode
		AND t.Tree_Id = n1.Tree_Id
		AND t.FileKey_Id = n1.FileKey_Id
	WHERE t.Tree_Id = @TreeId
		AND t.FileKey_Id = @FileKeyId

	UNION 

	SELECT t.Id
	, t.FromNode
	, w.value AS Value_From
	, t.ToNode
	, n1.Label AS Value_To
	, t.Head
	FROM TreeEdge t
	INNER JOIN Terminal n ON n.Id = t.FromNode
		AND t.Tree_Id = n.Tree_Id
		AND t.FileKey_Id = n.FileKey_Id
	INNER JOIN Term te ON te.Tid = n.Tid
		AND te.FileKey_Id = n.FileKey_Id
	INNER JOIN Word w ON w.Wid = te.Wid
		AND w.FileKey_Id = te.FileKey_Id
	INNER JOIN NonTerminal n1 ON n1.Id = t.ToNode
		AND t.Tree_Id = n1.Tree_Id
		AND t.FileKey_Id = n1.FileKey_Id
	WHERE t.Tree_Id = @TreeId
		AND t.FileKey_Id = @FileKeyId

	UNION

	SELECT t.Id
	, t.FromNode
	, n.Label AS Value_From
	, t.ToNode
	, w.value AS Value_To
	, t.Head
	FROM TreeEdge t
	INNER JOIN NonTerminal n ON n.Id = t.FromNode
		AND t.Tree_Id = n.Tree_Id
		AND t.FileKey_Id = n.FileKey_Id
	INNER JOIN Terminal n1 ON n1.Id = t.ToNode
		AND t.Tree_Id = n1.Tree_Id
		AND t.FileKey_Id = n1.FileKey_Id
	INNER JOIN Term te ON te.Tid = n1.Tid
		AND te.FileKey_Id = n1.FileKey_Id
	INNER JOIN Word w ON w.Wid = te.Wid
		AND w.FileKey_Id = te.FileKey_Id
	WHERE t.Tree_Id = @TreeId
		AND t.FileKey_Id = @FileKeyId
		) AS Total
ORDER BY CAST(SUBSTRING(Total.Id, 4, 10) AS INT)

END 

GO
USE [master]
GO
ALTER DATABASE [Legal.Ner] SET  READ_WRITE 
GO
