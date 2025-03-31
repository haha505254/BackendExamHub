using BackendExamHub.Services;

var builder = WebApplication.CreateBuilder(args);

// �K�[�A�Ȩ�e��
builder.Services.AddControllers();
builder.Services.AddSingleton<SqlDbService>();

// �t�m Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "BackendExamHub API",
        Version = "v1",
        Description = "Backend exam API for Mercury Engineering"
    });
});

var app = builder.Build();

// �t�m HTTP �ШD�޹D
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();