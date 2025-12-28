using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CategoryController(ICategoryRepository CategoryRepository,IMapper mapper,UserManager<AppUser> userManager)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appuserid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (appuserid==null)
            {
                return Unauthorized();
            }
            var appuser = await _userManager.FindByIdAsync(appuserid);
            var categories = await _CategoryRepository.GetAllAsync(appuser!);
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appuserid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (appuserid==null)
            {
                return Unauthorized();
            }
            var appuser = await _userManager.FindByIdAsync(appuserid);
            var category = await _CategoryRepository.GetByIdAsync(appuser!,id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto CreateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appuserid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (appuserid==null)
            {
                return Unauthorized();
            }
            var category = _mapper.Map<Category>(CreateCategoryDto);
            category.AppUserId = appuserid;
            await _CategoryRepository.CreateAsync(category);
            return CreatedAtAction(nameof(GetById),new{id = category.Id},_mapper.Map<CategoryDto>(category));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryDto UpdateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appuserid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (appuserid==null)
            {
                return Unauthorized();
            }
            var appuser = await _userManager.FindByIdAsync(appuserid);
            var update = await _CategoryRepository.UpdateAsync(appuser!,id,UpdateCategoryDto);

            if (update==null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDto>(update));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appuserid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (appuserid==null)
            {
                return Unauthorized();
            }
            var appuser = await _userManager.FindByIdAsync(appuserid);
            var delete = await _CategoryRepository.DeleteAsync(appuser!,id);

            if (delete==null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}