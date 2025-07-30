using DTOs;

namespace Services.Perfil;
   public interface IUserProfileService
    {
        // CRUD
    //    Task<GenericResponseDto<List<ApplicanteDto>>> GetAllAsync();
        Task<GenericResponseDto<ApplicanteDto>> GetByIdAsync(int id);
        Task<GenericResponseDto<bool>> CreateAsync(ApplicanteDto dto);
        // Task<GenericResponseDto<bool>> UpdateAsync(int id, UserProfileDto dto);
        // Task<GenericResponseDto<bool>> DeleteAsync(int id);
    }