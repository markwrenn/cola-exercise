create table employee
(
	id INTEGER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY ,
	first_name varchar(50) not null,
	last_name varchar(100) not null,
	phone_number varchar(30) not null
)