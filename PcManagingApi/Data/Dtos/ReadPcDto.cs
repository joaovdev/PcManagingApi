namespace PcManagingApi.Data.Dtos;

public class ReadPcDto
{
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Usuario { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
}
