using Microsoft.AspNetCore.HttpOverrides;

namespace toDoList.Extensions;

public static class ConifgureHttpRequestPipeline
{
    public static void ConfigurePipeline(this WebApplication app)
    {
        // global error with request delegate
        // app.UseMiddleware<GlobalErrorHandlerMiddleware>(); // adds the GlobalErrorHandlerMiddleware to the pipeline


        // global error with IExceptionHandler :
        // check the Servicee (Problem details & ExceptionHandler are injected in service )
        app.UseExceptionHandler();


        // No, HSTS (HTTP Strict Transport Security)
        // HSTS is a security policy mechanism that helps to protect websites against
        // man-in-the-middle attacks by ensuring that browsers only interact with the server over HTTPS.
        // It instructs browsers to automatically convert all HTTP requests to HTTPS and to refuse any connections to the server over HTTP.
        // 
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS 
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API ");
        });


        app.UseStaticFiles(); // Enable static files ( html , css , js , images .. )  to be served
        app.UseRouting(); // maps incoming requests to route handlers

        // ensures that the ASP.NET Core application behaves as if it were directly receiving requests from the client, even though it is behind a reverse proxy
        app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
        app.UseCors("CorsPolicy"); // allowing or blocking  requests from different origins ( cross-origin requests )

        app.UseAuthentication(); // handles authentication or identity of use by checking ( jwt tokens , cookies , ect ... ) 
        app.UseAuthorization(); // after authentication , checks if the user is authorized to access the requested resource


        app.MapControllers(); // adds the endpoint from controller actions to the IEndpointRouteBuilder
        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapControllers();
        // });

        // Run the App and block the calling thread until the host is shutdown
        // app.Run(); // terminal middleware 
    }
}
