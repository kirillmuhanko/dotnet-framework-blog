using System;
using System.IO;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace BlogApplication.Configuration
{
    public static class FileServerConfig
    {
        public static void ConfigureApp(IAppBuilder app)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var physicalFileSystem = new PhysicalFileSystem(Path.Combine(baseDirectory, "wwwroot"));

            var fileServerOptions = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem,
                RequestPath = PathString.Empty,
                StaticFileOptions = {FileSystem = physicalFileSystem, ServeUnknownFileTypes = false}
            };

            app.UseFileServer(fileServerOptions);
        }
    }
}