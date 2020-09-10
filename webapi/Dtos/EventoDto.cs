using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webapi.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage="{0} deve ser preenchido")]
        [StringLength(100,MinimumLength=3,
        ErrorMessage="Local deve ter entre 2 e 100 caracteres ")]
        public string Local { get; set; }
        public string  DataEvento { get; set; }

        [Required(ErrorMessage="{0} deve ser preenchido")] 
        public string Tema { get; set; }

        [Range(2,500,ErrorMessage="Quantidade entre 2 e 500 pessoas")]
        public int QtdPessoas { get; set; }
        public string  ImagemUrl { get; set; }

        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage="E-mail invalido")]
        public string Email { get; set; }       

        public List<LoteDto> Lotes { get; set; } 
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<PalestranteDto> Palestrantes  { get; set; }  
    }
}