using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDoApp.Models.Dto;
using ToDoApp.Models;
using ToDoApp.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace ToDoApp.Controllers
{
        [Route("api/ColorApi")]
        [ApiController]
        [Authorize]

    public class ColorController : ControllerBase
        {
            protected APIResponse _response;
            private readonly IColor _dbRepository;
            private readonly IMapper _mapper;
            public ColorController(IColor dbRepository, IMapper mapper)
            {
                _dbRepository = dbRepository;
                _mapper = mapper;
                this._response = new();
            }

            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<ActionResult<APIResponse>> GetColors()
            {
                try
                {
                    IEnumerable<Color> colorList = await _dbRepository.GetAllAsync();
                    _response.Result = _mapper.Map<List<ColorDto>>(colorList);
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

            [HttpGet("{id:int}", Name = "GetColor")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<APIResponse>> GetColor(int id)
            {
                try
                {

                    if (id == 0)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;

                        return BadRequest(_response);
                    }
                    var color = await _dbRepository.GetAsync(u => u.id == id);
                    if (color == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound();
                    }
                    _response.Result = _mapper.Map<ColorDto>(color);
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
        public async Task<ActionResult<APIResponse>> CreateNote([FromBody] ColorCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }


                Color model = _mapper.Map<Color>(createDto);
                


                await _dbRepository.CreateAsync(model);
                _response.Result = _mapper.Map<ColorDto>(model);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNote", new { Id = model.id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteColor")]
        public async Task<ActionResult<APIResponse>> DeleteNote(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var color = await _dbRepository.GetAsync(u => u.id == id);
                if (color == null)
                {
                    return NotFound();
                }
                await _dbRepository.RemoveAsync(color);

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
        [HttpPut("{id:int}", Name = "UpdateColor")]
        public async Task<ActionResult<APIResponse>> UpdateNote(int id, [FromBody] ColorUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.id)
                {
                    return BadRequest();
                }

                Color model = _mapper.Map<Color>(updateDto);



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
