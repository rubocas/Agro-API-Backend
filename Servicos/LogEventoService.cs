using Agro.API.Entidades;
using Agro.Dados;

public class LogEventoService : ILogEventoService
{
    private readonly Contexto _contexto;

    public LogEventoService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task SalvarLogAsync(LogEvento log)
    {
        if (log == null) throw new ArgumentNullException(nameof(log));
        log.TimeStamp = DateTime.UtcNow;

        _contexto.LogEventos.Add(log);
        await _contexto.SaveChangesAsync();
    }
}