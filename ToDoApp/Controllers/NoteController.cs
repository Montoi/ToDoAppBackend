using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Models.Dto;
using ToDoApp.Repository.IRepository;

namespace ToDoApp.Controllers
{
    [Route("api/noteApi")]
    [Authorize]
    [ApiController]
    public class NoteController : ControllerBase
    {
        protected APIResponse _response;
        private readonly INotaRepository _dbRepository;
        private readonly IColor _dbColor;
        private readonly IMapper _mapper;
        public NoteController(INotaRepository dbRepository, IMapper mapper, IColor color)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;
            _dbColor = color;
            this._response = new();
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNotes()
        {
            try
            {
                IEnumerable<Note> notesList = await _dbRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<NoteDto>>(notesList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)    
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response; 
        }

        [HttpGet("{id:int}", Name = "GetNote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNote(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }
                var note = await _dbRepository.GetAsync(u => u.Id == id);
                if (note == null) 
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }
                _response.Result = _mapper.Map<NoteDto>(note);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task< ActionResult<APIResponse>> CreateNote([FromBody] NoteCreateDto createDto)
        {
            try { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }
                if (await _dbColor.GetAsync(u => u.id == createDto.ColorId) == null)
                {
                    ModelState.AddModelError("CustomError", "Color ID es Invalido");
                    return BadRequest(ModelState);
                }

                Note model = _mapper.Map<Note>(createDto);
                string claims = HttpContext.User.Claims.Where(e => e.Type == ClaimTypes.NameIdentifier).Select(x=>x.Value).SingleOrDefault();

                model.UserId = int.Parse(claims);

            await _dbRepository.CreateAsync(model);
            _response.Result = _mapper.Map<NoteDto>(model);
            _response.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetNote", new { Id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteNote")]
        public async Task<ActionResult<APIResponse>> DeleteNote(int id)
        {
            try { 
            if (id == 0)
            {
                return BadRequest();
            }
            var note = await _dbRepository.GetAsync(u => u.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            await _dbRepository.RemoveAsync(note);

            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateNote")]
        public async Task<ActionResult<APIResponse>> UpdateNote(int id, [FromBody]NoteUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }
                if (await _dbColor.GetAsync(u => u.id == updateDto.ColorId) == null)
                {
                    ModelState.AddModelError("CustomError", "Color ID es Invalido");
                    return BadRequest(ModelState);
                }

                Note model = _mapper.Map<Note>(updateDto);



                await _dbRepository.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }
    }
}
