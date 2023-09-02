using Assessment.Management;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Assessment : ControllerBase
    {
        private readonly AssessmentManagement _assessmentManagement;

        public Assessment(AssessmentManagement assessmentManagement)
        {
            _assessmentManagement = assessmentManagement;
        }

        [HttpGet("GetPlatformWellActual")]
        public async Task<string> GetPlatformWellActual()
        {
            var result = await _assessmentManagement.GetPlatformWellActual();
            return result;
        }

        [HttpGet("GetPlatformWellDummy")]
        public async Task<string> GetPlatformWellDummy()
        {
            var result = await _assessmentManagement.GetDataPlatformWellDummy();
            return result;
        }
    }
}
