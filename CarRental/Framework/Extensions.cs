namespace CarRental.API.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app) 
            => app.UseMiddleware(typeof(ErrorHandlerMiddleware));
    }
}
