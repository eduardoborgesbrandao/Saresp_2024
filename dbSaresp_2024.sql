create database dbSaresp_2024;
use dbsaresp_2024;
-- drop database dbSaresp_2024;
create table tbProfessorAplicador(
IdProfessor int primary key auto_increment,
Nome varchar(150),
cpf decimal(11,0),
rg varchar(10),
telefone decimal(11,0),
Data_nasc datetime
);

create table tbAluno(
IdAluno int primary key auto_increment,
Nome varchar(150),
email varchar(50),
telefone decimal(11,0),
serie varchar(20),
turma varchar(40),
data_nasc datetime
);

Delimiter $$
create procedure sp_insertAluno(vnome varchar(150), vemail varchar(50), vtelefone decimal(11,0), vserie varchar(20), vturma varchar(40), vdata_nasc datetime)
begin
	insert into tbAluno(nome, email, telefone, serie, turma, data_nasc) values(vnome, vemail, vtelefone, vserie, vturma, vdata_nasc);
end
$$

Delimiter $$
create procedure sp_insertProfessor(vnome varchar(150), vcpf decimal(11,0), vrg varchar(10), vtelefone decimal(11,0), vdata_nasc datetime)
begin
	insert into tbProfessorAplicador(nome, cpf, rg, telefone, data_nasc) values(vnome, vcpf, vrg, vtelefone, vdata_nasc);
end
$$

Delimiter $$
create procedure sp_updateAluno(vidaluno int, vnome varchar(150), vemail varchar(50), vtelefone decimal(11,0), vserie varchar(20), vturma varchar(40), vdata_nasc datetime)
begin
	update tbAluno set nome = vnome, email = vemail, telefone = vtelefone, serie = vserie, turma = vturma, data_nasc = vdata_nasc where idaluno = vidaluno;
end
$$

Delimiter $$
create procedure sp_updateProfessor(vidprofessor int, vnome varchar(150), vcpf decimal(11,0), vrg varchar(10), vtelefone decimal(11,0), vdata_nasc datetime)
begin
	update tbProfessorAplicador set nome = vnome, cpf = vcpf, rg = vrg, telefone = vtelefone, data_nasc = vdata_nasc where idProfessor = vidprofessor;
end
$$

Delimiter $$
create procedure sp_deleteAluno(vidaluno int)
begin
	delete from tbAluno where idaluno = vidaluno;
end
$$

Delimiter $$
create procedure sp_deleteProfessor(vidprofessor int)
begin
	delete from tbprofessorAplicador where idprofessor = vidprofessor;
end
$$