-- Implementación BD --
USE master
GO

IF db_id('BDCOLEGIO') is not null
begin
	ALTER DATABASE BDCOLEGIO
	SET SINGLE_USER WITH ROLLBACK IMMEDIATE

	DROP DATABASE BDCOLEGIO
end
go

CREATE DATABASE BDCOLEGIO
COLLATE Modern_Spanish_CI_AI
GO

SET LANGUAGE SPANISH
SET NOCOUNT ON
GO

USE BDCOLEGIO
GO

-- Creando las tablas --
--- Distrito ---
CREATE TABLE tb_distrito (
	dis_cod INT NOT NULL PRIMARY KEY,
	dis_desc VARCHAR(50) NOT NULL
)
GO

--- Direción ubi --
CREATE TABLE tb_dirPre (
	dirPre_cod INT NOT NULL PRIMARY KEY,
	dirPre_desc VARCHAR(50) NOT NULL
)
GO

--- Tipo de documento ---
CREATE TABLE tb_tipDoc(
	tipDoc_cod INT NOT NULL PRIMARY KEY,
	tipDoc_desc VARCHAR(50) NOT NULL
)
GO

--- Grado ---
CREATE TABLE tb_grado(
	gra_cod INT NOT NULL PRIMARY KEY,
	gra_desc VARCHAR(50) NOT NULL
)
GO

--- Curso ---
CREATE TABLE tb_curso(
	cur_cod INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	cur_desc VARCHAR(50) NOT NULL,
	cur_eli CHAR(2) DEFAULT 'No' 
)
GO

--- Género ---
CREATE TABLE tb_genero(
	gen_cod INT NOT NULL PRIMARY KEY,
	gen_desc VARCHAR(50) NOT NULL
)

--- Estudiantes ---
CREATE TABLE tb_estudiante (
	est_cod INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	est_nom VARCHAR(50) NOT NULL,
	est_apePat VARCHAR(50) NOT NULL,
	est_apeMat VARCHAR(50) NOT NULL, 
	est_fechNac DATE NOT NULL,
    dis_cod INT NOT NULL FOREIGN KEY REFERENCES tb_distrito(dis_cod),
	dirPre_cod INT NOT NULL FOREIGN KEY REFERENCES tb_dirPre(dirPre_cod),
	est_dir VARCHAR(50) NOT NULL,
	est_tel INT NOT NULL,
	gen_cod INT NOT NULL FOREIGN KEY REFERENCES tb_genero(gen_cod),
	tipDoc_cod INT NOT NULL FOREIGN KEY REFERENCES tb_tipDoc(tipDoc_cod),
	est_docN INT NOT NULL,
	suscribed INT DEFAULT(0) NOT NULL,
	est_eli CHAR(2) DEFAULT 'No' 
)
GO

--- Profesores ---
CREATE TABLE tb_profesor (
	pro_cod INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	pro_nom VARCHAR(50) NOT NULL,
	pro_apePat VARCHAR(50) NOT NULL,
	pro_apeMat VARCHAR(50) NOT NULL, 
	pro_fechNac DATE NOT NULL,
    dis_cod INT NOT NULL FOREIGN KEY REFERENCES tb_distrito(dis_cod),
	dirPre_cod INT NOT NULL FOREIGN KEY REFERENCES tb_dirPre(dirPre_cod),
	pro_dir VARCHAR(50) NOT NULL,
	pro_tel INT NOT NULL,
	gen_cod INT NOT NULL FOREIGN KEY REFERENCES tb_genero(gen_cod),
	tipDoc_cod INT NOT NULL FOREIGN KEY REFERENCES tb_tipDoc(tipDoc_cod),
	pro_docN INT NOT NULL,
	pro_suel MONEY NOT NULL,
	pro_eli CHAR(2) DEFAULT 'No' 
)
GO

--- Grado-Curso ---
CREATE TABLE tb_gradoCurso (
	graCur_cod INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	gra_cod INT NOT NULL FOREIGN KEY REFERENCES tb_grado(gra_cod),
	cur_cod INT NOT NULL FOREIGN KEY REFERENCES tb_curso(cur_cod),
	graCur_eli CHAR(2) DEFAULT 'No' 
)
GO

