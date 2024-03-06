USE [BackendTZ]
GO
/****** Object:  StoredProcedure [dbo].[GetStatistic]    Script Date: 06.03.2024 19:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetStatistic]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT v.[Name] AS ValueName, v.[Probability], COUNT(v.[Name]) AS ValueCount, k.[Name] AS KeyName, k.[Id] AS KeyId
	FROM [dbo].[Experiment] AS e
	INNER JOIN [dbo].[Key] AS k ON k.[Id] = e.KeyId
	INNER JOIN [dbo].[Value] AS v ON v.[Id] = e.[ValueId]
	GROUP BY v.[Name], k.[Name], v.[Probability], k.[Id]
	ORDER BY k.[Id], ValueCount DESC
END
