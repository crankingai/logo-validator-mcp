using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
   // Do not interfere with MCP - send all logs to go to stderr
   consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();
await builder.Build().RunAsync();

#if false
[McpServerToolType]
public static class StringFormattingTool
{
   [McpServerTool, Description("Ensure input string is ALL CAPS. For example, message = 'Foo' returns 'FOO'")]
   public static string ValidateUrlStr(string message) => $"{message.ToUpper()}";
}
#endif

[McpServerToolType]
public static class LogoUrlValidatorTool
{
   [McpServerTool]
   [Description("Checks whether the URL resolves to a valid logo.")]
   public static async Task<bool> ValidateLogoUrl(string logo_url)
   {
      return await ImageValidator.IsValidImageUrl(logo_url);
   }
}