--- Salón ---
CREATE TABLE tb_salon (
	sal_cod INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	pro_cod INT NOT NULL FOREIGN KEY REFERENCES tb_profesor(pro_cod),
	graCur_cod INT NOT NULL FOREIGN KEY REFERENCES tb_gradoCurso(graCur_cod),
	sal_eli CHAR(2) DEFAULT 'No' 
)
GO

--- Matrícula ---
CREATE TABLE tb_matricula (
	mat_cod INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	sal_cod INT NOT NULL FOREIGN KEY REFERENCES tb_salon(sal_cod),
	est_cod INT NOT NULL FOREIGN KEY REFERENCES tb_estudiante(est_cod),
	mat_eli CHAR(2) DEFAULT 'No' 
)
GO

--- Tienda ---
CREATE TABLE tb_categoria(
	cat_cod INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	cat_desc VARCHAR(50) NOT NULL
)
GO

CREATE TABLE tb_producto (
	pro_cod INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	pro_desc VARCHAR(50) NOT NULL,
	cat_cod INT NOT NULL FOREIGN KEY REFERENCES tb_categoria(cat_cod),
	pro_stock INT NOT NULL, 
	pro_precio MONEY NOT NULL,
	pro_eli CHAR(2) DEFAULT 'No' 
)
GO

CREATE TABLE tb_cliente (
	cli_cod CHAR (5) NOT NULL PRIMARY KEY CHECK(cli_cod like 'C[0-9][0-9][0-9][0-9]'),
	cli_nom VARCHAR(50) NOT NULL UNIQUE,
	cli_tel INT NOT NULL
)


CREATE TABLE tb_ventas_cab (
	vta_num CHAR(5) NOT NULL Primary Key,
	vta_fec DATE NULL ,
	cli_cod CHAR (5) NULL,
	vta_tot DECIMAL(8,2) DEFAULT 0,
	anulado CHAR(2) DEFAULT 'No'
	)
GO



CREATE TABLE tb_ventas_deta (
	vta_num CHAR(5) NOT NULL FOREIGN KEY REFERENCES tb_ventas_cab(vta_num),
	pro_cod INT NOT NULL FOREIGN KEY REFERENCES tb_producto(pro_cod),
	cantidad  INT NULL,
	precio DECIMAL (7,2),
	anulado CHAR(2) DEFAULT 'No',
        Primary Key(vta_num,pro_cod))
GO

 
ALTER TABLE tb_producto
	ADD CONSTRAINT ck_pro_stock CHECK (pro_stock>=0)
GO

-- Ingresando datos --
--- Distritos ---
INSERT INTO tb_distrito (dis_cod,dis_desc) VALUES (1,'Ancón'),(2,'Ate Vitarte'),(3,'Barranco'),(4,'Breña'),(5,'Carabayllo'),(6,'Chaclacayo'),(7,'Chorrillos'),(8,'Cieneguilla'),(9,'Comas'),(10,'El Agustino'),(11,'Independencia'),
(12,'Jesús María'),(13,'La Molina'),(14,'La Victoria'),(15,'Lima'),(16,'Lince'),(17,'Los Olivos'),(18,'Lurigancho'),(19,'Lurín'),(20,'Magdalena del Mar'),(21,'Miraflores'),(22,'Pachacamac'),(23,'Pucusana'),(24,'Pueblo Libre'),
(25,'Puente Piedra'),(26,'Punta Hermosa'),(27,'Punta Negra'),(28,'Rímac'),(29,'San Bartolo'),(30,'San Borja'),(31,'San Isidro'),(32,'San Juan de Lurigancho'),(33,'San Juan de Miraflores'),(34,'San Luis'),(35,'San Martín de Porres'),
(36,'San Miguel'),(37,'Santa Anita'),(38,'Santa María del Mar'),(39,'Santa Rosa'),(40,'Santiago de Surco'),(41,'Surquillo'),(42,'Villa El Salvador'),(43,'Villa María del Triunfo')
GO

--- Dirección Ubi ---
INSERT INTO tb_dirPre (dirPre_cod,dirPre_desc) VALUES (1,'Calle'),(2,'Avenida'),(3,'Jirón'),(4,'Pasaje')
GO

--- Género ---
INSERT INTO tb_genero (gen_cod, gen_desc) VALUES (1,'Masculino'),(2,'Femenino')
GO

--- Tipo de documento ---
INSERT INTO tb_tipDoc (tipDoc_cod, tipDoc_desc) VALUES (1,'DNI'),(2,'Carnet de extranjería'),(3,'Pasaporte')
GO

