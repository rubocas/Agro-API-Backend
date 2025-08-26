using Agro.API.Entidades;

public interface ILogEventoService
{
    Task SalvarLogAsync(LogEvento log);
}