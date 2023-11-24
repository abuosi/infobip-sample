namespace api.Models;

public record MessageTextModel(String Sender, String Recipient, String Message);

public record WhatsAppMessageText(String Sender, String Recipient, String Message);

public record WhatsAppMessageTemplate(String Sender, String Recipient, String TemplateName, String TemplateData);

public record EmailMessage(String Sender, String Recipient, String Subject, String Body);
