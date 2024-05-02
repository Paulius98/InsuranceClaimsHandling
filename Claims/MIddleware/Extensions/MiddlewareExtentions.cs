namespace Claims.MIddleware.Extensions;

public static class MiddlewareExtentions
{
    public static void UseErrorHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
