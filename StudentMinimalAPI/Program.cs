using StudentMinimalAPI.Data;
using StudentMinimalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Very basic Get/Post for Minimal API.
app.MapGet("/Welcome", () => "Welcome to .NET Core Minimal API");
app.MapPost("/Greetings", (String Name) => Results.Ok("Nice to meet you, "+Name));

// Model based Get/Post for Minimal API.
app.MapGet("/api/GetAllStudents", () =>
{
   return Results.Ok(StudentStore.StudentList);
}).WithName("StudentList");

app.MapGet("/api/GetStudentById/{id:int}", (int Id) =>
{
    return Results.Ok(StudentStore.StudentList.FirstOrDefault(s => s.Id==Id));
}).WithName("GetStudent");

app.MapPost("/api/SaveStudent", ([FromBody] Student student) => {
    if(student.Id !=0 || string.IsNullOrEmpty(student.Name))
    {
        return Results.BadRequest("Invalid Id or Student Name");
    }
    student.Id=StudentStore.StudentList.OrderByDescending(s => s.Id).FirstOrDefault().Id+1;
    StudentStore.StudentList.Add(student);
    //return Results.Created("Created",student);

    return Results.CreatedAtRoute("GetStudent", new {id=student.Id}, student);
}).WithName("CreateStudent"); 

app.MapPut("/api/UpdateStudent", () => "");

app.MapDelete("/api/DeleteStudent/{id:int}", (int id) => "");


// For API HTTPS configure
app.UseHttpsRedirection();
// For run minimal API
app.Run();