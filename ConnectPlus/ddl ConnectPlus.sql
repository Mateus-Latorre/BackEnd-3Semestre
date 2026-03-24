Create database ConnectPlus
Use ConnectPlus

Create Table TipoContato(
IdTipoContato UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
Titulo VARCHAR(100) NOT NULL
);

Create Table Contato(
IdContato UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
Nome VARCHAR(100) NOT NULL,
FormaDeContato VARCHAR(100) UNIQUE NOT NULL,
IdTipoContato UNIQUEIDENTIFIER FOREIGN KEY REFERENCES TipoContato(IdTipoContato)
)