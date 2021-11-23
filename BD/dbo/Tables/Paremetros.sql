CREATE TABLE [dbo].[Paremetros] (
    [ParametroID]       INT            IDENTITY (1, 1) NOT NULL,
    [PorcentajeIVA]     DECIMAL (6, 2) NULL,
    [ImpuestoProductos] DECIMAL (6, 2) NULL,
    [ImpuestoConsumo]   DECIMAL (6, 2) NULL,
    [RestauranteID]         INT            NULL,
    PRIMARY KEY CLUSTERED ([ParametroID] ASC),
    CONSTRAINT [FK_Parametros_Restaurante] FOREIGN KEY ([RestauranteID]) REFERENCES [dbo].[Restaurante] ([RestauranteID])
);

