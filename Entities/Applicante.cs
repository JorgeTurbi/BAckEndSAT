using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Aplicante {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Clave primaria de la entidad

        // Datos personales
        public int UserId { get; set; }
        public string ? ProfileImage { get; set; }  // Base64 o URL de la imagen
        [MaxLength(150)]
        public string ? Name { get; set; }
        [MaxLength(15)]
        public string ? Cedula { get; set; }
        public DateTime ? BirthDate { get; set; }
        [MaxLength(100)]
        public string ? Country { get; set; }
        [MaxLength(100)]
        public string ? Nationality { get; set; }
        [MaxLength(50)]
        public string ? MaritalStatus { get; set; }
        [MaxLength(50)]
        public string ? Phone { get; set; }
        [MaxLength(150)]
        public string ? Email { get; set; }
        [MaxLength(200)]
        public string ? Address { get; set; }
        [MaxLength(150)]
        public string ? Institution { get; set; }
        [MaxLength(50)]
        public string ? Rank { get; set; }
        [MaxLength(100)]
        public string ? Specialization { get; set; }
        public string ? Bio { get; set; }

// Contacto de emergencia
        [MaxLength(150)]
        public string ? EmergencyContact { get; set; }
        [MaxLength(50)]
        public string ? EmergencyPhone { get; set; }

        // Habilidades
        public string ? Skills { get; set; }
        public DateTime CreateAt { get; set; }

        // Relaciones 1:N
        public ICollection<Experience> Experience { get; set; } = new List<Experience>();
        public ICollection <Education> Education { get; set; } = new List<Education>();
    }
