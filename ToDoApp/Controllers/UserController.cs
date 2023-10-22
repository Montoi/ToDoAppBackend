using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDoApp.Models.Dto;
using ToDoApp.Models;
using ToDoApp.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace ToDoApp.Controllers
{
    [Route("api/UserApi")]
   

    [ApiController]
    public class UserController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUser _dbRepository;
        private readonly IMapper _mapper;
        public UserController(IUser dbRepository, IMapper mapper)
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
                IEnumerable<User> userList = await _dbRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<UserDto>>(userList);
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


        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetUser(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }
                var User = await _dbRepository.GetAsync(u => u.Id == id);
                if (User == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }
                _response.Result = _mapper.Map<UserDto>(User);
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
        public async Task<ActionResult<APIResponse>> CreateNote([FromBody] UserCreateDto createDto)
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


                User model = _mapper.Map<User>(createDto);



                await _dbRepository.CreateAsync(model);
                _response.Result = _mapper.Map<UserDto>(model);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetUser", new { Id = model.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteUser")]
        public async Task<ActionResult<APIResponse>> DeleteUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var user = await _dbRepository.GetAsync(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                await _dbRepository.RemoveAsync(user);

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
        [HttpPut("{id:int}", Name = "UpdateUser")]
        public async Task<ActionResult<APIResponse>> UpdateNote(int id, [FromBody] UserUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }

                User model = _mapper.Map<User>(updateDto);



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
