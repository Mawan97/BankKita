using BankKita.Web.API.Contract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ViewModels.Rekening;

namespace BankKita.Web.API.Controllers
{
    [ApiController]
    [Route("api/rekening")]
    public class RekeningController : ControllerBase
    {
        private readonly IRekeningRepository _rekeningRepository;
        private readonly IJenisRekeningRepository _jenisRekeningRepository;

        public RekeningController(IRekeningRepository rekeningRepository, IJenisRekeningRepository jenisRekeningRepository)
        {
            _rekeningRepository = rekeningRepository;
            _jenisRekeningRepository = jenisRekeningRepository;
        }

        [HttpGet]
        public IActionResult GetRekeningAll()
        {
            try
            {
                var result = _rekeningRepository.GetRekeningAll();

                if (result.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{noRekening}")]
        public IActionResult GetRekeningByNoRekening(string noRekening)
        {
            try
            {
                var result = _rekeningRepository.GetRekeningByNo(noRekening);

                if (result is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult InsertRekening([FromBody] RekeningUpsert dataInput)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var jenisRekeningList = _jenisRekeningRepository.GetJenisRekeningDropDown();

                if (!jenisRekeningList.Any(x => x.Value == dataInput.JenisrekeningId))
                {
                    return NotFound(new { Message = "Jenis Rekening tidak ditemukan !" }); 
                }

                _rekeningRepository.UpsertRekening(null, dataInput);

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{noRekening}")]
        public IActionResult UpdateRekening([FromBody] RekeningUpsert dataInput, string noRekening)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rekeningUpdated = _rekeningRepository.GetRekeningByNo(noRekening);

                if (rekeningUpdated is null)
                {
                    return NotFound(new { Message = "Rekening tidak ditemukan !" });
                }

                _rekeningRepository.UpsertRekening(noRekening, dataInput);

                return StatusCode(200, new { Message = "Rekening berhasil diupdate !" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{noRekening}")]
        public IActionResult DeleteRekening(string noRekening)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rekeningDeleted = _rekeningRepository.GetRekeningByNo(noRekening);

                if (rekeningDeleted is null)
                {
                    return NotFound(new { Message = "Rekening tidak ditemukan !" });
                }

                _rekeningRepository.DeleteRekening(noRekening);

                return StatusCode(200, new { Message = "Rekening berhasil dihapus !" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
