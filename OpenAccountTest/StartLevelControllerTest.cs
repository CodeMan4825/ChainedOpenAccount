using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenAccount.Api.Controllers.Requests;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Repository.Infrastructure;

namespace OpenAccountTest
{
	public class StartLevelControllerTest : IClassFixture<RequestStartController>
	{
		public StartLevelControllerTest(RequestStartController fixture)
		{
			Fixture = fixture;
			var x = new DbContextOptionsBuilder<AppDbContext>();
			x.UseSqlServer("Data Source=192.168.5.110,1433;Initial Catalog=TestOA;User ID=sa;password=123456;multipleactiveresultsets=True;TrustServerCertificate=True;");
			_ = new AppDbContext(x.Options);
		}

		protected RequestStartController Fixture { get; }

		[Fact]
		internal async void Get()
		{
			var data = await Fixture.Get();

			Assert.Equal("http://blog2.com", data.ToString());
		}
	}
}
