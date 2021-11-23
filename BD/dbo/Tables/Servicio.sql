CREATE TABLE [dbo].[Servicio] (
    [ServicioID]      INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]          VARCHAR (60)    NOT NULL,
    [Descripcion]     VARCHAR (150)   NOT NULL,
    [Peticiones] VARCHAR (100)   NULL,
    [Precio]      DECIMAL (12, 6) NOT NULL,
    [ConsumibleID]   INT        NOT     NULL,
    [CuentaID]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ServicioID] ASC),
    CONSTRAINT [FK_Servicio_Consumible] FOREIGN KEY ([ConsumibleID]) REFERENCES [dbo].[Consumible] ([ConsumibleID]),
    CONSTRAINT [FK_Servicio_Cuenta] FOREIGN KEY ([CuentaID]) REFERENCES [dbo].[Cuenta] ([CuentaID])
);

