# MCP Server

A .NET 8 Model Context Protocol (MCP) server implementation using the official ModelContextProtocol NuGet package.

## Features

- **Stdio Transport**: Standard input/output communication for CLI-based MCP clients
- **HTTP Transport**: Web-based communication for HTTP MCP clients
- **Example Tools**: Built-in tools for testing and demonstration
- **Automatic Tool Discovery**: Tools are automatically discovered and registered using attributes

## Available Tools

The server includes the following example tools:

- **Echo**: Echoes the message back to the client
- **Add**: Adds two numbers together
- **GetCurrentTime**: Returns the current date and time
- **ReverseText**: Reverses the provided text
- **CountWords**: Counts the number of words in the provided text

## Running the Server

### Stdio Transport (Default)

The server runs with stdio transport by default:

```bash
dotnet run
```

This creates a stdio-based MCP server that can be used with MCP clients that support stdio transport.

### HTTP Transport

To run the HTTP-based server, you can modify the `Program.cs` file to use the HTTP transport instead:

```csharp
// Replace the stdio configuration with HTTP configuration
builder.Services
    .AddMcpServer()
    .WithHttpServerTransport()  // Use HTTP transport
    .WithToolsFromAssembly();
```

## Adding Custom Tools

To add your own tools, create a static class with the `[McpServerToolType]` attribute and add methods with the `[McpServerTool]` attribute:

```csharp
[McpServerToolType]
public static class MyCustomTools
{
    [McpServerTool, Description("My custom tool description")]
    public static string MyCustomTool(string input) => $"Processed: {input}";
}
```

## Testing with MCP Clients

### Using with Claude Desktop

1. Start the server: `dotnet run`
2. In Claude Desktop, add a new MCP server:
   - Name: "My MCP Server"
   - Command: `dotnet`
   - Arguments: `run --project /path/to/McpServer`

### Using with HTTP Clients

If using HTTP transport, clients can connect to the server via HTTP endpoints.

## Dependencies

- **ModelContextProtocol**: Core MCP SDK for .NET
- **ModelContextProtocol.AspNetCore**: ASP.NET Core extensions for HTTP transport
- **Microsoft.Extensions.Hosting**: Dependency injection and hosting

## Configuration

The server uses the standard .NET configuration system. You can add configuration in `appsettings.json` or through environment variables.

## Logging

Logging is configured to output to stderr for stdio transport compatibility. Log levels can be adjusted in the configuration.

## Development

To build and run in development mode:

```bash
dotnet build
dotnet run
```

To run with hot reload:

```bash
dotnet watch run
```

## Package References

- `ModelContextProtocol` (0.3.0-preview.3)
- `ModelContextProtocol.AspNetCore` (0.3.0-preview.3)

## License

This project uses the MIT license, same as the ModelContextProtocol package. 