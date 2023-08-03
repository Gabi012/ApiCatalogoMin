using APICatalogoMin.ApiEndpoints;
using APICatalogoMin.AppServicesExtensions;
using APICatalogoMin.Context;
using APICatalogoMin.Models;
using APICatalogoMin.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

var app = builder.Build();

app.MapAutenticacaoEndpoints();
app.MapCategoriasEndpoints();
app.MapProdutosEndpoints();


var enviroment = app.Environment;

app.UseExceptionHandling(enviroment).UseSwaggerMiddleware().UseAppCors() ;

app.UseHttpsRedirection();
//nessa ordem
app.UseAuthentication();
app.UseAuthorization();
app.Run();
