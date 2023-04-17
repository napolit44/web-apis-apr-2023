using AutoMapper;
using HrApi.Domain;
using HrApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrApi.Controllers;

public class DepartmentsController : Controller
{
    private readonly HrDataContext _context;
    private readonly IMapper _mapper;

    public DepartmentsController(HrDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("/departments")]
    public async Task<ActionResult> AddADepartment([FromBody] DepartmentCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400
        }

        var departmentToAdd = _mapper.Map<DepartmentEntity>(request);
        _context.Departments.Add(departmentToAdd);
        try
        {
            await _context.SaveChangesAsync();

            var response = _mapper.Map<DepartmentSummaryItem>(departmentToAdd);
            return Ok(response);
        }
        catch (DbUpdateException)
        {
            return BadRequest("That department exists");
        }
    }

    [HttpGet("/departments")]
    public async Task<ActionResult<DepartmentsResponse>> GetDepartments()
    {
        var response = new DepartmentsResponse
        {
            Data = await _context.Departments
                 .Select(d =>
                     new DepartmentSummaryItem {
                         Id = d.Id.ToString(),
                         Name = d.Name })
                 .ToListAsync()
        };
        return Ok(response);
    }
}
