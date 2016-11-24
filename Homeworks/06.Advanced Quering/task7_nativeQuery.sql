SELECT (a.FirstName + ' ' + a.LastName) AS FullName
FROM Authors AS a
WHERE a.FirstName LIKE ('%e')