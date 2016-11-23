USE [BookShopSystem]
GO

SELECT b.Title
FROM Books As b
INNER JOIN CategoryBooks AS cb
ON b.Id = cb.Book_Id
INNER JOIN Categories AS c
ON c.Id = cB.Category_Id
WHERE c.Name = 'crime'