--- Grado ---
INSERT INTO tb_grado (gra_cod, gra_desc) VALUES (1,'1er grado'),(2,'2do grado'),(3,'3er grado'),(4,'4to grado'),(5,'5to grado'),(6,'6to grado')
GO

--- Curso ---
INSERT INTO tb_curso (cur_desc) VALUES ('Inglés'),('Arte y Cultura'),('Matemáticas'),('Castellano'),('Educación personal y social'),
('Ciencias'),('Computación'),('Estudios peruanos'),('Educación física')
GO

--- Estudiantes --- Date: YYYY-MM-DD
INSERT INTO tb_estudiante (est_nom,est_apePat,est_apeMat,est_fechNac,dis_cod,dirPre_cod,est_dir,est_tel,gen_cod,tipDoc_cod,est_docN,suscribed) VALUES 
('Carlos','Sotomayor','Segura','2017-07-12',2,1,'Las quechuas 145',997423241,1,1,84534957,0),('Camila','Torres','Ramos','2015-07-12',4,3,'Las magnolias 312',999563745,2,1,89746576,0),
('Ricardo','Pazos','Guevara','2016-02-19',2,1,'Osa Mayor 12',976234856,1,1,88945676,0),('Alfredo','Ramos','Sanchéz','2017-01-16',2,1,'Zavaleta 176',995238745,1,1,89456743,0)

--- Profesores ---
INSERT INTO tb_profesor(pro_nom,pro_apePat,pro_apeMat,pro_fechNac,dis_cod,dirPre_cod,pro_dir,pro_tel,gen_cod,tipDoc_cod,pro_docN,pro_suel) VALUES 
('Sergio','Guitierrez','Lopez','1989-07-12',2,1,'Faucett 132',999621789,1,1,12565423,2200.0),('Alicia','Carmargo','Matos','1996-01-23',4,3,'Azucena 523',912351567,2,1,74523487,1800.0),
('Carmen','Linares','Guevara','1992-02-19',2,1,'Escorpio 12',993143675,2,1,12445676,2300.0)

--- Grado Curso ---
INSERT INTO tb_gradoCurso(gra_cod,cur_cod) VALUES (1,3),(1,4),(1,1),(1,9),(2,1),(2,3),(2,4),(2,9),(3,1),(3,3),(3,4),(3,5),(3,6),(3,7),(3,9),(4,1),(4,2),(4,3),(4,4),(4,5),(4,6),(4,7),(4,8),(4,9)
,(5,1),(5,2),(5,3),(5,7),(5,5),(5,6),(5,8),(5,9)

--- Salón ---
INSERT INTO tb_salon (graCur_cod, pro_cod) VALUES (1,1),(6,1),(10,1),(3,2),(5,2),(9,2),(2,3),(6,3),(10,3)

--- Matrícula ---
INSERT INTO tb_matricula (sal_cod,est_cod) VALUES (1,3),(4,3),(7,3)
GO

--- Tienda ---
INSERT INTO tb_categoria (cat_desc) VALUES ('Uniformes'),('Utiles escolares'),('Material de aprendizaje')

INSERT INTO tb_producto (pro_desc,cat_cod,pro_stock,pro_precio) VALUES ('Camisa',1,5,50.0),('Blusa',1,5,50.0),('Zapato',1,5,120.0), ('Medias x 3',1,5,30.0),
('Conjunto completo hombre',1,3,200.0),('Lapicero',2,25,2.0),('Cuaderno cuadriculado',2,12,6.0),('Cuaderno rayado',2,12,6.0),('Diccionario español/inglés',3,12,6.0)
,('Libro Inglés A1',3,7,70.0)
GO

INSERT INTO tb_ventas_cab VALUES ('V0001', '2017-07-12', 'C0001', 120.0,'No')
GO

INSERT INTO tb_ventas_deta VALUES ('V0001', 3, 1, 120.0,'No')
GO

INSERT INTO tb_cliente VALUES ('C0001','Alvarez Peña, Angel',981234567),('C0002','Ponte Gomez, Alejandro',6584503),('C0003','Zuñiga Mateo, Carlos',5674566),
('C0004','Tucto de Souza, Bernardo',5634166)
GO

---- Al matricular al alumno con cod 3, tenemos que cambiar su estado a suscribed = 1 ----
UPDATE tb_estudiante SET suscribed = 1 WHERE est_cod = 3
GO



-- SP --
-- Listados --

