﻿using DataEmploy.Base;
using DataEmploy.Models;
using DataEmploy.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataEmploy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmensController : BaseController<Departments, DepartmentsRepository, int>
    {
        public DepartmensController(DepartmentsRepository departmentsRepository) : base(departmentsRepository)
        {

        }
    }
}
