-- Cria o banco de dados 'Contacts'
CREATE DATABASE Contacts;
GO

-- Seleciona o banco de dados 'Contacts'
USE Contacts;
GO

-- Cria a tabela 'Contatos'
CREATE TABLE Contatos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Surname NVARCHAR(50) NOT NULL,
    DDD CHAR(2) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);
GO

-- Insere um contato na tabela 'Contatos'
INSERT INTO Contatos (Name, Surname, DDD, Phone, Email)
VALUES ('João', 'Silva', '11', '99999-9999', 'joao.silva@email.com');
GO