--- Estudiantes ---
CREATE OR ALTER PROCEDURE sp_listar_estudiantesCC
AS 
	SELECT e.est_cod, e.est_nom, e.est_apePat, e.est_apeMat, e.est_fechNac, d.dis_desc, dp.dirPre_desc, e.est_dir, e.est_tel, g.gen_desc, td.tipDoc_desc, e.est_docN, e.suscribed
	FROM tb_estudiante e 
	INNER JOIN tb_distrito d  ON e.dis_cod = d.dis_cod
	INNER JOIN tb_dirPre dp ON e.dirPre_cod = dp.dirPre_cod
	INNER JOIN tb_genero g ON e.gen_cod = g.gen_cod
	INNER JOIN tb_tipDoc td ON e.tipDoc_cod = td.tipDoc_cod
	WHERE e.est_eli = 'No'
GO 

CREATE OR ALTER PROCEDURE sp_listar_estudiantes
AS
	SELECT * FROM tb_estudiante e
	WHERE e.est_eli = 'No'
GO

CREATE OR ALTER PROCEDURE sp_listar_distritos
AS 
	SELECT d.dis_cod, d.dis_desc FROM tb_distrito d
GO

CREATE OR ALTER PROCEDURE sp_listar_dirPres
AS 
	SELECT dp.dirPre_cod, dp.dirPre_desc FROM tb_dirPre dp
GO

CREATE OR ALTER PROCEDURE sp_listar_generos
AS 
	SELECT g.gen_cod, g.gen_desc FROM tb_genero g
GO

CREATE OR ALTER PROCEDURE sp_listar_tipoDocs
AS 
	SELECT td.tipDoc_cod, td.tipDoc_desc FROM tb_tipDoc td
GO

-- Profesores --
CREATE OR ALTER PROCEDURE sp_listar_profesoresCC
AS
	SELECT p.pro_cod, p.pro_nom, p.pro_apePat, p.pro_apeMat, p.pro_fechNac,  d.dis_desc, dp.dirPre_desc, p.pro_dir, p.pro_tel, g.gen_desc, td.tipDoc_desc, p.pro_docN, p.pro_suel
	FROM tb_profesor p
	INNER JOIN tb_distrito d  ON p.dis_cod = d.dis_cod
	INNER JOIN tb_dirPre dp ON p.dirPre_cod = dp.dirPre_cod
	INNER JOIN tb_genero g ON p.gen_cod = g.gen_cod
	INNER JOIN tb_tipDoc td ON p.tipDoc_cod = td.tipDoc_cod
	WHERE p.pro_eli = 'No'
GO

CREATE OR ALTER PROCEDURE sp_listar_profesores
AS
	SELECT * FROM tb_profesor p
	WHERE p.pro_eli = 'No'
GO

-- Cursos ---
CREATE OR ALTER PROCEDURE sp_listar_cursos
AS
	SELECT c.cur_cod, c.cur_desc FROM tb_curso c 
	WHERE c.cur_eli = 'No'
	ORDER BY c.cur_cod
GO

CREATE OR ALTER PROCEDURE sp_listar_grados
AS
	SELECT g.gra_cod, g.gra_desc FROM tb_grado g 
	ORDER BY g.gra_desc 
GO

CREATE OR ALTER PROCEDURE sp_listar_cursosCC
AS
	SELECT c.cur_cod, g.gra_desc, c.cur_desc FROM tb_gradoCurso gc
	INNER JOIN tb_curso c ON gc.cur_cod = c.cur_cod
	INNER JOIN tb_grado g ON gc.gra_cod = g.gra_cod
	WHERE gc.graCur_eli = 'No'
	ORDER BY g.gra_desc 
GO

--- salón
CREATE OR ALTER PROCEDURE sp_listar_salonCC
AS
	SELECT s.sal_cod, p.pro_nom + ' '+ p.pro_apePat as 'Nombre y Apellido', g.gra_desc, c.cur_desc FROM tb_salon s
	INNER JOIN tb_profesor p ON s.pro_cod = p.pro_cod
	INNER JOIN tb_gradoCurso gc ON s.graCur_cod = gc.graCur_cod
	INNER JOIN tb_curso c ON gc.cur_cod = c.cur_cod
	INNER JOIN tb_grado g ON gc.gra_cod = g.gra_cod
	WHERE s.sal_eli = 'No'
GO

