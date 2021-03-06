﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo_Team_App.Data;
using ToDo_Team_App.Models;

namespace ToDo_Team_App.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase {
        private readonly ToDo_Team_AppContext _context;

        public ToDosController(ToDo_Team_AppContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDo() {
            return await _context.ToDos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDo(int id) {
            var toDo = await _context.ToDos.FindAsync(id);

            if (toDo == null) {
                return NotFound();
            }

            return toDo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo(int id, ToDo toDo) {
            if (id != toDo.Id) {
                return BadRequest();
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ToDoExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ToDo>> PostToDo(ToDo toDo) {
            _context.ToDos.Add(toDo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDo>> DeleteToDo(int id) {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null) {
                return NotFound();
            }

            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();

            return toDo;
        }

        private bool ToDoExists(int id) {
            return _context.ToDos.Any(e => e.Id == id);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetOwnTodos(int assignedUserId) {
            var todo = await _context.ToDos.Where(r => r.UserId != assignedUserId).ToListAsync();

            if (todo == null) {
                return NotFound();
            }

            return todo;
        }
        public const string StatusApproved = "Approved";
        public const string StatusRejected = "Rejected";

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id, ToDo todo) {
            todo.Status = StatusApproved;
            return await PutToDo(id, todo);
        }
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(int id, ToDo todo) {
            todo.Status = StatusRejected;
            return await PutToDo(id, todo);
        }
    }
}
