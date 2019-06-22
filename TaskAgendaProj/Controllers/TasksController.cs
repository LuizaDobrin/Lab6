using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using TaskAgendaProj.Models;
using TaskAgendaProj.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAgendaProj.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace TaskAgendaProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskService taskService;
        private IUsersService usersService;
        public TasksController(ITaskService taskService, IUsersService usersService)
        {
            this.taskService = taskService;
            this.usersService = usersService;
        }


        /// <summary>
        /// Returneaza toate task-urile
        /// </summary>
        /// <param name="from"> Optional, filtreaza dupa data adaugarii de la</param>
        /// <param name="to">Optional, filtreaza dupa data adaugarii pana la</param>
        /// <remarks>
        /// Sample response:
        ///
        ///     Get /filme
        ///    {
        ///    "id":1,"title":"Sustinere3 lab",
        ///    "description":"Raport task-uri pe luna Mai 2019",
        ///    "dateTimeAdded":"2019-06-02T12:00:00",
        ///    "deadline":"2019-07-31T17:00:00",
        ///    "importance":"High",
        ///    "status":"In_progress",
        ///    "dateTimeClosedAt":"2019-08-01T18:00:00",
        ///    "comments":
        ///    [
        ///                  {
        ///                  "id":1,
        ///                  "text":"Nu",
        ///                  "important":false
        ///             },
        ///              {
        ///                   id: 2,
        ///                   "text": "Da",
        ///                   "important": true,
        ///
        ///             }
        ///         ]
        ///     }
        ///
        ///     </remarks>
        ///     <returns>lista cu task-uri</returns>


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/tasks
      //  [Authorize(Roles = "Admin,Regular")]   -crapa la browser pt ca nu are voie sa afiseze
        [HttpGet]
        public PaginatedList<TaskGetModel> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]int page = 1)
        {
            // TODO: make pagination work with /api/tasks/page/<page number>
            page = Math.Max(page, 1);
            return taskService.GetAll(page, from, to);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/tasks/2
        [Authorize(Roles = "Admin,Regular")]
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = taskService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
      
        // POST: api/Expenses
       // [Authorize(Roles = "Admin,Regular")]
        [HttpPost]
        public void Post([FromBody] TaskPostModel task)
        {
            User addedBy = usersService.GetCurentUser(HttpContext);
            //if (addedBy.UserRole == UserRole.UserManager)
            //{
            //    return Forbid();
            //}
            taskService.Create(task, addedBy);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        // PUT: api/Expenses/2
        [Authorize(Roles = "Admin,Regular")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TaskPostModel task)
        {

            var result = taskService.Upsert(id, task);
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/2
        [Authorize(Roles = "Admin,Regular")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = taskService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}