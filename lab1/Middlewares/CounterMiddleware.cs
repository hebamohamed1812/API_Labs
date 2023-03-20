public class CounterMiddleware
{
    private readonly RequestDelegate _next;
    private int _counter = 0;

    public CounterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        _counter++;
        Console.WriteLine($"Request count: {_counter}");
        await _next(context);
    }
}