using Microsoft.AspNetCore.Mvc;
using SE4458FinalProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SE4458FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly JobService _jobService;
        public JobController(JobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAll(
            [FromQuery] string? title,
            [FromQuery] string? company,
            [FromQuery] string? country,
            [FromQuery] string? city,
            [FromQuery] string? town,
            [FromQuery] string? department,
            [FromQuery] string? positionLevel,
            [FromQuery] string? workType)
        {
            var jobs = await _jobService.SearchAsync(title, company, country, city, town, department, positionLevel, workType);
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetById(string id)
        {
            var job = await _jobService.GetAsync(id);
            if (job == null) return NotFound();
            return Ok(job);
        }

        [HttpPost]
        [Authorize(Roles = "admin,company")]
        public async Task<ActionResult<Job>> Create(Job job)
        {
            await _jobService.CreateAsync(job);
            return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,company")]
        public async Task<IActionResult> Update(string id, Job updatedJob)
        {
            var job = await _jobService.GetAsync(id);
            if (job == null) return NotFound();
            updatedJob.Id = id;
            await _jobService.UpdateAsync(id, updatedJob);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,company")]
        public async Task<IActionResult> Delete(string id)
        {
            var job = await _jobService.GetAsync(id);
            if (job == null) return NotFound();
            await _jobService.RemoveAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/apply")]
        public async Task<IActionResult> Apply(string id)
        {
            var success = await _jobService.ApplyAsync(id);
            if (!success) return NotFound();
            return Ok(new { message = "Başvuru başarılı!" });
        }
    }
} 