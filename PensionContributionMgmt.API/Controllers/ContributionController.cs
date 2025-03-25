using System.Net;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;
using PensionContributionMgmt.Application.Infrastructure;
using PensionContributionMgmt.Domain.DTOs;
using PensionContributionMgmt.Domain.DTOs.Contribution;
using PensionContributionMgmt.Domain.DTOs.Employeer;
using PensionContributionMgmt.Domain.Entitie;

namespace PensionContributionMgmt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {
        private readonly IUnitOfwork _unitOfwork;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiResponse;

        public ContributionController(IUnitOfwork unitOfwork, IMapper mapper)
        {
            this._unitOfwork = unitOfwork;
            this._mapper = mapper;
            _apiResponse=new ();
        }

        [HttpPost("monthly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddMonthlyContribution([FromBody] AddContributioDto contributionRegDto)
        {

            try
            {
                if(contributionRegDto !=null )
                {
                    await _unitOfwork.Contribution.HasMonthlyContribution(contributionRegDto.MemberId, contributionRegDto.ContributionDate);
                }else
                {
                  return BadRequest("A monthly contribution already exists for this month.");
                }               
                Contribution contribution = _mapper.Map<Contribution>(contributionRegDto);
                contribution.IsVoluntary = false;
                contribution.IsMonthly = true;
                contribution.ContributionType = "Monthly";

                await _unitOfwork.Contribution.AddAsync(contribution);
               

                _apiResponse.Data = contribution;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                // return CreatedAtRoute("GetMemberContributionsByMemberId",new { id = contribution.Id },  _apiResponse);
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
        [Route("voluntary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddVoluntaryContribution([FromBody] AddContributioDto contributionRegDto)
        {
            try
            {

                if (contributionRegDto == null)
                    return BadRequest();
                Contribution contribution = _mapper.Map<Contribution>(contributionRegDto);
                contribution.ContributionType = "Yearly";
                contribution.IsVoluntary = true;
                await _unitOfwork.Contribution.AddAsync(contribution);
               
                _apiResponse.Data = contribution;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                // return CreatedAtRoute("GetMemberContributionsByMemberId",new { id = contribution.Id },  _apiResponse);
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
       [Route("{memberId}", Name = "GetMemberContributionsByMemberId")]
        public async Task<ActionResult<APIResponse>> GetMemberContributionsByMemberId(Guid memberId)
        {
            try
            {
                if (memberId <= Guid.Empty)
                {
                    // _logger.LogWarning("Bad Request");
                    return BadRequest();
                }
                var contributions = await _unitOfwork.Contribution.GetAsync(u => u.MemberId == memberId);
                if (contributions == null)
                {
                    // _logger.LogError("Student not found with given Id");
                    return NotFound($"The employer with id {memberId} not found");
                }
                _apiResponse.Data = contributions;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(contributions);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
           
        }

       
        [HttpGet("statements/{memberId}")]
        public async Task<ActionResult<APIResponse>> GetContributionStatement(Guid memberId)
        {
            try
            {
                var total = await _unitOfwork.Contribution.GetTotalContributionsAsync(memberId);
                _apiResponse.Data = total;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(new { MemberId = memberId, TotalContributions = total });

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

