using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PensionContributionMgmt.Application.Infrastructure;
using PensionContributionMgmt.Domain.DTOs;
using PensionContributionMgmt.Domain.Entitie;
using PensionContributionMgmt.Domain.DTOs.Employeer;

namespace PensionContributionMgmt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IUnitOfwork _unitOfwork;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiResponse;

        public EmployerController(IUnitOfwork unitOfwork, IMapper mapper)
        {
            this._unitOfwork = unitOfwork;
            this._mapper = mapper;
            _apiResponse=new (); 
        }
       
        [HttpGet]
        [Route("All", Name = "GetAllEmplyers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       
        public async Task<ActionResult<APIResponse>> GetEmployersAsync()
        {
            try
            {
                //  _logger.LogInformation("GetStudents method started");
                var employer = await _unitOfwork.Employer.GetAllAsync();

                _apiResponse.Data = _mapper.Map<List<EmployerDto>>(employer);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("{id:Guid}", Name = "GetEmployerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[DisableCors]
        public async Task<ActionResult<APIResponse>> GetEmployerByIdAsync(int id)
        {
            try
            {

                if (id <= 0)
                {
                    // _logger.LogWarning("Bad Request");
                    return BadRequest();
                }

                var employer = await _unitOfwork.Employer.GetAsync(u => u.Id == id);

                //NotFound - 404 - NotFound - Client error
                if (employer == null)
                {
                    // _logger.LogError("Student not found with given Id");
                    return NotFound($"The employer with id {id} not found");
                }

                _apiResponse.Data = _mapper.Map<EmployerDto>(employer);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
           }
           
        }
        [HttpPost]
        [Route("create")]
        //api/Employer/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> CreateEmployerAsync([FromBody] AddEmployerDto AddEmployerdto)
        {
            try
            {
                
                if (AddEmployerdto == null)
                    return BadRequest();  

                Employer employer = _mapper.Map<Employer>(AddEmployerdto);

                var studentAfterCreation = await _unitOfwork.Employer.AddAsync(employer);

                _apiResponse.Data = AddEmployerdto;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //Status - 201
                //https://localhost:7185/api/Student/3
                //New student details
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }

        }

    }
}
