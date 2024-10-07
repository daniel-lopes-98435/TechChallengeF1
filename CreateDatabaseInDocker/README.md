# Voce já deve ter o docker instalado e configurado na sua máquina

# Abra o terminal na pasta onde está o arquivo docker-compose.yml e execute **

docker-compose up --build
Após finalizar o de rodar, vá para o passo abaixo

# Acessar o banco de dados SQL com sua ferramenta de gerenciamento de Banco de dados, você precisa ter os acessos nescessário no BD para isto
Se estiver utlizando o SQL Management Studio é só copiar o código abaixo e rodar, em outra plataforma como Dbeaver, será necessário rodar um item por vez
Abra uma nova janela para rodar o script,  copie o código abaixo, da linha 11 a 35 e excute o código
-- 1 Cria o banco de dados 'Contacts'
CREATE DATABASE Contacts;
GO

-- 2 Seleciona o banco de dados 'Contacts'
USE Contacts;
GO

-- 3 Cria a tabela 'ContatctInformation'
CREATE TABLE ContatctInformation (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Surname NVARCHAR(50) NOT NULL,
    DDD CHAR(2) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);
GO

-- 4 Insere um contato na tabela 'ContatctInformation'
INSERT INTO ContatctInformation (Name, Surname, DDD, Phone, Email)
VALUES ('João', 'Silva', '11', '99999-9999', 'joao.silva@email.com');
GO
-- 5 Lista os contatos inseridos
SELECT TOP 1 * FROM ContatctInformation