using StudentMinimalAPI.Data;
using StudentMinimalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using StudentMinimalAPI.Models.DTO;
using StudentMinimalAPI;
using AutoMapper;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add AutoMapper dependency injection
builder.Services.AddAutoMapper(typeof(MappingConfig));

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
app.MapGet("/api/GetAllStudents", (ILogger<Program> _logger) =>
{
   _logger.Log(LogLevel.Information, "Getting all students");
   return Results.Ok(StudentStore.StudentList);
}).WithName("StudentList").Produces<IEnumerable<Student>>(200);

app.MapGet("/api/GetStudentsList", (ILogger<Program> _logger) =>
{
    APIResponse response = new();
    _logger.Log(LogLevel.Information, "Getting all students");

    response.Result = StudentStore.StudentList;
    response.IsSuccess = true;
    response.StatusCode=HttpStatusCode.OK;
    
    return Results.Ok(response);
}).WithName("StudentListWithCustomResponse").Produces<IEnumerable<APIResponse >>(200);


app.MapGet("/api/GetStudentById/{id:int}", (int Id) =>
{
    return Results.Ok(StudentStore.StudentList.FirstOrDefault(s => s.Id==Id));
}).WithName("GetStudent")
  .Accepts<Int32>("application/json")
  .Produces<Student>(200);

app.MapPost("/api/SaveStudent", ([FromBody] Student student) => {
    if(student.Id !=0 || string.IsNullOrEmpty(student.Name))
    {
        return Results.BadRequest("Invalid Id or Student Name");
    }
    student.Id=StudentStore.StudentList.OrderByDescending(s => s.Id).FirstOrDefault().Id+1;
    StudentStore.StudentList.Add(student);
    //return Results.Created("Created",student);

    return Results.CreatedAtRoute("GetStudent", new {id=student.Id}, student);
}).WithName("CreateStudent")
  .Accepts<Student>("application/json")
  .Produces<Student>(201);

///Save Student with model/data hiding way and manual object mapping
app.MapPost("/api/SaveStudentManualMapping", ([FromBody] StudentEntity student_dto) => {
    if (string.IsNullOrEmpty(student_dto.Name))
    {
        return Results.BadRequest("Invalid Student Name");
    }

    Student student = new()
    {
        Id = StudentStore.StudentList.OrderByDescending(s => s.Id).FirstOrDefault().Id + 1,
        Name = student_dto.Name,
        Email=student_dto.Email,
        MobileNo=student_dto.MobileNo, 
    };
    StudentStore.StudentList.Add(student);

    return Results.CreatedAtRoute("GetStudent", new { id = student.Id }, student);
}).WithName("SaveStudentManualMapping")
  .Accepts<Student>("application/json")
  .Produces<StudentEntity>(201);

///Save Student with model/data hiding way and auto mapper mapping
app.MapPost("/api/SaveStudentAutoMapper", (IMapper _mapper, [FromBody] StudentEntity studentEntity) => {
    if (string.IsNullOrEmpty(studentEntity.Name))
    {
        return Results.BadRequest("Invalid Student Name");
    }

    Student student = _mapper.Map<Student>(studentEntity);
    student.Id = StudentStore.StudentList.OrderByDescending(s => s.Id).FirstOrDefault().Id + 1;
    StudentStore.StudentList.Add(student);

    return Results.CreatedAtRoute("GetStudent", new { id = student.Id }, student);
}).WithName("SaveStudentAutoMapper")
  .Accepts<Student>("application/json")
  .Produces<StudentEntity>(201);


app.MapPut("/api/UpdateStudent", () => "");

app.MapDelete("/api/DeleteStudent/{id:int}", (int id) => "");


// For API HTTPS configure
app.UseHttpsRedirection();
// For run minimal API
app.Run();
