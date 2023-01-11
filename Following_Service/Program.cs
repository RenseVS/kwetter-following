using Following_Service.Services;
using Following_Service.Services.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<FollowerService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TweetMadeConsumer>();

    x.UsingRabbitMq((cxt, cfg) =>
    {
        cfg.Host(new Uri("amqps://ckgicxje:3GxKLqpiE0FSyqfHALy-ch1iYx5TeJ0E@kangaroo.rmq.cloudamqp.com/ckgicxje"), h =>
        {
            h.Username("ckgicxje");
            h.Password("3GxKLqpiE0FSyqfHALy-ch1iYx5TeJ0E");
        });

        cfg.ReceiveEndpoint("Following_Service", c =>
        {
            c.ConfigureConsumer<TweetMadeConsumer>(cxt);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

