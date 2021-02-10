﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace efDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IUnitOfWork _unitofwork;

        public UserController(ApplicationDbContext context,
            IUnitOfWork unitofwork)
        {
            _context = context;
            _unitofwork = unitofwork;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            List<User> users = _unitofwork.Users.GetAll().ToList();
            //List<User> users = _unitofwork.Users.GetByFirstName("Peter").ToList();
            //List<User> users = _context.Users.ToList();
            return users;
        }

        GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {

            User user = _unitofwork.Users.Get(id);
            //User user = _context.Users.Find(id);
            return user;
        }

        // GET api/<UserController>/5
        //[HttpGet("{firstName}")]
        //public User GetFirst(string firstName)
        //{
        //    //User user = _unitofwork.Users.Get(id);
        //    User user = _unitofwork.Users.GetOneByFirstName(firstName);
        //    //User user = _context.Users.Find(id);
        //    return user;
        //}

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]User user)
        {
            if( id != user.UserId)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            User user = _context.Users.Find(id);

            if(user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
