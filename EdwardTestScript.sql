/*Generé la base de datos utilizando CodeFirst. Cree el script en SQL server, aunque al final terminé utilizando SQLLite, 
de igual modo pueden probar con este script que la tabla se crea correctamente*/
create Database EdwardTest;

use EdwardTest;

create table Person(
	Id int identity(1,1) primary key not null,
	FullName varchar(100) not null,
	DateOfBirth datetime not null,
)
