using Microsoft.AspNetCore.Mvc;
using ResourcePlanner.Api.DTOs;
using ResourcePlanner.Core.Entities;
using ResourcePlanner.Core.Repositories;
using ResourcePlanner.Core.UnitOfWork;

namespace ResourcePlanner.Api.Controllers
{
    [ApiController]
    [Route("api/resources")]
    public class ResourceController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAllResourcesDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllResources([FromServices] IResourceRepository repo)
        {
            var resources = await repo.GetAllAsync();
            return Ok(resources.Select(x => new GetAllResourcesDTO()
            {
                Id = x.Id,
                Name = x.Name
            }));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetResourceByIdDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetResourceById(int id, [FromServices] IResourceRepository repo)
        {
            var resource = await repo.GetByIdAsync(id);
            if (resource == null)
            {
                return NotFound();
            }
            return Ok(new GetResourceByIdDTO()
            {
                Name = resource.Name
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetResourceByIdDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateResource([FromBody] CreateResourceRequestDTO createResourceRequestDTO, [FromServices] IUnitOfWork uow)
        {
            Resource resource = new()
            {
                Name = createResourceRequestDTO.Name
            };
            await uow.Resources.AddAsync(resource);
            await uow.CompleteAsync();
            return CreatedAtAction(nameof(GetResourceById), new { id = resource.Id }, new CreateResourceReponseDTO()
            {
                Id = resource.Id,
                Name = resource.Name
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateResource(int id, [FromBody] UpdateResourceRequestDTO updateResourceRequestDTO, [FromServices] IUnitOfWork uow)
        {

            Resource resource = new()
            {
                Id = id,
                Name = updateResourceRequestDTO.Name
            };
            uow.Resources.Update(resource);
            await uow.CompleteAsync();
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteResource(int id, [FromServices] IUnitOfWork uow)
        {
            var resource = await uow.Resources.GetByIdAsync(id);
            if (resource == null)
            {
                return NotFound();
            }
            uow.Resources.Delete(resource);
            await uow.CompleteAsync();
            return NoContent(); 
        }
    }
}
