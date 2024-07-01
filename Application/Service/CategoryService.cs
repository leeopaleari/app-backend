using Application.DTOs;
using Application.DTOs.Category;
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

    public async Task<IEnumerable<ReadCategoryDto>> GetAll()
    {
        var entity = await _categoryRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ReadCategoryDto>>(entity);
    }

    public async Task<ReadCategoryDto> GetById(int? id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id);

        return _mapper.Map<ReadCategoryDto>(entity);
    }

    public async Task<CreateCategoryDto> Add(CreateCategoryDto categoryDto)
    {
        var entity = _mapper.Map<Category>(categoryDto);
        var createdCategory = await _categoryRepository.CreateAsync(entity);
        
        return _mapper.Map<CreateCategoryDto>(createdCategory);
    }

    public async Task Update(CreateCategoryDto categoryDto)
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