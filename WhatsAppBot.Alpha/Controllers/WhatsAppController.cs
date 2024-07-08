using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WhatsAppBot.Alpha.Models;

namespace WhatsAppBot.Alpha.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class WhatsAppController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    
    public WhatsAppController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> ReceiveMessage([FromBody] dynamic data)
    {
        // Handle incoming message
        var message = data.entry[0].changes[0].value.messages[0];
        string from = message.from;
        string body = message.text.body;
        // Send response
        var responseMessage = new WhatsAppMessage
        {
            To = from,
            Text = new Text { Body = "Hello! You said: " + body }
        };

        var json = JsonSerializer.Serialize(responseMessage);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_configuration["GraphApiUrl"], content);

        response.EnsureSuccessStatusCode();
        
        return Ok();
    }
}