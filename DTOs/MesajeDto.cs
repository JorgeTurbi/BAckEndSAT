namespace DTOs;

public class MensajeDto
{
public int Id { get; set; }
public int AplicanteId { get; set; }
public int VacanteId { get; set; }
public int ReclutadorId { get; set; }
public DateTime Fecha { get; set; }
public List<string>? Documentaciones { get; set; }
public int EstadoMensaje { get; set; }

}