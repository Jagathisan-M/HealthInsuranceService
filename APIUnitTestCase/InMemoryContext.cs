using HealthInsuranceAPI.HealthInsuranceDBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceUnitTestCase
{
    [ExcludeFromCodeCoverage]
    internal static class InMemoryContext
    {
        static HealthInsuranceContext Context;
        public static HealthInsuranceContext CreateContext
        {
            get
            {
                if (Context == null)
                {
                    var options = new DbContextOptionsBuilder<HealthInsuranceContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .UseInternalServiceProvider(
                                (IServiceProvider?)new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider()
                            )
                            .Options;

                    Context = new HealthInsuranceContext(options);
                }
                return Context;
            }
        }
    }
}
