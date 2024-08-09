USE master;
GO

--ALTER DATABASE BankKita SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--GO

--DROP DATABASE BankKita;
--GO

CREATE DATABASE BankKita;
GO

USE BankKita;
GO

CREATE TABLE dbo.JenisRekening(
	JenisRekeningId bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Deskripsi varchar(100) NOT NULL
);
GO

CREATE TABLE dbo.RekeningNasabah(
	RekeningNasabahId bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	NoRekening varchar(12) NOT NULL,
	JenisRekeningId bigint NOT NULL,
	Saldo money NOT NULL,
	TanggalBuka datetime NOT NULL,
	FOREIGN KEY (JenisRekeningId) REFERENCES dbo.JenisRekening(JenisRekeningId)
);
GO

INSERT INTO dbo.JenisRekening
           (Deskripsi)
VALUES
    ('Tabungan'),
	('Giro')
GO