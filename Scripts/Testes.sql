USE BaseMoedas
GO

SELECT * FROM dbo.Cotacoes

INSERT INTO dbo.Cotacoes (Sigla, Horario, Valor)
VALUES ('USD', GETDATE(), 5.05)

UPDATE dbo.Cotacoes SET Excluir = 1
WHERE Sigla = 'USD'

DELETE FROM dbo.Cotacoes
WHERE Sigla = 'USD'