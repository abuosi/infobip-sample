using System.Text.Json.Nodes;
using api.Models;
using api.Services;
using api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddUserSecrets<Program>().Build();

builder.Services.AddSingleton<Configuration>();
builder.Services.AddScoped<IWhatsAppServiceBasic, WhatsAppServiceBasic>();
builder.Services.AddScoped<IWhatsAppServiceSdk, WhatsAppServiceSdk>();
builder.Services.AddScoped<ISMSServiceBasic, SMSServiceBasic>();
builder.Services.AddScoped<IEmailServiceBasic, EmailServiceBasic>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/v1/basic/whatsapp", async (IWhatsAppServiceBasic whatsAppService, MessageTextModel whatsAppMessage) => { 
    return Results.Ok( JsonObject.Parse(await whatsAppService.SendText(whatsAppMessage)));
});

app.MapPost("/v1/basic/whatsapp/template", async (IWhatsAppServiceBasic whatsAppService, WhatsAppMessageTemplate whatsAppMessage) => { 
    return Results.Ok( JsonObject.Parse(await whatsAppService.SendTemplate(whatsAppMessage)));
});

app.MapPost("/v1/basic/sms", async (ISMSServiceBasic smsService, MessageTextModel smsMessage) => { 
    return Results.Ok(JsonObject.Parse(await smsService.SendText(smsMessage)));
});

app.MapPost("/v1/basic/email", async (IEmailServiceBasic emailService, EmailMessage emailMessage) => { 
    return Results.Ok(JsonObject.Parse(await emailService.SendEmail(emailMessage)));
});

app.MapPost("/v1/sdk/whatsapp", async (IWhatsAppServiceSdk whatsAppService, MessageTextModel whatsAppMessage) => { 
    return Results.Ok( JsonObject.Parse(await whatsAppService.SendText(whatsAppMessage)));
});

app.MapPost("/v1/sdk/whatsapp/template", async (IWhatsAppServiceSdk whatsAppService, WhatsAppMessageTemplate whatsAppMessage) => { 
    return Results.Ok( JsonObject.Parse(await whatsAppService.SendTemplate(whatsAppMessage)));
});

app.MapPost("/v1/sdk/sms", async (ISMSServiceBasic smsService, MessageTextModel smsMessage) => { 
    return Results.Ok(JsonObject.Parse(await smsService.SendText(smsMessage)));
});

app.MapPost("/v1/sdk/email", async (IEmailServiceBasic emailService, EmailMessage emailMessage) => { 
    return Results.Ok(JsonObject.Parse(await emailService.SendEmail(emailMessage)));
});

app.Run();