CREATE OR ALTER PROCEDURE sp_listar_gradoCurso
AS 
	SELECT gc.graCur_cod, g.gra_desc + ' ' + c.cur_desc FROM tb_gradoCurso gc
	INNER JOIN tb_grado g ON gc.gra_cod = g.gra_cod
	INNER JOIN tb_curso c ON gc.cur_cod = c.cur_cod
	WHERE gc.graCur_eli = 'No'
GO

--- Matricula 
CREATE OR ALTER PROCEDURE sp_listar_matriculasCC
AS
	SELECT m.mat_cod, e.est_nom + ' ' + e.est_apePat as 'Alumno', s.sal_cod FROM tb_matricula m
	INNER JOIN tb_estudiante e ON m.est_cod = e.est_cod
	INNER JOIN tb_salon s ON m.sal_cod = s.sal_cod
	WHERE m.mat_eli = 'No'
GO



--- CRUD ---

-- Estudiantes --
CREATE OR ALTER PROCEDURE sp_insertar_estudiante
	@est_nom VARCHAR(50),@est_apePat VARCHAR(50),
	@est_apeMat VARCHAR(50),@est_fechNac DATE,@dis_cod INT,
	@dirPre_cod INT, @est_dir VARCHAR(50),@est_tel INT,@gen_cod INT,
	@tipDoc_cod INT,@est_docN INT
AS
	INSERT INTO tb_estudiante VALUES(@est_nom,@est_apePat,@est_apeMat,@est_fechNac,
		@dis_cod,@dirPre_cod,@est_dir,@est_tel,@gen_cod,@tipDoc_cod,@est_docN,0,'No');
GO

------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_obtener_estudiante 
@est_cod INT
AS
	SELECT e.est_cod, e.est_nom, e.est_apePat, e.est_apeMat, e.est_fechNac, e.dis_cod, e.dirPre_cod, e.est_dir, e.est_tel, e.gen_cod,
	e.tipDoc_cod, e.est_docN, e.suscribed FROM tb_estudiante e
	WHERE e.est_cod = @est_cod
GO

CREATE OR ALTER PROCEDURE sp_editar_estudiante
	@est_cod INT, @est_nom VARCHAR(50),@est_apePat VARCHAR(50),
	@est_apeMat VARCHAR(50),@est_fechNac DATE,@dis_cod INT,
	@dirPre_cod INT, @est_dir VARCHAR(50),@est_tel INT,@gen_cod INT,
	@tipDoc_cod INT,@est_docN INT
AS
	UPDATE tb_estudiante SET est_nom = @est_nom, est_apePat = @est_apePat, est_apeMat = @est_apeMat, est_fechNac = @est_fechNac,
		dis_cod = @dis_cod, dirPre_cod = @dirPre_cod, est_dir = @est_dir, est_tel = @est_tel, gen_cod = @gen_cod, tipDoc_cod = @tipDoc_cod,est_docN = @est_docN 
		WHERE est_cod = @est_cod
GO

EXEC sp_editar_estudiante @est_cod =5,	@est_nom ='Camilo',@est_apePat = 'Torres',
	@est_apeMat = 'Rondón' ,@est_fechNac ='2016/04/03', @dis_cod = 5 ,
	@dirPre_cod = 2, @est_dir= 'Rosales 532',@est_tel = 987612345,@gen_cod = 1,
	@tipDoc_cod = 1,@est_docN =94537212
GO

--------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_eliminar_estudiante
	@est_cod INT
AS
	DECLARE @suscribed AS INT
	SELECT @suscribed = suscribed from tb_estudiante WHERE est_cod = @est_cod
	
	IF @suscribed = 0
	BEGIN
		UPDATE tb_estudiante SET est_eli = 'Sí'
		WHERE est_cod = @est_cod 
	END
	ELSE
	BEGIN
		Print 'Alumno se encuentra matriculado'
	END
GO

EXEC sp_eliminar_estudiante @est_cod = 5
GO

-- Profesores --
CREATE OR ALTER PROCEDURE sp_insertar_profesor
	@pro_nom VARCHAR(50),@epro_apePat VARCHAR(50),
	@pro_apeMat VARCHAR(50),@pro_fechNac DATE,@dis_cod INT,
	@dirPre_cod INT, @pro_dir VARCHAR(50),@pro_tel INT,@gen_cod INT,
	@tipDoc_cod INT,@pro_docN INT, @pro_suel MONEY
