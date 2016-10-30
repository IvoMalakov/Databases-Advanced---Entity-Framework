USE SoftUni
GO

--Task 3--
SELECT e.FirstName, 
	e.LastName, 
	e.MiddleName, 
	e.JobTitle, 
	e.Salary
FROM Employees AS e

--Task 4--
SELECT e.FirstName
FROM Employees AS e
WHERE e.Salary > 50000

--Task 5--
SELECT e.FirstName,
	e.LastName,
	d.Name,
	e.Salary
FROM Employees AS e
INNER JOIN Departments AS d
	ON d.DepartmentID = e.DepartmentID
WHERE d.Name = 'Research and Development'
ORDER BY e.Salary ASC,
	e.FirstName DESC

--Task 6--
SELECT *
FROM Employees AS e
WHERE e.LastName = 'Nakov'

SELECT a.AddressID
FROM Addresses AS a
WHERE a.AddressText = 'Vitoshka 15'

SELECT TOP 10 a.AddressText
FROM Employees AS e
INNER JOIN Addresses AS a
	ON a.AddressID = e.AddressID
ORDER BY e.AddressID DESC

--Task 7--
SELECT *
FROM Projects as p
WHERE p.ProjectID = 2

DELETE FROM EmployeesProjects
WHERE ProjectID = 2

DELETE FROM Projects
WHERE ProjectID = 2

SELECT TOP 10 p.Name
FROM Projects AS p
