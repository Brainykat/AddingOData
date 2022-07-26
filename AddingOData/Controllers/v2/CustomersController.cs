﻿using AddingOData.Models.V2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AddingOData.Controllers.v2
{
  [ApiVersion("2.0")]
  public class CustomersController : ODataController
  {
    private Customer[] _customers = new Customer[]
    {
            new Customer
            {
                Id = 11,
                ApiVersion = "v2.0",
                FirstName = "YXS",
                LastName = "WU",
                Email = "yxswu@abc.com"
            },
            new Customer
            {
                Id = 12,
                ApiVersion = "v2.0",
                FirstName = "KIO",
                LastName = "XU",
                Email = "kioxu@efg.com"
            }
    };

    [EnableQuery]
    public IActionResult Get()
    {
      return Ok(_customers);
    }

    [EnableQuery]
    public IActionResult Get(int key)
    {
      var customer = _customers.FirstOrDefault(c => c.Id == key);
      if (customer == null)
      {
        return NotFound($"Cannot find customer with Id={key}.");
      }

      return Ok(customer);
    }
  }
}
