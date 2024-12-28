using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedSP_and_Views_inDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER   PROC [dbo].[EmpSpInsertOrUpdate](
	@Id INT = 0
	,@EmpName NVARCHAR(300)
	,@Email NVARCHAR(300)
	,@PhoneNumber NVARCHAR(20)
	,@DepartmentId INT
	,@Deleted BIT=0
	,@Gender NVARCHAR(20)
	,@JoiningDate DATETIME,
	@Position NVARCHAR(250)
	,@Salary DECIMAL(18,2)
	,@IsActive BIT = 1
)
AS
BEGIN
BEGIN TRY
    BEGIN TRANSACTION;
	IF(@Id<>0)
	BEGIN
		UPDATE Employees
		SET
		[Name]=@EmpName,
		Email=@Email,
		PhoneNumber=@PhoneNumber,
		DepartmentId=@DepartmentId,
		Deleted=@Deleted,
		Gender=@Gender,
		JoiningDate=@JoiningDate,
		Position=@Position,
		Salary=@Salary,
		IsActive=@IsActive
		WHERE Id = @Id
	SELECT CAST(IIF(@@ROWCOUNT > 0, 1, 0) AS BIT) AS isSuccess;
	END
	ELSE
	BEGIN
		INSERT INTO Employees 
	([Name], Email, PhoneNumber, DepartmentId, Deleted,Gender,JoiningDate,Position,Salary,IsActive)
    VALUES 
	(@EmpName,@Email,@PhoneNumber,@DepartmentId,@Deleted,@Gender,@JoiningDate,@Position,@Salary,@IsActive);
	
	SELECT CAST (IIF(SCOPE_IDENTITY()>0,1,0) AS BIT) isSuccess;
	END

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
	SELECT CAST (0 AS BIT) isSuccess;
END CATCH;
END");


            migrationBuilder.Sql(@"CREATE or ALTER   PROC [dbo].[EmpSpGetEmployeeBySearchingCriteria](
    @SearchName NVARCHAR(100) = NULL,
    @SearchDepartment NVARCHAR(100) = NULL,
    @Position NVARCHAR(100) = NULL,
    @SearchScore DECIMAL(18,2) = NULL,  -- Average performance score
    @PageNumber INT = 1,
    @PageSize INT = 10
)
--EXEC EmpSpGetEmployeeBySearchingCriteria NULL, NULL, NULL, NULL, 1, 10
--EXEC EmpSpGetEmployeeBySearchingCriteria 'Amil',NULL,NULL,NULL,1,10

AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(E.[Id],0) Id
      ,ISNULL(E.[Name],'') [Name]
      ,ISNULL([Email],'') Email
      ,ISNULL([PhoneNumber],'') PhoneNumber
      ,ISNULL([DepartmentId],0) DepartmentId
      ,ISNULL(E.[Deleted],0) Deleted
      ,ISNULL([Gender],'') [Gender]
      ,ISNULL(JoiningDate,GETDATE())JoiningDate
      ,ISNULL([Position],'') [Position]
      ,ISNULL([Salary],0) [Salary]
      ,ISNULL([IsActive],1) [IsActive]
	  ,ISNULL(D.[Name],'') DepartmentName
	  ,CAST(ISNULL(ReviewScore,0) AS decimal(18,2)) ReviewScore
    FROM Employees E
    LEFT JOIN (
        SELECT EmployeeId,
               FORMAT(AVG(ReviewScore), 'N2') AS ReviewScore
        FROM PerformanceReviews
        WHERE Deleted = 0
        GROUP BY EmployeeId
    ) P ON P.EmployeeId = E.Id
    LEFT JOIN Departments D ON D.Id = E.DepartmentId AND D.Deleted = 0
    WHERE E.Deleted = 0
    AND (E.Name LIKE '%' + @SearchName + '%' OR @SearchName IS NULL)
    AND (D.Name LIKE '%' + @SearchDepartment + '%' OR @SearchDepartment IS NULL)
    AND (E.Position LIKE '%' + @Position + '%' OR @Position IS NULL)
    AND (@SearchScore<=P.ReviewScore OR @SearchScore IS NULL OR @SearchScore=0)
    ORDER BY E.Id DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;");

            migrationBuilder.Sql(@"/*
Average performance score for each
department based on employee performance reviews
*/
CREATE OR ALTER VIEW vw_DepartmentPerformanceScores AS
	SELECT 
    D.Name AS Department,
    CAST(ISNULL(AVG(P.ReviewScore), 0) AS decimal(18,2)) AS AveragePerformanceScore
FROM Departments D
INNER JOIN Employees E ON E.DepartmentId = D.Id AND E.Deleted = 0
INNER JOIN PerformanceReviews P ON P.EmployeeId = E.Id AND P.Deleted = 0
WHERE D.Deleted = 0
GROUP BY 
    D.Id, D.Name;
--SELECT * FROM vw_DepartmentPerformanceScores
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
