using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebIdentity.Entities
{
    public class Produto
    {
        public int ProdutoId {  get; set; }
        [Required, MaxLength(80, ErrorMessage = "Nome não pode exceder 80 caracteres")]
        public string? Nome { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }
    }
}
