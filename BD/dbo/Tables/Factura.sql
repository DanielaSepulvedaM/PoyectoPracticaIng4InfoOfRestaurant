CREATE TABLE [dbo].[Factura] (
    [FacturaID]           INT             IDENTITY (1, 1) NOT NULL,
    [Fecha]               DATE            NOT NULL,
    [NombreCliente]       VARCHAR (60)    NULL,
    [TotalCuenta]         DECIMAL (12, 2) NULL,
    [CodAutorizacionPago] INT             NOT NULL,
    [NombreFuncionario]   VARCHAR (60)    NULL,
    [CuentaID]          INT             NULL,
    [UsuarioID]           INT             NULL,
    PRIMARY KEY CLUSTERED ([FacturaID] ASC),
    CONSTRAINT [FK_Factura_Cuenta] FOREIGN KEY ([CuentaID]) REFERENCES [dbo].[Cuenta] ([CuentaID])
);

