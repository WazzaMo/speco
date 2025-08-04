using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = Host.CreateApplicationBuilder(args);

// Configure logging to output to stderr for stdio transport
builder.Logging.AddConsole(consoleLogOptions =>
{
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

// Configure MCP Server with stdio transport and tools from assembly
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();

// Example tools that will be automatically discovered and registered
[McpServerToolType]
public static class ExampleTools
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"Echo: {message}";

    [McpServerTool, Description("Adds two numbers together.")]
    public static int Add(int a, int b) => a + b;

    [McpServerTool, Description("Gets the current date and time.")]
    public static string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    [McpServerTool, Description("Reverses the provided text.")]
    public static string ReverseText(string text) => new string(text.Reverse().ToArray());

    [McpServerTool, Description("Counts the number of words in the provided text.")]
    public static int CountWords(string text) => text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
}
