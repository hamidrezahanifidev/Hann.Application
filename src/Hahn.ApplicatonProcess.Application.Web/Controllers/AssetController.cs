using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.Application.Service.Dtos;
using Hahn.ApplicatonProcess.Application.Service.Exceptions;
using Hahn.ApplicatonProcess.Application.Service.Interface;
using Hahn.ApplicatonProcess.Application.Web.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicatonProcess.Application.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly ILogger<AssetController> _logger;
        private readonly IStringLocalizer<AssetController> _localizer;

        public AssetController(IAssetService assetService,
            ILogger<AssetController> logger,
            IStringLocalizer<AssetController> localizer)
        {
            _assetService = assetService;
            _logger = logger;
            _localizer = localizer;
        }

        /// <summary>
        /// Retrieves a specific asset by unique id
        /// </summary>
        /// <param name="id" example="1">The asset id</param>
        [HttpGet("{id}" , Name = "GetAsset")]
        [ProducesResponseType(typeof(AssetReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAsset(int id)
        {
            _logger.LogWarning("Getting asset by id: {ID}", id);

            var asset = _assetService.GetAssetById(id);
            if ( asset != null)
            {
                return Ok(asset);
            }
            return NotFound();
        }

        /// <summary>
        /// Creates an asset
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AssetReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsset(AssetCreateDto assetCreateDto)
        {
          
            try
            {
                var asset = await _assetService.CreaterAssetAsync(assetCreateDto);

                return CreatedAtRoute(nameof(GetAsset), new { id = assetCreateDto.ID }, asset);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Updates an asset
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public IActionResult UpdateAsset(AssetUpdateDto assetUpdateDto)
        {
            try
            {
                _assetService.UpdateAsset(assetUpdateDto);

                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes an asset
        /// </summary>
        /// /// <param name="id" example="1">The asset id</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        public IActionResult DeleteAsset(int id)
        {
            try
            {
                _assetService.DeleteAsset(id);

                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
