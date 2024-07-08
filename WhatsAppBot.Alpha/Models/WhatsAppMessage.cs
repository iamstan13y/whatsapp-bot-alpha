using static System.Net.Mime.MediaTypeNames;

namespace WhatsAppBot.Alpha.Models;

public class WhatsAppMessage
{
    public string MessagingProduct { get; set; } = "whatsapp";
    public string To { get; set; }
    public string Type { get; set; } = "text";
    public Text Text { get; set; }
}