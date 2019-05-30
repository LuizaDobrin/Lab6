using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAgendaProj.Services;
using TaskAgendaProj.ViewModels;

namespace TaskAgendaProj.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/Comments
        [HttpGet]
        public IEnumerable<CommentGetModel> GetAll([FromQuery]string filter)
        {
            return commentService.GetAllFiltered(filter);
        }
    }
}