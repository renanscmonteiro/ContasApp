using ContasApp.Data.Entities;
using ContasApp.Data.Enums;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        // GET: /Categorias/Cadastro
        public IActionResult Cadastro()
        {
            // Carregando uma ViewBag com as opções que serão exibidas no campo DropDownList
            ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));

            return View();
        }

        //POST: /Categorias/Cadastro
        [HttpPost]
        public IActionResult Cadastro(CategoriasCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Capturando os dados do usuário autenticado através do cookie do AspNet
                    var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);

                    // preenchendo os dados da categoria para gravar no banco de dados
                    var categoria = new Categoria
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Tipo = model.Tipo,
                        UsuarioId = auth.Id // chave estrangeira do usuário
                    };

                    var categoriaRepository = new CategoriaRepository();
                    categoriaRepository.Add(categoria);

                    ModelState.Clear();

                    TempData["MensagemSucesso"] = $"Categoria {categoria.Nome}, cadastrada com sucesso";
                }

                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            // Carregando uma ViewBag com as opções que serão exibidas no campo DropDownList
            ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));
            return View();
        }

        // GET: /Categorias/Consulta
        public IActionResult Consulta()
        {
            var model = new List<CategoriasConsultaViewModel>();

            try
            {
                //Capturando os dados do usuário autenticado através do cookie do AspNet
                var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);

                var categoriaRepository = new CategoriaRepository();

                foreach (var item in categoriaRepository.GetByUsuario(auth.Id))
                {
                    model.Add(new CategoriasConsultaViewModel
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Tipo = item.Tipo
                    });
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            // Enviando a lista de categorias para a página
            return View(model);
        }

        //GET: /Categorias/Exclusao/{id}
        public IActionResult Exclusao(Guid id)
        {
            try
            {
                // buscar a categoria no banco de dados pelo Id
                var categoriaRepository = new CategoriaRepository();
                var categoria = categoriaRepository.GetById(id);

                //Excluindo a categoria
                categoriaRepository.Delete(categoria);

                TempData["MensagemSucesso"] = $"Categoria {categoria.Nome}, exlcuída com sucesso";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            //redirecionando de volta para a página de consulta
            return RedirectToAction("Consulta");
        }

        //GET: /Categorias/Edicao
        public IActionResult Edicao(Guid id)
        {
            var model = new CategoriasEdicaoViewModel();

            try
            {
                var categoriaReporistory = new CategoriaRepository();
                var categoria = categoriaReporistory.GetById(id);

                model.Id = categoria.Id;
                model.Nome = categoria.Nome;
                model.Tipo = categoria.Tipo;
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            // Carregando uma ViewBag com as opções que serão exibidas no campo DropDownList
            ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));
            return View(model);
        }

        //POST: /Categorias/Edicao
        [HttpPost]
        public IActionResult Edicao(CategoriasEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar a categoria no banco de dados, através do ID
                    var categoriaRepository = new CategoriaRepository();
                    var categoria = categoriaRepository.GetById(model.Id.Value);

                    categoria.Nome = model.Nome;
                    categoria.Tipo = model.Tipo;

                    //atualizar a categoria
                    categoriaRepository.Update(categoria);

                    TempData["MensagemSucesso"] = $"Categoria '{categoria.Nome}', atualizada com sucesso.";
                    return RedirectToAction("Consulta");

                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));
            return View(model);
        }
    }
}