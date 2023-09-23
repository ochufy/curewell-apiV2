using AutoMapper;
using curewell.DAL.Contracts;
using curewell.Entity.Models;
using curewellAPI_ADO.NET.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace curewellAPI_ADO.NET.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICureWellRepository _cureWellRepository;
        private readonly IMapper _mapper;

        public HomeController(ICureWellRepository cureWellRepository, IMapper mapper)
        {
            _cureWellRepository = cureWellRepository;
            _mapper = mapper;
        }

        [HttpGet("doctors")]
        public IActionResult GetDoctors()
        {
            var data = _cureWellRepository.GetAllDoctors();
            return Ok(data);
        }

        [HttpGet("specializations")]
        public IActionResult GetSpecializations()
        {
            var data = _cureWellRepository.GetAllSpecializations();
            return Ok(data);
        }

        [HttpGet("surgeries")]
        public IActionResult GetAllSurgeryTypeForToday()
        {
            var data = _cureWellRepository.GetAllSurgeryTypeForToday();
            return Ok(data);
        }


        [HttpGet("specialization/{specializationCode}")]
        public IActionResult GetDoctorsBySpecialization([FromRoute] string specializationCode)
        {
            var data = _cureWellRepository.GetDoctorsBySpecialization(specializationCode);
            return Ok(data);
        }

        [HttpPost("add-doctor")]
        public IActionResult AddDoctor(Doctor dObj)
        {
            bool isDoctorAdded = _cureWellRepository.AddDoctor(dObj);
            return isDoctorAdded ? Ok(true) : BadRequest(false);
        }

        [HttpPut("update-doctor")]
        public IActionResult UpdateDoctorDetails(Doctor dObj)
        {
            bool isDoctorUpdated = _cureWellRepository.UpdateDoctorDetails(dObj);
            return isDoctorUpdated ? Ok(true) : BadRequest(false);
        }

        [HttpPut("update-surgery")]
        public IActionResult UpdateSurgery(SurgeryDto surgeryDto)
        {
            var sObj = _mapper.Map<Surgery>(surgeryDto);
            bool isSurgeryUpdated = _cureWellRepository.UpdateSurgery(sObj);
            return isSurgeryUpdated ? Ok(true) : BadRequest(false);
        }

        [HttpDelete("doctors/{doctorId}")]
        public IActionResult DeleteDoctor([FromRoute] int doctorId)
        {
            bool isDoctorDeleted = _cureWellRepository.DeleteDoctor(doctorId);
            return isDoctorDeleted ? Ok(true) : BadRequest(false);
        }
    }
}
