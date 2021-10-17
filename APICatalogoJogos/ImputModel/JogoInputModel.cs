using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace APICatalogoJogos
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter no mínimo 3 e no máximo 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome do produtora deve conter no mínimo 3 e no máximo 100 caracteres")]
        public string Produtora { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "PREÇO INVÁLIDO, o valor do preço deve ser no mínimo R$ 1,00 e no no máximo R$ 1.000,00")]
        public double Preco { get; set; }
    }
}
