namespace ConsoleApp1
{
    public class MyService
    {
        private readonly MyDbContext _context;

        public MyService(MyDbContext context)
        {
            _context = context;
        }

        public async Task DoWorkThatFailsAsync()
        {
            await _context.BeginTransactionAsync();
            try
            {
                throw new Exception("Error");
            }
            catch
            {
                await _context.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
