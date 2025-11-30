namespace FNT_Security_Api.Endpoints
{
    public static class MenuEndPoint
    {
        public static RouteGroupBuilder MapMenu(this IEndpointRouteBuilder app)
        {

            var api = app.MapGroup("/v1/menus");
            api.MapPost("/", async ([FromServices] MenuHandler Handler, [FromBody] MenuDTO data) => await Handler.AddHandler(data));
            api.MapPut("/", async ([FromServices] MenuHandler Handler, [FromBody] MenuDTO data) => await Handler.PutHandler(data));
            api.MapGet("/all-active", async ([FromServices] MenuHandler Handler) => await Handler.GetAllActive());
            api.MapGet("/id", async ([FromServices] MenuHandler Handler, [FromQuery] string id) => await Handler.GetById(id));
            api.MapGet("/all", async ([FromServices] MenuHandler Handler) => await Handler.GetAll());            

            return api;
        }
    }
    [JsonSerializable(typeof(MenuDTO))]
    [JsonSerializable(typeof(List<MenuDTO>))]
    internal partial class MenuSerializerContext : JsonSerializerContext
    {

    }
}