AS
	INSERT INTO tb_profesor VALUES(@pro_nom ,@epro_apePat,@pro_apeMat,@pro_fechNac,@dis_cod,
	@dirPre_cod, @pro_dir,@pro_tel,@gen_cod ,
	@tipDoc_cod,@pro_docN, @pro_suel ,'No');
GO

------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_obtener_profesor 
@pro_cod INT
AS
	SELECT p.pro_cod, p.pro_nom, p.pro_apePat, p.pro_apeMat, p.pro_fechNac, p.dis_cod, p.dirPre_cod, p.pro_dir, p.pro_tel, p.gen_cod,
	p.tipDoc_cod, p.pro_docN, p.pro_suel FROM tb_profesor p
	WHERE p.pro_cod = @pro_cod
GO

CREATE OR ALTER PROCEDURE sp_editar_profesor
	@pro_cod INT, @pro_nom VARCHAR(50),@pro_apePat VARCHAR(50),
	@pro_apeMat VARCHAR(50),@pro_fechNac DATE,@dis_cod INT,
	@dirPre_cod INT, @pro_dir VARCHAR(50),@pro_tel INT,@gen_cod INT,
	@tipDoc_cod INT,@pro_docN INT, @pro_suel MONEY
AS
	UPDATE tb_profesor SET pro_nom = @pro_nom, pro_apePat = @pro_apePat, pro_apeMat = @pro_apeMat, pro_fechNac = @pro_fechNac,dis_cod = @dis_cod, 
			dirPre_cod = @dirPre_cod, pro_dir = @pro_dir, pro_tel = @pro_tel, gen_cod = @gen_cod, tipDoc_cod = @tipDoc_cod,pro_docN = @pro_docN,
			pro_suel = @pro_suel
		WHERE pro_cod = @pro_cod
GO

--------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_eliminar_profesor
	@pro_cod INT
AS
	UPDATE tb_profesor SET pro_eli = 'Sí'
		WHERE pro_cod = @pro_cod 
GO

-- Grado-Curso --

CREATE OR ALTER PROCEDURE sp_insertar_gradoCurso
	@uno_grad INT, @dos_grad INT, @tres_grad INT, @cuat_grad INT, @cinc_grad INT, @seis_grad INT, @cur_desc VARCHAR(50)
AS
	BEGIN TRAN 
		BEGIN TRY
			INSERT INTO tb_curso (cur_desc) VALUES (@cur_desc)
	
			DECLARE @cur_cod AS INT
			SELECT @cur_cod = cur_cod from tb_curso WHERE cur_desc = @cur_desc

			IF @uno_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (1,@cur_cod)
			END
			IF @dos_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (2,@cur_cod)
			END
			IF @tres_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (3,@cur_cod)
			END
			IF @cuat_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (4,@cur_cod)
			END
			IF @cinc_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (5,@cur_cod)
			END
			IF @seis_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (6,@cur_cod)
			END
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH 
			ROLLBACK TRANSACTION
		END CATCH
GO

EXEC sp_insertar_gradoCurso @uno_grad =0, @dos_grad =0, @tres_grad =0, @cuat_grad =1, @cinc_grad =1, @seis_grad =1, @cur_desc ='Ciencia y ambiente'
GO

----------------------------------------------------
CREATE OR ALTER PROCEDURE sp_obtener_gradoCurso
@cur_cod INT
AS
	SELECT c.cur_cod, c.cur_desc FROM tb_curso c
	WHERE c.cur_cod = @cur_cod
GO

CREATE OR ALTER PROCEDURE sp_editar_gradoCurso 
	@uno_grad INT, @dos_grad INT, @tres_grad INT, @cuat_grad INT, @cinc_grad INT, @seis_grad INT, @cur_desc VARCHAR(50), @cur_cod INT
AS 
	BEGIN TRAN 
		BEGIN TRY
			UPDATE tb_gradoCurso SET graCur_eli = 'Sí' WHERE cur_cod = @cur_cod
			UPDATE tb_curso SET cur_desc = @cur_desc WHERE cur_cod = @cur_cod
	
			DECLARE @cur AS INT
			SELECT @cur = cur_cod from tb_curso WHERE cur_desc = @cur_desc

			IF @uno_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (1,@cur)
			END
			IF @dos_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (2,@cur)
			END
			IF @tres_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (3,@cur)
			END
			IF @cuat_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (4,@cur)
			END
			IF @cinc_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (5,@cur)
			END
			IF @seis_grad = 1
			BEGIN
				INSERT INTO tb_gradoCurso (gra_cod,cur_cod) VALUES (6,@cur)
			END
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH 
			ROLLBACK TRANSACTION
		END CATCH
