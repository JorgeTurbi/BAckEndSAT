namespace DTOs;
  public class ApplicanteDto
    {
        // Datos personales
        public int UserId { get; set; }
        public string? ProfileImage { get; set; }  // Base64 desde Angular
        public string? Name { get; set; }
        public string? Cedula { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Country { get; set; }
        public string? Nationality { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Institution { get; set; }
        public string? Rank { get; set; }
        public string? Specialization { get; set; }
        public string? Bio { get; set; }

        // Contacto de emergencia
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }

        // Habilidades
        public string? Skills { get; set; }

        // Arrays dinámicos
        public List<ExperienceDto> Experience { get; set; } = new();
        public List<EducationDto> Education { get; set; } = new();
    }

    public class ExperienceDto
    {
        public string? Company { get; set; }
        public string? Position { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
    }

    public class EducationDto
    {
        public string? Institution { get; set; }
        public string? Degree { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
    }