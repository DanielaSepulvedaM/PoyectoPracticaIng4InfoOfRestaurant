CREATE TABLE [dbo].[FuncionarioRestaurante] (
    [FuncionarioRestauranteID] INT            IDENTITY (1, 1) NOT NULL,
    [RestauranteID]          INT            NOT NULL,
    [FuncionarioID]            NVARCHAR (450) NOT NULL,
    PRIMARY KEY CLUSTERED ([FuncionarioRestauranteID] ASC),
    CONSTRAINT [FK_Funcionario_Restaurante] FOREIGN KEY ([RestauranteID]) REFERENCES [dbo].[Restaurante] ([RestauranteID])
);

