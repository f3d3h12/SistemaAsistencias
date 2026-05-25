using ISFDyT124.Models;
using System.ComponentModel.DataAnnotations;

namespace ISFDyT124.DTOs
{

    public class UsuarioDTO // Define la clase DTO para usuario, usada para transferir datos entre capas 
    {
        //ID
        [Key]
        public int UsId { get; set; }


        //APELLIDO
        [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]*$", ErrorMessage = "Ingrese un apellido válido.")]
        [MaxLength(100, ErrorMessage = "No se permiten más de 100 caracteres.")] // Limita longitud a 100 caracteres 
        [Required(ErrorMessage = "Debe ingresar un Apellido")] // Campo obligatorio
        [Display(Name = "Apellido")] // Etiqueta para vistas
        public string UsApellido { get; set; } = null!; // Propiedad para el apellido - Negado nulo



        //NOMBRE
        [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]*$", ErrorMessage = "Ingrese un nombre válido.")]// Valida que el nombre solo contenga letras y espacios 
        [MaxLength(100, ErrorMessage = "No se permiten más de 100 caracteres.")] // Limita longitud a 100 caracteres 
        [Required(ErrorMessage = "Debe ingresar un Nombre.")] // Campo obligatorio
        [Display(Name = "Nombres")] // Etiqueta para vistas 
        public string UsNombre { get; set; } = null!; // Propiedad para el nombre - Negado nulo


        //DNI
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Sólo se permiten números de DNI válidos.")]
        [Range(6000000, 99999999, ErrorMessage = "Debe ingresar los 7-8 dígitos del DNI.")] // Valida que el DNI tenga un rango válido entre 7 y 8 dígitos 
        [Required(ErrorMessage = "Debe ingresar un número de DNI válido (8 dígitos).")] // Campo obligatorio 
        [Display(Name = "DNI")] // Etiqueta para mostrar en vistas y formularios
        public int UsDni { get; set; } // Propiedad para almacenar el DNI del usuario
    }
}
