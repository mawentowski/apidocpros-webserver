using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace ReleaseWebServer
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            const string CACHE_CONTROL_HEADER = "Cache-Control";
            const string ERROR_PAGE_PATH = "/Home/Error";
            const string INDEX = "index.html";

            
            applicationBuilder.UseExceptionHandler(ERROR_PAGE_PATH);
            //applicationBuilder.UseHsts();
            

            //applicationBuilder.UseHttpsRedirection();

            applicationBuilder.UseRewriter(
                new RewriteOptions().Add(
                    (RewriteContext context) =>
                    {
                        HttpRequest request = context.HttpContext.Request;
                        /* If the request's path targets neither the error page nor a static
                         * file by name, (e.g., "search-page" rather than "main123.js") rewrite
                         * request path to target index.html.
                         */
                        if (!request.Path.Equals(ERROR_PAGE_PATH) &&
                            new Regex(@"^[^.\n]*?(?:=.*)?$").IsMatch(request.Path))
                            request.Path = $"/{INDEX}";
                    }
                )
            );

            applicationBuilder.UseStaticFiles(
                new StaticFileOptions
                {
                    OnPrepareResponse = context =>
                    {
                        /* Indicate that these files may be placed into any HTTP cache, but they must not be served from that
                         * cache unless they are validated as fresh by the web server. For some web browsers such as Chrome, HTTP
                         * requests for service worker scripts are not eligible to be fulfilled by the browser's HTTP cache.
                         * So this step is moot for the ngsw-worker.js file for those browsers, but it is a good precaution to
                         * take for other HTTP cache owners.
                         */
                        if (context.File.Name == INDEX)
                            context.Context.Response.Headers.Add(CACHE_CONTROL_HEADER, "no-cache");
                        /* Other static files should either be named to support cache busting or will
                         * not change (e.g., Duke Energy logo image). Indicate that these may be cached
                         * for the commonly recommended maximum period, 1 year.
                         */
                        else
                            context.Context.Response.Headers.Add(CACHE_CONTROL_HEADER, "public, max-age=31536000, immutable");
                    },
                }
            );

            applicationBuilder.UseRouting();

            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
