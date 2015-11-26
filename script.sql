USE subibaje
GO

DROP DATABASE micros
GO

CREATE DATABASE micros
GO

USE micros
GO

/**
 *
 * EN TODAS LAS TABLAS HAY UN CAMPO BORRADO QUE ES PARA HACER UN BORRADO LOGICO,
 * NO ELIMINAMOS NINGUN DATO DE LA DB PORQUE DESPUES PODEMOS VOLVER A ACTIVAR
 * LOS REGISTROS. ES DATETIME CON DEFAULT NULL PARA SABER EN QUE MOMENTO EXACTO
 * SE BORRO Y PODER RESTITUIR.
 *
**/

/**
 *
 * ESTA TABLA ESTA CREADA PORQUE LA IDEA DEL SISTEMA ES QUE SIRVA PARA CUALQUIER CIUDAD
 * QUE QUIERA IMPLEMENTAR EL BOLETO ESTUDIANTIL.
 *
**/

CREATE TABLE provincias (
	id int identity(1,1) not null,
	nombre varchar(50) not null,
	borrado datetime null,
	CONSTRAINT PK_PROVINCIAS PRIMARY KEY (id),
	CONSTRAINT UNQ_NOMBRE_PROVINCIA UNIQUE (nombre)
)

GO

/**
 *
 * 'NOMBRE' no es UNIQUE porque puede haber dos ciudades con el mismo 
 * nombre en diferentes provincias
 *
**/

CREATE TABLE ciudades (
	id int identity(1,1) not null,
	nombre varchar(50) not null,
	provincia_id int not null,
	borrado datetime null,
	CONSTRAINT PK_CIUDADES PRIMARY KEY (id),
	CONSTRAINT FK_PROVINCIA_CIUDAD FOREIGN KEY (provincia_id) REFERENCES provincias(id)
)

GO

/**
 *
 * 'LINEA' no es UNIQUE porque puede haber dos lineas con el mismo 
 * nombre/numero en diferentes ciudades
 *
**/

CREATE TABLE urbanos (
	id int identity(1,1) not null,
	linea varchar(50) not null,
	ciudad_id int not null,
	borrado datetime null,
	CONSTRAINT PK_URBANOS PRIMARY KEY (id),
	CONSTRAINT FK_CIUDAD_URBANO FOREIGN KEY (ciudad_id) REFERENCES ciudades(id)
)

GO

/**
 *
 * 'NOMBRE' no es UNIQUE porque puede haber dos establecimientos con el mismo 
 * nombre en diferentes ciudades
 *
**/

CREATE TABLE establecimientos (
	id int identity(1,1) not null,
	nombre varchar(50) not null,
	ciudad_id int not null,
	borrado datetime null,
	CONSTRAINT PK_ESTABLECIMIENTOS PRIMARY KEY (id),
	CONSTRAINT FK_CIUDAD_ESTABLECIMIENTOS FOREIGN KEY (ciudad_id) REFERENCES ciudades(id)
)

GO

CREATE TABLE niveles (
	id int identity(1,1) not null,
	nombre varchar(50) not null,
	borrado datetime null,
	CONSTRAINT PK_NIVELES PRIMARY KEY (id),
	CONSTRAINT UNQ_NOMBRE_NIVEL UNIQUE (nombre)
)

GO

/**
 *
 * No usamos una clave primaria compuesta porque desde otra tabla referenciamos a esta. Pero si 
 * creamos una clave UNIQUE para que no haya duplas establecimiento/nivel repetidas.
 *
**/

CREATE TABLE establecimiento_nivel (
	id int identity(1,1) not null,
	establecimiento_id int not null,
	nivel_id int not null,
	borrado datetime null,
	CONSTRAINT PK_ESTABLECIMIENTO_NIVEL PRIMARY KEY (id),
	CONSTRAINT FK_ESTABLECIMIENTO_ESTABLECIMIENTO_NIVEL FOREIGN KEY (establecimiento_id) REFERENCES establecimientos(id),
	CONSTRAINT FK_NIVEL_ESTABLECIMIENTO_NIVEL FOREIGN KEY (nivel_id) REFERENCES niveles(id),
	CONSTRAINT UNQ_ESTABLECIMIENTO_NIVEL UNIQUE (establecimiento_id, nivel_id)
)

GO

/**
 *
 * Estos son los tipos de roles (estudiante, admin, etc)
 *
**/

CREATE TABLE tipos (
	id int identity(1,1) not null,
	nombre varchar(50) not null,
	borrado datetime null,
	peso int default 0,
	CONSTRAINT PK_TIPO PRIMARY KEY (id),
	CONSTRAINT UNQ_NOMBRE_TIPO UNIQUE (nombre)
)

GO

/**
 *
 * Esta es una tabla general para cualquiera que ingrese al sistema. El tipo de ingreso lo da 
 * la tabla roles
 *
**/

CREATE TABLE usuarios (
	id int identity(1,1) not null,
	nombre varchar(50) not null,
	email varchar(50) not null,
	dni int not null,
	password varchar (32) not null,
	borrado datetime null,
	CONSTRAINT PK_USUARIO PRIMARY KEY (id),
	CONSTRAINT UNQ_EMAIL_USUARIO UNIQUE (email),
	CONSTRAINT UNQ_DNI_USUARIO UNIQUE (dni)
)

GO

