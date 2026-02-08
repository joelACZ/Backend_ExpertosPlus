-- 1. Crear la base de datos
CREATE DATABASE [oficiosLocales];
GO

-- 2. Usar la base de datos creada
USE [oficiosLocales];
GO


-- 3. Crear la tabla de Usuarios
CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY, -- Se incrementa solo
    [Username] NVARCHAR(50) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    [Role] NVARCHAR(20) NOT NULL
);
GO


-- 4. Insertar el rol administrador inicial
INSERT INTO [dbo].[Users] ([Username], [Password], [Role])
VALUES ('administrador', 'administrador123', 'Administrador');
GO

-- 5. Verificar la creación
SELECT * FROM [dbo].[Users];


CREATE TABLE categorias (
nombre NVARCHAR(100) PRIMARY KEY
);
GO


CREATE TABLE profesionales (
id INT IDENTITY(1,1) PRIMARY KEY,
nombre NVARCHAR(100) NOT NULL,
email NVARCHAR(100) NOT NULL,
telefono BIGINT NOT NULL,
direccion NVARCHAR(255),
especialidad NVARCHAR(100) NOT NULL,
oficios NVARCHAR(MAX) NOT NULL,
experiencia INT NOT NULL,
disponibilidad BIT NOT NULL
);
GO

CREATE TABLE clientes (
id INT IDENTITY(1,1) PRIMARY KEY,
nombre NVARCHAR(100) NOT NULL,
email NVARCHAR(100) NOT NULL,
telefono BIGINT NOT NULL,
direccion NVARCHAR(255),
preferencias NVARCHAR(MAX) NOT NULL,
notificaciones BIT NOT NULL
);
GO

CREATE TABLE servicios (
id INT IDENTITY(1,1) PRIMARY KEY,
nombre NVARCHAR(100) NOT NULL,
categoria NVARCHAR(100) NOT NULL,
descripcion NVARCHAR(MAX) NOT NULL,
precioBase DECIMAL(10, 2) NOT NULL,
duracionEstimada INT NOT NULL,
profesional_id INT,
activo BIT NOT NULL,
estado NVARCHAR(50),
CONSTRAINT FK_Servicios_Profesionales FOREIGN KEY (profesional_id) REFERENCES profesionales(id),
CONSTRAINT FK_Servicios_Categorias FOREIGN KEY (categoria) REFERENCES categorias(nombre)
);
GO

CREATE TABLE solicitudes (
id INT IDENTITY(1,1) PRIMARY KEY,
cliente_id INT NOT NULL,
servicio_id INT NOT NULL,
fecha DATETIME NOT NULL,
estado NVARCHAR(50) NOT NULL,
nivelUrgencia NVARCHAR(20) NOT NULL,
descripcion NVARCHAR(MAX) NOT NULL,
ubicacion NVARCHAR(255) NOT NULL,
fechaCreacion DATETIME DEFAULT GETDATE(),
fechaActualizacion DATETIME,
CONSTRAINT FK_Solicitudes_Clientes FOREIGN KEY (cliente_id) REFERENCES clientes(id),
CONSTRAINT FK_Solicitudes_Servicios FOREIGN KEY (servicio_id) REFERENCES servicios(id)
);
GO

CREATE TABLE resenas (
id INT IDENTITY(1,1) PRIMARY KEY,
servicio_id INT NOT NULL,
cliente_id INT NOT NULL,
calificacion INT NOT NULL CHECK (calificacion >= 1 AND calificacion <= 5),
comentario NVARCHAR(MAX) NOT NULL,
fecha DATETIME NOT NULL,
anonima BIT NOT NULL,
fechaCreacion DATETIME DEFAULT GETDATE(),
fechaActualizacion DATETIME,
CONSTRAINT FK_Resenas_Servicios FOREIGN KEY (servicio_id) REFERENCES servicios(id),
CONSTRAINT FK_Resenas_Clientes FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);
GO


INSERT INTO categorias (nombre) VALUES
('Mantenimiento'),
('Tecnolog�a'),
('Salud y Belleza'),
('Construcci�n'),
('Limpieza'),
('Educaci�n'),
('Jardiner�a'),
('Automotriz'),
('Electrodom�sticos'),
('Dise�o'),
('Marketing'),
('Fotograf�a'),
('Plomer�a'),
('Carpinter�a'),
('Pintura'),
('Seguridad'),
('Eventos'),
('Belleza'),
('Deportes'),
('Consultor�a'),
('Legal'),
('Contabilidad'),
('Redes'),
('Soporte T�cnico'),
('Arquitectura'),
('Mudanzas'),
('Decoraci�n'),
('Log�stica'),
('Energ�a'),
('Otros');
GO

/* =========================
   PROFESIONALES (10)
   ========================= */
INSERT INTO profesionales
(nombre, email, telefono, direccion, especialidad, oficios, experiencia, disponibilidad)
VALUES
('Juan P�rez','juan@mail.com',999111111,'Av. Central 123','Plomer�a','Plomer�a general',8,1),
('Mar�a L�pez','maria@mail.com',999111112,'Calle Norte 45','Electricidad','Instalaciones el�ctricas',6,1),
('Carlos Ruiz','carlos@mail.com',999111113,'Av. Sur 88','Carpinter�a','Muebles a medida',10,1),
('Ana Torres','ana@mail.com',999111114,'Barrio Centro','Limpieza','Limpieza residencial',4,1),
('Pedro G�mez','pedro@mail.com',999111115,'Av. Los Andes','Tecnolog�a','Soporte t�cnico',7,1),
('Luc�a Morales','lucia@mail.com',999111116,'Calle 10','Dise�o','Dise�o gr�fico',5,1),
('Jorge Vega','jorge@mail.com',999111117,'Av. Pac�fico','Pintura','Pintura interior/exterior',9,0),
('Sof�a R�os','sofia@mail.com',999111118,'Zona Norte','Jardiner�a','Mantenimiento de jardines',6,1),
('Miguel Castro','miguel@mail.com',999111119,'Calle Sur','Automotriz','Mec�nica b�sica',11,1),
('Elena Paredes','elena@mail.com',999111120,'Av. Industrial','Electrodom�sticos','Reparaci�n de electrodom�sticos',8,1);
GO

