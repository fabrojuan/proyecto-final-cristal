# proyecto-final-cristal




Desde la consola de administracion depaquetes ejecutar:

Scaffold-DbContext "Data Source=NB-ROMAN;Initial Catalog=M_VPSA_V3;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Modelos -force


dotnet ef dbcontext scaffold "Server=tcp:cristal-sql.database.windows.net,1433;Initial Catalog=M_VPSA_V3;Persist Security Info=False;User ID=cristal;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Modelos --force