/**
 *
 * Existe una tabla roles porque asumimos que una misma persona puede ser estudiante y admin a la vez
 * por ejemplo, o puede ser director de un primario y estar estudiando un doctorado en otro lado y no tendria
 * sentido crearle dos usuarios diferentes.
 *
 * SOLO PUEDE TENER ACTIVO UN ROL DE CADA TIPO A LA VEZ, PARA ESO ES EL UNQ CREADO
 *
 *
**/

CREATE TABLE roles (
	id int identity(1,1) not null,
	tipo_id int not null,
	usuario_id int not null,
	establecimiento_nivel_id int null,
	borrado datetime default CURRENT_TIMESTAMP,
	CONSTRAINT PK_ROL PRIMARY KEY (id),
	CONSTRAINT FK_TIPO_ROL FOREIGN KEY (tipo_id) REFERENCES tipos(id),
	CONSTRAINT FK_USUARIO_ROL FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
	CONSTRAINT FK_ESTABLECIMIENTO_NIVEL_ROL FOREIGN KEY (establecimiento_nivel_id) REFERENCES establecimiento_nivel(id),
	CONSTRAINT UNQ_ROLES_USUARIO UNIQUE (tipo_id, usuario_id, borrado)
)

GO

/**
 *
 * Asociamos el boleto a un rol y no a un usuario porque puede haber estudiado primero en un lugar y luego
 * en otro y queremos tener registro de eso.
 *
**/

CREATE TABLE boletos (
	id int identity(1,1) not null,
	urbano_id int not null,
	rol_id int not null,
	fecha datetime default CURRENT_TIMESTAMP,
	CONSTRAINT PK_BOLETOS PRIMARY KEY (id),
	CONSTRAINT FK_URBANO_BOLETO FOREIGN KEY (urbano_id) REFERENCES urbanos(id),
	CONSTRAINT FK_ROL_BOLETO FOREIGN KEY (rol_id) REFERENCES roles(id),
)

GO

/**
 *
 * Tenemos creados muchos TRIGGERS pero estamos en proceso de optimizacion para entregar el trabajo. 
 * Dejamos uno como ejemplo, la idea es manejar las cascadas de los borrados logicos.
 *
 * Cuando marcamos como borrada una provincia, marcamos con el mismo TIMESTAMP como borradas las ciudades
 * que pertenezcan a esa provincia, salvo aquellas que ya habian sido borradas (le dejamos el timestamp viejo).
 * Cuando restituimos una provincia, restituimos todas las ciudades, salvo aquellas que habian sido borradas
 * antes de que la provincia fuera borradas
 *
**/

GO

CREATE TRIGGER borrar_ciudades_provincias
on provincias
for update 
as
declare @fecha_vieja datetime, @fecha_nueva datetime, @id int
select @fecha_vieja = borrado, @id = id from deleted 
select @fecha_nueva = borrado from inserted
if @fecha_nueva is null 
	update ciudades set borrado = @fecha_nueva where provincia_id = @id and borrado = @fecha_vieja
else 
	update ciudades set borrado = @fecha_nueva where provincia_id = @id and borrado is null
	
GO

CREATE TRIGGER borrar_establecimientos_ciudades
on ciudades
for update 
as
declare @fecha_vieja datetime, @fecha_nueva datetime, @id int
select @fecha_vieja = borrado, @id = id from deleted 
select @fecha_nueva = borrado from inserted
if @fecha_nueva is null 
	update establecimentos set borrado = @fecha_nueva where ciudad_id = @id and borrado = @fecha_vieja
else 
	update establecimientos set borrado = @fecha_nueva where ciudad_id = @id and borrado is null
	
GO

CREATE TRIGGER borrar_urbanos_ciudades
on ciudades
for update 
as
declare @fecha_vieja datetime, @fecha_nueva datetime, @id int
select @fecha_vieja = borrado, @id = id from deleted 
select @fecha_nueva = borrado from inserted
if @fecha_nueva is null 
	update urbanos set borrado = @fecha_nueva where ciudad_id = @id and borrado = @fecha_vieja
else 
	update urbanos set borrado = @fecha_nueva where ciudad_id = @id and borrado is null
	
GO

CREATE TRIGGER borrar_estanivel_establecimientos
on establecimientos
for update 
as
declare @fecha_vieja datetime, @fecha_nueva datetime, @id int
select @fecha_vieja = borrado, @id = id from deleted 
select @fecha_nueva = borrado from inserted
if @fecha_nueva is null 
	update establecimiento_nivel set borrado = @fecha_nueva where establecimiento_id = @id and borrado = @fecha_vieja
else 
	update establecimiento_nivel set borrado = @fecha_nueva where establecimiento_id = @id and borrado is null
	
GO

CREATE TRIGGER borrar_roles_usuarios
on usuarios
for update 
as
declare @fecha_vieja datetime, @fecha_nueva datetime, @id int
select @fecha_vieja = borrado, @id = id from deleted 
select @fecha_nueva = borrado from inserted
if @fecha_nueva is null 
	update roles set borrado = @fecha_nueva where usuario_id = @id and borrado = @fecha_vieja
else 
	update roles set borrado = @fecha_nueva where usuario_id = @id and borrado is null
	
GO

CREATE TRIGGER borrar_roles_estanivel
on establecimiento_nivel
for update 
as
declare @fecha_vieja datetime, @fecha_nueva datetime, @id int
select @fecha_vieja = borrado, @id = id from deleted 
select @fecha_nueva = borrado from inserted
if @fecha_nueva is null 
	update roles set borrado = @fecha_nueva where establecimiento_nivel_id = @id and borrado = @fecha_vieja
else 
	update roles set borrado = @fecha_nueva where establecimiento_nivel_id = @id and borrado is null
	
GO