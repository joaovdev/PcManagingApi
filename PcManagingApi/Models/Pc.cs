using System.ComponentModel.DataAnnotations;

namespace PcManagingApi.Models;

public class Pc
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O modelo do Pc é obrigatorio")]
    public string Marca { get; set; }

    [Required(ErrorMessage = "O modelo do Pc é obrigatorio")]
    [MaxLength(50, ErrorMessage = "O tamanho do modelo nao pode exceder 50 caracteres")]
    public string Modelo { get; set; }

    public string Usuario { get; set; }

}
