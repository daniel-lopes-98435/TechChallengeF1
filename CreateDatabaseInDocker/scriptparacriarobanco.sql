-- Cria o banco de dados 'Contacts'
CREATE DATABASE ContactManagment;
GO

-- Seleciona o banco de dados 'Contacts'
USE ContactManagment;
GO

-- Cria a tabela 'Contatos'
CREATE TABLE ContatctInformation (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(70) NOT NULL,
    LastName NVARCHAR(70) NOT NULL,
    DDD CHAR(2) NOT NULL,
    Phone INT NOT NULL,
    Email NVARCHAR(100) NOT NULL
);
GO

-- Insere um contato na tabela 'ContatctInformation'
INSERT INTO ContatctInformation (FirstName, LastName, DDD, Phone, Email)
VALUES ('João', 'Silva', '11', 777799999, 'joao.silva@email.com');
GO
INSERT INTO ContatctInformation (FirstName, LastName, DDD, Phone, Email)
VALUES ('Maria', 'Silva', '11', 888899999, 'maria.silva@email.com');
GO
INSERT INTO ContatctInformation (FirstName, LastName, DDD, Phone, Email)
VALUES ('Jose', 'Silva', '11', 999999999, 'jose.silva@email.com');
GO
-- 5 Lista os contatos inseridos
SELECT TOP 1 * FROM ContatctInformation