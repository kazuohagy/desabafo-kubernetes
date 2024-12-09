namespace IntegraBrasilAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bancoController : ControllerBase
    {
        private readonly IntegraBrasilContext _context;

        public bancoController(IntegraBrasilContext context)
        {
            _context = context;
        }

        // GET: api/banco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<banco>>> Getbanco()
        {
            return await _context.banco.ToListAsync();
        }

        // GET: api/banco/5
        [HttpGet("{id}")]
        public async Task<ActionResult<banco>> Getbanco(int id)
        {
            var banco = await _context.banco.FindAsync(id);

            if (banco == null)
            {
                return NotFound();
            }

            return banco;
        }

        // PUT: api/banco/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putbanco(int id, banco banco)
        {
            if (id != banco.id)
            {
                return BadRequest();
            }

            _context.Entry(banco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bancoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/banco
        [HttpPost]
        public async Task<ActionResult<banco>> Postbanco(banco banco)
        {
            _context.banco.Add(banco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getbanco", new { id = banco.id }, banco);
        }

        // DELETE: api/banco/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<banco>> Deletebanco(int id)
        {
            var banco = await _context.banco.FindAsync(id);
            if (banco == null)
            {
                return NotFound();
            }

            _context.banco.Remove(banco);
            await _context.SaveChangesAsync();

            return banco;
        }

        private bool bancoExists(int id)
        {
            return _context.banco.Any(e => e.id == id);
        }
    }
}