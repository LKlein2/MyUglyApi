using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyAPI.Controllers
{
    [Route("[controller]")]
    public class WeatherForecastController : MainController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        #region MARCADORES DO METODO
        //[HttpGet]                                                                     //Apenas diz que é um metodo GET
        //[Route("{id:int}")]                                                           //Apenas estabelece a rota do controller esperando um id inteiro como parametro
        //[Route("{id}")]                                                               //Apenas estabelece a rota do controller esperando um id de qualquer tipo como parametro
        //[HttpGet, Route("{id}")]                                                      //Especifica que é um metodo GET e espera um id de qualquer tipo como parametro
        //[HttpGet("ById/{id}")]                                                        //Especifica que é um get, estabelece a rota como ById/{id} esperando um id como parametro
        #endregion

        #region TIPOS DE ASSINATURA DO METODO
        //public IEnumerable<WeatherForecast> Get(int id)                               // Preparado para retornar apenas um IEnumerable de WeatherForecast
        //public ActionResult Get(int id)                                               // Preparado para retornar um codigo de resultado + um tipo (Ok(product), BadRequest()...)
        //public ActionResult<IEnumerable<WeatherForecast>> Get(int id)                 // Preparado para retornar tanto um IEnumerable de WeatherForecast quanto um codigo de resultado (Ok(), BadRequest()...)
        #endregion

        #region MARCADORES DOS PARAMETROS DO METODO
        //public ActionResult<IEnumerable<WeatherForecast>> Get([FromBody]int id)       //Busca o parametro do corpo da requisição, se for um tipo complexo(Classe), não é necessario ter o modificador especificando a origem.
        //public ActionResult<IEnumerable<WeatherForecast>> Get([FromRoute]int id)      //Busca o parametro da URI da requisição, se esta no marcador[HttpGet("{id:int}")], não precisa ter o modificador especificando a origem.
        //public ActionResult<IEnumerable<WeatherForecast>> Get([FromForm]int id)       //Busca o parametro do form-data do corpo da requisição
        //public ActionResult<IEnumerable<WeatherForecast>> Get([FromHeader]int id)     //Busca o parametro do header do corpo da requisição
        //public ActionResult<IEnumerable<WeatherForecast>> Get([FromQuery]int id)      //Busca o parametro da URI da requisição, da query string, quando o atributo não faz parte da rota.
        #endregion

        #region MARCADORES DE RESPONSE
        //Na hora de criar a documentação com o Swagger, a documentação é gerada com os tipos de retornos decorados nos marcadores de response.

        //[ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]         //Diz que pode retornar um JSON de produto com um codigo 201 em caso de sucesso
        //(...) return CreatedAtAction("Post", product);
        //(...) return CreatedAtAction(nameof(Post), product);

        //[ProducesResponseType(StatusCodes.Status400BadRequest)]                       //Diz que pode retornar um 400 em caso de erro
        //(...) return BadRequest();
        #endregion


        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<WeatherForecast>> Get(int id)
        {
            var rng = new Random();

            if (id > Summaries.Length -1)
            {
                //return BadRequest(); //Possível pois é um ActionResult
                //return BadRequest();
                return CustomResponse();
            }

            //OK(...) Retorna um 200
            //return Ok(Enumerable.Range(1, Summaries.Length).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray());

            return Ok(new WeatherForecast
            {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[id]

            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Faz coisas
            return CreatedAtAction(nameof(Post), product);
        }
    }

    //Reescrevemos nossa classe base para que os controller que serão criados posteriormente já sigam um padrão de comportamento estabelecido para toda a aplicação
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        //Resposta padrão para padronizar o dado recebido pelo client
        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = GetErrors()
            });
        }

        public bool ValidOperation()
        {
            //Minhas validações
            return true;
        }

        protected string GetErrors()
        {
            //Pega os erros
            return "";
        }
    }

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
