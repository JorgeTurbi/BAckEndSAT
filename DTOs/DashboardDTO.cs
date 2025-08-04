namespace DTOs;

public class DashboardDTO
{
    public required int UsuarioRegistrados { get; set; }
    public required int VacantesActivas { get; set; }
    public required int TotalAplicaciones { get; set; }
    public required int  TotalAsignaciones { get; set; }
}