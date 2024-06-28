using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Service;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAll()
    {
        var entity = await _categoryRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<CategoryDTO>>(entity);
    }

    public async Task<CategoryDTO> GetById(int? id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id);

        return _mapper.Map<CategoryDTO>(entity);
    }

    public async Task Add(CategoryDTO categoryDto)
    {
        var entity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.CreateAsync(entity);
    }

    public async Task Update(CategoryDTO categoryDto)
    {
        var entity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.UpdateAsync(entity);
    }

    public async Task Remove(int? id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id);
        await _categoryRepository.RemoveAsync(entity);
    }
}