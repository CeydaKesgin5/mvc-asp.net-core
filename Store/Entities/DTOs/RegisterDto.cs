using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public record RegisterDto
    {
        //record olduğu içn değerler tanımlandığı anda verilmeli
        [Required(ErrorMessage ="Username is required.")]
        public String? UserName {  get; init; }

        [Required(ErrorMessage = "Email is required.")]
        public String? Email { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }

    }
}
