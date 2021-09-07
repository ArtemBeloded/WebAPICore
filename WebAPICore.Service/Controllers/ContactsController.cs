using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICore.DAL.DataBaseContext;
using WebAPICore.DAL.Models;

namespace WebAPICore.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {

        private readonly ContactContext db;
        public ContactsController(ContactContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult GetContacts() 
        {
            return Ok(db.Contacts.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetContactById(int id) 
        {
            var contact = db.Contacts.Find(id);
            if (contact == null) 
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
            var newContact = db.Contacts.Last();
            return Ok(newContact);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Contact contact) 
        {
            if(id != contact.Id) 
            {
                return BadRequest();
            }
            db.Entry(contact).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch 
            {
                if (db.Contacts.Find(id) == null) 
                {
                    return NotFound();
                }
                throw;
            }

            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var contact = db.Contacts.Find(id);
            if (contact == null) 
            {
                return NotFound();
            }
            db.Contacts.Remove(contact);
            db.SaveChanges();

            return Ok();
        }
    }
}
