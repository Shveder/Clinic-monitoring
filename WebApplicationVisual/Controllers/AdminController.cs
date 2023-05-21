using Microsoft.AspNetCore.Mvc;
using WebApplicationVisual.DTO;
using WebApplicationVisual.Models;
using WebApplicationVisual.Repositories;

namespace WebApplicationVisual.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }

        [HttpGet, Route("getUserById")]
        public async Task<IActionResult> GetUserById(int userId)
    {
        UserRepository userRepository = new UserRepository();
        try
        {
            User user = userRepository.GetUserById(userId);
            Console.WriteLine(user.toString());
            _logger.LogInformation("Пользователь получен");
            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogInformation("Пользователь не найден");
            return BadRequest("Пользователь не найден");
        }
    }

        [HttpPut, Route("setStatusDel")]
        public async Task<ActionResult> SetStatusDel([FromBody] AddSetStatusDeletedRequest request)
    {
        try
        {
            var ur = new AdminRepository();
            ur.SetUserStatusDel(request.Id, request.Is_deleted);
            _logger.LogInformation("Статус изменен");
            return Ok("Статус изменен");
        }
        catch (Exception)
        {
            _logger.LogInformation("Статус не изменен");
            return BadRequest("Статус не изменен");
        }
    }

        [HttpDelete, Route("deleteAccount")]
        public async Task<ActionResult> DeleteAccount(int id)
    {
        try
        {
            var ur = new AdminRepository();
            ur.DeleteUser(id);
            _logger.LogInformation("Аккаунт успешно удален");
            return Ok("Аккаунт успешно удален");
        }
        catch (Exception)
        {
            _logger.LogInformation("Аккаунт не удален");
            return BadRequest("Аккаунт не удален");
        }
    }

        [HttpPut, Route("setStatusBlock")]
        public async Task<ActionResult> SetStatusBlock([FromBody] AddSetStatusBlockedRequest request)
    {
        try
        {
            var ur = new AdminRepository();
            ur.SetUserStatusBlock(request.Id, request.Is_blocked);
            _logger.LogInformation("Статус изменен");
            return Ok("Статус изменен");
        }
        catch (Exception)
        {
            _logger.LogInformation("Статус не изменен");
            return BadRequest("Статус не изменен");
        }
    }

        [HttpGet, Route("getUserList")]
        public async Task<ActionResult> GetUserList()
    {
        var ur = new Repositories.AdminRepository();
        try
        {
            _logger.LogInformation("Список пользователей доступен");
            return Ok(ur.GetUserList());
        }
        catch (Exception)
        {
            _logger.LogInformation("Список пользователей недоступен");
            return BadRequest("Список пользователей недоступен");
        }
    }

        /*[HttpPut, Route("editUser")]
        public async Task<ActionResult> EditUser([FromBody] EditUserRequest request)
    {
        if (request.Balance < 0)
        {
            return UnprocessableEntity("Баланс должен быть положительный");
        }
        _logger.LogInformation($"Изменить логин: (" + request.Login + ")\n пароль: (" + request.Password + ")\n роль: (" +
                               request.Role + ")\n баланс: " + request.Balance);
        var ur = new UserRepository();
        var ar = new AdminRepository();
        try
        {
            if (request.Login != null)
            {
                _logger.LogInformation("Логин изменен");
                ur.ChangeUserName(request.Id, request.Login);
            }

            if (request.Password != null)
            {
                _logger.LogInformation("Пароль изменен");
                ur.ChangePassword(request.Id, request.Password);
            }

            if (request.Role != null && request.Role > 0 && request.Role < 4)// 1 - User,2 - admin,3 - main admin
            {
                _logger.LogInformation("Роль изменена");
                ar.ChangeUserRole(request.Id, request.Role);
            }

            if (request.Balance != null)
            {
                _logger.LogInformation("Баланс изменен");
                ar.ChangeUserBalance(request.Id, request.Balance);
            }

            return Ok("Пользователь отредактирован");
        }
        catch (Exception)
        {
            return BadRequest("Пользователь не изменен");
        }
    }*/

        [HttpGet, Route("getUserPreviousPasswordsList")]
        public async Task<ActionResult> GetUserPreviousPasswordsList(int id)
        {
            var ur = new Repositories.UserRepository();
            try
            {
                var passwords = ur.GetUserPasswordsHistory(id);
                if (passwords.Count == 0)
                {
                    _logger.LogInformation("Здесь нет паролей");
                    return Ok("Здесь нет паролей");
                }
                _logger.LogInformation("Пароли доступны");
                return Ok(passwords);
            }
            catch (Exception)
            {
                return BadRequest("Невозможно получить пароли");
            }
        }

        [HttpPost("AddDoctor")]
        public async Task<ActionResult> AddDoctor([FromBody] AddDoctorRequest request)
        {
            try
            {
                var ur = new AdminRepository();
                var ar = new AuthorizationRepository();
                if (!ar.IsValidEmail(request.Email))
                {
                    _logger.LogInformation("Email неверный");
                    return UnprocessableEntity("Email неверный");
                }

                ur.WriteNewDoctorToDatabase(request.Name, request.Speciality, request.Phone, request.Email);
                _logger.LogInformation("Доктор добавлен");
                return Ok("Доктор добавлен");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Доктор не добавлен");
                return BadRequest("Доктор не добавлен");
            }
        }

        [HttpDelete, Route("deleteDoctor")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            try
            {
                var ar = new AdminRepository();
                ar.DeleteDoctor(id);
                _logger.LogInformation("Доктор удален");
                return Ok("Доктор удален");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Доктор не удален");
                return BadRequest("Доктор не удален");
            }
        }

        [HttpGet, Route("getDoctorList")]
        public async Task<ActionResult> GetDoctorList()
        {
            var ur = new AdminRepository();
            try
            {
                _logger.LogInformation("Список докторов доступен");
                return Ok(ur.GetDoctorList());
            }
            catch (Exception)
            {
                _logger.LogInformation("Список докторов недоступен");
                return BadRequest("Список докторов недоступен");
            }
        }

        [HttpPost("AddClinic")]
        public async Task<ActionResult> AddClinic([FromBody] AddClinicRequest request)
        {
            try
            {
                var ar = new AdminRepository();
                AuthorizationRepository repository = new AuthorizationRepository();
                if (!repository.IsValidEmail(request.Email))
                {
                    _logger.LogInformation("Email неккоректен");
                    return UnprocessableEntity("Email неккоректен");
                }

                ar.WriteNewClinicToDatabase(request.Name, request.Address, request.Email);
                _logger.LogInformation("Клиника добавлена");
                return Ok("Клиника добавлена");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Клиника не добавлена");
                return BadRequest("Клиника не добавлена");
            }
        }

        [HttpDelete, Route("deleteClinic")]
        public async Task<ActionResult> DeleteClinic(int id)
        {
            try
            {
                var ar = new AdminRepository();
                ar.DeleteClinic(id);
                _logger.LogInformation("Клиника удалена");
                return Ok("Клиника удалена");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Клиника не удалена");
                return BadRequest("Клиника не удалена");
            }
        }

        [HttpGet, Route("getClinicList")]
        public async Task<ActionResult> GetClinicList()
        {
            var ur = new AdminRepository();
            try
            {
                _logger.LogInformation("Список клиник доступен");
                return Ok(ur.GetClinicList());
            }
            catch (Exception)
            {
                _logger.LogInformation("Список клиник недоступен");
                return BadRequest("Список клиник недоступен");
            }
        }

        [HttpPost("AddService")]
        public async Task<ActionResult> AddService([FromBody] AddServiceRequest request)
        {
            try
            {
                if (request.Price < 0)
                {
                    _logger.LogInformation("Цена должна быть положительной");
                    return UnprocessableEntity("Цена должна быть положительной");
                }
                var ar = new AdminRepository();
                if (ar.IsThereThisDoctor(request.DoctorId) == false)
                {
                    _logger.LogInformation("Доктор с id = " + request.DoctorId + " не существует");
                    return BadRequest("Доктор с id = " + request.DoctorId + " не существует");
                }

                if (ar.IsThereThisClinic(request.ClinicId) == false)
                {
                    _logger.LogInformation("Клиника с id = " + request.ClinicId + " не существует");
                    return BadRequest("Клиника с id= " + request.ClinicId + " не существует");
                }

                if (request.ExpertViewPrice < 0)
                {
                    _logger.LogInformation("Цена экспертного мнения должна быть положительной");
                    return UnprocessableEntity("Цена экспертного мнения должна быть положительной");
                }
                var sr = new ServicesRepository();
                sr.WriteServiceToDataBase(request.ServiceName, request.DoctorId, request.ClinicId, request.Price, request.ExpertView, request.ExpertViewPrice);
                _logger.LogInformation("Тариф добавлен");
                return Ok("Тариф добавлен");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Тариф не добавлен");
                return BadRequest("Тариф не добавлен");
            }
        }

        [HttpPut, Route("changePriceOfService")]
        public async Task<ActionResult> ChangePriceOfService([FromBody] AddChangePriceRequest request)
        {
            try
            {
                if (request.Price < 0)
                {
                    return UnprocessableEntity("Цена должна быть положительной");
                }
                var sr = new ServicesRepository();
                var or = new ObserverRepository();
                if (request.Price == sr.GetServiceById(request.Id).Price)
                {
                    _logger.LogInformation("Цена должна отличаться");
                    return UnprocessableEntity("Цена должна отличаться");
                }
                sr.AddRecentPrice(sr.GetServiceById(request.Id));
                or.ChangePrice(request.Id, request.Price);
                _logger.LogInformation("Цена сервиса с id = " + request.Id + " равна " + request.Price);
                return Ok("Цена сервиса с id = " + request.Id + " равна " + request.Price);
            }
            catch (Exception e)
            {
             _logger.LogInformation("Цена не изменена");
                return BadRequest("Цена не изменена");
            }
        }

        [HttpGet, Route("getRecentPricesList")]
        public async Task<ActionResult> GetRecentPricesList(int id)
        {
            try
            {
                var sr = new ServicesRepository();
                if (sr.GetRecentPricesListFromDataBase(id).Count == 0)
                {
                    _logger.LogInformation("Тут нет цен");
                    return Ok("Тут нет цен");
                }
                _logger.LogInformation("Лист цен доступен");
                return Ok(sr.GetRecentPricesListFromDataBase(id));
            }
            catch (Exception e)
            {
                _logger.LogInformation("Лист цен недоступен");
                return BadRequest("Лист цен недоступен");
            }
        }

        [HttpGet, Route(("getLoginHistoryList"))]
        public async Task<ActionResult> GetLoginHistoryList()
        {
            try
            {
                var ar = new AdminRepository();
                var loginList = ar.GetLoginHistory();
                _logger.LogInformation("История входов доступна");
                return Ok(loginList);
            }
            catch (Exception e)
            {
                _logger.LogInformation("История входов недоступна");
                return BadRequest("История входов недоступна");
            }
        }
        
        [HttpDelete, Route("deleteService")]
        public async Task<ActionResult> DeleteService(int id)
        {
            try
            {
                var ur = new ServicesRepository();
                ur.DeleteService(id);
                _logger.LogInformation("Тариф с id = " + id + " удален");
                return Ok("Тариф с id = " + id + " удален");
            }
            catch (Exception)
            {
                _logger.LogInformation("Тариф с id = " + id + "не удален");
                return BadRequest("Тариф с id = " + id + "не удален");
            }
        }

        [HttpPut, Route("editExpertView")]
        public async Task<ActionResult> EditExpertView([FromBody] EditExpertViewRequest request)
        {
            try
            {
                var ar = new AdminRepository();
                ar.EditExpertView(request.ServiceId, request.NewView, request.NewPriceOfView);
                _logger.LogInformation("Мнение изменено");
                return Ok("Мнение изменено");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Невозможно изменить мнение");
                return BadRequest("Невозможно изменить мнение");
            }
        }
        
        [HttpGet, Route(("getDoctorById"))]
        public async Task<ActionResult> GetDoctorById(int id)
        {
            try
            {
                var ar = new AdminRepository();
                var doctor = ar.GetDoctorByID(id);
                if (!ar.IsThereThisDoctor(id))
                {
                    return BadRequest("Здесь нет этого доктора");
                }
                _logger.LogInformation("Доктор с id = " + id + " доступен");
                return Ok(doctor);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Доктор с id = " + id + " недоступен");
                return BadRequest("Доктор с id = " + id + " недоступен");
            }
        }
        
        [HttpGet, Route(("getClinicById"))]
        public async Task<ActionResult> GetClinicById(int id)
        {
            try
            {
                var ar = new AdminRepository();
                var clinic = ar.GetClinicByID(id);
                if (!ar.IsThereThisClinic(id))
                {
                    return BadRequest("Здесь нет этой клиники");
                }
                _logger.LogInformation("Клиника с  id = " + id + " доступна");
                return Ok(clinic);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Клиника с  id = " + id + " недоступна");
                return BadRequest("Клиника с  id = " + id + " недоступна");
            }
        }

        [HttpGet, Route(("getDoctorNameById"))]
        public async Task<ActionResult> GetDoctorNameById(int id)
        {
            try
            {
                var ar = new AdminRepository();
                var name = ar.GetDoctorNameById(id);
                if (!ar.IsThereThisDoctor(id))
                {
                    return BadRequest("Здесь нет этого доктора");
                }
                _logger.LogInformation("Имя доктора с id = " + id + " доступно");
                return Ok(name);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Имя доктора с id = " + id + " недоступно");
                return BadRequest("Имя доктора с id = " + id + " недоступно");
            }
        }
        
        [HttpGet, Route(("getClinicNameById"))]
        public async Task<ActionResult> GetClinicNameById(int id)
        {
            try
            {
                var ar = new AdminRepository();
                var name = ar.GetClinicNameById(id);
                if (!ar.IsThereThisClinic(id))
                {
                    return BadRequest("Здесь нет этой клиники");
                }
                _logger.LogInformation("Название клиники с id = " + id + " доступно");
                return Ok(name);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Название клиники с id = " + id + " недоступно");
                return BadRequest("Название клиники с id = " + id + " недоступно");
            }
        }
        
}