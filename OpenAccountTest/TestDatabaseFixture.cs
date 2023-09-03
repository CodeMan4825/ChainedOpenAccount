using Microsoft.EntityFrameworkCore;
using OpenAccount.Repository.Infrastructure;

namespace OpenAccountTest
{
	public sealed class TestDatabaseFixture
	{
		private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=EFTestSample;Trusted_Connection=True";

		private static readonly object _lock = new();
		private static bool _databaseInitialized;

		public TestDatabaseFixture()
		{
			lock (_lock)
			{
				if (!_databaseInitialized)
				{
					using (var context = CreateContext())
					{
						context.Database.EnsureDeleted();
						context.Database.EnsureCreated();
						//context.AddRange(
						//	new Blog { Name = "Blog1", Url = "http://blog1.com" },
						//	new Blog { Name = "Blog2", Url = "http://blog2.com" });
						context.SaveChanges();
					}

					_databaseInitialized = true;
				}
			}
		}

		public AppDbContext CreateContext() => new(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options);
	}
}