CREATE DATABASE DapperExercise
GO
USE DapperExercise
GO
CREATE SCHEMA SuperHero
GO
CREATE TABLE SuperHero.SuperHeroes 
(
	Id     int Not Null Identity(1,1) CONSTRAINT PK_SuperHero_SuperHeroes_Id Primary Key,
	[Name]  nvarchar(150) Not Null,
	FirstName nvarchar(180) Not Null,
	LastName  nvarchar(200) Not Null,
	Place     nvarchar(500) Not Null
)
GO