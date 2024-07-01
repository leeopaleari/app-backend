using Application.DTOs;
using Application.DTOs.Category;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<ReadCategoryDto>> GetAll();
    Task<ReadCategoryDto> GetById(int? id);
    Task<CreateCategoryDto> Add(CreateCategoryDto categoryDto);
    Task Update(CreateCategoryDto categoryDto);
    Task Remove(int? id);
}