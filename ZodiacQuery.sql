create database ZodiacReg
use ZodiacReg
create table tb_Assembly(id_assembly int primary key identity(1,1), idNumber varchar(255), model varchar(255), regDate datetime)

create table tb_Model(id_model int primary key identity(1,1), model varchar(255))

create table tb_RepitedAssembly(id_ra int primary key identity(1,1), idNumber varchar(255), model varchar(255), regDate datetime)

select idNumber, model, regDate from tb_Assembly

select idNumber AS IDNumber, model AS Model, regDate AS Date from tb_Assembly

SELECT TOP 1 * from tb_assembly order by id_assembly desc
SELECT COUNT(*) from tb_assembly

select * from tb_RepitedAssembly

select idNumber AS IDNumber, model AS Model, regDate AS Date from tb_RepitedAssembly

select TOP 1000 id_assembly, idNumber AS IDNumber, model AS Model, regDate AS Date from tb_Assembly order by id_assembly desc
