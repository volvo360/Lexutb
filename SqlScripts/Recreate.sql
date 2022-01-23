--USE master
--CREATE DATABASE BlogPostDemo 

USE BlogPostDemo

DROP TABLE IF EXISTS BlogPost

CREATE TABLE BlogPost
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(50) NULL,
	Author NVARCHAR(50) NULL,
)

INSERT INTO BlogPost (Author, Title) VALUES ('Lily', 'The sun is bright')

INSERT INTO BlogPost (Author, Title) VALUES ('Ethan', 'I will go swimming')
