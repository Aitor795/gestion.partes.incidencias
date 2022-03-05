CREATE TABLE tipo_registro (
id INT PRIMARY KEY AUTO_INCREMENT COMMENT 'identificador único',
descripcion VARCHAR(40) COMMENT 'descripcion del tipo del registro'
);

ALTER TABLE tipo_registro COMMENT 'tabla maestra con los tipo de registro';

CREATE TABLE motivo_registro (
id INT PRIMARY KEY AUTO_INCREMENT COMMENT 'identificador único',
id_tipo_registro INT COMMENT 'FK al maestro de tipos de registro',
motivo VARCHAR(40) COMMENT 'motivo del registro',
descripcion VARCHAR(255) COMMENT 'descripición del motivo del registro'
);

ALTER TABLE motivo_registro COMMENT 'tabla maestra con los posibles motivos del registro';
ALTER TABLE motivo_registro
ADD CONSTRAINT fk_tipo_registro2
FOREIGN KEY (id_tipo_registro) REFERENCES tipo_registro(id);

-- TODO: Preguntar la codificación de los grupos.

CREATE TABLE grupo (
codigo VARCHAR(15) PRIMARY KEY COMMENT 'código del grupo',
nombre VARCHAR(20) COMMENT 'nombre del grupo'
);

ALTER TABLE grupo COMMENT 'tabla maestra de grupos';

CREATE TABLE alumno (
nia INT PRIMARY KEY COMMENT 'identificacor único',
nombre VARCHAR(15) COMMENT 'nombre alumno',
apellido1 VARCHAR(15) COMMENT 'apellido 1',
apellido2 VARCHAR(15) COMMENT 'apellido 2',
codigo_grupo VARCHAR(15) COMMENT 'FK al grupo al que pertenece el alumno'
); 

ALTER TABLE alumno COMMENT 'tabla de alumnos';

ALTER TABLE alumno
ADD CONSTRAINT fk_codigo_grupo
FOREIGN KEY (codigo_grupo) REFERENCES grupo(codigo);

CREATE TABLE profesor (
dni VARCHAR(9) PRIMARY KEY COMMENT 'identificador del profesor',
nombre VARCHAR(15) COMMENT 'nombre del profesor',
apellido1 VARCHAR(15) COMMENT 'apellido 1',
apellido2 VARCHAR(15) COMMENT 'apellido 2',
contrasenya VARCHAR(32) COMMENT 'contraseña profesordel profesor',
tutor_grupo VARCHAR(15) COMMENT 'grupo del que es tutor'
);

ALTER TABLE profesor COMMENT 'tabla de los profesores';

ALTER TABLE profesor
ADD CONSTRAINT fk_profesor_tutor_grupo
FOREIGN KEY (tutor_grupo) REFERENCES grupo(codigo);

CREATE TABLE registro (
id INT PRIMARY KEY AUTO_INCREMENT COMMENT 'identificador único',
id_tipo_registro INT COMMENT 'FK al maestro de tipos de registro',
id_motivo_registro INT COMMENT 'FK al maestro de motivos de registro',
fecha_suceso DATETIME COMMENT 'fecha en la que se realiza el suceso',
nia_alumno INT COMMENT 'FK a la tabla de alumnos',
dni_profesor VARCHAR(9) COMMENT 'FK al profesor que presencia el suceso',
fecha_registro DATETIME NULL DEFAULT current_timestamp() COMMENT 'fecha de registro del suceso',
dni_profesor_registro VARCHAR(9) COMMENT 'FK al profesor que registra el suceso',
sancionado BOOLEAN COMMENT 'flag que indica si ha sido sancionado (unicamente en registros de tipo amonestación)'
);

ALTER TABLE registro COMMENT 'tabla de registros';

ALTER TABLE registro
ADD CONSTRAINT fk_tipo_registro
FOREIGN KEY (id_tipo_registro) REFERENCES tipo_registro(id);

ALTER TABLE registro
ADD CONSTRAINT fk_motivo_registro
FOREIGN KEY (id_tipo_registro, id_motivo_registro) REFERENCES motivo_registro(id_tipo_registro, id);

ALTER TABLE registro
ADD CONSTRAINT fk_nia_alumno
FOREIGN KEY (nia_alumno) REFERENCES alumno(nia);

ALTER TABLE registro
ADD CONSTRAINT fk_registro_dni_profesor
FOREIGN KEY (dni_profesor) REFERENCES profesor(dni);

ALTER TABLE registro
ADD CONSTRAINT fk_dni_profesor_registro
FOREIGN KEY (dni_profesor_registro) REFERENCES profesor(dni);