GO

EXEC sp_editar_gradoCurso @uno_grad =0, @dos_grad =0, @tres_grad =0, @cuat_grad =0, @cinc_grad =0, @seis_grad =1, @cur_desc ='Ciencias', @cur_cod = 10
GO



---------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_eliminar_gradoCurso
	@cur_cod INT
AS
	BEGIN TRAN 
	BEGIN TRY
		UPDATE tb_gradoCurso SET graCur_eli = 'Sí'
		WHERE cur_cod = @cur_cod

		UPDATE tb_curso SET cur_eli = 'Sí'
		WHERE cur_cod = @cur_cod
	COMMIT TRANSACTION
		END TRY
		BEGIN CATCH 
			ROLLBACK TRANSACTION
		END CATCH
GO

EXEC sp_eliminar_gradoCurso @cur_cod = 10
GO


-- Salón de clases --
CREATE OR ALTER PROCEDURE sp_insertar_salon
	@pro_cod INT, @graCur_cod INT
AS
	INSERT INTO tb_salon (graCur_cod, pro_cod) VALUES (@graCur_cod, @pro_cod)
GO

CREATE OR ALTER PROCEDURE sp_obtener_salon 
@sal_cod INT
AS
	SELECT s.sal_cod, s.pro_cod, s.graCur_cod FROM tb_salon s
	WHERE s.sal_cod = @sal_cod
GO

CREATE OR ALTER PROCEDURE sp_editar_salon
	@pro_cod INT, @graCur_cod INT, @sal_cod INT
AS
	UPDATE tb_salon SET pro_cod = @pro_cod, graCur_cod = @graCur_cod 
	WHERE sal_cod = @sal_cod
GO

CREATE OR ALTER PROCEDURE sp_eliminar_salon
	@sal_cod INT
AS
	UPDATE tb_salon SET sal_eli = 'Sí' 
	WHERE sal_cod = @sal_cod
GO

CREATE OR ALTER PROCEDURE sp_listar_profesoresCOD
AS
	SELECT p.pro_cod, p.pro_nom + ' ' + p.pro_apePat FROM tb_profesor p 
	WHERE p.pro_eli = 'No'
GO

-- Matrícula --
CREATE OR ALTER PROCEDURE sp_insertar_matricula
	@gra_cod INT, @est_cod INT
AS
	BEGIN TRAN
		BEGIN TRY
		-- Declarando variables --
		--- Contador de salones de grado requerido ---
		DECLARE @sal_cant AS INT
		--- Contador x ---
		DECLARE @contador AS INT
		-- Asignando 1 al contador x --
		SET @contador = 1
		-- Asignando el total de salones al contador de salones --
		SELECT @sal_cant = COUNT(sal_cod) FROM tb_salon WHERE graCur_cod IN(SELECT graCur_cod FROM tb_gradoCurso WHERE gra_cod = @gra_cod)
	
		-- Declarando una lista como variables e insertando los salones requeridos 
		DECLARE @lista TABLE(id INT IDENTITY(1,1) PRIMARY KEY, cod INT)
		INSERT INTO @lista (cod) SELECT sal_cod FROM tb_salon WHERE graCur_cod IN(SELECT graCur_cod FROM tb_gradoCurso WHERE gra_cod = @gra_cod)
	
		-- Si existen salones para ese grado, entonces --
		IF @sal_cant > 0
		BEGIN
			WHILE @contador <= @sal_cant
				BEGIN
				INSERT INTO tb_matricula (sal_cod, est_cod) VALUES ((SELECT cod FROM @lista WHERE id = @contador),@est_cod)
				SET @contador = @contador + 1
				END
			UPDATE tb_estudiante SET suscribed = 1 WHERE est_cod = @est_cod
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH 
		ROLLBACK TRANSACTION
	END CATCH
GO

EXEC sp_insertar_matricula @gra_cod =1 ,@est_cod =1
GO

CREATE OR ALTER PROCEDURE sp_eliminar_matricula
	@mat_cod INT
