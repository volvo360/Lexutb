
USE BlogPostDemo

IF NOT EXISTS
   (  SELECT [name]
      FROM sys.tables
      WHERE [name] = 'BlogPost'
   )
   BEGIN 
        CREATE TABLE  Personid (Id int IDENTITY(1,1) PRIMARY KEY, Author TEXT NOT NULL , Title TEXT NOT NULL);
 END

 INSERT INTO BlogPost (Author, Title) 
        VALUES ('Lily', 'The sun is bright')

INSERT INTO BlogPost (Author, Title) 
        VALUES ('Ethan', 'I will go swimming')