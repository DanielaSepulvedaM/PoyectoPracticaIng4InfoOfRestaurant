CREATE TABLE [dbo].[Mesa] (
    [MesaID]        INT          IDENTITY (1, 1) NOT NULL,
    [Identificador] VARCHAR (10) NOT NULL,
    [RestauranteID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([MesaID] ASC),
    CONSTRAINT FK_Mesa_Restaurante FOREIGN KEY ([RestauranteID]) REFERENCES Restaurante ([RestauranteID])
);

