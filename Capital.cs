namespace Platform;

public class Capital
{
    public static async Task Endpoint(HttpContext context)
    {
        string? capital = null;
        string? country = context.Request.RouteValues["country"] as string;
        switch ((country ?? "").ToLower())
        {
            case "uk":
                capital = "London";
                break;
            case "france":
                capital = "Paris";
                break;
            case "monaco":
                //context.Response.Redirect($"/population/{country}");
                //URL адреса генерируютс€ с использованием класса LinkGenerator, экземпл€р данного класса
                //можно получить с использованием DI, как в данном случае
                LinkGenerator? generator = context.RequestServices.GetService<LinkGenerator>();
                string? url = generator?.GetPathByRouteValues(context,
                "population", new { city = country });
                if (url != null)
                {
                    context.Response.Redirect(url);
                }
                return;
        }
        if (capital != null)
        {
            await context.Response.WriteAsync($"{capital} is the capital of {country}");
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
    }
}