AS
	DECLARE @est_cod AS INT
	SELECT @est_cod = m.est_cod FROM tb_matricula m WHERE m.mat_cod = @mat_cod  

	UPDATE tb_matricula SET mat_eli = 'Sí' 
	WHERE mat_cod IN (SELECT mat_cod FROM tb_matricula WHERE est_cod = @est_cod)
	UPDATE tb_estudiante SET suscribed = 0 
	WHERE est_cod = @est_cod
GO

CREATE OR ALTER PROCEDURE sp_listar_estudianteCOD
AS
	SELECT e.est_cod, 'Ape Pat: ' + e.est_apePat + ' N° Doc: '+ CONVERT(VARCHAR(10),e.est_docN) FROM tb_estudiante e 
	WHERE e.est_eli = 'No' AND e.suscribed = 0
GO

CREATE OR ALTER PROCEDURE sp_listar_productos 
AS
SELECT p.pro_cod, p.pro_desc, c.cat_desc, p.pro_stock, p.pro_precio FROM tb_producto p 
	INNER JOIN tb_categoria c ON p.cat_cod = c.cat_cod
	WHERE p.pro_eli = 'No'
GO

CREATE OR ALTER PROCEDURE sp_listar_categorias
AS
	SELECT c.cat_cod, c.cat_desc FROM tb_categoria c
GO

CREATE OR ALTER PROCEDURE sp_listar_clientes
AS
	SELECT c.cli_cod, c.cli_nom, c.cli_tel FROM tb_cliente c
GO

CREATE OR ALTER PROCEDURE sp_insertar_cliente
@cli_cod CHAR(5), @cli_nom VARCHAR(50), @cli_tel INT 
AS
	INSERT INTO tb_cliente VALUES (@cli_cod, @cli_nom, @cli_tel) 
GO

CREATE OR ALTER PROCEDURE sp_insertar_productos
@pro_desc VARCHAR(50), @cat_cod INT, @pro_stock INT, @pro_precio MONEY
AS
	INSERT INTO tb_producto (pro_desc,cat_cod,pro_stock,pro_precio) VALUES (@pro_desc, @cat_cod, @pro_stock, @pro_precio)
GO

CREATE OR ALTER PROCEDURE sp_obtener_producto
@pro_cod INT
AS
	SELECT p.pro_cod, p.pro_desc, p.cat_cod, p.pro_stock, p.pro_precio FROM tb_producto p 
	WHERE p.pro_cod = @pro_cod
GO

CREATE OR ALTER PROCEDURE sp_editar_productos
@pro_desc VARCHAR(50), @cat_cod INT, @pro_stock INT, @pro_precio MONEY, @pro_cod INT
AS
	UPDATE tb_producto SET pro_desc = @pro_desc, cat_cod = @cat_cod, pro_stock = @pro_stock, pro_precio = @pro_precio
		WHERE pro_cod = @pro_cod
GO

CREATE OR ALTER PROCEDURE sp_eliminar_productos
@pro_cod INT
AS
	UPDATE tb_producto SET pro_eli = 'Sí'
	WHERE pro_cod = @pro_cod
GO

CREATE OR ALTER PROC sp_grabar_web_ventas_cab
@CLI_COD CHAR(5), @TOT_VTA DECIMAL(10,2)
AS
	DECLARE @NUMERO VARCHAR(5) 
	SELECT @NUMERO=RIGHT(MAX(vta_num),4)+1 FROM tb_ventas_cab
	SELECT @NUMERO='V'+RIGHT('000'+@NUMERO,4)
	IF @NUMERO IS NULL
		BEGIN
			SELECT @NUMERO = 'V0001' 
		END
	ELSE
		BEGIN
			INSERT INTO tb_ventas_cab VALUES(@NUMERO,GETDATE(),
			@CLI_COD, @TOT_VTA, 'No')
		END	
	SELECT @NUMERO AS NUMERO
GO

CREATE OR ALTER PROC sp_grabar_web_ventas_det
@vta_num CHAR(5), @pro_cod INT, 
@cantidad INT, @precio DECIMAL(7,2)
AS
	IF @vta_num IS NULL
	BEGIN
		INSERT INTO tb_ventas_deta
			VALUES('V0001', @pro_cod, @cantidad, @precio, 'No')
	END
	ELSE
	BEGIN
		INSERT INTO tb_ventas_deta
			VALUES(@vta_num, @pro_cod, @cantidad, @precio, 'No')
	END
	UPDATE tb_producto SET pro_stock=pro_stock - @cantidad 
	WHERE pro_cod = @pro_cod
GO