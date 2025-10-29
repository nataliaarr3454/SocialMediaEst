using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Queries
{
    public static class PostQueries
    {
        public static string PostQuerySqlServer = @"
                        select Id, UserId, Date, Description, Imagen 
                        from post 
                        order by Date desc
                        OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";
        public static string PostQueryMySQl = @"
                        select Id, UserId, Date, Description, Imagen 
                        from post 
                        order by Date desc
                        LIMIT @Limit
                    ";
        public static string PostComentadosUsuariosActivos = @"
                        SELECT 
                        p.Id AS PostId,
                        p.Description,
                        COUNT(c.Id) AS TotalComentarios
                    FROM Post p
                    JOIN Comment c ON p.Id = c.PostId
                    JOIN User u ON c.UserId = u.Id
                    WHERE u.IsActive = 1        
                    GROUP BY p.Id, p.Description
                    HAVING COUNT(c.Id) > 2
                    ORDER BY TotalComentarios DESC;            
                    ";
        //1
        public static string UsuariosSinComentarios = @"
                SELECT 
                    u.FirstName, 
                    u.LastName, 
                    u.Email
                    FROM [User] u
                    LEFT JOIN Comment c ON u.Id = c.UserId
                    WHERE c.Id IS NULL
                    AND u.IsActive = 1;
                    ";
        //2
        public static string ComentariosTresMesesUsuariosMayores = @"
                SELECT 
                    c.Id AS IdComment,
                    c.Description AS CommentDescription,
                    u.FirstName,
                    u.LastName,
                    DATEDIFF(YEAR, u.BirthDate, GETDATE()) AS Edad
                    FROM Comment c
                    INNER JOIN [User] u ON c.UserId = u.Id
                    WHERE 
                    c.Date >= DATEADD(MONTH, -3, GETDATE())
                    AND DATEDIFF(YEAR, u.BirthDate, GETDATE()) > 25
                    ORDER BY c.Date DESC;
                    ";
        //3
        public static string PostsSinComentariosUsuariosActivos = @"
                SELECT 
                    p.Id AS IdPost,
                    p.Description AS PostDescription,
                    p.Date AS PostDate
                    FROM Post p
                    LEFT JOIN Comment c ON p.Id = c.PostId
                    LEFT JOIN [User] u ON c.UserId = u.Id AND u.IsActive = 1
                    WHERE u.Id IS NULL
                    ORDER BY p.Date DESC;
                    ";
        //4
        public static string UsuariosComentanPostsDiferentes = @"
                SELECT 
                    u.FirstName,
                    u.LastName,
                    COUNT(DISTINCT p.UserId) AS UsuariosDiferentes
                    FROM Comment c
                    INNER JOIN [User] u ON c.UserId = u.Id
                    INNER JOIN Post p ON c.PostId = p.Id
                    GROUP BY u.FirstName, u.LastName
                    HAVING COUNT(DISTINCT p.UserId) >= 3
                    ORDER BY UsuariosDiferentes DESC;
                    ";
        //5
        public static string PostsConComentariosMenores = @"
                  SELECT 
                    p.Id AS IdPost,
                    p.Description AS PostDescription,
                    COUNT(c.Id) AS ComentariosMenores
                    FROM Post p
                    INNER JOIN Comment c ON p.Id = c.PostId
                    INNER JOIN [User] u ON c.UserId = u.Id
                    WHERE DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) < 18
                    GROUP BY p.Id, p.Description
                    HAVING COUNT(c.Id) > 0
                    ORDER BY ComentariosMenores DESC;
                    ";
        //6
        public static string DensidadComentariosPorDia = @"
                SELECT 
                    DATENAME(WEEKDAY, c.Date) AS DiaSemana,
                    COUNT(c.Id) AS TotalComentarios,
                    COUNT(DISTINCT c.UserId) AS UsuariosUnicos
                    FROM Comment c
                    GROUP BY DATENAME(WEEKDAY, c.Date)
                    ORDER BY TotalComentarios DESC;
                    ";
        //7
        public static string CrecimientoMensualComentarios = @"
            WITH ComentariosMensuales AS (
                SELECT 
                    YEAR(c.Date) AS Anio,
                    MONTH(c.Date) AS Mes,
                    COUNT(c.Id) AS TotalComentarios
                    FROM Comment c
                    GROUP BY YEAR(c.Date), MONTH(c.Date)
                    )
                SELECT 
                    Anio,
                    Mes,
                    TotalComentarios,
                    LAG(TotalComentarios, 1) OVER (ORDER BY Anio, Mes) AS MesAnterior,
                        CASE 
                    WHEN LAG(TotalComentarios, 1) OVER (ORDER BY Anio, Mes) = 0 THEN NULL
                        ELSE ROUND(
                        ((CAST(TotalComentarios AS FLOAT) - LAG(TotalComentarios, 1) OVER (ORDER BY Anio, Mes)) 
                        / LAG(TotalComentarios, 1) OVER (ORDER BY Anio, Mes)) * 100, 2
                        )
                        END AS CrecimientoPorcentual
                        FROM ComentariosMensuales
                        ORDER BY Anio, Mes;
                        ";
    }
}
