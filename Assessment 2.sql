SELECT PlatformName, Id, PlatformId, UniqueName, Latitude, Longitude, CreatedAt, UpdatedAt
FROM (
  SELECT P.UniqeuName AS PlatformName, w.Id, w.PlatformId, w.UniqueName, w.Latitude, w.Longitude, w.CreatedAt, w.UpdatedAt, ROW_NUMBER() OVER (PARTITION BY w.PlatformId ORDER BY w.UpdatedAt DESC) AS rn
  FROM [dbo].[Wells] w
  INNER JOIN [dbo].[Platforms] P ON P.Id = w.PlatformId
) AS subquery
WHERE rn = 1;
