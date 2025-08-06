using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.AspNetCore;
using ModelContextProtocol.Server;
using ModelContextProtocol.Protocol;
using System.ComponentModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

// Example HTTP-based MCP Server
// This can be used as an alternative to the stdio-based server
public class HttpMcpServer
{
    public static async Task RunHttpServerAsync(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging
        builder.Logging.AddConsole();

        // Configure MCP Server with HTTP transport and tools from assembly
        builder.Services
            .AddMcpServer()
            .WithHttpServerTransport()
            .WithToolsFromAssembly();

        var app = builder.Build();

        // Map MCP endpoints
        app.MapMcpServer();

        await app.RunAsync();
    }
}

// Example tools for HTTP server (same as stdio server)
[McpServerToolType]
public static class HttpExampleTools
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"HTTP Echo: {message}";

    [McpServerTool, Description("Adds two numbers together.")]
    public static int Add(int a, int b) => a + b;

    [McpServerTool, Description("Gets the current date and time.")]
    public static string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss UTC");

    [McpServerTool, Description("Reverses the provided text.")]
    public static string ReverseText(string text) => new string(text.Reverse().ToArray());

    [McpServerTool, Description("Counts the number of words in the provided text.")]
    public static int CountWords(string text) => text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
} 