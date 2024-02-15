using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Company;
using backend.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CompanyController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //CRUD
        //Create

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto companyCreateDto)
        {
            var newCompany = _mapper.Map<Company>(companyCreateDto);
            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();
            return Ok("Company Created Succssfully");
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<CompanyGetDto>>> GetCompanies()
        {
            var companies = await _context.Companies.OrderByDescending(q =>q.CreatedAt).ToListAsync();
            var companyMap= _mapper.Map<IEnumerable<CompanyGetDto>>(companies);

            return Ok(companyMap);

        }
    }
}
