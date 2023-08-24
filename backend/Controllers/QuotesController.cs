namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;
using WebApi.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{
    public QuotesController()
	{



	}
}
