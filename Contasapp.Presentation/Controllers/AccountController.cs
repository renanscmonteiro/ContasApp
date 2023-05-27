using Bogus;
using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Messages.Models;
using ContasApp.Messages.Services;
using ContasApp.Presentation.Helpers;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Contasapp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        //GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        //Post: Account/Login
        [HttpPost]
        public IActionResult Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // buscar o usuário no banco de dados através do e-mail e senha
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, MD5Helper.Encrypt(model.Senha));

                    //Verificando se o usuário foi encontrado
                    if (usuario != null)
                    {
                        // Criar os dados que serão gravados no cookie para autenticação do usuário
                        var auth = new AuthViewModel
                        {
                            Id = usuario.Id,
                            Nome = usuario.Nome,
                            Email = usuario.Email,
                            DataHoraAcesso = DateTime.Now
                        };

                        //Serializando o objeto AuthViewModel para JSON
                        var authJson = JsonConvert.SerializeObject(auth);


                        // Criando o conteúdo do cookie de autenticação (Identificação)
                        var identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, authJson)
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                        //gravando o cookie de autenticação
                        var principal = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Dashboard", "Principal");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado.";
                    }
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }

        //GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        //Post: Account/Register
        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Verificando se o e-mail já está cadastrado no banco de dados
                    var usuarioRepository = new UsuarioRepository();
                    if(usuarioRepository.GetByEmail(model.Email) != null)
                    {
                        // Gerando uma mensagem de erro na página
                        ModelState.AddModelError("Email", "O e-mail informado já está cadastrado para outro usuário.");
                    }
                    else
                    {
                        var usuario = new Usuario
                        {
                            Id = Guid.NewGuid(),
                            Nome = model.Nome,
                            Email = model.Email,
                            Senha = MD5Helper.Encrypt(model.Senha)
                        };

                        usuarioRepository.Add(usuario);

                        TempData["Mensagem"] = "Parabéns sua conta de usuário foi criada com sucesso.";

                        ModelState.Clear();
                    }
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }
            return View();
        }

        //GET: Account/PasswordRecover
        public IActionResult PasswordRecover()
        {
            return View();
        }

        //Post: Account/PasswordRecover
        [HttpPost]
        public IActionResult PasswordRecover(AccountPasswordRecoverViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //obter o usuárioo no banco de dados através do e-mail
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmail(model.Email);

                    // verificar se o usuario foi encontrado
                    if(usuario != null)
                    {
                        // gerando uma nova senha para o usuário
                        Faker faker = new Faker();
                        var novaSenha = $"@{faker.Internet.Password(8)}{new Random().Next(999)}";

                        // enviadndo um e-mail para o usuario
                        var emailMessageModel = new EmailMessageModel
                        {
                            EmailDestinatario = usuario.Email,
                            Assunto = "Recuperação de senha de usuário - Contas App",
                            Corpo = $"Prezado {usuario.Nome}, \n\nSua nova senha de acesso é: {novaSenha}\n\nAtt,\n\nEquipe Contas App."
                        };

                        //Enviando a mensagem
                        EmailMessageService.Send(emailMessageModel);

                        //Atualizando a senha no banco de dados
                        usuario.Senha = MD5Helper.Encrypt(novaSenha);
                        usuarioRepository.Update(usuario);


                        TempData["Mensagem"] = "Recuperação de senha realizada com sucesso. Verifique sua caixa de e-mail";
                        ModelState.Clear();
                    }
                    else
                    {
                        TempData["Mensagem"] = "Usuario não encontrado. Verifique o e-mail informado.";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //GET: Account/Logout
        public IActionResult Logout()
        {
            //Apagar o cookie de autenticação do AspNet
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //redirecionar para a página /Account/Login
            return RedirectToAction("Login", "Account");
        }
    }
}
