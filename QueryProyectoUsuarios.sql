-- Crea la base de datos llamada SistemaUsuariosDB
CREATE DATABASE SistemaUsuariosDB;
GO

USE SistemaUsuariosDB;
GO

-- Crea la tabla Usuarios con campos:
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    FechaNacimiento DATE,
    Sexo CHAR(1)
);
GO

-- Procedimiento almacenado para insertar, modificar, eliminar o consultar usuarios
CREATE PROCEDURE sp_Usuario_CRUD
    @Accion NVARCHAR(10), -- 'INSERTAR', 'MODIFICAR', 'ELIMINAR' o 'CONSULTAR'
    @Id INT = NULL,
    @Nombre NVARCHAR(100) = NULL,
    @FechaNacimiento DATE = NULL,
    @Sexo CHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Accion = 'INSERTAR'
    BEGIN
        INSERT INTO Usuarios (Nombre, FechaNacimiento, Sexo)
        VALUES (@Nombre, @FechaNacimiento, @Sexo);
    END
    ELSE IF @Accion = 'MODIFICAR'
    BEGIN
        UPDATE Usuarios
        SET Nombre = @Nombre,
            FechaNacimiento = @FechaNacimiento,
            Sexo = @Sexo
        WHERE Id = @Id;
    END
    ELSE IF @Accion = 'ELIMINAR'
    BEGIN
        DELETE FROM Usuarios
        WHERE Id = @Id;
    END
    ELSE IF @Accion = 'CONSULTAR'
    BEGIN
        SELECT * FROM Usuarios;
    END
END;

-- Se insertan 20 registros en la tabla Usuarios
INSERT INTO Usuarios (Nombre, FechaNacimiento, Sexo) VALUES 
(N'Carlos P�rez', '1990-05-10', 'M'),
(N'Ana Rodr�guez', '1988-03-22', 'F'),
(N'Juan G�mez', '1995-07-15', 'M'),
(N'Laura Mart�nez', '1992-11-01', 'F'),
(N'Sergio Ram�rez', '1985-01-17', 'M'),
(N'Mar�a L�pez', '1993-04-08', 'F'),
(N'Andr�s Torres', '1991-09-30', 'M'),
(N'Sof�a Vargas', '1996-02-14', 'F'),
(N'Jos� Herrera', '1987-12-25', 'M'),
(N'Camila Jim�nez', '1990-06-06', 'F'),
(N'Luis Castro', '1989-10-10', 'M'),
(N'Valentina R�os', '1994-08-03', 'F'),
(N'Miguel �ngel', '1986-07-21', 'M'),
(N'Daniela Acosta', '1997-03-05', 'F'),
(N'Jorge Pe�a', '1993-05-12', 'M'),
(N'Mariana Duarte', '1995-11-19', 'F'),
(N'Cristian Le�n', '1988-01-01', 'M'),
(N'Isabela M�ndez', '1991-09-09', 'F'),
(N'Felipe Ruiz', '1992-02-22', 'M'),
(N'Luc�a Navarro', '1996-12-31', 'F');
GO
