CREATE TABLE [dbo].[Cuenta] (
    [CuentaID]          INT             IDENTITY (1, 1) NOT NULL,
    [FechaCreacion]             DATE            NOT NULL,
    [PorcentajePropina] DECIMAL (6, 2)  NOT NULL,
    [Subtotal]          DECIMAL (12, 2) NULL,
    [MesaID]        INT  NOT NULL,
    [RestauranteID] INT  NOT NULL,
    [Cerrada] BIT NOT NULL,
    [MetodoDePago] VARCHAR(10),
    [CodigoDeAprobacion] VARCHAR(100),
    PRIMARY KEY CLUSTERED ([CuentaID] ASC),
    CONSTRAINT [FK_Orden_Mesa] FOREIGN KEY ([MesaID]) REFERENCES [dbo].[Mesa] ([MesaID]),
    CONSTRAINT [FK_Cuenta_Restaurante] FOREIGN KEY ([RestauranteID]) REFERENCES [dbo].[Restaurante] ([RestauranteID]),
);