CREATE TABLE rol (
codigo VARCHAR(15) PRIMARY KEY COMMENT 'código del rol',
descripcion VARCHAR(20) COMMENT 'descripción del rol'
);

ALTER TABLE rol COMMENT 'tabla maestra de roles';

CREATE TABLE permiso (
codigo VARCHAR(15) PRIMARY KEY COMMENT 'codigo del permiso',
descripcion VARCHAR(255) COMMENT 'descripcion del permiso'
);

ALTER TABLE permiso COMMENT 'tabla maestra de permisos de la aplicación';

CREATE TABLE permisos_rol (
id INT PRIMARY KEY AUTO_INCREMENT COMMENT 'identificador único',
codigo_rol VARCHAR(15) COMMENT 'FK del codigo del rol',
codigo_permiso VARCHAR(15) COMMENT 'FK del codigo del permiso'
);

ALTER TABLE permisos_rol COMMENT 'tabla relacional de roles y permisos';

ALTER TABLE permisos_rol
ADD CONSTRAINT fk_codigo_rol
FOREIGN KEY (codigo_rol) REFERENCES rol(codigo);

ALTER TABLE permisos_rol
ADD CONSTRAINT fk_codigo_permiso
FOREIGN KEY (codigo_permiso) REFERENCES permiso(codigo);

CREATE TABLE roles_profesor (
id INT PRIMARY KEY AUTO_INCREMENT COMMENT 'identificador único',
dni_profesor VARCHAR(9) COMMENT 'FK del dni del profesor',
codigo_rol VARCHAR(15) COMMENT 'FK del codigo de rol'
);

ALTER TABLE roles_profesor COMMENT 'tabla relacional entre profesores y roles';

ALTER TABLE roles_profesor
ADD CONSTRAINT fk_dni_profesor
FOREIGN KEY (dni_profesor) REFERENCES profesor(dni);

ALTER TABLE roles_profesor
ADD CONSTRAINT fk_codigo_rol_profesor
FOREIGN KEY (codigo_rol) REFERENCES rol(codigo);

-- CREACIÓN INSERTS INTO --

INSERT INTO tipo_registro (descripcion) VALUES ('Incidencia');
INSERT INTO tipo_registro (descripcion) VALUES ('Amonestación');

INSERT INTO rol (codigo, descripcion) VALUES ('PROFESOR', 'Profesor');
INSERT INTO rol (codigo, descripcion) VALUES ('TUTOR', 'Tutor');
INSERT INTO rol (codigo, descripcion) VALUES ('DIRECTIVO', 'Directivo');
INSERT INTO rol (codigo, descripcion) VALUES ('ADMIN', 'Administrador');

INSERT INTO permiso (codigo, descripcion) VALUES ('ADD_REGISTRO', 'Añadir incidencia');
INSERT INTO permiso (codigo, descripcion) VALUES ('MOD_REGISTRO', 'Modificar incidencia');
INSERT INTO permiso (codigo, descripcion) VALUES ('DEL_REGISTRO', 'Borrar incidencia');

INSERT INTO permiso (codigo, descripcion) VALUES ('REPORT_ALUMN', 'Realizar informe de alumno');
INSERT INTO permiso (codigo, descripcion) VALUES ('REPORT_PROFE', 'Realizar informe de profesor');
INSERT INTO permiso (codigo, descripcion) VALUES ('REPORT_GRUPO', 'Realizar informe de grupo');
INSERT INTO permiso (codigo, descripcion) VALUES ('REPORT_INCID', 'Realizar informe de incidencia');

INSERT INTO permiso (codigo, descripcion) VALUES ('MOD_CONTRASENYA', 'Modificar contraseña');

INSERT INTO permiso (codigo, descripcion) VALUES ('ADD_ROL', 'Alta de rol');
INSERT INTO permiso (codigo, descripcion) VALUES ('DEL_ROL', 'Borrar rol');
INSERT INTO permiso (codigo, descripcion) VALUES ('MOD_ROL', 'Modificar rol');

INSERT INTO permiso (codigo, descripcion) VALUES ('ADD_PERMISO', 'Alta permiso');
INSERT INTO permiso (codigo, descripcion) VALUES ('DEL_PERMISO', 'Borrar permiso');
INSERT INTO permiso (codigo, descripcion) VALUES ('MOD_PERMISDO', 'Modificar permiso');

INSERT INTO permiso (codigo, descripcion) VALUES ('IMPORT_EXPORT', 'Importación y exportación de datos');

INSERT INTO permiso (codigo, descripcion) VALUES ('GRAPH_VIWER', 'Visualización de gráficos');