/* =========================
   CLIENTES (10)
   ========================= */
INSERT INTO clientes
(nombre, email, telefono, direccion, preferencias, notificaciones)
VALUES
('Luis Andrade','luis@mail.com',988221111,'Centro','Rapidez',1),
('Paola N��ez','paola@mail.com',988221112,'Norte','Precio',1),
('Ricardo Salas','ricardo@mail.com',988221113,'Sur','Experiencia',0),
('Valeria Mena','valeria@mail.com',988221114,'Este','Calidad',1),
('Diego Flores','diego@mail.com',988221115,'Oeste','Disponibilidad',1),
('Camila Ortiz','camila@mail.com',988221116,'Centro','Rese�as',1),
('Andr�s Le�n','andres@mail.com',988221117,'Norte','Urgencia',0),
('Natalia Cruz','natalia@mail.com',988221118,'Sur','Confianza',1),
('Fernando Lima','fernando@mail.com',988221119,'Este','Soporte',1),
('Daniela Soto','daniela@mail.com',988221120,'Oeste','Comunicaci�n',1);
GO

/* =========================
   SERVICIOS (10)
   ========================= */
INSERT INTO servicios
(nombre, categoria, descripcion, precioBase, duracionEstimada, profesional_id, activo, estado)
VALUES
('Reparaci�n de tuber�as','Plomer�a','Arreglo de fugas',50.00,120,1,1,'Disponible'),
('Instalaci�n el�ctrica','Tecnolog�a','Cableado b�sico',80.00,180,2,1,'Disponible'),
('Muebles a medida','Carpinter�a','Fabricaci�n personalizada',200.00,480,3,1,'Disponible'),
('Limpieza profunda','Limpieza','Limpieza completa hogar',60.00,150,4,1,'Disponible'),
('Soporte PC','Soporte T�cnico','Mantenimiento PC',40.00,90,5,1,'Disponible'),
('Dise�o logo','Dise�o','Identidad visual',120.00,240,6,1,'Disponible'),
('Pintura vivienda','Pintura','Pintura general',150.00,360,7,0,'No disponible'),
('Mantenimiento jard�n','Jardiner�a','Corte y poda',55.00,120,8,1,'Disponible'),
('Revisi�n veh�culo','Automotriz','Chequeo general',70.00,120,9,1,'Disponible'),
('Reparaci�n lavadora','Electrodom�sticos','Diagn�stico y arreglo',65.00,150,10,1,'Disponible');
GO

/* =========================
   SOLICITUDES (10)
   ========================= */
INSERT INTO solicitudes
(cliente_id, servicio_id, fecha, estado, nivelUrgencia, descripcion, ubicacion)
VALUES
(1,1,GETDATE(),'Pendiente','Alta','Fuga urgente','Centro'),
(2,2,GETDATE(),'Pendiente','Media','Instalaci�n nueva','Norte'),
(3,3,GETDATE(),'Confirmada','Baja','Mueble cocina','Sur'),
(4,4,GETDATE(),'Pendiente','Media','Limpieza general','Este'),
(5,5,GETDATE(),'Confirmada','Alta','PC lenta','Oeste'),
(6,6,GETDATE(),'Pendiente','Baja','Logo marca','Centro'),
(7,7,GETDATE(),'Cancelada','Media','Pintura sala','Norte'),
(8,8,GETDATE(),'Confirmada','Baja','Jard�n peque�o','Sur'),
(9,9,GETDATE(),'Pendiente','Media','Chequeo auto','Este'),
(10,10,GETDATE(),'Confirmada','Alta','Lavadora no enciende','Oeste');
GO

/* =========================
   RESE�AS (10)
   ========================= */
INSERT INTO resenas
(servicio_id, cliente_id, calificacion, comentario, fecha, anonima)
VALUES
(1,1,5,'Excelente servicio',GETDATE(),0),
(2,2,4,'Buen trabajo',GETDATE(),1),
(3,3,5,'Muy profesional',GETDATE(),0),
(4,4,3,'Aceptable',GETDATE(),1),
(5,5,4,'R�pido y eficaz',GETDATE(),0),
(6,6,5,'Dise�o impecable',GETDATE(),0),
(7,7,2,'Demora excesiva',GETDATE(),1),
(8,8,4,'Buen resultado',GETDATE(),0),
(9,9,5,'Muy confiable',GETDATE(),0),
(10,10,4,'Reparaci�n correcta',GETDATE(),1);
GO



-- Eliminar datos en el orden correcto para respetar las restricciones de clave for�nea
-- Primero las tablas que dependen de otras

DELETE FROM resenas;
GO

DELETE FROM solicitudes;
GO

DELETE FROM servicios;
GO

-- Ahora las tablas principales
DELETE FROM profesionales;
GO

DELETE FROM clientes;
GO

DELETE FROM categorias;
GO

-- Si tambi�n deseas reiniciar los contadores de identidad (opcional):
DBCC CHECKIDENT ('profesionales', RESEED, 0);
DBCC CHECKIDENT ('clientes', RESEED, 0);
DBCC CHECKIDENT ('servicios', RESEED, 0);
DBCC CHECKIDENT ('solicitudes', RESEED, 0);
DBCC CHECKIDENT ('resenas', RESEED, 0);
GO


