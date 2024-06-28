using Application.DTOs;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetAll();
    Task<CategoryDTO> GetById(int? id);
    Task Add(CategoryDTO categoryDto);
    Task Update(CategoryDTO categoryDto);
    Task Remove(int? id);
}