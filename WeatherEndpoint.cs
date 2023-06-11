using Platform.Services;

namespace Platform;
public class WeatherEndpoint
{
    private IResponseFormatter formatter;

    public WeatherEndpoint(IResponseFormatter resFormatter)
    {
        formatter = resFormatter;
    }
    public async Task Endpoint(HttpContext context, IResponseFormatter formatter)
    {              
        await formatter.Format(context, "Endpoint Class: It is cloudy in Milan");       
    }
}