USE [BdEstudiantes]
GO
/****** Object:  Table [dbo].[Asignaciones]    Script Date: 21/05/2021 03:21:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asignaciones](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AsignacionesEstudiante]    Script Date: 21/05/2021 03:21:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AsignacionesEstudiante](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IdEstudiante] [int] NULL,
	[IdAsignacion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiantes]    Script Date: 21/05/2021 03:21:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiantes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](60) NOT NULL,
	[FechaNacimiento] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AsignacionesEstudiante]  WITH CHECK ADD FOREIGN KEY([IdAsignacion])
REFERENCES [dbo].[Asignaciones] ([ID])
GO
ALTER TABLE [dbo].[AsignacionesEstudiante]  WITH CHECK ADD FOREIGN KEY([IdEstudiante])
REFERENCES [dbo].[Estudiantes] ([ID])
GO
/****** Object:  StoredProcedure [dbo].[sp_AgregarAsignacion]    Script Date: 21/05/2021 03:21:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AgregarAsignacion]
(
    @Nombre VARCHAR(50)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ID INT,
            @ERROR VARCHAR(500);
    BEGIN TRY
        IF EXISTS
        (
            SELECT *
            FROM Asignaciones
            WHERE Nombre = @Nombre
        )
		BEGIN
			SELECT @ERROR = 'La asignacion ya existe.';
            THROW 50000, @ERROR, 1; 
		END
		ELSE
        BEGIN
            INSERT INTO Asignaciones
			(
				Nombre
			)
			VALUES
			(
				@Nombre
			);
			SELECT @ID = SCOPE_IDENTITY();
			SELECT @ID AS ERROR, 'Registro exitoso' AS MENSAJEERROR;
        END;

    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SELECT ERROR_NUMBER() * -1 AS ERROR,
               ISNULL(@ERROR, 'Error al actualizar el registro.') AS MENSAJEERROR;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_AgregarEstudiante]    Script Date: 21/05/2021 03:21:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AgregarEstudiante]
(
    @Nombre VARCHAR(50),  
    @FechaNacimiento DATE
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ID INT,
            @ERROR VARCHAR(500);
    BEGIN TRY
        IF EXISTS
        (
            SELECT *
            FROM Estudiantes
            WHERE Nombre = @Nombre
        )
		BEGIN
			SELECT @ERROR = 'El estudiante ya existe.';
            THROW 50000, @ERROR, 1; 
		END
		ELSE
        BEGIN
            INSERT INTO Estudiantes
			(
				Nombre
				,FechaNacimiento
			)
			VALUES
			(
				@Nombre
				,@FechaNacimiento-- Activo - bit
			);
			SELECT @ID = SCOPE_IDENTITY();
			SELECT @ID AS ERROR, 'Registro exitoso' AS MENSAJEERROR;
        END;

    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SELECT ERROR_NUMBER() * -1 AS ERROR,
               ISNULL(@ERROR, 'Error al actualizar el registro.') AS MENSAJEERROR;
    END CATCH;
END;


GO
/****** Object:  StoredProcedure [dbo].[sp_InscribirAlumno]    Script Date: 21/05/2021 03:21:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InscribirAlumno]
(
    @IdEstudiante int
	,@IdAsignacion int
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ID INT,
            @ERROR VARCHAR(500);
    BEGIN TRY
		IF EXISTS
        ((
            select
				e.ID
				,e.Nombre
				,a.Nombre
			from  
				AsignacionesEstudiante ae 
				inner join Estudiantes e on e.ID = ae.IdEstudiante
				inner join Asignaciones a on a.ID = ae.IdAsignacion
			WHERE ae.IdAsignacion = @IdAsignacion and ae.IdEstudiante = @IdEstudiante
        )) 		
		BEGIN
			SELECT @ERROR = 'El alumno ya esta asignado a un curso.';
            THROW 50000, @ERROR, 1; 	
		END
        ELSE IF EXISTS
        ((
            SELECT *
            FROM Asignaciones
            WHERE ID = @IdAsignacion
        )) 
		AND
		EXISTS
		((
            SELECT *
            FROM Estudiantes
            WHERE ID = @IdEstudiante
        )) 
		BEGIN
			INSERT INTO AsignacionesEstudiante
			(
				IdAsignacion
				,IdEstudiante
			)
			VALUES
			(
				@IdAsignacion
				,@IdEstudiante
			);	
			SELECT @ID = SCOPE_IDENTITY();
			SELECT @ID AS ERROR, 'Registro exitoso' AS MENSAJEERROR;
		END
		ELSE
        BEGIN
            SELECT @ERROR = 'El alumno o asignacion no existe.';
            THROW 50000, @ERROR, 1; 			
        END;

    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SELECT ERROR_NUMBER() * -1 AS ERROR,
               ISNULL(@ERROR, 'Error al actualizar el registro.') AS MENSAJEERROR;
    END CATCH;
END;
GO
