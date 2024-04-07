using Monitoring.Domain.Models;
using Monitoring.Domain.ValueObjects;

namespace Monitoring.Infrastructure.Extensions;

internal class InitialData
{

  public static IEnumerable<Product> Products =>
    [
      new Product()
        { Id = ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), Name = "IPhone X", Price = 500 },
      new Product()
        { Id = ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), Name = "Samsung 10", Price = 400 },
      new Product()
      {
        Id = ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), Name = "Huawei Plus", Price = 650
      },
      new Product()
        { Id = ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), Name = "Xiaomi Mi", Price = 450 }
    ];

}